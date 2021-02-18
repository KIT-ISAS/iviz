/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionActionResult")]
    public sealed class ObjectRecognitionActionResult : IDeserializable<ObjectRecognitionActionResult>, IActionResult<ObjectRecognitionResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ObjectRecognitionResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ObjectRecognitionResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ObjectRecognitionResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionResult(ref b);
        }
        
        ObjectRecognitionActionResult IDeserializable<ObjectRecognitionActionResult>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionResult(ref b);
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
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1ef766aeca50bc1bb70773fc73d4471d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8Va/1MbNxb/fWf4HzThB6BjnCakNOWGuSHgFDoEKNC762U6jLwr2wrrlSNpMe7N/e/3" +
                "eU/SejGm5WhDPSTYsvT0vr/Pe8uhkoWyYsS/Mpl7bapS96/Gbuhefm9keeGlr51w/Cs77X9SuT9XuRlW" +
                "mvaeK1eXXlj+le3+ya/sw8X3O7i7CPwcBi5XBZiqCmkLMVZeFtJLMTAQQg9Hym6W6kaVxPB4ogrB3/rZ" +
                "RLkuDl6OtBP4GapKWVmWM1E7bPJG5GY8riudS6+E12N15zxO6kpIMZHW67wupcV+Ywtd0faBlWNF1PHj" +
                "1OdaVbkSRwc72FM5lddeg6EZKORWSaerIb4UWa0rv/WaDmSrl1OziY9qCFM0lws/kp6YVbcT6Jf4lG4H" +
                "d3wVhOuCNpQDW1SFE+u8doWPbkPgErCgJiYfiXVwfjbzI1OBoBI30mrZLxURzqEBUF2jQ2sbLcoVk65k" +
                "ZRL5QHF+x2PIVg1dkmlzBJuVJL2rh1AgNk6sudEFtvZnTCQvtaq8gP9ZaWcZnQpXZqvvScfYhFNsEfyW" +
                "zplcwwCFmGo/ypy3RJ2tcaWL7At544MxktFbWHaIX3Q/GfhtCpzw4ax3cnB08r1Ir13xNf4nt1R8TIyk" +
                "EzPlySH7ivSTB8NHBYW7YXN7gzgINPf2L4/+0RMtmq/u0iSL1NZCs3DCviIdPYrw2Xmv9+HssnfQEH59" +
                "l7BVuYJrwy1hcrgHrcD7nRdy4OHJ2pP0lgykbjkOqmEmfuO1in9wEtZCcDhE5aRUREF7l6iA0fVLZceI" +
                "vpJSgVcbkeWLn/b3e72DFstbd1megrLMR1oR267OSQuDmvLAMkU8dM3eu9PzuV7omjdLrukbFr2o2S3n" +
                "vC+9qajV76qGvMIZhMFA6rK26iH2zns/9PZb/O2Kb+6zZxVl8gc8gAPK1H7RXTq/z2Nf5RI5lWk2l9XI" +
                "k16CU8oQyNS6upGlLh4SIHpeEym7YvsZPK9xvcp4DsK58zXGazS8v3d8PI/kXfHtYxnsK5QqtZTDx2gX" +
                "NrlvrbtMVwNtx1TUqHz4dhZgTlRxR4i2m7z9E4R4nJrJKe6EX7iAysYDPnF8enHZJrUrvmOCe1VSRqwe" +
                "oCQKWI2IqKAE2aiAqHQDCnBw8LJgvfUfEXuOaBvSNql0qiE+IkdWC6kzW90rSzNlPEIbEQqW4rYpVmAm" +
                "FiqKMdGCWHSkUP16OCQ1xk1e3frsGUvZ0UEWPCBAkKgk58ncJA/XZKh0OtLAFlyPWymFvUMVhIWOGLrU" +
                "scYs6gnnVUX+AymVIwUB4qjxBLYqS5wmmi4Yb6pwdUM6uR5cUllKKcxRGypE/pFdIrxAKgZ7s7tWGChV" +
                "9GV+Td6IEwG/Ak46J4cqmMZNVK4HOk/BwBy4bqROWC9sAFPjmoMCeU5jVzcZj0DIFzKdYSB+ZedIPNjw" +
                "AYCeZQ8diFt/VUU4umctdGWb1atw0D2zHEvZylaf8hKHvb2D3rl40uHwWskO233SSraSeonkMdRhIHpd" +
                "bnWfPW5iPDKGhue4HM1GyMvD2kqScYeyAJK1GUQFRF+n3AW0X7JfUxDS0ZVH2+7jL4kcc/gkZV309s73" +
                "D/+YslZiDOZm0+QBewLLjSWi4hZh4qdKBfnmbjZnfFAaicYIsqDFak6vPHcgLer2L/S9pM7gfKHBgXKg" +
                "GDfh5gyZjbQ5QbcWvu2Ik9PLuIbqeZWXpi5in7rMl/9/uU7fEcYURyfvT58uXBBt31SUyNGXV0jtYw4Q" +
                "Qs8R6nBkEWaIIrKNKHKo6a+r5DlE6YLAMb4BdiVNaFVCOWM9HPlY5rHGGKMNUsMAQOUjzt1oLw1vpp30" +
                "/Zjw6sJ+1GmjGHKtcO0IoIg2z43FbDMvaoHP307dl3SODjMpzhoFBcCOGAFWOIL9M1MLGeCXjokj0GQ1" +
                "EfNo3HGvFaYie6dinCLva973qlEqvCOiFOxvqCra78lBYkSKOTNPcZrkMvvHP11c9s4vnuo2QcVkap6U" +
                "sDZC3vQWGIM53yqC40ft9FVpCEuFMAm66oiYYzkqXDKVsXrI4BN4zxnr6K71oBLCMFwcZbg0ONZI3iBP" +
                "o5+080MbK1l4Fwx7RqzsUwhSVmtFpFuQpdMIA1Q2S/gFjt4nByJE0Te3L91IThDhuLIiGbaKldZgKwo1" +
                "TwVghfYHTj4oN2qIXeHG0VIO+HYW7DUa72qCSHQm8bZG0z4enQ3RHHClcyqc+0QwaKgIcMNXvKlth6KH" +
                "+L9NcVFK7GEdAGUhxBCUpa6uwxCO7aEtdpAnytobSgc0T5qtZENFXNpZS6lQZyNNvPHJpU+cnV70/pSE" +
                "lmwRKhwDS3ZDcpJZ3xQzqvFc9HfmxoLhQm/P2Z1UWwUYSjvYBgF5AmLiIIW/lYWW1csx9Trsp9Rou1qW" +
                "91Xl1D9Bat/wlA7xexGHm3Tzs1fWeZ77I4Xn4DnKzr0qQzYga/mpIQDHaWMiyWRkBLgwvJ8Sb2gEECOF" +
                "GlAbKiu6MlWAFAvXarasRASvCCQEJVxPXYZNsR18o+ivpDYDZOY0i36bIW7caG4MOX64OD15STONOEz+" +
                "ee/DcexFu9RGxxqCxilVN0C2a8VJTTUTIS4YDfbAWbqYRrJ96VRX9LrDLqfW+9bvUGmh5FYac42gv0ZN" +
                "e/GfNVL02s7avqnz0cG7tY5Ys8Z4rIy8n+y8fFkaxD+U7tf++yIKaXnMXNHQvroh9RjK2MGKsfMmI7X0" +
                "QKVO+zUc0kCiyDTXSsUB+6BUt7qvS+1n3aTEJa6bc9dJeuRnB2gLD94FJ2mQO9JicQd7sJuF0StCXtI0" +
                "ye3Q4nvwGAXmz4Ip7YhGC2GRFIHFRUXsfPPd2zdxS24AaPLQU6zdZ3st3Xbx4zEABLDLyJRFY6+7l198" +
                "Lg/TlkierxNr06Hb2o5LE2Ox9M2brdfhM+Eh2qJp7pH2oNGeGlssrlewBwmUbknNZfx6bIq6pA2eBoXe" +
                "TNYaHyd3/1KPl5aX6sUOj/h1XNmSxil2TzYL1M3KBRcMoKMTpyNj6li5ElI/J4tCR09tZxwHgwue32Gl" +
                "dBQ7nuj5GUq8zznpg0qomOnRRzus+4Ardib6pel3eEpeyhkFampG04Q3sqIq34CdF8FNXwRc002jk3BX" +
                "6Bn4RghCddrYoQzNGmDBuh5DK5sUxhtU5F/RRGS9hhhoKlSx0RVnczKudRZME6qg0y5RBtIp6pxZJTbh" +
                "JVZCgAlNYQKkavTkEM/K0I38UMkMNgclAbHAvQ4NQDgUmJf551q7mIEayLvw9I7qyTpljq0iKdttdO92" +
                "SnTB6wPKmHXuCYdHLbbU1RVHgwSpoTwagCWFdECFAaN2oBPA91QXkDAihVJVQ3xaQjQ9KgwE0ic+TDwd" +
                "NFMHvnkkq0qVLomqbXKIWD6iv7BuyGm6GRvqPbkC8FRwiSzrG1PS3FO7q75GyiOsIdBJuNZDuOaLvyem" +
                "IrqFkSY8OD1uhJJRJE0PQLxyzQlrpmn/4gl8dXf/WzDIN89Hs3uwRYo8/q4jHPyMtLqeSH8VNLcRpYJI" +
                "Bde0hsilpUrLmqcsj38oLukhRfCGZ8s+bIjlyYfMF8J6kvIP4eQgOwLbzqKRcbyVyRoqIet040T96OTy" +
                "bZikv4orP8WlXfF6vufVNq9stfbQ0q54M99DdqTHTK09tLQrtuPK++PTPVraFd+2V7bf0FOHLCV5Kg/J" +
                "JCcyhDL7Y3IWMxjQ2Iw3nIb3A2vG4SkEYzFWRQjReBE7BRdqHDpI71VVU5IJWcEpRcjvRqV7cqAxHxk5" +
                "hBOOZQXQXqoxZ8/UODJnX8ot7jZtHOZAkq3hB3VvFP6ldiy6J2g/BML4W2wZCnUr4ME007BqEBr8ND9h" +
                "IdCOoLlS7uMvGd1xGQkgxhpadEF83kRRlk4EzMPosJ7wBuZmeYeWDj2TqpIYS1SWxAIObJgKFv+4FfhU" +
                "t1dQ3JfkdomOUrDnqSO5P+lqcufAwlfdROYqjGUQQbfNu1nz7tfnYv+BzjKJlP50hacnyqFk899rcMfL" +
                "3a0MvqnCw/6FoswlLP05TLZQje/fzWT/KsGTxFaRxJwnZNPZz60WpK6r+PTKE+o642aFeV8V52a6OZaf" +
                "AEUaSjL5AfnF9u02FNWIHIbqqW2xutnean5ocok2Vt+qYlPetnnkrfy0DvQJp3SC77UaJ6voT47WbzsC" +
                "gPTXDmqyb7fK/xJE8d7yz8uX/83LG8lNP25t/9IS5vlMB4n2luj3vrk6/EcAhtrl8H2ISXLMlrK7ImCo" +
                "ZkP2Yw0vthXTne97HgHndy/zyTsMLfgmPn2eM05oAd7522kmvZtm2f8AGoHbbUsoAAA=";
                
    }
}
