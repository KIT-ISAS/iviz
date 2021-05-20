using System;
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
        MeshMarkerResource cube;

        protected override void Awake()
        {
            base.Awake();
            cube = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, transform);
            cube.Layer = LayerType.IgnoreRaycast;
            cube.CastsShadows = false;
            MainColor = Color.blue.WithAlpha(0.5f);
            Caption = "0.5 m/s";
        }

        public override Color MainColor
        {
            get => cube.Color;
            set => cube.Color = value;
        }

        public string Caption
        {
            get => text.text;
            set
            {
                text.text = value;
                cube.Transform.localScale = new Vector3(text.preferredWidth + 0.1f, text.preferredHeight + 0.05f, 0.01f);
                BoxCollider.size = cube.Transform.localScale;
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            Caption = "";
        }

        void IRecyclable.SplitForRecycle()
        {
            cube.ReturnToPool(Resource.Displays.Cube);
        }

        protected override void Update()
        {
            base.Update();
            Transform.LookAt(2 * Transform.position - Settings.MainCameraTransform.position, Vector3.up);
        }
    }
}