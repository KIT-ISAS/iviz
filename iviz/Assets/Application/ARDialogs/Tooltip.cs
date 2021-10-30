using System;
using Application.Displays;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class Tooltip : ARWidget, IRecyclable
    {
        [SerializeField] TMP_Text text = null;
        RoundedPlaneResource cube;

        protected override void Awake()
        {
            base.Awake();
            cube = ResourcePool.RentDisplay<RoundedPlaneResource>(transform);
            cube.Radius = 3f;
            cube.Layer = LayerType.IgnoreRaycast;
            cube.ShadowsEnabled = false;
            BackgroundColor = Color.blue;
            CaptionColor = Color.white;
            Caption = "0.5 m/s";
        }

        public Color BackgroundColor
        {
            get => cube.Color;
            set => cube.Color = value;
        }

        public override Color MainColor
        {
            get => BackgroundColor;
            set => BackgroundColor = value;
        }
        
        public Color CaptionColor
        {
            get => text.color;
            set => text.color = value;
        }
        
        public override Color SecondaryColor
        {
            get => CaptionColor;
            set => CaptionColor = value;
        }

        public string Caption
        {
            get => text.text;
            set
            {
                text.text = value;
                cube.Size = new Vector2(text.preferredWidth + 5f, text.preferredHeight + 2f);
                BoxCollider.size = cube.Bounds.size;
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            Caption = "";
        }

        void IRecyclable.SplitForRecycle()
        {
            cube.ReturnToPool();
        }

        public void PointToCamera()
        {
            Transform.LookAt(2 * Transform.position - Settings.MainCameraTransform.position, Vector3.up);
        }
        
        protected override void Update()
        {
            base.Update();
            PointToCamera();
        }
    }
}