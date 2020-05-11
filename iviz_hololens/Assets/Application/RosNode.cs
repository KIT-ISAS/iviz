using Iviz.Msgs.sensor_msgs;
using Iviz.Msgs.tf2_msgs;
using Iviz.Msgs.visualization_msgs;
using Iviz.RoslibSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.WebCam;


namespace Iviz.Hololens
{
    public class RosNode : MonoBehaviour
    {
        public GameObject billboard;
        public GameObject cameraSrc;
        public GameObject cameraDst;

        SurfaceObserver surfaceObserver;

        const string markerTopic = "/hololens/environment";
        RosPublisher markerPublisher;

        const string tfTopic = "/tf";
        RosPublisher tfPublisher;

        const string imageTopic = "/hololens/image_color";
        RosPublisher imagePublisher;

        WebCamTexture texture;

        Quaternion startQuaternion;
        Vector3 startPosition;

        void Awake()
        {
            startQuaternion = transform.rotation;
            startPosition = transform.position;
        }

        void Start()
        {
            RoslibSharp.Logger.Log = Debug.Log;
            RoslibSharp.Logger.LogError = Debug.LogError;
            RoslibSharp.Logger.LogDebug = Debug.Log;
            RosClient client = new RosClient(
                "http://141.3.59.5:11311",
                "/IvizHololens",
                "http://141.3.59.140:9014"
                );

            client.Advertise<Marker>(markerTopic, out markerPublisher);
            client.Advertise<TFMessage>(tfTopic, out tfPublisher);
            client.Advertise<Image>(imageTopic, out imagePublisher);

            surfaceObserver = new SurfaceObserver();
            surfaceObserver.SetVolumeAsAxisAlignedBox(Vector3.zero, new Vector3(3, 3, 3));
            StartCoroutine(UpdateLoop());

            texture = new WebCamTexture(1280, 720, 15);
            texture.Play();
            StartCoroutine(ImageLoop());
        }

        IEnumerator UpdateLoop()
        {
            var wait = new WaitForSeconds(2.5f);
            while (true)
            {
                surfaceObserver.SetVolumeAsAxisAlignedBox(cameraSrc.transform.position, new Vector3(3, 3, 3));
                surfaceObserver.Update(OnSurfaceChanged);
                yield return wait;
            }
        }

        readonly Dictionary<SurfaceId, GameObject> spatialMeshObjects = new Dictionary<SurfaceId, GameObject>();

        static Msgs.geometry_msgs.Point ToRos(Vector3 v)
        {
            return new Msgs.geometry_msgs.Point
            {
                x = v.z,
                y = -v.x,
                z = v.y
            };
        }

        static Msgs.geometry_msgs.Vector3 ToRosVector(Vector3 v)
        {
            return new Msgs.geometry_msgs.Vector3
            {
                x = v.z,
                y = -v.x,
                z = v.y
            };
        }

        static Msgs.geometry_msgs.Quaternion ToRos(Quaternion q)
        {
            return new Msgs.geometry_msgs.Quaternion
            {
                x = -q.z,
                y = q.x,
                z = -q.y,
                w = q.w
            };
        }

        static Msgs.geometry_msgs.Transform ToRosTransform(Transform t)
        {
            return new Msgs.geometry_msgs.Transform
            {
                rotation = ToRos(t.rotation),
                translation = ToRosVector(t.position)
            };
        }

        readonly List<Msgs.geometry_msgs.Point> points = new List<Msgs.geometry_msgs.Point>();

        uint markerSeq = 0;
        void OnSurfaceChanged(SurfaceId surfaceId, SurfaceChange changeType, Bounds bounds, DateTime updateTime)
        {
            switch (changeType)
            {
                case SurfaceChange.Added:
                case SurfaceChange.Updated:
                    //Debug.Log("Updated " + spatialMeshObjects.Count);
                    if (!spatialMeshObjects.ContainsKey(surfaceId))
                    {
                        spatialMeshObjects[surfaceId] = new GameObject("spatial-mapping-" + surfaceId);
                        //spatialMeshObjects[surfaceId].transform.SetParent(transform.parent, true);
                        spatialMeshObjects[surfaceId].AddComponent<MeshRenderer>();
                        //spatialMeshObjects[surfaceId].GetComponent<MeshRenderer>().enabled = false;
                    }
                    GameObject target = spatialMeshObjects[surfaceId];
                    SurfaceData sd = new SurfaceData(
                        //the surface id returned from the system
                        surfaceId,
                        //the mesh filter that is populated with the spatial mapping data for this mesh
                        target.GetComponent<MeshFilter>() ?? target.AddComponent<MeshFilter>(),
                        //the world anchor used to position the spatial mapping mesh in the world
                        target.GetComponent<WorldAnchor>() ?? target.AddComponent<WorldAnchor>(),
                        //the mesh collider that is populated with collider data for this mesh, if true is passed to bakeMeshes below
                        target.GetComponent<MeshCollider>() ?? target.AddComponent<MeshCollider>(),
                        //triangles per cubic meter requested for this mesh
                        300,
                        //bakeMeshes - if true, the mesh collider is populated, if false, the mesh collider is empty.
                        false
                        );
                    //target.GetComponent<MeshRenderer>().enabled = false;
                    surfaceObserver.RequestMeshAsync(sd, OnSurfaceReady);
                    break;
                case SurfaceChange.Removed:
                    //Debug.Log("Removed " + spatialMeshObjects.Count);
                    var obj = spatialMeshObjects[surfaceId];
                    spatialMeshObjects.Remove(surfaceId);
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                    Marker del = new Marker()
                    {
                        points = points.ToArray(),
                        action = Marker.DELETE,
                        id = surfaceId.handle,
                        ns = "",
                    };
                    markerPublisher.Publish(del);

                    break;
                default:
                    break;
            }
        }

