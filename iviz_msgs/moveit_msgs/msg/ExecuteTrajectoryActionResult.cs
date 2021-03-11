/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryActionResult")]
    public sealed class ExecuteTrajectoryActionResult : IDeserializable<ExecuteTrajectoryActionResult>, IActionResult<ExecuteTrajectoryResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ExecuteTrajectoryResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ExecuteTrajectoryResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ExecuteTrajectoryResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ExecuteTrajectoryResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionResult(ref b);
        }
        
        ExecuteTrajectoryActionResult IDeserializable<ExecuteTrajectoryActionResult>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8aaeab5c1cdb613e6a2913ebcc104c0d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X227bRhB9J+B/WMBAkxSVG9u5A3ygZdpmI5EuRRvoE7EiV9K2FKnuLu3o73tmeTGl" +
                "2I0fkgiOKGlnZ86eOTM7uRI8F4qt7MPhmZFVWch5utZL/ftlxYuZ4abWTNuH438RWW1EovjfIjOV2sZC" +
                "14Vhyj4OHPc7vw6c6ezyE6LnDaIri/PAOWTAVeZc5WwtDM+54WxR4RxyuRJqVIg7URDm9UbkzK6a7Ubo" +
                "I2xMVlIz/C1FKRQvii2rNYxMxbJqva5LmXEjmJFrsbMfO2XJONtwZWRWF1zBvlK5LMl8ofhakHf8afFv" +
                "LcpMsOD8E2xKTZRJANrCQ6YE17JcYpE5tSzN6QltcA6T+2qEr2KJbPTBmVlxQ2DFlw0oJpxcf0KMX5vD" +
                "HcE32BGIkmv20v6W4qt+xRAEEMSmylbsJZBfb82qKuFQsDuuJJ8XghxnYABeX9CmF68Gngn2J1bysurc" +
                "Nx4fYjzHbdn7pTONVshZQafX9RIEwnCjqjuZw3S+tU6yQorSMEhQcbV1aFcT0jm8II5hhF02I3hyratM" +
                "IgE5u5dm5WijyLvNRipz54cJ8slCOXDoM5K7xIMgUI4/dOXTfLn2w/MgvGTdy2Wv8U7KFHYbW3HNtsKQ" +
                "JueCKMqa3LccNcGRdnWHmm18euMkuPXZwOfxrk9KSq0UyIUO54Joepbj69j3p9eJf947Ptl1rEQmoG4o" +
                "E1mHQugXFIA2jC8MxCwNnV5RjoTtHgjtPAD9+nWIf9CJZaHRHApzUwjyII3uvADoy0SoNQqwoG5gxKsW" +
                "8uxmPPb98wHk013I9/DMs5VEl8ghxYxYWNTUCh4j4qkw3lkUP/BCYd48EmZe2aPntVXmA/ZHI+W1+CY1" +
                "pApdoRIWXBa1Ek/Bi/0//PEAn8vefg1PCerjTyjA1lRVm325/PZtjHORcbRV67MPVqNVGg6k1CTQrGV5" +
                "xwuZP3WAVnl9pbjs3U9QXi+9sjK2CB/E1yevZ3jsTSYPleyy988FOBe4rcSjCJ/DLnLydbZ2QZcLqdZ0" +
                "r9EN0qfBtmZCIvKdQwxl8uE7HOJ5NJModsqvCUA3xxOamESzZOjKZR+tQ6/syGgvEHhiObJGTkRDAu8p" +
                "IC9HzSCgIfAit7zNn1F7mnxXxDZRei9xfFQOYu22TufQK4rq3o4kZIhSwIfq4b4CmPauohpjg0GLtuRi" +
                "Xi+XRGNrZMQX4/zU2yw4pyGLRNAMIi1PGjNfU9X2Zgar9yuJCcPeyoOuYgUicpqIAjvA2BnrEaqwX5Qk" +
                "IRxUaOIIg45Yb5CuosBu8qmb/N0LhO5dd+qDKoWirmIRDQeGFj8aTDtkoBsD3nY3EQsh8jnP/iFBYkcz" +
                "yGKo1JovRZMdvRGZXMisqweLQB+13mniawwAal3bukCrk7A66vIHqx+XvTX0KE2Tuifm8wPHcaYwC4yv" +
                "VKXGFZEh6GOa4fPPwbYP4MBpxl9cApQrqirkHOyt+J2sVLtqL/PZzD1uv194weQm9t2P9HLaH68nXhii" +
                "Eae06p+7o846CG+9SXCeTqMkiMKU7NzRSbs4+DFtDT1cmOnZX6kf3gZxFE79MEnHV1546buj03bbOAqT" +
                "OJr0sd60v9+E3tnET5Mo9f68CWI/nfnhLIpTOPXc0dvWKgmmCBHdJO7oXYe+G7Hc0XtiYlPwsiTZ/ML+" +
                "Qfdac/yHoy+qJm26Yyfx4iTFe+LjCOk4wn00w6HAwOtHTG6DaILnLL32kitYh7Mk9oIwmcH+uCPzMvIm" +
                "+85Ohmv/5+V0aDhY6jZRbt44e9m5jKOb6zT0pmD5+O3+4p4nmLzbM4mjs6g9ojs6fr+3ihv6c+f8w95a" +
                "dEZDUrcKPaGYt2gv612aL2IYpAAQzi6ieGpTTyIcnXRC68mCXPzxZ9Ii9HALOxIFDDsGB1jp3a51pLWC" +
                "CcKLqF8DWYdDGezgCqM0+JzOoskNKRkSPUYp/wcNkjlx0w8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
