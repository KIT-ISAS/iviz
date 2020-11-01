using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class NamedBoundary : MonoBehaviour, IDisplay, IRecyclable
    {
        const float FrameAxisLength = 0.015f;
        const float FrameAxisWidth = FrameAxisLength / 16;

        readonly GameObject[] frames = new GameObject[8];

        GameObject holder;
        GameObject labelObject;
        TextMesh labelObjectText;
        Billboard labelBillboard;

        public string Name
        {
            get => labelObjectText.text;
            private set => labelObjectText.text = value;
        }

        public Vector3 LabelOffset
        {
            get => labelBillboard.offset;
            set => labelBillboard.offset = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
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
                if (target is null || !target.UsesBoundaryBox)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                    labelObject.transform.localScale = TfListener.Instance.FrameLabelSize * Vector3.one;
                }
            }
        }

        Bounds bounds;
        public Bounds Bounds
        {
            get => bounds;
            private set
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

        public Bounds WorldBounds => Bounds;

        public bool ColliderEnabled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        void Awake()
        {
            holder = CreateSelectionFrame();
            holder.transform.SetParentLocal(transform);

            labelObject = ResourcePool.GetOrCreate(Resource.Displays.Text, transform);
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
            GameObject tmpHolder = new GameObject("Selection Frame");

            GameObject frame;
            frame = CreateFrame(tmpHolder);
            frames[0] = frame;

            frame = CreateFrame(tmpHolder);
            frame.transform.localRotation = Quaternion.Euler(0, 90, 0);
            frames[1] = frame;

            frame = CreateFrame(tmpHolder);
            frame.transform.localRotation = Quaternion.Euler(0, 180, 0);
            frames[2] = frame;

            frame = CreateFrame(tmpHolder);
            frame.transform.localRotation = Quaternion.Euler(0, 270, 0);
            frames[3] = frame;

            frame = CreateFrame(tmpHolder);
            frame.transform.localRotation = Quaternion.Euler(0, 90, 180);
            frames[4] = frame;

            frame = CreateFrame(tmpHolder);
            frame.transform.localRotation = Quaternion.Euler(0, 180, 180);
            frames[5] = frame;

            frame = CreateFrame(tmpHolder);
            frame.transform.localRotation = Quaternion.Euler(0, 270, 180);
            frames[6] = frame;

            frame = CreateFrame(tmpHolder);
            frame.transform.localRotation = Quaternion.Euler(0, 0, 180);
            frames[7] = frame;

            return tmpHolder;
        }

        static GameObject CreateFrame(GameObject parent)
        {
            GameObject tmpHolder = new GameObject("Frame Corner");
            tmpHolder.transform.SetParentLocal(parent.transform);

            GameObject[] frame = new GameObject[3];
            for (int i = 0; i < 3; i++)
            {
                GameObject gameObject = ResourcePool.GetOrCreate(Resource.Displays.Cube, tmpHolder.transform);
                gameObject.name = "Cube";
                MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
                renderer.sharedMaterial = Resource.Materials.SimpleLit.Object;
                renderer.receiveShadows = false;
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.SetPropertyEmissiveColor(Color.green / 2);
                renderer.SetPropertyColor(Color.green);

                frame[i] = gameObject;
            }

            frame[0].transform.localScale = new Vector3(FrameAxisLength, FrameAxisWidth, FrameAxisWidth);
            frame[0].transform.localPosition = -0.5f * FrameAxisLength * Vector3.right;
            frame[1].transform.localScale = new Vector3(FrameAxisWidth, FrameAxisWidth, FrameAxisLength);
            frame[1].transform.localPosition = -0.5f * FrameAxisLength * Vector3.forward;
            frame[2].transform.localScale = new Vector3(FrameAxisWidth, FrameAxisLength, FrameAxisWidth);
            frame[2].transform.localPosition = 0.5f * FrameAxisLength * Vector3.up;

            return tmpHolder;
        }

        public void SplitForRecycle()
        {
            GetComponentsInChildren<MeshRenderer>().ForEach(x => ResourcePool.Dispose(Resource.Displays.Cube, x.gameObject));
            Destroy(holder);

            ResourcePool.Dispose(Resource.Displays.Text, labelObject);
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
            if (target is null)
            {
                return;
            }

            //float maxSize = Mathf.Max(Mathf.Max(bounds.size.x, bounds.size.y), bounds.size.z);
            Bounds targetBounds = Target.Bounds;
            Bounds = new Bounds(targetBounds.center, Vector3.Scale(targetBounds.size, Target.BoundsScale));
            transform.SetPose(Target.BoundsPose);
            LabelOffset = Bounds.center + new Vector3(0, Target.WorldBounds.size.y / 2 + 0.15f, 0);
            Name = target?.Name;
        }


        public void Suspend()
        {
        }
    }

}