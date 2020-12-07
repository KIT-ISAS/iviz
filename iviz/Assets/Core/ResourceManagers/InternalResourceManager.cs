using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Resources
{
    public class InternalResourceManager
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

        readonly ReadOnlyDictionary<string, string> robotDescriptions;

        public IEnumerable<string> GetRobotNames() => robotDescriptions.Keys;

        public InternalResourceManager()
        {
            string robotsFile = UnityEngine.Resources.Load<TextAsset>("Package/iviz/resources")?.text;
            if (string.IsNullOrEmpty(robotsFile))
            {
                Logger.Debug("InternalResourceManager: Empty resource file!");
                robotDescriptions = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
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
                    Logger.Debug("InternalResourceManager: Empty or null description file " + pair.Value + "!");
                    continue;
                }

                tmpRobotDescriptions[pair.Key] = robotDescription;
            }

            robotDescriptions = new ReadOnlyDictionary<string, string>(tmpRobotDescriptions);
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

        static bool TryGet<T>([NotNull] string uriString,
            [NotNull] Dictionary<string, Info<T>> repository,
            [NotNull] HashSet<string> negRepository,
            out Info<T> info)
            where T : UnityEngine.Object
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            if (repository.TryGetValue(uriString, out info))
            {
                return true;
            }

            if (negRepository.Contains(uriString))
            {
                return false;
            }

            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                Logger.Warn($"[InternalResourceManager]: Uri '{uriString}' is not a valid uri!");
                negRepository.Add(uriString);
                return false;
            }

            string path = $"Package/{uri.Host}{Uri.UnescapeDataString(uri.AbsolutePath)}".Replace("//", "/");
            T resource = UnityEngine.Resources.Load<T>(path);
            if (resource == null)
            {
                negRepository.Add(uriString);
                return false;
            }

            info = new Info<T>(path, resource);
            repository[uriString] = info;
            return true;
        }

        public override string ToString()
        {
            return "[InternalResourceManager]";
        }
    }
}