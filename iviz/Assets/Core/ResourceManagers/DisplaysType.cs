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
        public Info<GameObject> NamedBoundary { get; }
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

        public DisplaysType()
        {
            Cube = new Info<GameObject>("CoreDisplays/Cube");
            Cylinder = new Info<GameObject>("CoreDisplays/Cylinder");
            Sphere = new Info<GameObject>("CoreDisplays/Sphere");
            Text = new Info<GameObject>("CoreDisplays/Text");
            LineConnector = new Info<GameObject>("Displays/LineConnector");
            NamedBoundary = new Info<GameObject>("Displays/NamedBoundary");
            Arrow = new Info<GameObject>("Displays/Arrow");
            MeshList = new Info<GameObject>("CoreDisplays/MeshList");
            PointList = new Info<GameObject>("CoreDisplays/PointList");
            MeshTriangles = new Info<GameObject>("CoreDisplays/MeshTriangles");
            TfFrame = new Info<GameObject>("Displays/TFFrame");
            Image = new Info<GameObject>("Displays/ImageResource");
            Square = new Info<GameObject>("CoreDisplays/Plane");
            Line = new Info<GameObject>("CoreDisplays/Line");
            Grid = new Info<GameObject>("Displays/Grid");
            DepthImageResource = new Info<GameObject>("Displays/DepthImageResource");
            OccupancyGridResource = new Info<GameObject>("Displays/OccupancyGridResource");
            RadialScanResource = new Info<GameObject>("Displays/RadialScanResource");
            ARMarkerResource = new Info<GameObject>("Displays/ARMarkerResource");
            AxisFrame = new Info<GameObject>("Displays/AxisFrameResource");
            AngleAxis = new Info<GameObject>("Displays/AngleAxis");
            Trail = new Info<GameObject>("Displays/Trail");
            InteractiveControl = Settings.IsHololens
                ? new Info<GameObject>("Hololens Assets/HololensControl")
                : new Info<GameObject>("Displays/InteractiveControl");
            GridMap = new Info<GameObject>("Displays/GridMap");

            resourceByType = CreateTypeDictionary();
        }

        [NotNull]
        Dictionary<Type, Info<GameObject>> CreateTypeDictionary()
        {
            Dictionary<Type, Info<GameObject>> tmpResourceByType = new Dictionary<Type, Info<GameObject>>();
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                if (!typeof(Info<GameObject>).IsAssignableFrom(property.PropertyType))
                {
                    continue;
                }

                Info<GameObject> info = (Info<GameObject>) property.GetValue(this);
                IDisplay display = info.Object.GetComponent<IDisplay>();
                Type type = display?.GetType();
                string name = type?.FullName;
                if (name is null)
                {
                    continue;
                }

                if (tmpResourceByType.ContainsKey(type))
                {
                    tmpResourceByType[type] = null; // not unique! invalidate
                    continue;
                }

                tmpResourceByType[type] = info;
            }

            return tmpResourceByType;
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