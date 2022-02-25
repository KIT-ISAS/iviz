/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Detection2DArray : IDeserializable<Detection2DArray>, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections;
    
        /// Constructor for empty message.
        public Detection2DArray()
        {
            Detections = System.Array.Empty<Detection2D>();
        }
        
        /// Explicit constructor.
        public Detection2DArray(in StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// Constructor with buffer.
        public Detection2DArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection2D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection2D(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Detection2DArray(ref b);
        
        public Detection2DArray RosDeserialize(ref ReadBuffer b) => new Detection2DArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections);
        }
        
        public void RosValidate()
        {
            if (Detections is null) BuiltIns.ThrowNullReference(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) BuiltIns.ThrowNullReference($"{nameof(Detections)}[{i}]");
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Detections);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5f62729a3134356dd48cf97c678c6753";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZW2/cuBV+rn4FAT/Y3p1R0mQRFCmKtom7u35oN92k1yAwOBJnhglFKiRlj/Lr+51D" +
                "UtLYTnYfGhtBRkORh+f6ncuciD8Lo0MUbiueXIhWRdVE7WxYia3zQopuMFGv3eY91ucdztdV9aOSrfJi" +
                "zx9VdbIgFfcq71St6L3rXZAm1NiR6JWliZzo9G4fxU5Z5WVUICZARIdE8UbHveikHUUjbatb7FiwOp1q" +
                "xda7DkwHbXdGCW37IdbVRdn55OLtu8W5qvrD//mv+uvrH56LENurLuzCo6QgyPI6gm3pW9GpKMG9ZOXu" +
                "IbLya6OulcEh2fWQgN/GsVehxsE3pAJdJDRmFEPApuhE47pusLohVUTdqaPzOKkt9NBLH3UzGOmx3/lW" +
                "W9q+9bJTRB3/gvo4KNsocXnxHHtsUM0QNRgaQaHxSpIq8VJUg7bx6RM6UJ28uXFrfFU7mH+6HPaSkZhV" +
                "h96rQHzK8Bx3fJOEq0EbylG4pQ3ijNeu8DWck73Agupdsxdn4PzVGPfOshddS6/lhowZYHxjQPWUDp2e" +
                "LyhbJm2ldYV8ojjf8WvI2okuybTew2aGpA/DDgrERnjttW6xdTMykcZoZSNcdOOlHys6la6sTr4nHScP" +
                "ZovgU4bgGs1uSv5cheiJOlvjSrdfyxuvdYCzJ4dcRAKkvFBbbRUYO4p8AdshRuEfC/8LutPkRXA83t0Y" +
                "SKO35H44shKbIZK/mKEFPUS2Zkraws27tIUjGop2NyS0vEUhX5oxJ/SqoVeiAUwILOlO7hTowufAQgIH" +
                "JzZKGNewQnWyKlgkl+Tt9wHUS7qUzLiRG23ApArVTwxtP469A4Wgw79gm1fwI0BFYirQSci8cQOwB8xv" +
                "3AE+4X35TjcngKyrF3nxhTvQEWytWI2KdcyxTVEyIxZdqspN8NJa1fi2I51MIElqoNh2A4FrxkaVxDyv" +
                "xd9cxJGPg/aEgKRBYwgn4NhBAciDEzoCPkfSmOr6ONZVUDY4n7zikrUb3OAbOGK3I44vgeF+UKvkwojw" +
                "QHsQW1FqIG7OB9HL5gOpYGHputo4Z+A0V+Ut07tIyEXsEcwA1QE7o5CQDSbhrNCbBaqXO5E02AmvpRkU" +
                "AscYlt/oD4RRrd5uYWGGfVYJbtDKtIAZckihJBBFwyKI2wGavG3sOok6uoFPsLBMYEW6gpEVu7yBCpni" +
                "LBStv4GG6hLH5cUDhfLn3JbSsC322U+vk9tN9rsvROviqUgrSApkspzJMzUOWZjDwX2jkG3LJKDVBREh" +
                "ESZxTt+XF/C/AUaQwBE4+H5AGl8jr7SMvkwSwN3B0wz7hPJAm70bYMNeeaIrZLK4cx+GPuW1KY3Sfxt4" +
                "eS1eK3Wkn3/y8yU4q/Gd/a5znh1MajNZjYyVpJ5RYRTJSbfAesoiyfVuVzU54sWLkfZeIxFMMBdnh82i" +
                "IFEUjPLSIpLePl7/9l1dbY2T8dl3IjTgrXDy7ILso25pf7Zljgjek+lvUsXUMqJT3uWThUpwyEVbfcAb" +
                "rxAvLFVC05wO0x3FVBPC7BROQlFAYsiHQMssHUEhlMXJcNrRkU3TTqZUsPGEkEolT4yTBPi0ALBcv/w+" +
                "5cMjl0LNR9FYhNuMTBW1DSXfCSbI8SlO7XRHAgfaxG5GmQkIqdl628x1SJehTkzoTCi5l9dZnROB+WA2" +
                "Du7LyhmTv1HsUQy+dFxaJA0jHL8SFPzS5SV3e0W1GESgNJ/0baFiBEvoZaNSZT3ggCdgoMxQEbHM+4n4" +
                "2d2sO/ke2pooJaNkT3h2eAb3n0SGxbw+ZD92Xk/bYS1oOlJ0U2aSyR3X8rDkMcMQIBn0PXL2KrnxfBY2" +
                "p4ry7LAS40p8Wgnv4gJ3xL8FUbyz/J/7l//Ly+clCt8+ffZuIczDmY5bp7v6vWuuFZX9tNzm9wnEUagu" +
                "lV0L2JCCu2yo/j6g1vCW6c77HkpAsFLccUpAGZ50kVVmPDoSd4LHw/Q0Tk+fHob9WXX3hdSRPm+FFr59" +
                "nPVOcIbg+rJE5enmISqIo1KVXfB2kZtqhoS+HDqUWjhyKJUnwE+4i0YezY8N3I4jSEn4HhFuQuoNJp2B" +
                "wsAtJYuKh3Wf/UOkjBlSwYU+Aclos2YiBL8Nc15TtQaG1EEiJaaX3CSwXVIJzLRSI1GYXkq1omI4C4XO" +
                "j7rjmasXzOVedqc0ebAqdy07WDnuu3pRy0/OezaJen47EO9Nl0Vtd2GA+ip+Wa4J+pM6on8fvfs7EWgD" +
                "cumcyKDSZT2wtN5UgeCuq8Px1/HhIDA3pGRoMge+vDIKhR1nC+J9O8DJnqbky3ZAjshTEfRFlJ5oIqJs" +
                "m0OR/Sinmnz0LsKqa+VHlCLUkdpUFMAIU/sp+97kBhUtivyQOMEqPM5TJ09F4/syhYIbZzUbaVWpMLSf" +
                "vIx3UTS4PqJ3o2kO9TOCufLXiTr4XNY+7cBlKu5pUOjgsc7d2Y3iOoWTozEUBq5HO1vmeKhleIiURBT/" +
                "uEy+iRs8vKlX5NzpdqhMoK62SrW5rWG63C7RjMdvAWaszd6gTptVVqPRaLl1WtLhgoIKuo1itVKVny0B" +
                "0SJaw1D0PLWoRBQmY+ceT73iLGeQ8aEhgIJGN4aKFjedtok5EPRK056OqjOdJ4RzvcYVXUgyD5GegRzx" +
                "NJWa81TMULHn20m4OMW2Sj1Ah6PJLqO8EQqR5qfSkeuk0hnTTaRcxegnDXU4YxaX3qUZERloAtXW5X4T" +
                "9CAbDFsyTBmg5Zrt3iRPlTuPYtBP6a0z7S8kFzAX5VebeN6eJhRJ7gwOIDeqTeg1Twh13n32eCUen/OY" +
                "jEY8/dqoLfWr3qaeIu+7NdMR+e9E5OV5GDk1RkI2yAtZiTyim8iJ+/4mWmUwtyBFgYtozj0K6DT49PJz" +
                "hGCunWagSQfuEpqbpi9T+vYwNcV5CJZ6SR6a58byizJ9Ox4TaN2N/XUHPx0fTCjnMsjlhPRFCpdpz6TO" +
                "FCUpXubVTOklq4Ha9s+RmyLueJw688GtO3A7fo4Cp1AFINGOx7pInqm1rMqQG7BNip2PJMJpeVUG3Sth" +
                "h26TzOfdTSinb3QLfu6c5uV7DzfODJ0NJe0btYNr5GqI0AH9umNwzpXVVgNUg28eMeGr8jrUTd/PA60b" +
                "mRwl5B8fqJiQwHkUVnnyzFlmJd7DsDjmXVgDkn34E01iQp0Ggti0U7WlgY+lsRr+o/qrk9rkAWWa5xLd" +
                "wgigKF8xcV5U8ZeyQP0LVzVivRbNXlqLHNYpgJndrVL3x09UiNxvSLIkcrKdp3/l16d0OWXkMpR+tESo" +
                "21rbJ7v/jsZ7G42iokX/ly0XFjP86d0fpx9DouqPGPqeSg34Amxod5FGj2IzRhWqE76Bfn4iSosTqGMp" +
                "06bGmd8mmenqM6b/DTvXebWk8Jty/O3PQJB3VfU/X69zfsgbAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
