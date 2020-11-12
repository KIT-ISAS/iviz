using System;
using Iviz.Core;
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
