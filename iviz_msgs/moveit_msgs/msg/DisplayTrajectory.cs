/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/DisplayTrajectory")]
    public sealed class DisplayTrajectory : IDeserializable<DisplayTrajectory>, IMessage
    {
        // The model id for which this path has been generated
        [DataMember (Name = "model_id")] public string ModelId { get; set; }
        // The representation of the path contains position values for all the joints that are moving along the path; a sequence of trajectories may be specified
        [DataMember (Name = "trajectory")] public RobotTrajectory[] Trajectory { get; set; }
        // The robot state is used to obtain positions for all/some of the joints of the robot. 
        // It is used by the path display node to determine the positions of the joints that are not specified in the joint path message above. 
        // If the robot state message contains joint position information for joints that are also mentioned in the joint path message, the positions in the joint path message will overwrite the positions specified in the robot state message. 
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DisplayTrajectory()
        {
            ModelId = "";
            Trajectory = System.Array.Empty<RobotTrajectory>();
            TrajectoryStart = new RobotState();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DisplayTrajectory(string ModelId, RobotTrajectory[] Trajectory, RobotState TrajectoryStart)
        {
            this.ModelId = ModelId;
            this.Trajectory = Trajectory;
            this.TrajectoryStart = TrajectoryStart;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DisplayTrajectory(ref Buffer b)
        {
            ModelId = b.DeserializeString();
            Trajectory = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < Trajectory.Length; i++)
            {
                Trajectory[i] = new RobotTrajectory(ref b);
            }
            TrajectoryStart = new RobotState(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DisplayTrajectory(ref b);
        }
        
        DisplayTrajectory IDeserializable<DisplayTrajectory>.RosDeserialize(ref Buffer b)
        {
            return new DisplayTrajectory(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ModelId);
            b.SerializeArray(Trajectory, 0);
            TrajectoryStart.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ModelId is null) throw new System.NullReferenceException(nameof(ModelId));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            for (int i = 0; i < Trajectory.Length; i++)
            {
                if (Trajectory[i] is null) throw new System.NullReferenceException($"{nameof(Trajectory)}[{i}]");
                Trajectory[i].RosValidate();
            }
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(ModelId);
                foreach (var i in Trajectory)
                {
                    size += i.RosMessageLength;
                }
                size += TrajectoryStart.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/DisplayTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c3c039261ab9e8a11457dac56b6316c8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA+1bbW8bNxL+vr+CqD/YvipyL06Lnop8SGKncdDEbuy+BoFA7VLS1qululxZVg/332+e" +
                "GZL7IrlJcWcDB1waxN5dcjgznLdnyO6pq7lRC5uZQuWZmtpKred5Olf1PHdqqeu5mmunJsaUamZKU+na" +
                "ZImrq7ycybRxniXJHpOpzLIyzpS1rnNbKjslKkaIpJbe5iWRtC7nrze6WBnHK+qi4JG/2bysHf2qa6Ur" +
                "sHWDZXRh6d9A6hullTO/r0yZGl6i0r+ZtLZVTtQWekO8Krc0aT7NidN3dmLrqzBk8/5DM34T2cYY5Yhr" +
                "o0jolTOZqq2yE3AcGY6sHjm7MEE4z7J/YkpDRXTP6khqsmnUkOVuWRCPJWkOa2SmNtUiL40MiUt1qUeF" +
                "lOAzyKaIuThIyC+Mc3pmlJ7YGyN8tBjzIoZBcUs8gbAxeUmCLmQLIXKfCV04S0RKDPgzLgY9me5md52T" +
                "ARDH1brK674qtuTdIQuJyht9yS+bHR7TqKpOkqf/5T/Jm8tvRzBPk9fjhZu5o56dJS0e+PtrSN18Fy2M" +
                "67snvFkVdX5y/rI/cYH348xOx1sk7knMj4iSvDI6M5Wa8w8fGsjPhL1S0xYlvSkXeKQhS7as++Lb1Zkw" +
                "LAySM5B5lJmuMjKbWme61mzf83w2N9WjwtxQDCSDWSzJ1vhrvVkaN+QoQc5MfyUCFsUmBonULharMk/Z" +
                "7nKStT2fZpLNajL2qs7TVaErGm+rLC8xfFqRbkCd/saAdnYygmM6k67qnBjaEIW0MtohEJ6dqGRFKjt+" +
                "jAnJ3tXaPqJHMyP1x8XFUYlZc4toDD61G9EafxPhhkSblGNolcypA343pkd3qGgRYsEsLcX/A+L8YlPP" +
                "rTjdja5yPSk4QKakAaK6j0n7hy3KJZMudWkDeaHYrPEpZMtIFzI9mtOeFZDerWakQGSlivJC1kTWtMgp" +
                "Gqkin1QankSzZMlk7yV0LLmMd4R+audsmiONUdyp5yGX8W5wLrsfa/yIF7FLkMinGrm3cXd2kRgDaVOJ" +
                "ZdrtGB7fDxRZCQlU01d60GlqCmRpfPzwgSja7mgzJZuvP7DlQ3mttcicKXeaW9ieyWCZzyguN/HXZ2zk" +
                "AB+KHbRLBk0cadeK7uz1GNSTckiiJ8m0sLr+6gkHAM9Y610jTutlR6zWe5EmyVbyiS1mPK3s4n4j/ycG" +
                "a19fSGRslxaiU2+9vYigyN4lz5mpqTgosEXv2LAgtuvFYOzcjmJMS/J4RMnD71OL1oGB5Ym5wUvwrXSx" +
                "EjgMtikjmpKsRYLTODl8scogBRlKRaUOLRa3+ajZ3KPulu5J2Jp7M2LDKkw5oyrBv4rUWiY2YGV1Jok1" +
                "hmFwd8XEu6bJxIbJ7mx1x24+TNb6RNPqxIv+tqak/GBirY1UB6slrO8rRfQOk5mhOrYO61yFUVIiy+/O" +
                "l8jk8aBJ0UEH/9y0AkMwTarBZ3nZKV5Vf5l17uqul28tUXbc/T9bp2tlDx4o7tBxKCmij7rgS36nJqZe" +
                "A3LVa7sVIDiwTisDmKNTqiCSH9kmjmV+wQIm369oQlVC1spKDHgYIT0zO0SE7eBbj/8GLtmS6p2F0YhM" +
                "tplJE7O8oqkkw1BshZRE+CKvVWZJHwSLiMZCXxNJQ+UDZuvlsojWX/hNt5hyYIaz4YBgLumXRzHGBBdc" +
                "3uWpgnllvQDINJUXjqDN9LGHLeBZFqMtJCJB24dDgK+NXak1BKJfKl9VcpoNfHH1U1s7QHLwJLoKZVeP" +
                "WIkAW01RfhjTqLqNv23ib388yFY3NrZrt0v4acw/nT3H0++NgULJHxUo/LZ+IF9FAAlihVLaNdGvK8+k" +
                "stcGQrKJOapFS0PFKrKTLmdc+QMEEJgIvuqHNM9+3IOBVcbKQcCtnkC7CaAndlX3oTd535B8d6ttAelD" +
                "6iXpScprl8ArbdUqfAWoS9JlcsgCr1uNBjRy5vrGSG5bko9QzuLSs0kHCAQd0p1cKUv04XJc7FldU+ok" +
                "KqktitxxiTRBciWMosM3lGpo9kAKZVvth8MkzH8Rpp/zbKQc/2UcKY89Zaz7stAz0m4GyIjAQ2GIK3rG" +
                "KC6lCIRIKDEC2K4iy6sZw3EYnE4ZtZCjQQ915NAyEZ7fwqsL62pAyMWSqmREIca7AVBxVRVEndgM+OIg" +
                "8EMD0W0CxC1gyjsGA1xwVcmmNRqlFJRHo1YjZsJxWq2WmchKwZeZr1smd5hMrKWy0Y0h3L31AnYaYEtT" +
                "OrqA1KG2yFwM15lxaZVPpDMljScW3JmaixBbEXpn36ksY1hxAGQ2DwHiJK6ypWI5qMyNLVZ4T9Ghyh3c" +
                "LT0EN5mZ5iUDXAB3wtdtN+vWPQeVzkBgcThohsYotTX0yPHgI0L7cM9miiCp2DxARtvqMR68XWD228Mh" +
                "C3bayAJsU+akBVhbht4geygBdMQDlNVdLNQAWq57gG9o3dB5Duux1pxaozik/I/SPhsyKO2PkU6LLZvu" +
                "JgcPcaMwFUtEnIA1h35/ugHQUeB3LbyE4MN4geAGVoAPCViiiN7VMDPDBQ1knVldeGfm2oQn+fEkOyzM" +
                "LrGjuhiqn1COoDKRSsGHUJaitETQ70+vewFaiwFXGaieyVNvTHs7BTSZxbLeeGuE9kQa2duW7G5uV0Xm" +
                "NRf05PI/KNaTyGgNMJ2W13AxjgJorbl9EG0gstlDXpFpUnSFkg69L95Byv99FBvBGdvPdtdgu2mw2W4N" +
                "PEBA2U47JNW7LfgtMaDVUWddRBPLzIzqCTY8FBaZpX0lOlOKKHYdwjWpZJXWq4pjSbOeGLIU0aT61QK5" +
                "i/v1wT/RhnQbKh0Xonm35P4cF7UN7OA5M1M3/k9kG6x/TUGJg5RK51QlDNVLuMKtXhD/A/Q5C13qKgQL" +
                "zRb2w7uTlxzTjpHAD27JWOmvXqOhIJ0OS3Uvf/T4r9233OpDkCeRVdwO/Fxd974zDsCIQI0c+sZU8JeJ" +
                "Tq8hcIeH/4exhw1ja3S05p8cxsLw/6UwdlcUa7eYPqn3srujUTMw6X77idVEH0VfD4OQZNEdyI+4F4jX" +
                "AKQBBUyyM95wgCQY+qfBJCbWPEqt9RAg6Y4C3ztF7630AiatEpktRJOD3IZ6EJbFhgo0EY4d8DvbBGED" +
                "1mNEYFxwU6wn53NzvTSCpyj3+UPzepuLZE8iRRu/YBgvuHc2BT3G4tguyMokS+uRzzDP8Imq2gEfyQfU" +
                "sreLXsAwEh6jGFERskCW9BUlRMPRu6+gGSX6cN7GGQGRybEz5UHPg12x4dHCVClrim7qUWRM2EAiLCpy" +
                "wY3EQnJF4dRPaNySiY0Fp8qBeUr5Aif/whWHbcpsqVTDndM82g1OxrIf6GCJkNy9WoBGFIVI+8YTBThD" +
                "aqf6RLphaWH5dE9XdsWO4KkcxgN0XqM0KQJcteHVKlPIoaBvbvuFsX1EM4Djj51CZwbcjf0SYVPWJp/N" +
                "Y0robQbh/qm6Lu26bDoyPP4hfHLbF5/5ODuQs5Updxc9ag5FE/vM9hEJRCWL92J6BR6w9TAt2r03tPYZ" +
                "gX3vrM1Nl7DPm6URowBUnGjHdRlrJ3qP/Bwje89KrltFFhHhChRAJlBu2hE+2qK62JEug8/KtLzykQGe" +
                "0m8QcBJsdU2CAi5tQfKHZVJg0EWOU2eXcMQRPnnURfiE8rsZ1m9Uus73sSieVnpj3LxLFW9o7EI+7KSD" +
                "bw2J53AObAKKTDQqDBqIPppF6QZq4ltVPCwUgdKIJOFX4mm0Y1mWS83Cijts83aBqRCEV7pDSHxruHuW" +
                "Za5tRl7rGFXy2TUaM8NwG8cPIhu9ye3KUaFhbimpg/28lugsAWeYTDZUfDw7OXn6RcKgAt7QWQlnGIL4" +
                "ypu8siUu5ihUrhUKmANDxe+GQhO7AvfVanJm17OJPDuUld6dvjn/8fTp31mm5RJxChChjHIxqPCBlZmO" +
                "93r+XNbQp5dJQU7ahUbIi4vTtydPH/sg3Ky5ezleZUBRce0t3281HxgcYETYt1AnLlauxojCTGupAYFB" +
                "KJo5W0BX/rZZN5pmxpEmM2GRdXMMBs+X4XRKMi49omYLA234fF9B8eNBJdn7y3/U+fPXpy+ucM3kr0/2" +
                "f6CcF3/eROagybhkygnPBzIKY0CCOPFyRpALKsYFbsgBfMykLxlrc+mRkZ2gDdkpKq5NbHy1VxjxG5nf" +
                "oLsqGNSMIlapskkI9kQlEMwmbVZ8gmVw+vry/O1RahcBsf7y7M13SggQwosmTGE2OkDrmAqBOmilQeWS" +
                "1ENCGapTrhrycsemsx8xYrL2muqVazNSn/1zHxreH+2/QGVz8nx/oPYra2t6M6/r5ejoqLCpLkjb9f6/" +
                "PounrznYE7hc+sgou+erG2xOSwuoHPN6nyblKSPUa2P83aNpQa46yYs8oCyzy17RLRYlhmO3k+diG0wE" +
                "UsHv/coCNGFcK9ITQpy0HfhmE9oQXljulzKZkYoK4HdQAb3rq2D05T++fiIjkHrllJHGbXO871e6/P47" +
                "RdvmDJrEcZ86C1/+XrwKI4Q2L6X21zN3/JW8QUt+pL58cvyYH2l0hQE5ylw/gtL+miB/7zUqFAgSFgin" +
                "C/J1YbNVge98sljb5X4waDLt+2qG3VUtEEcn4qYTe0sYcAlLG6h0Q6U1F20pOhHhopFHOZWJ/W8yqwDj" +
                "qcKZhBKAiCHgI6WzJ0rh/MWA/iP8jRtyX6vn5z9TGpPfLy9enb47pdQijy9++e7s7cnpOwrl/sX529On" +
                "T4K3h/jEWQY8+VFSpYWQkFOide27ujK06T82I8IcnGyD/faE1rCRdFYAV/isRpQgqRrqug2Rar+Zsy/J" +
                "LfGmia8kOLMq6OHngfpFmmW/tnmGkhkwyRUbz1E/BgE2RflI6cNGt+OfqSJpnn6JusbTr8jiLZZE/54r" +
                "brRg2xE26WcZb0oPfFDhUCxyVzrLV2DBwxyxoGFnX8fvnp2c/XCJCqm1ZthkpokNltucohUxHW4/8L2D" +
                "UB5yq9Mv9avSVHAMVXPhoEN3/Or07NtXV+oAtP3DYSOTnI21NN7INO/Aq+AL6gC+cCjrIc6FdUQ6v448" +
                "tNa5axVcRAi6k+3z4GT3mi9sKc2A8InmN3V+3yfRVs0rhsBDcZl82dgQ6xTzATZh76vlwDeRP/dKTXqe" +
                "6PUXTaonPOrRxlO3BjeKwcD7CXHbIOCOS3YoRvu9L94tlAetu3Cs7dYFiaFKLjr38Ns3eFrjHkpAuV62" +
                "3ZJqn0LqcF+wLe5HrnHcfwoCtAyJp8Uq4CQCBKFrcb4q1+WMKohvWhHWX3Hlq5ew9M7/vYBzBCp23PsP" +
                "Cda48gS4aetpJT54+MZdmBGw1zUOw3kAc7ND53yWJZMeSFVBjB0qC2Ltu4YpuX/+/lj4NLdj7gM+CLeM" +
                "y3cercnRkxl4fN/g/9gk0Lfqc7T/PlfpH/RPpp4qRtRajZ6SgZvp+y9wWXoSH/+OxzQ+PsZjFh+PPzTX" +
                "mJ984HdJ8m9E7+UlTDUAAA==";
                
    }
}
