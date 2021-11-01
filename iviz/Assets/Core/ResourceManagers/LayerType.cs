namespace Iviz.Resources
{
    public static class LayerType
    {
        public const int Default = 0;

        /// <summary>
        /// Maps to the 'Ignore Raycast' layer, should be invisible to raycasts in any scene 
        /// </summary>
        public const int IgnoreRaycast = 2;

        public const int UI = 5;

        /// <summary>
        /// Maps to the 'Clickable' layer, should be visible to raycasts in any scene 
        /// </summary>
        public const int Clickable = 8;
        
        public const int TfAxis = 9;

        public const int ARSetupMode = 14;
        
        public const int Collider = 15;
    }
}