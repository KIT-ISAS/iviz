/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceAction")]
    public sealed class PlaceAction : IDeserializable<PlaceAction>,
		IAction<PlaceActionGoal, PlaceActionFeedback, PlaceActionResult>
    {
        [DataMember (Name = "action_goal")] public PlaceActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public PlaceActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public PlaceActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceAction()
        {
            ActionGoal = new PlaceActionGoal();
            ActionResult = new PlaceActionResult();
            ActionFeedback = new PlaceActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceAction(PlaceActionGoal ActionGoal, PlaceActionResult ActionResult, PlaceActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PlaceAction(ref Buffer b)
        {
            ActionGoal = new PlaceActionGoal(ref b);
            ActionResult = new PlaceActionResult(ref b);
            ActionFeedback = new PlaceActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceAction(ref b);
        }
        
        PlaceAction IDeserializable<PlaceAction>.RosDeserialize(ref Buffer b)
        {
            return new PlaceAction(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "75889272b1933a2dc1af2de3bb645a64";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXPb1pX+zhn/B0w8s5YaWXZsJ03UamdkiXbUypIqyW7SjIcDkiCFmiQYAJTM7Ox/" +
                "3+c559wXgJTttDWzO10lYwnAfTn33PN+zgXOJ+kgOxjUeTF7WaSTJJU/e2P83TkPzy6yajGp3dNSruLn" +
                "L7Js2E8H71yLkV3f6+z/i3/udV5dvtxLpsVNlte9aTWuHrXWcK/zfZYOszK5ll8dBWmS97U1mxwfJVxh" +
                "Lx/qImTpvPPZ4K3qoU6vsN3r3E8u63Q2TMthMs3qdJjWaTIqAHQ+vs7Kh5PsJpugVzqdZ8NEntbLeVbt" +
                "ouPVdV4l+H+czbIynUyWyaJCo7pIBsV0upjlg7TOkjqfZo3+6JnPkjSZp2WdDxaTtET7ohzmMzYflek0" +
                "4+j4v8p+XmSzQZYcH+2hzazKBos6B0BLjDAos7TKZ2M8TDqLfFY/fcIOnftXt8VDXGZjoN5PntTXaU1g" +
                "s/dzkA3hTKs9zPE7XdwuxgZ2MswyrJItudfDZbWdYBKAkM2LwXWyBcjPl/V1McOAWXKTlnnan2QceAAM" +
                "YNQH7PRgOxqZYO8ls3RWuOF1xDDHpww78+NyTQ+vsWcTrr5ajIFANJyXxU0+RNP+UgYZTPJsViegtzIt" +
                "lx320ik7918Qx2iEXrIj+J1WVTHIsQHD5DavrztVXXJ02Q2S52cjyLVcQbK8whp066rrYjEZ4qIoCbWS" +
                "VILtvL3OsSeyDjJNcptWSUmaqbAO0tCxbLlQJbCSzmw27HN5A+q4vc5mSV4nWGtWkW5BGtl0DvkymaA3" +
                "x6yUcG4zTO2HTvoZWAQgJIOsrFNsHiGKUWzw50O3LcAwwMPOFAHViZNOgGyIHirOwIZVlY4z2YekmmeD" +
                "fJQPdIEGQbVro5NHtAGAmi6qGpAlYDy02nVbiFadzYk/FXz4sdnHZbGY92agonAvret0cJ0Ne0X/79mg" +
                "dk+l/0kBoYEt+ultMud1b2I3Kg7aL4qJ3c+yUTRNtZjPi7LuVYtyxKc2pPXAdha3vXGZz+dZ2XNtB8Vk" +
                "klcYGs0OMUFdYidrMFJaX+OhvxFNg6lnkHVkiHAXoMoEWFBdLAbXtiz2G02KtP7mmX8u/dGlJ/QiS5br" +
                "s7ksMfHPC72xwY1ziCfrHUBagZSpBpS5yrSax/Rk1A0hW85AlvOiqhfgh2Ik7EnZZJ0z63ufbXJdJJ9m" +
                "IzwHrlP0Int2gGsirSiXCtOfCox+5W/KFEQf9tZmc1C4gd3s2Wz4EMNLvyYUYJ8J1niTCRfiagSupiQW" +
                "KUeFs+W0RDoBs1YRc2WTKoO4KLOdZFagCQU12hmvbnfGWQH16aA/h6i/NJ3ngfYQp3MIBXAANoGAd14q" +
                "YWK1s4oAYi0QRLZa19h1LjFLBiA/0DegyhqzL4SfEhX2C2RfE11FH9JqMMm8kMPWATtVNk1nUM1YJfA3" +
                "1VHTfrGoZZyhth5gREgbIfls+Gi+qPiLdDVU9EAFFYuyErIQ/IN6PsYyn43eP0JfbVvNg/l3thNxUnVa" +
                "fc55STnF378d6AIGubZLkgqNFS5PwrB8oE+wQs+IP+0kMKWgimo8xUU6GGQTGHLy8O1bjFg0WyvTvvVM" +
                "Fc0FhgItZO9poKnqPZhMIva5SSdQXMLuRhsVWQ5WHyCC1uYdQbVIHliVs6S1zF2s3QtUwboBFt0Ly4lu" +
                "NpYV3dfVdIYLfSQqvDcqi2kPTIEHn20/7xQVKnp5Q3V/EFBt+1i50Nm2LdqVAUTebHAJCnuZ0biG2ako" +
                "BeMTDu7mCJYV6AFCaYfeAW8P7blKb66ogB1mfXcTrIP04Bp0/rLA8suZjBvabW6NxmXi9MA6oNmndBsr" +
                "IHg1AnVjxd4MeO//Wvq/ftnUCgL+/DL8dlEpx1htws+rnwP2qRN2Ox9ZlPvrdjMmzKoa5CKH2Sifield" +
                "R/ox2ATSZ0f9A6xxng/eLeYi9ag9kzqt3lUYhh2y92A0eGT8e5KParFjgTPRW9h5bn0tPhsaOJ3NRiIo" +
                "5cHIBqYapBuHB8O8RPfIeIngbJkUb0QMPnVWhe/ph8oq3Bo2VjqEnk8hP3Q34B9bo55/YJ2nWL27p9rd" +
                "ORO0g+G+cOQR/W0uxLwfdKSTJJYVHesbOFMw7elAwaBzU2LoMN2GSL2Jq7Xknlibf0rSujFu5PeGV7d+" +
                "WQpKk38hS50rDE97CaMV5h1Vtu+Jjp6gxLcsZYt36BwPC3AQbF6MMU3f0TpEKEIs6PkcgzV5C7fRZSvb" +
                "He/uqHstrYRbCIWEimBZlvkYvrH0DCamWOW2up2kHj3B5sCKEJh1MiW5slAptb2bHI+SZbGAZ4014I/S" +
                "IlRijTi4hPnqohBGd1S7Kty93w25XmN/PybiNiLXIr/U77eD0yuh1Bv06hR4N1K417o7myy65Zm8Ajor" +
                "sdMoo1KNNSgSI/6HDUffz9xA9bDFSAtAeoO54T6fm4JstHNas9n0LOigRutINzU7vIEH388neb1stL/x" +
                "txvNN7JnLZxw3/zVGoNBzV7FNoWRxEZdXAlOF0JDhvLgizi5LY+tL/UNZDA0WZX85Kd4iKe0fiHben2Y" +
                "x7c7Yfovo2dw726yt566vdXlbrRarrkvoxOwAziSCB6LikQwJrjhajgZWpKtYQapAtnivfIcZmFpOqhI" +
                "CvFWYgpODicwGzFSkfySlYXIsSqB+1r5rvV2sDwEiM0EUFYI/G5mVV0Nu6RhM9qOhNVC+jR1jXP/iz7C" +
                "D4j8vltBqCgxhjEdvbCVJxd2hplSZcC80aGMIlpuO/h0aTlGE7Vii6gdggP03rxoQYf1FooNgYgHqVWn" +
                "dADcFJOFizyvgzzpPCdFY/w32jI0QixjbObO/yoC24xMaaKF1HUkti01vmIKe/r0SLHDPQZdVQXktdN0" +
                "0LnzMp+CzG5cuEe0OezdOtqYwmzmZAtOecpQMtRsp7pO55kCcslBz91IlON+1CggR5s7hOezeGoSkcR0" +
                "YSOuupHxgBIzk1GPIRKHQ2URidK70XbIX9cY1YJRIeBAW2Iov7ycJLnYOrfSMXYcIY9qzRJfYUjAoSN/" +
                "YFE29UcXxHa2ls+X1rtrhyJS6Rfvd4AhDWIOluBqyBV643jmAjYcRpekdCAYlKAgVpyXAZEYjGiHUhbb" +
                "IC1FEz3ewX8QXUzHfZs8P/th/yv7+/L8++5Fd/+JXR7+eHJ8etS92H/qbpyddvefOWwzxehcIoHJWvF+" +
                "xzUawiqHccKQcqNpCPGEFq4POZrgxx2iZntJxiCayASalM6BZFui672LXj0IfR5g8WW65AwvTIhi4QLq" +
                "jlz9sJP8CFIDev4Ww0wki3TNZmO4IAbRoChhks+RJqSbiPSO5I7sIZC+G3Db+2H/cXT1o8c1r/4GVMcg" +
                "Kf4NKrGoue0SiZnRAbA0mcIJE35scgIaJx3mC4JgNodSkINDx+1dHBwdv74EPPGcbpNlTG6wpo4VK0o6" +
                "1C2S5lQDj5Q0KWThbPO3JH2fQywHj6Qxbu/77vHL76+SLY5tF9thTQxgjmKMhzVdi+j2ODdeSLbIC9s6" +
                "H602N4+uzubRi2ieu2ahp+Jwp9uXajB8/ZwwHtTbdI+YNGnKzYgnaZrnpWTPNRlY5/NAQ4JTSbpgk0jv" +
                "i/mOYjb50pDqmLSFTE9SrcWDuCJOXWkcEIOGm5ByFNJetkWmFIUtadB5RNB86WwMFf6HiIktGi1mhygU" +
                "n5sCeyMxjNwHIuJvO5zkygaAOPFjOfMbtscCnpLrsapWBZo1vqbEqrXTxrDlFrIOa25lD6oAl5ZU/PRU" +
                "Qc3e94C7zwtwbPCsdQR/lVXdDGh+3LJ2AbS4lzFUZHQHC0MMpBAWbW3zHdHqf9aK9yk88vBD2bMkK0sK" +
                "XOeIRVnLkH/uwxhEZqb3vseOPd94tcXyoy1+abf49zTJ10UeXN1KtGRVrKOFBFSmvEW/PsQnNOqFCOmA" +
                "2WfngG2vlEj5uihhAGnPyFnV0CypH3ipWuD2mvkX6gYmtRFmoBfI4JunbYbzipIDvzLgxE4O8HE2DoUC" +
                "GjRdCE0GpnDRQckllwvEwsWdCN0JBkc/xb7vRfkThx8Z4PTsCoNjPSADBIzz6WLKOPoUhMY/Xfy4gtqr" +
                "bzNEFQPkPiXtUMcEfanDwo5xo0a+rWpPM+TAJigtAi5u8uw2MnIUK5qOa/n24mxscVJE+fuILi6tQmlb" +
                "qm+YTCDtV8hrJ/NFKZb/ruf8hkUg2yhaw2qcMICjEUbciEZweIi2qGutg8SuSTzgH1xCVXeINGs2V7sl" +
                "5sIGoVwDohR9bjMpe9IdUofKYUlC4RqrCCQZpNhOnCP5QC2EDw1oMcQLZFsjdDvKrItblCNWjT31ICvR" +
                "p20SiyhL7ElJwYB6pKJtIXWCKGpYqj29KyaqgWtxam3zTBvs0CHSWpCZyDPR6gpRNLGGJCT4lsyXwFEu" +
                "qW8BRGwMt61WToKGT4WuYcnudrRkkQD3ZNLGfmJe9TSbnKi0G0hEp3MbBHffJgyR/Zb+wgDBgzW0i2jQ" +
                "kqxQNIcqNXjKw7gdaWJSFO8sV8CofN3YJMos4DikEhwyZDBRXSoPYTWhN8UGZAZ2oL+ovYcP5bbKczRm" +
                "OT0WVUzEG5XS04p1bneTm8Ltya3LDBWKLBfj60BPtCZWOE7prUVijidgDoGOploj2M8G6SJw1RrjQWax" +
                "LBM1swigWMYovgGkTN3KN6KtSCYbCaWCkynqe7DhYJ1UqFXwrps6oB5YB7qhD7VRNE3h0Et2gKqVMzy2" +
                "uIh6EjKfmhaU/Q2fQ6xeeWSS2OQlRC4Q4lYWzS6rdwI6pDstTTVaKwAcUSsLSVw5TnJZwlI1gxltAWbl" +
                "bfBjVrIGTJ7sCJSeKSWrhMU8No17Wxg86mUB9UzpokgQVJhV225EOnyTrGb1sVN2NvP64c/zR090hnh0" +
                "wDXHNqgIxdiRsHCo1hWzTNoWa2ow2pgVOaDjBC2S2pgmwAvMqfV6uM1Ct8c7Ap+m0B5zJkQNHdnH+x+p" +
                "ZkgHp4gAUo/tetKu4/nc9RItWMF5xSKZtytaPYTjJ3Rum0vU4e6v0fNOOazQkCMuGqVWmVagSBPVZOaE" +
                "t5VLbAuYYlOyi4iOa2GI0OGvFB3psWZGRws9SY6gqxoYj5uoku4xrmJwY2GL9ClpQ+rm2/UBsi8uTu+N" +
                "nx+S/eQJIkz49dUOIib7iXPKL7unl2cXiAS1boRAkd34wYflTGDKPjUqDP4N7ftWbe69EHZEsYeccdDM" +
                "qN8SnxqpENsCsThfbduX+V7KA1/kK+2A5xEqmaF2R4rO0SQdGz8KsYqCtGgDd4N14qh/lUJaq7iTDDeH" +
                "9eQqIcRK2cyb6FacYZ1IVYmvqJ71GBT8FXBI8ZZb8X+gn43KCuHKVXeyo1gMFh9OtohhI7MKTg7tbVjp" +
                "VZZNjRsIKIiIOjouP+WYNynECZfqalG13PcmL4sZwm+1Lobz9XS+eDmerzXYc/OBxYSlqAy+YzGaK3eS" +
                "a7aY9kEMtJEVzdUf3OyRUJlkI+xDhUC5C1mkLPn3zy0GK1hHdzEbhkt4//kACTCQ3Cgfs/ZZbcdoqT03" +
                "q62ZuyYiahS3Eo1iGxnjRMR1SiMrFsYDGhpu8bpa1savVgYEKhT70UUhNQJnnTCLHiniNs+i+ZHDYWCX" +
                "NCGR+ZKaL01mEHZC0rMsG1LBYViPvd3HqkDWrwx6wxdFxfglTm7ydB1CHRIaUrtKR1nPMwsKCSpJZNr6" +
                "BDiYfzA+GTeU6gjxNlBRyoX4jlJFE1l4LqXiXAwZiLBM5yye1fp4z6JgZZajehUvbJnNiGxyjIR5R4uZ" +
                "kHEqJpeyAQwmjOvzHitUqs+F4B3pJEZW+qhBUaKxpkC0BlV50IWeRMqVzby2Xjcmk8127inIcZthCC2x" +
                "3Kw0FxHMMI2ccIjZXqRxs7qFmIUtp+4TtqHOOhf8G/4FFLTc7ultQ5EbNPa+sUildlUL3EIobptHx5BG" +
                "LsYX+UFinUh7ENV8QrtAoi+jZGudQxFy85LTbzlGUhtJgWreEWLQo/w9igH1jJevxeJ+y7Id5/tzMqAi" +
                "QP2+c6APDt39V3LbV/T79j1rT37GeOJ/zrm82bjqnODqXC8AiQQ57VmjfTVIGXBn60v+6drKfbFMzHO1" +
                "Uky47gFef0tqE8UOhukyTaXGM17WXLwjyXPSUyomYq2HI3GK4KmxoOT7osJ6edQ5k8mAlKJkwZIebdKh" +
                "XL6gMWXTKvhrUcJwv+W/EhkR5aumIHcUIQoxnZqE5CJZMOvhHYnsYGhELJQ2gVSgBbO/lNJFSiCOK/bH" +
                "Rngw8M7dgfyVIycRLwmrQZruwsmL66gknEZPQgqk9IARiaTqmD0byrKUc7WQynOuPPHZm6XSwRR6I6cH" +
                "fnT2Qhy3EPlnMrIx9Cu2RbtoCuneGxajXmsyT68rZIqYonvm9ktYwKpwBAfbHdffc5/SHQ86uGN1gQH9" +
                "SbSNUFQ4EcwQBQ/pOgteTxS7I6tid7ml9osheWjLwYOGEheCgT3JEGRc01gTu6nR1t4ecubZ3l4kma1A" +
                "eTGHQhaDtFbgm0e2NsMA60nQMwDFiecCocDrYoLYp6tjRYBjUOYWeREi0rVbjQ8csZ8Xyj4ldh0oUh5g" +
                "ya/pJN9JShu02nCrzBjx4X3UpJc5olkI6m7H8Z7+ktH65HcNTnO6zY2SMl2WTLd3QlM7g7NcbfqIWf5k" +
                "+gh2Pzk0dNGTOMEZRqkv2cx8GRvgVDICp9uwMBnKC2uh5SYnXklw8D9rZVKkKygSRKkqIjTd15TbK0eJ" +
                "3XyCtcqd+wWFiQ3KGpl2G/oIQAz4x22j1ssIJ7muLlIqp544py6jLQMrJDpEmqVB/oi2gQ7jDBIZ5NIZ" +
                "hW9iWICRSm+uVSp4lZ8lHy2drL2kPDCLuae7yV9pMbNkW0uoTYrKKmYFPTvdn9YRcVF7O1J+LYHhzKqE" +
                "fHPRkjQCl0aNxJ6upn1+0gWuXKWR4qnKf2GxCqqOMhsn4hrR5qxjsVPVngbC8eqAHLHLHNBaYzOgf6U7" +
                "iORMOyXsz/8J/ayeOls9dLZcPVq2EZmyqnsoWy5WDmKpGCAB2f4KOjyVDbNxmWmlEQ8ODAtsrYSiadQ5" +
                "oa1BTzvpGyZUWm6nDUDxrrhZcpnVsoJLEBU4CXXa8QEqfe2DqFwQAWIJFjb7O8glkVNMWTFc10jiwM2E" +
                "PQWFYfICegvTvr44eiFi7SnV+BYK4Jb4P711ETxkAxAlkYcW3Y/fDxFDp4hUi1arpATc5nOMqi3caOBp" +
                "qZ6APOI5fyy4AcP/S7LNSrJbHvK5/mRJ5pr/X5Jkdwmy+CDzHY6hlDh5L7DV6BaqiQ34u/Xsr4ImPFR8" +
                "beoclId73Uko0e1esvh0AlI97RNeVeuwVMcf64oP40X1PO740cbWSZT7NTr5VAVLq3lYs18W7zTZw7NY" +
                "CDlCbEIsUmIhASGJe/IcaMWt05qEa2u3qQUq/azbRa3daB0frjLAL8zLNUoA+ZNWKYOFSzWdN+P53uG0" +
                "uXKh1m2fQfR+jzB8qvGacGBI5A5dxHWlYqvnlK1a0dV4s35Fitpd/qIFRee+nSuKnFI2kwnvIwhqoXPZ" +
                "MQnrc8gZkjzSfRfFCC6ryGiOc0XvrxvPZ73EbvfL8IjQCYadNqJ0UBd9M59IXH/TzrHz6NxsOvNRSF9e" +
                "QMGU49KHix96wBQMyQGUkKjLOOodOgQpq2+z0OCDhordSS6FSvwJOV0s/k2jnMa9RcUn1uxwsRzUZKo3" +
                "LIUZXD1jaUl9/8IT5vQl/qpRcX9EedtXAMkcyI9SX6GQgbMhW6fv0rI4lU3M7eM7lyzi8bFXtAwzQtd+" +
                "PYum7JzGbm0Ggjmj5N2suJ39Jim+Nex4YJrTToUTOz4a4sxgPcuxtogURO8qQRSHW0JA7oT5K0x+jCDO" +
                "yqt03Fbz3IPQBf1/V9UnCPIMZJFFepZjLXe1ilm5f8UR9JiEK0V0YSZ3EojMv2oAObbVbrmQo5Wdry1e" +
                "iaJhDgGXdx44+tQTRJ96IIj1zM1Rm4d2PnYOB0O4E1XiNjAAlbGW1wSaX52WJrngui8B8qcVtGqMLxYL" +
                "UXtO0ThQxECvLERmumORfBagOxha/VuQAaspZsmJRY1Aozd5sahgOmaoLgB8LvUk+RYp4OgvYfccHB3x" +
                "qAbdRKkOjAfxJTlRYhV/16gGwLhbiKSXSx5SHbt4aQ1+rlo0kQ+3daaL7quzN10eHsCa5ix8EafPvzhB" +
                "3USTrQK0WdQfW6tPaksnt07sQljk+Xn39IjHYEQOhznXTyez7GjaUSjfHSnj+qWKx+2bs/zd6WnJSopV" +
                "T6+SNSCojOYx+0KqrJoC1QqWFETBzVMCeIayIH8IHmOyegmGq2tYuMefTy5+XKxAPP7qn+Ts+Z+6h1d8" +
                "R+Ov72w/xM/hh/MD7rwYX11JtWeyDJJMKtxg9cM2kOAGTUfsopbYjzXk7B0uO/uItOn9lmnxLvMBzXiG" +
                "Pbmj/UPwUV6/IBQDoYWKpb6T9xjFV5H0Y1BMzUrE4U+XZ6ePmAy2MMSPB69OEh0AAUhPxZC0ngei9zJQ" +
                "VjustM+SOZ2ym3TFdmDYcmXXhZV8peckf5ftJV/81wNi+MHeg0PaN0fPH+wkD8qiqHHnuq7ne48e8QTk" +
                "BNiuH/z3F7pETarDHpQYyMylNGX3zMaRFxUFLOjRswfolGu50Lsssxd3jibgVi0pdNWBawiWiQBFonvP" +
                "xNFzpQ3/xjKyvs2s0QMS1wJ4opTTWJK8FpSxJVusxMFlmL3EI0DuEQW410bB3tfffftMW1D7ahUV2q1C" +
                "/MBmuvzLCeL+sBIY/Pf71Jj48ufJ966Fji1TJQ9ux9XTb/QOsy17ydfPnj6RS7Qu2QBGdHFrLaD5kWQc" +
                "tm7TSOFC3AQucaRPkZ5eTPhcSjzqYv7AETRIexPHl0SBrg9ratgPgls1cdDUXp2n75Mvaat/mQx+wT9D" +
                "qZCTEpO9fexPNvrpMd911veXX/Fy4C+f8HLoL5++DW8he/ZW7m084NF+sU2ICcThVNHkK++zUXNt17/Y" +
                "9b6zLVZaDq5zUEJo2M5XhDyfe8Eqh0GrP6bJdZmN9r8wxrjN3+W7ZVHtFuX4UT364j/r0R8fpf8JYhy8" +
                "w0AS6ruEf08/flgMED92G0wxIdVBkdhfiXEZLTbBTdST8XXx7mgkG+ndjkdnwNmGogFr6ydcMMCdXAQS" +
                "YGv41+FpYZbUU3hvU5pYTM85IBD2N/mQjj6f5qHzneUcCB6CV3govFpO1XTfsZfPNt7RF8/WXkKXzzxE" +
                "er5yzcn9dhGEhMqx7dlkhCkJY2XHzVsFXIoMLePySbCAITleYwbi6jpdJSOUNBASStng5ADPiuGtGapn" +
                "Z1LZ6/KQ1Kyq7P3rLO+APJT4+QNEq7NHkQNXS495/EtLuRQAIflvASOypt1rOMTexOrEGEa2+zpBB4m6" +
                "PBFHrQGtNNdUuWHRTgHZa4YFJCs50/jSLDk4PYpszUBoNkAvJgGmz1ce2c7/JmwkNEguapUShK2ApzJA" +
                "Fkm2Q0vohm4Z7nIjkEe1T4A3eteuRdfWnfy04qjw4q04HOdf22MVVBtbhdRkffIaWLT10TVoZddmDhmH" +
                "ki0nfYN1zdVoVQAEkzIjw39ouxq+cczvWF/a2Iv2ZfiLl88P7MHnf7W/n/Gefwtf6f8a+7/6/q/0N6jB" +
                "lII3Yn2lDqod5QV3rq1ouorq+USWxi/CC+GYMD7t6o71MALQi79C/MlbU+3hZ/SyPzC7hh+fHkk9Iqtq" +
                "YYxpgBbqRorC0QF5kPWRx/hsJ2ZhvmRd3M6KYOyN16uhKDsL42t+NWdgA/IVvOtW8Nvg7R/Hl9TCSXEV" +
                "T6vAFrCuW5KGZa7pUTEYLOZQvtvUIlIOK3fS2WDpsLG1268f7dJIyCeunEwHkojFBK5WbHlqBIOhb+3e" +
                "lCIXPNioFd+smJ5u+/PZPGiIGnDXbYZaYf8aYz0PKd04iC0Dbl8OQfuLd5K0q77/UCsS5FUlu1LKLx4x" +
                "jYjbMmeoSdpWLAX/luqdPLPpr6foV14+4fspLD9ZMHzCX/oBAftCjBo5m/xqhcJyr9P6rosdrFII9YJx" +
                "yePTl/Cs9QcOKf6Nyh2uQbtLf1J9XhZM1gQXq/ERCxvz4PDq+E3XD8nTYM0xae5pnAMk3s8k8v0pA59f" +
                "dLuvzq+6R37gJ82BEc7IEOcCDTHAMsj8VyqSdMQ3/PB4j55giM7VBEBXfxhyID0SC/qlFHfykuGhKjob" +
                "sXWVlVPQ/ITfsKnBg3as7fXhYbd7FIH8tAkyvxPiX5dYLQbEAnl8uRYRd01z8PzsIuCF0zxbM00fMZk1" +
                "h0LWzzREQvpjqPGlt6M0nzCXdwd4F11GXgN8+8nXq+CVGdXpHRTgg4ktctn5OIzuWHQdT7ZAcIEy0L0m" +
                "2Q7N3LUAozzPKfvJNxugPE96dB3IhIH4/OZ5DB8enJwETt5Pfv+pAFqF8joIPwW7FuBt7lYT6NkoZ/4g" +
                "5MG9FBBI4OrEi4jJ5Nt/wSI+Dc0kigb76QQsY76DJk7OLq/iofaT72RAxMUNGfbZI8Z6kZCWQczVTj0K" +
                "OErzxDTx1v8E3pNsEJSWOjq3SOWs+7AQdLE4p94jYiS2+ekf1EOq+hcjLdJlctgn6y/G4/C5jKTO3iMV" +
                "sTk17BRwp6MZ6y5fMXRIy0PfNoRI+5Av4otq7qPqAP1+gj0NBQJaXhY1GjNGFEonwjMterdP4LQ+DNT6" +
                "LFD0kZ3Gx3U2gqs2cu7ZATqesWfkBaTA0pd+xhcuwPXTp6KeLvmiNL1+cXB88vqiu/8dfzp28/zk4PQU" +
                "oqXHp92j/Yeu9fHpm4OT46Peq7Or47PTHtvtP3xiD6ObPWt4ABXQe/5jr3v65vji7PRV9/Sqd/j9wenL" +
                "7v7Dp9bt8Oz06uLsxM/1zO6/Pj14ftLtXZ31Dv7y+vii27MD4xj0YP/h19bq6vgVpjh7fbX/8BsHvTMa" +
                "9h/+XoIX4Wytr+L1n+pSkqocdq4OLq56+PeqiyX0Ds8gYS+xKGDg8Zomb47PTvD7snd+cPU9Wp9eXl0c" +
                "HJ9eXaI9ss7a4eXZwUl7sCfxsw+N8jRuGD1ynbg3zzqt3Xl5cfb6vHd68ApY/urr9sPWSGjyTavJxdnz" +
                "M1sinv6+9RQ6589u8G9bzzTh6p5+J2EXrbxuoPnFBRr0AMDp5Yuzi1c9R4QPnzhC88gCuXQP/0xaBD28" +
                "QTsSBRo6DEaw8l955pBmBHN8+uLMP5O3fEZk0IDr9Kx3/Ofe5dnJa1IySPSrDZ5Ni78H9LFqJ62nDa1W" +
                "OjQK86OO7ZNh0RAb+4TQHbC5IJgFCdzBcBqcFv9amyhyb+AIX1GQUzVrPhPkPrazpnpq3cdj7BTMQyDL" +
                "VT2Gsbb0IIw7A9Qu1d92p4S0RThGEA0h5YXwMCaLIVdhhd7RV2mqR+GTQo+aHxKyPAGNLl/mba9ytVt+" +
                "tOjDRnpyoNFJ8yuuGWuzXPIj/iCSDLZ7Rz33Hdv5W32g6kPg+A9VtXeWBpujsjiLucVXaRYw/THg9qdV" +
                "rvv6AyvhSUOpdEiqOOpsvg1PYbmj9j36vNTKFLQAA3n8c/M0Ce03+ELVdH1Yxn1c9x8LzPhP827+m7wB" +
                "8PDBSD0p9T9qtv5Dd3gAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
