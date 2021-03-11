/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlanningSceneWorld")]
    public sealed class PlanningSceneWorld : IDeserializable<PlanningSceneWorld>, IMessage
    {
        // collision objects
        [DataMember (Name = "collision_objects")] public CollisionObject[] CollisionObjects { get; set; }
        // The octomap that represents additional collision data
        [DataMember (Name = "octomap")] public OctomapMsgs.OctomapWithPose Octomap { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlanningSceneWorld()
        {
            CollisionObjects = System.Array.Empty<CollisionObject>();
            Octomap = new OctomapMsgs.OctomapWithPose();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlanningSceneWorld(CollisionObject[] CollisionObjects, OctomapMsgs.OctomapWithPose Octomap)
        {
            this.CollisionObjects = CollisionObjects;
            this.Octomap = Octomap;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlanningSceneWorld(ref Buffer b)
        {
            CollisionObjects = b.DeserializeArray<CollisionObject>();
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                CollisionObjects[i] = new CollisionObject(ref b);
            }
            Octomap = new OctomapMsgs.OctomapWithPose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlanningSceneWorld(ref b);
        }
        
        PlanningSceneWorld IDeserializable<PlanningSceneWorld>.RosDeserialize(ref Buffer b)
        {
            return new PlanningSceneWorld(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(CollisionObjects, 0);
            Octomap.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (CollisionObjects is null) throw new System.NullReferenceException(nameof(CollisionObjects));
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                if (CollisionObjects[i] is null) throw new System.NullReferenceException($"{nameof(CollisionObjects)}[{i}]");
                CollisionObjects[i].RosValidate();
            }
            if (Octomap is null) throw new System.NullReferenceException(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in CollisionObjects)
                {
                    size += i.RosMessageLength;
                }
                size += Octomap.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningSceneWorld";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "373d88390d1db385335639f687723ee6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZbVMbORL+PlX8B9Xmg+HimGxgt/Z8xQfAzsJWCCxwd0molEuekW0tGmmi0WCcq/vv" +
                "97Q0mhmDs3t7dUCoYM20+r2fbskvWGqUkqU0mpnpbyJ1ZXIcn5z7BzefW5pJpElesOuFYCZ1JucFcwvu" +
                "mBWFFaXQrmQ8y6QDPVcd/hl3PKl3TPJyXu6eh8U/pVtcmLJhl2wlB//nn63k7OrnIcvNnZAuCH9g5hZM" +
                "OmQLwTNh+6wqRcZmxjKpnbCwy0k9h5mCFVC0TE48XU0evSEzZmaeKLiJbWuei8BLanYG4aduJymdJWYy" +
                "a7wYqN2qAA/NuPfUlJNDZuxWm2Ubm/B3YkVq5tq7uPakf35NHIhN5Nw6fy5MLiBYIDhlaVLJHbRawvMd" +
                "hQdhm7TBSsatCCSIa+FVNJ6aFnImwaB1wJVRsD+KSVlhZQ4F7+CscsELEfT0VBfxFVKrQ1bvXQVKSoju" +
                "+0lwPCSdiXKxzpWegDYPLzbyoXctiyNT6YyCUCiuYei2+FJx8iaTZWtdn00rF4JOZCzlmk3hK3gRWZEh" +
                "sMQCDzvpTiJ2urpd0FYyxEv6hpH0rtXuMMvKbhrVXicqTRLLVGgxYKdruYYcvZOmKtWKiXtZQr8+k47s" +
                "QV0qnopskExXTrDD0ejgNYm5FFQOa5Jm1uR+LfSdtEbnMBSfnbQCfLfFnbArtwilgIrPuUvh8Qc5IbOd" +
                "IOlyfHb+j/HB996mohA6I1Pgr2gX8eDKIodqpUvK/z+2NTMQqo0Lm6KdiEJr5MXF+P3o4A2Jxt5W5mZx" +
                "XkqfabGsM78ONaEZ2yaKGDfUhOWrkuVV6YhCiRnUyAu32oEoOWOlUeQruDYiRi0ZGmaihCezoKL3zR4p" +
                "eF4IG7IP7gFPLAE9eSQ08fXT4WLpspCQAdcIC68c1xm3GSrHcQIkD4cLOV8I+0ohExR28bxAwP1bgp0y" +
                "AAgsxe8cLrVcwRceAGFaavK80jIF9DAnUZHd/eQ9wr6CWyfTSnELemNRpUQ+s0BS4o7fEsUqdCrY6WgI" +
                "Gl2KtCKEgCSpU4TXl+XpiCUV0HvvDW1IXlwvzSsC8zlguxEeUhDKinvqXaQnL4eQ8Zdg3AC84R2grUZJ" +
                "bvtnEyzLHQYhUEEUJl2wbWh+gcKgCCLgd9xKPlUAc0okpcC1R5t6Ox3OpPaQaa5NZB84tjL+G7a64Us2" +
                "vVogZsqXTTWHA0FYWCQi6oJNV55Jqgi8mJJTy+0qoV1BZPLiLfkYRJSoFBH5qFXE1uWjMUEDe7KE/OM+" +
                "hxz90z/s/OiX8fE15caf31z/UMEeG+241AQfVKihdvnU1N3CN3K0bioZ9JnYXFEZNOS4pUFGAXtAgWSH" +
                "JzFhAEPNXGCzZagQ5DeSORMzia7DNSQGFrGt34pVnDS6Eob+SdgPFCbcBiDTDONfzFEjmMKmMYrgEhlm" +
                "064qpTNAKQQfFvxydf5+F4Uby+Pj4dk7FhgM2GEDq2j9DSjn/NYjJ2ylpIleSY2lMQKpjr2QG4ecARsP" +
                "5gOAuN4QdY/tBMbKmFtk7a0Ysu/+1SMP94a9Y1Oli9FRr8961hiHJwvniuHurjIoEHjb9f79XTARkwzy" +
                "WRMIabih7tYheh7ZoCgU7ngBpQThPWySKEvA160QNWDMFNrHVCrpVvXItClhYbAITvRQiJlodBRywzMh" +
                "q6gX1ZIRKZWVlFwV/ERt9x5VqYSHo7dQsDaWlsyzGbLGAf4ZuQDPHrpg+MNff9oPFDQOQksoB7rHGvdq" +
                "SVe/vmMIWykWRmVNnNYEX31RJ5Ei8PaiWG85L/d+DE8KY/Hkh/29N34JaksEUimzrCmAL0tg/IPHNDWT" +
                "IVFAPHWEt7nJKkXvoZYSzhS9mNBI7afrkN+aYalXjkKlTs19H3MQJVufpSsAsT9LIOME9a1DpQKbMFiH" +
                "8vaNccHvKClo8J7GyRTMaA6hSdMXo/WF/rqPf4PEd7af2NH5B0xX4fPVxcn4coyJJyyPP747fT8aX2LC" +
                "qB+cvx8f7MeCjxDlhx/SqaYKh4eICmgOmk4P5TrpTBnuftzH4NpSxD254H5u627okA2Z4GiY1LUxX7ja" +
                "CWGCJHfdR7DqtXt6YeZK6uyktzDcq9r3qw999rHv6/VTV2dyMr1WQs9xhqk1eghDJYCysQ9OH7S+nXzA" +
                "oNyuPja+ptUnGi47KgX/11oZDfymsBNy4i8sBfrQHBT0BKgRGge7Lc9kRSpQF/eTNGVQ1CPwnVwejk7/" +
                "fkWDe0dmDLLnSQEOU1jwSkgdYCbUgPDm1MKV8YYTzSfGMQcPWEBIwKBY4zs5GZ/+fHLNtol3vdhpbaK+" +
                "NOt6vLUJuTxfuMbndS2wbaqFnSCPoC7KCdbVcsKiI+dbUsCh8V0IX31m3iwTXZu6ada8wv72+PmwJjGH" +
                "p9L6QZQ6N02sRZtD3qe03yBIlO9V0Q+eZS9rp8YifeDMJqUeGE/HpLZSHxG3jgHhk6Hc49NpuBZprnbC" +
                "qEOTC/Us1OrMCspYnC/7IWA0JIT3vqd4h4fc83sHLLkgnzUEya84eQurPd+W7vlshDJb8diSxsGuvukJ" +
                "JvhRzkd63eIIhOy++bRqPn19Lgta/zVmdG/i1ry6rj+tvrTep2EIWfv7RsVPy2dptXSx0zTYTjDoNoeA" +
                "EJdbAWRwMtJzDEt/63SSO64q1DVwjY58pomnP/XjJgPHTFHefE5IyHXNAD2t4UUCiBtPXYVZMe6IVx8Y" +
                "ciuaB0XQZkNagVnc9GzeioZs8lq0DDNto1c4It/sBVXF/QS+ezaF/d0YaXr5CF9QcPSSbmKbm1d/Cdfc" +
                "1PF79pJOtS9Z+hX/ZeyA+WstzoYHKGQxu3n9Gctps/yelmmzfEPLrFnufW5S/2afrrzF7AmPt79zCx4Q" +
                "d2+EFCsoU6e4AcG9UziqoMmUDBtQw5vvoD0ux1toT7fxBjdsCgd5tuFSkkBjLnXkWpdAzRBUG+/xn/7+" +
                "fpPU/91fbxVHZhl0fRxbYWTcuu3HN8LJXZOmVYFLzR04hM0qTPD+Cdcp5rvgje3B1O0OcO6dSYWr36kx" +
                "qmbkrwoUzjidbwbqDkNNMmxf/zrgUuD+ELdZyG+6/ckhtx6kc7rxIbQL27TB2NqgMaJbbyMmtRk4b0mu" +
                "5NempsJW/+VGOAZbP1AN8BWACUdRatZLK+nS0dOWCU0fNOjTdzdbyX8AG5BB8S8aAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
