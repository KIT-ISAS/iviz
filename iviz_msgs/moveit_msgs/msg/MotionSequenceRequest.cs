/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionSequenceRequest : IDeserializable<MotionSequenceRequest>, IMessage
    {
        // List of motion planning request with a blend_radius for each.
        // In the response of the planner all of these will be executable as one sequence.
        [DataMember (Name = "items")] public MotionSequenceItem[] Items;
    
        /// Constructor for empty message.
        public MotionSequenceRequest()
        {
            Items = System.Array.Empty<MotionSequenceItem>();
        }
        
        /// Explicit constructor.
        public MotionSequenceRequest(MotionSequenceItem[] Items)
        {
            this.Items = Items;
        }
        
        /// Constructor with buffer.
        internal MotionSequenceRequest(ref ReadBuffer b)
        {
            Items = b.DeserializeArray<MotionSequenceItem>();
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new MotionSequenceItem(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MotionSequenceRequest(ref b);
        
        public MotionSequenceRequest RosDeserialize(ref ReadBuffer b) => new MotionSequenceRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Items);
        }
        
        public void RosValidate()
        {
            if (Items is null) throw new System.NullReferenceException(nameof(Items));
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is null) throw new System.NullReferenceException($"{nameof(Items)}[{i}]");
                Items[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Items);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceRequest";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c89266756409bea218f39a7f05ef21a1";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+1cW3MbN5Z+56/oGj9I3NC0YnumZjXlB8eSJ0rFlsfy5OZysUA2SHbUbDDdTVHM1v73" +
                "Pd85OAC6SY2T2pF2t2azWyN3N3AAnPsNfJR9WzRt5ubZyrWFq7J1aaqqqBZZbX/ZWPq0LdplZrJpaat8" +
                "Upu82DTZ3NWZNbPlePAou6iydmlpeLN2VWMBCs8Mx9aZKUv/ir5tC3qa2sze2tmmNQQzM03mKps1WK2a" +
                "2fHgDe/jyj9ftHb18VNW0J9mMHjxT/5v8Obqr6d08htbtJNVs2ie7K9OR/zgzxNwgvO3y6LhfTES2oye" +
                "wrlT/IWxjV2sbNUqgvTEI6CgmBOobGtrQkjWuLJoTb3zJFGUvCPI7z1QAj7AxpzQhXDabq2tAlBB2IgX" +
                "8oRdmR1Q36yco7d5tmmwS5PNinq2KU0dVpPzClxPb79lDCWS6WJ8KkOj8K2yt202c6sVvRhl2yWOcpKt" +
                "rKmarPLbpAXHg3npTPun5x2GejjKJkjkgzJd6puCUDZzVWuKSsiY23lRFYw4ENAEcrYuQSqBCHzu8eA2" +
                "7XrTgprr2t0UuW1AqHemNivb2rrx/ECy4OrrZm1o4XZp2o7MNEu3KXMekdGGCMjgex2dQAoQJuvwEotd" +
                "EfO0IG7TmtZmm3VOf5pxdjHPZrbGGbOfXVG1jS405cNjndrmBIC2s3YNn55oTzvDjo3I+WxT18zFlRX+" +
                "IrGOgwUggQCn2TYDGwzeu6lrr3gvDbY24X0NVLBc0xTQBAtnStlyRNLK5bYEzln86O04Oye9k9nSelki" +
                "KBho6pr4m6lG040Aq+0CHM3L8Avw62xZ2Bs+ZuG5mjbe1oYRIrReA38iFAKCwNPeTVs084KmvoozSDcB" +
                "8iQBgoO9dR75hEpT7eiQ9IV0oSOqMKUNKdVCMEp8km9mJJGJBhZNeVO4EkAEy+k+j0X21uuyoOMSfqBm" +
                "eREiSuXa7OcNVLfZybthumVevL/hDz1EeJW+KZmR6O3PdtY66CQAFlTsBh/C+xR+HH1olYo4VRVKanPA" +
                "ZI60kmVGJZXBAyGfazsrgPcRWBM0NrSt/lw1LQQgH9Ca2Lb/NinyQ4svardZ48ELAwHbLgtiLsatwqV/" +
                "urWtDfCgcHnmBLAC3M1qSoMBuViBIgqCzYITCVuRYrD5OLtaurqFLiFFv/FqRLdf2zU+5uMB7enZUwCe" +
                "qEmZmJa0+jqicmVui9VmlZmV23jDQqsfwiyYpSzdlrgsEabsuIDJICLlxCKqmP3AuCyAsl6ZmRLHnxsQ" +
                "V4TFrbEOTdnR1omNC8/gujXGbXZjSzcjFWHZXoCMsxmJMLBKDDLOspd+czem3GAQiRtt7fhk9OUn+vrh" +
                "LoC7A+Aiv6iA1dA/XpUA8gpcDckhzbcDqWzN7gvxbHFjCZw/IK1MnEgGBAoQE0m3Q0kCaFFjrwUsZLWg" +
                "HR8XKxDOVG25GzH5oWCqWbkhC0CEZXVs2XycjE+GYpllHeZxGyyL8jejAjT9cnzCsEi9C6KPi7Edj+7C" +
                "CAHsfkmRM4z2lwZNdNKkEdJOZEedMen0/riHsNsHLJ9abpK0xiwSy20YkYSzaBChEOYbUv2QMvrK+ozk" +
                "Hqi+IfkjeTkm79bdDsEuqgCUabpyg22NdfEigHYVEYU8nsR5hmis3LQoLRsT9qC8pRLA5PNtbVmOIVdn" +
                "bLWEI2qvoGo7J9MJT05NIW2RDlpXOP/X1pCtJuuKP0EbFKQsiOoySLUcTRuJD+9ZXPlM9tGor0Bm2A4W" +
                "1hHWSG8z5r9jJf4MgCcCtK94/vlLEcPpUvfDWk2by5KCQ3GXqtzUZH9ta8hVMozyZbEgsX9ckq/AXslq" +
                "TTzAX9vdmryphA0WlvbLkgnjg0PDD95UxYyNNwxCOp+FuetlzJyryTvGcOYAQGd29d78xdkpW2dETaSi" +
                "aCVSLbU17MJfnGWDjVgLmjB49GHrHkNbLGBRdXFxMmmz9nZN1ME+TXNKa/ybHG5MsE/VFmTH/G5Cj80w" +
                "o0VoC2SXSDJgMN7t2qUTzXpj6oKjOAJMmqEkqEeYdDRMIFcMujKVU/ACMa7xW8BWAS7O9Jjc0ZyNUbNZ" +
                "GFah3t32Op2UAil40n5lMa0plhqwZeQlB49es5SxjWaKQMqbhjQhESBnDlZLz9Rg/+F+uPGgFChrkS9A" +
                "pLLwTQzpd7EVZLhrSyeBRhxnIfRkHYRoi4UuzIS/VNQwbN4PJr3iaoo5KT7JnUVohjhoZa7hryN8hy9J" +
                "biVZVvhxVVOKUaXXNOXYjhfjkeg6HiUhJEFgGShmWV0syCjyTFpoFSabzB+OLN/8qbg7vGdZjAhGQGrX" +
                "eisFw7tzG/Jg6Qz0j9qLHntSui9mkda5EeTOg+gi9B0bQDUTZCFaEnrSuWrfbsO/duFfvz6ATYsR0Z2m" +
                "rKgi/syUPI+uJm1BQ3IDJIKIwRryCI16tIhJSUiumwFo62pZ/Bt8lHCMx8Vw7BvnfX9SFUgXLM1N8JZs" +
                "dnb5WuKg4F9xdJeCfoOxNC5ZgqdPcjef9BZ72bYUixGUmSvLosE53RRxA6kDo9+I4A0RlE+RucSdGw50" +
                "/iudfsmzKSDT2ZMAeeIhY93XpVkQdnNoZ7AvMbOPcOGHzYiPo8/OTheJUsvqkoVpPt8zcbxDCZN5fmIa" +
                "Vq5poa3VORTTorqLA1c96tTl8I+PdT80EIEcrElpTX1oMC1EBDeetU5PyXLa09Mk3J6ytEsGAAsWPp3X" +
                "Jiw3HEydQ2g8weHuzeweZMAEUyaIALPf0pV5E4SefOhZXUzFR5LAmg/uvT3SK2QoWXZqx+ZCBAD60Xss" +
                "YRLSlt4xPq4tPEC8r8l8FA3EbTbEbiSRAFsCG0mmLBUz9XcUisnZ6R6O4tDojfeHPml48BMyrBDPOMXO" +
                "iVZtsNMS24cYxgN4u8Lst+TG42Dn8SzwRquCsABuy0nzi4SSLYQ+4FBVECE+YxBiifTZONK60f0VqD4b" +
                "w3lJsiLkpCB+pWBtb4w4NcjkKhlZeYgY6dTM57kaNr8F+zkHFGBTNOIxm6h8OL/TjHgFzT6zB9XFMG+G" +
                "zWKa84HrDgvHk/x4Ojs4TOPXcfY9jBrsm9gbr0L5FJUjgJ4+PUcBsFYjtlUzCqpJUm9sSk7JSyFq33lu" +
                "BPbkNELb5Ow+HbdUGIynpviVdD0dGXlzhpNIDWccOfygVZA+UR4I20yQw8kP3TR8dzgGcDOZgmQW+3GF" +
                "OEGkUZl/vIGkRxWE5JUyfPJKMPAQCmXf7NCp3qsPZFRmRQeAezxxGReBxXK7IM+KGQ8uVu6IrkgGOASA" +
                "qq4JJZtZu6lZl8T1hJHFFSPUU1SZiyQblU94/M2OHJCVYB7VklxSd9FdkjkL20b5R3YjpA2vSSmxkspm" +
                "S/ISxtlriMItObUl6ggcf5palYVhDvv7+7PXrNOewYAfU4i2o/83W7jSkrQmv1w+goO5OhNDhHR3xtcR" +
                "iCsIisyFRHe+szeJEQqNBPrGIqWVTc3sGgfu7OH/1djDqrEtsgrL36zGdPj/JTV2lxYTBxTTm1764YOy" +
                "MI0K7Lw3aEsExQD87X37ntFEHwVfDxMuhl0fCBhZHIJaCRW7rdvLNTS9mHIw0GRMEv8N/rYxyD9CAWiU" +
                "9jCHjAsfCotJVusi6PjOQfD0S9w18PDZyE//tX0gAjIn+WOp1m2i89g9z7R21xaH5Fi8QUyEuAB62FQL" +
                "ziNxrnEcCOiHxGc/7mFOJzJxgGpECiFPPNyI7Dlyti17vC0U1W88IgOLjxIKPERe+o740+vs3ttQ3wkR" +
                "nG+rmBe3Gq5ItrTgU19rAgr/1koT47FTpTbkiqBoujRrKa1wbjWWT/f2JoYsDa8xjBd8dDEHPJYjkAtn" +
                "ZZCV84H5uMh9rWIkbR0+qH50CJ6G2GK9wzGSQhcWQC21iygBqolmH+BxEiPWycN6mjBAWiLJ07duw4w3" +
                "3YVqyuOwsYnWFU1Zk4XYJaXvZEK0GgxsImkU+HixiC67Yq+CHK+ZBGudvC5Rg33F0AMiG5YU3QowwlEI" +
                "tM+ukVJD6wW5z5Lym5VOWjVqt2FB8FB8EUnXqOwM9rfe8Wq1LSU97KsOfmGQj2Bq7iap1MbIPBZ1CRnY" +
                "3cQvoUTZ2mKxDB5LjxgjlNWvK7etojbl8Q8hk/uy+NK7ASPJzc85heqTOurTs8wcrqkQx/tjegQeM/cw" +
                "LKLeG1r7oh2qsMY6s9J5t7bCFMhkTI20RzF2gvTI3wmcy4V0nMhZ5AgfAAFgYpFes2Ve23JRdd+bU5mV" +
                "aUXtNQMk5VCJJk3qKQKuHAqcuswMKZJVgfpDM2CNI/vkUe/0E6LDOKyfjW063yeCeFrpjW2WXah4Q2NX" +
                "8uEgHHyLIL6CcGjRH3k0C+PvtVlsIcimm9hrYzVGESciNkURxfK8EJeaETdM94YeIj4Ir3THIfEt7u5l" +
                "njcpG3msh3oh5w05650MIh69KdymIT/Y3pKngO0XrWhnUTjjwXRHftzLs7MXJwOOeSENnZXmtVtJQqK6" +
                "KWpXcdsMAqsa/vWxpdhsR6qJRYHTvi0Jc9PjiSIfykrvz99cfnf+4ks+03oNPYUItgrn4pjXK1bedBPa" +
                "XP7hWbUYIZP0nESFeMh3787fnr146pVwXPPwcrzKiLTi1nO+JzVXRY65/8PTTcMY7myhEaWdtxKiDKVJ" +
                "qHElcEWoVY0RtWlumwJNU7xFxs0zbPByrd0IYnHpEQ6oDnT6+b6U4ueVyuDR7/4vu/zqm/NXH1Bw/P2T" +
                "/X9Azqt/XONgpclh85wNnldkpMaQqEAI01gJrJMqf+sWkjYPoaOkcIlPkCXvOBXXNuRl0xVO+Y3Mj8mH" +
                "WhlqQRqryvKpKnuCogDzaboVb2A5d/LN1eXbJ+j88QmVH1+++TYTAOPsZWBhUrNBAJJaHBS1YiUmjcSo" +
                "q0EZZ+fsNRTVAaKzHHFA79w1+SvX9jT7w38cAcNHp0ev4NmcfXU0yo5q51p6s2zb9emTJxR+mJKw3R79" +
                "5x/kiDV7TJWTbE7lNaNQz3s3IE6CBXiORXtEk9BbSVJwba2vQs9LEtVpURaaBLCH+BXFDEGi1hbPvhLe" +
                "YCAzbsc1mr+SPAiYy/cn+qwY17iRJfOH5XQ+gznNAgL4HVBA7/ooOP3jv//5uYyA6ZVSKo3b3/GRX+nq" +
                "b99mRLbGooYR6NRZ+OqX8msdIbB5qexou2ie/UneoGJ0mv3x+bOn/Eijawwo4Ob6EWT2txTM917DQ8FB" +
                "dAEtfsnXlcs3Jb5z+bR16yNlaGLt+8rV3uUtxOYX7h1p1uC0UTbbkWvNTtsMiTKfbdIop7ahPENspVkm" +
                "8nCm6gIQMCh8mHSWRHGcT0b0f+MB90r8Ofvq8gcyY/Lvq3dfn78/J9Mij69+/Pbi7dn5e1Ll/sXl2/MX" +
                "z1XaVT+xlcGe/Cjx0lQlFGRoG63JxqExPR5HhK4aa9hAphOSYaeS+OOWSpQStdkVY4GuW9VUR3HOkRi3" +
                "gWdNfKWD81YlevhhlP0oudyf0j0DyRww2WrRhmRjXwchbGqS7p9xxO3kB/JI4tOPAdd4+glWPNmS4N/v" +
                "ivOAIDvUJv31WeEG3o8oFVbFvkVV+9N9mCMcNO7QdfL+5dnF36/gISVrKpEZJggsfT2CFWEdTj9wc4W6" +
                "h5yJ90v9lBlyOKQ7UboqOnAnX59f/PXrD9kxYPuHYTyTlG4TjMczLTvhlcpCdgxZGMp60HO6jpzOryMP" +
                "yTp3rYJui05vvwYnh9d85SpJBugnmh/9/L5MTm24VSDN122xjjzEOMV8BJvSjTfyNY4vPFIHPUn0+Ass" +
                "1Ts8/NEoqXuDI2Iw8H5U3H4QwMFnvVeEgjPaz30xteAeyHcpMgPbSXJznA2kmyUU35KUbDLuoQ5YVCFz" +
                "2UlJpUVy4/veO8f9TAr2/k0QQks1PMlWEU5CQZT+PhQZRVMtyIP4S6JhfX8yd2dyJ3XovKEzosxFzk7z" +
                "8dMAa3zwALim4GENvPLwiTudobHXtdXWVN7NAZxzqVUmPRCq9BgHUKbHOmripqQT8eMz2ae9nXAe8EF2" +
                "y3H5wcqvVEbtyMf3Mf4PSQJzm32B9N8X2exX+p88e5FxRG2y0xfE4Hb+8eQTMorh8Us8zsLjUzzm4fHZ" +
                "p1Bq+Pj8E7+7LwR8JofXy2sdLIb1piijyd2R/6F9q4bh4nBy/0Q0Sqz7+vb8IIgfR8ltA3ro3DT4BCq5" +
                "7mhpU/gUcubJWmLK5LIi8hDeDw1pke5thVDqRJG4RujSrVmyjuidckxHHxzorGj2WyvQ7xVfdo6133SR" +
                "bzT9QLZ/ghzQhNuvHyYJG+/83N2oX95x7TS5MaQIT+8laZImXMbyV7a4Rq4XbUI+n69oaAVY2geE2eMe" +
                "gyh0riq983TojFPidIdeRtvbGZ3Y5O6E74rGR7+d8TfhdXf4/ROshxHJ0MjDAZMealVTSVBweltLIhyC" +
                "Kb6jilGzx5/9XL2IBwf7Y1jiMX0FX1czO5kS429Hcfkvkm9mSgf4FPyGfkdSf+SB9wyd85i+QBGvNsV6" +
                "TaREdpzbyrVs/FETp6hTuzolvyENoCn7Zq9K8urYT/jV1s5fhiUvoIkNocN+leT+yb3P23eKKWiW94x+" +
                "IEc86n7PhQS00q3M1cA+NtmDQtruUKFTUkLzOap/x54JGQo3LAyjqjb1wrbeJLhk3NayUk6v49xx4URA" +
                "TBjERJbUDfjbQXfuPBtozeE7GRkHTeTa6P8+7noA9uoiJWZ4jCKUCPrsTFDjy7vNnYWmfcdYqeI0wXvM" +
                "OcIN4qDhb61LDcLVY5jvtGGsF9QiDUim/zcXsi5i5YjDgwBt5EtZoQwRnAjUT/JQOg85rHA3bUHk5t8I" +
                "2D9it0h296H80p89UFpRu39GOWg4f48q6jb/fF4d+VpNZ5bPSiSaKlKGGSu2EA3uakzqht//TdWnnXyc" +
                "CHnM4VVm6xoqQ01XUs+Ml3anfJnYTm4nmDgJg/dH7D474tf+iH9FPXbITQs1+HDeeAuUuzML5tbEl5MG" +
                "37xoZlJ0FHsz3OsUCdf+mPt5PPcgdnJzJgDeSR5tKyWxYs3FUikUOVzBCowtbdIA/MZvjpVL3B9WAyiK" +
                "fWnoptb4WBg4VKXcVDqfRQfH6dgGoL8lop8mCSHFDwN4e/mBgEvb14p2gKujyTVlOm0rfB1/WUR3HpqU" +
                "FXX4eYNawBZtgJp4A3pzJ7bHAhc3hd0maWLBCjH1vjPEGvqYe6kIB2Za7nxD61B/XoAZv9mgc3dTs7oc" +
                "B7Hv5FSZjGzB4g9tKI8gOinkhy+icyrOiP4mStTnKcC/aGSp19pvNGvdH8l3uGZLXFDX+8aBQmKFFEt6" +
                "67j70xdBhY2kV1kqagdMx5W/3hqcqYZVxmttPRB0K2e2bmtq3w6hNA1bFqY3fRZLOMv62zG1I+4xsWlj" +
                "hd/Z4IpEry2ebyvKmOcyYCR3sY1kJkmZsYfR9Hnb//YHYpVsvSMcFXmQULmd78lqyi0aCGjgM+ZrhxKE" +
                "3MjFhieN/gxMoGe4at6VROHdyCJaHhUCkY/kF4z3O3vGiwBEs+/RzqpBerTiTxXFSwphXKgTy41RdF+3" +
                "HSJBZxGO44VSRQYDW2uzzabZ8GVovr1Wb6z03CQNNfsyh0SBFHTJAt14n7sgqITHu9nNXwFRdju/Yc3h" +
                "Notl5Ce4EnsSNzqkxVQm0BCTNSvDwjK1M7OJUnXAc+BVDnZUeR0j+NZfuRk8SmUJY1kzeUjXxEq4Mihd" +
                "O4a5dRSZZWbknuve1j360I5DbvLScKMK+gZ5hRPvTEothtcTvyK56OZv1iI1zZ+8Jvb6skKhv9STJavz" +
                "6VVBB8bQy8rzgwpAmVpEiMPwfue6Ggn12OKeRbYNN9QA2fgy4l0GodQO0xNvcbfO70c8YEI92kzXHMfa" +
                "ZqgQUYQpbYvWETV2fuXD4N8VT57KCil02he6okSFDsepsgh3CvnEaC3yh/VmMCHMnh7wv1QVrIjxML0C" +
                "d2tpDLc1vUYn6cmI9ycXqU9C1267R//ENOe93wahcRPJ/Ac511lsBRuKbeiQ5U5cn3QGS3yJ2Kd7RC0k" +
                "7Nt5NQ57PKTMBY/Ud364quKuXylj9o1L6gt4wyZslzAdzoK4KvxCDNvIgDXvdPTQw61g+hMuHVTx9BRX" +
                "6XZTZetCV3RPYNRD0cxGcH5+yF5kT0fZj/Tny1H2E5clfHH7/O3V5fvJTy96L2Kp3b/4ITQ2eIXJdApr" +
                "/8s493caEkYAnlWP60+v9K/KCDPqT2r0gkwGILbofvafBicHf3hLWY/vahFHdX7Py4W8eHKfMNY6er9p" +
                "1slA/xe6JtrHJFIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
