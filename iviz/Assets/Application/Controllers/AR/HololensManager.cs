using System;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Msgs;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    class HololensManager : IDisposable
    {
        readonly DisplayNode node;
        readonly TfFrame leftPalm;
        bool disposed;

        float leftHandScale = 1;

        Pose leftPalmPose;
        
        public HololensManager()
        {
#if UNITY_EDITOR
            ConnectionManager.Connection.MyUri = new Uri("http://141.3.59.11:7613");
#else
            ConnectionManager.Connection.MyUri = new Uri("http://141.3.59.45:7613");
#endif
            ConnectionManager.Connection.MasterUri = new Uri("http://141.3.59.5:11311");
            ConnectionManager.Connection.MyId = "/iviz_hololens";

            ConnectionManager.Connection.KeepReconnecting = true;

            TfListener.RootFrame.SetPose(TimeSpan.Zero, new Pose(new Vector3(0, -1.5f, 2.0f), Quaternion.identity));


            leftPalm = TfListener.GetOrCreateFrame("iviz/hololens/left_palm");
            leftPalm.transform.localScale = 0.05f * Vector3.one;


            leftPalm = TfListener.GetOrCreateFrame("iviz/controller/left_hand");
            node = SimpleDisplayNode.Instantiate("Hololens");
            node.Parent = leftPalm;
            GameThread.EverySecond += Update;

            leftPalmPose = new Pose(Vector3.up * 1f, Quaternion.identity);
            leftPalm.SetPose(leftPalmPose);
            leftPalm.ParentCanChange = false;

            LeftHandScale = 0.1f;
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

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            
            node.Parent = null;
            UnityEngine.Object.Destroy(node.gameObject);
            
            GameThread.EverySecond -= Update;
        }

        void Update()
        {
            //TfListener.Publish(null, leftPalm.Id, leftPalmPose);
        }
    }
}