        void OnSurfaceReady(SurfaceData bakedData, bool _, float __)
        {
            Mesh mesh = bakedData.outputMesh.sharedMesh;
            GameObject target = bakedData.outputMesh.gameObject;
            int handle = bakedData.id.handle;

            if (mesh == null)
            {
                Debug.Log("No mesh!");
                return;
            }
            int[] indices = mesh.GetIndices(0);
            Vector3[] vertices = mesh.vertices;
            if (vertices == null)
            {
                Debug.Log("No vertices!");
                return;
            }

            points.Clear();
            for (int i = 0; i < indices.Length; i++)
            {
                points.Add(ToRos(vertices[indices[i]]));
            }

            Quaternion targetRotation = startQuaternion * target.transform.rotation;
            Vector3 targetPosition = startQuaternion * target.transform.position + startPosition;

            Marker marker = new Marker()
            {
                points = points.ToArray(),
                type = Marker.TRIANGLE_LIST,
                scale = new Msgs.geometry_msgs.Vector3 { x = 1, y = 1, z = 1 },
                pose = new Msgs.geometry_msgs.Pose
                {
                    orientation = ToRos(targetRotation),
                    position = ToRos(targetPosition)
                },
                action = Marker.ADD,
                id = handle,
                ns = "",
                header = new Msgs.std_msgs.Header
                {
                    seq = markerSeq++,
                    frame_id = "/hololens/start_point"
                },
                color = new Msgs.std_msgs.ColorRGBA { r = 1, g = 1, b = 1, a = 1 }
            };
            markerPublisher.Publish(marker);
        }

        uint tfSeq = 0;
        void Update()
        {
            transform.localRotation = cameraSrc.transform.rotation;
            transform.localPosition = cameraSrc.transform.position;

            Msgs.geometry_msgs.TransformStamped[] tfs =
            {
            new Msgs.geometry_msgs.TransformStamped
            {
                header = new Msgs.std_msgs.Header
                {
                    seq = tfSeq++,
                    frame_id = "/hololens/start_point"
                },
                child_frame_id = "/hololens/camera",
                transform = ToRosTransform(cameraDst.transform)
            },
            new Msgs.geometry_msgs.TransformStamped
            {
                header = new Msgs.std_msgs.Header
                {
                    seq = tfSeq++,
                    frame_id = "/hololens/start_point"
                },
                child_frame_id = "/hololens/billboard",
                transform = ToRosTransform(billboard.transform)
            },
            new Msgs.geometry_msgs.TransformStamped
            {
                header = new Msgs.std_msgs.Header
                {
                    seq = tfSeq++,
                    frame_id = "/hololens/start_point"
                },
                child_frame_id = "/hololens/billboard",
                transform = ToRosTransform(billboard.transform)
            },
            new Msgs.geometry_msgs.TransformStamped
            {
                header = new Msgs.std_msgs.Header
                {
                    seq = tfSeq++,
                    frame_id = "/map"
                },
                child_frame_id = "/hololens/start_point",
                transform = new Msgs.geometry_msgs.Transform
                {
                    rotation = new Msgs.geometry_msgs.Quaternion { x = 0, y = 0, z = 0, w = 1 },
                    translation = new Msgs.geometry_msgs.Vector3 { x = 0, y = 0, z = 0.4f }
                }
            }
        };

            TFMessage tfMessage = new TFMessage
            {
                transforms = tfs
            };
            tfPublisher.Publish(tfMessage);
        }

        uint imgSeq = 0;
        Image image;
        Color32[] srcColors;
        IEnumerator ImageLoop()
        {
            var wait = new WaitForSeconds(0.5f);
            while (true)
            {
                if (imagePublisher.NumSubscribers == 0)
                {
                    yield return wait;
                    continue;
                }

                AsyncGPUReadback.Request(texture, 0, request =>
                {
                    if (image == null)
                    {
                        image = new Image
                        {
                            header = new Msgs.std_msgs.Header
                            {
                                frame_id = "hololens/billboard",
                                seq = imgSeq++
                            },
                            width = (uint)(texture.width / 2),
                            height = (uint)(texture.height / 2),
                            encoding = "rgb8",
                            step = (uint)(texture.width * 3 / 2),
                            data = new byte[texture.width * texture.height * 3 / 4]
                        };
                    }
                    if (srcColors == null)
                    {
                        srcColors = new Color32[texture.width * texture.height];
                    }
                    request.GetData<Color32>().CopyTo(srcColors);
                    Task.Run(CaptureImage);
                });
                yield return wait;
            }
        }

        void CaptureImage()
        {
            image.header.seq = tfSeq++;

            int textureWidth = (int)(image.width * 2);
            int textureHeight = (int)(image.height * 2);

            for (int v = 0; v < image.height; v++)
            {
                int srcOff = (textureHeight - 1 - v * 2) * textureWidth;
                int dstOff = v * (int)image.step;
                for (int u = 0; u < image.width; u++)
                {
                    Color32 srcColor = srcColors[srcOff++];
                    image.data[dstOff++] = srcColor.g;
                    image.data[dstOff++] = srcColor.b;
                    image.data[dstOff++] = srcColor.a;
                    srcOff++;
                }
            }
            imagePublisher.Publish(image);
            imagePublisher.Cleanup();
        }
    }
}
