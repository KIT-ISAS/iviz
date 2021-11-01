using System;
using System.Text;
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
        uint? prevTextHash;

        protected override void Awake()
        {
            base.Awake();
            cube = ResourcePool.RentDisplay<RoundedPlaneResource>(transform);
            cube.Radius = 3f;
            cube.Layer = LayerType.IgnoreRaycast;
            cube.ShadowsEnabled = false;
            cube.Metallic = 1f;
            cube.Smoothness = 1f;
            //BackgroundColor = Color.blue;
            //CaptionColor = Color.white;
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
                uint hash = Crc32Calculator.Compute(value);
                if (hash == prevTextHash)
                {
                    return;
                }
                
                prevTextHash = hash;
                text.text = value;
                UpdateSize();
            }
        }

        public void SetCaption([NotNull] StringBuilder str)
        {
            uint hash = Crc32Calculator.Compute(str);
            if (hash == prevTextHash)
            {
                return;
            }
                
            prevTextHash = hash;
            text.SetText(str);
            UpdateSize();
        }

        void UpdateSize()
        {
            cube.Size = new Vector2(text.preferredWidth + 5f, text.preferredHeight + 2f);
            BoxCollider.size = cube.Bounds.size;
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