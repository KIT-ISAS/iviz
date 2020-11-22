using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Controllers
{
    public sealed class BoundaryFrame : MonoBehaviour, IDisplay, IRecyclable
    {
        const float FrameAxisLength = 0.015f;
        const float FrameAxisWidth = FrameAxisLength / 16;

        readonly GameObject[] frames = new GameObject[8];

        Bounds bounds = new Bounds(Vector3.zero, Vector3.one);
        GameObject holder;
        Billboard labelBillboard;
        GameObject labelObject;
        TextMesh labelObjectText;

        public Vector3 LabelOffset
        {
            get => labelBillboard.offset;
            set => labelBillboard.offset = value;
        }

        void Awake()
        {
            holder = CreateSelectionFrame(frames);
            holder.transform.SetParentLocal(transform);

            labelObject = ResourcePool.GetOrCreate(Resource.Displays.Text, transform);
            labelObject.name = "Frame Axis Label";
            labelObjectText = labelObject.GetComponent<TextMesh>();
            labelObject.transform.SetParentLocal(transform);

            labelBillboard = labelObject.GetComponent<Billboard>();

            Name = "";
        }

        string Name
        {
            get => labelObjectText.text;
            set => labelObjectText.text = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
        }

        public Bounds? Bounds
        {
            get => bounds;
            private set
            {
                bounds = value ?? throw new NullReferenceException();

                Vector3 center = bounds.center;
                Vector3 size = bounds.size;

                frames[0].transform.localPosition = center + new Vector3(size.x, -size.y, size.z) / 2;
                frames[1].transform.localPosition = center + new Vector3(size.x, -size.y, -size.z) / 2;
                frames[2].transform.localPosition = center + new Vector3(-size.x, -size.y, -size.z) / 2;
                frames[3].transform.localPosition = center + new Vector3(-size.x, -size.y, size.z) / 2;
                frames[4].transform.localPosition = center + new Vector3(size.x, size.y, size.z) / 2;
                frames[5].transform.localPosition = center + new Vector3(size.x, size.y, -size.z) / 2;
                frames[6].transform.localPosition = center + new Vector3(-size.x, size.y, -size.z) / 2;
                frames[7].transform.localPosition = center + new Vector3(-size.x, size.y, size.z) / 2;
            }
        }

        public bool ColliderEnabled
        {
            get => false;
            set { }
        }

        public void Suspend()
        {
        }

        public void SplitForRecycle()
        {
            foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.receiveShadows = true;
                meshRenderer.shadowCastingMode = ShadowCastingMode.On;
                meshRenderer.SetPropertyEmissiveColor(Color.black);
                meshRenderer.GetComponent<BoxCollider>().enabled = true;
                ResourcePool.Dispose(Resource.Displays.Cube, meshRenderer.gameObject);
            }

            ResourcePool.Dispose(Resource.Displays.Text, labelObject);
            labelObject = null;
            labelObjectText = null;
            labelBillboard = null;

            Destroy(holder);
        }

        static GameObject CreateSelectionFrame(IList<GameObject> frames)
        {
            GameObject frameHolder = new GameObject("Selection Frame");

            frames[0] = CreateFrame(frameHolder, Quaternion.identity);
            frames[1] = CreateFrame(frameHolder, Quaternion.Euler(0, 90, 0));
            frames[2] = CreateFrame(frameHolder, Quaternion.Euler(0, 180, 0));
            frames[3] = CreateFrame(frameHolder, Quaternion.Euler(0, 270, 0));
            frames[4] = CreateFrame(frameHolder, Quaternion.Euler(0, 90, 180));
            frames[5] = CreateFrame(frameHolder, Quaternion.Euler(0, 180, 180));
            frames[6] = CreateFrame(frameHolder, Quaternion.Euler(0, 270, 180));
            frames[7] = CreateFrame(frameHolder, Quaternion.Euler(0, 0, 180));

            return frameHolder;
        }

        static GameObject CreateFrame(GameObject parent, Quaternion rotation)
        {
            GameObject frameLinkHolder = new GameObject("Frame Corner");
            frameLinkHolder.transform.SetParentLocal(parent.transform);
            frameLinkHolder.transform.localRotation = rotation;

            GameObject[] frameLinks =
            {
                CreateFrameLink(frameLinkHolder),
                CreateFrameLink(frameLinkHolder),
                CreateFrameLink(frameLinkHolder)
            };

            frameLinks[0].transform.localScale = new Vector3(FrameAxisLength, FrameAxisWidth, FrameAxisWidth);
            frameLinks[0].transform.localPosition = -0.5f * FrameAxisLength * Vector3.right;
            frameLinks[1].transform.localScale = new Vector3(FrameAxisWidth, FrameAxisWidth, FrameAxisLength);
            frameLinks[1].transform.localPosition = -0.5f * FrameAxisLength * Vector3.forward;
            frameLinks[2].transform.localScale = new Vector3(FrameAxisWidth, FrameAxisLength, FrameAxisWidth);
            frameLinks[2].transform.localPosition = 0.5f * FrameAxisLength * Vector3.up;

            return frameLinkHolder;
        }

        static GameObject CreateFrameLink(GameObject holder)
        {
            GameObject gameObject = ResourcePool.GetOrCreate(Resource.Displays.Cube, holder.transform);
            gameObject.name = "Cube";
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = Resource.Materials.Lit.Object;
            renderer.receiveShadows = false;
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            renderer.SetPropertyEmissiveColor(Color.green / 2);
            renderer.SetPropertyColor(Color.green);
            renderer.GetComponent<BoxCollider>().enabled = false;
            return gameObject;
        }
    }
}