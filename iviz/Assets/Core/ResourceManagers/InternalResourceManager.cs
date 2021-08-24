using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Iviz.Core;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Resources
{
    public sealed class InternalResourceManager
    {
        readonly Dictionary<string, Info<GameObject>> gameObjects = new Dictionary<string, Info<GameObject>>
        {
            ["package://iviz_internal/cube"] = Resource.Displays.Cube,
            ["package://iviz_internal/cylinder"] = Resource.Displays.Cylinder,
            ["package://iviz_internal/sphere"] = Resource.Displays.Sphere,
        };

        readonly Dictionary<string, Info<Texture2D>> textures = new Dictionary<string, Info<Texture2D>>();

        readonly HashSet<string> negGameObjects = new HashSet<string>();
        readonly HashSet<string> negTextures = new HashSet<string>();

        [NotNull] readonly ReadOnlyDictionary<string, string> robotDescriptions;

        [NotNull, ItemNotNull]
        public IEnumerable<string> GetRobotNames() =>
            robotDescriptions.Keys ?? (IEnumerable<string>) Array.Empty<string>();

        public InternalResourceManager()
        {
            string robotsFile = UnityEngine.Resources.Load<TextAsset>("Package/iviz/resources")?.text;
            if (string.IsNullOrEmpty(robotsFile))
            {
                Logger.Warn($"{this}: Empty resource file!");
                robotDescriptions = new Dictionary<string, string>().AsReadOnly();
                return;
            }

            Dictionary<string, string> robots = JsonConvert.DeserializeObject<Dictionary<string, string>>(robotsFile);
            Dictionary<string, string> tmpRobotDescriptions = new Dictionary<string, string>();
            foreach (var pair in robots)
            {
                string robotDescription =
                    UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/" + pair.Value)?.text;
                if (string.IsNullOrEmpty(robotDescription))
                {
                    Logger.Info($"{this}: Empty or null description file {pair.Value}!");
                    continue;
                }

                tmpRobotDescriptions[pair.Key] = robotDescription;
            }

            robotDescriptions = tmpRobotDescriptions.AsReadOnly();
        }

        public bool ContainsRobot([NotNull] string robotName)
        {
            if (robotName == null)
            {
                throw new ArgumentNullException(nameof(robotName));
            }

            return robotDescriptions.ContainsKey(robotName);
        }

        [ContractAnnotation("=> false, robotDescription:null; => true, robotDescription:notnull")]
        public bool TryGetRobot([NotNull] string robotName, out string robotDescription)
        {
            if (robotName == null)
            {
                throw new ArgumentNullException(nameof(robotName));
            }

            return robotDescriptions.TryGetValue(robotName, out robotDescription);
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        public bool TryGet([NotNull] string uriString, out Info<GameObject> info)
        {
            return TryGet(uriString, gameObjects, negGameObjects, out info);
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        public bool TryGet([NotNull] string uriString, out Info<Texture2D> info)
        {
            return TryGet(uriString, textures, negTextures, out info);
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        bool TryGet<T>([NotNull] string uriString,
            [NotNull] Dictionary<string, Info<T>> repository,
            [NotNull] HashSet<string> negRepository,
            [CanBeNull] out Info<T> info)
            where T : UnityEngine.Object
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            if (repository.TryGetValue(uriString, out info))
            {
                if (info != null)
                {
                    return true;
                }

                repository.Remove(uriString);
                info = null;
                return false;
            }

            if (negRepository.Contains(uriString))
            {
                return false;
            }

            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                Logger.Warn($"{this}: Uri '{uriString}' is not a valid uri!");
                negRepository.Add(uriString);
                return false;
            }

            string path = $"Package/{uri.Host}{Uri.UnescapeDataString(uri.AbsolutePath)}".Replace("//", "/");
            T resource = UnityEngine.Resources.Load<T>(path);

            if (resource == null)
            {
                string alternativePath = Path.GetDirectoryName(path) + "/" + Path.GetFileNameWithoutExtension(path);
                resource = UnityEngine.Resources.Load<T>(alternativePath);
            }

            if (resource == null)
            {
                negRepository.Add(uriString);
                return false;
            }

            info = new Info<T>(resource);
            repository[uriString] = info;
            return true;
        }

        [NotNull]
        public override string ToString()
        {
            return "[InternalResourceManager]";
        }
    }
}