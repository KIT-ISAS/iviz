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
        public PlaceAction(ref Buffer b)
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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAAE+09a3PbRpLfWaX/gIqrTtKGoh9ysol2tVW0RNvKSqJWkp04LhcKBIYkViDAYABRzNb9" +
                "9+vXPACStrO7Zu5qT0lZAjDT093T09OvAa6yKFb9uEqL/FURZUFEf4YT+Ltz5Z5dK11nlXla0pX//KVS" +
                "ySiK70yLsVx3jv/NP52Lm1dHway4V2kVzvREP75qUtB5raJElcGUfnUYnywdcWNscXYaIHlhmjAFRDcR" +
                "/GWQ1VXCgzNmnUfBTRXlSVQmwUxVURJVUTAuAON0MlXlQabuVQadotlcJQE9rZZzpXvQ8Xaa6gD+n6hc" +
                "lVGWLYNaQ6OqCOJiNqvzNI4qFVTpTDX6Q880D6JgHpVVGtdZVEL7okzSHJuPy2imEDr8r9UvtcpjFZyd" +
                "HkGbXKu4rlJAaAkQ4lJFOs0n8DDo1GleHT7DDp1Ht4viAC7VBPhuBw+qaVQhsuphDgKDeEb6CMb4AxPX" +
                "A9jAHAWjJDrYo3shXOr9AAYBFNS8iKfBHmB+taymRQ4AVXAflWk0yhQCjoEDAHUXO+3ue5BzAp1HeWHA" +
                "M0Q3xueAzS1cpOlgCnOWIfW6ngADoeG8LO7TBJqOlgQkzlKVVwEIWxmVyw724iE7j14ij6ER9KIZgd+R" +
                "1kWcwgQkwSKtph1dlQidZgNl8wtJ49oFQaIlyAZ6WtRZAhdFqYguIgTmcjFNYUKICFwuwSLSQYkCo4EI" +
                "FKAzmm8SSWBJlMtgMMnlPYjGYqryIK0CIFRpFFqQCzWbg1rJMuiNMDVLzULB0BZ0MFJjxCUKYlVWEcwc" +
                "YuTzV/BPEzMnwF5Ab4mDWD4HY6uk8gR6sBaDNah1NFE0CYGeqzgdpzETKBjonkDHBcINAKlZrSvALIBV" +
                "B616Zv5w5ral9UjfwY8MPSmLeh7mID/uXlRVUTxVSViM/q7iyjyl7ucFqAuYn/cfgjleh5nc0Ah0VBSZ" +
                "3Fdq7A2j6/m8KKtQ1+UYnwpI6QFzWSzCSZnO56oMTdu4yLJUA2hodgIDVCVMYwVLKKqmYexueMPA0Dlo" +
                "OVoK9i6gSgMAQVVRx1MhC/uNsyKqvn1un1N/6BKSsBDJdD2cE4mBfV7MheYtzZphO0hTP0DmkfbnZVVG" +
                "er7jRGmns2NEG9RrmYNMzgtd1bAYijGtTdRK0l+Z7o+wUcpU4mM1hgbA7Ai64eLc6QC3kW1FuWS8figA" +
                "/q29SYOEPPcynsPEwDYYqDw5gBGoZxMTWD9ZhLsHLUO4GsOyRj1MOi5AgHtmk4gyWK7aW14q0woURqm6" +
                "QV5UuH8Rh2S17u90JqqA7dNQcAWq/kb2PIu4h3U0B80AKwGmA5Hf6bxiCQWic53RfKA6EqJNa9e/hKEU" +
                "oPqx7o5p0pq7gx5kEYPZg0VQIeOKESiuOFNW38FEAp+0mkU5bNFALnByxnCjUVFXDCjh5jGAHCFXa1za" +
                "j+e1xl8oaAkzCvaioi41SQlNBYjTzieW0M6XWgCfELaWwWaR/Ds2I+WiO60uV3iJWqtgnfG74E1IwKwM" +
                "UKoqf+3AfSvIYP7AvgLk2SX5vhuAPQVbUgVP4SKKY5WBNUcPP3wAiEWzNS/fD3ZteWPBugI5UA9opfEW" +
                "3M8ybxHdR1mteOGLXGhceWD6AUaRpjvEZ9JDGhu1qOwB6Va3EssFMe+eI8e72SDLu8/UdJKaH9FWHo7L" +
                "YhbCioAHX2gyN2oL0sJ4zQaAU1JtC5mXn7FuW1JLAFDjbA9/QrxUaFuD1RkZlYxI4DyOwbYCSQBt1EXn" +
                "AG8n8pzVN5JTlKnp2ws6JNG2QedvdYR7DsF17bZFIC8ucnjAPkCrj8XV334iWWwNcq0h8GD/Wtq/ft0O" +
                "+o51hgY7UZr2A8fPJvJ49YvjO24Cvc4nKDJ/LbZhwazue0BhosZpTjZ35e2HzhagLl12DIDAeRrf1XNS" +
                "c7hbBlWk7zRucNhDPcDqytgmytJxRUYscIw2KZh0nPWKXDVoYPZpbESqkR6MBTJteggXHyVpqWLfbvFQ" +
                "bRsTb0n3HRp7wnb1oCkNN5MGvQns7RFojh2eEXCOpVXonpj+s9S15z3duBNoDIMDg8DH6G4jQeL/QEfs" +
                "z7YVOtb34E+BgY8+FFp1ZlgA7g25FYFvcmyd0AfS5F9SswbGPf3eKmlraWI8mku4F1hPGPzsJVisYNLh" +
                "Tm17QkcrUuRaljS/XfSNkwLWERi8AGMW3aFFmGu2n+dzANZcYXAbuuyp3qTXZe+aWtGSQSwoTATWZJlO" +
                "UpFVZ1aSTS7EdYNq/AxmBowHwpkHA3lDN7lgRbXfC87GwbKowbEGGuCPUqJTZIQYvGgFVkVBy11ArNHt" +
                "1u0GvV7B5H5Sy32ZqfZVm+eXmsk2SNodKLIWPDsC1osMPCe2tyOGmHfPrm8NzNRknKGaijjQwCz0lj4Y" +
                "buz9iSuI5hleknXmELVmsjcSNruSTbLR0uyc7cZDtxk12nubVLvLW/DlR2mWVstGj3t7u9lhC5PXYgxM" +
                "gL1YYzawzctcR31E0VETXAJ3K08s550b4rQ3tZDuuPsocrx08N6OcgBP0foFDReOwDxedB0GX3vPwLe7" +
                "Vx92rJybRu5Oq+26BzQAY9cHVzKdTGnXHEeeS85mlHAn2EsU6BilnYeezjBOw9tRERTksvgiHZxkYEGW" +
                "+PBXVRak1XQADqy2Xat9hxxjsY2Iyoqkb1y7vG+DpdKwH2VSHK095GRj2/GiAMWowABvfrfCUtrRMKpp" +
                "JQebeYKD/cF80aqi4DNiQ4Bo19t33l1UTlQlhm3htVso8uOswoEOG+wWgRESjJDHdDjcF1ltYtHr8A92" +
                "Oi9QvmGMt9zUtQpLNbF20P8uWduKlmkyBnhwSpYvWgLMKpjYw1PDHpxqkDJdgC43eyDsxvMynaXICAn+" +
                "0D5fzzkPIZNTiE0d7EW46dToDQDJehrNFaNyg1CvDChU7xZsI1aHZrkL3St/dBQnCvmi8bjqYPowKZom" +
                "gM9AWSZJaowIB7CLa26qtIlPuTgE2hoJ/bLqE6VGqN2LJjDxXQxIrCH0AmACKgz6o6TJ6J8mCxsair5U" +
                "0m/TTDmhGRUPXeARxzjjJSzyBL0kWAkqMJEchMIUsTwQDylUCASnpWMlAEPGKwwTY6atpC3qSRf+A9MK" +
                "k3XfBS+GPx0/lb9vrl4PrgfHz+Ty5N352eXp4Pr40NwYXg6On5vECiYgjd9EOEkrvN8xjRIw2nNNUedG" +
                "Uxf7cS1MH1zaiL7fwWt2FCiMrpFyQKPTOJoVRcQT9WDCWruuzy4QX0ZLHOGl6FQgnFDt0tVP3eBdl9yM" +
                "n32cI8l2ZSqfgIciGMVFCUb7vCAuY/6HkkvyEJjec7wNfzp+4l29s7zGq5+B1T5KzH/BimxunHYK1OTo" +
                "IkgejfEEI38i+gL2oChJa0RBjBGWoF5jXsPr/unZmxvAxx/TTDLBxAnmxDJzhUUHtxpKgrLxh5KUFUQ4" +
                "tvk5iB5S0M/OZ2nADV8Pzl69vg32ELZc7DuaMLI59jnuaJqSCrc8l7UQ7OFa2Ofx0KIz4zB1Mg5feONs" +
                "GgV9GcM7nr5Iq81jnuCEIKfMI8yrNJWntybRfE9Lyq1ztrBK506GiKeUl4FJQnmv513mbPC1MLXTWonC" +
                "PytSLeJBuLyVutLYMQYbfnEVhzraKDbPukI9iwJofCbY/qJ8Ahv5n7wVLDFqsj9oP7G5K1jb9worFpR+" +
                "/6GDY9wKANAlFlZHhBMskBpcKdNjdW8lbNa4ohTB5k5bYpUhYw3LDFm72iHFpRbvDxlP9RAC474ktr7F" +
                "s9Y5/C0mdjPY+Xlmtomu+T1lLXkWuDMvyEZyYdP2rr8+kP3vseptZg8X8QFNXaDKEjWu8c+8rKYzWqMR" +
                "mIV1pcKHEHuGtvWaJstPN/l1pcl/qIW+LjIh0+xRzLvruKbIywxvoePvwhccHEtSHWOG2jhl+ytVVFw6" +
                "tWPWA3XACJtu7C+RhbzkvWAxxSQN7hApmbDYuMAgnRVzDPsVJUG+EPTIYHYY4nAIa7QMoG1dmhA4C7MJ" +
                "I1KiuaxjHsPrjngQ+EuY+SNLgccjAnE5vAXwQFJMMet0Vs8w7j4DacM/TZRZw/5XLZTKPeRdxtrwDzP5" +
                "JcMFi8aA9Zxe3kfFpIP1klHG6j5VC/htgi/CGs7atXx/8jz2cNQ58CEagVnF1Uz7VKmjJeqmazAq53VJ" +
                "LkDPUwQN84Bmk3YRqYgCEEZWMESXcnDei8iw281Qmp6KD/NPJvPKU4XSKzZYuyUMBzM1Ba2isM9CZZmb" +
                "KfaxDLModM4hDSeeTrF1/dzKx2onbOjAFE+8LIuZx3cjp1WxiMpEN2bXos1LIGrLmy9lZGRS+gYkiYrg" +
                "aiotnEX5ko3sHtmtgrKEt7nNc27QDVgkIk4Agn6j7V63RZ1jFhSrC+ZLYFSauCVL1oeZYClCgZaHJOVg" +
                "4EJLLnREnEMatzWzMDa7oM3FybLs5KVnOvFUpYkZ1KUFWvsbQHC+rXCf9AVXdBFAzjNEWoMfnfgNUTyy" +
                "oriTTAPG9KvGZKEqA1a7RIRlCUGbc24S9WRNhaaoTECTwEyM6soGAGDjW12FlMtDBICuIruXWFMKYJGd" +
                "m2WPUfdkb3BPGqWoJ1MnXGh3rKzC7jr9ZtYIWE0gU7OIV89IxVHt1tkaG4OGkTQVbt2VCVwY1cNcBzxp" +
                "7HbmEhqTyhJQdyBWswIMYCxyABFEGeo6qYkjTpOsIG+YqECbgQULTj+lGbKC80FPJHrC3gYNyNYHbgwN" +
                "v4SMY3okStpo0hxzQZkhzhufGGB0t0ucSrZrvFYjWPnmFUUxaT9ZJllP3jbEvnNY82KH9alKrCSjJ13G" +
                "0y5SSk8BPU9kT14UghE7Y8B+TBDPyX9Qet+ARL8wUxWWMJvNUIbeAP8qffyMh/DBA2ZzhXY3atb9nq89" +
                "DLuZZoSaCr2yTXqzs6IWGJDbYSIBKoq9mGPNLLrmcJsq5p50CUNOxz3BobRRvk0p8PZuVBZmlwKkQmwY" +
                "UkOjzHDdm460T2rwdIHSbMkmkt+HNECGnnCTTAH4aI0xYLaNFWEyUobmqxS3FXmO9Wgm3d3ed3yDQbY9" +
                "FkBP+pAajCkaLpa0hVrWGdOkxaQgHVPMFldXi2EEoMkxH2dfBxcBuWdUiN+uO6AJMpF+Zyb9FBwHz7rB" +
                "O/j1tBv8DL+e7Jh4zuDyZngd/nzcvvPu+Gnrzk/Hz8wd0aQ0Za3qhf9An6BV+evczHQ85rMTnHe1M2Nz" +
                "LDpWGIM3Th6gb2Dd0BNbQ0wNQwQoMfIxc3ScRRNZoiS6tIFKtIKmBMvQq7qkSl0p5KMMOkK20ksBSM3r" +
                "zlr2UvghnVDAQPWYou08xJjib8KFisMM4f+FHQVyPuECXaoZxZ5kV0iEOdhDTovAafCQyE4H814rNZPV" +
                "gdiCNOEu7te1ItD7CJQM0muLXCsqKb5PyyKfwcYhJOGQIQ/ZJMqudw4b3X+EJEePqOgNJHFe3ii1vJ6N" +
                "QDTQtGaG6z/Z8T11k6lxhTb6k64JgkR4uMA+l2AucR/7k3mRLHOwRuOwxFMq43RCddZsbnoEh2ZgSznO" +
                "IOmvsd+Odh2ZVJ8zNR+9saVHQliMNolhAdOM1firtQhOKnse5dRdHA7oBePw+SWc8tzDoEtmNAkIBfpL" +
                "2iCjIAdNSEKeK5XgPghwLRN7T3iPWU8c7CyuDsvnM/LlPo3W8dUwoqnWdTRWoV1AIdLkiZesQjAYCy6O" +
                "paoMclYSdrhtV6rd8WxCk6axHgpBQnxm85rC6nnirdxSUfGrNQdoqaocea6Jl2Rj1nnMeghtNFkYYGEB" +
                "ZLcnrYott8jlMAgJUmCkjJ+1BIy2thkwnWO2KecjF1FKfpbZ29eBxby2nLjyNL0MksBGstyqvicdDQTl" +
                "4hs31fpKbQ1yGCxA9sBgPiq4e40XN/g33w/lvuGTAe2780Aoyz/vHjiduM3LaAyGWrkwoudOkUlDXUDI" +
                "5hkaEhTaGQd761wSVw5AZQRt9+rW1HyJj/X+QzBOH1QS8jEzWxLGU0/0G5Vgj+yATAHqDzudPj85MQ8u" +
                "6L49TWA7hKYDrfQsY3d2jkTmExjpHC6v+ArwoYCqPGx10XGUKelwg3+b5vRA7Blxh6VCVHc9xO0tdS/W" +
                "aAEGzyyi2lOfvjl5WpRXJberyMjsdyf0mNszWZ6UYPRK/OnRTmdIw51gZyyf4gNXDMvVDjWGbdkTPxYl" +
                "OAEL/FeCL7RlszmJk7yYqop1rC9eJmwGTgK4W5UJiJN505YZXaDAEhBZBaRGUi3GyxaWp1tTG/MIK8df" +
                "vAVGKxD0bQ+cxsqr7KK4HbokVLHFJ59QWID3Yg67QjFe0Vza5a3oH7inZI6WLBAz2F9S9OtPhy/JE3R5" +
                "B8yCNqFfYGNo6I1C/cOkGIcr41nZXRHZYM+Eeeyk0YqQgiDiBCx2A8AuSRZBPH5hzv25VWnP+WxLstx5" +
                "ZYx/4BFi4wfweWdzoJYMNkPuqEhwSe0ZhKAhhZ7ATM9UVK5rvMOZ5Uik7OgoBovj6MjT3VJAXc8Tpha2" +
                "MkK/daRsK0thvTB6zIrseiBBnBZZom2lbaJ0XKYS1SFRYtKl1gjcuV9qXkhlQceYeTVgUXLHHvLlTlRa" +
                "wZWQe6W6p0QSFeWXqca1F+/7saTREs9uB39orDmz9RkoUUJx0/2uayqHg5arTR9ravwYvAZcq64LHxFy" +
                "nvUctkF3sk0AXFIi4nK/R4QNHC2pliO5KHDgxVa8VkdLUg50WJUZwVnHphpfOetsxiOuaXMwOcYTVGCs" +
                "YY1Ou02HDttjBM9MI9fr0FIyXU00lo5j4Zg9mZ+mNtSprrTItlFDtPnoLo1AYUckHYP+TQ4TMlSLjrRS" +
                "lTEvaEqJUydpT4kWbTOZveBHtLCxqJyLvEWfEhV5gX4hz0/rADvtgl0qEKfgs5IqJducNk00FJcijcg9" +
                "pqZ9vNPEw0ylE/NJp79isUxJZ94IjrdqaHfHOho59m1lwJ3/dswhs80gzTU+MaYJeQZ7nU7r3IE7lUjy" +
                "s3ocbvU03HL1zNsWFMrq/gNUXa8cEmMdgNIjk0u8sCKWqEmpuMwJzzUkBcwrBbnR0jMam0OpchTZjceC" +
                "3M5LRJWtuqYcql5q8Bm86irNKVdrkHKfiarc+ifLsJDR70ApkZLC/BgG/xrJoojs4qg0yiIiCXtzffqS" +
                "dNohbuV7DyCs8H+02LeZSoyt00PJHPivrvCxY0aymdt153KbzzuPpIWBBguaCjhAGeFbCIDgBg7/r8a2" +
                "q8YWeABp+tlqzDT/v6TGNmkx/2z1BoeRSqyMd9hutIAJxQb4u/XsR2ITPGR+beeMlsXacLKVeHJqxaYm" +
                "FsXK0TPdOsjVsefNvENX/rFYczRqS0QSt4VAo5m0M7CaB0lHZXHHqaOCNAYWdkacb4nyCZUH4GoDKTFE" +
                "ShN3Le22Qx3LzZr54yqR1olmrQB5WrNIIEWcP4tEAuYu2VzehtO7wVMTvda6a1OR1tGhRR5x7MadXiJd" +
                "g37hhuq01dPTUiRp6sqxUIbq6E3mo4UIwJBTTp43iu140EdnYxNppzmjVAACzQtxZHtpYjOUGNaxPuij" +
                "dRBt5oz2OUuLZQePkOx02vwqBFsTlhNviNx/2Zp9t9E42ejNe0kAeh0G5S+XNrB8YJFjVChtUII+Xfoh" +
                "ctfBe8sGv11DQhASVDZnzBgz2obpDHSyUlpm3vNik3NyApoOkmL+2JFDaWE+BCr1AvZ9LFguQCFaDqLb" +
                "g9T7tuCIBslVjDtWuaThSpXxi75MzEqGxonEl0KZwMen3iKTKERw9Q0yi0a1d2tWupjVuMuLRf675ApX" +
                "12dfts+uqx+zARFjC8tZkg3lrGlii02YkXskSuY4/AUMf4bRnDWv/TGTjucvSEIwDmDqColLbklJwBFt" +
                "swkX4EoJL92/RRAIxwF3USdzOEmtvJbMQ9tUeKSlOXxTqvVlMl6AzHHiZuMxqM8/1/T5h5Qu5LjQ5oNE" +
                "nzwZhGDMgS/yJzAspbDMWBSepZNrokw43tYd2TMUXLYW5X7ZFA7SPOiEoWDF7+LKNxOLD30M+4lU4Tn9" +
                "sJrC7pmEljQC2b1Pi1qDXakeUnzHmklgUbKGC0ZGSzCM+qenWHqwQ24klSr6cGwpkJexDdBXKdFk3VNY" +
                "eVXJ2xkoplrFU4HghCRN9mWw68HF8O0ACxuIsjkW3JBfaN//wJ6kKGBCXZu40Mcptmlz6mSohfnwSL26" +
                "GlyeYhGFKGs37PoRaaAuZzJpQZhzb8gFKiAyU2j8A3MInBKdZPtTLUpKhwSRZcBho1Oc0pVyKUGTWHTI" +
                "SA7nqrSn+UdUpIRWrm1bmOdfTHd+Wul0Hv3mn2D44ofByS2+Z/K3d5YfZtDJxzMK5lwbvoAT90dRdKDl" +
                "qMoOfASt2HNFcxOmkg8DTDg0bX0zOasZUT1H2xa5Uzb26Q9yRHcYhHPwSyNbeGopD5KR3RUAjHdAYuQj" +
                "JJsyhSh+uBlePsYss8Qt3vUvzgMGAX6+FWjQxHZFeO+ZQG1ueNM4+YYDm62nFwzI1kjzNbNPK8sWn2bp" +
                "nToKvvrHLjJ692j3BE2i0xe73WC3LIoK7kyran70+DGe18yA6dXuf38lRHLOPi84bJKb9CjNolhF9NIl" +
                "xwc+KrcLnVIuUrpTSl5DOs5g6XJ1Y6+5tzZEN6bXTCIfzaszTl+wkNj3rqEikKE54kBiVgOvUPVxAEpT" +
                "QT1GpIRgug4I0lFgucA3kRFws82Io2++/+65NMGNmmu4oOEq2rtmtJu/nQcwf1phzsDOV3Pwm1+y16aJ" +
                "gKfhgt3FRB9+K7cwT3MUfPP88BlfQ4cSm6RoLZs2YCoswINu30fjBgkyo5jEkzyeFUmdYQMqKamK+a6V" +
                "cRT3L38Ci/bZtWFRDhuqrmzYbkO3u370EHyN1v7XQfwr/JNgqV6H6lmOjmGa1Pj9E3yD28hePsXL2F4+" +
                "w8vEXh5+cO9We/6B7m05YNJ6Y48LK/ixWNrmV17Uw8Zdz76w9pGxPVZaxtM0MyUH2LAdInQZQvPuWAQD" +
                "rf4cBdNSjY+/ktWxSO/SXlnoXlFOHlfjr/5Sjf/8OPoLiGJ8B4AoTnijFEUDkiKuZ3Z2x1LP728EKwEy" +
                "EcMmugH7QLZs3xzqxEZ8t3ProtY2ELWNmMLaegzRbubMJXAA7A/7ej+uAOPyDOupUhsOB/pvHE3S+zTB" +
                "cAE+T13/zRUijwINKwXPs+vljO38rrxYt/HewcaAbTIG+NBixcdD1755oF1OQbF2mHqVjbv0uoxMF66o" +
                "wy8XY6Zw0ZgNPjtO9RpFYKvkmkpK2LpL9yafeZRSwRXzei9/CqYh1xqbTCbutmwD2Ld0bsDe1Ra6g0+r" +
                "43sRCFPqH1m/mskBNCiFzoh4Rrd5swgZpEAgGcyjAqYKelAM5xk5eA18qbmk24WXcnRJXqZMSEmJG8es" +
                "8qB/eeobop7cCYiwIQ6YhF95ZqTgd1hVJI6YLmgWJLjZAJcmvpMqWa7ZSywV9noLiHtlVZ1H3nuF1cdC" +
                "hKYsy71irBHhs+8lMvVZW6KDqr1+AxVYEPZpKqRsbBvHpl0pWDOSk7K24LzcwrjBSy45WxsCMmrA6ABu" +
                "lthwVVFev3rRN0++9DcM7ID2bYOl/Wti/xrZv6KtF3xSER0X8TVrqlbCxu8/BBuqo269UkHSqf5b/1wM" +
                "xw2BtvZOR7qICPDFj6AF6e2w8vDLeeIfGZzCmIenVOmINbxgnHGsF7YdqkqH9qVS7Xzh6vnVgtqtjfpJ" +
                "HprtpDXBKzmmY6BKJkIA4luG1xFgqNom0/5pZlFNHdVo4dEZsAik6x4ldDF99biI43oO++8+biNUbkt3" +
                "ojxeGlbs9UbV4x5aCmmm9rkmjQHhGCdZhAdqnBlamJOQ0t3/DgH6OPSCABRRLM6e7dsD5ngcUqGJyN3y" +
                "InGvb+aDmzWnNh8ZMsAFTEHL/mrdJe7KL3rk2gZ640pvykmKiKvcF2VaGcHRWHf+He7usF629sJ9/xM2" +
                "n/48DFax1Jpy6LXmDyXI12/kszdfBuuNmHRa36yRE178jC8wbnl2+SowP+CUwr9eycQUpHZpD9jPyyLm" +
                "6JB4Wo3PdAjM/snt2dtB4MF82oSJph6HPUC4R4qC5J8D+Op6MLi4uh2cWsDPmoBLFasU36gYYcAlVvY7" +
                "HEE0xlcUpVQVQnU09kRPJ/jID0YdUBKRC/whGHMmNCFP0Z3D2LtV5SxF1U61TPvmgN2bk5PB4NRD+bCJ" +
                "Mn4Jxb4IUtcxcgFX93ItIzYN038xvHZ8wWGerxlmVJTrTqCsHymp1SdZY4t3x1GaYTZwA3rXAwzKOvyO" +
                "g29W0SsVbqMbJMDGF1vi0v00jubUduUPVueYPExz8zpoOaGziQCRPLtSjoNvtyB5VvTQa8BF6ITPTp7l" +
                "8En//Nyt5OPgj5+LoFQ4r8Pwc7grMd/mbDWRzsdpaYv5Kl8LECYqaRDhi8l3/wYiPo/NKBSN5ccDYB30" +
                "Bpk4H97c+qCOg+8JYN9+vEi+6oSx3wTjzDMM9UtFo2EBQmme5Ea+jT5j7VGuqLhX7OIs0lKt+3QS7MLk" +
                "llpfqJaTtq4qLJIoP9tm3kZGJ4nUqJ5MkI3SqFIP1fa+UyRbb6fDqe4Bvh/pBA0OflVSGMPf8NQr2PdK" +
                "C+SzEPzUVRdwfZrXaIIvxHLFF+4Zl8ybj/w0P33U+vCR9xmh5ueDvjyj2qyRE3p06p8iLvf0xTcQKnwT" +
                "BLp63ID2ppsbTJryjZf9s/M314Pj7/EH+/Ltq/P+5SWolhCfD06PD2yHs8u3/fOz0/BieHs2vAyx4fHB" +
                "M/PUuxtKyz5sAuGLd+Hg8u3Z9fDyYnB5G5687l++GhwfHJp+J8PL2+vhuR3uuXnw5rL/4nwQ3g7D/t/e" +
                "nF0PQjm+DmD7xwffmGa3ZxcwyvDN7fHBt5YGYzocH/xRohfudK+tCLYfJWPp0pZTt/3r2xD+vR0AJeHJ" +
                "EFTtDdAGrHiyrs3bs+E5/L4Jr/q3r6H55c3tdf/s8vYGOjx1jH017J+34T1rPPwYoMNGS++Z6YUz9dyN" +
                "Zibr1fXwzVV42b8Anj/9ZuVpCxi0+bbd5nr4YiikwuM/th/DNvRXA/+79kPOz5rH30sMhmu6m0x/eQ1t" +
                "QkDj8ubl8PoiNNJ58OypkxRhHAjR4OSvKKMgI2+hIQoKtLTc9FDGf+mhZaCI0dnly6F9+Jwx80Sjid3l" +
                "MDz7a3gzPH9zSxN3+HR7J+C8LyB9qpCKy3Vdq9XSq0bdv9ezffjMh7GlzyZtQK1ZvW6OqaMpKhGxtZkk" +
                "87oQ9/0IqnRf82kk84Ehveqnr/tsjlSmHwCrTH2lg7XHxemmLr99EGDfVO5zC3dIwQNBJYzge2R1omh7" +
                "pzJy73s8+rH7jNLj5seTJIEwldeGUSpF3lIrtyw072NOXWt42k6cfzHNsNYrkNyC/xEoAtbbUC2+YTZ/" +
                "ny9yfQwZMyftaUU7zoiYn+Pcw7eEFuARALz9zyuKt3UKUvcTuVpsl2gxotl8vx/jsqGs3vue1soQeePD" +
                "Wv/aOE0p2/onuTbEacynhP+pSI39DvG2P0BssXafyaTTV/8DXQzCrmB5AAA=";
                
    }
}
