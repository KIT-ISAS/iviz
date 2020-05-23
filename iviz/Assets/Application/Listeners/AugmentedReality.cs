using UnityEngine;
using System.Collections;
using Iviz.RoslibSharp;
using System.Runtime.Serialization;
using System;
using Iviz.Resources;
using UnityEngine.XR.ARFoundation;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.AR;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public Vector3 Origin { get; set; } = Vector3.zero;
        [DataMember] public bool SearchMarker { get; set; } = false;
        [DataMember] public float WorldScale { get; set; } = 1.0f;
    }

    public class AugmentedReality : MonoBehaviour
    {
        public Canvas Canvas;
        public Camera MainCamera;

        GameObject TFRoot => TFListener.Instance.gameObject;

        public Camera ARCamera;
        public ARSessionOrigin ARSessionOrigin;

        readonly ARConfiguration config = new ARConfiguration();
        public ARConfiguration Config
        {
            get => config;
            set
            {

            }
        }

        public Vector3 Origin
        {
            get => config.Origin;
            set
            {
                config.Origin = value;
                ARSessionOrigin.gameObject.transform.position = value;
            }
        }

        public float WorldScale
        {
            get => config.WorldScale;
            set
            {
                config.WorldScale = value;
                TFRoot.transform.localScale = value * Vector3.one;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                MainCamera.gameObject.SetActive(!value);
                ARCamera.gameObject.SetActive(value);
            }
        }

        void Awake()
        {
            if (Canvas == null)
            {
                Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            if (MainCamera == null)
            {
                MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            }
        }

        public void Stop()
        {
            Visible = false;
        }
    }
}