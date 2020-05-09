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
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.WebCam;

public class RosNode : MonoBehaviour
{
    SurfaceObserver surfaceObserver;

    const string markerTopic = "/hololens/environment";
    RosPublisher markerPublisher;

    const string tfTopic = "/tf";
    RosPublisher tfPublisher;

    const string imageTopic = "/hololens/image_color";
    RosPublisher imagePublisher;

    WebCamTexture texture;

    void Start()
    {
        Iviz.RoslibSharp.Logger.Log = Debug.Log;
        Iviz.RoslibSharp.Logger.LogError = Debug.LogError;
        Iviz.RoslibSharp.Logger.LogDebug = Debug.Log;
        RosClient client = new RosClient(
            "http://141.3.59.5:11311",
            "/IvizHololens",
            "http://141.3.59.140:9014"
            );

        client.Advertise<Marker>(markerTopic, out markerPublisher);
        client.Advertise<TFMessage>(tfTopic, out tfPublisher);
        client.Advertise<Image>(imageTopic, out imagePublisher);

        surfaceObserver = new SurfaceObserver();
        surfaceObserver.SetVolumeAsAxisAlignedBox(Vector3.zero, new Vector3(20, 20, 20));
        StartCoroutine(UpdateLoop());

        texture = new WebCamTexture(1280, 720, 15);
        texture.Play();
        StartCoroutine(ImageLoop());

        //PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
        //StartCoroutine(ImageLoop());
    }

    IEnumerator UpdateLoop()
    {
        var wait = new WaitForSeconds(2.5f);
        while (true)
        {
            surfaceObserver.Update(OnSurfaceChanged);
            yield return wait;
        }
    }

    readonly Dictionary<SurfaceId, GameObject> spatialMeshObjects = new Dictionary<SurfaceId, GameObject>();

    static Iviz.Msgs.geometry_msgs.Point ToRos(Vector3 v)
    {
        return new Iviz.Msgs.geometry_msgs.Point
        {
            x = v.z,
            y = -v.x,
            z = v.y
        };
    }

    static Iviz.Msgs.geometry_msgs.Vector3 ToRosVector(Vector3 v)
    {
        return new Iviz.Msgs.geometry_msgs.Vector3
        {
            x = v.z,
            y = -v.x,
            z = v.y
        };
    }

    static Iviz.Msgs.geometry_msgs.Quaternion ToRos(Quaternion q)
    {
        return new Iviz.Msgs.geometry_msgs.Quaternion
        {
            x = -q.z,
            y = q.x,
            z = -q.y,
            w = q.w
        };
    }

    static Iviz.Msgs.geometry_msgs.Pose ToRos(Transform t)
    {
        return new Iviz.Msgs.geometry_msgs.Pose
        {
            orientation = ToRos(t.rotation),
            position = ToRos(t.position)
        };
    }

    static Iviz.Msgs.geometry_msgs.Transform ToRosTransform(Transform t)
    {
        return new Iviz.Msgs.geometry_msgs.Transform
        {
            rotation = ToRos(t.rotation),
            translation = ToRosVector(t.position)
        };
    }

    static readonly Iviz.Msgs.geometry_msgs.Vector3 One = new Iviz.Msgs.geometry_msgs.Vector3 { x = 1, y = 1, z = 1 };
    readonly List<Iviz.Msgs.geometry_msgs.Point> points = new List<Iviz.Msgs.geometry_msgs.Point>();

