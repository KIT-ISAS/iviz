
using Iviz.App.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

#pragma warning disable CS0162 // Unerreichbarer Code wurde entdeckt.

namespace Iviz.App
{
    public class FlyCamera : DisplayNode
    {
        Vector2 lastPointer;
        Vector2 lastPointerAlt;
        float lastAltDistance;

        float orbitX, orbitY = 45, orbitRadius = 5.0f;

        bool invalidMotion;
        bool alreadyMoving;
        bool alreadyScaling;

        Camera MainCamera => TFListener.MainCamera;
        NamedBoundary namedBoundary;

        public bool PointerOnGui { get; private set; }

        Vector3 orbitCenter;

        TFFrame orbitCenterOverride_;
        public TFFrame OrbitCenterOverride
        {
            get => orbitCenterOverride_;
            set
            {
                orbitCenterOverride_ = value;
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
        /*
        public Vector3 OrbitCenter
        {
            get => orbitCenter_;
            set
            {
                orbitCenter_ = value;
                StartOrbiting();
            }
        }
        */

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

        public ClickableNode SelectedDisplay { get; private set; }

        public bool PointerDown { get; private set; }

        public bool PointerAltDown { get; private set; }

        public Vector2 PointerPosition { get; private set; }

        public Vector2 PointerAltPosition { get; private set; }

        public float PointerAltDistance { get; private set; }

        public HashSet<Canvas> Canvases { get; } = new HashSet<Canvas>();

        public HashSet<IBlocksPointer> GuiPointerBlockers { get; } = new HashSet<IBlocksPointer>();

        void Start()
        {
            if (IsMobile)
            {
                Application.targetFrameRate = 30;
            }
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
                PointerAltDistance = PointerAltDown ?
                    Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) :
                    0;
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
            if (IsMobile)
            {
                if (!(OrbitCenterOverride is null))
                {
                    ProcessOrbiting();
                    ProcessScaling(false);
                    Quaternion q = OrbitCenterOverride.AbsolutePose.rotation * Quaternion.Euler(orbitY, orbitX, 0);
                    transform.SetPositionAndRotation(
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
                    transform.SetPose(CameraViewOverride.AbsolutePose);
                }
                if (!(OrbitCenterOverride is null))
                {
                    ProcessOrbiting();
                    Quaternion q = OrbitCenterOverride.AbsolutePose.rotation * Quaternion.Euler(orbitY, orbitX, 0);
                    transform.SetPositionAndRotation(
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
                        RectTransformUtility.RectangleContainsScreenPoint(canvas.transform as RectTransform, PointerPosition, MainCamera);
        }

        /*
        public void StartOrbitingAround(TFFrame frame)
        {
            if (OrbitFrame == frame)
            {
                return;
            }
            if (OrbitFrame != null)
            {
                OrbitFrame.OrbitColorEnabled = false;
                OrbitFrame.RemoveListener(this);
            }
            orbitFrame = frame;
            if (OrbitFrame != null)
            {
                OrbitFrame.OrbitColorEnabled = true;
                OrbitFrame.AddListener(this);

                OrbitCenter = OrbitFrame.transform.position;

                Debug.Log("Orbiting around " + frame.Id);
            }
            else
            {
                Debug.Log("Stopped orbiting");
            }
        }
        */

        void StartOrbiting()
        {
            Vector3 diff = orbitCenter - transform.position;
            orbitRadius = Mathf.Min(5, diff.magnitude);
            orbitX = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            orbitY = -Mathf.Atan2(diff.y, new Vector2(diff.x, diff.z).magnitude) * Mathf.Rad2Deg;

            Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
            transform.SetPositionAndRotation(
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
            else
            if (Input.GetKey(KeyCode.S))
            {
                orbitRadius += 0.1f;
            }

            Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
            transform.SetPositionAndRotation(
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

            Transform ttransform = transform;
            Quaternion q = ttransform.rotation;

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
                orbitCenter -= tangentCoeff * pointerAltDiff.x * orbitScale * ttransform.TransformDirection(Vector3.right);
                orbitCenter += tangentCoeff * pointerAltDiff.y * orbitScale * ttransform.TransformDirection(Vector3.down);
            }
            ttransform.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
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

            /*
            if (Input.GetKey(KeyCode.W))
            {
            }
            if (Input.GetKey(KeyCode.S))
            {
                orbitRadius += 0.1f;
            }
            */

            //Vector3 parentPosition = OrbitCenter.transform.position;
            transform.rotation = Quaternion.Euler(orbitY, orbitX, 0);
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

            /*
            //Debug.Log("down: " + PointerDown);
            if (!PointerDown)
            {
                alreadyMoving = false;
                invalidMotion = false;
                return;
            }
            //Debug.Log("ongui: " + PointerOnGui);
            if (!alreadyMoving && PointerOnGui)
            {
                //Debug.Log("invalid: " + true);
                invalidMotion = true;
                return;
            }
            //Debug.Log("invalid: " + invalidMotion);
            if (invalidMotion)
            {
                return;
            }
            Vector2 pointerDiff = alreadyMoving ? PointerPosition - lastPointer : Vector2.zero;

            alreadyMoving = true;

            pointerDiff = new Vector2(-pointerDiff.y * camSens, pointerDiff.x * camSens);
            pointerDiff = new Vector2(transform.eulerAngles.x + pointerDiff.x, transform.eulerAngles.y + pointerDiff.y);
            if (pointerDiff.x < 180)
            {
                pointerDiff.x = Mathf.Min(pointerDiff.x, 89);
            }
            else if (pointerDiff.x > 180)
            {
                pointerDiff.x = Mathf.Max(pointerDiff.x, 271);
            }
            transform.eulerAngles = pointerDiff;
            lastPointer = PointerPosition;
            */
            //Mouse  camera angle done.  
            //Keyboard commands
            Vector3 p = GetBaseInput();
            totalRun += Time.deltaTime;
            p *= Mathf.Max(totalRun * shiftAdd, mainSpeed);
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);

            p *= Time.deltaTime;
            /*
            Vector3 newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space))
            { //If player wants to move on X and Z axis only
                transform.Translate(p);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(p);
            }
            */
            //transform.Translate(p);
            transform.position += transform.rotation * p;
            //transform.position = -orbitRadius * (transform.rotation * Vector3.forward) + OrbitCenter;
        }

        public void LookAt(in Vector3 position)
        {
            if (!IsMobile)
            {
                transform.position = position - transform.forward * 3;
            }
            else
            {
                orbitCenter = position;
                orbitRadius = Mathf.Min(orbitRadius, 3.0f);
                transform.position = -orbitRadius * (transform.rotation * Vector3.forward) + orbitCenter;
            }
        }

        Vector3 GetBaseInput()
        { //returns the basic values, if it's 0 than it's not active.
            Vector3 pVelocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                //Debug.Log("was here");
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