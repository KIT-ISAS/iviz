/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceActionGoal : IDeserializable<PlaceActionGoal>, IActionGoal<PlaceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PlaceGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public PlaceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PlaceGoal();
        }
        
        /// Explicit constructor.
        public PlaceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PlaceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal PlaceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PlaceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceActionGoal(ref b);
        
        PlaceActionGoal IDeserializable<PlaceActionGoal>.RosDeserialize(ref Buffer b) => new PlaceActionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "facadaee390f685ed5e693ac12f5aa3d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a3PbyJHf+StQ66qTlKVpr+Wk9rRxqmxLu/bW+hHL2ZfLxRoCQxIRiMFiAFH01f33" +
                "6+fMgKRs7+Wk3FUuScUiMI/unn53D55ZU9g2W9I/I5N3paurcjZd+YW/950z1fPTbAH/TMti9LoyucVn" +
                "9GT06H/4P6MX59+dZL4rePNnDNKd7LwzdWHaIlvZzhSmM9ncAcTlYmnbu5W9tBVMMqvGFhm97TaN9ROY" +
                "+HZZ+gz+t7C1bU1VbbLew6DOZblbrfq6zE1ns65c2cF8mFnWmcka03Zl3lemhfGuLcoah89bs7K4OvzP" +
                "2996W+c2e356AmNqb/O+KwGgDayQt9b4sl7Ay2zUl3V3/AAnjO68Xbu78NMugO5h86xbmg6BtVdNaz3C" +
                "afwJ7PEHRm4CawNxLOxS+OyQnk3hpz/KYBMAwTYuX2aHAPnrTbd0NSxos0vTlmZWWVw4BwrAqgc46eAo" +
                "WbmmpWtTO12eV4x7fM6ydVgXcbq7hDOrEHvfL4CAMLBp3WVZwNDZhhbJq9LWXQbM1pp2M8JZvOXozrdI" +
                "YxgEs+hE4F/jvctLOIAiW5fdcuS7Flen00DevCFu3CsQxFoCbOaXrq8K+OFaS3gRInCW62UJB0JIoLhk" +
                "a+OzFhnGAxLIQM/pvIklgSSmls3gkNtLYI310tZZ2WWAqPXItMAXdtV0GRAcZuOanrlmbWHrsHQ2s3OE" +
                "xWS5bTsDJ4cQpfQV+MtCzwTIC+BtcJNA52xubTEz+QVAVsAMYMq+6kAGvTcLS4eQ+cbm5bzMGUGBwE9k" +
                "dRQQHgBArXrfAWQZSB2Mmuj54cnd0NGt3KUtOz63oLlgN9160bq+mdbAP/GZ6TqTL20xdbO/27zTtzT9" +
                "BwfqAs7n3fuswd/TSh54XHTmXCXPrZ0n2/i+aVzbTX3fzvGtLCkz4Czderpoy6ax7VTH5q6qSg9Lw7Cn" +
                "sEHXwjF2IEKmW07z+CDZBrauQcuRKISnACptAAh1rs+XghbOm1fOdH96GN7TfJgyJWYhlOn3q4ZQzMJ7" +
                "1wjOt3RqSnbgpscZEo+0P4tVa3yTcpLwNejWtgaGbJzvepAENyfBRJUkk63MvYNjSsYQ39o5vAdCG5iF" +
                "gjkCQiPFXLthkL53sPrb8JC2mPKxy24KhS6su9u6uAvL07whFCA4lUGzQfIHv+Ygz6iASbmhnTlU42Aq" +
                "EFOfiJWtvAVF0dpxVrsO7RZRRqT0aLSwDqymQv8aNPy5mLoAdIDYNKAOgP3hDIjg3zFXAra1r+gMUAUJ" +
                "tjpYJ7ewiwUgPzI3kkoG41xQe8xRcF7A8x2Sy81AT+WVDeoNjg6o4+3K1GCRAUug34pXNTPXd7ROwaNz" +
                "WHGGpOxRkO81vcd/kK0KJg9YHte3ntiC6A/c80l5uRlm/wR3jZ4NnLMA499xGCkSP9qa8hp/ooZyrB/+" +
                "KXATEHAmZ8hMXSos8DwwL7g6YEMAvSCC78YZ+E5gfjp4Cz9MntsKPDd6+f49rOiGo1lc3wdxSvYCUQIu" +
                "sFfokbG5fVxVieBcmqq3LOjCFR6FDdw8gMh4ekJ0Jp3jcdAWlhNAPehRIrkAljyL6CQPB2glzxmbUdHz" +
                "KzLb03nrVlMQB3hxQ4d5rYogjYu/2dhHvbTtDbPwqSe7xbW0AKuZ24KfAG8t+tHgYRrVwggEnuMc/Cjg" +
                "BFBEYwwE8HEh71ljIzquLXXuJBsRR4cBo7/2Bk0MrRvH3RaCLFwU3IAvgB4es2tqcYwI2wDdYPSvwl+b" +
                "8NeH2wE/kk5xCAflyRhEeg6Bx1+/RbqjBZiMPoGR/rW+DW9l1+QBhoWdlzX5111iCqP5pyljDgIAwabM" +
                "L/qG1Bwayqwz/sLDMjjBXoFwVez+VOW8I38VCEYmCs4cD72jqAwGqHnGQaQZ6cVcFkaLh4EavCjK1uap" +
                "n5LAueU9/Eh671gdiDAzLGU9PCoGmBZg0k2trAcRsAyahhcyeVXGwWzINWJAfxdiFFx5jhE1IiIhDkzE" +
                "SIicKAydLyFiAhceoyRQX7olLJ1sdzOM8DFK7WP0TIb8Q6pV17ikf28Vtb04MRxDsZ1kIdKFOHoDnin4" +
                "cGidw0yYGFiJQseWDneMsW/hQHbAsYU1VuYCXcDas5vcNLDYUKrgMUw5tJPFZMzRM40iOUEoKA0E7mNb" +
                "Lkrh0uhHkustyI2zbv4ATgYcBoKZN2Nmax0rp6NJ9nyebVwPgTPgAH+0kn0ix0PhIrHrnCMRV37d1ech" +
                "rAZd3sHhflKz3cxRp+osiTv1sBXIYHVMcNnZ7Q9RYpYEqep7JY+CbHugpSd/DFWT4TwCUzARe/DVMLqT" +
                "QI8DaHLGIozBKx5Ex6/FIg7GqZkcDn0V7c5gdGKPhhN+hAB9VlZltxmMvwyPh8Nv/sC2KAJUDz/2uAfs" +
                "2zKpUQdRxlMTRhBT1YXSO0YbqqvptcxFG2MptvLZu7DFXXiLLi6otOkMfOD1OG7/ZfIOordL+z7wdXCw" +
                "9MHWyD3PafUReXxrWy6WZBbnJomy2U0SqmSHhQV9Yn0MussV5lzY7rjMUUiSsm/2tAIPscWXH2zrSIP5" +
                "DKJTH6Z2R9HVICBuIzmyw9vXiikbZ3BEBu6hHEdEFZTO0L5oaO9mDvO09cUONclwYXJSmQVHBV7ByeCX" +
                "eNtRAhnBoFXIsh3FqM20C9uJw+qScRD4Y3wWlApM2O+SyBJTWmLKWyoAl67qNZm8D/Js9ATZGdb/kUfG" +
                "QdPWLsS/+d/FXbfAXkOiAAVOyZNFK89kggM9PmXS4AEDU3kHalqtG9jZpi1XJRJB8jhkwfuGKwhyKk48" +
                "5OzQoDnp0bc/GvmlaSzDcY6LvtaVUH2HVZNMG3rYMeNu062RgyhTC47YbqiYLkjJMFr1OSjDoijVMYir" +
                "jVG4ltZrlinmE9B/KOifoCGRVwTPQ7OA4x5jYmEXxRewJMDBK38EKdn6kwjhOMXlZhjl2vOJfDJzV2Mg" +
                "D6cm8w3Ic4GBDrA+ZTQpGYOrMD7MBEQ+SvUBumUbqQiLIc0tJnaxMNaSAbo/hv+C0sLa2tfZk1c/P/pK" +
                "/j5//ezszdmjB/Lz6S8/PH95evbm0bE+ePXy7NFDJTXWCzX6IZhkFD4f6aACfPDaU6J4MDSmb+IInYOy" +
                "jOCnE5JhJ5nFBBlpA/QhNVbsKIdd2CvNTB3EOQeAfGs2uMO3oj4BcQJ1TL9+Hme/jClq+DWF2UhxqrL1" +
                "AgIOgSh3LfjgjSMqY7mGakHyEog+ibSd/vzofvLrl0Br/PUrkDoFiekvUJELjcdOuZYaPX4pezGc4LMv" +
                "REmArTFF2SMI4mowB00G5zp98/j0+d/OAZ50Tz1kWhMPmOvATBVmHbQqVLNkpw45qXKEOI75NTNXJSjk" +
                "GIIM1p0+O3v+3bO32SGuLT+OIk6YnJynFI84LUlpB5qLLGSHKAtHvB86a7oPYyf78I9kn+t2wdBEacfH" +
                "Z7y9fs+neCBIKX2FpZCh0kxkEt3xsqVSOBf3urKJPEQ0pVIKHBLye9+MmbLZl0LU0ZYkCv0CS20hD8yV" +
                "SOrO4EgYHHjjKg71syq2xINCNYsMqCEQ2DxTL8Byf5NIsKSZydsgUxLKTSDblxYbDKx/936Ee7yVBUCX" +
                "hLXU5QaXo4fQSGfsGlSCZk9kSUlonnRLpFI09pBM0TrwESjujHh3zHDaqykQ7iahTZ2cvUHf73Gjh/nK" +
                "T7vSmiJLZ4kcJV529CrIKYpZz9F1udRhGvofdNtDPQ5F9y4dWGbbFvWshl1JCTJWkmfgAPadnV5NceI0" +
                "DN4dsfnkiA/bI/4VffB9KQY53ARfNqbznhInK3yEIXzMQ3Bqqyh9jnVkDbeOdnqcQmMTcT+Nx/SYH1gT" +
                "ExbesOZfL7GqgvagJHcVBzvMsAXGxpyda3HhFwIcOcYRPtwNl5ptMhjat5qzZgbWFCBVhds+5y2S6QgG" +
                "rv4SDv0kKYwofWiBl6/ewuKAT06p5nLVrzBNvgIuwz81PezB1HVra+sE8lBcVtJhqb3lZcF30VWTSJYt" +
                "pjhvICMVlZcuS7tOHBumClfYtgJ5ii4OcdMGaGBm4D9xl9ERddB4SZf5HrzHpm/J1Z8EsR94AXSMZCyk" +
                "TwkWUB7BzFrJ+fSYWOFAmhdJY5F0wW+0QMonhCwrftb2SNgLDmgJOsTinLWl1iU+IY6glEqU7ObMRGTJ" +
                "qMLGaQnkI10NIRHAbQ3ftm6VkFs5s3Nr0xZ+cKYBZGZ6s81iCWeRD0kVFuAeaknrqdFvZeoN+9ATcksF" +
                "XElG85iHPGCcER8YrtCBMiNj7rd5mxMQlGfLmg3QqCyChJJroccqjSEw8Jj42qHTzD2HCPCUNh2cJ+zL" +
                "oeVQEpl3I4tMZA4fEMT3smFM328ZL1gghqxCdlIN3FkVG9+M9xAaF+k45InKuQspCGDqvRscEuosoHGs" +
                "FygxaLGGq4aoD3tq90S1AToDTmDWdyGkB8u2K3PowOL2gJSrLiVfVMKqQMfr2Y3hDux2dkmaw/WLZeQn" +
                "dCV2JG68T4upTIAjBHy04j6/mc1NH6Vqj+dAu0gdCc1yp1kI1TFMbwCStt4qJ8JY0kyy0gWw0sqBR4uN" +
                "B8B1yDjjyCy54TLGDuhCPgtKCzxSCOKpClA5Ltfcl0QIRw+0H/sVqPsHcQY5u/RKNLHoyxorNZViluxO" +
                "2KuCjtVMqUXN9yoAZWoWIUohp5UsqUeyZRCPLcLMsg3yaFvs5qI3Y4IyCCWVjgCZ+2Jx107g4cgKSI8V" +
                "24aCAeuPdEUM8irbYfuwGjvZef/yr8t7D3iHdHWAq7HoQ6MKPZqkykJJzRhjn7MgK2YwOZgdPcDrRCti" +
                "ZE1R4K7BblWMsuExtqzdHxN8XCe7jzt5VbPD809MM2gHNUQA0hTHTTmWCHKus8gKeghYAclqw65POoMk" +
                "vsKAdoiihia7dl6Nww4PKXOhRyo9Zq6usS9MAu9t45L6AmLYmO0SpkNcMCeo9GvJRgaqidOxRZ6snFOy" +
                "FSVqSCqantIqBTdVti6j+Ioa37fL/3QumpUPzs/P2aPswTj7Bf75apz9Cv9oIH5+9vL81Zvpr4+2HsTk" +
                "kDz4OaTiRGHSOQ0aCP7lnPutBtuYZ5zP+YYClz/DeYQqiM8t5ss1SjsKrbrn9CI06tK4Ka5HKe0503Je" +
                "mYUII3EqWUfJMHDLSmu7vqV+WGmfoxo2Lht4lXKGnmUs+OfSeCGTkKWy0BVdTzEL+DvgoH4sxfjfYJ6s" +
                "Wi+4DZaaNHEiuQuSEM4OkcDCYx4iHHS2wUX31q5EFBBQ4CA00GkXKa55aUCXIKraUtpR1+5l2bp6hRk+" +
                "Qgb3m/J+KTpBqDnBc/kRZCIqrICvQYYL4qq26n41A2ZAB5nJ7L/R3RONUtl5h372/bEmKwz27If3knQl" +
                "qsN08hmKDcT9ZT5t8e7HvFxgCzM7jgmqU91VcMZTI/00T0eROZGDTGnS820W7fQRlHL0MhR5xhb723fL" +
                "/5ELJxFnmi3xAkyCXfhCEB5znew/Jl+YeIJS8S2aPZPVoOmIpWtrC7RusGyg3uQ+W4/9mIHRCA1PKX2R" +
                "Jpel2UdQJcJAZXszt9MgLFNEaBTxI+DA93PcfEotEBRqFBwgh4nUJ5O4d1pD0fiCFkJYVk1PKe+6SES0" +
                "tdRbGuw7iaWtkdieqIjeYl/nrGvQ32IxAG8J1g2Fjh0u5fe13Kkg1smErfjVgKPIXK2A0JxILbkyuDYl" +
                "hUdqqvetiXVlubUUlbjsUICJ2NymKicFDOjUEssOVfawfwXJCl4cB05wBp0dvcG/z/FPfjzlx0IfXTSN" +
                "uwFDZnW2CXh+TvvEZQ0apKm9JAIiv4TGA0c1FXoElHeZZ4f7QolYg6fa/VZI9FZ7qSQuevc+m5dXtpjy" +
                "9azQaoWHTWir2IeLLsBCAPXV6DG/eKrPX9Dj0JUfxk9lPApzVXHk2SB69cKPfoBfr/kHQEK5TXk3GO9z" +
                "gxl2HH2Of+pYek4+icSs0mPpxwm84ZG9FC/SgdOyMtS8maLVUFxEVU2MkVxFfnq8zcYEXon8UXUvaZGn" +
                "V6NXtNlTnIstSXw3iZfSAsFgy6FL8JNrwWVf4/9TToQsLzuBeKLrpe1YcaaMpDkscOghLuo0F03uyTaD" +
                "eIesSYswp5OKKL04HzcvgFFyrk3e79wZSQSJ5Az06ARiuy7plKIsGgYQ1ALFN4SQQ/xI3NjYd8Viy61S" +
                "QWy/52lSq9kwE6zAYpQYeJ+++pbitZjtx7rjYOkXOBbGJVvQ9Gnh5tOtzQKz7vBodqjJl3BYxP/SakM0" +
                "OBrp/CB6zHR4X0EvxUXpC/diboWd4k1ezEzg5Vp13PkmsF41JY9LUZ25AgXoUOGBgZQOAte6sqbdN5hr" +
                "uEZY6+QkB8/h5CRRy9J23DcF4wr2iYAf3rm6De7fz4AJpUwQAWK/pasKHxpUC+vztpRsC3EQIy6NPBB8" +
                "/daz7LSObveyAGAv7yjcfeVJ1MLAzYSHrb2k6g31r7elR3HLj9Icz2yDGfrsDwMxU6umq5iCUphH4zhU" +
                "7tFsdofe8zT4Hrj7KJ5xCt+miQFwA0YuXgGTBV5SFeDl0YQQO4u4lF5uqiK3QczZsYTONqQPyJwyIbi+" +
                "N9TYO1eAdT+imtf7ujleNgLHC3thtseM6A46Jtb0GLkvhsRIp2p2lG4u4Z4TOZ+hAvSl77xwtiofsjN+" +
                "TDtQNhBRx8z7kMIEDLVwI67UncvCTKVnmiTjqczhQ+1wkv2EjjL2YnNvtKhQwqJ2GNDx+Wzd6yaDN6a+" +
                "akoGW+kGCsPJPqLvtxFuROoxNtu3HzVZpR1FTCdffsCmlJauh9E6idSQHcd+FbkNHXggXouOxCGPTIHm" +
                "Xpocwyo+wclopwYcLvAR/+zeHNu9OLbZvR52Cwpl1+wAVm927lOxDkDukcMlWgQWK+yitdxOhNcBCrei" +
                "osfcoS+n6pqznHJJN+7HjLxdJzBdaFym4qXfeAgDki4mz7XO4G/ynIXtovyTA+hk9wtQSqSksEaF+blB" +
                "1caQ12taVRaGOOxvb06/JZ12jAb88AqYFf5n1pqy66ggSi8lnZ9+0SGFjgnJjuw4XmAdvh/dkRG6Ggg0" +
                "NUqAMsLL+YDwAIb/V2O3q8bWeG9n+dlqTIf/X1Jj12mx9BryNfEgtTKF4G9r0BoOFAfgv1vvfiIywUum" +
                "1+1cbQpQKyW3SkJRrYTiwdrt3NjyW/efRuGaVnqzLmnd0RtFt4QkUVsQVM3ko4M1vHM5a90F13UcaQxs" +
                "oDRcETH1gmr0KG3AJYqkDIm/ZdztYMd8s+f8uEdj6/KvtwA8ySwiSLniz0KRFos/2V2+jTj3mhhN9NrW" +
                "01AmDFEOCbnh1Ey8AES6BgPCfc1gu7eMpRNRm7exSYVa1bVOsQOb3BNKQlAcRhveeT7XFDkdF6Xvccna" +
                "SfA6KYtQOsTEjQaed/atF0pbZOECGoEQvEEx2iYUL6qJNgmCKNAXi5yGihpUY+iepO7pexFYV9yEtPDd" +
                "ABiDQbn+FrToJs1uxwlRs/LHJzjVwClhvZnFUJHlpRvCxU4bl370JFTP5IIwXbnEem5EBcu0fFtSKvfh" +
                "+yRYuKc8K2e/wzXjo9DmQ3vUNkcb1W5ot9ZW/MUrSUnJxnh8+HEkyW986osqhUXotr+msh70UG8dxhjr" +
                "EBe1W9f/hDreriw+FlM5jq1aIfGhfi9fz9jbI1oWodeDCXhI3KNXxF/A3s+7o93P3ug5420GYgqM9rVv" +
                "j6gTpEcyiOiALbibVfph6flbXIEvP2izoWaU9HKP3fkgVwKwdFeUrV5iae3+9pQk8aUEOL/2DtHnXgr6" +
                "3Ds+L/i6zbX3cD55teZOpnekKE7AXJPFVl3RZgE7bj7SJHpo8gl3ELgvzNRpfxJuMbgjhAldy1+eqq9F" +
                "Et9F6B4X0uEWFcBuHXmi9SYZBDx6Wbreg69or0r8nJjWl6ioQi0asw34Oo9PT/ECBsaF1P+XLhKabpLq" +
                "aYbBR4s+6KHF/qZOPk1AqdEuX8oKkSfK4oh3enP24tWPZ9jpDzg12NpCUV748gHHhaJYCWivWZ6P4xoq" +
                "1zRJ8YRTiEi+fn328hQvt5ASjnvu3452GXNtkThfb4kh/tSno+emrr7eg6bSI7nxGEaWdJcOaQWkVY0R" +
                "tam0JDGIRJtjBPBVY9twl31GfUDorOpAp69vSil+WqmM7vzu/2Svnnx/9vQtfkPx90+W/yBxnn68DqBX" +
                "wPDTkmjwRJGBGqMGNnDzveXgEz1GOEJun19wajmEV3KX0WDUPnQqLmzIXaY7nNATnh8D9FYZCm/31Fkx" +
                "U2UPq4Q+kVkKihhYyi98f/7q5T0s90rS4ZfHL37IeAEI0gMLg5oNApB8WwEVtVJl+3qYGpRJdkZeQ1nv" +
                "OXSSo9DIWZUX9iT74j8OkMIHJwdP0bM5fXIwzg5a5zp4suy65uTePbzRWAG1u4P//IJR5LJ57TjjUWvd" +
                "kk5PvBv6tFCkAt8mO4BJJXcDXVgrH9acVyCq3DE4GdjLAb/m9N1EJKJ+K+L0CfNG+LQYyr3szLkCZK4e" +
                "6IQqjjNH9NlOzCQJspTypmVOskAAeoYkgGfbJDj5479//ZBHoOnlJikYtwvxgex0/tcfMjg2bzHPH85p" +
                "sPH5b9UzHcFr01bZwXrhj//ET7CqcpL98eHxA/oJo1scUKKbKyPA7K8h4N16jB4KIqIbaIGI365c0Vf4" +
                "npo4OtccKEMDa9/8pSSynXszmJzhs2OxwdFGB0NurrIv0UX/Mss/wP8V1P1GHSQnj+Bw7Pzdffwu2Sz8" +
                "/Ap/5uHnA/xZhJ/H7+MXwx6+p2e3nNvY+iZNzACkaVMy4DufomEvbRI+uXpHXYqdkfmyrLT4jwO3s3mx" +
                "kqdfP8VlYNSfTbZs7fzRFyIS6/KinLTOT1y7uNfNv/hLN//zPfMXYMP8AhailN45BPQYuBcu71fhdOfS" +
                "BZ8q/J1clnDhENyMo5fQ8K73HHEQPx29jQnmkDO6jfB/b3OEqDO9hggUAP8ifLSOO65oXAgvaYgk7jTo" +
                "AB1/WRYY2ePbMk6+tlXjTuZBSvB6t9+s2F0fy2dhB1/SS3fbxuAM3wWI+LLkngv42w0OlA+HM7fVfExf" +
                "iai8Cy0WaWcWE4P7s0J+OFJokjRc7eKpLYpgm9v4iZrGlNThxBQ+rL8Ch4/6dbXSiAaVbXz43OQ1kMfe" +
                "vXAtaHf3JFWgHfImBMKMCgBB5W0CI/Gg9VMa5GMCduQAzxycEEygNMsDCs4G0NJwroQLFeVuj3wAmECS" +
                "XjJOKNXZ45enqX8ZGE0WmKYsgNXxnVd68rcvQ8SBmMcftgnEc4DQJL+QvlNujCsUB/15C2AnLU2jO8lX" +
                "cO21mTtth4pfy0ozb+GLO9oXdTsoUJ/V5yKAfVifRECatW4e/KQHa5hwKVkzcJFsrSHshlu9dtM0KvAq" +
                "7jSmCLkk17757sljeXHTn9gP+4Vv5bXhr0X4axb+MrfeTUm9a9w3N2xq2k7ivnuf7W1Pept05pHmTL9Y" +
                "FxMucX10nkcyQ06ef/wEyo4+ZiovbyyO/sjelF08PqW2QuyMBaeLk69gWaixG8a31u5PLKaXMx2N25uW" +
                "k1KwfHx6N9Mkl1lC3y7XA2RB/CbuPgT+GUT7bxOLWtqoTQrvmoDNl6mHVFPFCtI9l+d9A0b2CA0GtbTS" +
                "E1PnGyXF4WTW3ZugM1BW2hXGC1FCojJ4AyW6l05vCMr0oeZ4Y+liPDIotjyvjsLtarwmaNH142m1K+LH" +
                "hvk2Y693bAQNiOpK0KwfQhjEU/kThdxeQB8XmSy5aGC4aXzdlp0yjsde7q/RjKO0jP4LqErTj0VkAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
