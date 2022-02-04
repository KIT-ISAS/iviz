/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceAction : IDeserializable<PlaceAction>,
		IAction<PlaceActionGoal, PlaceActionFeedback, PlaceActionResult>
    {
        [DataMember (Name = "action_goal")] public PlaceActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public PlaceActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public PlaceActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public PlaceAction()
        {
            ActionGoal = new PlaceActionGoal();
            ActionResult = new PlaceActionResult();
            ActionFeedback = new PlaceActionFeedback();
        }
        
        /// Explicit constructor.
        public PlaceAction(PlaceActionGoal ActionGoal, PlaceActionResult ActionResult, PlaceActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        public PlaceAction(ref ReadBuffer b)
        {
            ActionGoal = new PlaceActionGoal(ref b);
            ActionResult = new PlaceActionResult(ref b);
            ActionFeedback = new PlaceActionFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlaceAction(ref b);
        
        public PlaceAction RosDeserialize(ref ReadBuffer b) => new PlaceAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "75889272b1933a2dc1af2de3bb645a64";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09aXPbRrLf+StQcdWztKHoQ0420a62SpZoR1lJ1Eqyc7hcKBAYkliBAINDFLP1/vvr" +
                "c2bAw3J2V8x7tU9JWQIwR3dPT0+fwGUWxeYortMif1tEWRDRn+EY/u5cumdXpmqyWp+WdOU/f2NMMozi" +
                "W20xkuvO4b/5p3N+/fYgmBZ3Jq3DaTWunl22Meh8Z6LElMGEfnUYniwdcmNscXoSIHphmjAGhDch/DjA" +
                "VnXCkzNknSfBdR3lSVQmwdTUURLVUTAqAOJ0PDHlXmbuTAadounMJAE9rRczU/Wg480krQL4f2xyU0ZZ" +
                "tgiaChrVRRAX02mTp3FUm6BOp6bVH3qmeRAFs6is07jJohLaF2WS5th8VEZTg6PD/5X5pTF5bILTkwNo" +
                "k1cmbuoUAFrACHFpoirNx/Aw6DRpXu+/xA6dJzfzYg8uzRjobicP6klUI7DmfgYMg3BG1QHM8QdGrgdj" +
                "A3EMzJJUwQ7dC+Gy2g1gEgDBzIp4EuwA5JeLelLkMKAJ7qIyjYaZwYFjoACM+hQ7Pd31Rs5p6DzKCx2e" +
                "R3RzfM6wuR0XcdqbwJpliH3VjIGA0HBWFndpAk2HCxokzlKT1wEwWxmViw724ik7T94gjaER9KIVgd9R" +
                "VRVxCguQBPO0nnSqusTRaTWQNx+JG9duCGItATaoJkWTJXBRlIbwIkRgLeeTFBaEkMDtEsyjKiiRYSpA" +
                "AhnolNabWBJIEuUyGSxyeQesMZ+YPEjrABA1FTIt8IWZzkCsZBn0xjEr5pq5gant0MHQjBCWKIhNWUew" +
                "cgiRT1+BP010TYC8AN4CJ7F0DkZWSOUJ9GApBnuwqqKxoUUIqpmJ01EaM4ICQdWT0XGDcAMAatpUNUAW" +
                "wK6DVj1dP1y5bUk9knfwI1OPy6KZhTnwj7sX1XUUT0wSFsO/m7jWp9T9rABxAevz4WMww+swkxsVDjos" +
                "ikzuGzPypqma2awo67BqyhE+lSGlB6xlMQ/HZTqbmTLUtnGRZWkFQ0OzY5igLmEZa9hCUT0JY3fDmwam" +
                "zkHK0VawdwFUmgAQqosmngha2G+UFVH99Sv7nPpDl5CYhVCm68GMUAzs82ImOG9p1ZTswE1HARKPpD9v" +
                "qzKqZj4nCV+DbC1zYMhZUdUN7IRiRBsTRZJ0NtL3CbZJGUN8akbwHAgdQS/cmB0gNFKsKBcM0vcFjH5j" +
                "b9IUIS+7zKZQ6MA6u8mTPRie+rWhgI2TRXhs0P6DqxHsZxTAJNzwnNnRwyHKYJtW3rYyWWVAUJSmG+RF" +
                "jecWUUZ26W5nbAo4NRX6S5Dw13LUWaAtxNEMxAGwP6wBEfwtcyVgm1cZrQGKIMFWG2vnEmYxAOQn+jpS" +
                "SWPsC2KPOQrWC3i+RnIVQ5BTcWaseIOlA+pUZhrlcCIDlkC/KY8aDYumpnESbh3DiEMkZYMb+dmsqfAX" +
                "slXC5IGTp2jKitiC6A/c8+B+eRxmf4C7lpQzC+PfsRkJkqqz1OUSL1FCFSwffhe4CQhYkz4yU+1vFrhv" +
                "mRdUHThDAD27BT90A9Cd4Pip4SlcRHFsMtDc6OHHjzBi0W7N2/Wj3U7eXLCVgAvMPWpkfNweZZm3ce6i" +
                "rDG80YUrKtxsoOYBRFFFd4jOJHMqbLSEZQ9Qt3KUSC6AefccOt7NFlrefcamkzT8iI7tcFQW0xC2Azx4" +
                "pMXcKCJI4uI1H/ZOLi1rw7z5VJNd4loagMXMtuAnwEuDejRomJFKYQQC13EEehRwAgiiLhoCeDuR5yyx" +
                "EZ2iTLVvL+gQR9sGnb81ER4xNK5rty0EeXORcQO6AGp4zK7+iRPJZmuhaw/9e/vXwv7163bAd6RTHOxC" +
                "VXQYOHq2gcerXxzd8QTodR7ASP+ab0NbWT3yAMPEjNKc9OvaOwrd8U9dumwEAIKzNL5tZiTm8KAM6qi6" +
                "rWAY7GDuYXNlrP5k6agmfRUIRkcUrDkuek1WGTTQ4xkbkWSkByMZGE88NNTgQZKWJvb1FA/OJe3hPcm9" +
                "fVUgbE87lKngVtLCNIEjPcqV9cAClkahfSCdp6lrzAe5Wgyo74KNgiOP0KJGRMTEgY5oCZEShabzHVhM" +
                "oMKjlQTiS6eEob3pHocRPkWpdYweSJN/SbTqGHf0e6uorcWJ4Whv215gLV2woxegmYIOh6ez7QkdLSuR" +
                "6VjS4nbR9k0K2Dug2MIY0+gWVcC8YjV5NoPB2rsKbkOXHdMb97psPVMr2icIBbmBQH0s03EqXOr0SFK9" +
                "BbluUI9ewsqAwkAw82TMbGXBwmm3F5yOgkXRgOEMOMAfpXifSPFQuGjb1UVBW1z5dVWeW7MaZHkNi/ug" +
                "ZHucpfbFmWd36mIrkPbUiazKzmq/tRIDz0hV3cu7Zfd2BbSsSB9D0RSxH4Ep6G170NXQuhNDjw1oUsYc" +
                "jFYrblnHl3IittrpMdluOnDnTqu1dx61O7wHA32YZmm9aLW/s7fbzR9/wZYoAlS3F2vUA9ZtmdQog8jj" +
                "qQ4jsKnyROntrA2V1fRY+uIZY8i2qoIPdoo9eIoqLoi0cAg68Lzrpv/SewbW2535aPnaKlh6Y6nlmvs0" +
                "eoc0vrlJxxM6FkeRZ2WzmiRUCXYSA/LEVM7oTqfoc+FzpwgKMkl89g2OM9AQS3z4qykLkmBVANZpZbvW" +
                "u07VICC24RxZ4e2N25QPZ1BEWuqhLIdDFYRO+3xR074YFuinzW9XqEkHFzonlVmwleUV7Ax6SWVqciAj" +
                "GDQKnWy7zmqLyrGpRWEtvHZg+KN9ZoUKdFivksgQIQ0R8pQKwF2RNepMXgd50HmN7Azjv+eWrlFYmrHo" +
                "N/+7uGsL7NUmClDghDRZPOWZTLCg+ydMGlxgYKqqADGtpxucs7MynaZIBPHj0AnezDiCIKtSiIYc7ER4" +
                "nDSo2+92qkk0MwzHNQ56qSOh+Lajep421LCdx934UyMHkacWFLFVU9EfkJxhNOopCMMkSVUxcKN1cXNN" +
                "TKVeJudPQP0hoV9WQiKvCJ470RiWu4uOhVUUz2FIgINH/gRSMvWDCGE7xeVxGGXj+jg+GRb3XSAPuybj" +
                "BeznBA0dYH3yaJIzBkdhfJgJiHzk6gN009JREQZDmht07GJgrKQD6HkX/gOhhbG1b4LXgx8PX8jf15ff" +
                "9a/6hy/l8vins9OLk/7V4b7eGFz0D18pqTFeqNYPwSSt8H5HGyWgg+cVOYpbTZ37xrXQPriXEXy/g9fs" +
                "IDDoICNpgDqk2oo1+bATc6+eqaeuz1NAvowWOMMbEZ+AOIHapasfu8FPXbIafvZhjiQ4lZl8DAaHQBQX" +
                "Jejgs4KojOEaigXJQyB6z9E2/PHwuXf1k6U1Xv0MpPZBYvoLVKRC47KTryVHjV/CXgwn6OxjERJw1kRJ" +
                "2iAIomowB/Va6xpeHZ2cvrsGePw5dZFpTFxgjgMzVZh18FShmCUrdchJWUGIY5ufg+g+BYHsTJDWuOF3" +
                "/dO3390EOzi2XOw6nNA5OfIp7nCakNC2NJe9EOzgXtjl+VBZ03kYO5mHL7x5Ns2CponSjpcvqszmOY9x" +
                "QZBS+ghDIW2h6e1JVMfTkkLhHNyr05njIaIphVJgkZDfm1mXKRt8KUTtLO1EoZ9lqSXkgbm8nbrS2BEG" +
                "Gz66iEP5rILN06BQzCIDqgkEZ16Uj+Hk/pO3g8XNTNoGHSU23AR7+85ggoGpPnzs4Bw3MgDIEjuWqtyg" +
                "cjRgGmmP1QOVoFljWZITmjttiVSKxhqSKVpPKwcUZ0Z82Gc4zX0IhHtMaH0lZ63R91vU6La/8mFVWl1k" +
                "fi/ZR56W7bQKUoqc17OzyZfadkP/i2q7jcfh1t2jBQtMWaKcVbPLC0G6SPIQFMCmNuF9iB1D23i1xeLB" +
                "Fr8ut/hP1MHXuRhkcT18+TAdNeQ4meItNOGdH4JdW0laxRhHVnNrdyXHySY2EfdTe3SPVa3TJLIDL1jy" +
                "zycYVcHzICV1FRsX6GGzjI0+u6LEgc8FOFKMHXw4Gw41XATQtCnVZ80MrC5AigqXTcxTeN0RDBz9Ahb9" +
                "wAuMKH1ogIvBDQwO+MTkak6nzRTd5FPgMvxT3cMVHHX13Jjcg9wGl5V0GGoveVjQXXRUz5LlE1OUN9gj" +
                "GYWX7lIz9xQbpgpH2JYMebIudnDSGdAgGoL+xFlGu5RBU4m7rGpAe5w1Jan6PbvtW1oALSMdFpKnBAMo" +
                "j6BnLWV/unOssCHNg/i2iD/gnzRAyiuELCt61nJLmAsWaAIyxGCfuaHUJV4htqCUSuTsZs+EY0knwrp+" +
                "COQTWQ3WEcBpDW/KYuqRWzmzLuZRmVStNbUgM9NHyyzmcRbpkBRhAe6hlLSGEv2mUb5gHbpHaqmAK85o" +
                "bvOKG3QD4oOII3QgzOgwr5Z5mx0Q5GcLZgugUZrYHUqqhS6rJIZAw33i6wKVZs45RIBDmrS1njAvm5bt" +
                "nci861ikJ314gcC+lwmd+37p8IIBnMkqZCfRwJlVLvEtqiowjRO/HfJEVhS3EhBA13vdWiSUWUBjFy9Q" +
                "YtBgM44aojxsKN0TxQbIDFiBYVNbkx5OttU9hwosTg9IFdmd+ItSGBXouJndGG7Lbv07khxFM544fkJV" +
                "YmXHdddJMd0ToAgBH005z29o4qhxu2qN5kCzSBwJj+VavRAqY5jeACRNvRROhLYkmWSkW2ClaQEaLSYe" +
                "ANch43Qds8QRhzFWQBfyGRBaoJGCEU9RgKzgcM1zcYSw9UDzsV6Bsr9lZ5CyS49EEou8zDFSkylm3uyE" +
                "vQpoF82UWNRorQBQpuYtRC5kP5Il8Ug+GURjczDz3ob9aErM5qInXYLSbkoKHQEyz+XEnRcCD1tWQHqM" +
                "2M7IGDDVro6IRl5makwf1sNOZl4//GX67CXP4I8OcM0M6tAoQnd7vrBQUjPGmOcsyMox6C3Mihzgcdwp" +
                "EsmYIsCLGWaropUNtzFl7XmX4OM42XOcqVIx215/72gG6aAHEYAUYruQbQm7z7UXnYIVGKyAZLZg1cfv" +
                "QTs+Q4O2jaKaJqvnvB4OKzykzIUaqeSYFXmOeWFieC8fLr4uIAcbs53HdIgL+gSVfiWdkZZqonQskSdI" +
                "R+RsxR3VJhV192nlg+sL2yIg+4oS35fD/7Qu6pW3ys+PwWHwshv8BL9edIOf4Zca4tf9i+vBVfjz4dIN" +
                "5xySGz9aV5wITFqnVgLBf5xyv5Rg6/yMoxFXKHD4066HjYJUsUF/uVppuzZV95oe2ERdahfieOTSHjEt" +
                "R1k0ls1InEqno3gYOGWlNHVTUj6spM9RDBuHtbxKPsOK95jVzyXxQjohSwU2KzoP0Qv4G+CgfCzF+L+g" +
                "n4yajzkNlpI0sSOpC+IQDnaQwMJjFVg4qGyDil4ZM5WtgIACB+EB7WeR4ph3EcgSRFVTSmvK2r1LyyKf" +
                "ooePkMH5Qp7PR8duanbw3H0CGYcKC+ANyHBAXMVW3kyHwAyoIDOZqz/p7J5EycyoRj37eVedFRHm7Nvn" +
                "4nQlqkN30hmSBdj9aRyWWPsxSseYwsyKo4dqqLMKzrhqJJ9Gfis6TmQhfZo0XM2imT6CUoxahiLP2GJ+" +
                "+2r433Fhz+FMvcVegE4wCxcE4TLn3vxd0oWJJ8gVX+KxFwU5SDpi6dyYBE83GNZSr/ecT4/1mMGhYROe" +
                "fPoiTe7SaB1BlQgtkV1FIxPazRIiQh2HHwEHul/ByaeUAkGmRsIGsu1IeTKeeqcxFLUvaCCEZTpryOWd" +
                "J94WLQ3lltrznbalyZHYFVERtcUmj1nWoL7F2wC0JRjXBjpWuJSf51JTQawTCFvxoxZH0XE1BUKzIzXl" +
                "yOA8Ssk80qN63ZgYV5aqJSfEZYYEjojFNkU5CWBAJxdbti2y2/krSFbQ4thwgjWoTecK/77GP/l2yLeF" +
                "Pjqob3cDhszqfCbg+hWaJy5jUCN17XkWEOkl1B44apahRkB+l1Gws86UcDF4it0vmUQ3mksldtGHj8Eo" +
                "vTdJyOVZNtUKF5vQ1m1vC12AhQDq+84RPzjW++d022bl2/ahtMfNnGVsec4QvXxcdc7g6pIvABLybcqz" +
                "VvsqjtDDjq2v8U9tS/dJJxGbVXIsq64Hr71l7kSLLEBpmUaUvOmjNSO7iKKaaCMVGenprpqNCTyV/UfR" +
                "PS9Fnh51BjTZMfbFlCSuTeKhNEDQmrKtEvxQlKCyz/Ff8onQyctKIK7ofGJqFpw+I6kPCxR6sItq9UWT" +
                "erLMIFWBrEmDMKeTiEgrUT4efwO6nbPReb9SM+JtJNpnIEd7YNvVXqYUedHQgKAUKK4QQg6pOqLGurwr" +
                "3racKmW37ffcTWI1C2aCKZwYKRreJ4M3ZK85bz/GHVtDn2NbaOdNQd3DpBiFS5NZZl3h0WBHnS92sYj/" +
                "JdWGaLDb0f526zHTYb2CFsW53WfrYrbCTq6SFz0TWFyrijtXAmupKWlciuqwSHAD7Sg80JDcQaBaZyYq" +
                "1zXmGG4krHVwEIPmcHDgiWVJO25mCeMK5xMB36652gb3r2dAj1KR3QLEfpMiSyqboJqYKi5T8bYQBzHi" +
                "ksgDxtcvDe+dsqDqXt4AmMvbsbWv3IlSGDiZcKc0dxS9ofz1Mq1wu8W7vo9nuEAPffCH1jbTU01HiRJy" +
                "Ye52XVOpo1msNn1WUeNnoO7j9nRduJrGGcAzOORcCZgMcEFRgIvdHiHWd7iklVSqIreBzVnzDh0uSB7Q" +
                "ccqE4PheW2KvlADrfES1Sut1Yyw2AsULc2GW23SoBh0da7qMnBdD20i7qneUKpdwzp6sT1sAVmlVV8LZ" +
                "KnzonKm6NAN5AxF19Ly3KUzAUAo34krZubyZKfRMnaQ9hTkqGzvsBT+gooy52JwbLSKUsMgLNOh4fZbq" +
                "uunA61JeNTmDjWQD2eZ0PqLutxBuROoxNsvVj+qs0owiplOV/opJKSWVh9E43q6hcxzzVaQa2vKAK4t2" +
                "xCGNTIHmXJoYzSpewV5nJQZsC/iIf1Yrx1YLxxar5WFbECirxw5gdbVST8UyALlHFpdoYVksMePScDoR" +
                "lgMkxZSCHqMCdTkV1+zllCJdNx8z8nKcIKpt4jIFL6tFBWaAl8VUcazT6pvcZ2xqt/9JASxk9lsQSiSk" +
                "MEaF/rlW1CYirTcqVVhExGHvrk7ekEzbxwN85x6YFf6P5uqyqykgSg/Fne+/0cGHjgnJimzXFbC2n3ee" +
                "SAsdDTY0JUqAMMLifEC4BcP/i7HtirE51u1MPluMafP/S2JskxTzy5A32IOUymSNv6VGc1hQbIC/l579" +
                "QGSCh0yv7ZQ2WaiVkkshISdWbPBgXqxUbFVL9U8dW6blV9Z5qTtaUbQlJInagqBKpsopWO2ay2FZ3HJc" +
                "pyCJgQmUEUdEonxMMXrcbcAliqQ0cdfSbjvYMd+sWT/O0Vgq/q0MAE97FhEkX/FnoUiDuUtWl7dh526w" +
                "0USuLd21YUJr5dAmj9g14wqASNagQbguGWy1ylgyETV5G5NUKFVd4xQrsEmdkGeCYjOa8MnpSF3ktFzk" +
                "vsch80KM116a2NAhOm7U8Hyybjwb2qITzqJhCcETJJ1lQvGg6mgTI4gMfTmRfVNRjWo03T3XPb0vAuOK" +
                "C+sW3rOAMRjk6y9Bii5877br4CQrv3yCXQ3sEtbKLIaKTl6qEE5W0rj0pSc2eiYFwlRyifFchwqGabla" +
                "UiL39v0kGLgnPyt7v22Z8a5N86E5chPjGVUuaLbSZPzGK3FJycS4fPhyJPFvPPRGlcQgdMtvU5m3cqiX" +
                "FqOLcYjbvJjnv0Mcb3UvHslR2XWpWtbxoXovl2eszRFNE5vrwQTcIe7REvFzmPu03l197Y2uM1YzEFOg" +
                "ta95e0Qdu3vEg4gK2JizWSUflu7f4Ahc/KDJhupR0uIes/JCLg9gya5ISy1iKc369BTP8aUEuN5YQ/S5" +
                "RUGfW+NzzuU2G+twHiyteRJojRTZCehrMpiqK9LMYsfJR+pEt0k+tgaB88Ki3M9PwilaNULo0DX85ql8" +
                "I5L4zEF3lEiGmxMAq3HknsabpBHw6F1aNBXoiuY+xdeJaXyJgiqUojFcgK5zdHKCBRhoF1L+nz+ITbrx" +
                "oqcBGh8l6qA7BvObank1AblG63giIzieSJNdnumqfz5438dMf8BphqktZOXZNx+wXSiClYCu1MvzaVxt" +
                "5Jo6KZ6wCg7Jy8v+xQkWt5AQdnOun45m6XJskThfq8QQf8rT0XVTVV/roCn0SGo8mpEp1dIhrYC0KjGc" +
                "NJWUJAaRaLOPAA5mprS17EPKA0JlVRsW+vixhOLDQqXz5Df/BIPX3/ePb/Adir+9s/wgcY4/HQfQEjB8" +
                "tSQeeCLIQIxRAhuo+ZVh4xM1RlhCTp8fs2vZmldSyxih1d5WKm6N9V36MxzQHe7vDPRSGQqre/IgGaqw" +
                "h1FsnsjQB0UOWPIvfH89uHiG4V5xOvx0dH4W8ABgpFsWBjFrN4D3bgUU1EqV5fIwPVB6QZ+0hjRfs+i0" +
                "j2wiZ5bemoPgi388RQo/PXh6jJrNyeun3eBpWRQ13JnU9ezg2TOsaMyA2vXT//6CUeSweV6wxyPXuCWt" +
                "nmg39GohRwWuJnsKnVLOBro1Rl6sOcpgq3LGYK91Xrb4Nab3JiIR9V0RJ6+ZN+yrxXDfy8zsK0DmaoBO" +
                "KOLYc0Sv7URPkiBLLm8a5iCwBKB7SAK4t0yCg6++/eYVt8Cjl5OkoN0qxE9lpuu/nQWwbJVBP79dp9bE" +
                "179k32kLHpumCp7Ox9X+13wHoyoHwVev9l/SJbQusUGKaq60gGN/Dgbv0m3UUBARnUADRPx0WiRNhs8p" +
                "iaMuZk+VoYG1H78oic7OtR5M9vCZrpzB7oy2B3l0H3yJKvqXQfwr/JNQ9htlkBwcwuKY0Yfn+F6yob18" +
                "gZexvXyJl4m93P/o3hj26iPd27JvY+mdNM4D4LtN6QBfeRUNa2k9+8rVJ6pSrLSMJ2mmwX9suOzNc5E8" +
                "ffspDgOt/hwFk9KMDr+QLTFPb9NeWVS9ohw/q0df/KUe/flZ9Bdgw/gWBiKX3jUY9Gi4J0XcTO3qjiQL" +
                "3hf4K74s4cI2uAFbLzbhXescsRHf7dw4B7P1GW3D/F+bHCHiTMsQgQKgX9iX1nHGFbWz5iU1EcedGh0g" +
                "4+/SBC17fJq6zhtTNZ4EFewSLO+uFlNW17vyWtjWm/T82ZYx6OMzCxEXS64pwF9OcCB/OKy5yUZdektE" +
                "VhU2xcLPzGJicH6W9Q87CvW8hKtVPDVFEc7m0r2iZhallOHEFN7JX4DCR/m6GmnEA5XPePu6yQ2Qu9w9" +
                "Wxa0OrvnKtAM+cgawowKAEHhbQLD06D1VRqkYwJ2pAAPC1gh6EBulpdknLWgpeYcCRcqSm2PvACYQJJc" +
                "MnYo5cHRxYmvX1pGkwFCnwUwOr7ySFd++3uIOBD9+O00AbcOYJrEt5J3yolxieKgl1sA20tp6jzx3oJr" +
                "NnruNB3KvS3L97zZN+5oXtR2UKA8q89FAPOwHkRAkrUeH3wvB6vtcElZMnCQbK4m7IJTvVbdNLrhdbtT" +
                "m8T6kory6u3rI3nw2K/Yt/PZd+WV9q+x/Wto/4q2nk1JuWucN9dOalp24n74GKxNT7rxMvNIcvpvrHMO" +
                "Fzc+Ks8d6SErzxc/gLCjl5nKw0ezoz8xN3kX908orRAzY0HpYucrnCyU2A3tS2PWOxb94syC2q11y0ko" +
                "WF4+veppkmIWm7fL8QAZEN+Juw6B34No/zSxKKWN0qSw1gTOfOm6QzFVjCA9K+K4mcEhu4sHBqW00p0o" +
                "jxdKip3esH7WQ2UgzTQrjAcih0QWYQWKUy8LrRCU7m3JcWWoMB4ZFFOep7u2uhrLBA2qftwtLxL3smGu" +
                "Zmy0xkbQAKsuBcn6qzWDuCu/opDTC+jlIr0JBw0iThqfl2mtjFNhLvc3eIzjbtnuZ0v44yoPf7gEE0ma" +
                "isLYTcWv8JfvssgHWR4H6o2QdJa+piIFUfyML9DbeHrxNtAfMDbhXy9rYQJcu7AV5rOyiNnHIxZU6wMS" +
                "MubR8c3p+37gjfmiPSYqdOzAAOYeGvJnf87Al1f9/vnlTf/EDvyyPXBpYpPiqwEj9JzExn4hIohG+Dae" +
                "lBIzKJXFlcQEn/hBXwJyIlKBP1GiFZMJWYCurGHnxpTTFAU7pRPtajnau+Pjfv/EA3m/DTJ+o8O+0bBq" +
                "YqQC7u7FWkJsmubo9eDK0QWnebVmmmFRrqvnWD9T0pgHSWNzZ0dRmmF4bgN4V310qTr4DoOvVsErDR6h" +
                "GzjAegmX2KX7MIxazlz7kzU5RvTSXN9eLPUumxAQzrM75TD4egucZ1kP7QPchI757OJZCh8fnZ25nXwY" +
                "/PFzAZQU43UQfg51xXPbXq020PkoLW0+Xe1LAYLEJC0kfDb55t+AxOeRGZmitf14AkxF3sATZ4PrG3+o" +
                "w+BbGvDIflZHvjeETtwE/cVTdNVLUqGSAEdpVzoj3YafsfcoxlPcGbZp5mlp1n3UB05hMkCt8dNIaapL" +
                "zIrEV8+6mXeQUamOGTbjsftgRVCb+3p7X9CRo7fT4SB0H18KdIwKB78fKIzhb3jqZcx70X75iAE/dQF/" +
                "ThHzGo3REeBSIdwzzlrXctn2R3mWPsnjfeCm/WGbxyfUMmmk6g2r4tGrckdfIgOWwlckgInHT+lgusbX" +
                "mfH1m6PTs3dX/cNv8acjNy/Pji4uQKiE+LR/crinrU8v3h+dnZ6E54Ob08FFiO0O917KQ+9mKA2PQPiH" +
                "r38K+xfvT68GF+f9i5vw+Luji7f9w7196XY8uLi5GpzZuV7J/XcXR6/P+uHNIDz627vTq34oJd4w6NHh" +
                "3lfS6ub0HKYYvLs53PtaoVd14XDvj+SecAWxNg3XfiCL+Ulpd31zdHUTwr83fUAhPB6AbL0GpIACz9c0" +
                "eX86OIPf1+Hl0c130Pri+ubq6PTi5hrav1Bivh0cnS0P9tJ/9qlR9v2G3iPthGvzqrO0Om+vBu8uw4uj" +
                "c6Dyi6+WHy6NBE2+XmpyNXg9EBTh6R+XnsJp81cd/JulZxxD1affkm+FU6dbZH5zBQ1CAODi+s3g6jxU" +
                "Jtx7+cIyhRAL2KV//FfkReCH99AOmQIaKgU9WPFfeqZEE4Y5vXgzsM/oXZweG7TguhiEp38Nrwdn725o" +
                "nfYf7aV/KzVl3rd4Hspb4mzYenOHVla913G5ossbYkuf79kAWTs1XOu4UckUD9fa2I++LcN904DSyNd8" +
                "okc/dLMmD2rd51sk7XsPKKXJi26sHc781qT35Sz7XU2L5xauAsAbgrIEwarImsTQwU052t53Yapn7nM+" +
                "z9of8RHv/0Tei0UBEHnVqtyyo3kfFepaldJ24qiJNsMsKw1p+B8josF6G1KxN6zm7/NlqE8Bo2uyvKyo" +
                "oSmL+VHJHXzVZQG6Poy3sQK5nXFuMwkkEydyic4uTqKs2X5tHcOyIWfd+67TyhR56wNP/9o8bS7b+qeh" +
                "Nnhg9PO1/5QPxn77dtsfvbVQu08zUmnT/wD+eZNP1HcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
