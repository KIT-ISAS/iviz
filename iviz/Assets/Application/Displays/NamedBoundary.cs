using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App.Displays
{
    public class NamedBoundary : MonoBehaviour, IDisplay, IRecyclable
    {
        const float FrameAxisLength = 0.15f;
        const float FrameAxisWidth = FrameAxisLength / 16;

        readonly GameObject[] frames = new GameObject[8];

        GameObject Holder;
        GameObject labelObject;
        TextMesh labelObjectText;
        Billboard labelBillboard;

        public string Name
        {
            get => labelObjectText.text;
            set
            {
                labelObjectText.text = value;
            }
        }

        public Vector3 LabelOffset
        {
            get => labelBillboard.offset;
            set
            {
                labelBillboard.offset = value;
            }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set
            {
                gameObject.SetActive(value);
            }
        }

        public Vector3 WorldScale => transform.lossyScale;
        public Pose WorldPose => transform.AsPose();

        public int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
        }

        ClickableNode target;
        public ClickableNode Target
        {
            get => target;
            set
            {
                target = value;
                if (target == null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                    labelObject.transform.localScale = TFListener.Instance.AxisLabelSize * Vector3.one;
                }
            }
        }

        Bounds bounds;
        public Bounds Bounds
        {
            get => bounds;
            set
            {
                Vector3 center = value.center;
                Vector3 size = value.size;

                frames[0].transform.localPosition = center + new Vector3(size.x, -size.y, size.z) / 2;
                frames[1].transform.localPosition = center + new Vector3(size.x, -size.y, -size.z) / 2;
                frames[2].transform.localPosition = center + new Vector3(-size.x, -size.y, -size.z) / 2;
                frames[3].transform.localPosition = center + new Vector3(-size.x, -size.y, size.z) / 2;
                frames[4].transform.localPosition = center + new Vector3(size.x, size.y, size.z) / 2;
                frames[5].transform.localPosition = center + new Vector3(size.x, size.y, -size.z) / 2;
                frames[6].transform.localPosition = center + new Vector3(-size.x, size.y, -size.z) / 2;
                frames[7].transform.localPosition = center + new Vector3(-size.x, size.y, size.z) / 2;

                bounds = value;
            }
        }

        public Bounds WorldBounds => bounds;

        public bool ColliderEnabled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        void Awake()
        {
            Holder = CreateSelectionFrame();
            Holder.transform.SetParentLocal(transform);

            labelObject = ResourcePool.GetOrCreate(Resource.Markers.Text, transform);
            labelObject.name = "Frame Axis Label";
            labelObjectText = labelObject.GetComponent<TextMesh>();
            labelObject.transform.SetParentLocal(transform);

            labelBillboard = labelObject.GetComponent<Billboard>();

            //Active = false;
            Target = null;
            Name = "";
        }

        GameObject CreateSelectionFrame()
        {
            GameObject Holder = new GameObject("Selection Frame");

            GameObject frame;
            frame = CreateFrame(Holder);
            frames[0] = frame;

            frame = CreateFrame(Holder);
            frame.transform.localRotation = Quaternion.Euler(0, 90, 0);
            frames[1] = frame;

            frame = CreateFrame(Holder);
            frame.transform.localRotation = Quaternion.Euler(0, 180, 0);
            frames[2] = frame;

            frame = CreateFrame(Holder);
            frame.transform.localRotation = Quaternion.Euler(0, 270, 0);
            frames[3] = frame;

            frame = CreateFrame(Holder);
            frame.transform.localRotation = Quaternion.Euler(0, 90, 180);
            frames[4] = frame;

            frame = CreateFrame(Holder);
            frame.transform.localRotation = Quaternion.Euler(0, 180, 180);
            frames[5] = frame;

            frame = CreateFrame(Holder);
            frame.transform.localRotation = Quaternion.Euler(0, 270, 180);
            frames[6] = frame;

            frame = CreateFrame(Holder);
            frame.transform.localRotation = Quaternion.Euler(0, 0, 180);
            frames[7] = frame;

            return Holder;
        }

        GameObject CreateFrame(GameObject parent)
        {
            GameObject Holder = new GameObject("Frame Corner");
            Holder.transform.SetParentLocal(parent.transform);

            GameObject[] Frame = new GameObject[3];
            for (int i = 0; i < 3; i++)
            {
                GameObject gameObject = ResourcePool.GetOrCreate(Resource.Markers.Cube, Holder.transform);
                gameObject.name = "Cube";
                MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
                renderer.sharedMaterial = Resource.Materials.SimpleLit.Object;
                renderer.receiveShadows = false;
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.SetPropertyEmissiveColor(Color.green / 2);
                renderer.SetPropertyColor(Color.green);

                Frame[i] = gameObject;
            }

            Frame[0].transform.localScale = new Vector3(FrameAxisLength, FrameAxisWidth, FrameAxisWidth);
            Frame[0].transform.localPosition = -0.5f * FrameAxisLength * Vector3.right;
            Frame[1].transform.localScale = new Vector3(FrameAxisWidth, FrameAxisWidth, FrameAxisLength);
            Frame[1].transform.localPosition = -0.5f * FrameAxisLength * Vector3.forward;
            Frame[2].transform.localScale = new Vector3(FrameAxisWidth, FrameAxisLength, FrameAxisWidth);
            Frame[2].transform.localPosition = 0.5f * FrameAxisLength * Vector3.up;

            return Holder;
        }

        public void Recycle()
        {
            GetComponentsInChildren<MeshRenderer>().ForEach(x => ResourcePool.Dispose(Resource.Markers.Cube, x.gameObject));
            Destroy(Holder);

            ResourcePool.Dispose(Resource.Markers.Text, labelObject);
            labelObject = null;
            labelObjectText = null;
            labelBillboard = null;
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        void Update()
        {
            if (target == null)
            {
                return;
            }

            //float maxSize = Mathf.Max(Mathf.Max(bounds.size.x, bounds.size.y), bounds.size.z);
            Bounds bounds = Target.Bounds;
            Bounds = new Bounds(bounds.center, Vector3.Scale(bounds.size, Target.BoundsScale));
            Pose pose = Target.BoundsPose;
            transform.position = pose.position;
            transform.rotation = pose.rotation;
            LabelOffset = Bounds.center + new Vector3(0, Target.WorldBounds.size.y/2 + 0.15f, 0);
            Name = target?.Name;
        }


        public void Stop()
        {
        }
    }

}