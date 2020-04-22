
namespace Iviz.Msgs.actionlib_msgs
{
    public sealed class GoalStatusArray : IMessage
    {
        // Stores the statuses for goals that are currently being tracked
        // by an action server
        public std_msgs.Header header;
        public GoalStatus[] status_list;
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_msgs/GoalStatusArray";
    
        public IMessage Create() => new GoalStatusArray();
    
        public int GetLength()
        {
            int size = 4;
            size += header.GetLength();
            for (int i = 0; i < status_list.Length; i++)
            {
                size += status_list[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public GoalStatusArray()
        {
            header = new std_msgs.Header();
            status_list = new GoalStatus[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.DeserializeArray(out status_list, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.SerializeArray(status_list, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8b2b82f13216d0a8ea88bd3af735e619";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1WTW/jNhC9C8h/IODDJkWTtrv92AbwwbXd1EV2N4jdXooioMixxJaSvCRlx/++byhZ" +
                "lhMH68NuDSeOHPLN45s3wxmIeagceRFyEj7IUHs8LCsnskpa/loGIR0JVTtHZbBbkZIpMxGcVP+STgYi" +
                "3QpZCqmCqUrhya3JJb+R1OREHj+SG0DNI/Zff7dBHqzxIUnOkuFnfp0l7+Y314iiHwqf+W8aJmfgCQal" +
                "lk6LgoLUMsh4zNxkOblLS2uyzK1YkRbxv2G7In+FjYvceIF3RiU5aSEBRNIiVEJVRVGXRslAIpiCDvZj" +
                "p4EuYiVdMKq20mF95bQpefnSyYIYHW9PH2sqFYnZ5BprSk+qDgaEtkBQjqRnxWcTkdSmDG9e84ZksNhU" +
                "l3ikDEJ3wZuEgSw9rpBW5in9NWJ81RzuCthQhxBFe3Eev3vAo78QCAIKtKpULs7B/G4bcmSUjbGWzsjU" +
                "EgMrKADUV7zp1UUPmWlfi1KW1Q6+QdzHOAW27HD5TJc5cmb59L7OICAWrly1NhpL4TsGUdbAl8Ka1Em3" +
                "TXhXEzIZ/MoaYxF2xYzgU3pfKYMEaLExIU98cIwes/Fg9JczZFMfYNnYcl8SZ7E8kFwuOKbAOX7blkn7" +
                "cDd9P5m9vxG711B8i9/sTIrbRC692FJgT6bEEqkm961Gh8XZYI7Gi9mfU9HD/O4Qk5PypOhPAr67n07f" +
                "3S2mkw749SGwI0VwN5yJrMMh/A0KwKPRLAPMbAKf3nGO6DGWQpkle6LPXwP8wCdRhcZzKMyVJUYwwe9Q" +
                "QPR8Qa5AAVruBoEuWsrzP8bj6XTSo/zmkPIGyFLlBl1Cw4qKVVjW3AqOCfFSmNEvH+73unCY74+ESat4" +
                "dF1HZ+65H42ka/qkNOwKX6ESltLYGr38BXr309+n4x6/ofjhOT1H/5BifkfpcE1VdXhql68/zTElJdFW" +
                "I2YXrEarDBJMuUmgWZtyLa3RLx2gdV5XKUPx4//gvM56ZRViEe7N1yWvU3g8ur3dV/JQ/HQqwZRwW9FR" +
                "hqeoi5w8z9Yh6XJpXMH3Gt8gXRpia2YmuOv7h+jb5O1nOMRpMrMpDsqvCcA3xwueuP0wX/ShhuLnCDjq" +
                "Rpb2AgGS0Mgag1A793QSMMpVMwh4GNzqqFt6Qu15xq5YbZZ0Y3D8YwNTMhhZW23iSMILUQr4o9rfVyDT" +
                "3lVcY2J/f8QtmtI6y1jGdlGgxy85Xh25zWYTHrLYBM0g0urkebyMR4o3M1Td5AYTRryVe10lGoQ0T0Sz" +
                "OMDU7TXzVCrsp5IthIPy3FqhzRAVK6TLWuzuza0bQugOeuc+uJIcd5XIqD8wtPzRYNohA90Y9NDo+olY" +
                "EukUwy8bEjswZdU2YKj0XmacYWTHr0iZpVG7eogMPBuI0XniaxaAVFHHukCrM1h1tcsfViF7/wFND/Q0" +
                "ngsAAA==";
                
    }
}
