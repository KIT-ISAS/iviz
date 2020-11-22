using System;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.App;
using Iviz.Core;
using Iviz.Hololens;
using Iviz.Ros;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
#if UNITY_WSA
    class HololensManager : MonoBehaviour
    {
        static readonly Pose InfinitePose = new Pose(new Vector3(5000, 5000, 5000), Quaternion.identity);

        [SerializeField] FloorFrameObject floorFrame = null;
        [SerializeField] GameObject floorHelper = null;

        FrameNode node;
        TfFrame leftPalm;

        float leftHandScale = 1;
        Pose leftPalmPose;

        TfFrame RootFrame => TfListener.RootFrame;
        static ReadOnlyCollection<ModuleData> ModuleDatas => ModuleListPanel.Instance.ModuleDatas;
        static string MyId => ConnectionManager.Connection.MyId;

        void Start()
        {
            if (ModuleListPanel.Initialized)
            {
                Initialize();
            }
            else
            {
                ModuleListPanel.InitFinished += Initialize;
            }
        }

        void Initialize()
        {
            Debug.Log("Hololens Manager: Initializing!");
            ModuleListPanel.InitFinished -= Initialize;

            ModuleData grid = ModuleDatas.FirstOrDefault(data => data.ModuleType == Resource.ModuleType.Grid);
            if (grid != null)
            {
                ModuleListPanel.Instance.RemoveModule(grid);
            }

            if (!UnityEngine.Application.isEditor)
            {
                floorHelper.gameObject.SetActive(false);
            }

            floorFrame.OkClicked += StartWorld;


            StartRosConnection();
            StartPalms();
            StartOrigin();
        }

        void StartRosConnection()
        {
#if UNITY_EDITOR
            ConnectionManager.Connection.MyUri = new Uri("http://141.3.59.11:7613");
#else
            ConnectionManager.Connection.MyUri = new Uri("http://141.3.59.45:7613");
#endif
            ConnectionManager.Connection.MasterUri = new Uri("http://141.3.59.5:11311");
            ConnectionManager.Connection.MyId = "/iviz_hololens";
            ConnectionManager.Connection.KeepReconnecting = true;
        }

        void StartPalms()
        {
            leftPalm = TfListener.GetOrCreateFrame($"{MyId}/controller/left_palm");
            leftPalm.transform.localScale = 0.05f * Vector3.one;

            leftPalm = TfListener.GetOrCreateFrame($"{MyId}/controller/left_hand");
            node = FrameNode.Instantiate("Hololens");
            node.Parent = leftPalm;

            leftPalmPose = new Pose(Vector3.up * 1f, Quaternion.identity);
            leftPalm.SetPose(leftPalmPose);
            leftPalm.ParentCanChange = false;

            LeftHandScale = 0.1f;
        }

        void StartOrigin()
        {
            RootFrame.transform.SetPose(InfinitePose);
            floorFrame.gameObject.SetActive(true);
        }

        void StartWorld()
        {
            floorFrame.gameObject.SetActive(false);
            RootFrame.transform.SetPose(floorFrame.OriginPose);
        }

        public float LeftHandScale
        {
            get => leftHandScale;
            set
            {
                leftHandScale = value;
                leftPalm.transform.localScale = value * Vector3.one;
            }
        }

        void OnDestroy()
        {
            if (node != null)
            {
                node.Parent = null;
                Destroy(node.gameObject);
            }
        }
    }
#else
    class HololensManager : MonoBehaviour
    {
    }
#endif
}