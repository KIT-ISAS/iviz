#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Iviz.Core;
using Iviz.Tools;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class InternalResourceManager
    {
        readonly Dictionary<string, ResourceKey<GameObject>> gameObjects = new()
        {
            ["package://iviz_internal/cube"] = Resource.Displays.Cube,
            ["package://iviz_internal/cylinder"] = Resource.Displays.Cylinder,
            ["package://iviz_internal/sphere"] = Resource.Displays.Sphere,
        };

        readonly Dictionary<string, ResourceKey<Texture2D>> textures = new();

        readonly HashSet<string> blacklistedModelUris = new();
        readonly HashSet<string> blacklistedTextureUris = new();

        readonly Dictionary<string, string> robotDescriptions;

        public Dictionary<string, string>.KeyCollection GetRobotNames() => robotDescriptions.Keys;

        public InternalResourceManager()
        {
            string? robotsFile = UnityEngine.Resources.Load<TextAsset>("Package/iviz/resources")?.text;
            if (robotsFile.IsNullOrEmpty())
            {
                RosLogger.Warn($"{ToString()}: Empty resource file!");
                robotDescriptions = new Dictionary<string, string>();
                return;
            }

            var robots = JsonUtils.DeserializeObject<Dictionary<string, string>>(robotsFile);
            var newRobotDescriptions = new Dictionary<string, string>();
            foreach (var (key, value) in robots)
            {
                string? robotDescription =
                    UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/" + value)?.text;
                if (robotDescription.IsNullOrEmpty())
                {
                    RosLogger.Info($"{ToString()}: Empty or null description file '{value}'!");
                    continue;
                }

                newRobotDescriptions[key] = robotDescription;
            }

            robotDescriptions = newRobotDescriptions;
        }

        public bool ContainsRobot(string robotName)
        {
            ThrowHelper.ThrowIfNull(robotName, nameof(robotName));
            return robotDescriptions.ContainsKey(robotName);
        }

        public bool TryGetRobot(string robotName, [NotNullWhen(true)] out string? robotDescription)
        {
            ThrowHelper.ThrowIfNull(robotName, nameof(robotName));
            return robotDescriptions.TryGetValue(robotName, out robotDescription);
        }

        public bool TryGet(string uriString, [NotNullWhen(true)] out ResourceKey<GameObject>? info)
        {
            return TryGet(uriString, gameObjects, blacklistedModelUris, out info);
        }

        public bool TryGet(string uriString, [NotNullWhen(true)] out ResourceKey<Texture2D>? info)
        {
            return TryGet(uriString, textures, blacklistedTextureUris, out info);
        }

        bool TryGet<T>(string uriString, IDictionary<string, ResourceKey<T>> repository, ISet<string> blacklistedUris,
            [NotNullWhen(true)] out ResourceKey<T>? info)
            where T : UnityEngine.Object
        {
            ThrowHelper.ThrowIfNull(uriString, nameof(uriString));

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

            if (blacklistedUris.Contains(uriString))
            {
                info = null;
                return false;
            }

            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                RosLogger.Warn($"{ToString()}: Uri '{uriString}' is not a valid uri!");
                blacklistedUris.Add(uriString);
                info = null;
                return false;
            }

            string path = $"Package/{uri.Host}{Uri.UnescapeDataString(uri.AbsolutePath)}".Replace("//", "/");
            var resource = UnityEngine.Resources.Load<T>(path).CheckedNull()
                          ?? UnityEngine.Resources.Load<T>(
                              $"{Path.GetDirectoryName(path)}/{Path.GetFileNameWithoutExtension(path)}");

            if (resource == null)
            {
                blacklistedUris.Add(uriString);
                info = null;
                return false;
            }

            info = new ResourceKey<T>(resource);
            repository[uriString] = info;
            return true;
        }

        public override string ToString() => $"[{nameof(InternalResourceManager)}]";
    }
}