﻿#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Controllers.XR;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    public sealed class XRModuleData : ModuleData
    {
        readonly XRModulePanel panel;
        readonly XRController controller;

        public override ModuleType ModuleType => ModuleType.XR;
        public override ModulePanel Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public XRModuleData(ModuleDataConstructor constructor)
        {
            panel = ModulePanelManager.GetPanelByResourceType<XRModulePanel>(ModuleType.XR);

            controller = new XRController(ModuleListPanel.Instance.XRController, (XRConfiguration?)constructor.Configuration);

            UpdateModuleButton();
        }

        public override void Dispose()
        {
            base.Dispose();
            controller.Dispose();
        }


        public override void SetupPanel()
        {
            panel.HideButton.State = controller.Visible;
            panel.GazeSender.Set(controller.GazeSender);
            panel.LeftSender.Set(controller.LeftHandSender);
            panel.RightSender.Set(controller.RightHandSender);

            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.XR = controller.Config;
        }
    }
}