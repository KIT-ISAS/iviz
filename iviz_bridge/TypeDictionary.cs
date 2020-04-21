using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iviz.Msgs;

namespace Iviz.Bridge
{
    class TypeDictionary
    {
        readonly Dictionary<string, TypeInfo> typeInfos = new Dictionary<string, TypeInfo>();
        readonly Dictionary<string, Type> types = new Dictionary<string, Type>();

        public TypeDictionary()
        {
            Type[] allTypes = Assembly.GetAssembly(typeof(BuiltIns)).GetTypes();
            IEnumerable<Type> messageTypes = allTypes.Where(IsMessageType).ToArray();
            foreach (Type messageType in messageTypes)
            {
                string rosType = BuiltIns.GetMessageType(messageType);
                types.Add(rosType, messageType);
            }
            /*
            foreach (Type messageType in messageTypes)
            {
                string rosType = BuiltIns.GetMessageType(messageType);
                typeInfos.Add(rosType, new TypeInfo(rosType, messageType));
            }
            */
        }

        bool IsMessageType(Type type)
        {
            return
                type.Namespace != null &&
                !type.IsInterface &&
                typeof(IMessage).IsAssignableFrom(type);
        }

        public bool TryGetType(string rosType, out TypeInfo typeInfo)
        {
            if (typeInfos.TryGetValue(rosType, out typeInfo))
            {
                return true;
            }
            else if (types.TryGetValue(rosType, out Type type))
            {
                typeInfo = new TypeInfo(rosType, type);
                typeInfos.Add(rosType, typeInfo);
                return true;
            }
            return false;
        }
    }
}
