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
    [RequireComponent(typeof(BoxCollider))]
    public sealed class BoundaryFrame : MonoBehaviour, IDisplay, IRecyclable
    {
        readonly GameObject[] frames = new GameObject[8];

        Bounds bounds;
        GameObject holder;
        Billboard labelBillboard;
        GameObject labelObject;
        TextMesh labelObjectText;
        Color color;

        float frameAxisLength;
        float frameAxisWidth;

        public Vector3 LabelOffset
        {
            get => labelBillboard.offset;
            set => labelBillboard.offset = value;
        }

        void Awake()
        {
            Color = new Color(0, 1, 0, 0.3f);
            FrameAxisLength = 0.33f;
            Bounds = new Bounds(Vector3.zero, Vector3.one);
            GetComponent<BoxCollider>().enabled = false;

            labelObject = ResourcePool.GetOrCreate(Resource.Displays.Text, transform);
            labelObject.name = "Frame Axis Label";
            labelObjectText = labelObject.GetComponent<TextMesh>();
            labelObject.transform.SetParentLocal(transform);

            labelBillboard = labelObject.GetComponent<Billboard>();

            Name = "";
        }

        public float FrameAxisLength
        {
            get => frameAxisLength;
            set
            {
                if (Mathf.Approximately(frameAxisLength, value))
                {
                    return;
                }

                frameAxisLength = value;
                frameAxisWidth = frameAxisLength / 8;
                DestroySelectionFrame();
                CreateSelectionFrame();
                Bounds = Bounds;
            }
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
            set
            {
                bounds = value ?? throw new NullReferenceException();

                Vector3 center = bounds.center;
                Vector3 size = bounds.size / 4;

                float minAxisLength = Mathf.Min(Mathf.Min(bounds.size.x, bounds.size.y), bounds.size.z);
                FrameAxisLength = minAxisLength * 0.05f;
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

        public Color Color
        {
            get => color;
            set
            {
                color = value;

                if (holder == null)
                {
                    return;
                }

                foreach (MeshRenderer meshRenderer in holder.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.SetPropertyEmissiveColor(value / 2);
                    meshRenderer.SetPropertyColor(value);
                }
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
            DestroySelectionFrame();
            ResourcePool.Dispose(Resource.Displays.Text, labelObject);
            labelObject = null;
            labelObjectText = null;
            labelBillboard = null;
        }

        void DestroySelectionFrame()
        {
            if (holder == null)
            {
                return;
            }

            foreach (MeshRenderer meshRenderer in holder.GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.receiveShadows = true;
                meshRenderer.shadowCastingMode = ShadowCastingMode.On;
                meshRenderer.SetPropertyEmissiveColor(Color.black);
                meshRenderer.GetComponent<BoxCollider>().enabled = true;
                ResourcePool.Dispose(Resource.Displays.Cube, meshRenderer.gameObject);
            }

            Destroy(holder);
            holder = null;
        }

        void CreateSelectionFrame()
        {
            holder = new GameObject("Selection Frame");
            holder.transform.SetParentLocal(transform);

            frames[0] = CreateFrame(holder, Quaternion.identity);
            frames[1] = CreateFrame(holder, Quaternion.Euler(0, 90, 0));
            frames[2] = CreateFrame(holder, Quaternion.Euler(0, 180, 0));
            frames[3] = CreateFrame(holder, Quaternion.Euler(0, 270, 0));
            frames[4] = CreateFrame(holder, Quaternion.Euler(0, 90, 180));
            frames[5] = CreateFrame(holder, Quaternion.Euler(0, 180, 180));
            frames[6] = CreateFrame(holder, Quaternion.Euler(0, 270, 180));
            frames[7] = CreateFrame(holder, Quaternion.Euler(0, 0, 180));
        }

        GameObject CreateFrame(GameObject parent, Quaternion rotation)
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

            frameLinks[0].transform.localScale = new Vector3(frameAxisLength, frameAxisWidth, frameAxisWidth);
            frameLinks[0].transform.localPosition = -0.5f * frameAxisLength * Vector3.right;
            frameLinks[1].transform.localScale = new Vector3(frameAxisWidth, frameAxisWidth, frameAxisLength);
            frameLinks[1].transform.localPosition = -0.5f * frameAxisLength * Vector3.forward;
            frameLinks[2].transform.localScale = new Vector3(frameAxisWidth, frameAxisLength, frameAxisWidth);
            frameLinks[2].transform.localPosition = 0.5f * frameAxisLength * Vector3.up;

            return frameLinkHolder;
        }

        GameObject CreateFrameLink(GameObject frameLinkHolder)
        {
            GameObject frameLink = ResourcePool.GetOrCreate(Resource.Displays.Cube, frameLinkHolder.transform);
            frameLink.name = "Cube";
            MeshRenderer meshRenderer = frameLink.GetComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = Resource.Materials.Lit.Object;
            meshRenderer.receiveShadows = false;
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            meshRenderer.SetPropertyEmissiveColor(color / 2);
            meshRenderer.SetPropertyColor(color);
            meshRenderer.GetComponent<BoxCollider>().enabled = false;
            return frameLink;
        }
    }
}