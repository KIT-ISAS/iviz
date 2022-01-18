/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceAction : IDeserializable<MoveGroupSequenceAction>,
		IAction<MoveGroupSequenceActionGoal, MoveGroupSequenceActionFeedback, MoveGroupSequenceActionResult>
    {
        [DataMember (Name = "action_goal")] public MoveGroupSequenceActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public MoveGroupSequenceActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public MoveGroupSequenceActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupSequenceAction()
        {
            ActionGoal = new MoveGroupSequenceActionGoal();
            ActionResult = new MoveGroupSequenceActionResult();
            ActionFeedback = new MoveGroupSequenceActionFeedback();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceAction(MoveGroupSequenceActionGoal ActionGoal, MoveGroupSequenceActionResult ActionResult, MoveGroupSequenceActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        public MoveGroupSequenceAction(ref ReadBuffer b)
        {
            ActionGoal = new MoveGroupSequenceActionGoal(ref b);
            ActionResult = new MoveGroupSequenceActionResult(ref b);
            ActionFeedback = new MoveGroupSequenceActionFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveGroupSequenceAction(ref b);
        
        public MoveGroupSequenceAction RosDeserialize(ref ReadBuffer b) => new MoveGroupSequenceAction(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "146b2ccf95324a792cf72761e640ab31";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a3PbxrXf+Ssw8cy11NC0/EiaqFVnZIl2lMqiKtHOazwYEFiSqECAAUDJTOf+93ue" +
                "uwsQtJ22ou+dXrcTm8Du2d2zZ897D14Xt+ZVWayW1+bXlcljcxzXaZG/KqIsiOif4Qz+3Xvd3e7KVKus" +
                "1pYl/drW9qUxySSKb7T1VH73jv7Nf3qvr18dBguYRVqHi2pWPd4yI1xl7zsTJaYM5vRXj+eWpRPuiC3O" +
                "TgNEQZgmmysjPBGC7mcRVZ3wRHiWvQfBdR3lSVQmwcLUURLVUTAtYPbpbG7KR5m5NRl0ihZLkwT0tl4v" +
                "TTWAjuN5WgXw/5nJTRll2TpYVdCoLoK4WCxWeRpHtQnqdGEa/aFnmgdRsIzKOo1XWVRC+6JM0hybT8to" +
                "YRA6/L8SnARnp4fQJq9MvKpTmNAaIMSliao0n8HLoLdK8/rZU+zQezC+Kx7BTzODPbCDB/U8qnGy5v0S" +
                "iArnGVWHMMYfeHEDgA3IMTBKUgV79CyEn9V+AIPAFMyyiOfBHsz8cl3PixwAmuA2KtNokhkEHAMGAOpD" +
                "7PRw34OcE+g8ygsFzxDdGJ8CNrdwcU2P5rBnGa6+Ws0AgdBwWRa3aQJNJ2sCEmepyesACK+MynUPe/GQ" +
                "vQcvEcfQCHrRjsDfUVUVcQobkAR3aT3vVXWJ0Gk3kE7viRo7DweRlkw2qObFKkvgR1EaWhctBPbybp7C" +
                "htAi8LgEd1EVlEgwFSwCCeiM9ptIElAS5TIYbHJ5C6RxNzd5kNYBLNRUSLRAF2axBNaTZdAbYVZMNXcG" +
                "hragg4mZ4lyiIDZlHcHO4Yx8/Mr800T3BNAL01vjIBbPwdQyrzyBHszp4AxWVTQztAlBtTRxOk1jXqDM" +
                "oBoIdDwg3AAmtVhVNcwsgFMHrQa6f7hzn4MbEh8E5oYI12dXPH9dB0zsMovyHKY5WmI7IGH5HRb8YCcz" +
                "75giYPY8hZkWU2hLNKMz08nz/kQBHNI8CcsoSVcVsU0TxXMiPj7JsKtL5FsICn8THCA+oDF5VOFewy/Y" +
                "O/Me+RsdfKDlIjeWAQ5aqDyrzeKXd0C9ZlHtZn/bowuN43osTnD9dB5xXu4E2nX7+LNtKzNbIJ8SBOmK" +
                "+4iCdIoHlI5fFFRFltbAyWRLFCVIQx5l0eEreF8Ap/WdMbmTI4SwPg0kG7uAYwmorxZFAU8TYBc4Szjb" +
                "acmiSUfj9TJc2W+ZMjaFLdPBaFVwpuldbt7XJA3hQR9ZDizlAA55BNSeyzRhwEFvmhVR/fXzBkHtbmc9" +
                "JKpcRy6ZAspAYiGT421MzDTNU0IcbmBktxM4m0MqgLB0LngoVvVyVeNuKkvEjbqMULzUpqyEHuAsFOVN" +
                "tYxiw6zXPzMiCbAFiP8KgPR+0NYeJAshXNqHPVZzQN9AiVkj+10tQfSBHhOcTS0j/3sBekOlA01o8ThO" +
                "aZDZwnSWRZUyq0JqwRlHfM7jVVkSFeeG6QuOtWvMAAEEUpqpAySD3lUxKeprmkuFUwtpXio8oHOVIicg" +
                "1k+vHJIWRQJKGYosPH7wdBAMge8EJjNyllBNgIZRWQJ9066REBI5MkOKpmHoAdJrPE9B08M5pkLVMPG6" +
                "jAghvNeevsYgADzMParTCkVQ78T1AN5ECq4HBBd2UQjyAZVRvoZFwhvghQXsCu10BEw1ZYwCnSSrGCWb" +
                "48DMKW/TIiPFkrDsz3OPz95ymYlIRzZLg8Cm5EUd/B2lJEhifrbvT5kGb0943EKEsHQQ1EhI8PTvJgbF" +
                "ZM3il1Gx7o3tcx++a901So4amTAUX+YgkRWoxBChAsughng+VfT3kTRxjyPSH5p9VbSgFqQ6gbwjrW5z" +
                "8BmKcfwhhwGAeZqWwoV/FktQ+hEPCpd6hgjLwl0tJtAYIaMe7kCQWCj4hC2AMYDCFlzPi7JGXgKMfiVs" +
                "RKdfgvZdklrHej4ADq2yENU16m0WlYvofbpYLYJoUaxEsKQL04VZJJYsK+7YaNHDRNq4qOr7ljFLQzcs" +
                "aX3IV0BDJ1U5ws3lw8L6C5lEwDtWcSoErlMj3AZgyIAuWCOtsg4YxTEcYcQqEMggCI5lcrdRtsJGcNxg" +
                "ansH/Sfv4O14G8B1BzhPVZQDViL/EVaCkBdI1XhyxIJITUnqC9AsmFwAThYIIwMlggBBBogdgbcjk0Sg" +
                "aYlzTVFC5jODds0CNy7K62zdp+0n4y/OVqgUTwyxY0Pi42BwsM+SmcchGjdWsih9EypwT58MDggWsHdG" +
                "9F46MIP+NowAwOYbHzn7Tv5Co1A7hRVvbcgzarTxu7fb7UJud0g+ldxqQljJHREiAWdOICJDmK6A9edk" +
                "ZtfEz1ZLtj7h/MF52QPttni/j+SiDECJpnlucFq+O0BAg1m3ZiPLKn94NBbFJM0MCRPSoERSMWDQ+e5M" +
                "lg3wXJ2S1GKKKIVBlWYKohM1ORWFMEVYaJnj+pt+F+UGKTAL2HVupFwOuvVZhxcSVzrjeVSqK4AYNr2Z" +
                "KQBrwLcJ82+JiT9DwCEDbTOef/9QQHA61P2QVue4uqfAfWHqBqVBBCeKTyewSrCXA6LBQWCVfdp11G9p" +
                "mbYnSqi0NGSAk+ZRkhHdR40wKQwqw6h5LqIb1JDQYELpDYIceBlKzrzKmI3BY+iyZwazQZ+pi1qx0o5m" +
                "O/qiwGIu0xmwIeoJAy1s5yiQxQGvmT5lAUNz5sFgX9AWL2rhC8jq1sUKdAZYA/yjFBcYyS6dF1lsdVH0" +
                "kfQFRBOhl8Ry9GDCmayBRoHKlaO8t/9a23/9tgMu4nTQrcwjzR3+ognw+ibt1riHwHhZZ3PqMVpuleoQ" +
                "aAUAg7yperi3RcmDf48vWQGmdk4B/r4QbQv0RDTQ5tGtlU8mOB29ZM3TSjTSp33Qr7EttPOGoO5hUkzD" +
                "1mDHdQ3aL0CJiyxLK1xnMUFNDcRXpO9gwyvYUFpFUHgCdL+n/U+0+4h6gwqsvUMLORTIOO7LLJoBdhP0" +
                "kiL5AjGLTYGSLwY6dloSiTk4SjW5LekwTacbTIVmyIYJ9fd48qKoavSaqjhmF6/6EMlU0KVOigQ1kj2d" +
                "DzRE1Rm9upmJyq7GMBBseCSkdXgIvMocHnoGjrjMyOYit5Q4UGqP5PZ7k6JAYyTExd0Xo+smQA9TkT0C" +
                "RH7zIksqe+hBa4nLdMJSiU0ZWrjIV+ArYA7T2SkLctvyAUD+6Jya3AkdRaKK7JUGZS4+L8HuSSs8bvE+" +
                "zoZNN9TI0Fcd/KFxzFTCKJQoITVnv++aOv2n3fRxRY0fV/t0PF0XM4W9qq2/nK0pqzUKgIsF9r4AxQkX" +
                "NnRrQflPLkiktgQ4P59Q0CiRH5BxwIhgKW0PcdXp29XxxP4VRyyQV4IWA6jHG204uIC+M91GYh58jLRr" +
                "IJ6FitzgKcUbOhhglVaso0SO+ZBFDdYkjqD+PlJmmhimyZBY9K1sVJZQwlEnaQ9rRwpTi2EQ/IBCDeUb" +
                "yxthobSKvACAsj8thz3CWvRJVsVgxsBJvTX+drInAO2ktVAjYo9Xw3vrrV0cIHOFQXiq0t+A18OS0VNJ" +
                "cLxTQz4eUvjYzW1pwPm7HXLI3NRJo7aEigHqobSDIBbbmhwbmMBRiX5EQMJPPQjeIyV47xFjYBcMZVPs" +
                "wKquVAeK9MwyD0Dqkc0lXFgSS8wMNCsiPFSxkgL2Fc2vAlVuZdeAklVcr0riJW48JmRWxQD1oMcnfJIj" +
                "PZ8YeavWoIAsGPPon07YWeLUJe4zM7U7/2hPWkfNDTAlYlJBPActYRC8xKPwPlrA/PvoKgKNPyqVWURE" +
                "YW+uTl8ST3uGAnwPlGIwCdfRHYa02E0ICjC/RAomf7gL1fmzi8RzC1QBULgvnujGe9ImsYVCgwN9a0oK" +
                "4GDUBRbcmMP/s7HdsrE7tOPmn8zGtPn/JTa2jYuxAordq5bBN1YShlaWnDca3cGGYgP8u/XuB0ITvGR8" +
                "7cZctLPuMBjpOFi2YmMkd8VGzL9q2ZS9npq/nv3X+9sqQo8PMgC10nazSDdwl1kMZ7VMLY9vLAR//epm" +
                "jXj4qOWn/7rb0QYSJcmylOtWTnlsrmdSFjcYOs/JFq/QJkK7APlwlM8oPkDenYHdQGnifku73ayOz0TH" +
                "rsFW8Pa4xfVBnqOXrCaNt0ZG9YlLJGDuJ5sCu/AEbrE/hWe3nlqPurXgJJA9Td+rucL+qZRWfaPOffy3" +
                "+vYJj424YASqCIap5tGSndnkzXIBq425sSDzzWtsRgM+OJtqpgZtF66VQOaFGOaDNBHvcJ8D6WJUP+iC" +
                "pyY2S2+7DC+0gANg9KqJKAaqrj0x8MiJ4SKTdjx1GKBbwvOM1sWKCG+ytv7rR3ZioUZyoqwECbH2go1e" +
                "Byc1CFjIbhTU8VzYkmdFWgUoXjEba414HewG6Yo26s4TZhfdAmHYpQBo8a4BU8NgN6jP7PKLs4KD42Wx" +
                "ooMgUMRtr2PkJkb5W65ptNJknKYlfl4ZGLcPM3rEd+PFxpxl7sJogAycXShD6KbcmXQ2txpLazP6GMi8" +
                "yYu73HFTar+LM7l5Fo9FDehzQtKUXKji1FGdns5MtxcbKF6WKQjcI+ohWLB7mH9zVu/7WT/cT/d5vTRM" +
                "FOjJmESckELYsaeH/w5RuZxxjJ/XwksYIwQE48Ki6i0TbkthrE1tTs8sd0tL4Qx4Urqc4r5TTxFwXWBI" +
                "SYeJ0UWySDEoVfWI4/A8qdWlvkLr0DVre2OrxvuQEQ8jvTbVvAkVn0DbBb/ohIPvHIgXeDg0zIp+NIPC" +
                "X7iZC9oGk5XLbjBqo7AS4dJQYMeSJGWVmhC3788NszZoITTSlkXiOze74ySpfDISrNsIDfkNyevtNQIa" +
                "vU2LVQV6sHmfYg4c+e1ZmBLDGfQma9Djjk9Pjw56ZPPiaWiMNC2LBTsk8tu0LHJKVEDDqkT9es+AbbYG" +
                "1kRHgdy+NRzmqkUTabLPI10NX4/eDo+e0JqWS+RTaMHmdl1k8wpjpUlXNrHgg2vVYAR30nXCLrhFXl4O" +
                "L06PngoTdmN2D0ej9IEr3gnly1ZTVGSPIu6yb2rGaCpfZqY1myj7nJZRFRniClCrHMNx08RUKaap0BQJ" +
                "N89wgqOlxn9Z4sJPVEC1YaGv74spfpyp9B787j/B6MX3w5MxJv7+/s7yB5Fz8uEYBzFNMpunJPCEkQEb" +
                "Q0cFmjCVYcPai6vWxYzd5tZ0ZBcu0Al6yRtKxY2xfll/hEN6wv2d86FUgpoBx8qDZKLMHqAowGTiT0UE" +
                "LPlOvr8eXTzGXAtxqPx0/Po8YACD4NiSMLBZewC8WBwyasWKcxqxUFeBMgiGpDWkecem0zkig74obkBf" +
                "uTGHwRf/eIgYfnj48AQ1m9MXD/vBw7Ioangyr+vl4ePHYH5EGWC7fvjfX/ASS9KY8oK9OblwRt490W5w" +
                "czwsoOaY1g+hE2azwSm4MUaywacZHNVJmqXqBDBd9BpTsi9lT0ls8fQF0wYBiSkBMlL/FftBkLgkI0y8" +
                "YpRrjl4yWSy58wnMYWARQM8QBfCsjYLDr7795jm3QNHLoVRotznjhzLS9d/OA9i2ymAMw+5TY+DrX7Pv" +
                "tAXDpqGCh3ez6tnX/AQjRofBV8+fPaWf0LrEBimqudICxP4dGPOtx6ih4EJ0AA1+8dtFkawyfE/h07pY" +
                "PlSCBtK+L1/tNm3BpRtQtL5aIqX1g3gNqjUpbTE6ysTbpFZOaWx4BshKvUyg4UxUBQBgyPBRpNNJZMX5" +
                "oA//G/TozsI3wYvRjyDG+N/Xl98Nr4YgWvjnyU/nZxenwytg5fJgdDE8eq6nXfkTSRmck7RiLU1ZQgqC" +
                "ttKYrGvq3OOuhc1jMBEJSL+D1+yQHX+UxIahRE0vxLaIrvfKqR66Pg9ZuPWENPEtLJymytbDj/3gJ/bl" +
                "/uzPOZKk/8zks9o6G9s8qKIce5dvMXC4DX8EjcT9+sniGn/9jFLcmxLjX2ZFfkDcdmSb8HdurxP0hakQ" +
                "K5akQM0IFjOHKWjQ2Nfw6vj07M01akjemLrJBBM3mO/XMFaYdMj9QMkVqh6SJ16G+jmIQOHgfDDOqmjA" +
                "Db8bnr36bhzsIWz5se/WxKFbD+NuTfOGeaVnIdjDs7DP4yGf03F4dTIO//DG2TYKZls0sqnVOOke86TI" +
                "2Rmgr6C/0/PbZ3JibB43p7vW6dLREOEU+6OxyflPfYlxfClI7bVOouDPklRr8aiPupO60dghBhveD4vb" +
                "NALI+Cw3glCojLZ9X7RbqB7wew4yI7Y95+Yg6HE2iw2+eS5Zr92uFpjm1nPZcEn5QfJIMo0by/2IC/b+" +
                "RRCalip4vKmiOYkMIpMbKCAUo3wGGsSfPA4rGaGUD0e5qzbzBtaIYS5Qdqpf3vVwjLEAoJiCwOoJ8xDH" +
                "nfZQ2+vGaDIgzaYD5xRq5U47QpUuowNluqyHlZsU3wj85RnP07wPyQ+4k9mSXd4Z+eXIqOmLfe/sf+sk" +
                "iN4HX6L778sg/g3+kwRHAVnUUXB4BARupr8cvEOPov35BH/G9udT/JnYn8/e2VDDL8/f0bP7QsBHfHgt" +
                "v1ZnMKzVRQmNs/U/07yVw1Bw2Mv4Z47i4r6SEG0P4i99L78bfjRyu9/hLhXN1pym8M76zL2xWJTx9TC+" +
                "20h6qHWLNPPDbagTg8Qlmi7NmCXxiNYqB7D0XkdmRbWZWoH5Xu5hY1mbSRfJSt0PIPtD9AGFlPC6Gyes" +
                "u2WxPTU623LRz7ujoQj3b4Kok8Zef5FLMhQj16sN1p9PSfEaAeb0ASZ2N0d7FBqXQy5lHxrtdHOaTUdO" +
                "9jZaezK52eFtWon122h/ax83m9//hrUwwh4a/tEh0m2sasIOCnJva0iETDDFt2MxKvbotfTVq0+oYP9i" +
                "h3gEb5Gu89iEEyD8u74b/kvvXTSBBbyzekM7I6ndsuM5QSc/pgQo3GUSF69xOxHsJSYvahL+GBMHq1Oz" +
                "Otm/wQmgPvkGJxlodaQn/GbKQq4fghZQuYTQ/XaU5P63e5O2tx5T3LOkJfTtdrilbuZcsEHL2coUDWxj" +
                "kzQodNt1BTrZJTSdYvRvT4iQoFDCwr5j1VE5M7WIhMJrd2eIKfsXILak+DOIkECEPKROQO5jbJ150NOY" +
                "w1tu6RqFfFHvfx917YC8mkhxHp5IEQob+uyUUSPh3WproGlTMdZdKdTBu0c+whXaQfufGpfq2cueKL79" +
                "hLGWUYtuQBD9nxzIOnORIy5toND6EsqyYQirRGD8JLGhc+vDsreBZrDddCt7c4nNINn2RcnQH12QH1G7" +
                "f0LpFJy/hxU1k38+zo4kVtPoJV4Jj1O5nSHCcilEvW2JSU3z+19kfZrJR46QR2ReBaYskWWo6PLime6a" +
                "5ISub5rwfYgdQ9t4s8X6oy1+a7f4T+RjXWqajcHb9bp7d5SdmRK1erocJ/gmaRVz0JHlzf5Gpogtv0PU" +
                "T+0pB7Hhm4ss4DX70e44JJYuKVjKgaICr2BZwuY0aQT8WiZHzMXND0dDUGD7QtNVqfYxE7CNShUTznxm" +
                "Huy64zQQ+gVs+qHnEFL8EICL0RiAc9rXAmaAl/W8i6Gw2prp2tVy0JnbJGVFHV4oLxlsWluonjagN3dc" +
                "eizi4jY1d56bmLECRL2pDBGH3qNcKsBBNMnWktC6rxe6ifCrFWburkpilwN77Bs+VdpGkmCutIHSCFon" +
                "KZcacMopKyNahcLxcx/gn9Sy1IvEt+q1brekO1zxHK8E6w1Pu0MshRRLes+zWWzAsrA+5ypzRK1DdFxL" +
                "mSmrTFXEMl5q6gGjWymzLu6iUtIhdE/tlJnoozaJeZRl5HZMWQD1RC5pY4GVDSgi0UqLp9uK3OY5N+jz" +
                "7deIPZPAzEjDqNq0LdUW0FYJlmvAUZrYE8r3oWVbo+wOEwig4TOi6wJDEHxjHiccVlp4w+6nvdzbPIlM" +
                "u45ENDzKGwQ6kgzo7ne2hBcAcGJf0E6sgXO0XHEYd0nBtrNxYr4xitnXdWOTkGcBjt2FUkUGAVtqss2q" +
                "WtHFcLq9Vq4M59x4CTWbZw4dBRzQBQl0Kzp3ClABj9vJTa6AKLkNb4lzFKvZ3NETqhIbJ67fxcX0TGBC" +
                "TFAtuBrVxMTRyp2qDs2BRunMqBIew/jWuiK9B/5ZwrbEmQTSDZASXhnkrJ2IqLXviCWO+J7rxtQFfZiO" +
                "A2ryPKJEFcwbpBEORJnkWAyNx3qFd9FNbtaia5peCScWfpljoD/TlXmj0+qVQVvC0MvK004GoETNR4jM" +
                "8HbmugoJ1djcnPlsR5RQg8jGN32apT2UmmF6IBL3rpD5sAYMqMc00yXZsabaV4gYhMlMjakjKuxk5G7w" +
                "l+njpzyCDx3mhVlRzEL3Bz6zsHcKacWYWiSLFTHobcwGH5DaQFaKRAJTGHix5MRwU8JjzCQ96NP8+CL1" +
                "gc3arTf23xPNSasaA7QL2fNvz7n2IilYgW0Di8zWrPr4PejEZ2j7NJeogYRNOa/CYYOGlLhQI5XMjyLP" +
                "KeuXw5ht4eLrAiLYmOw8osO1oF1la3KQjLRYE6WjhR5KBdOiGQ1UUXcfV/50fWZb2Kzo1oFRDUU9G1b5" +
                "+TE4Cp72g5/gryf94GcKS0hwe3hxPboKfz5qPXChdnnwo01sEIZJ+2TH/o9R7rcKEkIA/lY+rsUu2ldl" +
                "mBi1tGXLyCQALIvuZ/6+cdJZ6khJj+5qAUU1KigV1i/u3Sd0sY5WFakde6BbBQldEs102pi5Ho9mLmmw" +
                "p0bzvi1teE0vbNEgaicX3B9w6imQ9pRKAbh6N6SsSHiWLxyVpl6VOV008qoEcimlVqVBZHnWXJLaSNIJ" +
                "T3jA1+yxa4gpLr9jHpQWoCv+L+gnUPNZqyIMaW+S7RTs0QUOPvIVGJxo+4DFVBmzEM5E6bY56Ut+JibC" +
                "vI2AtVNBREzLFC3AyyDmxeB4IY/nL8fyWHYV335gMW4pLA+3LIZjPCpFXHkrLUH1Jx3dY/CUxgtmz0Ff" +
                "fUdeISOXUURY7z3go5+sc9DyY8rqy6fpDC89sB7vLbVR+OqMZQKJi6nfiqS7bKSPkxXfqxWUutI1VW0X" +
                "r2XfMJG/HdFyVDhwa6beYr5hJbdKqggHUidPx++TaUI0QXlmJd0u5hxpJOncGLoZC2At9gYHLMy7VwYy" +
                "XFbSxC/i5DaNuhCqSGhI0CqamtAelhAX1HPro8mBKl5wEJWiemT5JeyvsB01b9xW8ZMEQTX3pGymFj+j" +
                "bXNHtDQUI7XqFh1LkyOyuYYaKu+rPGZeg+ovHwPKxreev00q5fdE8Eo6gZAVv9oopdYuoQZWXZSStaqa" +
                "UxfMzvppMkICEnu9S1Z+LZVQtMZdk2U3Q7KIVlCqveI2fpVGetys0ugXzvPrqTVruGDUR8ZhGNRIPa2e" +
                "QSr3YPHcJatlppVh6mmw12XZubAShaO23SUW7QKkKV3yC7mms3e9uPeAlq3H3t3s4Qv0PSlDZy81veZ7" +
                "9VoRz1W3kfZ4mAEeOQKWuLx8VvXO4dcl/4CZkKtZ3jXaYyk1w62xsJ7RtvS8Ua5Hson7m3V7+oG5FaW+" +
                "AKViES2RxfjLWpKZSim7aLIWGZlN7vISI3gh548VmcYV/8Wgx1cYTrAvRtk5DZtBaZi5MWRTJfihKLmW" +
                "aZbsqBjQLqrrfJgC1XxzN4z9khNkpG9ouqzUDmwd8gdK/Bst43maKXFjw3YEx1VB0pLgCAZa/TkK5qBr" +
                "H30hKfd36U06KItqUJSzx/X0i7/U0z8/jv4CpBzfACAqh3BtDF0MTop4tbCumKk43Xw1ZiMIJJygOd3g" +
                "gecAdZf9qBE/7Y1dcQ57334X14s7D7/wP02eAQyUa5dcxBoFtbN5XtRE8rykN9L7bZpgAiK+TV3nrawI" +
                "zOxfVxHm5lfrBUdppbheM+PJH629giG+szPi3KmOmGn7ABPPgz032bRPgf2sKiwL8TUPRgbrH7a2hsPQ" +
                "wFMoNtepKvivK1Omng6WkgRnDO/lYILn5B5Qyx5VaL5DJBjcOnOnm9ooxObonqKnDrnIZoLzUmASVBqM" +
                "prFRXJQIgxxMpMWB0JsH0IGucT8lB1VjttRcUtEZixJKkKr4NCXRlfjCeh4cX5z699csoQmA0CcB5H0b" +
                "r3Tnd3+GiAJR12+WWHP7AAIuvhG7ihW/RNegP3cwbU9k9x6QqqN277bKACruXfVG/2a/TZJSub+bJZAe" +
                "8akLkFKuH16AKCP3P31Px9BsJHtRD5fC7vg7TeNYsyqzeQ3cVs2V405tEntXvSivXr04lhf3/d0ZOx6j" +
                "E40P+6+Z/dfE/ivaubVAuhnrhU3Fsl0kghxUHaUdx57mSZzTLxXj1H0HHy/n9aSH7Dz/+AGYHTn05OW9" +
                "3dP9wNjklnx2SmozWn6gdHFxB5As5LiA9qUx3bkufiy4oHadQSopo8X6T0eoTXzn1i7lawsCEHOXuxbw" +
                "OZD2TyOLLADKJkPXNn5ahLvu0T00vLbyuIjj1RKE7D4KDDLZ6EmUx2tFxd5gUj8eoDKQZlpRkwHRhecs" +
                "Qoe3Uy8LDUhK9ybnuDJaip1M+sW+TebAqCSWapdueZG4pHAOnq7UpS/LqECdAc76m41Zclcumcul2ehm" +
                "2GDORUkidorclWmthFOhr+IbFON4Wj7fN774C2Uf/8oXeg1WFfkRVtXmh77kQ2fyhbP7Wc3WWfVanyGT" +
                "uAy/4x9Y5eDs4lWgf46CA/ivVwluDtS8tokuy7KI+W65WFaNry0JzOOT8dnbYeDBfNKEiYoeX5zO1lIf" +
                "/pMAX14Nh68vx8NTC/hpE3BpYpNilneEpn9s7OeUgmiKVyxTqcRtXZuooAQf+IN3mJFCEQv8PS8N3CZk" +
                "GTp33t7YlIsUGT75kfY1Kvbm5GQ4PPWm/Kw5ZfyglU1Or1YxYgFP/boTEduGOX4xunJ4wWGedwwzoc8r" +
                "bPgxu0dKVuajqLEuiGmUZuht3TK9qyGWcnDzOwq+2pxeaVC0bqEAW52gRS79j89Rsypqf7BVjlkqKanZ" +
                "AZXp4U8abFmAUJ49KUfB1zugPEt6aDfgIXTEZzfPYvjk+PzcneSj4I+fOkEp29w1w0/BrlSMaO5Wc9L5" +
                "NC1tjdLa5wI0E5M0FuGTyTf/hkV8GpqRKBrHjwfA8s5baOJ8dD32QR0F3xLAY/sNOvk4H3ohE6xTseBP" +
                "HVChVkUBQmkmXCDeJp9w9igjq7g18kmltDRdX8AD6UyGqTWKVhIhd8UuI6kRwjqbJ9TIRW0mq9nM+wBM" +
                "bd7Xn+dzcyKSN784JzFRDY7u/ltpOoUe7T4nUMegLmF0n4p4kLOXPjIIe3ZX4r1+ruY1xLYnqFlxtxC7" +
                "qQJM2l/V/KJVIw6hpWFrr6arfgat8ekpeabX8qS0hMbjU736YOMNeMdRP83EGWTKbgiqSwiQ4lOgXYY+" +
                "uC2hJfwQIwZa+eOhxMrYjeiu5Tn/QfMTPLvY0uaGSNAMc5zQaXVLXz+Fk4kJb2BB81uS79d41Z9/vzw+" +
                "O39zNTz6Fv/05OHl+fHFBfDmEN8OT48eaeuzi7fH52en4evR+Gx0EWK7o0dP5aX3MJSGxyBDwxc/hcOL" +
                "t2dXo4vXw4txePLd8cWr4dGjZ9LtZHQxvhqd27Gey/M3F8cvzofheBQe/+3N2dUwlIQdAHp89OgraTU+" +
                "ew1DjN6Mjx59rbNXrevo0R/J++Pi6bZCtP0oJ1Ox4u56fHw1DuG/4yEsITwZgYi6hkUBBg46mrw9G53D" +
                "39fh5fH4O2h9cT2+Oj67GF9D+yeKzFej4/M2sKf+uw9BeeY39F5pJ9yb573W7ry6Gr25DC+OXwOWn3zV" +
                "ftmCBE2+bjW5Gr0YyRLh7R9bb0Fo/1WBf9N6xyWw9O235Lriqt4NNL+8ggYhTODi+uXo6nWoRPjo6RNL" +
                "FIIsIJfhyV+RFoEe3kI7JApoqBj05or/pXeKNCGYs4uXI/uO6tR4ZNCY18UoPPtreD06fzOmfXp2bwUx" +
                "Nj534l1J/1jZSb44Wm/v0Cj4PvY/B9f82IgHYke32LfMrNeoWq5pIKiriwOxM7SmuY8uI414dsdNdb3v" +
                "3VHGsqsIiVQkf5To9+Z8WHtclFzrsbcLwGPcYehadCWTcY6sfG+M9B9NSbP32x+7W+2Pm3fZJbgyl1sO" +
                "FF+SMkTyyELz7tb3rWZuO3FQyn1RD95J4MW/k0/ABluqhG/Zzc9TIOFDk9E9aW8rKrpKYn7Qdw9vUxZg" +
                "MgG8rQkMzWLoPS0EJ3nykatT7cJQSprNS0g8ly3l1L3yBhtD5Nu+pfdPjNOkss9aIWGLg+ulfJn6X3Zx" +
                "KSD7revPsSa7Glt/gVN4/gdoTK1MqIAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
