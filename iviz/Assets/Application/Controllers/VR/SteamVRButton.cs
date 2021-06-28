//#define USING_VR

using System;
using UnityEngine;

namespace Iviz.Controllers
{
#if USING_VR
    using Valve.VR;

    public class SteamVRButton : MonoBehaviour, IVRButton
    {
        [SerializeField] SteamVR_Behaviour_Pose pose;
        [SerializeField] SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
        [SerializeField] SteamVR_Action_Boolean teleport = SteamVR_Input.GetBooleanAction("Teleport");

        public bool PointerDown() => interactWithUI.GetStateDown(pose.inputSource);
        public bool PointerUp() => interactWithUI.GetStateUp(pose.inputSource);
        public bool State() => interactWithUI.GetState(pose.inputSource);
        public bool AltState() => teleport.GetState(pose.inputSource);
    }
#endif
}