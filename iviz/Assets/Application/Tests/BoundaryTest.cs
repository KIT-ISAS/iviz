#nullable enable

using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App.Tests
{
    public class BoundaryTest : MonoBehaviour
    {
        async void Start()
        {
            var obstacles = new ObstacleScene
            {
                Obstacles = transform.Cast<Transform>().Select(FromTransform).ToArray()
            };

            string str = JsonConvert.SerializeObject(obstacles, Formatting.Indented);
            await FileUtils.WriteAllTextAsync($"/Users/akzeac/{name}.json", str, default);
        }

        static Obstacle FromTransform(Transform t, int id)
        {
            return new Obstacle
            {
                Id = id,
                Type = t.name switch
                {
                    "Source" => ObstacleType.Source,
                    "Destination" => ObstacleType.Destination,
                    _ => ObstacleType.Obstacle
                },
                Center = t.localPosition.Unity2RosPoint(),
                Size = t.localScale.Unity2RosVector3().Abs()
            };
        }
    }

    public enum ObstacleType
    {
        Source,
        Obstacle,
        Destination
    }

    [DataContract]
    public sealed class Obstacle
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public ObstacleType Type { get; set; }
        [DataMember] public Msgs.GeometryMsgs.Point Center { get; set; }
        [DataMember] public Msgs.GeometryMsgs.Vector3 Size { get; set; }
    }

    [DataContract]
    public sealed class ObstacleScene
    {
        [DataMember] public Obstacle[] Obstacles { get; set; } = Array.Empty<Obstacle>();
    }
}