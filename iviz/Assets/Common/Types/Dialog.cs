using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{


    public enum BindingType : byte
    {
        BindToTf,
        BindToUser,
    }
}