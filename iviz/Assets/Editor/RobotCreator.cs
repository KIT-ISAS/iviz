using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Linq;
using RosSharp.Urdf;
using Joint = RosSharp.Urdf.Joint;
using RosSharp;
using UnityEditor;
using RosSharp.Urdf.Editor;

namespace Iviz.App
{
    public class RobotCreator : Editor
    {
        const string packageRoot = "Assets/Resources_Internal/Robots/";
        static readonly Link.Inertial DummyInertial = new Link.Inertial(
                1.0f,
                new Origin(new double[] { 0, 0, 0 },
                new double[] { 0, 0, 0 }),
                new Link.Inertial.Inertia(1, 0, 0, 1, 0, 1)
            );


        static RosSharp.Urdf.Robot Robot;

        [MenuItem("MyMenu/Do Something")]
        static void DoSomething()
        {
            //string projectRoot = packageRoot + "husky_description";
            //string urdfFile = projectRoot + "/urdf/husky.urdf.xml";
            //string materialsRoot = projectRoot + "/unity_materials/";

            //string projectRoot = packageRoot + "iosb";
            //string urdfFile = projectRoot + "/urdf/iosb.urdf.xml";
            //string materialsRoot = projectRoot + "/unity_materials/";

            //string projectRoot = packageRoot + "turtlebot3_description";
            //string urdfFile = projectRoot + "/urdf/turtlebot3_burger.urdf.xml";
            //string urdfFile = projectRoot + "/urdf/turtlebot3_waffle.urdf.xml";
            //string materialsRoot = projectRoot + "/unity_materials/";

            string projectRoot = packageRoot + "pr2_description";
            string urdfFile = projectRoot + "/robots/robot.urdf.xml";
            string materialsRoot = projectRoot + "/unity_materials/";

            AssetDatabase.CreateFolder(projectRoot, "unity_materials");

            Robot = CreateRobot(urdfFile);
            if (Robot == null)
            {
                return;
            }
            GameObject robotObject = BuildRobotObject(Robot);
            robotObject.GetComponentsInChildren<Rigidbody>().ForEach(x =>
            {
                x.isKinematic = true;
                x.useGravity = false;
                x.detectCollisions = false;
            });
            robotObject.GetComponentsInChildren<HingeJointLimitsManager>().ForEach(x => x.enabled = false);
            robotObject.GetComponentsInChildren<PrismaticJointLimitsManager>().ForEach(x => x.enabled = false);

            List<Texture2D> textures = new List<Texture2D>();
            HashSet<Texture2D> set = new HashSet<Texture2D>();
            MeshRenderer[] renderers = robotObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                Material[] materials = renderer.sharedMaterials;
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == null)
                    {
                        continue;
                    }
                    Texture2D texture2D = materials[i].mainTexture as Texture2D;
                    if (texture2D != null && !set.Contains(texture2D))
                    {
                        textures.Add(texture2D);
                        set.Add(texture2D);
                    }
                }
            }

            const int AtlasSize = 1024;
            Texture2D atlas_comp = new Texture2D(AtlasSize, AtlasSize);
            Rect[] rects = atlas_comp.PackTextures(textures.ToArray(), 0, AtlasSize);

            Texture2D atlas_png = Decompress(atlas_comp);
            //AssetDatabase.CreateAsset(atlas_png, materialsRoot + "atlas.png");
            File.WriteAllBytes(materialsRoot + "atlas.png", atlas_png.EncodeToPNG());
            Debug.Log("Writing: " + materialsRoot + "atlas.png");

            AssetDatabase.ImportAsset(materialsRoot + "atlas.png");
            Texture2D atlas = AssetDatabase.LoadAssetAtPath<Texture2D>(materialsRoot + "atlas.png");
            if (atlas == null)
            {
                Debug.Log(materialsRoot + "atlas.png" + " not found!");
                return;
            }


            Material LitMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/BaseMaterials/Standard.mat");
            Material TexturedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/BaseMaterials/Standard.mat");
            Dictionary<Color, Material> colorMaterials = new Dictionary<Color, Material>
            {
                [Color.white] = LitMaterial
            };
            Dictionary<Texture2D, Material> textureMaterials = new Dictionary<Texture2D, Material>();
            for (int i = 0; i < textures.Count; i++)
            {
                Material material = Instantiate(TexturedMaterial);
                material.mainTextureOffset = new Vector2(rects[i].x, rects[i].y);
                material.mainTextureScale = new Vector2(rects[i].width, rects[i].height);
                material.mainTexture = atlas;
                AssetDatabase.CreateAsset(material, materialsRoot + "textured_" + i + ".mat");
                textureMaterials[textures[i]] = material;
            }


            foreach (MeshRenderer renderer in renderers)
            {
                Material[] materials = renderer.sharedMaterials.ToArray();
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == null)
                    {
                        materials[i] = LitMaterial;
                    }
                    Texture2D texture2D = materials[i].mainTexture as Texture2D;
                    if (texture2D == null)
                    {
                        Color color = materials[i].color;
                        if (!colorMaterials.TryGetValue(color, out Material material))
                        {
                            material = Instantiate(LitMaterial);
                            material.color = color;
                            AssetDatabase.CreateAsset(material, materialsRoot + "lit_" + colorMaterials.Count + ".mat");
                            colorMaterials[color] = material;
                        }
                        materials[i] = material;
                    }
                    else
                    {
                        if (textureMaterials.TryGetValue(texture2D, out Material material))
                        {
                            materials[i] = material;
                        }
                        else
                        {
                            materials[i] = TexturedMaterial;
                        }
                    }
                    renderer.sharedMaterials = materials;
                }
            }
        }

        public static Texture2D Decompress(Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }

            public static GameObject BuildRobotObject(RosSharp.Urdf.Robot robot)
        {
            string robotName = "Robot: " + robot.name;
            GameObject oldRobotObject = GameObject.Find(robotName);
            if (oldRobotObject != null)
            {
                DestroyImmediate(oldRobotObject);
            }
            GameObject robotObject = new GameObject("Robot: " + robot.name);
            robotObject.AddComponent<UrdfRobot>();


            CreateLink(robotObject.transform, robot.root);
            return robotObject;
        }

        public static RosSharp.Urdf.Robot CreateRobot(string file)
        {
            RosSharp.Urdf.Robot robot = new RosSharp.Urdf.Robot(file, null);
            string urdfFile = AssetDatabase.LoadAssetAtPath<TextAsset>(file)?.text;
            if (urdfFile == null)
            {
                Debug.LogWarning("RobotCreator: Cannot find asset " + file);
                return null;
            }

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(urdfFile)))
            {
                XDocument xdoc = XDocument.Load(stream);
                XElement node = xdoc.Element("robot");
                robot.name = node.Attribute("name").Value;
                robot.materials = node.Elements("material").Select(x => new Link.Visual.Material(x)).ToList();
                robot.links = node.Elements("link").Select(x => new Link(x)).ToList();
                robot.joints = node.Elements("joint").Select(x => new Joint(x)).ToList();
            }

            //robot.materials.ForEach(x => Debug.Log(x.name + " " + string.Join(",", x.color.rgba)));

            foreach (Link link in robot.links)
            {
                link.joints = robot.joints.FindAll(v => v.parent == link.name);
                if (link.inertial == null)
                {
                    link.inertial = DummyInertial;
                }
            }
            foreach (Joint joint in robot.joints)
            {
                joint.ChildLink = robot.links.Find(v => v.name == joint.child);
            }
            robot.root = FindRootLink(robot.links, robot.joints);
            return robot;
        }

        static Link FindRootLink(List<Link> links, List<Joint> joints)
        {
            if (joints.Count == 0)
            {
                return links[0];
            }

            Joint joint = joints[0];
            string parent;
            do
            {
                parent = joint.parent;
                joint = joints.Find(v => v.child == parent);
            }
            while (joint != null);
            return links.Find(v => v.name == parent);
        }

        static UrdfLink CreateLink(Transform parent, Link link = null, Joint joint = null)
        {
            GameObject linkObject = new GameObject("link");
            linkObject.transform.SetParentLocal(parent);
            UrdfLink urdfLink = linkObject.AddComponent<UrdfLink>();

            CreateVisuals(linkObject.transform, link?.visuals);

            if (link != null)
            {
                ImportLinkData(urdfLink, link, joint);
            }
            else
            {
                UrdfInertial.Create(linkObject);
            }

            return urdfLink;
        }


        static void ImportLinkData(UrdfLink urdfLink, Link link, Joint joint)
        {
            if (link.inertial == null && joint == null)
            {
                urdfLink.IsBaseLink = true;
            }

            urdfLink.gameObject.name = link.name;

            if (joint?.origin != null)
            {
                UrdfOrigin.ImportOriginData(urdfLink.transform, joint.origin);
            }

            if (link.inertial != null)
            {
                UrdfInertial.Create(urdfLink.gameObject, link.inertial);

                if (joint != null)
                {
                    UrdfJoint.Create(urdfLink.gameObject, UrdfJoint.GetJointType(joint.type), joint);
                }
            }
            else if (joint != null)
            {
                Debug.LogWarning("No Joint Component will be created in GameObject \"" + urdfLink.gameObject.name + "\" as it has no Rigidbody Component.\n"
                                 + "Please define an Inertial for Link \"" + link.name + "\" in the URDF file to create a Rigidbody Component.\n", urdfLink.gameObject);
            }

            foreach (Joint childJoint in link.joints)
            {
                Link child = childJoint.ChildLink;
                CreateLink(urdfLink.transform, child, childJoint);
            }
        }

        static void CreateVisuals(Transform parent, List<Link.Visual> visuals)
        {
            GameObject visualsObject = new GameObject("Visuals");
            visualsObject.transform.SetParentLocal(parent);
            //UrdfVisuals urdfVisuals = visualsObject.AddComponent<UrdfVisuals>();

            visualsObject.hideFlags = HideFlags.NotEditable;
            //urdfVisuals.hideFlags = HideFlags.None;

            if (visuals != null)
            {
                foreach (Link.Visual visual in visuals)
                {
                    CreateVisual(visualsObject.transform, visual);
                }
            }
        }

        static void CreateVisual(Transform parent, Link.Visual visual)
        {
            GameObject visualObject = new GameObject(visual.name ?? "unnamed");
            visualObject.transform.SetParentLocal(parent);
            UrdfVisual urdfVisual = visualObject.AddComponent<UrdfVisual>();

            urdfVisual.GeometryType = GetGeometryType(visual.geometry);
            CreateGeometry(visualObject.transform, urdfVisual.GeometryType, visual.geometry);
            //if (visualObject.GetComponentInChildren<Renderer>().material == null)
            //{
                SetUrdfMaterial(visualObject, visual.material);
            //}

            UrdfOrigin.ImportOriginData(visualObject.transform, visual.origin);
        }


        static void SetUrdfMaterial(GameObject gameObject, Link.Visual.Material urdfMaterial)
        {
            if (urdfMaterial != null)
            {
                if (urdfMaterial.color == null && urdfMaterial.texture == null)
                {
                    urdfMaterial = Robot.materials.Find(x => x.name == urdfMaterial.name);
                }
                var material = CreateMaterial(urdfMaterial);
                SetMaterial(gameObject, material);
            }
            else
            {
                //If the URDF material is not defined, and the renderer is missing
                //a material, assign the default material.
                /*
                Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
                if (renderer != null && renderer.sharedMaterial == null)
                {
                    var defaultMaterial = Resources.Load<Material>("BaseMaterials/White");
                    SetMaterial(gameObject, defaultMaterial);
                }
                */
            }
        }

        static Material CreateMaterial(Link.Visual.Material urdfMaterial)
        {
            //Debug.Log("material name: " + urdfMaterial.name);
            Material material = InitializeMaterial();
            if (urdfMaterial.color != null)
            {
                //Debug.Log("color: " + CreateColor(urdfMaterial.color));
                material.color = CreateColor(urdfMaterial.color);
            }
            else if (urdfMaterial.texture != null)
            {
                Debug.Log("texture: " + urdfMaterial.texture.filename);
                material.mainTexture = LoadTexture(urdfMaterial.texture.filename);
            } else
            {
                Debug.Log("null material " + urdfMaterial.name);
            }

            return material;
        }

        static Color CreateColor(Link.Visual.Material.Color urdfColor)
        {
            return new Color(
                (float)urdfColor.rgba[0],
                (float)urdfColor.rgba[1],
                (float)urdfColor.rgba[2],
                (float)urdfColor.rgba[3]);
        }

        static Texture2D LoadTexture(string filename)
        {
            if (filename == "")
            {
                return null;
            }
            else
            {
                string path = GetRelativeAssetPathFromUrdfPath(filename);
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                if (texture == null)
                {
                    Debug.LogWarning("Cannot find resource " + path);
                }
                return texture;
            }
        }

        static Material InitializeMaterial()
        {
            Material LitMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/BaseMaterials/Standard.mat");
            return Instantiate(LitMaterial);
        }

        static void SetMaterial(GameObject gameObject, Material material)
        {
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.sharedMaterial == null)
                {
                    renderer.sharedMaterial = material;
                }
            }
        }


        static GeometryTypes GetGeometryType(Link.Geometry geometry)
        {
            if (geometry.box != null)
            {
                return GeometryTypes.Box;
            }
            if (geometry.cylinder != null)
            {
                return GeometryTypes.Cylinder;
            }
            if (geometry.sphere != null)
            {
                return GeometryTypes.Sphere;
            }
            return GeometryTypes.Mesh;
        }

        static void CreateGeometry(Transform parent, GeometryTypes geometryType, Link.Geometry geometry = null)
        {
            GameObject geometryGameObject = null;

            switch (geometryType)
            {
                case GeometryTypes.Box:
                    geometryGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geometryGameObject.transform.DestroyImmediateIfExists<BoxCollider>();
                    geometryGameObject.GetComponentsInChildren<Renderer>().ForEach(x => x.material = null);
                    break;
                case GeometryTypes.Cylinder:
                    geometryGameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    geometryGameObject.transform.DestroyImmediateIfExists<CapsuleCollider>();
                    geometryGameObject.GetComponentsInChildren<Renderer>().ForEach(x => x.material = null);
                    break;
                case GeometryTypes.Sphere:
                    geometryGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    geometryGameObject.transform.DestroyImmediateIfExists<SphereCollider>();
                    geometryGameObject.GetComponentsInChildren<Renderer>().ForEach(x => x.material = null);
                    break;
                case GeometryTypes.Mesh:
                    if (geometry != null)
                    {
                        geometryGameObject = CreateMeshVisual(geometry.mesh);
                    }
                    break;
            }


            if (geometryGameObject != null)
            {
                geometryGameObject.transform.SetParentLocal(parent);
                if (geometry != null)
                {
                    SetScale(parent, geometry, geometryType);
                }
            }
        }

        static void SetScale(Transform transform, Link.Geometry geometry, GeometryTypes geometryType)
        {
            switch (geometryType)
            {
                case GeometryTypes.Box:
                    transform.localScale =
                        new Vector3((float)geometry.box.size[1], (float)geometry.box.size[2], (float)geometry.box.size[0]);
                    break;
                case GeometryTypes.Cylinder:
                    transform.localScale = new Vector3(
                        (float)geometry.cylinder.radius * 2,
                        (float)geometry.cylinder.length / 2,
                        (float)geometry.cylinder.radius * 2);
                    break;
                case GeometryTypes.Sphere:
                    transform.localScale = new Vector3(
                        (float)geometry.sphere.radius * 2,
                        (float)geometry.sphere.radius * 2,
                        (float)geometry.sphere.radius * 2);
                    break;
                case GeometryTypes.Mesh:
                    if (geometry?.mesh?.scale != null)
                    {
                        Vector3 scale = geometry.mesh.scale.ToVector3().Ros2UnityScale();

                        transform.localScale = Vector3.Scale(transform.localScale, scale);
                        transform.localPosition = Vector3.Scale(transform.localPosition, scale);
                    }
                    break;
            }
        }

        static GameObject CreateMeshVisual(Link.Geometry.Mesh mesh)
        {
            string path = GetRelativeAssetPathFromUrdfPath(mesh.filename);
            GameObject meshObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (meshObject == null)
            {
                Debug.LogWarning("Cannot find resource " + path);
            }
            return meshObject == null ? null : Object.Instantiate(meshObject);
        }

        static string GetRelativeAssetPathFromUrdfPath(string urdfPath)
        {
            if (!urdfPath.StartsWith(@"package://"))
            {
                Debug.LogWarning(urdfPath + " is not a valid URDF package file path. Path should start with \"package://\".");
                return null;
            }

            var path = urdfPath.Substring(10);
            /*
            string extension = Path.GetExtension(path);
            if (extension != null)
            {
                path = path.Substring(0, path.Length - extension.Length);
            }
            */
            return Path.Combine(packageRoot, path);
        }
    }

    static class TransformExtensions
    {
        public static void DestroyImmediateIfExists<T>(this Transform transform) where T : Component
        {
            T component = transform.GetComponent<T>();
            if (component != null)
                Object.DestroyImmediate(component);
        }

        public static Vector3 ToVector3(this double[] array)
        {
            return new Vector3((float)array[0], (float)array[1], (float)array[2]);
        }

        public static Vector3 Ros2UnityScale(this Vector3 vector3)
        {
            return new Vector3(vector3.y, vector3.z, vector3.x);
        }
    }
}