namespace Iviz.App
{
    public sealed class PublishedFramePanel : ModulePanel
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public FrameWidget Parent { get; private set; }
        public InputFieldWithHintsWidget ParentId { get; private set; }
        public Vector3MultiWidget RollPitchYaw { get; private set; }
        public Vector3MultiWidget Position { get; private set; }

        
        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Published Frame");
            Frame = p.AddFrame();
            Parent = p.AddFrame();
            ParentId = p.AddInputFieldWithHints("Parent Frame").SetPlaceholder("<empty>");
            HideButton = p.AddHideButton();
            CloseButton = p.AddCloseButton();
            RollPitchYaw = p.AddVector3Multi("Roll, Pitch, Yaw").SetRange(10);
            Position = p.AddVector3Multi("Translation");
            
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}