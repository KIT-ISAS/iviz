#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class DisplaysType
    {
        readonly Dictionary<Type, ResourceKey<GameObject>?> resourceByType;

        public ResourceKey<GameObject> Cube { get; }
        public ResourceKey<GameObject> Cylinder { get; }
        public ResourceKey<GameObject> Sphere { get; }
        public ResourceKey<GameObject> Pyramid { get; }
        public ResourceKey<GameObject> Text { get; }
        public ResourceKey<GameObject> LineConnector { get; }
        public ResourceKey<GameObject> BoundaryFrame { get; }
        public ResourceKey<GameObject> Arrow { get; }
        public ResourceKey<GameObject> MeshList { get; }
        public ResourceKey<GameObject> PointList { get; }
        public ResourceKey<GameObject> MeshTriangles { get; }
        public ResourceKey<GameObject> Image { get; }
        public ResourceKey<GameObject> Square { get; }
        public ResourceKey<GameObject> Line { get; }
        public ResourceKey<GameObject> Grid { get; }
        public ResourceKey<GameObject> DepthImageDisplay { get; }
        public ResourceKey<GameObject> OccupancyGridDisplay { get; }
        public ResourceKey<GameObject> RadialScanDisplay { get; }
        public ResourceKey<GameObject> AxisFrame { get; }
        public ResourceKey<GameObject> AngleAxis { get; }
        public ResourceKey<GameObject> Trail { get; }
        public ResourceKey<GameObject> GridMap { get; }
        public ResourceKey<GameObject> OccupancyGridTextureResource { get; }

        public ResourceKey<GameObject> XRDialog { get; }
        public ResourceKey<GameObject> XRDialogIcon { get; }
        public ResourceKey<GameObject> XRDialogMenu { get; }
        public ResourceKey<GameObject> XRDialogShort { get; }
        public ResourceKey<GameObject> ARTfFrame { get; }
        public ResourceKey<GameObject> XRDialogNotice { get; }
        public ResourceKey<GameObject> XRButtonDialog { get; }
        public ResourceKey<GameObject> RotationDisc { get; }
        public ResourceKey<GameObject> SpringDisc { get; }
        public ResourceKey<GameObject> SpringDisc3D { get; }
        public ResourceKey<GameObject> TrajectoryDisc { get; }
        public ResourceKey<GameObject> Tooltip { get; }
        public ResourceKey<GameObject> TooltipWidget { get; }
        public ResourceKey<GameObject> TargetArea { get; }
        public ResourceKey<GameObject> PositionDisc3D { get; }
        public ResourceKey<GameObject> PositionDisc { get; }
        public ResourceKey<GameObject> ARMarkerHighlighter { get; }
        public ResourceKey<GameObject> RoundedPlane { get; }
        public ResourceKey<GameObject> Leash { get; }
        public ResourceKey<GameObject> Reticle { get; }
        public ResourceKey<GameObject> Ring { get; }
        public ResourceKey<GameObject> SelectionFrame { get; }
        public ResourceKey<GameObject> CanvasHolder { get; }
        public ResourceKey<GameObject> PalmCompass { get; }

        public ResourceKey<GameObject> Boundary { get; }
        public ResourceKey<GameObject> BoundaryCheck { get; }
        public ResourceKey<GameObject> BoundaryLink { get; }

        public DisplaysType()
        {
            var assetHolder = ResourcePool.AssetHolder;
            var appAssetHolder = ResourcePool.AppAssetHolder;

            Cube = Create(assetHolder.Cube, nameof(assetHolder.Cube));
            Cylinder = Create(assetHolder.Cylinder, nameof(assetHolder.Cylinder));
            Sphere = Create(assetHolder.Sphere, nameof(assetHolder.Sphere));
            Text = Create(assetHolder.Text, nameof(assetHolder.Text));
            LineConnector = Create(appAssetHolder.LineConnector, nameof(appAssetHolder.LineConnector));
            BoundaryFrame = Create(appAssetHolder.BoundaryFrame, nameof(appAssetHolder.BoundaryFrame));
            Arrow = Create(appAssetHolder.Arrow, nameof(appAssetHolder.Arrow));
            MeshList = Create(assetHolder.MeshList, nameof(assetHolder.MeshList));
            PointList = Create(assetHolder.PointList, nameof(assetHolder.PointList));
            MeshTriangles = Create(assetHolder.MeshTriangles, nameof(assetHolder.MeshTriangles));
            //TfFrame = Create(appAssetHolder.TFFrame);
            Image = Create(appAssetHolder.Image, nameof(appAssetHolder.Image));
            Square = Create(assetHolder.Plane, nameof(assetHolder.Plane));
            Line = Create(assetHolder.Line, nameof(assetHolder.Line));
            Grid = Create(appAssetHolder.Grid, nameof(appAssetHolder.Grid));
            DepthImageDisplay = Create(appAssetHolder.DepthImage, nameof(appAssetHolder.DepthImage));
            OccupancyGridDisplay = Create(appAssetHolder.OccupancyGrid, nameof(appAssetHolder.OccupancyGrid));
            RadialScanDisplay = Create(appAssetHolder.RadialScan, nameof(appAssetHolder.RadialScan));
            AxisFrame = Create(appAssetHolder.AxisFrame, nameof(appAssetHolder.AxisFrame));
            AngleAxis = Create(appAssetHolder.AngleAxis, nameof(appAssetHolder.AngleAxis));
            Trail = Create(appAssetHolder.Trail, nameof(appAssetHolder.Trail));
            GridMap = Create(appAssetHolder.GridMap, nameof(appAssetHolder.GridMap));
            OccupancyGridTextureResource = Create(appAssetHolder.OccupancyGridTexture, nameof(appAssetHolder.OccupancyGridTexture));

            Pyramid = Create(appAssetHolder.Pyramid, nameof(appAssetHolder.Pyramid));

            XRDialog = Create(appAssetHolder.ARDialog, nameof(appAssetHolder.ARDialog));
            XRDialogIcon = Create(appAssetHolder.ARDialogIcon, nameof(appAssetHolder.ARDialogIcon));
            XRDialogMenu = Create(appAssetHolder.ARDialogMenu, nameof(appAssetHolder.ARDialogMenu));
            XRDialogShort = Create(appAssetHolder.ARDialogShort, nameof(appAssetHolder.ARDialogShort));
            ARTfFrame = Create(appAssetHolder.ARTfFrame, nameof(appAssetHolder.ARTfFrame));
            XRDialogNotice = Create(appAssetHolder.ARDialogNotice, nameof(appAssetHolder.ARDialogNotice));
            XRButtonDialog = Create(appAssetHolder.ARButtonDialog, nameof(appAssetHolder.ARButtonDialog));
            ARMarkerHighlighter =
                Create(appAssetHolder.ARMarkerHighlighter, nameof(appAssetHolder.ARMarkerHighlighter));

            RotationDisc = Create(appAssetHolder.RotationDisc, nameof(appAssetHolder.RotationDisc));
            SpringDisc = Create(appAssetHolder.SpringDisc, nameof(appAssetHolder.SpringDisc));
            SpringDisc3D = Create(appAssetHolder.SpringDisc3D, nameof(appAssetHolder.SpringDisc3D));
            TrajectoryDisc = Create(appAssetHolder.TrajectoryDisc, nameof(appAssetHolder.TrajectoryDisc));
            Tooltip = Create(appAssetHolder.Tooltip, nameof(appAssetHolder.Tooltip));
            TooltipWidget = Create(appAssetHolder.TooltipWidget, nameof(appAssetHolder.TooltipWidget));
            TargetArea = Create(appAssetHolder.TargetArea, nameof(appAssetHolder.TargetArea));
            PositionDisc3D = Create(appAssetHolder.PositionDisc3D, nameof(appAssetHolder.PositionDisc3D));
            PositionDisc = Create(appAssetHolder.PositionDisc, nameof(appAssetHolder.PositionDisc));

            RoundedPlane = Create(appAssetHolder.RoundedPlane, nameof(appAssetHolder.RoundedPlane));
            Leash = Create(appAssetHolder.Leash, nameof(appAssetHolder.Leash));
            Reticle = Create(appAssetHolder.Reticle, nameof(appAssetHolder.Reticle));
            Ring = Create(appAssetHolder.Ring, nameof(appAssetHolder.Ring));
            SelectionFrame = Create(appAssetHolder.SelectionFrame, nameof(appAssetHolder.SelectionFrame));
            CanvasHolder = Create(appAssetHolder.CanvasHolder, nameof(appAssetHolder.CanvasHolder));
            PalmCompass = Create(appAssetHolder.PalmCompass, nameof(appAssetHolder.PalmCompass));

            Boundary = Create(appAssetHolder.Boundary, nameof(appAssetHolder.Boundary));
            BoundaryCheck = Create(appAssetHolder.BoundaryCheck, nameof(appAssetHolder.BoundaryCheck));
            BoundaryLink = Create(appAssetHolder.BoundaryLink, nameof(appAssetHolder.BoundaryLink));

            resourceByType = CreateTypeDictionary(this);

            static ResourceKey<GameObject> Create(GameObject obj, string? msg = null) => new(obj, msg);
        }

        static Dictionary<Type, ResourceKey<GameObject>?> CreateTypeDictionary(DisplaysType o)
        {
            Dictionary<Type, ResourceKey<GameObject>?> resourceByType = new();
            PropertyInfo[] properties = typeof(DisplaysType).GetProperties();
            foreach (var property in properties)
            {
                if (typeof(ResourceKey<GameObject>) != property.PropertyType)
                {
                    continue;
                }

                var info = (ResourceKey<GameObject>?)property.GetValue(o);
                if (info == null)
                {
                    RosLogger.Error($"{nameof(DisplaysType)}: Property {property.Name} has not been set!");
                    continue;
                }

                var display = info.Object.GetComponent<IDisplay>();
                var type = display?.GetType();
                string? name = type?.FullName;
                if (name is null || type is null)
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

        public bool TryGetResource(Type type, [NotNullWhen(true)] out ResourceKey<GameObject>? info)
        {
            ThrowHelper.ThrowIfNull(type, nameof(type));
            return resourceByType.TryGetValue(type, out info) && info != null;
        }
    }
}