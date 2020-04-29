using Iviz.Msgs.visualization_msgs;
using Iviz.RoslibSharp;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.XR.WSA;

public class RosNode : MonoBehaviour
{
    SurfaceObserver surfaceObserver;
    const string topic = "/hololens/environment";
    RosPublisher publisher;

    void Start()
    {
        Iviz.RoslibSharp.Logger.Log = Debug.Log;
        Iviz.RoslibSharp.Logger.LogError = Debug.LogError;
        Iviz.RoslibSharp.Logger.LogDebug = Debug.Log;
        RosClient client = new RosClient(
            "http://141.3.59.5:11311",
            "/IvizHololens",
            "http://141.3.59.140:7613"
            );

        Debug.Log(client.CallerUri);
        Debug.Log(client.MasterUri);

        client.Advertise<Marker>(topic, out publisher);

        surfaceObserver = new SurfaceObserver();
        surfaceObserver.SetVolumeAsAxisAlignedBox(Vector3.zero, new Vector3(10, 3, 10));
        StartCoroutine(UpdateLoop());
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

    static readonly Iviz.Msgs.geometry_msgs.Vector3 One = new Iviz.Msgs.geometry_msgs.Vector3 { x = 1, y = 1, z = 1 };
    readonly List<Iviz.Msgs.geometry_msgs.Point> points = new List<Iviz.Msgs.geometry_msgs.Point>();

    uint seq = 0;


    void OnSurfaceChanged(SurfaceId surfaceId, SurfaceChange changeType, Bounds bounds, System.DateTime updateTime)
    {
        switch (changeType)
        {
            case SurfaceChange.Added:
            case SurfaceChange.Updated:
                Debug.Log("Updated " + spatialMeshObjects.Count);
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
                for (int i = 0; i < indices.Length; i += 3)
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
                        seq = seq++,
                        frame_id = "/base_link"
                    },
                };
                publisher.Publish(marker);

                break;
            case SurfaceChange.Removed:
                Debug.Log("Removed " + spatialMeshObjects.Count);
                var obj = spatialMeshObjects[surfaceId];
                spatialMeshObjects.Remove(surfaceId);
                if (obj != null)
                {
                    GameObject.Destroy(obj);
                }
                break;
            default:
                break;
        }
    }

    void OnDataReady(SurfaceData bakedData, bool outputWritten, float elapsedBakeTimeSeconds)
    {

    }


    // Update is called once per frame
    void Update()
    {
    }
}
