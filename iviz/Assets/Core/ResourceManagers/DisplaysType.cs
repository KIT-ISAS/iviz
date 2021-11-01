using System;
using System.Collections.Generic;
using System.Reflection;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class DisplaysType
    {
        readonly Dictionary<Type, Info<GameObject>> resourceByType;

        public Info<GameObject> Cube { get; }
        public Info<GameObject> Cylinder { get; }
        public Info<GameObject> Sphere { get; }
        public Info<GameObject> Text { get; }
        public Info<GameObject> LineConnector { get; }
        public Info<GameObject> BoundaryFrame { get; }
        public Info<GameObject> Arrow { get; }
        public Info<GameObject> MeshList { get; }
        public Info<GameObject> PointList { get; }
        public Info<GameObject> MeshTriangles { get; }
        public Info<GameObject> TfFrame { get; }
        public Info<GameObject> Image { get; }
        public Info<GameObject> Square { get; }
        public Info<GameObject> Line { get; }
        public Info<GameObject> Grid { get; }
        public Info<GameObject> DepthImageResource { get; }
        public Info<GameObject> OccupancyGridResource { get; }
        public Info<GameObject> RadialScanResource { get; }
        public Info<GameObject> ARMarkerResource { get; }
        public Info<GameObject> AxisFrame { get; }
        public Info<GameObject> AngleAxis { get; }
        public Info<GameObject> Trail { get; }
        public Info<GameObject> InteractiveControl { get; }
        public Info<GameObject> GridMap { get; }
        public Info<GameObject> OccupancyGridTextureResource { get; }
        
        public Info<GameObject> ARDialog { get; }
        public Info<GameObject> ARDialogIcon { get; }
        public Info<GameObject> ARDialogMenu { get; }
        public Info<GameObject> ARDialogShort { get; }
        public Info<GameObject> ARTfFrame { get; }
        public Info<GameObject> ARDialogNotice { get; }
        public Info<GameObject> ARButtonDialog { get; }
        public Info<GameObject> RotationDisc { get; }
        public Info<GameObject> SpringDisc { get; }
        public Info<GameObject> SpringDisc3D { get; }
        public Info<GameObject> TrajectoryDisc { get; }
        public Info<GameObject> Tooltip { get; }
        public Info<GameObject> TargetArea { get; }
        public Info<GameObject> PositionDisc3D { get; }
        public Info<GameObject> PositionDisc { get; }
        public Info<GameObject> ARMarkerHighlighter { get; }

        public Info<GameObject> ClickedPoseHighlighter { get; }
        public Info<GameObject> TfFrameHighlighter { get; }

        public Info<GameObject> RoundedPlane { get; }

        public DisplaysType()
        {
            var assetHolder = UnityEngine.Resources.Load<GameObject>("Asset Holder").GetComponent<AssetHolder>();
            var appAssetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder").GetComponent<AppAssetHolder>();
            try
            {
                Cube = new Info<GameObject>(assetHolder.Cube);
                Cylinder = new Info<GameObject>(assetHolder.Cylinder);
                Sphere = new Info<GameObject>(assetHolder.Sphere);
                Text = new Info<GameObject>(assetHolder.Text);
                LineConnector = new Info<GameObject>(appAssetHolder.LineConnector);
                BoundaryFrame = new Info<GameObject>(appAssetHolder.BoundaryFrame);
                Arrow = new Info<GameObject>(appAssetHolder.Arrow);
                MeshList = new Info<GameObject>(assetHolder.MeshList);
                PointList = new Info<GameObject>(assetHolder.PointList);
                MeshTriangles = new Info<GameObject>(assetHolder.MeshTriangles);
                TfFrame = new Info<GameObject>(appAssetHolder.TFFrame);
                Image = new Info<GameObject>(appAssetHolder.Image);
                Square = new Info<GameObject>(assetHolder.Plane);
                Line = new Info<GameObject>(assetHolder.Line);
                Grid = new Info<GameObject>(appAssetHolder.Grid);
                DepthImageResource = new Info<GameObject>(appAssetHolder.DepthImage);
                OccupancyGridResource = new Info<GameObject>(appAssetHolder.OccupancyGrid);
                RadialScanResource = new Info<GameObject>(appAssetHolder.RadialScan);
                ARMarkerResource = new Info<GameObject>(appAssetHolder.ARMarkerResource);
                AxisFrame = new Info<GameObject>(appAssetHolder.AxisFrame);
                AngleAxis = new Info<GameObject>(appAssetHolder.AngleAxis);
                Trail = new Info<GameObject>(appAssetHolder.Trail);
                InteractiveControl = new Info<GameObject>(appAssetHolder.InteractiveControl);
                GridMap = new Info<GameObject>(appAssetHolder.GridMap);
                OccupancyGridTextureResource = new Info<GameObject>(appAssetHolder.OccupancyGridTexture);

                ARDialog = new Info<GameObject>(appAssetHolder.ARDialog);
                ARDialogIcon = new Info<GameObject>(appAssetHolder.ARDialogIcon);
                ARDialogMenu = new Info<GameObject>(appAssetHolder.ARDialogMenu);
                ARDialogShort = new Info<GameObject>(appAssetHolder.ARDialogShort);
                ARTfFrame = new Info<GameObject>(appAssetHolder.ARTfFrame);
                ARDialogNotice = new Info<GameObject>(appAssetHolder.ARDialogNotice);
                ARButtonDialog = new Info<GameObject>(appAssetHolder.ARButtonDialog);
                ARMarkerHighlighter = new Info<GameObject>(appAssetHolder.ARMarkerHighlighter);
                
                RotationDisc = new Info<GameObject>(appAssetHolder.RotationDisc);
                SpringDisc = new Info<GameObject>(appAssetHolder.SpringDisc);
                SpringDisc3D = new Info<GameObject>(appAssetHolder.SpringDisc3D);
                TrajectoryDisc = new Info<GameObject>(appAssetHolder.TrajectoryDisc);
                Tooltip = new Info<GameObject>(appAssetHolder.Tooltip);
                TargetArea = new Info<GameObject>(appAssetHolder.TargetArea);
                PositionDisc3D = new Info<GameObject>(appAssetHolder.PositionDisc3D);
                PositionDisc = new Info<GameObject>(appAssetHolder.PositionDisc);

                ClickedPoseHighlighter = new Info<GameObject>(appAssetHolder.ClickedPoseHighlighter);
                TfFrameHighlighter = new Info<GameObject>(appAssetHolder.TfFrameHighlighter);

                RoundedPlane = new Info<GameObject>(appAssetHolder.RoundedPlane);

                resourceByType = CreateTypeDictionary(this);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("DisplaysType: Missing at least one asset!");
            }
        }

        [NotNull]
        static Dictionary<Type, Info<GameObject>> CreateTypeDictionary(DisplaysType o)
        {
            Dictionary<Type, Info<GameObject>> resourceByType = new Dictionary<Type, Info<GameObject>>();
            PropertyInfo[] properties = typeof(DisplaysType).GetProperties();
            foreach (var property in properties)
            {
                if (typeof(Info<GameObject>) != property.PropertyType)
                {
                    continue;
                }

                Info<GameObject> info = (Info<GameObject>) property.GetValue(o);
                if (info == null)
                {
                    Debug.LogError("DisplaysType: Property " + property.Name + " has not been set!");
                    continue;
                }
                IDisplay display = info.Object.GetComponent<IDisplay>();
                Type type = display?.GetType();
                string name = type?.FullName;
                if (name is null)
                {
                    continue;
                }

                if (resourceByType.ContainsKey(type))
                {
                    resourceByType[type] = null; // not unique! invalidate
                    continue;
                }

                resourceByType[type] = info;
            }

            return resourceByType;
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        public bool TryGetResource([NotNull] Type type, out Info<GameObject> info)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return resourceByType.TryGetValue(type, out info) && info != null;
        }
    }
}