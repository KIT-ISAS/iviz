using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    [Obsolete]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class BoundaryFrame : MonoBehaviour, IDisplay, IRecyclable
    {
        readonly GameObject[] frames = new GameObject[8];

        Bounds bounds;
        GameObject holder;
        Color color;

        float frameAxisLength;
        float frameAxisWidth;

        void Awake()
        {
            Color = Color.green;
            FrameAxisLength = 0.33f;
            Bounds = new Bounds(Vector3.zero, Vector3.one);
            GetComponent<BoxCollider>().enabled = false;

            gameObject.name = "Boundary Frame";
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
                (float sizeX, float sizeY, float sizeZ) = bounds.size.Abs();

                float minAxisLength = Math.Min(Math.Min(sizeX, sizeY), sizeZ);
                float maxAxisLength = Math.Max(Math.Max(sizeX, sizeY), sizeZ);
                FrameAxisLength = Math.Min(maxAxisLength * 0.2f, minAxisLength * 0.5f);
                frames[0].transform.localPosition = center + new Vector3(sizeX, -sizeY, sizeZ) / 2;
                frames[1].transform.localPosition = center + new Vector3(sizeX, -sizeY, -sizeZ) / 2;
                frames[2].transform.localPosition = center + new Vector3(-sizeX, -sizeY, -sizeZ) / 2;
                frames[3].transform.localPosition = center + new Vector3(-sizeX, -sizeY, sizeZ) / 2;
                frames[4].transform.localPosition = center + new Vector3(sizeX, sizeY, sizeZ) / 2;
                frames[5].transform.localPosition = center + new Vector3(sizeX, sizeY, -sizeZ) / 2;
                frames[6].transform.localPosition = center + new Vector3(-sizeX, sizeY, -sizeZ) / 2;
                frames[7].transform.localPosition = center + new Vector3(-sizeX, sizeY, sizeZ) / 2;
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

                foreach (var resource in holder.GetComponentsInChildren<MeshMarkerResource>())
                {
                    resource.EmissiveColor = value / 2;
                    resource.Color = value;
                }
            }
        }

        void IDisplay.Suspend()
        {
        }

        public void SplitForRecycle()
        {
            DestroySelectionFrame();
        }

        void DestroySelectionFrame()
        {
            if (holder == null)
            {
                return;
            }

            foreach (MeshRenderer meshRenderer in holder.GetComponentsInChildren<MeshRenderer>())
            {
                var resource = meshRenderer.GetComponent<MeshMarkerResource>();
                resource.ReturnToPool(Resource.Displays.Cube);
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

        [NotNull]
        GameObject CreateFrame([NotNull] GameObject parent, Quaternion rotation)
        {
            GameObject frameLinkHolder = new GameObject("Frame Corner");
            frameLinkHolder.transform.SetParentLocal(parent.transform);
            frameLinkHolder.transform.localRotation = rotation;

            MeshMarkerResource[] frameLinks =
            {
                CreateFrameLink(frameLinkHolder),
                CreateFrameLink(frameLinkHolder),
                CreateFrameLink(frameLinkHolder)
            };

            frameLinks[0].Transform.localScale = new Vector3(frameAxisLength, frameAxisWidth, frameAxisWidth);
            frameLinks[0].Transform.localPosition = -0.5f * frameAxisLength * Vector3.right;
            frameLinks[1].Transform.localScale = new Vector3(frameAxisWidth, frameAxisWidth, frameAxisLength);
            frameLinks[1].Transform.localPosition = -0.5f * frameAxisLength * Vector3.forward;
            frameLinks[2].Transform.localScale = new Vector3(frameAxisWidth, frameAxisLength, frameAxisWidth);
            frameLinks[2].Transform.localPosition = 0.5f * frameAxisLength * Vector3.up;

            return frameLinkHolder;
        }

        [NotNull]
        MeshMarkerResource CreateFrameLink([NotNull] GameObject frameLinkHolder)
        {
            var frameLink =
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, frameLinkHolder.transform);
            frameLink.gameObject.name = "Cube";
            frameLink.EmissiveColor = (Color / 2).WithAlpha(1);
            frameLink.Color = Color;
            frameLink.EnableShadows = false;
            return frameLink;
        }
    }
}