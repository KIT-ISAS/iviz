using System;
using Iviz.Core;
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
