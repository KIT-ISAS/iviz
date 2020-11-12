using System;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    class HololensManager : IDisposable
    {
        TfFrame leftPalm;
        bool disposed;

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


            GameThread.EverySecond += Update;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            GameThread.EverySecond -= Update;
        }

        void Update()
        {
            TfListener.Publish(null, leftPalm.Id, Pose.identity);
        }
    }
}