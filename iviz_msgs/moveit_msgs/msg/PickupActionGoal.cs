/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupActionGoal")]
    public sealed class PickupActionGoal : IDeserializable<PickupActionGoal>, IActionGoal<PickupGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PickupGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PickupGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PickupGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PickupGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupActionGoal(ref b);
        }
        
        PickupActionGoal IDeserializable<PickupActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new PickupActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e12196da542c9a26bbc43e9655a1906";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09/W8bN5a/C/D/MEiAs90qSpp0Fz13vUAau02KJs7G2X4FgUBpKIn1aEYdzlhWDve/" +
                "3/skOSM5ae422jvsdYtaM0M+Pj4+vm9yn1qT2zpb0J+BmTauKgs3GS/93N//rjLFs7NsDn/GLh+8dNOr" +
                "doUv6dXg9B/8z+D55XcnmW9yHv0p43Q3u2xMmZs6z5a2MblpTDarAGU3X9j6XmGvbQGdzHJl84y+NpuV" +
                "9SPo+HrhfAb/zm1pa1MUm6z10Kipsmm1XLalm5rGZo1b2k5/6OnKzGQrUzdu2hamhvZVnbsSm89qs7QI" +
                "Hf719vfWllObPTs7gTalt9O2cYDQBiBMa2u8K+fwMRu0rmwePcQOg7uv19U9eLRzIHwYPGsWpkFk7c2q" +
                "th7xNP4ExviMJzcC2EAcC6PkPjuid2N49McZDAIo2FU1XWRHgPnLTbOoSgBos2tTOzMpLAKeAgUA6iF2" +
                "OjxOIJcEujRlpeAZYhzjj4AtA1yc070FrFmBs/ftHAgIDVd1de1yaDrZEJBp4WzZZMBttak3A+zFQw7u" +
                "fos0hkbQi1YE/hrvq6mDBciztWsWA9/UCJ1WA5nzE3Hjzh1BrCXIZn5RtUUOD1VtaV40EVjL9cLBgtAk" +
                "cLtka+OzGhnGwySQgZ7RehNLAklMKYPBItfXwBrrhS0z12QwUeuRaYEv7HLVZEBw6I0wPXPN2sLQAXQ2" +
                "sTPExWRTWzcGVg4xSukr+Ltc1wTIC+htcJBA52xmbT4x0yvALIcewJRt0cAe9N7MLS1C5ld26mZuyhMU" +
                "DPxIoOMG4QaA1LL1DWCWwa6DViNdP1y5T7R0y+rauobXLYouGE7Hbkw9t824BA6KL+d11a5672yZj+1s" +
                "ZqewxvD2u9r41Zu32ary3sEuGM/xhU8g+3a1qupm7Nt6ZqZWwQ0Gk6oqcP2qNXRyq5Wtx9p2WhWF87D+" +
                "EQ6MYZrGTBc2H1eT32D8cVO108UYNtYVjScQl650S/fOaqvcwULDFobvT0AqNTVwQQM70DSL8TS+SDBe" +
                "FaYEIUk7qTM+4grD87gMHvvNiso0f/4yfKf+0GVMvDYYvJTnixWytM/C94pf7GPRaZ1UDSjfwvRxTyDH" +
                "A99PYRFoz1UzeEHrKFuKNjXyK2zPAwBC/N7RCcAV95QrcOdwb9jGTKUhqoCizZG6i2oNLRCMWcGWgxWF" +
                "jT3MkAXoh22mo6yLZ17BriyrRhEGwJsD0kywuZeGkDaTqgVxkN3hoVcVLOqd7MggZzpqcfGCRBJjdDxC" +
                "AD/B9CzKF9aKDcqWdGCRZwtzDSKkAB2Wb1BdTlzJhNhGIBndM5222yAWc1uBBq9pGkBvfNWnIQy0ahuW" +
                "owR2DFOxqI5THGEeCONxhvuKjAGWstjhIEoWbkWyCLRtXYKIAmhNC7JRhkclJf0titd7AgLwK0GHKxmB" +
                "WWrlBNg6vxG6G2ay73Har8NLhDIOmONYH4lFwCAZHD4DkUBIfAwityARWGPXCnSQUI50nsmDC1HNEA4+" +
                "3oGdAOqbRNGdXcCGxL2g0lqyvBIQysrYNfvMlZ9t9cWhNys3lZ6AQdiPKRz4jXCWMMeMRB4o2Rr+MmRR" +
                "g4HCR+kIx9TGQ6O5KYGflDdFXcAAl2ISRjaMRAQt55Zki8BunpiJK1yzQYx8O50Cj/Z4cpjBo4cBsgqw" +
                "qQlnsA2FBVA43JlXVX4Htb3zgI1KVx77d6AgwI/DByGSu9qyzQB7pzFXVicNtLvCTRCk0QFoLdI3wCGl" +
                "L3hrRl5ViHGMGqhhTbNjCDMDNg7ScmGQiGCp4NYtLNLkiIckq03U1/Hu8WHhBAEZbnv8ZaWDQ2eUKmwX" +
                "1bYQ6zrKt6+Z5InwIXnFYrS0uDBgaHaZHEjubTEbZpO2uR3yULk+FWxrVxTIkQEy2z0MVh0NRdrQkrQr" +
                "wQhm19Yl2fikUnQDsZE1BGbZmguR89q4ggxvnATtIYOLAaONbqcw6F6wQToUxrGW5sYt2yUrGFguAAlG" +
                "P+AM0NB4LWQuSIqjv5w+wE+wy3D4Y2FR8GgAylggjAkCD4CMR5oV9mSBWxImWE1gi04LG6xWVjLeLk0J" +
                "SnVbazCgnJtPASTufzREbH5/1Xr8g/o+Z/2Aur2tWTIo4gcfsGQOPpUd8gHpPHja8bsDkr9hMzIX/aDX" +
                "5SU+ktXJpts/BW9CAlblHKVPk2g9fB+sfXBiHQq6qMPeDDPwisGxaOArPBgQkwVaH/jx7VtUuN3WrO/e" +
                "hs2ajAVsCHxgb9DXZkfqMWzE6Glcm6K1rCmFLzyaCeDAo9hiZUZ0JvMB91jWm+UIph5MXDb0GbHkXZxO" +
                "8rIzreQ9z2aQt/yJHLLxrK6WY9gR8OETLeatSo2MJ3xmc622M/Ag0eXvxzl4+2mMose1BAAV4/7wJ8Rr" +
                "ixESMD6MGjJqJM7AQwZOAHE3JH1UoRTm72z04HSq2mnfUTYgjg4NBn9rUaCWBDe229cEeXOR1RX8lKZn" +
                "tBnZbJ3pBovhJvzahF/v9oN+JJ3OISyUJ30Q6dlFHp9+j3RHJTAafGBG+mu9H0eyr1hhhrmdgUuEfmST" +
                "KNxoXFCXIRsCMEFW/yTmUB2DKeWvPCo47GFvDBpP9Ltws6ZjusGi46o3pPehgdpqaqI0wSBAyKT0VMVH" +
                "403siwTVvs37I8m+R2r2hq4JNOvhZd6Zr0YaokEgrcbxSzA4XGzPOl2DQhiTcCBSAPgMjS+cUDTfsb8Y" +
                "a6BZrsFGA7MSI2HoBgU7xJXJkHth+C7FdjF9Jk3+R2JWYVxL/GmPU9s5J8aju4VHWYhnkucM7g2IKNDU" +
                "oSfGW5SlyFCuaX2H6PNorAP9IvQwoL0nSxRYHYB1dxi8hi5HdjQfDdlip1a0ZRALCmuANVm7uRNejWYl" +
                "wtRFGWbN7CFb8YQzDwb8hsHOigXV8Sh7Nss2VZutcULwo5YcAxkhihftwKaqaLsLiB2yPcRYQK43sLgf" +
                "lHKfZqlT0ZaEB98TKVMLXpwxDeZlSSyRYktoiCXvwv72QExPxhmKKcPhYiZhsvXBcOOgjURw0DzDR7LO" +
                "IqLBTE5GwmYvRUl2Wqrm7De+iMqo0z5RUv0uPzoQTOTpd3pch9fdDntYvB5hYAHCww6zgW1epjrKI8px" +
                "aYoA3K0yD5SPbkiU3tRCuqP2seR4+exNGOUefEXrFyTceALm8XoYMfg8+Qa+3bV9G6Mc2ii+6bXd9YEG" +
                "0Djg2rr5grTmzCSBLDajhDrZUW5BxliUZihKwPN0S4y8szqqODbTYensSQEWJAUm39m6IqnmswIDPNq1" +
                "OY7IMRb7yGZscfqte5f1NlgqHftRFiXOlSKqHbWTRGKqSSUhtT5JSaN5Cm0L52CzhHGwP5gv3jaUQkRs" +
                "CBBpvePo3VEmRgzbKmm3tuTHBYEDHW6xWzSbQzDGPGbE4boqWs0o7sI/Oxh8g/wNY/zITWOrcW3nwQ76" +
                "38Vre5EyXcIADc7I8kVLgEkFC/voTMmDS42B0ApkuepA0Mar2i0dEkKCP6Tn2xVnk2VxKrGpsyODSqdF" +
                "bwCm7BdmZRmVS4T6UkGheA9gOxFuNMtjAtamoyM7geduyHjcdjBTmBT0FcDPQFjmuQuxyABwiHtuYb3G" +
                "p2IcAm2NnP4E8YlcI7M9MnNY+CEGJHZM9DnABFQY9HunJqN/eFrYUGf0qUo3blupyDST6mYINELzD1z0" +
                "DWzyHL0k2Ak200gOQuEZMT8QDSlUCBN2dSQlAEPCW8yrYCy1JhX1YAj/A9MKSy6+yr65+Pn0C/l9+fLp" +
                "+avz04fy+OSXH569ODt/dfpIX1y8OD/9UtPjWEaifhPhJK3w/UAb5WC0l57SNJ2mMfYTW2gf3NqIftoh" +
                "aXaSWYyukXBAo1MdTY4/5/ZGw1qHsc8hTL42GxzhW5GpMHFCdUhPPw+zX4bkZvya4mwkQVLYcg4eimA0" +
                "rWow2lcVURmz+FQiIB+B6KNI2/HPpw+Sp18CrfHpVyB1ihLTX7AimxuXnQI1JboIUg3BeIKRPxd5ATrI" +
                "5K5FFMQYYQ4addZ1/Orx2bO/XwI+6Zi6yAQTF5jLg5gqzDqoaqiUhY0/5KSiooljm18zc+M8JcPEZ+nA" +
                "HT89f/bd09fZEcKWh+M4J051JhSPc1qQCA80l72QHeFeOObx0KLTcXh2Mg4/JOPcNgr6Mko7Xj4Tcmc7" +
                "xnyCC4KU0k+YiOwKz2RPovnuasqGc+6jcavIQ0RTSmTCIiG/t6shUzb7XIg66O1EoV9gqd7kgbmSnbrV" +
                "OBIGG35yEYcyWgVbYl2hnB1QdpN9JlB/ppyDIv862cESoyb7g/RJSPbC3r62WGNg/Zu3AxzjtQAAWRJg" +
                "DYQ5Ob0aemzrVsJmhytKEWzutCdS6TR2kEyndegjUlww9+YR42lvxkC4T4ltavHsdA4/xsTuBjv/mJmt" +
                "0bW0p2YFowUezQuykWLYtK/1dwey/zFWfcjs4Sa+R0uX2bqmEg7xz3zENBqtZgJmYdvY8c0Ye45D6x1N" +
                "Nh9u8m6ryb+ohb4rMiHLnMyYteuspcjLEl+h4x/DFxwcy52fZkfRKTveqoXlAtgD3Q/UASNsvqNfTIC8" +
                "YV2wXmCSBjUE1QVwdh2DdIHNMexX1QT5uaBHBnPEEIdDWJNNBm3bWkPgzMyhZAoTzXU75TGS7ogHgX8B" +
                "K38SZpDQiEC8uHgN4DkdTpV17RLj7pov1ygz1j00ayx9iMjHjLXSD8tgaoYLFo2CTZxe1qNi0sF+KShj" +
                "de3sOqm2EdJw1q7n+5PncYSjcjEKmFVcw3Ws9Wu0AXwLRuWqrckFGCWCoGMe0GqSFpE6sIkNvIIhOsfB" +
                "+SQiw243Q+l6KinMrzXzykuF3Cs2WL8lDAcrtQCpYrHP2hZFXCn2sZRYFDrnkEZkzyjYhmlu5X0lPiF0" +
                "oDU+39bVMqG78mlTrU2d+87qBrR5C5g+v6VcRkYmpW9upHyopUKTpSk3bGSPyG4VlCW8zW2+5AbDjFnC" +
                "cAIQ6zZQ3fs+q3PMgmJ12WoDhHJ53LJkfegCm2JtNsjL2SPicjBwoSWXqyPOYxq3t7IwNrug3c3JvBz5" +
                "ZaSdeKlcroPGtEBPvwGE6NsK9ZM6IgLIeQbjPfjRedoQ2aOoqivJNGBMv+ksFpdRJomIQBKCttI6m9Zz" +
                "0RoKE5AklsuDNAAAim97F1IuDxGAeVXFtcSaHIC17y0vY9QT3ju/JolStfNFZC60O7Z24XCXfNM9AlYT" +
                "8NTS8O6Z2Klp4z7bYWPQMJKmQtXdaOBCRQ9TXWue+5lLaEwiS0BdAVtRWR4WOQALIg8NI9dMDadJtpBX" +
                "IlqQZi0Wl11TmqGoOB/0QKIn7G3QgGx9oGLo+CVkHNMnEdIqSUvMBRU6uWR8IoDK7pg4lWzXbKdECPzN" +
                "O4pi0mmyTLKerDbEvotY82Y3VCOGBMcvUmkWNqkWmT0QnbyuBCN2xoD8mCBekf9g/bGC1GI8dLFFGcrQ" +
                "t8B/6e4/5CFS8IDZyqLdjZL1eJRKDyU3z1nK2tKysmR1tsQCA4oaxghQEezVCk8+oGsOrxH00YMhYcjp" +
                "uAc4lFfh2+WCRHfnSSElVqlhwzE1VGGG+147kp704OnCTIsNm0hpH5IABXrC3WkKwLs7jAFVG1vMpFyG" +
                "5qsUt1VlifVomu7u653UYBC1xwyYcB/OBmOKSsWaVGggnZomPSJlbkYxW9xdPYIRgC7FUpxTGVxl5J4Z" +
                "Kd3u1B3QAmmkP5pJP2en2cNh9gv8+WKY/Qp/HhxoPOf8xeXFq/Gvp/03v5x+0Xvz8+lDfSOSlJasV73w" +
                "L+gT9A5gRDfTzWZcac9511j+rjkWP7UYg1cnD9BXWJf0JRzloIZjBCgx8hlTdFaYuWxRYl1SoBKtkKJc" +
                "rn+lsiAu5KMMOkIO3EsBSM/7Llj2UvghnZDBQPTQERjsO8aY4kfhQsVhOvF/w44CuaReUjOKPcmukAhz" +
                "doSUFobz4CGRnQ7mvbd2KbsDsQVuQi1+axFvKHJFhGx57eqqXILikCnhkGMesjupsN85bHT9ninF+YiI" +
                "vmVKnJdXoVa2ywmwBprWTHD/dRg/ETeFnTVooz8YahDE4BGx8F2CuUT9cIYm35RgjU7HNZ41nLk5nU5g" +
                "czOZ8FgHDjPHFST5NUvbkdaRRU0p0/IByu1aZ98EEvCc8VDUdi1C5MpRMnPqLg4HHnPxcgoVl7xMMBiS" +
                "GU0MQoF+rtU2WQmSkJi8tDZHPQhwAxFHD1jH7J4caJZYh5XSGely7cwuuiohumLdm5kdhw00xjkl7CW7" +
                "EAzGiotjqSqDnJWcHe7QdcjnFYJNqGma4KEQJMSHDvXkfJAl7NzaUvFrMAdoq9oSac5HksjGbMspyyG0" +
                "0WRjgIUFkKNO2mZbbkFbQBkpUy7jbz0GI9W2BKJzzNZxPnJtHPlZqtt3gcW8tpybTSS9DJKDItnsVd6T" +
                "jIYJleIbd8X6Vm0NUhgsQPbAYD0aePsKHy7xN78fy3ulk4JO3XmYKPM/aw/nOVUvozEYahXDiIk7RSYN" +
                "n5rJ21WBhgSFdmbZ0S6XJJYDUBlB3716rTVf4mO9eZvN3I3Nx3xYOJSE8dLT/FUkhEOYwFOA+s3B4DF/" +
                "eaIfntP7cJogdBhrB9rpRcHu7AonWc5hpB/g8SU/AT4UUJWPvS5+agorHS7xtzanD2LPiDssFaJ+mCAe" +
                "XtlrsUYrMHiWhmpP0/mtyNOivCq5XVVBZn88Z83UXsr2pARjUuJPnw4GFzTcE+yM5VN8/pRhxdqhzrA9" +
                "e+KnqgYnYI3/leALqWw2J3GR1wvbsIxN2UvDZuAkgLvVaECczJs+z8SjV7oLSIw4L8bLHrZn3FO35hF2" +
                "H5pMdibI2xE4jU1S2UVxO3RJfuPTlyhh6WAbbD02h2OhGO9oLu1KdvT33FMyRxtmiCXoF4d+/dnFt+QJ" +
                "xrwDZkG70J9jY2iYjEL9x3k1G2+NF3h3i2WzIw3zhEWjHSEFQUQJ2OwKIGxJZsH0sHTcleGcz744K946" +
                "gfEPvAhC/QC+tUKvRSCDTac7qXLcUkeKEDSk0BOY6YU19a7GcojWCJednEzB4jg5SWS3FFC3q5xnC6qM" +
                "0E9P0h7vaSvsZsaEWCbsB2LERVXkPlTa8kFtieoQK/HUpdYI3LnfW95IdUWXUfBuwKLkQbiqgTtRaQVX" +
                "Qh7V9poSSVSUXzuPe296nMaSJhu8gSP7rLPnVPUpFJNT3PR4GJvK4aDNdtP7nhrfB68B92rswkeEome9" +
                "AjUYT7YJgBeUiHhxPKKJnce5OC8XKyDDgRfb8F6dbEg40PUDTAjOOnbF+NaNFToeUc3r9RJTPEEFxhrW" +
                "6PTbDOjKFIzg6TJyvQ5tJe2q0Vg6joVjjmR9utLQO9944W0VQ6R8/JBGoLAjTj09OJkgQ7XoOFeqMuYN" +
                "TSlx6iTtKdHiQyZzlP2EFjYWlXORt8hTmkVZoV/I69O7hoS04JAKxCn4bKVKKTQnpYmG4ka4EanHs+mf" +
                "au+cvA908u6dpfPDeOaN4CS7hrQ71tHI5R2BB+ItHpE4ZLYp0lzjM8U0Ia/gaDDonTuIpxKJf7aPw22f" +
                "httsn3nbg0DZ1j8wq1dbh8RYBiD3/JZcVRBYLLfz2nKZE55ryCtYVwpyo6WnEptDqXJwO47HjNzPS5gm" +
                "VF1TDtVvPPgMSXWV55RrMEi5z9w2cf+TZVjJ6FcglEhIYX4Mg3+dZJEhu9jUKiwMcdjfX519SzLtEary" +
                "oxtgVvjXrI9DphJj6/RRMgfpBUQpdkxINnOH8Vxu9/vgrrRQaLChqYADhBHeJQMT7uDw/2Jsv2JsjQeQ" +
                "Fn9YjGnz/0ti7DYplp6tvsVhpBIr9Q77jdawoNgA//a+/URkgo9Mr/2c0QpYKyV7iacoVkJqYl1tHT3z" +
                "vYNcg3DeLDl0lR6L1aNRe5okUVsmqJLJRwOre5B0UldXnDqqSGJgYafhfIsp51QegLsNuEQnKU3is7Tb" +
                "z+yYb3asH1eJ9E40ewvI057FCVLE+Q9NkYDFRzaX9+H03uKpiVzrvQ2pyODoyJVLFLuJp5dI1qBfeEt1" +
                "2vbpaSmS1LpyLJShOnrNfPQQARhyyinxRrEdD3r32Uwj7bRmlApAoGUljuzI5SFDiWGd4IPe3QUxZM5I" +
                "z4W5BHLwCPnBoE8vvdhFw3LiDfG9NqyaU7dRnWz05pMkAF2HQfnLTQgs3wvIMSqUNtCboEKIPHZIbtlI" +
                "7icLQWU9Y8aYkRqmM9D5VmmZXowUknNyApoOki7pFiydDqWF+RCo1AuEW4ywXIBCtBxEDwepj0PBEQ3S" +
                "vREmXPuiMat4iQ1d7aeBjw9du5RbRHD73qV1p9q7typDzGpcldW6/KfkCrf352NRn8NYPxYCImoLy1mS" +
                "W8pZXR6KTZiQR8RKehz+OQz/DKM5O27r0kXH8xfEIRgH0LpColLcUhJwRNtszgW4UsJL718jCIQTgceo" +
                "kx5OsluXSyZoa4WHq/XwTW13l8kkAbJIictbj0H98XNNf/yQ0nM5LnT7QaIPngxCMHrgi/wJDEtZLDMW" +
                "gRfmyTVRGo4PdUfhDAWXrZkyLZvCQboHnTAUTPOhoW6bLH5MMXycSxVelA/bKeyRJrSkEfDutataD3al" +
                "vXF4U6YmsChZwwUjkw0YRo/PzrD04IDcSCpVTOGEUqAkY5uhr1KjyXqEd+ttGrmdgWKqzXQhECKTOLwS" +
                "iwZ7df784sdzLGygma2w4Ib8wnD/A3uSIoAJda9xoffPOKTNqZPOFtYjmerLl+cvzrCIQoR1HHb3iDTQ" +
                "kDOZtCH03BtSgQqIdAnVP9BD4JToJNufalEcHRJEkgGFu9dtcQiOyqUETSLRI0byYmXrcJp/YvWKrdC2" +
                "0u+fTHZ+WOgM7n70P9nFN9+fP3mNtwV/fGf5hwn05P0ZBT3Xhtcoo34UQQdSjqrswEfwlj1XNDdhKfkw" +
                "wJxD08E3k7Oahuo5+rbIlQ2xz3SQE3rDIKKDXytv4amlMssnQSsAmOSAxCRFSJQyhSi+v7x4cR+zzBK3" +
                "+OXx8x8yBgF+fmBokMRhRyT3TKA0V9p0Tr7hwKp6Rtk52Rqu3LH6tLNC8WnhruxJduc/DpHQhyeHT9Ak" +
                "OvvmcJgd1lXVwJtF06xO7t/H85oFEL05/M87MknO2ZcVh01KTY/SKopVRJcuRTrwUblD6OS4SOnKWrnD" +
                "dFbA1uXqxlFXt3ZYd0qXBSMd9eqMs2+YScK9aygIZGiOOBCbtXVNoo8DUJ4K6jEiJROm54wgnWSBCvwS" +
                "CQEv+4Q4+dO/f/WlNEFFzTVc0HAb7UMd7fJvP2Swft5iziCsV3fwy9+Lp9pEwNNw2eF67h/9WV5hnuYk" +
                "+9OXjx7yM3SosYlDa1nbgKmwBg+6/x6NG5yQjqKJJ/m8rPK2wAZUUtJUq8PA48jun/4EFunZnWFRDhva" +
                "oSjsqNCD1jc32edo7X+eTd/Bf3Is1RtQPcvJKSyTnb15gDe4TcLjF/g4DY8P8TEPj4/exrvVvnxL7/Yc" +
                "MOnd2BPDCmksltT81kU9bNyNwrXjd9X22Go5XbhCSw6wYT9EGDOEegM4goFWfzHZoraz0zuyO9buyo3q" +
                "yo+qen6/md35azP7y33zV2DF6RVehot9Lq2laEBeTdtlWN2Z1POnimArQCZs2EU3Yx8olO3roU5sxG8H" +
                "r2PUOgSi9hFT2FmPIdJNz1wCBcD+CNf7cQUYl2cET5XacDgwvaI3d9cux3ABfnex/+0VInczDzsFz7P7" +
                "zZLt/KFcj965d7AzYH8a5/gxYMXHQ3fePNAvp6BYu15iitdlFL6KRR1puRgThYvGQvA5UmrUKQLbnq5W" +
                "UoLqruNNPivjqOCKaX1UfgGmIdcaayYTtS3bAOGWzluwj7WF8eDT9vhJBEJL/U3wq3k6gAal0BmRxOjW" +
                "m0XIIIUJksE8qWCpoAfFcB6Sg9fBl5pLul1oKUeX5Ep8QkpK3DhmVWaPX5ylhmjCdwJi3GEHTMJvfVMu" +
                "+CfsKmJHTBd0CxLiaoBLwzcMo/ykmr08zCI87wHxpKxqcDe5Dty+L0SoZVnxirFOhC/cS6T1WXuaB1V7" +
                "fcQssCDsw7OQsrF9HJuOpWDdSI5jacF5ubW6wRsuOdsZAlIxoDKAm+UhXFXVr7775rF++dT/TzRhwHDb" +
                "YB1+zcOvSfhl9l7wSUV0XMTXranaChu/eZvdUh31OikVJJma3voXYzhxCLS1DwbSRViAH34CKUi3w8rH" +
                "T+eJv2dwCmM+OqNKR6zhBeOMY72gdqgqHdrX1vbzhdvnVytqtzPqJ3lotpN2BK/kmI5ClUyEAMRbhndN" +
                "QGe1T6L9t4lFNXVUo4VHZ8AikK5HlNDF9NX9ajptV6B/j1GNULktvTHldKOkOBpNmvsjtBRcYY+5Jo0B" +
                "4RhPCoMHaqIZWulJSOme/r/JoI9DFwQgi2Jx9vI4HDDH45AWTUTuVlZ5vL6ZD262nNq8q9MAF9CBlH0X" +
                "3CXuyhc9cm0D3bgyWnCSwnCV+7p2jTKOx7rzr1C7w34ZDP4LFoCeyXJrAAA=";
                
    }
}
