/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionAction : IDeserializable<ObjectRecognitionAction>,
		IAction<ObjectRecognitionActionGoal, ObjectRecognitionActionFeedback, ObjectRecognitionActionResult>
    {
        [DataMember (Name = "action_goal")] public ObjectRecognitionActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ObjectRecognitionActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ObjectRecognitionActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionAction()
        {
            ActionGoal = new ObjectRecognitionActionGoal();
            ActionResult = new ObjectRecognitionActionResult();
            ActionFeedback = new ObjectRecognitionActionFeedback();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionAction(ObjectRecognitionActionGoal ActionGoal, ObjectRecognitionActionResult ActionResult, ObjectRecognitionActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal ObjectRecognitionAction(ref Buffer b)
        {
            ActionGoal = new ObjectRecognitionActionGoal(ref b);
            ActionResult = new ObjectRecognitionActionResult(ref b);
            ActionFeedback = new ObjectRecognitionActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectRecognitionAction(ref b);
        
        ObjectRecognitionAction IDeserializable<ObjectRecognitionAction>.RosDeserialize(ref Buffer b) => new ObjectRecognitionAction(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7d8979a0cf97e5078553ee3efee047d2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACs1abVMbRxL+vr9iynwAUgLHxuEcrlxXGOSYFAYC5O5yrhQ12h1JG1Y78syuJTl1//2e" +
                "7p5ZrYSwiW3wqfwirWZ6+r2f7tFp7w+TVucmtYMyr3Jb7qf0709WF0rz26sB3ienq9edG18XVVzp+NNt" +
                "a18Zk/V0eh1X98Pn5MVXfiVvLn7aU5a5AE8NG1cjP/CPPyJx8trozDg15P8S4bPIe7KRVhwdKlLHVZ7d" +
                "lJJ1xsq6H4F8lQkjwmWypi4qXWbaZWpkKp3pSqu+Bff5YGjcVmHemwKb9GhsMsXfVrOx8dvYeDnMvcKf" +
                "gSmN00UxU7XHosqq1I5GdZmnujKqykdmYT925qXSaqxdlad1oR3WW5flJS3vOz0yRB1/vHlXmzI16uhw" +
                "D2tKb9K6ysHQDBRSZ7TPywG+VEmdl9XOU9qQrF1O7BY+mgFs0ByuqqGuiFkzHcPBiE/t93DGdyLcNmhD" +
                "ObBDmXm1wc+u8NFvKhwCFszYpkO1Ac7PZtXQliBo1Hvtct0rDBFOoQFQXadN65stysT2nip1aSN5oTg/" +
                "4y5ky4YuybQ1hM0Kkt7XAygQC8fOvs8zLO3NmEha5KasFBzPaTdLaJccmay9Ih1jEXaxRfC/9t6mOQyQ" +
                "qUleDRNfOaLO1iA/vSdvXBkc7FqBWeWHti4yfLCOWBZ/UrDlZJjDICwEhYuaaK8cOYyHEORAR2xvdkmo" +
                "RJfhMBjZvYdrTIamVHmlIKjx5LTwCzMaIw0VBXYTTS9eMzE4uiGtegbxARZUalylYTniqK3fwH+eRZtA" +
                "vWAPZrFzPauYuMBZhh2S9RCD3uuBYSMoPzZp3s9TETBw4LcDdQoQWQCmRrWvwJlC1GHVdrQfWe5bZ0bO" +
                "iUnP2oKMceVsnvQLqxGvb39X/byojLsq8lFe+W/NabsafTqLI29WNYKZ/7uZyENRC9XsfiS7latkqcxQ" +
                "fnweWZUPZ92Tw6OTn1R8vVDf419xXfa3IQJqZhAglhwLrpxK3gz5ZSGaAs39g8ujf3ZVi+aTRZqU0Grn" +
                "kJiQw3uGXPROhM/Ou903Z5fdw4bw00XCMK5BZUBWR8ZEdm3CRek+HIwCHdI7il8z5TJSDpI5ozdfa/iL" +
                "yGQtSL5GURsXhijAVSMVMLpxadwIxaugSlqZzcDyxa8HB93uYYvlnUWWKWHpdJijwiK/1SlpoV9TGV2l" +
                "iNuO2X95ej7XCx3zbMUxPcuiZzVnhTnvK0/KavNJ1ZBXeIuU19d5USMd3sLeeffn7kGLvxfqh5vsOUOx" +
                "c4sHcCq0dbXsLp1P89gzqUbKYZrNYTVgBqVtLrAAOnn5XhfI1bcIEDyviZQXavcBPK9xvdJWHIRz52uM" +
                "12j4YP/4eB7JL9Tf7spgqGSrOLyLdmGTm9ZaZLrs525EmJDqaGMGhjXEickWhGi7yfOvIMTd1ExOsRB+" +
                "cgChrlt84vj04rJN6oX6kQnuNxgjgC9QUhmsRkSMKEE3KiAqVMvxNoAc0lvvDrHnibYlbZNKJznEX4Fw" +
                "gEP2i8JOGM7TQoSCW8QgGjrjjMBwo1XUaEtmevVgQGoMiyozrb49nAjlObltQ1j6wWSydd85QC/XPL2S" +
                "jQ8NNlaylax9zku97u4fds/VZ22W1xK6ic1cRJ/U4sH+PnV5j9Hr2FbwuRyO61N0exLZg9ppEhA4BC4H" +
                "j+wH6QNuJu9Hu1UwRiZAT1vvbDeAw2ipz1TTRXf//OD1F6kpQPnUbtlUgAuAwEgjHqaI1GpijIg2d6+G" +
                "6TnGRXfbbH7o8FnW6rfzuKBK8ThpLKEZaMWPuSlGYiJNjtEly7cddXJ6GZ4h7V6lha2zOB9Y9t+/LtLp" +
                "S0Im6ujk1an6IqkObEl4AqOQEmkTzkEpGIgrlEeOJaozQTo2DsUKzVlqPBfT0SCG8BS+ANwhHeSmgFpG" +
                "mMNUoTJQt0RlqY1rZORi0iE3fmjoLS+mlfT9iCDO0nqkdouVWMZ9p5RRWhttxCwzI2aRx48n6UvaRntB" +
                "hxNERh6/p4aoQZ4w4szWSkutzkOOEIqsH2IbQxIc6pQtzbyFj4H2PS970igTDhEqGpY3RFELTYWSGwNQ" +
                "zVn5634SveTg+NeLy+75xed6ShLMy/Mo1oMkx8qhZDPTO5m4edBLzxSWSq4EhWipQ3MztgnFgI8Wsi5H" +
                "labUbEpvncdRG6IMQgRc/bScKb401O+Ri9F1uPmezUTeiDnPiJEDCjdKX63o8wtydBpB0IPO4rADfg3f" +
                "LzHOG+DN9LEf6jFiGedh3FJCztbkMMgzj/mEVwsXb4wfNqSucN5wxel8Mkv0FH1ZOUbQeRv5Wqf2mweT" +
                "A2BHLmMerNC3f9C8ZICiBTyJ+LW161CkEO/TEAWFxhIWHvAI0YT4w8TtWiacbIbcYQX5nq4rS4FPw7pZ" +
                "MjDEo5u1lAk1NpKE8z6zrKmz04vuV8ha0QRSvXhUwY5HfjHr2WxGpZtr+d7cRrCXNH2cvUmppUBHWsHa" +
                "l+EUplDYSKHudJbr8jH0YdgzqQHzNQZCy0ry5l8gdGB59olYvQgjYzr322BOSmdfUlgO77+s3KgipH4y" +
                "VDVBe25gA6zAnF2L/uG4cHnKrzImRGBkpk+tiS5xYkjywf2vzWxVDRBvkP0YLxIqRKWiNlr6O/aJrBd7" +
                "BlCJBLNemxUe6NIQHhL8fHF68pg63DCZ/23/zXHoTLapqQpFAk16LFzAYNeUxDAVb+YDXBIaQEF9yxqP" +
                "t3vam23V3R5scwK9aXQ8FrBaWHuNGL9GzXr05zppeH1v/cDW6fDw5XpHrTtrKzwZVtV47/HjwiLcoe1q" +
                "/b+PREQqbcQepRSogWgjxYr1QhdGxmlpgUpZXq1jUw5gibxybUy4q+gXZpr38iKvZnLZYlb5KwQ2okS+" +
                "hcGs+PCl+EYDwZECswVMQc4lQzjEOKKsMHwT8goMBmHpo2Iye6pRAD8jFeDZsgr2fvjx+TNZkVpgFO5D" +
                "se4mx+vhpItfjgEMgEaGFp1vtNPCwRfvitdxhdDmo9T6ZOB3duXJGIOtPfXDs52n/JHQDS1AvraTsALt" +
                "7gTXS0uPS5iABIkHxLZQvh3ZrC7oe7CFsYsdr0eHhmvf183c6gq83JsRs2TvuZ4pTE+2MhTF0ovLCZLo" +
                "hDuSETWaXOaoE9NZxoagIUQrs2ACMiRHKulJgb10hQZ61Qy1u0o5r4OK1MN4a9QO4h4wiMPssrA9CiiP" +
                "+olCWzVtZJzuBVbgfQ2CeSR++UjQynZMGHKWwH4+EYJQFbZuoKXXQs3fyEfQyhZF7SZV8CeELzZqiIHG" +
                "wGSb2+psTsa39oJpggy020fKgDBZnTKrxCZcxGkIMKa7GAFKjZ5wFeQMkKfcMW7Z/la/IHgl3NNlEIST" +
                "TcK8Tt/VuaD/zhzCLl18UtXYoEzBMEnMuHmz21FPDyk/1mlFsDposaWubXUkz0R5dA0WFdIBFYaBOeUB" +
                "AdOTPIOEAQwUphzg0wqi8ZZVCMRPvJl4OmzmBXwybihLgyu0IGruokOEShH8hXVDTrOdsKFekSvwxRCn" +
                "Krk3wiv3V70cOY7gBKZgR751f9l88Y/IVACtMNKYh2bHjVA6iAQuerPK+GaHs5O4fnkHvlpc/xwM8snz" +
                "sRzujgBrWvHRwbXBBy4IG5H0d6K5zSAVREL9RFpuiFw6KqqseUrr+ItiEgfU4g0Pln3YEKuTD5lPwnoc" +
                "8w+hYJEdgQ2zipGxvZXJGiqSdcSfngPLXj4Pl0Xhya/hEW555mue7MolSmsNPcJ9x3wN2ZGuGFpr6BFm" +
                "9uHJq+PTfXqEIXn7ye4zmjgnMcdTbYgmOaH3EJD9MTqL7fdp4MULTuV939mRTKAZc7EqJETDQewUXJmx" +
                "6TC+N2VNSUaygjcweg+D3XhOCuDFx2DLazjhSJfA5YUZcfaM3SBzdl9usdiPcZgDMbaGGLExK3JqhuC7" +
                "hN8HwBR/D11BZqb4RUNBwwln+tKwxzkIC4GOA62T8W9/T+iMy0AAMdbQogPCXQNFWdwhIIexYE2wyQg3" +
                "qxuwuOmBVBXFWKGyKBZwX8OUWPztjvBppldQ3H1yu0JHMdgDUvArJlZN7uzjxxL4+YHGUIXnLIigafNu" +
                "1rz78FDs39I+RpHir354JoJxF8o+3RRxU8sNrBbf5PnujaLMJSz+kihZqsY3z77XnvVTgkeJnSGJOU/I" +
                "3GPRaiJ1XYbfsFCPwZIE3tfUuZ1sjfQfgCINJQGKARjsTnehqEZkmYnHPgU/qYnLW80OjSDRruZTk23p" +
                "aZtHXsoDP9AnnNIR32s1So57g40pxjcd9aGDmly1W+J/K6J44/Fvqx//hx9vRjd9u7NLU/pGhQ9mOki0" +
                "v0K/N83V4QtgPM7C9xKT5JgtZW8rwVDNguSXGl7sSqY7X/cwAs7PXuWTCwwt+SY+vZszTmgB3vnxNBPf" +
                "Tb75/eT+ws9Uv/hnRM3vXf9ffujaSJb8D+Gl6wf2KwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
