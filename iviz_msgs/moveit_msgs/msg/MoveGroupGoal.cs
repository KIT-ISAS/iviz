/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupGoal")]
    public sealed class MoveGroupGoal : IDeserializable<MoveGroupGoal>, IGoal<MoveGroupActionGoal>
    {
        // Motion planning request to pass to planner
        [DataMember (Name = "request")] public MotionPlanRequest Request;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupGoal()
        {
            Request = new MotionPlanRequest();
            PlanningOptions = new PlanningOptions();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupGoal(MotionPlanRequest Request, PlanningOptions PlanningOptions)
        {
            this.Request = Request;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupGoal(ref Buffer b)
        {
            Request = new MotionPlanRequest(ref b);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupGoal(ref b);
        }
        
        MoveGroupGoal IDeserializable<MoveGroupGoal>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Request.RosSerialize(ref b);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Request is null) throw new System.NullReferenceException(nameof(Request));
            Request.RosValidate();
            if (PlanningOptions is null) throw new System.NullReferenceException(nameof(PlanningOptions));
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Request.RosMessageLength;
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a6de2db49c561a49babce1a8172e8906";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a5Mbt5Hf+SumvFW3ZExTa8lJ+TZRqmTtypbLekS78UulYoEckBzvcEAPZkjRV/ff" +
                "r58AZsi17Lvs5q5yTsrLmQEaQKPf3YAHL1xTuOp1aao39ufW+iar+e9gMMC3VVEtX22wjc828jx1/GIw" +
                "ePwP/mfw4urL82zttrZopmu/9A8O5jc4ya5Xhc+8rbfF3GZzVzWmgNk1K5vldlFUBfbIFq7OjC4maxx9" +
                "XxM0AEFLsdCiyumDa5tN22RFk21qty1yC4s7yV6b2qxtY2tP4LDhztU3fmNg4GZlGnqlsPzKtWVOLTKY" +
                "EAAZfKetE0gBwnQTXuJgV42pG8Bu5hvT2Kzd5PDHT7Lni2xua1xj9pMrqsbrQDNaPI5T2xwAwHQ2zhe8" +
                "Vw5mhjM2Fc1x3ta1rZrMVdaP8Y23SWMGCCAApLdNBl3t4I2bueaK5uJxalOa14DwT519MStttnSm5ClH" +
                "JK1dbkvEOaIG306ySzNfZba0a5rFAqBgQ1PXZs+7Bt0NA6vtEmY1oWHoBew29C7slpZZLHhFMPGmNoQQ" +
                "3usN4m/elqYWEAAe5m6awi8K6Po09nj7jiBPEyC4sJdOkA+oNNUeFglfMlM62BXaadOsYGv5d+3ydm5z" +
                "oSle6q4oy2xbuBKBMJbTeQ4bpFyz2ZQFLBfwY6A5DQKbUrkm+6kFWt2ZPb8bpVOmwfsTvu4hAidWW9+W" +
                "REjw9ic7b1y9z9YImFGxH1yH9yn82PrYKBVQKuxbwkWB7mEdrbdEqJXjhsifGzsvEO9jJE3cYwPT6vcl" +
                "hAHVAYB8AGPitOXbtMiPDb6sXbvBB2EGALZbFUBchFuFCz/dxtYG8aBwqecUYQW47XoGjRFyscYdURDY" +
                "H5ZFHLYGwWDzSXa1cnWDssS7shUxotOv7QY/5pMBzOnRQwQ8DdLSNI1dbyIq1+Z9sW7XmVm7lpiBRj+G" +
                "WSSWsnQ7oLKEmbIhkKC3sEk5kMiidKb502faMA6LQEmuzE2Jy18Y3FxmFhbg0GUPUwcyLoTAdWqE22xr" +
                "SzcHEYGsWZGEmc+BhRGrQCCTLHsik9uassVGwG4wteHZ+NN38PX6NoD7I+AivSiD1Sh/RJQg5DVSNXIO" +
                "SL49bpVFPsduTbG1AE4WCCMDJYICQQGIHUG2o5BEoEWNcy3yrDbVEmY8LNa4caZqyv2Yth8FTDUvW9AA" +
                "sLEkji2pj7PJ2Ygkp4xDNG6DZlH6JlTgnn46OSNYIN4Z0cNiYifj2zACALtfUuSMJmGbodFUO009b+2U" +
                "Z9Rpk3bvt7sPvX1E86nmBk7zZplobkOIBJxFhYgCYdGC6Ecug68kz4DvEdVb4D/gl6HJZu79CMlFBYAS" +
                "TZdvcFoTHbwIoF0Fm7Jb2SrYNcQaazcrSkvKxOOcRFMxYAPq25blBPnqgrQWU0QtAqq2C1CdFVgHqgph" +
                "irDQusL1f2UN6GrQrvgnSIMChAXsOjdSKQfdxiBdQN0IiSud8Ty82gqghu1gaR1gDeQ2Yf5bEuKPEPCU" +
                "gfYFzz9+KCA4HepuSMs3OQ/JOGRzqcpNDfrXNgZMJUMoXxVLYPtPSrAVyCpZb4AG6Guz34A1lZDB0sJ8" +
                "iTNR+eCiQdav26qYk/JGhZD2J2buWhlz5+q8qLA5UQBCJ3IFmxNp4PnFOWlnO29RRMFIIFpqazyi8/lF" +
                "NmhZW0CHwcn1zn2C0mKJGlUHZyMTJmvfb2B3cJ7Gn8MYf+DFTQD2ueqCbEjvpvDoRxkMAlMAvQScgQrj" +
                "9b5ZOZasW1MXBm03AAySoQSop9jpdJRArgh0ZSqn4BliHOO3gK0CXFzTJ2CO5qSMfLs0JELF3BaZDkIB" +
                "BDxIv7KY1abeD0gz0pCDk2fEZaSjaUeQy70HSQgbkBMFq6an3SD74W6o8SgXKGmBLQBbZdE2MSDfWVeA" +
                "4q4trAQl4gSJ5DltK8mgtQX9g/QXeqK9VNSo2MQOBrniajtG/yR3oJnAVgQYa3OD9jrgmGxJMCtBs6Id" +
                "V/mSlSq8hi5DO1mC5iFZR60QR0TRxAPFPKuLJShF6gkDrUNnk8niQPMtHrK5Q3PmwWDDAEjtGtFSqHj3" +
                "rgULFtYAP2phPbKkdF5EIo1zY+Q7AdFF6GtSgKomQEM0wPQgc1W/vQ+/9uHXL/eg06JHdKsqK6qIPzMD" +
                "y6MrSRvcQzAD2IOIztoCtsmrRYs+KTDJjR/g3rqaB/8aP7I7Ru2iO/a1E9sfRMUa3KmV2QZryWYXr56x" +
                "HxTsK/LuUtAvsC20S4ag7tPcLaa9wZ40DfhiAGXuyrLwuE43Q78BxIHRb7DhHjaUVpG5xJwbDbT/U+3+" +
                "inqDQ6a9pwHyVCDjuM9KswTs5iidkXyBmMXDRTtsDnQcbXYyuoCVGhKXxEyLxYGKoxmym0z9E9Wwdr5B" +
                "aa3GIasWlV3kuOpSZy5H+3io84GG6MihNimtqY81hoFgw42Q1vk5aE57fp642zPido4A4IDAwjT5JiG5" +
                "0WDmHLrGU1zcnandowSYYMoEFiDyW7ky94HpwYae18WMbSR2rGnhYu2BXAFFSbxTO1IXzAAoH8ViCZ0s" +
                "hg/YMB7WFi1AfF+D+ig8stt8hLPhQALqEtSRoMpSNlN7R6GYnIzu0Tg2jdZ4v+kDT40fgGJF9oxd7AL2" +
                "qgl6mn374MMIgJdr7P0SzHhc2GVcC1qjVQFYQGrLQfIzh4IuRHlAriojgm3GwMTs6ZNyhHGj+ctQJRqz" +
                "Az4HggfyytF/BWftoA0bNQ6YR7eRhAezkXbNJM7lSf0WZOccEYC+8Gwxmyh8KL7jxzQC8hBtI1pQXQzT" +
                "ZEgtpjEfNN1Rw1EnaQ9rRwpT/3WSfYdKDfUb6xsRobSKygFA2Z+eoYCw1mPSVXNwqoFTtzbdTo5Lode+" +
                "F2pE7PFqeG+TtUs4bqUwCE+++AVkPSwZEMlwEq6hiCO5HzAKhk+UBsI0E+RQ8EMnjbY7GgZoZtIOglrs" +
                "+xVsBIFEJfoRBQmPygjJKyX45BVj4D4EyqHagVW9URvIKM+yDEDqkc0lXAQSy+0SLCsiPDSxcgf7isEA" +
                "hw6gimtASTtv2ppkSRyPCZlNMUA9eJU5c7JR/kSL3+/BAFkz5v2GTGEyjYK5xH2Wton8j9GNEDa8AaFE" +
                "Qiqbr8BKmGTPkBXeg1FbAo0Y8j9NrcLCEIX9/c3FM5Jpj1CBD8FF28P/zQ5NaQ5ag13OH5GCkcoSFyGd" +
                "HSMS/tQFQOG+yNGd72RNYguFBgy9tRjSymZmfoML7szh/8XY/YqxHUYVVr9ZjGnz/0ti7DYpxgYodve9" +
                "8MO1kjC0CuR80GgHG4oN8G/v23eEJvjI+LofdzHM+ojDSOwQxMrMNjsLdNHs3EGswfd8ysFAgzGJ/zf4" +
                "W2sw/ogCQL20+1lkHPiYWwy8WhdBxncWgk8/x1kjHj7o+emv3T1tIFGSLEulro/GY3c9s9rdWFwk+eIe" +
                "fSL0C1AOm2pJcSSKNU7CBkqT+Czt7md1zBNHdg22grcnLm4M+hxjtg1ZvA0Kqt+4RAIWH9kVuI+49C3+" +
                "p8js3tuQ3wkeHAkwA/L7vborHC0taNU3GoDC35ppIjx2stQGTBFMmq7MhlMrFFuN6dODubEiS91rbEYD" +
                "njxfIDziI9wuXCuBrJw45pMil1zFmPKN6lSfHIOnLjZr77CMJNGFA2AutYsoBqqBZnHwKIgR8+RhPA0Y" +
                "YFgiidM3riXCm+1DNuWTMLGp5hVNWYOG2Cep76RD1BoEbMphFLTxYhKdZ0VWBRhec3bWOnFd2A2yFXk/" +
                "MEzHi6QQ3RphhKUAaImugVCzgHYwnznkNy8dxXlN7VpiBIEiSSQdo7Jz1L/1nkarbcnhYck6yMC4fQBT" +
                "YzdJpjZ65jGpC8jA2U1lCN2UnS2Wq2Cx9DZjjGn1m8rtqihNqf198OQhLz4RM2DMsfkFhVAlqKM2PfHM" +
                "8ZwKULwsUxA4JOohWLB7L2Ds581ImTXmmXWf9xvLRIGRjJnx5DYQdgL38N8pGpdLrjjhtfASrhECgolJ" +
                "eo2WibSlpOqhNac8y92KWiQDcsqxFE0a1FMEXDlMcOowcwyRrAvMP/gBSRyeJ7V6rZ/QO4zN+tFY3/k+" +
                "ZcTDSC+sX3Wh4htou+YPR+HgtwjiC2QOTfpjHM2i8hdpFksIslkba22s+ihsRMDiW+Y02LE8L9ikJsSN" +
                "0rlhDREthEa6ZZH4Lc7uSZ77lIwE6yFfSHFDinonjYBGt4VrPdjB9j1YCjj9omHpzAJnMpjtwY57cnHx" +
                "+GxAPi9yQ2ekRe3WHJCotkXtKiqbQceqRvt6aME324NoIlagsG8DzOx7NFHkIx7pzeWLV99ePv6U1rTZ" +
                "oJxCD7YK6yKfVwQrTdqHMpdfXasmI7iTrhN2IS7y9evLlxePH4oQjmMeH45GGYNU3Anly1ZTVmRI9R+y" +
                "b+rGUGULtCjtomEXZcRFQt6ViCtArUqMKE1z6wssmqIpEm4e4QRfbbQagTUuPKIBqg2dfr4rofhhoTI4" +
                "+d3/ZK+++Pry6TUmHH9/Z/kHkfP013McJDTJbV6QwhNBBmIMAxXownjLjnWS5W/cksPmwXXkEC7QCUbJ" +
                "O0bFjQ1x2XSEc3rD/WPwoVaCWoLEqrJ8psIeoCjAfJZORRQsxU6+vnr18gFW/khA5YcnL77JGMAkexJI" +
                "GMRsYIAkF4eCWrESg0as1FWhTLJLshqK6simEx+RQ+/cDdgrN/Y8++g/ThHDp+enT9GyufjidJyd1s41" +
                "8GbVNJvzBw/A/TAlYLs5/c+PeIk1WUyV42hOJZKRd0+sG9ycBAtoORbNKXTC2krgghtrJQu9KIFVZ0VZ" +
                "aBDAHqNXTGYwEjW3ePEF0wYBwVUh38vIHAdB4pL6RImKUY4bo2SyWArnE5jzLCCA3iEK4F0fBed//PfP" +
                "P+MWqHo5lQrtDmd8KiNd/e2bDLbNW8xhhH3qDHz1c/mVtmDYNFR2ulv6R3/iN5gxOs/++Nmjh/QIrWts" +
                "UKCZKy1A7e/Ame+9RgsFF6IDaPKLv65d3pb4ndKnjducKkEDad9VrPY2ayEWv1DtiN8gpY2z+R5MazLa" +
                "5hgok2iTejm1DekZICuNMoGFM1MTAIChwEeVTpzIhvPZGP43GVCtxOfZF6++BzXGv69ef3X55hJUCz8+" +
                "/eGb5y8vLt+AKJcXr15ePv5MuV3lE2kZnJO0YitNRUIBitZrTjY2jeHx2CJU1VhDCjLtkDQ758AflVRi" +
                "KlGLXbEtouu9SqrT2OeUldtASBO/wsJpquw9fD/OfuBY7o/pnBHJ5DDZatmEYGNfBqHb5JPqn0nE7fR7" +
                "sEji0w8B1/j0I2rxZEqMf5kVxQFx21Fswl+JCnu0fliokCiWElWTF62PZcBMQZPOvk7fPLl4/vcrtJCS" +
                "MXWTCSZuMNf1MFaYdCj8QMUVah5SJF6G+jEzYHBwdSJXVXTgTr+6fP7lV9fZEGHLwyiuiVO3CcbjmlYd" +
                "90p5IRsiL4x4PJRzOg6vTsbhh2Sc20bBagvFHW+fOCfHx3zqKg4G6CfoH+38Pk9i1L+oyQXm4uum2EQa" +
                "Ipxif3Q2uRpvLDmOjwWpgx4nCv4CSfUWj/Zo5NSDxhEx2PBuRNyhE0DOZ32QhEJjtB/7ot1C84C/c5IZ" +
                "sZ0ENyfZgKtZQvItCckm7e5rgUUVIpedkFSaJDdS995Z7gdCsHevgtC1VMWTTBXdSRQQ4F0z89WFqZZg" +
                "Qfw5kbBSn0zVmVRJHSpvYI2Y5gJjx799N8AxrgUA5RQE1kCEhwTutIf6XjdWS1NpNkdwTqlW7nRPqNJl" +
                "HEGZLuvUx0lxJeLbRzxP+35KccB7mS355Uczv5wZtWPx76P/H4IE5n32MYb/Ps7mv8C/8uxxRh61yc4f" +
                "A4HbxduzdxhRDI+f4uM8PD7Exzw8PnoXUg1vP3tH7+4KAR+I4fXiWkeTYb0uSmh8duSfNG+VMJQcTs6f" +
                "sESJeV8pzw+M+HacnDaAh85Jg3e4S67bmssU3oWYeTIWqzL7HstuMQ4hdmgIi3RPK4RUJyaJa3RdujlL" +
                "khG9VU5g6YMjlRX+sLQC673iy86yDosu8lbDD6D7pxgDmlL59f0EYeOZn9sL9VXMpgdVkDeTE0OK8PRc" +
                "kgZpwmEsObJFOXI9aBPi+XREQzPAXD7AxB7nGFihc1TptexDp51uTrfpq6h7O60Tndzt8G3hxfvttN+G" +
                "193md79hPYxwhIYfjqj0kKuacYCCwtuaEiEXTPEdRYyqPfosffUgHhrYb8MQn8BXpOtqbqczIPzdOA7/" +
                "cfLNzGAB74Ld0K9I6rc88p6gUxxTEhTxaFPM18SdyIa5rVxDyh9z4uB1alUnxze4ADQl3+xpCVYd2Qm/" +
                "2NqRf+fBpfI+FoSO+lmSu9/uQ9q+lU1xz/Ke0g/bEZd6WHPBDi1XK1M2sI9NsqAwbHcs0ckhocUCs39D" +
                "IUKCQgULoyiqTb20jagEl7TbWRLK6XGcWw6cMIgpgZjykDoBOR1068yzgeYcvuWWsdGUj43+76OueyCv" +
                "LlJihMcoQmFDH10waiS9629NNB0axrorTgO8Q4oRtugHjX5rXmoQjh6j+k4LxnpOLYYBQfX/5kTW85g5" +
                "IvcgQBtLKiukIYIRgfmTPKTOQwwrnE1bwnaP0Zo4XGI3SXb7omToDy4ozajdPaEcVZy/RxR1i38+LI4k" +
                "V9PpJVGJRFLFnSHCiiVEg9sKk7ru9/9Q9GklHwVCPiH3KrN1jSJDVVeSz4yHdmd0mNhO30+x4zQ0Pmyx" +
                "/2CLX/ot/hXl2DEzLeTgw3rjKVCqziyIWhNbjgt888LPOenI+mZ0UCkSjv0R9VN7qkHsxOZMALznONqO" +
                "U2LFhpKlnChyeAQrEDaXSSPgFzI5Ei5xfjgaggLfF5q2tfrHTMAhK+VmXPnMMjh2x2kg9Jew6edJQEjx" +
                "QwBevroG4Fz2tYYZ4NHR5JgyrLZhug51imHmoUhZUYfXG9QMtmgC1MQa0JM7sTwWcbEt7C4JEzNWgKgP" +
                "jSGS0EOqpQIcmFm5l4LWkV4vQITvW6zcbWsSl5PA9p2YKm0jabB40YbSCHonBV98EY1TNkYYSCrPU4B/" +
                "Vs9Sj7VvNWrdb0lnuOYrPKCu543DDrEWUizpqePu1RdBhI25VpkzakdUx5Ucbw3GlCeR8UxLDxjdSpmN" +
                "25layiF0T8OUmehNn8QSyrJyOqZ2QD0mFm2s8Z4Nykj0yuLptCK3+YwbjPkstuHIJAgzsjB8n7bl7g/0" +
                "VbLNHnBU5IFD+XS+bKspd1hAAA0fEV07TEHwiVyc8NTrNTBhP8NR8y4nMu1GEtH0KG8Q2EgyYDzf2VNe" +
                "ACCqfUE7iQau0ZocOaQQ2oU8MZ8YxerrprNJKLMAx/FAqSKDgG202Kb1LR2GptNrdWu55iYpqDnkOQwU" +
                "cEIXNNBWbO4CoAIebyc3OQKi5Ha5Jcnh2uUq0hOaEgccNz4mxZQnsCAm82tDzDKzc9NGrjpiOdAoRyuq" +
                "RMYwvvWWm8FJykvYliSTQLoBUsIjg1y1Y4hax5FY5obPuR5MXdCH5ThgJq8MFapg3SCNcCbGJOdiaDy2" +
                "K5KDbnKyFkPT9EkkscjLChP9pa4sGZ1WrwI6EIYeVl4cFQBK1MxC5Ib3K9dVSajFFufMvG2ooAaRjV/G" +
                "NMvAlFpheiYad+dkPmwBA+qxzHRDfqz1I4WISZjSNlg6ospORj4O/nXx4CGPkEKHeWFVFIvQ0SQVFuFM" +
                "Ia0YS4tksaIGk405kANyU1XQIkZgigB3Gy4MtzW8xkrSszHNjw9Sn4Wq3eZg/xPVnPfuBoF2U478Bz7X" +
                "XqQFPfg2sMhyz6ZP2oM4vkTfp7tETSQc6nlVDgc0pMSFFqlUfriqoqpfTmP2lUtqC4hiY7JLiA7Xgn5V" +
                "uCGGdGTAmhgdPfRQKZhe4dJBFXVPcZVONxW2LlRF9xhGLRSNbATj5/vscfZwnP0Afz4dZz9SWkKS25cv" +
                "r169mf74uPciptrlxfehsEEEJu1TGPtfxri/VZEQAvBZ5bhevdI/KsPEqFdq9JxMAsC66G7mnzonRy/e" +
                "UtKjs1pAUZ37vFyIiyfnCWOuo3en2T1HoHv3A8YimsWiM3Nlj24taTZUp3kUbhq8og/hCitqJwfcT7j0" +
                "FEh7QVcBxNuXyFiR9CwfOKpt09YVHTTiNBDdOcEXe4nooIIYzyIvuEtyU5d0Qg7P+Jg9dp1iicvvmAeV" +
                "BeiK/w36CdRq2bufiKw3qXbKhnSAg1neg8OJvg94TN7atUgmKretyF5KKzER5taAaMelUlmmWAFJBTEv" +
                "Bseb8njpcoKM5VDx9lcWE5fC+vCWxXCOR7VIvGxNL0T7s46eCHgq4wW352yssaPkWq1YUURYH5ww6+f7" +
                "Cqz8OVX1VYtiiYce2I5Pltq5hu056wRSF4u0FWl32cgUJy2fqxWUxouUfBMWr5cQYiF/P6MVqXAS10y9" +
                "xX3DewW93F6Uya2NOv6YXBOiCaozq+l0MddII0lX1tLJWAAbsDc5Y2V+fGWgw2UlXfwiTraFOYZQRUJH" +
                "g3qzsNPALFNc0CCujyYHprjjJCpl9cjzyzleETpq3Xi4U1IKBNXdI0A4F76Kj7YtsmhtKUcazC1iS1sh" +
                "svlGPzTe22rOsgbNX2YDqsYPkb9DKuXvRPBKOpmQFX86uNivf6EfeHWmIG9VLadjMI/e5icj5KCx9/cp" +
                "yq/kJhS9cbErsrspWUQrGNXJ5TbpnaH0untnaHqNY3q7X/cOF8z6yDgMgxpppDVxSOUcLPJd3m5KvRmm" +
                "WWTDY55dTCtROuq2s8RiXYA2pUN+U75LKjlePDihZSvbx5M9fIB+IJcihkNNL/hcvd7PGG+3kfbIzACP" +
                "AgEbXF619INv4Ok1P8BMKNQs3zrt8WI/y63xmkerbel957oeqSYeH97bM87sVox6B0bF2mxQxKTL2pCb" +
                "SiW76LK6ktymeHiJEbwW/mNDpnPEfz0Z8BGGp9gXs+xchs2gNM3cGbJrEnznar5Zt8zv6TKg+7hd59cp" +
                "UN23eMI4vXKCnPQDS5eN2km4/+xEif+g5XxVlErc2LCfwYm3IJHqk9vZoNVfTLYCW/vxR1Jyvytuiknt" +
                "/MTVywfN4qO/Nou/PDB/BVKe3wAgug7hylo6GJy7ebsOoZiFBN1SM+YgCSSSoDvd7CQJgMbDftSI3w6u" +
                "4+Uc4bz9fRwvPsr8Iv+0eAYwUO9jcRFbFNQu1HlRE6nzkt5I79sixwJE/FrEzreKInCzf24N1ub7/Zqz" +
                "tHLVY7fiKR2tv4JL/BZmxLVTR3KmfQYmmQd7bsvFmBL7pXdBhKSWByOD7Y9wt0bE0CQxKA7XqSb4z62t" +
                "i8QGK0iDM4aHFbjgFYUH1LNHE5rPEAkGb515tE1DFuJw9MTQ04CcCZXgvBSYBF0NRtM4uOqWCIMCTGTF" +
                "gdJbZdCBjnE/pABVZ7bUXErRGYuSSuCrn3lKYivxgfUqe/LyIj2/FghNAExTEkDZd/BJd/7+eYgoEG39" +
                "7hVrcR9Awc1vxK9iwy/XNejjPUw7UdmDEzJ11O+97WYAVffx9sb0ZH8oklK9fz9LIDvity5ALhb+9QWI" +
                "MXL3009sDK1GCgf1cCkcjt9pGceeTZnDY+DhDmdhd2qTh7Pqrn7z5RdP5MNd33cbxmN0ovMRfi3Dr1n4" +
                "Ze7dWyDbjO3CrmHZvySCAlRHrna8TixPkpzpVTHR3I/w8XDeQHrIzvPDdyDsKKAnH+/snO6vjE1hyUcX" +
                "ZDaj5wdGF1/uAJqFAhfQvrb2eK1Lmgt21O5okkqu0WL750iqTWLnwS/lYwsCEGuXjy3gn4G0/zayyAOg" +
                "ajIMbYPOl65DOoeGx1YeuPm83YCSHaHCIJeN3phqvldUDCez5sEEjYGi1Bs1GRAdeC4NBryjeek0ISnd" +
                "u5LjjdX/MAC59OtRKObArCT+hwOkW+XyWBTOydNWQ/qyDA/mDEjWX0LOkrvylbl8NRudDJus+FISw0GR" +
                "XV00SjgeYxWfoxpHbhn8F6IXYwDpZQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
