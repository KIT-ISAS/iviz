using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assimp;
using Iviz.Msgs.IvizMsgs;
using Iviz.RoslibSharp;
using Material = Assimp.Material;
using Mesh = Assimp.Mesh;

namespace Iviz.Loader
{
    public static class ModelLoader
    {
        static AssimpContext importer = new AssimpContext();
        
        static void Main(string[] args)
        {
            string fileName = @"/Users/akzeac/Downloads/crayler_data/crayler/meshes/base_link_simple.stl";

            var msg = LoadModel(fileName);

            RoslibSharp.RosClient client = new RoslibSharp.RosClient("http://i81node2:11311", "/iviz_model_service");

            GetModel call = new GetModel();
            call.Request.Uri = "package://iviz/simple_test";
            call.Request.Model = msg;
            client.CallService("/iviz_osxeditor/set_model", call);
        }

        static void Run()
        {
            RosClient client = new RosClient(
                "http://192.168.0.220:11311",
                //"http://141.3.59.5:11311",
                null,
                "http://192.168.0.157:7619"
                //"http://141.3.59.19:7621"
            );

            client.AdvertiseService<GetModelResource>("/iviz/load_model", Callback);

            WaitForCancel();
            
            client.UnadvertiseService("/iviz/load_model");
            client.Close();
        }

        static void WaitForCancel()
        {
            object o = new object();
            Console.CancelKeyPress += delegate
            {
                lock (o) Monitor.Pulse(o);
            };

            lock (o) Monitor.Wait(o);
        }

        void Callback(GetModelResource msg)
        {
            //msg.Request.
            throw new NotImplementedException();
        }


        Model LoadModel(string fileName)
        {
            Scene model = importer.ImportFile(fileName, PostProcessPreset.TargetRealTimeMaximumQuality);

            Model msg = new Model();
            msg.Meshes = new Msgs.IvizMsgs.Mesh[model.Meshes.Count];

            List<Triangle> faces = new List<Triangle>();

            for (int i = 0; i < model.MeshCount; i++)
            {
                Mesh srcMesh = model.Meshes[i];

                faces.Clear();
                for (int j = 0; j < srcMesh.FaceCount; j++)
                {
                    Face face = srcMesh.Faces[j];
                    switch (face.IndexCount)
                    {
                        case 3:
                            faces.Add(new Triangle(
                                (uint) face.Indices[0],
                                (uint) face.Indices[1],
                                (uint) face.Indices[2]
                            ));
                            break;
                        case 4:
                            faces.Add(new Triangle(
                                (uint) face.Indices[0],
                                (uint) face.Indices[1],
                                (uint) face.Indices[2]
                            ));
                            faces.Add(new Triangle(
                                (uint) face.Indices[0],
                                (uint) face.Indices[2],
                                (uint) face.Indices[3]
                            ));
                            break;
                    }
                }

                Msgs.IvizMsgs.Mesh dstMesh = new Msgs.IvizMsgs.Mesh
                (
                    Name: srcMesh.Name ?? "[mesh]",
                    Bounds: new BoundingBox(),
                    Vertices: srcMesh.Vertices.Select(x => ToVector3(x)).ToArray(),
                    Normals: srcMesh.Normals.Select(x => ToVector3(x)).ToArray(),
                    TexCoords: srcMesh.HasTextureCoords(0)
                        ? srcMesh.TextureCoordinateChannels[0].Select(x => ToVector2(x)).ToArray()
                        : null,
                    Colors: srcMesh.HasVertexColors(0)
                        ? srcMesh.VertexColorChannels[0].Select(x => ToColor(x)).ToArray()
                        : null,
                    Faces: faces.ToArray(),
                    MaterialIndex: (uint) srcMesh.MaterialIndex
                );

                msg.Meshes[i] = dstMesh;
            }

            msg.Materials = new Msgs.IvizMsgs.Material[model.MaterialCount];
            for (int i = 0; i < model.MaterialCount; i++)
            {
                Material srcMaterial = model.Materials[i];
                Msgs.IvizMsgs.Material dstMaterial = new Msgs.IvizMsgs.Material
                (
                    Name: srcMaterial.Name ?? "[material]",
                    Ambient: ToColor(srcMaterial.ColorAmbient),
                    Diffuse: ToColor(srcMaterial.ColorDiffuse),
                    Emissive: ToColor(srcMaterial.ColorEmissive),
                    DiffuseTexture: new Texture()
                );

                msg.Materials[i] = dstMaterial;
            }

            return msg;
        }

        static Vector3 ToVector3(in Vector3D v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        static Vector2 ToVector2(in Vector3D v)
        {
            return new Vector2(v.X, v.Y);
        }

        static Color ToColor(in Color4D color)
        {
            int r = (int) (Math.Max(Math.Min(color.R, 1), 0) * 255);
            int g = (int) (Math.Max(Math.Min(color.G, 1), 0) * 255);
            int b = (int) (Math.Max(Math.Min(color.B, 1), 0) * 255);
            int a = (int) (Math.Max(Math.Min(color.A, 1), 0) * 255);
            return new Color((byte) r, (byte) g, (byte) b, (byte) a);
        }
    }
}