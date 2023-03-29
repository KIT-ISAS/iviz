#nullable enable
using System;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public abstract class BaseBoundary : MonoBehaviour, IRecyclable, IDisplay
    {
        Tooltip? tooltip;
        Transform? mTransform;
        Vector3 scale;
        
        [SerializeField] BoxCollider? boxCollider;
        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));
        protected Transform Transform => this.EnsureHasTransform(ref mTransform);

        public string Id { get; set; } = "";
        public event Action<string>? EnteredCollision;
        public event Action<string>? ExitedCollision;
        
        public bool Interactable
        {
            set => BoxCollider.enabled = value;
        }

        public virtual Vector3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                BoxCollider.size = value;
            } 
        }

        public BehaviorType Behavior
        {
            set
            {
                (gameObject.layer, BoxCollider.isTrigger) = value switch
                {
                    BehaviorType.Collider => (LayerType.InteractingCollider, false),
                    BehaviorType.NotifyCollision => (LayerType.InteractingCollider, true),
                    _ => (LayerType.IgnoreRaycast, false),
                };
            }
        }
        
        public string Caption
        {
            set
            {
                if (value.Length == 0)
                {
                    tooltip.ReturnToPool();
                    tooltip = null;
                }
                else
                {
                    if (tooltip == null)
                    {
                        tooltip = CreateTooltip(Transform);
                        Update();
                    }

                    tooltip.Caption = value;
                }
            }
        }

        public abstract Color Color { set; }
        
        public abstract Color SecondColor { set; }

        public abstract float FrameWidth { set; }
        
        static Tooltip CreateTooltip(Transform transform)
        {
            var tooltip = ResourcePool.RentDisplay<Tooltip>(transform);
            tooltip.CaptionColor = Color.white;
            tooltip.Color = Resource.Colors.TooltipBackground;
            tooltip.Scale = 0.25f;

            return tooltip;
        }        
        
        void Update()
        {
            if (tooltip == null)
            {
                return;
            }

            var bounds = new Bounds(Vector3.zero, scale);
            var worldBounds = bounds.TransformBound(Transform.AsPose(), Transform.lossyScale);
            float labelSize = Tooltip.GetRecommendedSize(Transform.position);
            tooltip.Scale = labelSize;
            tooltip.Transform.position = worldBounds.center +
                                         2f * (worldBounds.size.y * 0.3f + labelSize) * Vector3.up;
        }        
        
        void OnTriggerEnter(Collider collision)
        {
            if (!BoxCollider.isTrigger || EnteredCollision == null) return;
            
            if (collision.gameObject.TryGetComponent<BaseBoundary>(out var boundary))
            {
                Debug.Log( this + ": Enter " + boundary.Id);
                EnteredCollision(boundary.Id ?? "");
            }
        }

        void OnTriggerExit(Collider collision)
        {
            if (!BoxCollider.isTrigger || ExitedCollision == null) return;

            if (collision.gameObject.TryGetComponent<BaseBoundary>(out var boundary))
            {
                //Debug.Log( this + ": Exit " + boundary.Id);
                ExitedCollision(boundary.Id ?? "");
            }
        }

        public void Initialize()
        {
        }
        
        public virtual void Suspend()
        {
            Caption = "";
            EnteredCollision = null;
            ExitedCollision = null;
        }          
        
        public virtual void SplitForRecycle()
        {
            tooltip.ReturnToPool();
        }

        public override string ToString()
        {
            return $"[Boundary Id='{Id}']";
        }
    }
}