using System;
using Iviz.App.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable HeuristicUnreachableCode

#pragma warning disable CS0162 // Unerreichbarer Code wurde entdeckt.

namespace Iviz.App
{
    public interface IDraggable
    {
        event Action<Pose> Moved;
        bool Visible { get; set; }
        void OnPointerMove(in Vector2 cursorPos);
        void OnStartDragging();
        void OnEndDragging();
    }
    
    public class FlyCamera : DisplayNode
    {
        Vector2 lastPointer;
        Vector2 lastPointerAlt;
        float lastAltDistance;

        float orbitX, orbitY = 45, orbitRadius = 5.0f;

        bool invalidMotion;
        bool alreadyMoving;
        bool alreadyScaling;

        static Camera MainCamera => TFListener.MainCamera;
        NamedBoundary namedBoundary;

        bool PointerOnGui { get; set; }

        Transform Transform { get; set; }
        
        Vector3 orbitCenter;

        TFFrame orbitCenterOverride;

        public TFFrame OrbitCenterOverride
        {
            get => orbitCenterOverride;
            set
            {
                orbitCenterOverride = value;
                if (value != null)
                {
                    StartOrbiting();
                    CameraViewOverride = null;
                }

                DisplayListPanel.Instance.UnlockButtonVisible = value;
            }
        }

        TFFrame cameraViewOverride;

        public TFFrame CameraViewOverride
        {
            get => cameraViewOverride;
            set
            {
                cameraViewOverride = value;
                if (value != null)
                {
                    OrbitCenterOverride = null;
                }

                DisplayListPanel.Instance.UnlockButtonVisible = value;
            }
        }

        public const bool IsMobile =
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
                true;
#else
            false;
#endif

        void OnUnlockClick()
        {
            CameraViewOverride = null;
            OrbitCenterOverride = null;
        }

        ClickableNode SelectedDisplay { get; set; }

        bool PointerDown { get; set; }

        bool PointerAltDown { get; set; }

        Vector2 PointerPosition { get; set; }

        Vector2 PointerAltPosition { get; set; }

        float PointerAltDistance { get; set; }

        IDraggable draggedObject;

        public IDraggable DraggedObject
        {
            get => draggedObject;
            set
            {
                if (draggedObject == value)
                {
                    return;
                }
                draggedObject?.OnEndDragging();
                draggedObject = value;
                draggedObject?.OnStartDragging();
            }
        }
        
        public HashSet<Canvas> Canvases { get; } = new HashSet<Canvas>();

        public HashSet<IBlocksPointer> GuiPointerBlockers { get; } = new HashSet<IBlocksPointer>();

        void Start()
        {
            Transform = transform;
            
            if (IsMobile)
            {
                UnityEngine.Application.targetFrameRate = 30;
            }

            //Physics.autoSimulation = false;

            DisplayListPanel.Instance.UnlockButton.onClick.AddListener(OnUnlockClick);

            namedBoundary = ResourcePool.GetOrCreate<NamedBoundary>(Resource.Displays.NamedBoundary);
            StartOrbiting();
        }

        public void Unselect(ClickableNode display)
        {
            if (SelectedDisplay != display)
            {
                return;
            }

            SelectedDisplay.Selected = false;
            SelectedDisplay = null;
            namedBoundary.Target = null;
        }

        public void Select(ClickableNode display)
        {
            if (SelectedDisplay == display)
            {
                return;
            }

            if (!(SelectedDisplay is null))
            {
                SelectedDisplay.Selected = false;
            }

            SelectedDisplay = display;
            if (!(SelectedDisplay is null))
            {
                SelectedDisplay.Selected = true;
                namedBoundary.Target = display;
            }
        }

        public void ToggleSelect(ClickableNode display)
        {
            if (SelectedDisplay != display)
            {
                Select(display);
            }
            else
            {
                Unselect(display);
            }
        }

        public void ShowBoundary(ClickableNode display)
        {
            if (display != SelectedDisplay)
            {
                return;
            }

            namedBoundary.Target = display;
        }

        float tmpAltDistance;

