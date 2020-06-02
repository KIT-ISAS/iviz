using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using Iviz.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using Iviz.App.Displays;

namespace Iviz.App.Listeners
{
    public sealed class TFFrame : ClickableNode, IRecyclable
    {
        public const int Layer = 9;
        static readonly string[] names = { "Axis-X", "Axis-Y", "Axis-Z" };

        //bool isDead;
        string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                labelObjectText.text = id;
                //isDead = false;
            }
        }

        bool forceInvisible;
        public bool ForceInvisible
        {
            get => forceInvisible;
            set
            {
                forceInvisible = value;
                LabelVisible = LabelVisible; // update
            }
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                selected = value;
                labelObject.SetActive(value || LabelVisible);
            }
        }

        bool orbitColorEnabled;
        public bool OrbitColorEnabled
        {
            get => orbitColorEnabled;
            set
            {
                orbitColorEnabled = value;
                if (!value)
                {
                    axisRenderers[0].SetPropertyColor(Color.red);
                    axisRenderers[1].SetPropertyColor(Color.green);
                    axisRenderers[2].SetPropertyColor(Color.blue);

                    axisRenderers[0].SetPropertyEmissiveColor(Color.red * 0.2f);
                    axisRenderers[1].SetPropertyEmissiveColor(Color.green * 0.2f);
                    axisRenderers[2].SetPropertyEmissiveColor(Color.blue * 0.2f);
                }
                else
                {
                    axisRenderers[0].SetPropertyColor(Color.black);
                    axisRenderers[1].SetPropertyColor(Color.black);
                    axisRenderers[2].SetPropertyColor(Color.black);

                    axisRenderers[0].SetPropertyEmissiveColor(Color.yellow);
                    axisRenderers[1].SetPropertyEmissiveColor(Color.yellow);
                    axisRenderers[2].SetPropertyEmissiveColor(Color.yellow);
                }
            }
        }

        public bool IgnoreUpdates { get; set; }

        GameObject labelObject;
        TextMesh labelObjectText;
        GameObject[] axisObjects;
        MeshRenderer[] axisRenderers;
        LineConnector parentConnector;
        BoxCollider boxCollider;
        readonly HashSet<DisplayNode> listeners = new HashSet<DisplayNode>();
        readonly Dictionary<string, TFFrame> children = new Dictionary<string, TFFrame>();

        public override Bounds Bounds => new Bounds(boxCollider.center, boxCollider.size);
        public override Bounds WorldBounds => boxCollider.bounds;


        public void AddListener(DisplayNode display)
        {
            /*
            if (isDead)
            {
                Debug.LogWarning("Adding listener to dead frame!");
            }
            */
            listeners.Add(display);
        }

        public void RemoveListener(DisplayNode display)
        {
            if (HasNoListeners)
            {
                return;
            }
            listeners.Remove(display);
            CheckIfDead();
        }

        public void AddChild(TFFrame frame)
        {
            /*
            if (isDead)
            {
                Debug.LogWarning("Adding child to dead frame!");
            }
            */
            children.Add(frame.Id, frame);
        }

        public void RemoveChild(TFFrame frame)
        {
            if (IsChildless)
            {
                return;
            }
            children.Remove(frame.Id);
        }

        void CheckIfDead()
        {
            if (HasNoListeners && IsChildless)
            {
                TFListener.Instance.MarkAsDead(this);
                //isDead = true;
            }
        }

        public void UpdateFrameMesh(float newFrameAxisLength, float newFrameAxisWidth)
        {
            if (axisObjects == null)
            {
                axisObjects = new GameObject[3];
                axisRenderers = new MeshRenderer[3];
                for (int i = 0; i < 3; i++)
                {
                    GameObject gameObject = ResourcePool.GetOrCreate(Resource.Markers.Cube, transform);
                    gameObject.name = names[i];
                    gameObject.layer = Layer;
                    MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
                    renderer.sharedMaterial = Resource.Materials.SimpleLit.Object;
                    renderer.receiveShadows = false;
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    axisObjects[i] = gameObject;
                    axisRenderers[i] = renderer;
                }
            }

            axisObjects[0].transform.localScale = new Vector3(newFrameAxisLength, newFrameAxisWidth, newFrameAxisWidth);
            axisObjects[0].transform.localPosition = -0.5f * newFrameAxisLength * Vector3.right;
            axisObjects[1].transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisWidth, newFrameAxisLength);
            axisObjects[1].transform.localPosition = -0.5f * newFrameAxisLength * Vector3.forward;
            axisObjects[2].transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisLength, newFrameAxisWidth);
            axisObjects[2].transform.localPosition = 0.5f * newFrameAxisLength * Vector3.up;

            boxCollider.center = 0.5f * (newFrameAxisLength - newFrameAxisWidth / 2) * new Vector3(-1, 1, -1);
            boxCollider.size = (newFrameAxisLength + newFrameAxisWidth / 2) * Vector3.one;
        }

        public bool AxisVisible
        {
            get => axisObjects[0].activeSelf;
            set
            {
                bool visible = !ForceInvisible && value;
                axisObjects[0].SetActive(visible);
                axisObjects[1].SetActive(visible);
                axisObjects[2].SetActive(visible);
                boxCollider.enabled = visible;
            }
        }

        bool labelVisible;
        public bool LabelVisible
        {
            get => labelVisible;
            set
            {
                labelVisible = value;
                labelObject.SetActive(!ForceInvisible && (value || Selected));
            }
        }

        public float LabelSize
        {
            get => labelObject.transform.localScale.x;
            set
            {
                labelObject.transform.localScale = value * Vector3.one;
            }
        }

        public bool ConnectorVisible
        {
            get => parentConnector.gameObject.activeSelf;
            set
            {
                parentConnector.gameObject.SetActive(value);
            }
        }

        float axisLength;
        public float AxisLength
        {
            get => axisLength;
            set
            {
                axisLength = value;
                UpdateFrameMesh(axisLength, axisLength / 20);
                parentConnector.LineWidth = axisLength / 20;
            }
        }

        public override TFFrame Parent
        {
            get => base.Parent;
            set
            {
                SetParent(value);
                /*
                if (isDead)
                {
                    Debug.LogWarning("Accessing dead frame!");
                }
                */

            }
        }

        public bool SetParent(TFFrame newParent)
        {
            if (newParent == Parent)
            {
                return true;
            }
            if (newParent == this)
            {
                Logger.Error($"TFFrame: Cannot set '{newParent.Id}' as a parent to itself!");
                return false;
            }
            if (newParent != null && newParent.IsChildOf(this))
            {
                Logger.Error($"TFFrame: Cannot set '{newParent.Id}' as parent to '{Id}' because it causes a cycle!");
                newParent.CheckIfDead();
                return false;
            }
            if (Parent != null)
            {
                Parent.RemoveChild(this);
            }
            base.Parent = newParent;
            if (Parent != null)
            {
                Parent.AddChild(this);
            }

            parentConnector.B = transform.parent != null ?
                transform.parent :
                TFListener.BaseFrame.transform;

            return true;
        }

        bool IsChildOf(TFFrame frame)
        {
            if (Parent == null)
            {
                return false;
            }
            return Parent == frame || Parent.IsChildOf(frame);
        }


        public IReadOnlyDictionary<string, TFFrame> Children => children;

        Pose pose;
        public Pose Pose
        {
            get => pose;
            set
            {
                /*
                if (isDead)
                {
                    Debug.LogWarning("Getting pose of dead frame!");
                }
                */
                if (!IgnoreUpdates)
                {
                    pose = value;
                    transform.SetLocalPose(pose);
                }
            }
        }

        public bool HasNoListeners => !listeners.Any();

        public bool IsChildless => !children.Any();

        public override string Name => Id;

        public override Pose BoundsPose => transform.AsPose();

        void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();

            labelObject = ResourcePool.GetOrCreate(Resource.Markers.Text, transform);
            labelObject.gameObject.SetActive(false);
            labelObject.name = "Frame Axis Label";
            labelObjectText = labelObject.GetComponent<TextMesh>();

            parentConnector = ResourcePool.
                GetOrCreate(Resource.Markers.LineConnector, transform).
                GetComponent<LineConnector>();
            parentConnector.name = "Parent Connector";
            parentConnector.A = transform;
            parentConnector.B = transform.parent != null ?
                transform.parent :
                TFListener.BaseFrame?.transform; // TFListener.BaseFrame may not exist yet

            gameObject.layer = Layer;

            AxisLength = 0.25f;
            OrbitColorEnabled = false;

            parentConnector.gameObject.SetActive(false);
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Markers.Cube, axisObjects[0]);
            ResourcePool.Dispose(Resource.Markers.Cube, axisObjects[1]);
            ResourcePool.Dispose(Resource.Markers.Cube, axisObjects[2]);
            ResourcePool.Dispose(Resource.Markers.Text, labelObject);
            ResourcePool.Dispose(Resource.Markers.LineConnector, parentConnector.gameObject);
            axisObjects = null;
            labelObject = null;
            parentConnector = null;
        }

        /*
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (GetClickCount(eventData) == 3)
            {
                TFListener.GuiManager.StartOrbitingAround(OrbitColorEnabled ? null : this);
            }
        }
        */

    }
}
