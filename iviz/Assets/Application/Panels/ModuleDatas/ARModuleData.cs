using System.Collections.Generic;
using System.Linq;
using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARPanelContents"/> 
    /// </summary>
    public sealed class ARModuleData : ModuleData
    {
        const string NoneString = "(none)";
        
        readonly ARController controller;
        readonly ARPanelContents panel;

        public override Resource.Module Module => Resource.Module.AR;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        bool isDraggingControl;
        
        public ARModuleData(ModuleDataConstructor constructor) :
            base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.AR) as ARPanelContents;

            controller = Resource.Controllers.AR.Instantiate().GetComponent<ARController>();
            controller.ModuleData = this;
            if (constructor.Configuration != null)
            {
                controller.Config = (ARConfiguration)constructor.Configuration;
            }

            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();

            controller.Stop();
            Object.Destroy(controller.gameObject);
        }

        public override void SetupPanel()
        {
            panel.HideButton.State = controller.Visible;
            panel.Frame.Owner = controller;
            panel.WorldScale.Value = controller.WorldScale;
            panel.WorldOffset.Mean = controller.WorldOffset;
            panel.WorldAngle.Value = controller.WorldAngle;

            panel.SearchMarker.Value = controller.UseMarker;
            //panel.MarkerSize.Value = controller.MarkerSize;
            panel.MarkerHorizontal.Value = controller.MarkerHorizontal;
            panel.MarkerAngle.Value = controller.MarkerAngle;
            panel.MarkerFrame.Value = controller.MarkerFrame;

            List<string> frameHints = new List<string> {NoneString};
            frameHints.AddRange(TFListener.FramesForHints);
            panel.MarkerFrame.Hints = frameHints;
            
            panel.MarkerOffset.Value = controller.MarkerOffset;

            TFListener.RootControl.PointerUp += RootControlOnPointerUp;
            
            /*
            panel.HeadSender.Set(controller.RosSenderHead);
            panel.MarkersSender.Set(controller.RosSenderMarkers);

            panel.PublishHead.Value = controller.PublishPose;
            panel.PublishPlanes.Value = controller.PublishPlanesAsMarkers;
            */

            CheckInteractable();

            panel.WorldScale.ValueChanged += f =>
            {
                controller.WorldScale = f;
            };
            panel.WorldOffset.ValueChanged += f =>
            {
                controller.WorldOffset = f;
            };
            panel.WorldAngle.ValueChanged += f =>
            {
                controller.WorldAngle = f;
            };
            panel.SearchMarker.ValueChanged += f =>
            {
                controller.UseMarker = f;
                CheckInteractable();
            };
            /*
            panel.MarkerSize.ValueChanged += f =>
            {
                controller.MarkerSize = f;
            };
            */
            panel.MarkerHorizontal.ValueChanged += f =>
            {
                controller.MarkerHorizontal = f;
            };
            panel.MarkerAngle.ValueChanged += f =>
            {
                controller.MarkerAngle = (int)f;
            };
            panel.MarkerFrame.EndEdit += f =>
            {
                if (f == NoneString)
                {
                    panel.MarkerFrame.Value = "";
                    controller.MarkerFrame = "";
                }
                else
                {
                    controller.MarkerFrame = f;
                }
                CheckInteractable();
            };
            panel.MarkerOffset.ValueChanged += f =>
            {
                controller.MarkerOffset = f;
            };
            /*
            panel.PublishHead.ValueChanged += f =>
            {
                controller.PublishPose = f;
                panel.HeadSender.Set(controller.RosSenderHead);
            };
            panel.PublishPlanes.ValueChanged += f =>
            {
                controller.PublishPlanesAsMarkers = f;
                panel.MarkersSender.Set(controller.RosSenderMarkers);
            };
            */

            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.HideButton.Clicked += () =>
            {
                controller.Visible = !controller.Visible;
                panel.HideButton.State = controller.Visible;
                UpdateModuleButton();
            };
        }

        void RootControlOnPointerUp()
        {
            var pose = TFListener.RelativePose(TFListener.RootControl.transform.AsPose());
            controller.WorldOffset = pose.position.Unity2Ros();

            float angle = pose.rotation.eulerAngles.y;
            if (angle > 180)
            {
                angle -= 360;
            }
            controller.WorldAngle = angle; 
            
            panel.WorldOffset.Mean = controller.WorldOffset;
            panel.WorldAngle.Value = controller.WorldAngle;
        }

        public override void CleanupPanel()
        {
            base.CleanupPanel();
            TFListener.RootControl.PointerUp -= RootControlOnPointerUp;
        }

        void CheckInteractable()
        {
            //panel.Origin.Interactable = !controller.SearchMarker;
            panel.MarkerHorizontal.Interactable = controller.UseMarker;
            panel.MarkerAngle.Interactable = controller.UseMarker;
            panel.MarkerFrame.Interactable = controller.UseMarker;
            panel.MarkerOffset.Interactable = controller.UseMarker && controller.MarkerFrame.Length != 0;
            //panel.MarkerSize.Interactable = controller.SearchMarker;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.AR = controller.Config;
        }
    }
}
