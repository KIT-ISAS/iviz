using Iviz.App;
using Iviz.Core;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Iviz.Controllers
{
    public class VRListener :  MonoBehaviour
    {
        [SerializeField] string topic = null;
        [SerializeField] string tfName = null;
        [SerializeField] GameObject controllerTemplate = null;
        
        GameObject controller;
        Transform controllerTransform;

        Pose startLocalPose;
        Pose startRemotePose;
        Pose? regTransform;
        
        Listener<Joy> listener;
        FrameNode node;

        bool currentHasMain;
        bool currentHasAlt;

        void Awake()
        {
            ModuleListPanel.CallWhenInitialized(Initialize);
        }

        void Initialize()
        {
            listener = new Listener<Joy>(topic, Handler);

            node = FrameNode.Instantiate("VrListener");
            node.AttachTo(tfName);
        }

        void Handler([NotNull] Joy joy)
        {
            bool newHasMain = (joy.Buttons.Length == 1 && joy.Buttons[0] == 0) || joy.Buttons.Length == 2;
            //Debug.Log("newhasmain " + newHasMain);
            if (currentHasMain != newHasMain)
            {
                if (newHasMain)
                {
                    OnMainDown();
                }
                else
                {
                    OnMainUp();
                }

                currentHasMain = newHasMain;
            }
            
            bool newHasAlt = (joy.Buttons.Length == 1 && joy.Buttons[0] == 1) || joy.Buttons.Length == 2;
            //Debug.Log("newhasalt" + newHasAlt);
            if (currentHasAlt != newHasAlt)
            {
                if (newHasAlt)
                {
                    OnAltDown();
                }
                else
                {
                    OnAltUp();
                }

                currentHasAlt = newHasAlt;
            }
        }

        void OnMainDown()
        {
            if (controller == null)
            {
                controller = Instantiate(controllerTemplate);
                controllerTransform = controller.transform;
            }

            regTransform = null;
            
            controller.SetActive(true);

            Vector3 forward = Settings.MainCameraTransform.forward.WithY(0).normalized;

            controllerTransform.parent = null;
            controllerTransform.position = Settings.MainCameraTransform.position + forward * 1 + Vector3.up * -0.3f;
            controllerTransform.rotation = Quaternion.Euler(0, Settings.MainCameraTransform.eulerAngles.y, 0);
        }

        void OnMainUp()
        {
            startRemotePose = node.Transform.AsPose();
            startLocalPose = controllerTransform.AsPose();
            Pose newRegTransform = startLocalPose.Multiply(startRemotePose.Inverse());
            //Pose newRegTransform = startRemotePose.Inverse().Multiply(startLocalPose);
            //newRegTransform.rotation = Quaternion.Euler(0, newRegTransform.rotation.eulerAngles.y, 0);

            regTransform = newRegTransform;
        }

        void OnAltDown()
        {
        }

        void OnAltUp()
        {
        }

        void Update()
        {
            if (regTransform == null)
            {
                return;
            }

            Pose remotePose = node.Transform.AsPose();
            //Pose localPose = regTransform.Value.Multiply(node.Transform.AsPose());
            //Pose localPose = remotePose.Multiply(regTransform.Value);
            //Pose localPose = regTransform.Value.Multiply(remotePose);
            
            //Pose localPose = startLocalPose.Multiply(startRemotePose.Inverse()).Multiply(remotePose);
            Pose localPose = regTransform.Value.Multiply(remotePose);
            controllerTransform.SetPose(localPose);
        }
    }
}