        void Update()
        {
            if (IsMobile)
            {
                PointerDown = Input.touchCount == 1;
                PointerAltDown = Input.touchCount == 2;
                
                if (PointerAltDown)
                {
                    PointerAltPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;
                }

                PointerAltDistance = PointerAltDown
                    ? Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position)
                    : 0;
                if (PointerDown || PointerAltDown)
                {
                    PointerPosition = Input.GetTouch(0).position;
                    PointerOnGui = Canvases.Any(IsPointerOnCanvas)
                                   || GuiPointerBlockers.Any(x => x.IsPointerOnGui(PointerPosition));
                }
            }
            else
            {
                PointerDown = Input.GetMouseButton(1);

                if (PointerDown)
                {
                    PointerPosition = Input.mousePosition;
                    PointerOnGui = Canvases.Any(IsPointerOnCanvas)
                                   || GuiPointerBlockers.Any(x => x.IsPointerOnGui(PointerPosition));
                }
            }

            if (!PointerDown)
            {
                DraggedObject = null;
            }
            if (!(DraggedObject is null))
            {
                DraggedObject.OnPointerMove(PointerPosition);
                return;
            }

            if (IsMobile)
            {
                if (!(OrbitCenterOverride is null))
                {
                    ProcessOrbiting();
                    ProcessScaling(false);
                    Quaternion q = OrbitCenterOverride.AbsolutePose.rotation * Quaternion.Euler(orbitY, orbitX, 0);
                    Transform.SetPositionAndRotation(
                        -orbitRadius * (q * Vector3.forward) + orbitCenter,
                        q);
                    orbitCenter = OrbitCenterOverride.AbsolutePose.position;
                }
                else
                {
                    ProcessOrbiting();
                    ProcessScaling(true);
                }
            }
            else
            {
                if (!(CameraViewOverride is null))
                {
                    Transform.SetPose(CameraViewOverride.AbsolutePose);
                }

                if (!(OrbitCenterOverride is null))
                {
                    ProcessOrbiting();
                    Quaternion q = OrbitCenterOverride.AbsolutePose.rotation * Quaternion.Euler(orbitY, orbitX, 0);
                    Transform.SetPositionAndRotation(
                        -orbitRadius * (q * Vector3.forward) + orbitCenter,
                        q);
                    orbitCenter = OrbitCenterOverride.AbsolutePose.position;
                }
                else
                {
                    ProcessTurning();
                    ProcessFlying();
                }
            }
        }

        bool IsPointerOnCanvas(Canvas canvas)
        {
            return canvas.enabled && canvas.gameObject.activeInHierarchy &&
                   RectTransformUtility.RectangleContainsScreenPoint(canvas.transform as RectTransform, PointerPosition,
                       MainCamera);
        }

        void StartOrbiting()
        {
            Vector3 diff = orbitCenter - Transform.position;
            orbitRadius = Mathf.Min(5, diff.magnitude);
            orbitX = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            orbitY = -Mathf.Atan2(diff.y, new Vector2(diff.x, diff.z).magnitude) * Mathf.Rad2Deg;

            Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
            Transform.SetPositionAndRotation(
                -orbitRadius * (q * Vector3.forward) + orbitCenter,
                q);
        }

