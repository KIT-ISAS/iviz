/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionSequenceRequest")]
    public sealed class MotionSequenceRequest : IDeserializable<MotionSequenceRequest>, IMessage
    {
        // List of motion planning request with a blend_radius for each.
        // In the response of the planner all of these will be executable as one sequence.
        [DataMember (Name = "items")] public MotionSequenceItem[] Items { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionSequenceRequest()
        {
            Items = System.Array.Empty<MotionSequenceItem>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionSequenceRequest(MotionSequenceItem[] Items)
        {
            this.Items = Items;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionSequenceRequest(ref Buffer b)
        {
            Items = b.DeserializeArray<MotionSequenceItem>();
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new MotionSequenceItem(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceRequest(ref b);
        }
        
        MotionSequenceRequest IDeserializable<MotionSequenceRequest>.RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Items, 0);
        }
        
        public void Dispose()
        {
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Items)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceRequest";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c89266756409bea218f39a7f05ef21a1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cW28bR5Z+J5D/UBg/SNowtGJ7BrMa+MGx5ImD2PJYntwMg2iym1RHZBfT3RTFLPa/" +
                "7/edU7duUuMEO+buYjYDjNzdVaeqzv1WfGC+LZvW2JlZ2ra0lVktsqoqq7mpi1/WBT5tyvbaZGayKKp8" +
                "XGd5uW7MzNamyKbXo8ED87Iy7XWB4c3KVk1BUHwWOEVtssXCvcK3TYmnSWGKu2K6bjPANFljbFWYhqtV" +
                "02I0eCX7uHLPL9ti+f6DKfGnGXw2ePpP/u+zwaurv57h7LdF2Y6Xzbx5uLv+ZzjlO3ekgBaioL0uG9ma" +
                "4KE1eApHT1EYxjbFfFlUgm4O9IceEgvlDKDMpqiBE9PYRdlm9dZRxWPlDSC/dUABfMCNWSUN0NpuiqIK" +
                "QBVnQ1nI0XaZbYn9Zmkt3uZm3XCXmZmW9XS9yOqwmp5X4TqSuy1zKKjmF5NTZRjFb1Vx15qpXS7xYmg2" +
                "1zzKqVkWWdWYym0TC44Gs4XN2j896fDUIYmboFFpK6Spb0tgbWqrNiuxY54oL2ZlVQruSMMsULS1CV4B" +
                "InC7Q4Vdt6s1GKI1q9relnnRkFZvsjpbFm1RqwRx4MbWN80qw8LtddZ2JKe5tutFLiMMNgQgg+/96ARS" +
                "gDBehZdc7Ar805K+TZu1hVmvcvxpRublzEyLmmc0P9uyahu/0EQOz3XqIgcAbGdlGzk9yI+dcceQAGGD" +
                "dV0LI1eFshiEOw5WgABBZitaQ04YvLUT217JXrCjuh3LvoSFeWjbNCX1wdxmC91yRNLS5sWCOBcJxNuR" +
                "uYD2McWicOIEKByY1TVYXKiG6ZkCq4s5mVqWkRdk2el1WdzKMSF3ciJsvK0zQYjSGuhsS5ULBQHw2HvW" +
                "ls2sxNTncQY0FCGPEyA82GvrkA9UZtUWh8QXaEQLqgilM6hW0EH+Xdt8PYVQJnpY9eVtaRcEolhO93ms" +
                "4rdaLUocF/ihspVFQJTKtubnNRV4ttV3J+mWZfH+homidAGn2NcLYSS8/bmYtpZqiYAVFdvBu/A+hR9H" +
                "71ulAqd6nZJaHjKZhWIqhFGhNWQg5XNVTEvifUjWJI0zbKs/1xsYAMgHWJPbdt/GZb5v8Xlt1ys+OGEA" +
                "sM11CeYS3Hq4+KddFTUOXM09XJk5JqwAd72cYDAhl0tSxIMQywANSOwuoRiKfGSurm0NJgca7WLt1Ijf" +
                "fl2s+DEfDbCnx48IeOytyjhrodhXEZXL7K5crpcmW9q1sy1YfR9mySyLhd2AyxJhMsdgwaYAkXKwiNfN" +
                "bmBclkBFr0yzBY8/y0hcFRa74jqYssXWwcbCLcnWBLfmtljYKVQERbMSDTOdQoSJVTDIyJhnbnO32QJa" +
                "VsQNWzs+HX75AV/f3Qdwuwdc5BcvYDX1j1MlhLwkV1NyoPm2JBUQRCcGPFveFgDnDoiVwYmwIFSAnAjd" +
                "TiVJoGXNvZY0ktUcOz4ulyRcVrWLLdVi2VDBVNPFGhYAhBV1DIUI7J+OTk/UOOs6wuP6yVke4W9BBWn6" +
                "5ehUYEG9K6KPy1ExGt6HEQDsfkmRcxJNMAaN/aRxo6Qd6446Y9Lp/XGHMd17bF8w3hC2JpsnxjsTXAJt" +
                "0SZSJ8zW0P4UNHwVlQbRJ7ZvIYIQmWO4ufbuhBzjdYDnm67ocF/qJdEBDKBtBbrA70m8aErH0k5KWDba" +
                "E/GjnLFSwPD8NsViMaJonYvhUqbgtjm4LmawnvTnvDXEFnHQGgLdDL4uMphrGFj+CQqhhL4A4XWQV3SY" +
                "NlRn3nG5ZzXdh9hkUZe2KQbzwgJrUN2C+u9Ejz8m4LEC7euef/5S4Dm31CfjrqbNdVHFIrkJ/kmVZzWs" +
                "cNFmcJgywfp1OYfwf7GAxyC+yXIFNpCv7XYFnyrhhHmBLYt80gTx3HSI11U5FRNOs5DOF5Hu+hpTa2u4" +
                "yRwuTEDowrHOrX95fiY2mhEUFBVWgoKpi0x8+ZfnZrBWm4EJgwfvNvYL6ow57apfXF1NbLa4W4FA3GfW" +
                "nGGNf9PDjQD7zFsEcyzvxnhsTgwWwRZgnSAcNBtvtu01lK3IUFaXEtEBMPTDAlCPOOnoJIHMbZ/B/lbW" +
                "g1eIcY3fArYKcHmmL+CU5mKSmvUcCKTNVafbaXboBah56MBFOakRVA3EPsqSgwcvRNDEUgtFKOhNA30I" +
                "AuTCxN7eCzXoRXwyhtwrCkHJwSkAtXAOqrdb+UjmmdUFDkPFODIhDBVNxMhLRC/MpONU1rRwziGGdrE1" +
                "4k8EKrmFiYLTCBjL7IaOO6N5OpXwL2Fi6dBVzUKtK15jynExmsMEicaTURpOAoKIQTk1dTmHdZSZWGgZ" +
                "JmfGnQ4mcPZI/R7Zsy4GmgFIbVtnrmiBt3YNVxZnwD9qJ33iUvl9CZe01g4peg5EF6NvxBJ6YwE70ULw" +
                "oXm9obsL/9qGf/16EOMWg6P7bVpZRRRmE3ghXZXakoxwCTSaiIEb0wqN924Zn0JUbpoByWtrXf0bftTQ" +
                "TMbF0Ey+NKowmD24zm6D51SY88sXGhMFX0sivRT0K47FuGQJmT7O7WzcW+xZ2yIuA5SpXSzKhue0E8YQ" +
                "UAqZ/waaN6CpnILOesDBycDPf+6nX8psBGd+NkyK+zR2kLnui0U2B3Zz6mhyMPjZRbv0yaZg5ei/iwMG" +
                "aaJygOmmPM1mO7ZOdqghs8xPDMTSNnAOTXAU1cB4DSZBrD/qxOb0lY/9fjCQQR1tyqKAotszGAuB4Jnj" +
                "rbMzmNDi7CwJvSci8JoN4IJMOXHzbcJyJ4OJtQyTxzzcJ7S/e1kwCAA1cZAC4cBru4DF8KIPl3pal1QA" +
                "TKXJ6eTszvODdoHFFPGpQXWgSGWAWtJ5L2ESc5nOTz6uC3qDfF/DjpQNJW4KnxALi3tGo0JjCZuWSpr3" +
                "fTyULBcf/GQYh0bnvD/0YSODH8LCUkLjlGIGcrXBYGuoH0IaB+D1krNfw6vnwSQzol/omVYlsECGy6H/" +
                "VUhhFKkSJHJVRKj/GORYA3+xklg3usIK1SVnJFMJWwJvheEsYredMerdML3rySj6QyXJT+USQkGxw1hT" +
                "j9HXgQ3S1KLNsqh/JN2DVAdX8ClpcaW6GJbNiHFMU0B042nnZJIbj7OTw3w4OzLf07TRyqnVcVpUTlFZ" +
                "AHT06XkMhLUcisWaIsaGsPLUkZyapmIQv3XcSOzpaZS2ydlddk4x5/HUlL9C3ePITKYLnERqJAEpoQhW" +
                "YTbF80DYZoIcyYX4TdOPp3tAf1MoCOPYjzHUG4JSFf5xZhKPXhCSV57hk1eKgcPolF3bQ93y1jtDaklB" +
                "eVUDZCBHX0FH4LK8mMPFEt6jr5VbkJbpASgVu/FKG1hZT9s10wMzExdUXlafDNhHkMn8MNOBXkTp/Tdb" +
                "eCISRGkVBSwoPlLwm3TOvGijCmC+IyQSb6CXRE+Z6TV8hZF5QWm4g4O7YHFBwlEYDKcvWGyozN/fnr8Q" +
                "tfaYZvwYERtSFttsQ7da09jw0fUjmViqNjFcSHeniMSfugQUnUuh7nwXt5IjPDTI9C3y0NRH2fSGB+7s" +
                "4f812WE12YZJhuvfrMn88P9Lmuw+RaZuKKc3vWzEO8/CGBXYeWfQBqaJA/i39+17QRM+Kr4OFTqGfe8L" +
                "HsW2B80SKnkbu5N6aHrx5WDg0zNJLDj42xoT6oo6wEdshzpnXHpvlAztXjPwVwXVOQuffokbJyo+FgiG" +
                "f20ORkZyVDiZV79NdCS7R5rU9gaUBDMzOm8YIjFMoEJGelqSS5KDHAUyuiHx2Y071AFVPPbRDgRRIsXz" +
                "DWHcmc6lbuIZ6V3+tlMKsPiokcFhstb3xKS+paD3OhSAQljnui9m5Z0PYDSXCmwxAva5Kf7bl6IElZ0y" +
                "NtL0a1ZVrzNkKwVTzLzG+mpvF4MH6qGnMTeHyYIPkIoBPBEoUoyHFZCo1WlMPULGR4sZcAaY13GR9oN9" +
                "8HzcrWFJOEZSCeMCLLZ2EaVAfRrahXyS2YiF9LCezyIwV5Fk8Vu7hkpGlXobyi1fhI3pNiTtj+Rqvk1q" +
                "48mEaEQE2FhzK3T5YpVddyXhEvwwlnj7KV9QQ1zH0CeiG9bU3ZIwwlEA2mXdoN3YngGHWlOB0wUccObf" +
                "UJYUWXBQXJXJr1EVU5pj1HC5Wo2ijmSOXU3CLUzyAaZP6CSl3Bisx6ovkMHdjd0SniibAmnz4MD0iIFc" +
                "1czcVHYT4gU3/jBiuUccnznHQNKHuWAnJHu8ly9is7/oAqZ3J3U4PBYGElgg4Css/hI5KievsRbtSY1C" +
                "gvIF0xuTTBupBEFBgPTvmIHzXLtS9DB6hneEQDCxkO+zaE7pSuF117/zYqvTUNJU5UBh2VfDSZN9HgFX" +
                "aFfKwzJT5k2W2OAtkCVKR/cpo974TwwZ47B+orbpfCdraQvNq6K57kLlG4xd6oe9cPgtgviK8uEbA5hf" +
                "Q2XFeQhpm4GZuAyrDPNRizoUsXcKFMtzoQUULJc4SffGRiM5iKx0zyH5Le7uWc7oL2EMxXooKEo+URLi" +
                "ySDwKLpE1g084+IOPgO3j8yemlTROaPBZAu37tn5+dNTLvNW9GpnpVltGWjCDa9uy9pW0lrDpBGUBMrN" +
                "KH7VKMuoKEg6uIU8K4Sk2J6f6EpvL15dfnfx9Es502pFVcWY1nOzi4KdbpVNu4DhY2f1dQqd5M8JKsRD" +
                "vnlz8fr86SOnh+Oa+5eTVdAvU2wc5ztSS8EELTYItBzdfGAj3S8YsShmrQYtDJqh0NDKQVwBtV5jRIWK" +
                "dCUwmesWBTePucFLbSvRsghg4pHOqB/ouk4+pUv9cbUC9fi7/zOXX31z8fwdK5K/f7L7j/h5/o/LH6I3" +
                "JSs4E7PndBk0GbMXDGrgGzS9ToDWzjWjHuJJTe2CVZhA77gWN0XI16YrnMkbnR9zq1KQE46B0qpMPvH6" +
                "HlA8wHySbsWZWUmofHN1+fohG4RcluXHZ6++ZWKJPZPmWeBiaNogA0mljrraYyVmktS0e5uCvjnxHZiV" +
                "3aG6iJJE+dbewGu5Kc7MH/7jiBg+Ojt6Tv/m/KujoTmq0T6KN9dtuzp7+BChSLYAttuj//yDHpFtO9Lx" +
                "KSmeyilHpZ7zcUicBAv0H8v2CJPYgglBuCkKV6aeLSCt6J9AuOP7UvcwLOscikRfeTz/SnlDgPBUFH23" +
                "siZHyFyujdGlyqQIztSZO6yk+QXMmQkIkHdEAd71UXD2x3//8xMdQeurhVaM293xkVvp6m/foqwBL4G1" +
                "jUCnzsJXvyy+9iMUtixljjbz5vGf9A2LSWfmj08eP5JHjK45AE603bgRsPxoFc17r+mk8CB+AV8X06/o" +
                "llov+F2Kq61dHXmGBmt/uhzufS7DZ7FHRlpMmhWZbWimW/jY4rqB4wrjslA+3AFn+MoNOMtnn+DnTLwj" +
                "AGBU+zTsIozqQZ8O8T8kBdhP8Wfz1eUPMGb676s3X1+8vYCB0cfnP3778vX5xVsodPfi8vXF0yde4L2K" +
                "ElvDPblR6qt5rYDCB+ILV7GNQ2PmPI4IzTeo73P76YRk2JkmBKX5koVG3xbLsUTXnVdWR3HOkZo4qYq6" +
                "4BAHl61qGPHD0PyoOd6f0j0TyRI5FdUcLqNvFu+pIcZP4XxA+ijidvwD/JL49GPANZ9+oi1PtqT4d7uS" +
                "/CDJTs2Jv67uBQWq+4RSozZ2zay+md3FO8pBfh8Kd/z22fnLv1/RT0rW9EQWmCSw9v4oVpR1JBUh3Rfe" +
                "SZQMvVvqJ4NeKXYThraLDtzx1xcv//r1O3NM2O7hJJ5JC7sJxuOZrjtxlpcFc0xZONH1qOr8Ono6t44+" +
                "JOvctwrbMToXAXyIsn9NWG3NCvhPmB+9/b5MshrgriBomzYKL5GHBKecz6hTm/aGrvbxuUOqF9IeMgNL" +
                "9Q5PrzRK6s7giBgMPFAmjMGARqH1Tn2KXmk/FSYEo5Og37UETYQnGU9k5LXjJZTmklRtMu5wZ8RmQrKv" +
                "k6FKq+hIjfjSWDzxR1Kzh7BFDDSDBUp2y+iSmgLBtkohGtaqObyJvySq1rU0SzenNF+HBh0ck3UwOD7N" +
                "+w8DLvLOAZCig4PFBZJUnp/hQzF4ga6VVXazB+9SjtVJB8OWP8g+rPmTwekL+9LOxfePdavF3ViSgwfa" +
                "sMTq+wvEWkCF1GnQH5MCIXOQ3ZnPmRb83Ex/xf/l5qmRMDszZ0/B6cXs/ekHZhrD45d8nIbHR3zMw+Pj" +
                "D6EW8f7JB3n36XDwkezeZ718196yWW+O5zi9d/I/tvWgcKQnJrm+ogomtru47v4glO+HyWUFPHQuKnwg" +
                "rWx3tLY1fAgZ9WQttW9645EpCuechoxJ97JDqIuyNwZVjaZX4BR90TvmCGcf7OnEaHZbMdgiFl92jrXb" +
                "pJGvfWYCDsGY6SH26tUHS9GGS0P/qM9/cc/11eTOkcd5erPJp3DCdS536Utq6v6qTkj4yyUPXzHWxhdl" +
                "+bjJIBCdy05vHCk64zx9ukMvo0HujE4MdXfCd0juamDcGX8bXneGH4RmPZyQbuFpj50P9ayJpi8k/+3L" +
                "JhKdeZRHXeMNoXx2c/1tPvre78MSqOUgHYdC9bQYT8D+m2Fc/vPkG5JJt8WH4Ez0+5j6I/e8F+iS6HRF" +
                "jHg/KtZ0IjHMMfJFlj10yKyCsAhIfTuoZj+0czTlYPMc1R31HH4tausu1cIvaGInaby7pZs4CMV3Gfx+" +
                "YXX3mjpuQKBIPO1up4aGu9rpLEXDPkLFrWJeb189VHNGsxmLhLxcEEqP0uNwEnV2ViMt6GyDTcZt2GmY" +
                "pKUx4Z5bKwoCyXxyqy7pN+CuGN27czPwdYnvdGQcNNbrp//rGOwwOqWLliQFlHmcgqaPzxU7rhDc3FuP" +
                "2nWYPWHYUCxw2R+XMbVrq5PfWr5yRHaVs7Rnthf1MlUIN+A317texgKThA0B2tBVvEK1IjgULLPkocge" +
                "klzhjtscFJdfHNg9YreWdv+h3NIfPVBSeDsIr+y1ob9LIXVbhj6ulFxVpzPLZS4SfRWJI7wVG496aLsn" +
                "Pv/vKkDfBSjJki8k7DJFXVNxeBuWVD7jFeCJXE0uxndjThyHwbsjth8d8Wt/xL+mNtvntPlmoOTI8U6p" +
                "NHfiFV2i6Nppf3BeNlOtUKrhOdnpLAk3CEUAZLy0MHZSeFkALF0u8MW0eIYkHCurWlICP85ZNvK/XMI2" +
                "awJ+5TYnKibuj6sRFGJiDF0LT0ahCPUrO9HGadXEcTq3QeivQfezJGPk8SMAXl++A3DtFFtiB7yImtx7" +
                "xmmFRZrk10r8zkOPs0cdfy+hVrBIGHuoiVvgr//E7lri4rZE1ThmkxUr4Otdt0j09LH0XgEHKKdsXT/s" +
                "if+9AuH9BkUms1rXojRHQfI7qVcho9ix+MsdnkcYrBCNkPDoqKpX4n5nJdHqKcC/+FjT35MXG+ObXjpL" +
                "8yIY+swrqFJ3ezlQSG2Rx5K/w9z9LY2gxYZ6aUNrb3sMyJW7KRu8qka0xgvfp6Do9pzZ2k1Wu94JT9Ow" +
                "ZWX6rM9iCWdpr9cKxAH3SH+MJnuW/OEOKVz0uurl1qOOeaID5FqAXtWpRJ+Jn6E7ShZ2PybCuMWstsBR" +
                "KVkB2Yhe93dkzRZoeyb3msfC1ygZ+B+E4IbHsmiHnuHielcSlXcji/hCqhIInpJbMN4T7dkvAIjG36Fd" +
                "VIP2dI323HEI40JFWW+esnm77RCJOgs4jhdTPTIEmJgu1YdIfGK2XIGr14U26CTdN7syx6qBln5hhKTs" +
                "J23lzRp4vJ/d3BUSz24XrOSztD6/jvxEb2JH4pTfeizmZYLdM6ZZ4gjMAxbTjJdUfcfYrvMgq+xtv3I6" +
                "RvHtfzYHMBNZ4ljRTA7SDViJ9w61xScTbhW8K1GntAP7tu7Qx94dOMuonEpihaZVfsHCuZRaspH11LVI" +
                "rsq5G7pMXMsnp4mdvoTKBUL8yZLV5fReQQfG8JeeZ3sVgGdqFSEJyftd795IeKct7lllO5PuGyKbX3hz" +
                "TzhGIfqO1FNncdFaovtRPxioZ1vqSgLaojnxEFmoQemcTSbe2LmV94N/Uz58pCuk0LEvtlCpCgXsRFmE" +
                "W4lyYvYhucM6M5gQZkcPuJ++ClYkczCdAkfzkbSTFzVes/P0dCj70wvZp6HLt92hf2Ka896PjWDcWMap" +
                "5pLfD3OzxAo2iHBwSN4Ct70ZIvELRkDdIyq4B3vsvDcOOzzkmYtOqesRsRV6YxUya9U945L6As6wKdsl" +
                "TMezMLoKPzkjNjJgzTkdPfRI35j/TZgOqmR6iqt0u6mytaGLuicw3kPxKY7g/PyAAsUjlPLx58shStMs" +
                "V7ga+MXrq8u3KLn3XsSKvHvxQ+h/cApT6BTW/hfy7+81JVrQ5Quvyv1vufRv2hjhR/8DHb1QUwCIOTpI" +
                "iLL357x8jKIXvsBXnZ8Jk1YvSZYnlxJjDaT3U2ndtPR/AQljsU6CUgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
