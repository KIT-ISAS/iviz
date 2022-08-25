#nullable enable

using System.Collections.Generic;

namespace Iviz.App
{
    internal sealed class DialogManager
    {
        public IReadOnlyList<DialogData> DialogDatas { get; }
        public AddModuleDialogData AvailableModules { get; }
        public AddTopicDialogData AvailableTopics { get; }
        public ConnectionDialogData ConnectionData { get; }
        public LoadConfigDialogData LoadConfigData { get; }
        public SaveConfigDialogData SaveConfigData { get; }
        public TfDialogData TfTreeData { get; }
        public MarkerDialogData MarkerData { get; }
        public RobotDialogData RobotData { get; }
        public NetworkDialogData NetworkData { get; }
        public ConsoleDialogData ConsoleData { get; }
        public SettingsDialogData SettingsData { get; }
        public EchoDialogData EchoData { get; }
        public SystemDialogData SystemData { get; }
        public ARMarkerDialogData ARMarkerData { get; }

        public DialogManager()
        {
            DialogDatas = new DialogData[]
            {
                AvailableModules = new AddModuleDialogData(),
                AvailableTopics = new AddTopicDialogData(),
                TfTreeData = new TfDialogData(),
                LoadConfigData = new LoadConfigDialogData(),
                SaveConfigData = new SaveConfigDialogData(),
                MarkerData = new MarkerDialogData(),
                RobotData = new RobotDialogData(),
                NetworkData = new NetworkDialogData(),
                ConnectionData = new ConnectionDialogData(),
                ConsoleData = new ConsoleDialogData(),
                SettingsData = new SettingsDialogData(),
                EchoData = new EchoDialogData(),
                SystemData = new SystemDialogData(),
                ARMarkerData = new ARMarkerDialogData(),
            };                
        }
    }
}