        void ProcessOrbiting()
        {
            if (!PointerDown)
            {
                alreadyMoving = false;
                invalidMotion = false;
                return;
            }

            if (!alreadyMoving && PointerOnGui)
            {
                invalidMotion = true;
                return;
            }

            if (invalidMotion)
            {
                return;
            }
            //Debug.Log(alreadyMoving);

            Vector2 pointerDiff;
            if (alreadyMoving)
            {
                pointerDiff = PointerPosition - lastPointer;
            }
            else
            {
                pointerDiff = Vector2.zero;
            }

            lastPointer = PointerPosition;
            alreadyMoving = true;

            const float orbitCoeff = 0.1f;

            orbitX += pointerDiff.x * orbitCoeff;
            orbitY -= pointerDiff.y * orbitCoeff;
            if (orbitY > 90)
            {
                orbitY = 90;
            }

            if (orbitY < -90)
            {
                orbitY = -90;
            }


            if (Input.GetKey(KeyCode.W))
            {
                orbitRadius = Mathf.Max(0, orbitRadius - 0.1f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                orbitRadius += 0.1f;
            }

            Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
            Transform.SetPositionAndRotation(
                -orbitRadius * (q * Vector3.forward) + orbitCenter,
                q);
        }

        void ProcessScaling(bool allowPivotMotion)
        {
            if (!PointerAltDown)
            {
                alreadyScaling = false;
                return;
            }
            //Debug.Log(alreadyMoving);ww

            Vector2 pointerAltDiff;
            float altDistanceDiff;
            if (alreadyScaling)
            {
                pointerAltDiff = PointerAltPosition - lastPointerAlt;
                altDistanceDiff = PointerAltDistance - lastAltDistance;
            }
            else
            {
                pointerAltDiff = Vector2.zero;
                altDistanceDiff = 0;
            }

            lastPointerAlt = PointerAltPosition;
            lastAltDistance = PointerAltDistance;

            alreadyScaling = true;


            const float radiusCoeff = -0.0025f;
            const float tangentCoeff = 0.001f;

            orbitRadius += altDistanceDiff * radiusCoeff;

            Transform tTransform = Transform;
            Quaternion q = tTransform.rotation;

            if (!allowPivotMotion)
            {
                if (orbitRadius < 0.1f)
                {
                    orbitRadius = 0.1f;
                }
            }
            else
            {
                if (orbitRadius < 0.5f)
                {
                    float diff = 0.5f - orbitRadius;
                    orbitCenter += diff * (q * Vector3.forward);
                    orbitRadius = 0.5f;
                }

                float orbitScale = 0.75f * orbitRadius;
                orbitCenter -= tangentCoeff * pointerAltDiff.x * orbitScale *
                               tTransform.TransformDirection(Vector3.right);
                orbitCenter += tangentCoeff * pointerAltDiff.y * orbitScale *
                               tTransform.TransformDirection(Vector3.down);
            }

            tTransform.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
        }

        void ProcessTurning()
        {
            if (!PointerDown)
            {
                alreadyMoving = false;
                invalidMotion = false;
                return;
            }

            if (!alreadyMoving && PointerOnGui)
            {
                invalidMotion = true;
                return;
            }

            if (invalidMotion)
            {
                return;
            }
            //Debug.Log(alreadyMoving);

            Vector2 pointerDiff;
            if (alreadyMoving)
            {
                pointerDiff = PointerPosition - lastPointer;
            }
            else
            {
                pointerDiff = Vector2.zero;
            }

            lastPointer = PointerPosition;
            alreadyMoving = true;

            const float turnCoeff = 0.1f;

            orbitX += pointerDiff.x * turnCoeff;
            orbitY -= pointerDiff.y * turnCoeff;

            orbitY = Mathf.Min(Mathf.Max(orbitY, -89), 89);

            Transform.rotation = Quaternion.Euler(orbitY, orbitX, 0);
        }

        float totalRun = 0;

        void ProcessFlying()
        {
            const float mainSpeed = 7.5f; //regular speed
            const float shiftAdd = 1.0f; //multiplied by how long shift is held.  Basically running
            const float maxShift = 100.0f; //Maximum speed when holding shift
            //const float camSens = 0.25f; //How sensitive it with mouse

            if (!PointerDown)
            {
                totalRun = 0;
                return;
            }
            
            //Mouse  camera angle done.  
            //Keyboard commands
            Vector3 p = GetBaseInput();
            totalRun += Time.deltaTime;
            p *= Mathf.Max(totalRun * shiftAdd, mainSpeed);
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);

            p *= Time.deltaTime;

            Transform.position += Transform.rotation * p;
        }

        public void LookAt(in Vector3 position)
        {
            if (!IsMobile)
            {
                Transform.position = position - Transform.forward * 3;
            }
            else
            {
                orbitCenter = position;
                orbitRadius = Mathf.Min(orbitRadius, 3.0f);
                Transform.position = -orbitRadius * (Transform.rotation * Vector3.forward) + orbitCenter;
            }
        }

        static Vector3 GetBaseInput()
        {
            Vector3 pVelocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                pVelocity += new Vector3(0, 0, 1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                pVelocity += new Vector3(0, 0, -1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                pVelocity += new Vector3(-1, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                pVelocity += new Vector3(1, 0, 0);
            }

            return pVelocity;
        }
    }
}