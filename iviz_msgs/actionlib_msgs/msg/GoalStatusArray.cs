/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GoalStatusArray : IDeserializable<GoalStatusArray>, IMessage
    {
        // Stores the statuses for goals that are currently being tracked
        // by an action server
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "status_list")] public GoalStatus[] StatusList;
    
        /// Constructor for empty message.
        public GoalStatusArray()
        {
            StatusList = System.Array.Empty<GoalStatus>();
        }
        
        /// Explicit constructor.
        public GoalStatusArray(in StdMsgs.Header Header, GoalStatus[] StatusList)
        {
            this.Header = Header;
            this.StatusList = StatusList;
        }
        
        /// Constructor with buffer.
        internal GoalStatusArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            StatusList = b.DeserializeArray<GoalStatus>();
            for (int i = 0; i < StatusList.Length; i++)
            {
                StatusList[i] = new GoalStatus(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GoalStatusArray(ref b);
        
        GoalStatusArray IDeserializable<GoalStatusArray>.RosDeserialize(ref Buffer b) => new GoalStatusArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(StatusList);
        }
        
        public void RosValidate()
        {
            if (StatusList is null) throw new System.NullReferenceException(nameof(StatusList));
            for (int i = 0; i < StatusList.Length; i++)
            {
                if (StatusList[i] is null) throw new System.NullReferenceException($"{nameof(StatusList)}[{i}]");
                StatusList[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(StatusList);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_msgs/GoalStatusArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8b2b82f13216d0a8ea88bd3af735e619";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW/jNhC961cQ8GGToknb3X5sA/iQxm7qRXY3SNxeiiKgyLHElhJdkrLjf983lCzL" +
                "iYP1YbeGE0cO+ebNzJuPkbiPzlMQsSQRooxNwMPCeVE4aflrGYX0JFTjPdXRbkROpi5E9FL9QzobiXwj" +
                "ZC2kisbVIpBfkc9+I6nJizJ9ZNeAuk/Yf/7VGXmwJsQsy8af+ZW9v7++gA39UIUifNPyAEmYr7X0WlQU" +
                "pZZRJh9LU5TkzyytyDKxaklapP/GzZLCOS7OSxME3gXV5KWF/4iQFtEJ5aqqqY2SkUQ0Fe3dx02DoIil" +
                "9NGoxkqP885rU/PxhZcVMTregf5tqFYkZpMLnKkDqSYaENoAQXmSgcM9m4isMXV885ovZKP52p3hkQpE" +
                "uTfeZgtk6XGJnDJPGS5g46vWuXNgIzgEKzqIk/TdAx7DqYARUKClU6U4AfPbTSyRTlbFSnojc0sMrBAB" +
                "oL7iS69OB8hM+0LUsnZb+BZxZ+MY2LrHZZ/OSuTMsvehKRBAHFx6tzIaRyE6BlHWQJTCmtxLv8n4Vmsy" +
                "G/3KMcYh3EoZwacMwSmDBGixNrHMQvSMnrLxYPSXUmNbGuDYanJXDakwkFkuNbbPCX7bFUj3cDv9MJl9" +
                "uBbb11h8i98sS0rXRCmD2FBkQebE8VFt4rsA7Zdli3l5NZ/9MRUDzO/2MTkjT8r9KODbu+n0/e18OumB" +
                "X+8De1IEaUOWSDnkwd9A/QEtZhGhZBPZe88JosdUB3WR7Yg+f43wA5GkKLSCQ1UuLTGCiWGLAqInc/IV" +
                "qs9yK4h02lG+//3qajqdDCi/2ae8BrJUpUGL0NCh4igsGu4DhwLxkpnLXz7e7eLCZr4/YCZ3yXXdJFnu" +
                "uB+0pBv6ZGhYFcGhDBbS2AZd/AV6d9N306sBv7H44Tk9T3+TYn4H6XBBuSY+lcvXn+aYk5LoqQmzN9ag" +
                "T0YJptwh0KlNvZLW6Jcc6JTXV8pY/Pg/KK+XXu1iKsKd+Prk9RG+ury52VXyWPx0LMGcMKroIMNjoouc" +
                "PM/WPul6YXzFQ43HR5+G1JeZCab80ImhTN5+BieOCzOLYq/8WgM8Nl7QxM3H+/kQaix+ToCX/bLSTQ8g" +
                "CY2sMQh1G08fAkY5b7eAAIFbneKWH1F7gbEdR5tDujZw/9CqlI0urXXrtI/wQZQC/nC7YQUy3aDiGhO7" +
                "8ZGuaMqbouAwdociPX65xerAKJtN0pbUzd1tkAJvlcmfNJMR0nVpsFukeTxoKUkdpHkXmqXVpelmzNM4" +
                "4T7VrB94yeuqQ48hqpbIlbW4PVhX1wTTPfRWepAkeW4pidFwVej4o7t06wVaMeihyw2zsCDSOXZeViNu" +
                "YL9qbMQ6GYIsOL1ITViSMgujtsWQGARWD6PzrtceAKmqSUWBPmdw6nybPF5Csv8ANJbo1ZQLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
