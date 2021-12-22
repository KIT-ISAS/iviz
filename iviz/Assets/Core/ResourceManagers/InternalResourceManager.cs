#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class InternalResourceManager
    {
        readonly Dictionary<string, Info<GameObject>> gameObjects = new()
        {
            ["package://iviz_internal/cube"] = Resource.Displays.Cube,
            ["package://iviz_internal/cylinder"] = Resource.Displays.Cylinder,
            ["package://iviz_internal/sphere"] = Resource.Displays.Sphere,
        };

        readonly Dictionary<string, Info<Texture2D>> textures = new();

        readonly HashSet<string> negGameObjects = new();
        readonly HashSet<string> negTextures = new();

        readonly ReadOnlyDictionary<string, string> robotDescriptions;

        public IEnumerable<string> GetRobotNames() =>
            robotDescriptions.Keys ?? (IEnumerable<string>)Array.Empty<string>();

        public InternalResourceManager()
        {
            string? robotsFile = UnityEngine.Resources.Load<TextAsset>("Package/iviz/resources")?.text;
            if (robotsFile is null or "")
            {
                RosLogger.Warn($"{this}: Empty resource file!");
                robotDescriptions = new Dictionary<string, string>().AsReadOnly();
                return;
            }

            var robots = JsonConvert.DeserializeObject<Dictionary<string, string>>(robotsFile);
            var tmpRobotDescriptions = new Dictionary<string, string>();
            foreach (var (key, value) in robots)
            {
                string? robotDescription =
                    UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/" + value)?.text;
                if (robotDescription is null or "")
                {
                    RosLogger.Info($"{this}: Empty or null description file {value}!");
                    continue;
                }

                tmpRobotDescriptions[key] = robotDescription;
            }

            robotDescriptions = tmpRobotDescriptions.AsReadOnly();
        }

        public bool ContainsRobot(string robotName)
        {
            if (robotName == null)
            {
                throw new ArgumentNullException(nameof(robotName));
            }

            return robotDescriptions.ContainsKey(robotName);
        }

        public bool TryGetRobot(string robotName, out string robotDescription)
        {
            if (robotName == null)
            {
                throw new ArgumentNullException(nameof(robotName));
            }

            return robotDescriptions.TryGetValue(robotName, out robotDescription);
        }

        public bool TryGet(string uriString, [NotNullWhen(true)] out Info<GameObject>? info)
        {
            return TryGet(uriString, gameObjects, negGameObjects, out info);
        }

        public bool TryGet(string uriString, [NotNullWhen(true)] out Info<Texture2D>? info)
        {
            return TryGet(uriString, textures, negTextures, out info);
        }

        bool TryGet<T>(string uriString,
            Dictionary<string, Info<T>> repository,
            HashSet<string> negRepository,
            [NotNullWhen(true)] out Info<T>? info)
            where T : UnityEngine.Object
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            if (repository.TryGetValue(uriString, out var infoCandidate))
            {
                if (infoCandidate != null)
                {
                    info = infoCandidate;
                    return true;
                }

                repository.Remove(uriString);
                info = null;
                return false;
            }

            if (negRepository.Contains(uriString))
            {
                info = null;
                return false;
            }

            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                RosLogger.Warn($"{this}: Uri '{uriString}' is not a valid uri!");
                negRepository.Add(uriString);
                info = null;
                return false;
            }

            string path = $"Package/{uri.Host}{Uri.UnescapeDataString(uri.AbsolutePath)}".Replace("//", "/");
            T? resource = UnityEngine.Resources.Load<T>(path).CheckedNull()
                          ?? UnityEngine.Resources.Load<T>(
                              $"{Path.GetDirectoryName(path)}/{Path.GetFileNameWithoutExtension(path)}");

            if (resource == null)
            {
                negRepository.Add(uriString);
                info = null;
                return false;
            }

            info = new Info<T>(resource);
            repository[uriString] = info;
            return true;
        }

        public override string ToString() => "[InternalResourceManager]";
    }
}