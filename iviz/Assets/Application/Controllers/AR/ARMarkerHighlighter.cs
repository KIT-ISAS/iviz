using System;
using System.Linq;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Iviz.Controllers
{
    public class ARMarkerHighlighter : MonoBehaviour, IDisplay
    {
        [SerializeField] Transform topLeft;
        [SerializeField] Transform topRight;
        [SerializeField] Transform bottomRight;
        [SerializeField] Transform bottomLeft;
        [SerializeField] BoxCollider boxCollider;
        [SerializeField] TextMeshPro text;
        MeshMarkerResource[] resources;
        const float Scale = 0.005f;
        const float Z = 0.05f;

        const float HighlightScale = Scale * 1.05f;

        float highlightTime = 0.5f;
        float? highlightStart;

        public void Highlight([NotNull] Vector2f[] corners2, [NotNull] string code, Intrinsic intrinsic,
            float highlightTimeInMs)
        {
            resources ??= GetComponentsInChildren<MeshMarkerResource>();

            var cornersWorld = corners2.Select(corner => intrinsic.Unproject(corner, Z)).ToArray();
            if (cornersWorld.Length == 0)
            {
                throw new ArgumentException("[ARMarkerHighlighter] Cannot highlight marker with no corners.");
            }
            
            float minX = cornersWorld.Min(corner => corner.X);
            float maxX = cornersWorld.Max(corner => corner.X);
            float minY = cornersWorld.Min(corner => -corner.Y);
            float maxY = cornersWorld.Max(corner => -corner.Y);

            Vector3 center = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, Z);
            float sizeX = maxX - center.x;
            float sizeY = maxY - center.y;


            Transform mTransform = transform;
            mTransform.parent = Settings.ARCamera.CheckedNull()?.transform;
            mTransform.localRotation = Quaternion.identity;
            mTransform.localPosition = center;
            mTransform.localScale = HighlightScale * Vector3.one;

            topLeft.localPosition = new Vector3(-sizeX, sizeY, 0) / Scale;
            topRight.localPosition = new Vector3(sizeX, sizeY, 0) / Scale;
            bottomRight.localPosition = new Vector3(sizeX, -sizeY, 0) / Scale;
            bottomLeft.localPosition = new Vector3(-sizeX, -sizeY, 0) / Scale;
            text.transform.localPosition = new Vector3(0, sizeY, 0) / Scale;
            text.text = code;

            highlightStart = GameThread.GameTime;
            highlightTime = Mathf.Max(highlightTimeInMs / 1000f, 0);
        }

        public Bounds? Bounds => boxCollider.bounds;

        public int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        [NotNull]
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        void Update()
        {
            if (highlightStart == null)
            {
                return;
            }

            float diff = GameThread.GameTime - highlightStart.Value;
            if (diff > highlightTime)
            {
                highlightStart = null;
                this.ReturnToPool();
                return;
            }

            float t = Mathf.Sqrt(1 - diff / highlightTime);
            transform.localScale = (Scale + (HighlightScale - Scale) * t) * Vector3.one;
        }

        public void Suspend()
        {
        }
    }
}