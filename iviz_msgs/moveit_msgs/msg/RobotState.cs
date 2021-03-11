/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/RobotState")]
    public sealed class RobotState : IDeserializable<RobotState>, IMessage
    {
        // This message contains information about the robot state, i.e. the positions of its joints and links
        [DataMember (Name = "joint_state")] public SensorMsgs.JointState JointState { get; set; }
        // Joints that may have multiple DOF are specified here
        [DataMember (Name = "multi_dof_joint_state")] public SensorMsgs.MultiDOFJointState MultiDofJointState { get; set; }
        // Attached collision objects (attached to some link on the robot)
        [DataMember (Name = "attached_collision_objects")] public AttachedCollisionObject[] AttachedCollisionObjects { get; set; }
        // Flag indicating whether this scene is to be interpreted as a diff with respect to some other scene
        // This is mostly important for handling the attached bodies (whether or not to clear the attached bodies
        // of a moveit::core::RobotState before updating it with this message)
        [DataMember (Name = "is_diff")] public bool IsDiff { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RobotState()
        {
            JointState = new SensorMsgs.JointState();
            MultiDofJointState = new SensorMsgs.MultiDOFJointState();
            AttachedCollisionObjects = System.Array.Empty<AttachedCollisionObject>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public RobotState(SensorMsgs.JointState JointState, SensorMsgs.MultiDOFJointState MultiDofJointState, AttachedCollisionObject[] AttachedCollisionObjects, bool IsDiff)
        {
            this.JointState = JointState;
            this.MultiDofJointState = MultiDofJointState;
            this.AttachedCollisionObjects = AttachedCollisionObjects;
            this.IsDiff = IsDiff;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public RobotState(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RobotState(ref b);
        }
        
        RobotState IDeserializable<RobotState>.RosDeserialize(ref Buffer b)
        {
            return new RobotState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            JointState.RosSerialize(ref b);
            MultiDofJointState.RosSerialize(ref b);
            b.SerializeArray(AttachedCollisionObjects, 0);
            b.Serialize(IsDiff);
        }
        
        public void Dispose()
        {
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
                foreach (var i in AttachedCollisionObjects)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/RobotState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "217a2e8e5547f4162b13a37db9cb4da4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1aW2/bRhZ+F5D/MKgfbLeK0sZp0dUiD0nsNC6aOLXdSxoEAkWOJNYUR+FQltXF/vf9" +
                "vnNmSEpWNi2w8WKBzS5qkZw59/vMnrmc5d7MrffJ1JrUlXWSl97k5cRV86TOXWmSsVvWpp5ZU7mxq42v" +
                "k9r2TT6wA3m7cD7nQm/cxOS1N7+7vMSfpMxMkZdXvudt6V01mvupf/A9P14QhK4bCbheb8/IFw+QSW3m" +
                "ydrMkmtr5suizheFNcdnz01SWeMXNs0nuc3MzFZ2A/RLrsW6DgrZPsrcZLSF7EldJ+kMUFJXFLknn278" +
                "u01BwEESv9XOeDe3woXBikYGh724/1ncfia7374zcfeogTwKkIn3eZFMId0sTyHccmpWMwuoFUBDDT61" +
                "pTX4AcRj/ChrWy0qW4OUBPI0WT6ZmFVez0xlKYe6odAJENnfCzqlWp2vi7XJ5wtX1UlZGygVYi0zMDQV" +
                "bhpWxy7LLXiP9GBh6QR+Wtik2rUYiKDwBFiubV4Ph6mr7HB4Tvmo8McW+KxZLjLlNa+V+Lpjcoe9sXMF" +
                "iB2Rud693uP/8L97vZcX3w3NbhO81xFW0niBWODMFZk3oDyhEDLr0yqHTigHMSLl3duaP2pXvV+q+1TQ" +
                "OkSkPjAwvT1B0dlkIUP9bA4qe+2KJd9XZlHlnh6XHpKazE7ykqJeDwHAfL7haYISzxFKkhHA/LDfLr22" +
                "hUvzen176QMvix/4Q/HQdoudQF21ck95LBYF3SwvuwBezbn71eFAGDtpecGOZZlDCjS4zJa1OukYT/Cp" +
                "MpmrYVq4bZLRVoMf0+Eh1hxGDLyrWQ6ILT6RmjcruDpsHhaW2WxgnhTFrTWADkod/CeqUeKHelLcShSi" +
                "QZAjOJWN7Rjocw+a1bhj/EmqKln7vmCgG4kaFwkEtiFhIYZqF16nLqFtk4p5cmV1U1gP3mlhbkGNJsXA" +
                "/DKzpbGD6cCs3bKKUVS4KB0ABv0k3kOzwJJFb7LzPreYNCkNnJVct+oUuo2dL+p1sEZKT7lR3XZ49zO3" +
                "LBBZIwyRk8//QLgHyxCkwul4DVe5EjpfAQvYbGygIbMjHFpBQzQEXUEvNYCpBge9Xu+FGofaSK/n6wqB" +
                "A0FV7GdSuKT+5hEeoyN0XkWD77xSCXzCmFJnGlCUagYTRJUyS6oMEq0TCR4ScvMpYur9woJIMjtfQHka" +
                "WtYLMt7Kc4oIXiUFBLj0moFSN59DqsgXarIb+9XqEzHEPF0WiNSpg6nnJZdPKoiN0CljC72UqTWnx0Ox" +
                "cZsu6xwEwUHLtLKJZ4g+PTa9JVR19JAbenuXK3efaWjKFBWRNyHC3iA7edKZeIapz5W5AWAz4gILDPxA" +
                "3o3wiIgDJCDBLhz84ACUv17Xs5BYr5MqT8bwNABOIQFA3eem/cMOZJI9hDWULoJXiC2OPwO2bOCSp/tN" +
                "RvTLKQSIhYvKXSOISfgSU0UkhP0W+bhKqnVPopWg7O09p4zVi0QjjJybHhqsWLUxyrO7SXK3iyHa57ml" +
                "xsCLlnYIRZqXaKUh4Ih/NmEvs9PKIgJj5QQ/ModYAzgTZDm3ilUEGFym9RIxGstahBpcTzWbeL+c06Bp" +
                "OknMGTRdv/a1nWs08AvRKawerlElpWcVqnumtm5zEsAmhQvYr5AoJXGadIbidWCeMzzfQDsF4hZ8o0hK" +
                "qDUkMBRSQPvT+fFzybNHrCsPbhBA8f9kRZtgVoT5eKsfGVUZ+Tq23qVOBYk/VQ4oupdZZuM7oOqKCA22" +
                "e20rWsg4Sa/I8AYN/0+td5taVxVC4+xPp9a4/H8ptX4os2pfxO2+N7XoJepqrRHkMpowVjXmfGvRCrUS" +
                "F/Dv1rdfREz4qPL6dHHvA3Q31X0Vo14oNpvIMrb1ysI06pW7lTdFhYx5KFWTFMGs9zNE6qoj3V+oY/+4" +
                "xIaqZAyonEbVu+IzkLOLywTlED9usWCacCx2NbfsCWFZzU7pMWk5YGNAN6ukhUOzX5vMQSToCSWWweGY" +
                "bKS+ZlCGUXbFwtfYckCX67PHLXUVE4YULVLmIGJX+TTPtoOphP/AXd/Uk4cwbDiW0KzIoEUAiQI/HJjT" +
                "ibjpigyJi8fGjS1boEuqgNq5PkurAGJToq/FlaLHYg5Sw1ug+FBSmpvmV1Nmmj/uSNutoe1UOHJ5xRpF" +
                "Jbihdj69b82Ucv4YT82v1Z05LeNHw1lMtr7tYzdZGlfuCkYFddHQPCc0nFIw/SblVOpgJhAEvui0YUn7" +
                "HNbdFYMaDHfpDgpRJbX89eFdoF8yEXlkmf/nuBRg7aMOJj4djzr7UQ4/MBJTlq3Zeq0+Pe5MlSR7oWHK" +
                "b+L8hA4sSZQDuFhG87fkK4zTRJTN0FJmVKhDURj4WYLGSiSFXhG/WODWt6no7WmM6I78uEwQ7iGqAJ44" +
                "FDVGZgVk6cKwcIDgxbmdrVH6MUSFQd/eLnhx7KdtRcNGIwhFkPW2BaVAe3GOpBMnGayGUrM7motDTI5K" +
                "QZFbBRrcEgl4j4gxWUpQ3Zv7DWFKBov0An1gttYRCMoEpTRsaEsGATbS0S4zysSkqGWhgkCVTGtQdac6" +
                "PdroTqENaRRUH8xEyqRkoTlhNKwAdEggiG4WYkc/r1ktLdD/M5VUbim+EKAc9uOYTHCUNmUor9aCrbIo" +
                "4LiNbR9LK0VM9QFmnCcjWfC1i24r3cxl8xLCIHWjgCIqZWXR4Tfl6pYykD0n5qp0q2ZcEdbfjVvucMcn" +
                "oQyUTJiJdJpZc+zpxG22q0blFkYfOA0yPBADElhQ4EsgP8WIPPgrGt6wL6oaMw+1CybpcYJk7IKAGgfS" +
                "vyPO7aaljHqUGeXhkhAIJkJuh/gh6HKuuKOaj26r23IxR66EsWyP1aVG75w1RAFcuAL8RzQpx7ZzEHgN" +
                "YUnQUTpl1ev4iROrdtl2zeE3vtO0sAiYXlo/24TKN1g71w874fBbC+Ip/YNKYA/M8T6GQKFC8C13fTMO" +
                "BzyyLPaoWlCA+aU6GzSWZaILBFiiOOzS9ppbyYhg+gCT/NZS9yRjr98xDJU6V5UyjuFxhtR2nUWw0evc" +
                "LT2qQHuDmoHko9TUlCoxZ9Abr1HEPzk+fvwl0ZxLXN3ANKkcxwpousrrvHLlnLUvZ9YIEmtICb05Jkjq" +
                "CnIaVcOf/ZZN5NmhYjo/eXn288njr4SnxYKhiiVstOYw8wixVYgO7eHHeI0lt26KfEILLZOvX5+8On78" +
                "MMThFududIKlj8C4CpYfVC21/wFXRL3FNna+9DVXFHZSa4vKEQkCmncFZQXRxojRBlSclkCSmZIosjki" +
                "gWcLjDZjhQ+YeGQxGhe6+PnTxcWPhxWEx7/8z5w9/f7k2SWHp399c/hH+Tz796evEjflUGIiaS/EMkQy" +
                "zqrYwqI2kEkdS0do0VYcj0z1QK+ZHujJEkyF53cbpcWVbY6LuhiG8kb3t0c70luKxSBolSYbx3gPKBFg" +
                "Nu6SEtKsjM++vzh79QCT7ThTe/Pk5Q9GAeB4p7FiRNrGBzpNJ2N1lEo7N9TUHnPKwJxI7cBDoVtaF1eS" +
                "mY5zV6haruzQfPaPfUp4f7j/jPXN8dP9vtmvnKvxZlbXi+GDB2hFkgLSrvf/+ZmyiMQBe0c9KAO9MgRH" +
                "1V6ocaicjhRYP+b1PjblqPrhCFfWhon6pIC3jvMC7U7IULsMlsesKsTYRB8/VdsQIOSKrh8w6yiMxrWE" +
                "nBjldDAq83oOSgOzcsooYIamEYC8owjwblsEw6//9u0jXcHsqzMDrLtN8X7AdPHjDzhVRZXAo9VGTxuI" +
                "L94XL+IKhS2ozP5q6o++0Tc8yx6arx8dPZRHrK64AEW0W4UVyPwrDHO2XrNIISMRQTyW169zly0Lfpc5" +
                "Qe0W+9GgYdqfbmL/oZKBZdqxeurYYTTsFzS2vknXqLGldIPFWRNmjrHdgWXEg2NYVpw1os4Zx0IAwBj2" +
                "mdjFGbWC/rKP/2EowKOfb83Ts1+RzPT3xesXJ+cnSDD6+OzND6evjk/OEdDDi7NXJ48fRYePIUpyDWkK" +
                "q7RWi1EBpyfoL8KFkXZpe3DXroh7OKoi+d0NnWVDHf+yb5F7DioETdgU100MVvvtnn1NcXIpIzSHYFxI" +
                "1Tbi1755oxP937o0U8jSOdlyipIxULQdhtg/NfxB6INWtqNfUZe0T28aWfPpN+byDkkq/0CVTMCodkZO" +
                "/A3H7gigSieCGqOx8o2j/nxJEkK/oxYU6VC4o/Mnx6c/XbBO6uCMShaYVLAeU6pU1HRkFCGDxFgkynlM" +
                "QPWbSVB2DEw7QdyAO3pxcvrdi0tzQNjh4bDlSe+VdCTe8jTb6LOiL5gD+sKh4mOoi3iUu4BHHzp4PoSF" +
                "k8UoO1VfaFF240TW1qlA/IT9bbW/7ZM8+8kr6YVlwIoTyEVrQyJT7mfXSXtfLvrhpOuLINTopFvCbExq" +
                "i3lWpa2n3lrcCgYL72gSxmZAu9Dq1mkkq9LtUZgojEWCftcbMBR4Z+KJ0bYOb5ubAZ3BfGfd3fEIYpph" +
                "38aEqnuJB6OReBDacvyR0exd5CI2mk0G6lDL7pKRAs22eiHO1sspqom/d0LtdVIsYfhwfF4acJ37gWCT" +
                "p54ofPzbdz0iuQwA5IgpwCKCzigv7oitGKrAJQsmuegzu9VoQpxyG0Q33Zm0IiO7pBY5Q9HX0KWXLN4e" +
                "Kan2ZiTDwTsiWHr13dcB9LgcXqdNfzsUaCYHyY35gmPBL0z6B/6TmcdG2uzEDB/D0u3k7ZfvOGlsHr/i" +
                "Y9o8PuRj1jwevWvOIt4+eifvPp0MPjLdu7c179p5SLq1J1qcOLL/r5HeBBy5ktcuDgGmvW2HuSDbwcYp" +
                "3/bjGQu+4iFJUwxKtRH376gr3k3srNZbVe+aiXoHl+Y3e8OrRRxRhOK0mZiEyMCUGAcSHBzyah5ONTQ8" +
                "tsfZEi+22ByA99Ze2otg/vZNMN5QbV9usHX7jli2jJMJFAQjjod4VVguj/0LUVA0FCMtAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
