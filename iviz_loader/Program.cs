using System;
using System.Collections.Generic;
using System.Linq;
using Assimp;

namespace Iviz.Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"/Users/akzeac/Downloads/crayler_data/crayler/meshes/base_link_simple.stl";
            AssimpContext importer = new AssimpContext();
            Scene model = importer.ImportFile(fileName, PostProcessPreset.TargetRealTimeMaximumQuality);

            Msgs.IvizMsgs.Model msg = new Msgs.IvizMsgs.Model();
            msg.Meshes = new Msgs.IvizMsgs.Mesh[model.Meshes.Count];

            List<Msgs.IvizMsgs.Triangle> faces = new List<Msgs.IvizMsgs.Triangle>();

            for (int i = 0; i < model.MeshCount; i++)
            {
                Msgs.IvizMsgs.Mesh mesh = new Msgs.IvizMsgs.Mesh();
                mesh.Name = model.Meshes[i].Name ?? "[mesh]";
                mesh.Vertices =
                    model.Meshes[i].Vertices.Select(x => new Msgs.IvizMsgs.Vector3(x.X, x.Y, x.Z)).ToArray();
                mesh.Normals =
                    model.Meshes[i].Normals.Select(x => new Msgs.IvizMsgs.Vector3(x.X, x.Y, x.Z)).ToArray();
                if (model.Meshes[i].HasTextureCoords(0))
                {
                    mesh.TexCoords =
                        model.Meshes[i].TextureCoordinateChannels[0].Select(x => new Msgs.IvizMsgs.Vector2(x.X, x.Y)).ToArray();
                }
                if (model.Meshes[i].HasVertexColors(0))
                {
                    mesh.Colors =
                        model.Meshes[i].VertexColorChannels[0].Select(x => ToColor(x)).ToArray();
                }

                faces.Clear();
                for (int j = 0; j < model.Meshes[i].FaceCount; j++)
                {
                    Face face = model.Meshes[i].Faces[j];
                    if (face.IndexCount == 3)
                    {
                        faces.Add(new Msgs.IvizMsgs.Triangle(
                            (uint)face.Indices[0],
                            (uint)face.Indices[1],
                            (uint)face.Indices[2]
                            ));
                    }
                    else if (face.IndexCount == 4)
                    {
                        faces.Add(new Msgs.IvizMsgs.Triangle(
                            (uint)face.Indices[0],
                            (uint)face.Indices[1],
                            (uint)face.Indices[2]
                            ));
                        faces.Add(new Msgs.IvizMsgs.Triangle(
                            (uint)face.Indices[0],
                            (uint)face.Indices[2],
                            (uint)face.Indices[3]
                            ));
                    }
                }

                mesh.Faces = faces.ToArray();
                mesh.MaterialIndex = (uint)model.Meshes[i].MaterialIndex;

                msg.Meshes[i] = mesh;
            }

            msg.Materials = new Msgs.IvizMsgs.Material[model.MaterialCount];
            for (int i = 0; i < model.MaterialCount; i++)
            {
                Msgs.IvizMsgs.Material material = new Msgs.IvizMsgs.Material();
                material.Name = model.Materials[i].Name ?? "[material]";
                material.Ambient = ToColor(model.Materials[i].ColorAmbient);
                material.Diffuse = ToColor(model.Materials[i].ColorDiffuse);
                material.Emissive = ToColor(model.Materials[i].ColorEmissive);

                msg.Materials[i] = material;
            }

            RoslibSharp.RosClient client = new RoslibSharp.RosClient("http://i81node2:11311", "/iviz_loader");

            Msgs.IvizMsgs.SetModel call = new Msgs.IvizMsgs.SetModel();
            call.Request.Uri = "package://iviz/simple_test";
            call.Request.Model = msg;
            client.CallService("/iviz_osxeditor/set_model", call);


        }

        static Msgs.IvizMsgs.Color ToColor(in Color4D color)
        {
            int r = (int)(Math.Max(Math.Min(color.R, 1), 0) * 255);
            int g = (int)(Math.Max(Math.Min(color.G, 1), 0) * 255);
            int b = (int)(Math.Max(Math.Min(color.B, 1), 0) * 255);
            int a = (int)(Math.Max(Math.Min(color.A, 1), 0) * 255);
            return new Msgs.IvizMsgs.Color((byte)r, (byte)g, (byte)b, (byte)a);
        }
    }
}
