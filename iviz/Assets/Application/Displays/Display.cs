using UnityEngine;
using System.Collections.Generic;
using System;

namespace Iviz.App
{
    public abstract class Display : MonoBehaviour
    {
        public static readonly Color EnabledFontColor = new Color(0.196f, 0.196f, 0.196f);
        public static readonly Color DisabledFontColor = EnabledFontColor * 3;

        TFFrame parent;
        public virtual TFFrame Parent
        {
            get => parent;
            set
            {
                if (value != parent)
                {
                    if (parent != null)
                    {
                        parent.RemoveListener(this);
                    }
                    parent = value;
                    if (parent != null)
                    {
                        parent.AddListener(this);
                    }
                    else
                    {
                        //Debug.LogWarning("Display: Setting parent of " + name + " to null! (ok if removing)");
                    }
                }
                transform.SetParentLocal(value == null ? null : value.transform);
            }
        }

        public virtual void SetParent(string parentId)
        {
            Parent = (parentId == "") ? TFListener.DisplaysFrame : TFListener.GetOrCreateFrame(parentId);
        }

        public event Action Stopped;

        public virtual void Stop()
        {
            Parent = null;
            Stopped?.Invoke();
            Stopped = null;
        }

        public virtual void Recycle() { }
    }


}