    uint markerSeq = 0;
    void OnSurfaceChanged(SurfaceId surfaceId, SurfaceChange changeType, Bounds bounds, System.DateTime updateTime)
    {
        switch (changeType)
        {
            case SurfaceChange.Added:
            case SurfaceChange.Updated:
                //Debug.Log("Updated " + spatialMeshObjects.Count);
                if (!spatialMeshObjects.ContainsKey(surfaceId))
                {
                    spatialMeshObjects[surfaceId] = new GameObject("spatial-mapping-" + surfaceId);
                    spatialMeshObjects[surfaceId].transform.parent = this.transform;
                    spatialMeshObjects[surfaceId].AddComponent<MeshRenderer>();
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
                    1000,
                    //bakeMeshes - if true, the mesh collider is populated, if false, the mesh collider is empty.
                    false
                    );
                //target.GetComponent<MeshRenderer>().enabled = false;
                surfaceObserver.RequestMeshAsync(sd, OnDataReady);

                Mesh mesh = target.GetComponent<MeshFilter>().sharedMesh;
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

                Marker marker = new Marker()
                {
                    points = points.ToArray(),
                    type = Marker.TRIANGLE_LIST,
                    scale = One,
                    pose = ToRos(target.transform),
                    action = Marker.ADD,
                    id = surfaceId.handle,
                    ns = "",
                    header = new Iviz.Msgs.std_msgs.Header
                    {
                        seq = markerSeq++,
                        frame_id = "base_link"
                    },
                    color = new Iviz.Msgs.std_msgs.ColorRGBA { r = 255, g = 255, b = 255, a = 255 }
                };
                markerPublisher.Publish(marker);

                break;
            case SurfaceChange.Removed:
                //Debug.Log("Removed " + spatialMeshObjects.Count);
                var obj = spatialMeshObjects[surfaceId];
                spatialMeshObjects.Remove(surfaceId);
                if (obj != null)
                {
                    GameObject.Destroy(obj);
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

    void OnDataReady(SurfaceData bakedData, bool outputWritten, float elapsedBakeTimeSeconds)
    {

    }


    uint tfSeq = 0;
    void Update()
    {
        Iviz.Msgs.geometry_msgs.TransformStamped tf = new Iviz.Msgs.geometry_msgs.TransformStamped
        {
            header = new Iviz.Msgs.std_msgs.Header
            {
                seq = tfSeq++,
                frame_id = "base_link"
            },
            child_frame_id = "hololens",
            transform = ToRosTransform(transform)
        };

        TFMessage tfMessage = new TFMessage
        {
            transforms = new[] { tf }
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

            srcColors = texture.GetPixels32(srcColors);

            if (image == null)
            {
                image = new Image
                {
                    header = new Iviz.Msgs.std_msgs.Header
                    {
                        frame_id = "hololens",
                        seq = imgSeq++
                    },
                    width = (uint)(texture.width / 2),
                    height = (uint)(texture.height / 2),
                    encoding = "rgb8",
                    step = (uint)(texture.width * 3 / 2),
                    data = new byte[texture.width * texture.height * 3 / 4]
                };
            }

            Task.Run(() =>
            {
                try
                {
                    CaptureImage();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
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
                image.data[dstOff++] = srcColor.r;
                image.data[dstOff++] = srcColor.g;
                image.data[dstOff++] = srcColor.b;
                srcOff++;
            }
        }
        imagePublisher.Publish(image);
        imagePublisher.Cleanup();
    }

    /*
    bool sendImage = false;
    IEnumerator ImageLoop()
    {
        var wait = new WaitForSeconds(0.5f);
        while (true)
        {
            sendImage = true;
            Debug.Log("Setting flag true");
            yield return wait;
        }
    }

    PhotoCapture photoCaptureObject;
    readonly Resolution cameraResolution = new Resolution
    {
        width = 1280,
        height = 720,
        refreshRate = 15
    };

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;


        CameraParameters c = new CameraParameters
        {
            hologramOpacity = 0.0f,
            //frameRate = cameraResolution.refreshRate,
            cameraResolutionWidth = cameraResolution.width,
            cameraResolutionHeight = cameraResolution.height,
            pixelFormat = CapturePixelFormat.BGRA32
        };

        Debug.Log("Starting photo mode");
        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Starting take photo");
            photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }


    readonly List<byte> srcData = new List<byte>();
    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            Debug.Log("Entering OnCaptured");
            if (!sendImage)
            {
                return;
            }
            Debug.Log("Copying raw data");
            photoCaptureFrame.CopyRawImageDataIntoBuffer(srcData);
            Debug.Log("CaptureImage");
            CaptureImage();
        }
        photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
    }

    Image image;
    uint imgSeq = 0;
    void CaptureImage()
    {
        sendImage = false;
        Debug.Log("Setting flag false");

        if (image == null)
        {
            Debug.Log("Creating image message");
            image = new Image
            {
                header = new Iviz.Msgs.std_msgs.Header
                {
                    frame_id = "hololens",
                    seq = 0
                },
                width = (uint)(cameraResolution.width / 2),
                height = (uint)(cameraResolution.height / 2),
                encoding = "rgb8",
                step = (uint)(cameraResolution.width * 3 / 2),
                data = new byte[cameraResolution.width * cameraResolution.height * 3 / 4]
            };
        }

        image.header.seq = imgSeq++;

        for (int v = 0; v < image.height; v++)
        {
            int srcOff = (cameraResolution.height - 1 - v * 2) * cameraResolution.width * 4;
            int dstOff = v * (int)image.step;
            for (int u = 0; u < image.width; u++)
            {
                image.data[dstOff++] = srcData[srcOff++];
                image.data[dstOff++] = srcData[srcOff++];
                image.data[dstOff++] = srcData[srcOff++];
                srcOff += 5;
            }
        }
        Debug.Log("Publishing message");
        imagePublisher.Publish(image);
    }
    */
}
