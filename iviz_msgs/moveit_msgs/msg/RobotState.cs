/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class RobotState : IDeserializable<RobotState>, IMessage
    {
        // This message contains information about the robot state, i.e. the positions of its joints and links
        [DataMember (Name = "joint_state")] public SensorMsgs.JointState JointState;
        // Joints that may have multiple DOF are specified here
        [DataMember (Name = "multi_dof_joint_state")] public SensorMsgs.MultiDOFJointState MultiDofJointState;
        // Attached collision objects (attached to some link on the robot)
        [DataMember (Name = "attached_collision_objects")] public AttachedCollisionObject[] AttachedCollisionObjects;
        // Flag indicating whether this scene is to be interpreted as a diff with respect to some other scene
        // This is mostly important for handling the attached bodies (whether or not to clear the attached bodies
        // of a moveit::core::RobotState before updating it with this message)
        [DataMember (Name = "is_diff")] public bool IsDiff;
    
        /// Constructor for empty message.
        public RobotState()
        {
            JointState = new SensorMsgs.JointState();
            MultiDofJointState = new SensorMsgs.MultiDOFJointState();
            AttachedCollisionObjects = System.Array.Empty<AttachedCollisionObject>();
        }
        
        /// Explicit constructor.
        public RobotState(SensorMsgs.JointState JointState, SensorMsgs.MultiDOFJointState MultiDofJointState, AttachedCollisionObject[] AttachedCollisionObjects, bool IsDiff)
        {
            this.JointState = JointState;
            this.MultiDofJointState = MultiDofJointState;
            this.AttachedCollisionObjects = AttachedCollisionObjects;
            this.IsDiff = IsDiff;
        }
        
        /// Constructor with buffer.
        internal RobotState(ref Buffer b)
        {
            JointState = new SensorMsgs.JointState(ref b);
            MultiDofJointState = new SensorMsgs.MultiDOFJointState(ref b);
            AttachedCollisionObjects = b.DeserializeArray<AttachedCollisionObject>();
            for (int i = 0; i < AttachedCollisionObjects.Length; i++)
            {
                AttachedCollisionObjects[i] = new AttachedCollisionObject(ref b);
            }
            IsDiff = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new RobotState(ref b);
        
        RobotState IDeserializable<RobotState>.RosDeserialize(ref Buffer b) => new RobotState(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            JointState.RosSerialize(ref b);
            MultiDofJointState.RosSerialize(ref b);
            b.SerializeArray(AttachedCollisionObjects);
            b.Serialize(IsDiff);
        }
        
        public void RosValidate()
        {
            if (JointState is null) throw new System.NullReferenceException(nameof(JointState));
            JointState.RosValidate();
            if (MultiDofJointState is null) throw new System.NullReferenceException(nameof(MultiDofJointState));
            MultiDofJointState.RosValidate();
            if (AttachedCollisionObjects is null) throw new System.NullReferenceException(nameof(AttachedCollisionObjects));
            for (int i = 0; i < AttachedCollisionObjects.Length; i++)
            {
                if (AttachedCollisionObjects[i] is null) throw new System.NullReferenceException($"{nameof(AttachedCollisionObjects)}[{i}]");
                AttachedCollisionObjects[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += JointState.RosMessageLength;
                size += MultiDofJointState.RosMessageLength;
                size += BuiltIns.GetArraySize(AttachedCollisionObjects);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/RobotState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "217a2e8e5547f4162b13a37db9cb4da4";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1aW2/bRhZ+168Y1A+2W0Vp47ToapGHJHYaF02c2u4lDQKBIkcSa4qjcCjL6mL/+37f" +
                "OTMkJTmbFlh7scBmF7VIzpz7fWbPXM5yb+bW+2RqTerKOslLb/Jy4qp5UueuNMnYLWtTz6yp3NjVxtdJ" +
                "bfsmH9iBvF04n3OhN25i8tqb311e4k9SZqbIyyvf87b0rhrN/dQ//J4fLwhC140EXK+3Z+SLB8ikNvNk" +
                "bWbJtTXzZVHni8Ka47MXJqms8Qub5pPcZmZmK7sB+hXXYl0HhWwfZW4y2kL2tK6TdAYoqSuK3JNPN/7d" +
                "piDgIInfame8m1vhwmBFI4PDXtz/PG4/k93v3pu4e9RAHgXIxPuiSKaQbpanEG45NauZBdQKoKEGn9rS" +
                "GvwA4jF+lLWtFpWtQUoCeZosn0zMKq9nprKUQ91Q6ASI7O8FnVKtztfF2uTzhavqpKwNlAqxlhkYmgo3" +
                "Datjl+UWvEd6sLB0Aj8tbFLdthiIoPAEWK5tXg+HqavscHhO+ajwxxb4rFkuMuU1r5X4umNyh72xcwWI" +
                "HZG5Xu/Jf/hf79XFd0NzuwF2JJU0LiDmN3NF5g3ITiiBzPq0yqEQCkEsSBn3tuaP2lUfluo7FVQO+agD" +
                "DExvT1B0NlkIUD+bg8peu2LJ95VZVLmnu6WHpCazk7yknNdDADCfb7iZoMRzhJJkBDA/7LdLr23h0rxe" +
                "7y596GXxQ38o7tlusRPoqlbuKY/FoqCP5WUXwOs5d78+HAhjJy0v2LEsc0iB1pbZslYPHeMJDlUmc5W1" +
                "hc8mGQ01ODG9HWLNYcHAu5rlgNjiE6l5s4Kfw+BhXpnNBuZpUeysAXRQ6uA8UY0SPNSN4laiEA2CHMGp" +
                "bGwHQJ970KyWHYNPUlXJ2vcFA31I1LhIILANCQsxVLvwOnUJDZtUzJMrq5vCevBOC3MLajQpBuaXmS2N" +
                "HUwHZu2WVQyhwkXpADDoJ/EemgWWLLqSnfe5xaRJaeCp5LpVp9Bt7HxRr4M1UnrKjeq2w7ufuWWBsBph" +
                "iJx8/gdiPViGIBVOx2u4ypXQ+QpYwGZjAw2ZHeHQChqiIegKeqkBTDU46PV6L9U41EZ6PV9XiBqIqGI/" +
                "k8Il9TeP8RgdofMqGnznlUrgzgJKnWk0UZrBBiJKmSVVBnHWiUQOCbb5FNH0QWFBITmdL6A5jSvrBblu" +
                "hTlF7K6SAtJbes09qZvPIVJkCrXXjf1q8olYYZ4uC8To1MHO85LLJxVkRugUsIVSytSa0+OhGLhNl3UO" +
                "guCdZVrZxDM4nx6b3hJ6OnrEDb29y5V7wAQ0ZXKKyJv4YG+QlzzpTDxj1OfK3ACwGW2BBdZ9IO9GeES4" +
                "ARKQYBcOTnAAyt+s61lIqddJlSdjuBkAp5AAoO5z0/5hBzLJHsIUShfBK8QWx58BWzZwydODJhf65RQC" +
                "xMJF5a4RwSR2iZ0iDMJ4i3xcJdW6J6FKUPb2XlDG6kKiEYbNTfcMJqzaGOXZfaS33SIIzJ5bqguMaEWH" +
                "IKQZiSYaQo14ZhPwMjutLGIvVk7wI3OIMoAzQX5zq1g8gLtlWi8RnbGsxadh9VTziPfLOa2ZdpPEbEG7" +
                "9Wtf27nGAb8QhcLk4RdVUnoWn7pnaus2GwFsUriA/QopUlKmSWeoWQfmBQPzDVRTIGLBMYqkhE5D6kL9" +
                "BLQ/nR+/kAx7xHLy4AahE/9PVjQI5kPYjrf6kfGUMa9j6F3qVJD4U+WAonuZXza+A6quiNBguNe2onmM" +
                "k/SKDG/Q8P+ker9JdVUhLs7+dFKNy/+XkurHcqq2Q9zue1OLFqKu1hpALqMJY1VjzjuLVqiSuIB/t779" +
                "ImLCR5XXXQW9j1AdJVnFkBdqzCasjG29srCLeuV2MqbojwEPFWqSIpL1foY8XXWk+wv16h+X2FCVDACV" +
                "05B6P0wGYm5hMUEJxG9b9JsmEItFzS2bQNhUs1OaStoMeBjQwSrp2dDd1yZzkAeaQIlicDVmGampGY5h" +
                "jl2Z8DW2HNDZ+mxqS13FVCG1ilQ3iNVVPs2z7TAqgT8w1zf15BFMGi4lNCsyqBBAorQPB+Z0Ig66IkPi" +
                "3LFZY5sW6JLkXzvXZ0UVQGwK9I04UfRVDD5q+Am0HspIc9P8akpL88e9qLq1sdu0jRResS5R8W3onE8f" +
                "WgOlkD/JUPy1uidflaAR2IoJ1rdd6yY/48pdwZygKJqY5zCGAwmm3KScSuHLpIFgF301LGmfw7r74U7D" +
                "3y1agypUPS1zfTgViJfUQwZZ1P85FgVY+6gziLtiUCc8yt5HBl8hPW+9VT8ed0ZHkqvQG+U3cU5Cp5WU" +
                "ySlbrJj5W7ITZmYix2YyKYMoVJ0oA/wsQQ8lYkJPiF8sZ/l9hzaNC925HpcJwj1EEsATP6K6yKuALF2Y" +
                "CA4QsDicszUKPYalMM3buw1enO1pB9Gw0QhCEWS9bUEp0F6cF+lkSaanobDszt/ipJLzUFDkVoEGtxTD" +
                "A2JMkBLU8uZBQ5iSwZK8QMuXrXXUgaJAKQ0b2gJBgI10fsssMjEpKleoIFAlUxnU2KlOiTYaUWhD2gLV" +
                "B7OPMimZZ04YDSsAHZIGgpqF2NG3ayZLC/T5TB+VW4ojBCiH/TgOExylTRm+q7VgqyzKNW5jh8dCShFT" +
                "fYAZh8ZIEHztostK73LZvIQwSN0ooIhKWVk0801xuqUMZMyJuSrdqhlLhPX34ZO7vvg0VHyS+jIRTTNN" +
                "ju2b+Mx2gaiswuIDm0GAB2I9AgvaewXcpxiCB2dlY6v7op4x21CjYFYeJ8i+Lkin8R79O+JwblrKPEd5" +
                "URYuCYFgIuR2TB+iLYeHtxTu0Wd1Wy62yJWwlO3BuZTjndOEKIALV4D/iCblbHYOAq8hLIk4SqesehM/" +
                "cSzVLtsuMvzGd9oVFgHTK+tnm1D5Bmvn+uFWOPzWgnhG56AS2O5ygI9hT6gKfMtd34zDEY4si+2oFhFg" +
                "fqmeBo1lmegC0ZUoDru0veFWMiKYPsIkv7XUPc3Y1ncMQ6XOVaWMXXhgIcVcZxFs9Dp3S4+yz96gUiD5" +
                "qC01mUrAGfTGa5TsT4+Pn3xJNOcSVDcwTSrHCQL6q/I6r1w5Z7HLwTQixBpSQhuOSZG6gpw31XBmv2UT" +
                "eXaomM5PXp39fPLkK+FpsWCcYs0arTmMN0JgFaJDJ/gpXmONrZsin9BCy+SbNyevj588CkG4xXk7OsHS" +
                "R1RcBcsPqpZi/4Arot5ixzpf+porCjuptRvlNATRzLuCsoJoY8RooymORCDJTEkU2RyRwLMFRpixpAdM" +
                "PLIAjQtd/HxXQfHTQaW395f/mbNn3588v+SE9K9vDv8onOf//nBVgqYcO0wk4YVAhjDGmRS7VVQFMpFj" +
                "xQgV2opjkKme1zVTAj07gp3weG6jqLiyzYFQF8NQ3uj+9vBGOkkxF0Ss0mTjGOwBJQLMxl1SQoKVMdn3" +
                "F2evH2J8HWdnb5+++sEoABzgNCaMMNs4QKfFZKCOUmnng5rUY0IZmBOpGnjss6N08SOZ3Th3hXrlyg7N" +
                "Z//Yp4T3h/vPWdkcP9vvm/3KuRpvZnW9GD58iPYjKSDtev+fnymLyBowdlSCMrgrQ2RU7YXqhsrpSIGV" +
                "Y17vY1OOYh9ecGVtGJtPCrjqOC/Q4oT0dJu98hRVhRhb5uNnahsChFzR7wNmHXnRuJaQE0OcDkBlKM+B" +
                "aGBWzhEFzNA0ApB3FAHebYtg+PXfvn2sK5h6dUKAdbsU7wdMFz/+gHNTlAg8PG30tIH44kPxMq5Q2ILK" +
                "7K+m/ugbfcOj6qH5+vHRI3nE6ooLUD67VViBtL/C3GbrNSsUMhIRxFN3/Tp32bLgd5kK1G6xHw0apn1X" +
                "Y/mPVQug6FjddOww//ULWlrfpGuU1lK0wdysCYPF2OXALOK5MMwqDhRR4YxjCQBgDPhM6eKJWjh/2cf/" +
                "MALg4c635tnZr0hj+vvizcuT8xOkFn18/vaH09fHJ+cI5eHF2euTJ4+jt8f4JFmGNIVVWqXFkIDzEbQV" +
                "4TJIu7Q9l2tXxD2cSpH87obOsqHOeNmuyB0GFYKmaorrJkaq/XbPviY3uXARekIwLqRq9/Br37zVsf1v" +
                "XZopZGmYbDlFsRgo2o5BbJsa/iD0QSvb0a+oSNqnt42s+fQbs3iHJJV/oEqGXVQ7wyb+hlN1RE+lExGN" +
                "oVj5xkl+viQJoc1RC4p0KNzR+dPj058uWCF1cEYlC0wqWA8iVSpqOjJ+kJlhLA/l0CWg+s0kKDgGph0W" +
                "bsAdvTw5/e7lpTkg7PBw2PKkd0Y6Em95mm20V9EXzAF94VDxMc5FPMpdwKMPHTwfw8IhYpSdqi80J7fj" +
                "RMrWYUD8hP1tnb/tkzzgyStpgWWWijPGRWtDIlPuZ7NJe18u+uE464sg1OikW8JsTGqLedajrafuLG4F" +
                "w4V3E+J2mwBpPqud80YWo9uzL9EWywP9rrdbKO3OcBMjbB3SNqf+nel7Z919MQhS4mhvYyTVvZ2DWUg8" +
                "52zZ/cQI9u5TEFvLmHg6pLKdZIBAd63Oh0PzcooK4u+dCHudFEvYO/ydtwFc58ofeOSJJood/+59jzgu" +
                "AwA5PgqwiKAzuIs7Yu+Fym/JIkmu78x2OkvIUu546KZ7ElVk4xaRRbZQ5TVE6dWJd0dKp70ZyRzwXqiV" +
                "vvzWQ349BIenaX/f9v/NkCC5MV9w/PeFSf/AfzLzxEhHnZjhExi4nbz78j0nis3jV3xMm8dHfMyax6P3" +
                "zVHDu8fv5d1dCeATM7ytudat555bW6KhifP6/xLdMcLI5bp2bYgo7b05zP7Y9jWO+K4fz0/wFQ9JmmIU" +
                "qt22f08t8ZZhZ7Xej3rfzMw7uDSV2RveE+IcItShzVgkRANmvzh14HSQl+xwaKHxsD2elhixxeUArLeW" +
                "0l7p8rt3unjRtH25wdbuba9sGccPyP0jzoB445fXwP4FXCib7+ksAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
