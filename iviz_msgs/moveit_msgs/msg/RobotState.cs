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
                "H4sIAAAAAAAAE+1aW28bNxZ+n19BNA+2W0Vp47ToapGHJHYaB02cJuklDQKBmqEk1qOhSlKW1cX+9/3O" +
                "OeTMSFaaFlgbWGCzi1ozJM/9zrmj3s5tUAsTgp4ZVbomatsEZZup8wsdrWuUnrhVVHFulHcTF1WIOpqB" +
                "skMz5LdLFyxtDMpNlY1B/eZsgz+6qVRtm4tQBNME58eLMAv3ntPiGwIh+8YMrijuqOdyLM51VAu9UXN9" +
                "adRiVUe7rI06OX+qtDcqLE1pp9ZUam682QL9gvZiXw8FHx9XbjreQfYoRl3OAaV0dW0D8ekmv5kSBBzq" +
                "vBadCm5hmAuFHa0Mjop8/kk+fs6n339Q+fS4hTxOkAnv01rPIN3KlhBuM1PruQFUD9BQQyhNYxR+APEE" +
                "P5po/NKbCFI05KkqO52qtY1z5Q3JIbYUOgbC54GEdUpqdSHWG2UXS+ejbqKCUiHWpqoJNXHTsjpxlTXg" +
                "PdODjY1j+GVttN+3GYigcA0sl8bG0ah03oxGr0k+IvyJAT6jVstKeLVRiI89kzsqJs7VIHZMzBXFw//y" +
                "v+LFm+9Gar8B9iSlWxdg85u7ugoKZGuSQGVC6e3EsBDYgoTxYCL9iM7/vhLf8VA55CMOMFTFHUbRO2Qg" +
                "QFlWh95cunpF771aehvI3cojoqYyU9uQnDcjAFCfb7kZo5ybFoquCMDiaNBtvTS1K23cXN96L/Dme+GI" +
                "3bM7YqbQVRTuSR7LZU0+Zps+gJcLOv3yaMiMnXa84MSqsZACWVtlmigeOtlwPGj0wiRBzI2uyFCTEweG" +
                "Hu2CjAu+YMt5Dx9LLag1/BwGD/OqTDVUj+r62h5AB6UOzpPVyMFD3CgfJRSsQZDDOIdJP9sBMNgQQ7Ls" +
                "HHy093oTBoyBfIjVuNQ+bkuYiSG1M68zp+vkzAt9YeRQ2g/eycLckjSq66H6eW4aZYazodq4lc8hlLlo" +
                "HAAm/egQoFlNESG5klkM6IgqdaPgqZemr06mW5nFMm6SNZL0hBvRbY/3MHerukqSy3IK9g/EerAMQQqc" +
                "ntfQLtdA52tgAZutDbRk9oRDVtASDUF76CUCmGhwWBTFMzEOsZGiCNEjaiCisv1Ma6fjNw/wmB2h9yob" +
                "fO+VSODGAkqsJJoIzWADEaWptK8gzqg5cnCwtTNE07u1AYXE6WIJzUlc2SyJ606YM8Rur2tIbxUk95Ru" +
                "sYBIS5Yj7HXrvJi8Ziu05apGjC4d7Nw2tH3qNds3bQsGSmlKo85ORmzgplxFe8m+2pTe6EDB+exEFSvo" +
                "6fg+HSjuvF27u5SAZpScMvI2Ppgr5KUQODFRjPpcmBsCNkVbYIF1H/K7MR4RboAEJJilgxMcgvJXmzhP" +
                "KfVSe6snNWe+UnMEPaBDB0c9yA2DbnTjMniB2OH4K2CbFi7xdLfNhWE1gwCxcendpa0kdrGdIgzCeGs7" +
                "8dpvCg5VjLK489RzICH1sUYobG67ZzJh0cbYVreR3q4XQWD2tSF1gRGdM4hkJDLRFGrYM9uAV5mZN4bD" +
                "4BQ/KocoAzhT5De3zsUDuFuVceU5s3X4JKyexSSQ1YKsmexG52xBdhs2IZqFxIGwZIXC5OEXXjeBik85" +
                "MzOxy0YAq2uXsF8gRXLKVOUcNetQPaXAfAXV1IhYcIxaN9rn1KU53v34+uQpZ9hjKicPrxA68X+9JoOg" +
                "fAjbCUYWKZ5SzOsZep86EST+eAsocpbyy9Y6oMqODA2Ge2k8mcdElxfE8BYN/0+qt5tU1x5xcf6Xk2re" +
                "/r+UVD+WU6UdouOhmBm0ENFvJIC8zSaMXa05X9u0hkJpA/3dWfuZxYRFkddNBb2PUJ0l6XPIS+7QhpWJ" +
                "iWsDu4hrdy1jsv4o4MGZdAlbLn6CPJ0/lvO1ePUPKxzwDQUA7ySk3g6TiZg9LGqUQLS2Q79qAzFb1MJQ" +
                "Ewibak9yU0k2Ax6G5GCee7YB9WqVgzzQBHIUg6tRlmH3p3C8ycFQZEKvceSQnG1ATW0juyhVcK3C1Q1i" +
                "tbczW+2GUQ78ibmBitP7MGm4FNMsyKBCAMnSPhqqsyk76JoYYufOzdrEtHRx8o/ODaiiSiC2BfqKnSj7" +
                "qm2QknQFracyUl21v9rSUv1xK6rubGyfthGWvW3T+ZbO6en3zkBJyJ9kKP9a35KvctBIbOUEG7qudZuf" +
                "iXcXhphkEws0jKGBBKVc3cy48KWkgWCXfTVt6Z7TvtvhTsLfHq1BFaKejrkBnArEc+ohBinl/jUWGVj3" +
                "KDOIm2JQJjzC3kcGXyk977wVP570Rkecq9Ab2as8JyGn5ZRJU7ZcMdNvzk4FOhFabCeTPIhC1YkyIMw1" +
                "eigWE3pCwxLm9Wu0SVzoz/VoGyO8g0iiS/EjUhfxyiAblyaCQwQsGs6ZiEKPwlKa5t3ZBy/P9qRQa9lo" +
                "BSEIqmJXUAK0yPMimSzx9DQVlv35W55U0jxUU0WeaHArNjwgrsxUo85Sd1vChAwqyWu0fNVGqjIUBUJp" +
                "OtAVCAxsLPNbyiJTVaJyhQoSVVxAosYuZUq01YhCG9wWiD4o+wiTnHkWBKNlBaBT0kBQMxA7+nbJZGXt" +
                "uDHV3q3YERKUo0EehzGOxpQUvv2GsXlTSz9LHR4VUoKY1AeYeWiMBPEbI9n0RoJv25cQBlE3TiiyUtYG" +
                "zXxbnO4oAxlzqi4at266aMr7b8Mnr/vio1TxDWSYMOXKIE2Tc/vGPrNbIAqrsPjEZhLgIVsPw4L2XgD3" +
                "WTzKzkqNrZzLet4sjRgFZeWJDtwhsnRa75G/Y+ojZg3Pc4QXYeEtQSAwGXI3pk/RlvqcPYV79lk5Zn2K" +
                "DOQpu4NzLsd7twlZAG9cDf4zmpJmswtLA5NQcMQROnnXq7xEY6lu226REbbWxyJ4YHphwnwbKr3B3oUs" +
                "7IVDax2Ix+QcpARqd2mAbyj5p2jWcjdQk3SFw9tyOypFBJhfiadBY1VlpXtiwR31aXtFR4kRxvQRJmmt" +
                "o+5RVYW+GSWp066Gxy50YcHFXG8TbPTSulVA2WeuUCkQ+TZKdJaAMywmG5Tsj05OHn5Z8HiDvGEL09S7" +
                "hUxCm0vrXbOgYpd6aE+t1KFBG75BaGJX4PumCGcOOzZhqyPB9Pr0xflPpw+/Yp6WS4pTVLM2LV883kiB" +
                "lYkOeXT+57zmGlsOZT6hhY7JV69OX548vJ+CcIdzPzrGMkBUXCfLT6rmYv+QdmS95Y51sQqRdtRmGqUb" +
                "pWkIollwNckKos0Ro4umlQmQZCUksmyOicDzpfFtSQ+YeKQCNG90efmmguKng0px52//U+ePn58+eUsT" +
                "0r9/OP0j4Tz588tVDpo8IZlywkuBDGGMZlLUrQYjMxSqGKFC42kMMpP7unZKIHdHsBO6ntsqKi5MeyHU" +
                "xzDiN3K+mzP5bFAzRKxGVZMc7AElA6wmfVJSguUx2fM35y/vlW6RZ2fvHr34XgmAoXrUmjDCbOsAvRaT" +
                "AnWWSjcflKSeE8pQnXLVYJs9Smc/4tmNcxeoVy7MSH32rwOS8MHo4AlVNiePDwbqwDsX8WYe43J07x7a" +
                "D11D2vHg358Ji54rpsbJ4K5JkVG0l6obUk5PClQ52niAQ7bkZvnCmDQ2n9Zw1YmtbZ73mH32SreoIsTc" +
                "Mp88FttgIMQV+X3CLCMvMq4V5EQhTgagPJSngWhilu8RGcxItQLgdyQCvNsVwejrf3z7QHZQ6pUJAfZd" +
                "p/ggYXrzw/cKaguGLk9bPW0hfvN7/SzvENiMSh2sZ+H4G3lDV9Uj9fWD4/v8iN2eNlgqc9MOpP2189XO" +
                "a6pQiJGMIN+6y+rCVaua1nkqEN3yIBs0TPumxvIfqxZA0Ym46cRdoQdckqUNVLlBac1FW0kz0TRYzF2O" +
                "N+29MMwqDxRR4UxyCQBgFPAppbMnSuH85QD/GxZ8ufOtenz+C9KY/H7z6tnp61OkFnl88u77s5cnp68R" +
                "ytOL85enDx9kb8/xibMM0ZR2SZWWQ4JFog35Y5Bua3cv1+3IZ2gqReT3D/S2jWTGS+0Kf8MgQpBUTeK6" +
                "ypHqoDtzIMmtSKZJq2CcSZXu4ZeBeidj+1/7NJOQuWEyzSy2c+XdGERtU8sfhD7sZDv+BRVJ9/SulTU9" +
                "/UpZvEeSyD9RxcMuUjuFTfxNFwCBqh8JKhyKhW+vK7siElKbIxY03NLr+PWjk7Mf31CF1MOZlcwwScFy" +
                "ESlSEdPh8QPPDHN5yJcuCdWvSqPgGKpuWLgFd/zs9Oy7Z2/VIcFOD0cdT/LNSE/iHU/zrfYq+4I6JF84" +
                "EnwU5zIe4S7hkYceno9hoSFilp2oLzUn+3E+cY0MA/ISznd1/q5P0gWP9dwCD8Vl7LKzIZYpnadmk+x9" +
                "tRyk66wvklCLHU9M8mtNaod5qkc7T722uRMMbbyZEHe9CeDm01+7b6RidHf2xdqi8kDW5esWknZvuDlU" +
                "hQxp21v/3vS9t++2GLRNO7ncGkn1v87RouNtdj8xgr35FEStZU48PVKpnaQAge5anM9b3cxQQfyzF2Ev" +
                "db0y1H9N6WsA1/vkDzzSjSaKnfD+Q0E43iYAfH2UYBUpeKTBXT6Re68L+kiMNzA1e2TO33jIoVsSVWZj" +
                "j8gyWwehI0o+nXh/LHSaqzHPAW+FWu7L917yyyW4GaT+vuv/2yGBvlJf0PjvC1X+gf9U6qHijlqr0UMY" +
                "uJm+//IDTRTbx6/osWwf79Nj1T4ef2ivGt4/+MDvbkoAn5jh7cy19t577hzJhsbOe2OK+wTdOcLwdwDd" +
                "3hRRuit+Y7ntax3x/SDfn2AVD7osTZ267fCBtOS2d8v3UR/amXkPl6Qyc0XfCdEcItWh7VgkRQPKfnnq" +
                "QNNB+h7AU+uyfT3NMWKHyyFYL/Z80hWuf9NFH5p2L7fYuv61V7XK4wfk/jHNgOiLX/oM7D9cKJvv6SwA" +
                "AA==";
                
    }
}
