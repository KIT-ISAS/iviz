#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using IDragHandler = Iviz.Core.IDragHandler;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Iviz.App
{
    public sealed class GuiInputModule : MonoBehaviour, IHasFrame, IDragHandler
    {
        const float MainSpeed = 2f;
        const float MainAccel = 5f;
        const float BrakeCoeff = 0.9f;
        const float LookAtAnimationTime = 0.3f;

        /// Weights for keyboard movements. Forward is slower.
        static Vector3 MoveDirectionWeight => new(1.5f, 1.5f, 1);

        static GuiInputModule? instance;
        static uint tapSeq;

        static bool IsDraggingAllowed =>
            Settings.IsXR || (Settings.IsPhone
                //? Input.touchCount == 1
                ? Touch.activeTouches.Count == 1
                : Mouse.current.rightButton.isPressed);

        static Camera MainCamera => Settings.MainCamera;

        public static GuiInputModule Instance => MainCamera.EnsureHasComponent(ref instance, nameof(MainCamera));

        Vector3 accel;
        Vector3 velocity;
        bool pointerIsAlreadyMoving;
        bool alreadyScaling;
        TfFrame? cameraViewOverride;
        IScreenDraggable? draggedObject;

        bool pointerMotionIsInvalid;

        CancellationTokenSource? lookAtTokenSource;

        Light? mainLight;

        Vector3 orbitCenter;
        TfFrame? orbitCenterOverride;

        float orbitRadius = 5.0f;
        Vector2 pointerDownStart;
        float pointerDownTime;
        bool pointerIsOnGui;

        bool pointerIsDown;
        bool altPointerIsDown;

        Vector2 pointerPosition;
        Vector2 altPointerPosition;
        Vector2 lastPointerPosition;
        Vector2 lastAltPointerPosition;

        float distancePointerAndAlt;
        float lastAltDistancePointerAndAlt;

        LeashDisplay? leash;
        Transform? mTransform;

        float cameraYaw;
        float cameraPitch = 45;
        float cameraRoll;

        Color backgroundColor = new(0.145f, 0.168f, 0.207f, 1);
        float sunDirectionX;
        float sunDirectionY;
        bool enableSun = true;
        float equatorIntensity = 0.5f;

        LeashDisplay Leash => ResourcePool.RentChecked(ref leash);
        Transform Transform => this.EnsureHasTransform(ref mTransform);
        bool IsLookAtAnimationRunning => lookAtTokenSource is { IsCancellationRequested: false };
        Quaternion OrbitRotation => Quaternion.Euler(cameraPitch, cameraYaw, cameraRoll);

        Light MainLight => mainLight != null
            ? mainLight
            : (mainLight = GameObject.Find("MainLight").AssertHasComponent<Light>(nameof(mainLight)));

        IScreenDraggable? DraggedObject
        {
            get => draggedObject;
            set
            {
                if (draggedObject == value)
                {
                    return;
                }

                try
                {
                    draggedObject?.OnEndDragging();
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during {nameof(IScreenDraggable.OnEndDragging)}", e);
                }

                draggedObject = value;

                try
                {
                    draggedObject?.OnStartDragging();
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during {nameof(IScreenDraggable.OnStartDragging)}", e);
                }

                Leash.Visible = draggedObject != null;
            }
        }

        public event Action<ClickHitInfo>? LongClick;

        public TfFrame? OrbitCenterOverride
        {
            get => IsLookAtAnimationRunning ? null : orbitCenterOverride;
            set
            {
                orbitCenterOverride = value;
                if (value != null)
                {
                    CameraViewOverride = null;
                    LookAt(value.Transform);
                }

                if (ModuleListPanel.TryGetInstance() is { } moduleListPanel)
                {
                    moduleListPanel.UnlockButtonVisible = value != null;
                }
            }
        }

        public TfFrame? CameraViewOverride
        {
            get => cameraViewOverride;
            set
            {
                cameraViewOverride = value;
                if (value != null)
                {
                    OrbitCenterOverride = null;
                }

                if (ModuleListPanel.TryGetInstance() is { } moduleListPanel)
                {
                    moduleListPanel.UnlockButtonVisible = value != null;
                }
            }
        }

        public Vector3 CameraRpy
        {
            get => new(cameraRoll, cameraPitch, cameraYaw);
            set => (cameraRoll, cameraPitch, cameraYaw) = UnityUtils.RegularizeRpy(value);
        }

        public Vector3 CameraPosition
        {
            get => Transform.localPosition;
            set
            {
                if (OrbitCenterOverride != null || CameraViewOverride != null)
                {
                    return;
                }

                Transform.localPosition = value;
                if (Settings.IsPhone)
                {
                    orbitCenter = value + orbitRadius * OrbitRotation.Forward();
                }
            }
        }

        public float CameraFieldOfView
        {
            get => Settings.VirtualCamera == null ? 0 : Settings.VirtualCamera.GetHorizontalFov();
            set
            {
                if (Settings.VirtualCamera != null)
                {
                    Settings.VirtualCamera.SetHorizontalFov(value);
                }
            }
        }

        public TfFrame Frame => TfModule.FixedFrame;

        public bool EnableSun
        {
            get => enableSun;
            set
            {
                enableSun = value;
                MainLight.enabled = value;
            }
        }
        
        public float SunDirectionX
        {
            set
            {
                sunDirectionX = value;
                UpdateSunDirection();
            }
        }

        public float SunDirectionY
        {
            set
            {
                sunDirectionY = value;
                UpdateSunDirection();
            }
        }

        public float EquatorIntensity
        {
            set
            {
                equatorIntensity = value;
                UpdateLighting();
            }
        }

        void UpdateSunDirection()
        {
            MainLight.transform.rotation = Quaternion.Euler(90 + sunDirectionX, sunDirectionY, 0);
        }

        public bool EnableShadows
        {
            get => MainLight.shadows != LightShadows.None;
            set => MainLight.shadows = value ? LightShadows.Soft : LightShadows.None;
        }

        public QualityType QualityType
        {
            set
            {
                QualitySettings.SetQualityLevel((int)value, true);

                // update materials
                bool isVeryLow = value == QualityType.VeryLow;
                if (Settings.UseSimpleMaterials != isVeryLow)
                {
                    Settings.UseSimpleMaterials = isVeryLow;
                    foreach (var display in GetAllRootChildren().WithComponent<MeshMarkerDisplay>())
                    {
                        display.UpdateMaterial();
                    }
                }

                // update main light
                if (isVeryLow)
                {
                    MainLight.renderMode = LightRenderMode.ForceVertex;
                    MainLight.intensity = Settings.IsHololens ? 2 : 1;
                    MainLight.shadows = LightShadows.None;
                }
                else
                {
                    MainLight.renderMode = LightRenderMode.Auto;
                    MainLight.intensity = 1;
                    MainLight.shadows = LightShadows.Soft;
                }
            }
        }

        public int TargetFps
        {
            set => Application.targetFrameRate = value;
        }

        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;

                Color colorToUse = Settings.IsHololens ? Color.black : value;
                MainCamera.backgroundColor = colorToUse.WithAlpha(0);
                UpdateLighting();
            }
        }

        void UpdateLighting()
        {
            RenderSettings.ambientSkyColor = backgroundColor.WithAlpha(0);
            Color.RGBToHSV(backgroundColor, out float h, out float s, out _);
            var equatorColor = Color.HSVToRGB(h, Mathf.Min(s, 0.3f),  equatorIntensity);
            RenderSettings.ambientEquatorColor = equatorColor;            
        }

        public int NetworkFrameSkip
        {
            set => GameThread.NetworkFrameSkip = value;
        }


        void Awake()
        {
            EnhancedTouchSupport.Enable();
        }

        void OnDestroy()
        {
            instance = null;
            TfModule.AfterProcessFrames -= ProcessPoseChanges;
            GameThread.EveryFrame -= ProcessPointer;
            TfModule.ResetFrames -= OnResetFrames;
        }

        void Start()
        {
            if (!Settings.IsPhone)
            {
                QualitySettings.vSyncCount = 0;
            }

            Settings.DragHandler = this;

            BackgroundColor = BackgroundColor;

            Leash.Color = Color.white.WithAlpha(0.75f);

            TfModule.ResetFrames += OnResetFrames;

            if (Settings.IsXR)
            {
                return;
            }

            StartOrbiting();

            if (ModuleListPanel.TryGetInstance() is { } moduleListPanel)
            {
                TfModule.AfterProcessFrames += ProcessPoseChanges;
                moduleListPanel.UnlockButton.onClick.AddListener(DisableCameraLock);
            }
            else
            {
                GameThread.EveryFrame += ProcessPoseChanges;
            }
        }

        void LateUpdate()
        {
            ProcessPointer();
        }

        void ProcessPoseChanges()
        {
            if (!this)
            {
                return;
            }

            if (CameraViewOverride != null)
            {
                Transform.SetPose(CameraViewOverride.AbsoluteUnityPose);
            }

            var mOrbitCenterOverride = OrbitCenterOverride;

            if (DraggedObject != null)
            {
                if (!Settings.IsPhone && mOrbitCenterOverride == null)
                {
                    ProcessFlying();
                    Transform.localPosition += Transform.localRotation * velocity;
                }

                return;
            }

            if (Settings.IsPhone)
            {
                if (mOrbitCenterOverride != null)
                {
                    ProcessOrbiting();
                    ProcessScaling(false);
                    orbitCenter = mOrbitCenterOverride.AbsoluteUnityPose.position; // !
                }
                else
                {
                    ProcessOrbiting();
                    ProcessScaling(true);
                }

                ProcessTurning();
                ProcessFlying();

                var rotation = OrbitRotation;
                Transform.SetLocalPose(-orbitRadius * rotation.Forward() + orbitCenter + rotation * velocity, rotation);
                return;
            }

            if (mOrbitCenterOverride != null)
            {
                ProcessOrbiting();

                var rotation = OrbitRotation;
                orbitCenter = mOrbitCenterOverride.AbsoluteUnityPose.position;
                Transform.SetLocalPose(-orbitRadius * rotation.Forward() + orbitCenter, rotation);
            }
            else
            {
                ProcessTurning();
                ProcessFlying();

                var rotation = OrbitRotation;
                Transform.localRotation = rotation;
                Transform.localPosition += rotation * velocity;
            }
        }

        void OnEnable()
        {
            GameThread.EveryFrame -= ProcessPointer;
        }

        void OnDisable()
        {
            GameThread.EveryFrame += ProcessPointer;
        }

        void OnResetFrames()
        {
            CameraViewOverride = null;
            OrbitCenterOverride = null;
        }

        public void DisableCameraLock()
        {
            CameraViewOverride = null;
            OrbitCenterOverride = null;
        }

        public float XRDraggableNearDistance => Settings.IsXR ? XRController.NearDistance : float.MaxValue;

        public void TryUnsetDraggedObject(IScreenDraggable draggable)
        {
            if (DraggedObject != draggable)
            {
                return;
            }

            DraggedObject = null;
        }

        public void TrySetDraggedObject(IScreenDraggable draggable)
        {
            if (!IsDraggingAllowed)
            {
                // reject!
                return;
            }

            Instance.DraggedObject = draggable;
        }

        void ProcessPointer()
        {
            const float shortClickTime = 0.3f;
            const float longClickTime = 0.5f;
            const float maxDistanceForClickEvent = 20;

            if (Settings.IsXR) // XR doesn't need cursor management
            {
                return;
            }

            bool prevPointerDown = !Settings.IsPhone
                ? pointerIsDown
                : pointerIsDown | altPointerIsDown;

            if (Settings.IsPhone)
            {
                var activeTouches = Touch.activeTouches;

                pointerIsDown = activeTouches.Count == 1;
                altPointerIsDown = activeTouches.Count == 2;

                if (altPointerIsDown)
                {
                    altPointerPosition = (activeTouches[0].screenPosition + activeTouches[1].screenPosition) / 2;
                }

                distancePointerAndAlt = altPointerIsDown
                    ? Vector2.Distance(activeTouches[0].screenPosition, activeTouches[1].screenPosition)
                    : 0;
                if (pointerIsDown || altPointerIsDown)
                {
                    pointerPosition = activeTouches[0].screenPosition;

                    if (!prevPointerDown)
                    {
                        pointerIsOnGui = IsPointerOnGui(pointerPosition) ||
                                         altPointerIsDown && IsPointerOnGui(altPointerPosition);
                        pointerDownTime = Time.time;
                        pointerDownStart = activeTouches[0].screenPosition;
                    }
                }
            }
            else
            {
                pointerIsDown = Mouse.current.rightButton.isPressed;

                if (pointerIsDown)
                {
                    pointerPosition = Mouse.current.position.ReadValue();

                    if (!prevPointerDown)
                    {
                        pointerIsOnGui = IsPointerOnGui(pointerPosition);
                        pointerDownTime = Time.time;
                        pointerDownStart = pointerPosition;
                    }
                }
                else
                {
                    pointerIsOnGui = false;
                }
            }

            // are we dragging?
            if (!pointerIsDown)
            {
                DraggedObject = null;
            }

            if (DraggedObject != null)
            {
                var pointerRay = MainCamera.ScreenPointToRay(pointerPosition);
                DraggedObject.OnPointerMove(pointerRay);
                if (DraggedObject.ReferencePoint is { } referencePoint
                    && DraggedObject.ReferenceNormal is { } referenceNormal)
                {
                    Leash.Set(pointerRay, referencePoint, referenceNormal);
                }

                return;
            }

            // check if we are clicking something interesting
            bool anyPointerDown = pointerIsDown || altPointerIsDown;
            if (prevPointerDown
                && !anyPointerDown
                && Vector2.Distance(pointerPosition, pointerDownStart) < maxDistanceForClickEvent)
            {
                if (IsPointerOnGui(pointerPosition)) // pointerIsOnGui is only set on button down
                {
                    return;
                }

                var clickInfo = new ClickHitInfo(pointerPosition);
                float timeDown = Time.time - pointerDownTime;
                if (timeDown < shortClickTime)
                {
                    OnClick(clickInfo, true);
                }
                else if (timeDown > longClickTime)
                {
                    LongClick?.Invoke(clickInfo);
                    OnClick(clickInfo, false);
                }
            }
        }

        static bool IsPointerOnGui(Vector2 pointerPosition)
        {
            var eventSystem = EventSystem.current;
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(new PointerEventData(eventSystem) { position = pointerPosition }, results);
            return results.Any(result => result.gameObject.layer == LayerType.UI);
        }

        void StartOrbiting()
        {
            var diff = orbitCenter - Transform.localPosition;
            orbitRadius = Mathf.Min(5, diff.Magnitude());

            var (diffX, diffY, diffZ) = diff;
            cameraYaw = Mathf.Atan2(diffX, diffZ) * Mathf.Rad2Deg;
            cameraPitch = -Mathf.Atan2(diffY, Mathf.Sqrt(diffX * diffX + diffZ * diffZ)) * Mathf.Rad2Deg;

            var rotation = OrbitRotation;
            Transform.SetLocalPose(-orbitRadius * rotation.Forward() + orbitCenter, rotation);
        }

        void ProcessOrbiting()
        {
            if (!pointerIsDown)
            {
                pointerIsAlreadyMoving = false;
                pointerMotionIsInvalid = false;
                return;
            }

            if (!pointerIsAlreadyMoving && pointerIsOnGui)
            {
                pointerMotionIsInvalid = true;
                return;
            }

            if (pointerMotionIsInvalid)
            {
                return;
            }

            var (pointerDiffX, pointerDiffY) = pointerIsAlreadyMoving
                ? pointerPosition - lastPointerPosition
                : Vector2.zero;

            lastPointerPosition = pointerPosition;
            pointerIsAlreadyMoving = true;

            const float orbitCoeff = 0.1f;
            const float orbitRadiusAdvance = 0.1f;
            const float minPitchInDeg = -90;
            const float maxPitchInDeg = 90;

            cameraYaw += pointerDiffX * orbitCoeff;
            cameraPitch -= pointerDiffY * orbitCoeff;
            cameraPitch = Mathf.Clamp(cameraPitch, minPitchInDeg, maxPitchInDeg);

            if (Keyboard.current[Key.W].isPressed)
            {
                orbitRadius = Mathf.Max(0, orbitRadius - orbitRadiusAdvance);
            }
            else if (Keyboard.current[Key.S].isPressed)
            {
                orbitRadius += orbitRadiusAdvance;
            }
        }

        void ProcessScaling(bool allowPivotMotion)
        {
            if (!altPointerIsDown)
            {
                alreadyScaling = false;
                return;
            }

            Vector2 pointerAltDiff;
            float altDistanceDiff;
            if (alreadyScaling && !pointerIsOnGui)
            {
                pointerAltDiff = altPointerPosition - lastAltPointerPosition;
                altDistanceDiff = distancePointerAndAlt - lastAltDistancePointerAndAlt;
            }
            else
            {
                pointerAltDiff = Vector2.zero;
                altDistanceDiff = 0;
            }

            lastAltPointerPosition = altPointerPosition;
            lastAltDistancePointerAndAlt = distancePointerAndAlt;

            alreadyScaling = true;

            const float radiusCoeff = -0.0025f;
            const float tangentCoeff = 0.001f;
            const float minOrbitRadius = 0.1f;
            const float minDistanceToAdvance = 0.5f;

            orbitRadius += altDistanceDiff * radiusCoeff;

            Quaternion q = Transform.localRotation;

            if (!allowPivotMotion)
            {
                if (orbitRadius < minOrbitRadius)
                {
                    orbitRadius = minOrbitRadius;
                }
            }
            else
            {
                if (orbitRadius < minDistanceToAdvance)
                {
                    // move forward
                    orbitCenter += (minDistanceToAdvance - orbitRadius) * q.Forward();
                    orbitRadius = minDistanceToAdvance;
                }

                float orbitScale = 0.75f * orbitRadius;
                orbitCenter -= tangentCoeff * pointerAltDiff.x * orbitScale * (q * Vector3.right);
                orbitCenter += tangentCoeff * pointerAltDiff.y * orbitScale * (q * Vector3.down);
            }
        }

        void ProcessTurning()
        {
            if (!pointerIsDown)
            {
                pointerIsAlreadyMoving = false;
                pointerMotionIsInvalid = false;
                return;
            }

            if (!pointerIsAlreadyMoving && pointerIsOnGui)
            {
                pointerMotionIsInvalid = true;
                return;
            }

            if (pointerMotionIsInvalid)
            {
                return;
            }

            var (pointerDiffX, pointerDiffY) = pointerIsAlreadyMoving
                ? pointerPosition - lastPointerPosition
                : Vector2.zero;

            lastPointerPosition = pointerPosition;
            pointerIsAlreadyMoving = true;

            const float turnCoeff = 0.1f;
            const float minPitchInDeg = -89;
            const float maxPitchInDeg = 89;

            cameraYaw += pointerDiffX * turnCoeff;
            cameraPitch -= pointerDiffY * turnCoeff;
            cameraPitch = Mathf.Clamp(cameraPitch, minPitchInDeg, maxPitchInDeg);
        }

        void ProcessFlying()
        {
            if (!pointerIsDown)
            {
                accel = Vector3.zero;
                velocity = Vector3.zero;
                return;
            }

            var baseInput = GetBaseInput();
            float deltaTime = Time.deltaTime;
            const float minAccelToStop = 0.001f;

            accel += baseInput.Mult(MainAccel * MoveDirectionWeight) * deltaTime;

            if (baseInput.x == 0)
            {
                accel.x *= BrakeCoeff;
                if (Mathf.Abs(accel.x) < minAccelToStop)
                {
                    accel.x = 0;
                }
            }

            if (baseInput.y == 0)
            {
                accel.y *= BrakeCoeff;
                if (Mathf.Abs(accel.y) < minAccelToStop)
                {
                    accel.y = 0;
                }
            }

            if (baseInput.z == 0)
            {
                accel.z *= BrakeCoeff;
                if (Mathf.Abs(accel.z) < minAccelToStop)
                {
                    accel.z = 0;
                }
            }

            velocity = deltaTime * (baseInput.Mult(MainSpeed * MoveDirectionWeight) + accel);
        }

        public void LookAt(Transform targetTransform, Vector3? localOffset = null)
        {
            lookAtTokenSource?.Cancel();
            CancellationTokenSource newToken = new();
            lookAtTokenSource = newToken;

            var lookAtCameraStartPosition = CameraPosition;

            void Update(float t)
            {
                var targetPosition = localOffset is { } validatedOffset
                    ? targetTransform.TransformPoint(validatedOffset)
                    : targetTransform.position;

                var lookAtCameraTargetPosition = CalculateTargetCameraPosition(targetPosition);
                var currentPosition =
                    Vector3.Lerp(lookAtCameraStartPosition, lookAtCameraTargetPosition, Mathf.Sqrt(t));
                CameraPosition = currentPosition;
            }

            void Dispose()
            {
                newToken.Cancel();
                if (OrbitCenterOverride != null)
                {
                    StartOrbiting();
                }
            }

            FAnimator.Spawn(lookAtTokenSource.Token, LookAtAnimationTime, Update, Dispose);
        }

        Vector3 CalculateTargetCameraPosition(in Vector3 targetPosition)
        {
            float baseScale = 1f / 0.125f * TfModule.Instance.FrameSize;
            float minDistanceLookAt = 0.5f * baseScale;
            float maxDistanceLookAt = 3.0f * baseScale;

            if (!Settings.IsPhone)
            {
                float distanceToFrame = (Transform.localPosition - targetPosition).Magnitude();
                float zoomRadius = Mathf.Clamp(distanceToFrame, minDistanceLookAt, maxDistanceLookAt);
                return targetPosition - Transform.localRotation.Forward() * zoomRadius;
            }

            orbitCenter = targetPosition;
            orbitRadius = Mathf.Clamp(orbitRadius, minDistanceLookAt, maxDistanceLookAt);
            return -orbitRadius * Transform.localRotation.Forward() + orbitCenter;
        }

        static Vector3Int GetBaseInput()
        {
            var pVelocity = new Vector3Int();
            if (Keyboard.current[Key.S].isPressed)
            {
                pVelocity.z = -1;
            }

            if (Keyboard.current[Key.W].isPressed)
            {
                pVelocity.z++;
            }

            if (Keyboard.current[Key.A].isPressed)
            {
                pVelocity.x = -1;
            }

            if (Keyboard.current[Key.D].isPressed)
            {
                pVelocity.x++;
            }

            if (Keyboard.current[Key.Q].isPressed)
            {
                pVelocity.y = -1;
            }

            if (Keyboard.current[Key.E].isPressed)
            {
                pVelocity.y++;
            }

            return pVelocity;
        }

        void OnClick(ClickHitInfo clickHitInfo, bool isShortClick)
        {
            TriggerEnvironmentClick(clickHitInfo, isShortClick);
        }

        public static void TriggerEnvironmentClick(ClickHitInfo clickHitInfo, bool isShortClick)
        {
            if (clickHitInfo.TryGetRaycastResults(out var hitResults))
            {
                Vector3? firstHitPoint = null;
                List<(IHighlightable highlightable, Vector3 hitPoint)>? hits = null;
                foreach (var (hitObject, hitPoint, _) in hitResults)
                {
                    if (!TryGetHighlightable(hitObject, out var toHighlight))
                    {
                        continue;
                    }

                    if (firstHitPoint is not { } referencePoint)
                    {
                        firstHitPoint = hitPoint;
                    }
                    else if (Vector3.Distance(hitPoint, referencePoint) > 0.25f)
                    {
                        continue;
                    }

                    hits ??= new List<(IHighlightable, Vector3)>();
                    hits.Add((toHighlight, hitPoint));
                }

                switch (hits?.Count)
                {
                    case 1:
                        hits[0].highlightable.Highlight(hits[0].hitPoint);
                        return;
                    case > 1:
                        HighlightAll(hits);
                        return;
                }
            }

            if (isShortClick)
            {
                return;
            }
            
            Pose poseToHighlight;
            if (clickHitInfo.TryGetARRaycastResults(out var arHitResults))
            {
                poseToHighlight = arHitResults[0].AsPose();
            }
            else if (hitResults.Length != 0)
            {
                if (hitResults.Any(result => result.GameObject.layer == LayerType.Clickable))
                {
                    return;
                }

                poseToHighlight = hitResults[0].AsPose();
            }
            else
            {
                return;
            }

            FAnimator.Start(new ClickedPoseHighlighter(poseToHighlight));
        }

        static async void HighlightAll(IEnumerable<(IHighlightable highlightable, Vector3)> hits)
        {
            var aliveHits = hits.Where(toHighlight => toHighlight.highlightable.IsAlive);
            foreach (var (highlightable, hitPoint) in aliveHits)
            {
                highlightable.Highlight(hitPoint);
                await Task.Delay(250);
            }
        }

        static bool TryGetHighlightable(GameObject gameObject, out IHighlightable h)
        {
            return gameObject.TryGetComponent(out h) ||
                   gameObject.transform.parent is { } parent && parent.TryGetComponent(out h);
        }

        public static void PlayClickAudio(in Vector3 position)
        {
            var assetHolder = ResourcePool.AppAssetHolder;
            AudioSource.PlayClipAtPoint(assetHolder.Click, position);
        }

        static IEnumerable<Transform> GetAllRootChildren()
        {
            var resourcePoolChildren = ResourcePool.Transform is { } poolTransform
                ? poolTransform.GetAllChildren()
                : Enumerable.Empty<Transform>();

            var rootChildren = TfModule.HasInstance
                ? TfModule.RootFrame.Transform.GetAllChildren()
                : Enumerable.Empty<Transform>();

            return rootChildren.Concat(resourcePoolChildren);
        }
    }
}