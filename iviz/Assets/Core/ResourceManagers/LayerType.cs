namespace Iviz.Resources
{
    public static class LayerType
    {
        public const int Default = 0;

        /// <summary>
        /// Maps to the 'Ignore Raycast' layer, should be invisible to raycasts in any scene 
        /// </summary>
        public const int IgnoreRaycast = 2;

        /// <summary>
        /// Maps to the 'UI' layer 
        /// </summary>
        public const int UI = 5;

        /// <summary>
        /// Interactable objects 
        /// </summary>
        public const int Clickable = 8;
        
        /// <summary>
        /// TF frames 
        /// </summary>
        public const int TfAxis = 9;

        public const int ARSetupMode = 14;
        
        /// <summary>
        /// Miscellaneous non-interactable colliders that can respond to events such as highlights 
        /// </summary>
        public const int Collider = 15;

        /// <summary>
        /// Colliders that interact with other colliders and can respond to events such as enter, exit 
        /// </summary>
        public const int InteractingCollider = 16;

        public const int RaycastLayerMask = (1 << Collider)
                                            | (1 << UI)
                                            | (1 << Clickable)
                                            | (1 << TfAxis);
    }
}