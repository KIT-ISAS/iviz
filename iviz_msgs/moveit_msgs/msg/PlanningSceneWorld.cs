/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlanningSceneWorld : IDeserializable<PlanningSceneWorld>, IMessage
    {
        // collision objects
        [DataMember (Name = "collision_objects")] public CollisionObject[] CollisionObjects;
        // The octomap that represents additional collision data
        [DataMember (Name = "octomap")] public OctomapMsgs.OctomapWithPose Octomap;
    
        /// Constructor for empty message.
        public PlanningSceneWorld()
        {
            CollisionObjects = System.Array.Empty<CollisionObject>();
            Octomap = new OctomapMsgs.OctomapWithPose();
        }
        
        /// Explicit constructor.
        public PlanningSceneWorld(CollisionObject[] CollisionObjects, OctomapMsgs.OctomapWithPose Octomap)
        {
            this.CollisionObjects = CollisionObjects;
            this.Octomap = Octomap;
        }
        
        /// Constructor with buffer.
        public PlanningSceneWorld(ref ReadBuffer b)
        {
            CollisionObjects = b.DeserializeArray<CollisionObject>();
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                CollisionObjects[i] = new CollisionObject(ref b);
            }
            Octomap = new OctomapMsgs.OctomapWithPose(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlanningSceneWorld(ref b);
        
        public PlanningSceneWorld RosDeserialize(ref ReadBuffer b) => new PlanningSceneWorld(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(CollisionObjects);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (CollisionObjects is null) BuiltIns.ThrowNullReference(nameof(CollisionObjects));
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                if (CollisionObjects[i] is null) BuiltIns.ThrowNullReference($"{nameof(CollisionObjects)}[{i}]");
                CollisionObjects[i].RosValidate();
            }
            if (Octomap is null) BuiltIns.ThrowNullReference(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(CollisionObjects) + Octomap.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningSceneWorld";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "373d88390d1db385335639f687723ee6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZW1MbORZ+71+hmjwYTxyTCZmpWW/xAJgMTCWBAXY3CUVRcrdsa1FLHUltcLb2v+93" +
                "pFZ3m5CZna0FhwpW99G5fDpX8YzlRinppNHMzP4pcu+yg/TkJDy4vOporhNN9oxdLAUzuTclr5hfcs+s" +
                "qKxwQnvHeFFID3quevwL7nnW7Lgu3cJtn8TFP6RfnhrXssuy3f/zJ3t3/suElWYlpI+i7xkJe/bYUvBC" +
                "2BGrnSjY3FgmtRcWRnmpF7BRsApauuwo0DXkCQpZMDMPRBEjtqV5KSIvqdk7yD72w8x5S8xk0UIYqf26" +
                "EkTHA0wzTmjM2Y02t93BxN/XVuRmoQO+DYzh+QVxIDaJc4f8QphSQLDAyThncsk9tLoF7D2Fx3GbtNFK" +
                "xq2IJDjUKqhoAjUt5FyCQQfAuVGwP4nJWWVlCQVXAMsteSWinoHqNL2CX/XImr3rSEne0H9/HYGHpHfC" +
                "LTe50hPQlvHFg3zoXcdi39S6oEOoFNcwdEt8rjmhyaTrrBuxWe3joRMZy7lmM2AFFDWhVztigYc9XycR" +
                "w75up7SVDAmSvmEkveu02ysK13ejBnWi0iTR5UKLMTve8DX46Eqa2qk1E3fSeVJferIHQal4LopxNlt7" +
                "wfam092XJOZMUDRsSJpbU4a10CtpjS5hKL57aQX4bomVsGu/jKGAcC+5z5cNh84nZDGMks4O3538/XD3" +
                "h2BTVQldkClct3YRD64sfKhR2pH//7GthYFQbXzclOzEKXRGnp4evp/uviLR2NvJfFhckDJiWtw2nt8c" +
                "NaUytkUU6dwQE5avHStr54lCiTnUKCu/HkKUnDNnFGEFaFPGaCRDw0I4IFlEFQM2O6TgSSVs9D7AA55Y" +
                "IvWUidCk14+VFJ0vojvGrEbB7LkuuC0QNp5TNgq5cCkXS2FfKLiBwiZeVjjt8JZyjovZA2biZwE8LVcA" +
                "ImQ/2JWbsqy1zJF3mJcIx/5+go4SX8Wtl3mtuAW9sQhRIp9bpFHijh+HSBU6F+x4OgGNdiKvKT1AktQ5" +
                "zjbE5PGUZTVS984r2pA9u7g1LyiTL5CzW+HR/6CsuKOqRXpyN4GM76NxY/AGOEi1GvG4FZ5dY+mGDEKg" +
                "gqhMvmRb0PwUUWGiK624lXymBDHOgQC4DmjTYNjjrANrzbVJ7CPHTsZ/w1a3fMmmF0ucmQoxUy8AIAgr" +
                "Cy9EULDZOjDJFWUupuTMcrvOaFcUmT17QxiDiLyUTkR+VSdS3QqncU3V63G88Y9LXPbsT3/Yyf6vhwcX" +
                "5Bh/fnPzoVA9MNpzqSlxUIjGqOUz09SJUMJRtCleUGFSWUVYUG/jbw3cCVkHFPB0wIjeAtnTLAQ2W4bw" +
                "gHPDkwsxl6g3XENiZJEK+o1Ypx6jL2ESnsT9yL+UsZGKbUpvCwQImq9ZOkJwSQyLWV8V540NYQALfj0/" +
                "eb+NqE2x8XHv3VsWGYzZXptQUfTbdFzym5AzXWwbEiq5sdRAmFByITe1N2N2OF6MR6Tl14cesjqlYWXM" +
                "DVz2RkzYd/8aEMKDyeDA1Plyuj8YsYE1xuPJ0vtqsr2tDKIDaPvBv7+LJtoQM5oykF4RMqFOx9MLac2H" +
                "w+mhgDiC8AE2ScQkcteNEE22mCsUjplU0q/HG93bhr/CYBFBDHkQ3dB0P/pGYEJWURVqJOOkVOHIuWrg" +
                "RAX3DiGpRMhFb6BgYywtWWAzYS0A4RlBgGf3IZj8+JefX0cKagShJZQD3dcaDxpJ57+9ZTg2J5ZGFe05" +
                "bQg+/6yOEkXkHUSxwe3C7fwUn1TG4smPr3dehSWoLRFIpcxtQ4HkcosEf+8x9ctkSBKQho34tjRFrei9" +
                "p0zoTTVIDg3Xfqza+K3eFRpNY5jOzN0I7Q952ojla6TgMELA3QRVrD2lIpfYT8fYDiVxyVfkEdRvz1JD" +
                "CmbUflCDGSLRhih/OcK/cRZq2s9s/+QDmqr4/fz06PDsEI1OXB58fHv8fnp4hsaieXDy/nD3dYr2lJ9C" +
                "z0M6NVRxZkgpAWVB09DgNknnynD/02v0qx1F2lMKHtq1/oYe2YQJjlJJ9RqdhW9AiI0jwXWXMtWg2zOI" +
                "rVbWuCa9heFB1VFYfRixj6MQrJ/6OhPI9FoJvcDo0mh0Pwc5ZMnWPoA+7rC9/oD+uFt9bLGm1SfqKXsq" +
                "RfwbrYxG8qZjp7SJ3zq0F9QBjZqkElJxtNvyQtakAtXv0ECTB403zvX6bG96/Ldz6td7MtMhB550wLH/" +
                "iqhE10HC1CHvtcMKVyYYTjSfGEf7O2YxPc5Ng1jie310ePzL0QXbIt7NYtjZREVp3ke8swm+vFj6FvMm" +
                "FtgWxcIwyqM8l+RE6xo5cdGT8y0p4NBiF4+vGZUflnlgQikt2lfY302d92MS7XcubWhBxzFkZNX5UMCU" +
                "9hscEvl7XY0isux5A2p2LxIb/FqXumc8TUddpH5F3AFDhI+T4r4eScNVSHuXE5sc6lmoWiFQ51aQu2Km" +
                "HMXTovYgvpexIaLGJDhe2Dtm2SkB1hJkv2HaFlYHvh3dUxkogw+HUSVP/VxztSOTrTye8aa5KQWyu/bb" +
                "uv325WnU76BLNvQv3Tbw3FSeVp873KkBgrP+vkXp2+0TlFe6xElFtXcMdHFDyU9hXg+BiDlIL9Ad/bVX" +
                "PVZc1YKQmNOAZ9qTDAP+StBQKdzlVUYyLhoGqGMtr6xJjDz3NZrDtCPdcqCrratAELR5wJ/ALG16IqiS" +
                "GQ9AlsxCB9sqFafhy52op7i7BnBPpG24AQv3TfcTCo83X6PmJq27aWuv4/gde07T63OWf8F/Bdtl4e6K" +
                "s8kuglfML19eYTlrlz/QMm+Xr2hZtMudq9bjL19fhWePNsb+zi13yK87U0b35nDQmdQYx5uRZESDA+gR" +
                "tw/fMocsnO6ZA92Dd7RxU5zW2QPXjpQoFlInro3nNwxB9eA1/WNfzz8k838G643iC0ID47DxsDBt3Qpt" +
                "GiXGbZPndSVFMQQabF4rAgBPuM7XCYqt8cxvjzHczqUSw2xmjGoYhfsAhUGmd/Hf1BOqh3H75m3/mXBG" +
                "1cGz6X6nHLYNc0l3OpTh4jZt0J626de224hJYwaGKsmV/NJGU9wa/nYRZ10bGqfxkkAI8ybV5VsrfXIc" +
                "l1GXQQ09/V0m+w+XyPb6ChoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
