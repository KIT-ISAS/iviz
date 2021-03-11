/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceAction")]
    public sealed class MoveGroupSequenceAction : IDeserializable<MoveGroupSequenceAction>,
		IAction<MoveGroupSequenceActionGoal, MoveGroupSequenceActionFeedback, MoveGroupSequenceActionResult>
    {
        [DataMember (Name = "action_goal")] public MoveGroupSequenceActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public MoveGroupSequenceActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public MoveGroupSequenceActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceAction()
        {
            ActionGoal = new MoveGroupSequenceActionGoal();
            ActionResult = new MoveGroupSequenceActionResult();
            ActionFeedback = new MoveGroupSequenceActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceAction(MoveGroupSequenceActionGoal ActionGoal, MoveGroupSequenceActionResult ActionResult, MoveGroupSequenceActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceAction(ref Buffer b)
        {
            ActionGoal = new MoveGroupSequenceActionGoal(ref b);
            ActionResult = new MoveGroupSequenceActionResult(ref b);
            ActionFeedback = new MoveGroupSequenceActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceAction(ref b);
        }
        
        MoveGroupSequenceAction IDeserializable<MoveGroupSequenceAction>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceAction(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "146b2ccf95324a792cf72761e640ab31";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXPbRpL+zir/B1RcdZYuMu3YTjbRrrZKlmRHu3rxSrKTbCrFgkhQwpokGACUrFzd" +
                "f7/n6Z6eGYCg7X0xfVd7uau1CMz09PT09PsMjoub7GVZLObn2a+LbDbMdod1XsxeFukkSeXPwRX+7h13" +
                "tzvLqsWktpal/FrV9kWWjS7T4VtrPXa/7/V2/sX/3esdn7/cTqbAI68H0+qqevSeed7rfZ+lo6xMruWf" +
                "nqI3yS+1J5sc7iekwiAfLU9OSMW3n2weVT1SVBTPe737yXmdzkZpOUqmWZ2O0jpNxgUmkF9dZ+XDSXaT" +
                "TdArnc6zUSJv67t5VvXR8eI6rxL8/1U2y8p0MrlLFhUa1UUyLKbTxSwfpnWW1Pk0a/RHz3yWpMk8Let8" +
                "uJikJdoX5Sifsfm4TKcZoeP/K0eW5HB/G21mVTZc1DkQugOEYZmlVT67wsukt8hn9dMn7NC7f3FbPMTP" +
                "7ArL4AdP6uu0JrLZuzlYi3im1TbG+E+dXB+wQZ0Mo4yqZEOeDfCz2kwwCFDI5sXwOtkA5q/u6utiBoBZ" +
                "cpOWeXo5yQh4CAoA6gN2erAZQSba28ksnRUGXiGGMT4G7MzD5ZweXmPNJpx9tbgCAdFwXhY3+QhNL+8E" +
                "yHCSZ7M6Ae+VaXnXYy8dsnf/BWmMRuglK4J/06oqhjkWYJTc5vV1r6pLQpfVIKt+Mobs3CFkywvMQZeu" +
                "ui4WkxF+FCWxVpZKsJy31znWRObBTZPcplVSkmcqzIM8dChLLlwJqqQzNxrWubwBd9xeZ7MkrxPMNavI" +
                "t2CNbDqHDJpM0JswK2Wc2wxDe9DJZYYtAhSSYVbWKRaPGMUkdvjnI1sWUBjoYWWKQOrEpBYwG6GHijxs" +
                "w6pKrzJZh6SaZ8N8nA91gg6Dqu+gc49oAyA1XVQ1MEuw8dCqb0uIVr3PIxZVIELKkeb28EynYFPp9Xqv" +
                "JulsBkxP52wHRna/B4U+WBPyHUiSDY9yIFuM0Vg4x5Az/HWV0gS7dTYalOkoX1QiP7N0eC0sqFsaazun" +
                "ACMo/hY4YEFwmnuEd7c5fmEFs3cUdCIBwNHFDFzlEOu3qHlYZ9OffwEPZ9NqXavcHt82K6fkyUISyMYk" +
                "amEr+qnHJPRtq+xqSpnlaGST3iIV8jF3quzDNKmKSV5DqrlVMaqQkyL+kl1Y6NKArPVthu3udYrQbEsG" +
                "cms7xf4E9atpUeDpCHKDWGKT56WqKRtN56tw3ZI7lNkUq2aDyaywueXdLHtXi2bEgy3KHkzlMXZ7Cp6f" +
                "OTQxYL83nhRp/c2zBk+tc3EjMuraytKUNzmoBgVGgacrOcrG+SwX2nENU7+ikHKBrgDhud2RoljU8wUY" +
                "ovbikWv1KqW2qbNSdxAb3hbl22qeYmARw/HOcVqBLWANVADS+8FaR5A8hMHcP+yp1QPzgwq0pihezKEJ" +
                "YdYkh2Mv1P9WwIyobCBwBk0QjFNmFLxAZ15UMnssPzAjxtgBwgaLshRGnmXKYtjcobECBAgyW1bDViyz" +
                "3llxWdTnggswKuuB4GWKBJ2rnPJA1IC8CkSaFiPYaFRf3IF42k8OIH2SbJK57USrAQ3TsgSLy6qJQnI6" +
                "5YpMLcPIA7Ls8DqH4Uccse9kRkC8LlMhiK51ZL4pCIAH7mmdV1RHvb3QAxJKTN4ICCd2Ujjig5Tp7A6T" +
                "xBtIxAKrIiudQrRSufLvshgthtRyQQ6rvLzJi4nYmULlGM8N3X7z+cSpdwpbGQSLMivq5G/UmNDK+mwz" +
                "RlkGbyNMEsUDOMEOpU1GwtO/ZUMYKRBLBKykuOtd+Ocx/NC6a5QZDTQnU2LNQyYraNAIo0JqSEPuTzMD" +
                "tsiaXONUbIlmX1MwtIjMPnDvaOR1DH5Fhc4fbjMAWGR1GVz8WczhA5AOBld6DgjLw11ML9GYkGmWBxCi" +
                "GSABSd0pBAOMt+T8uijB5CBjMVk4MWLolzDGSzHx1OwH4IG3GtIagn0eSDlN3+XTxTRJp8XC6RbaaR2U" +
                "JbNMJsWt+jC2mcQ4d5b7ppfNrmEYVixAyhUY7GI5w9QsnCRTQ0Y8JMiOxVC4JUJNaJvAr4FdWJNX1R5M" +
                "h0NsYVIVDNJPkl2H3E06gZSV7QbUNh5vffUL3l6sAnjXAS4yG90GKyl/nCgh5Cm5mjvHORQ5CEQjBjwL" +
                "Dwzg3AQxMjgRGoQCkB0h2ykkCTQviSuM4DKdXQHjjXzKhUtn9eSOYjGvxBccThY0kC8zEccQiKD+4/7j" +
                "TVXOOo7wuL5ymkf4W0jBNf2q/1hgQbwroTfyftbfWkURAGy+iYmzGVQwGg2s06DSpR0oRo02cfd2u/Wo" +
                "7g7d55W3eRReeadCS5At6ETKhPEC0p8bDW9FpGHriz+KLYgtswEzt3i3SY4xGWB809w6xCsOEDjQcPPu" +
                "1OfyJiB3x7S4zKHZqE/EjnLKSgHD8rvNJpM+t9a+KC5lCvFg0bjMxtCetOdMGwJFTLTEhq5awRgTCDnk" +
                "BRZeG5mgQ7ctNeYdlxurKR6ik0VcwovvXWUFqAbRLaR/I3L8KQEPFGhb9vzrhwLPuaE+GXd1jux5CjIY" +
                "2MO8IDfdyEsyxhgedCJ82E+81S8LT0NXZup7Uk/lJQWKsz9Kcau3aBeOCkgE6GjAmKZvaSfReaIOhzqH" +
                "RKP+nFUTFWZ4jC4bWf8KO14YTFqp9U5HngEq+NBlfgVhJD0x0NR3ThM3O0ic8RNVM4KzDoaloXde1E46" +
                "UODdFQtYDpgD/ihdXEw0mOEl3ltdFFvkfgeiSdFXInhsb2Jb1mBTMLrJlXf+rzv/129rkSXBFl0tQvJZ" +
                "IGF6CaHf5OCaywgJrMZbsJPpxVVmTNAdgKR8W/W4vEWpo/+JL9USlnbBEpY3LiJDZ+06vfGKKkv2T1+o" +
                "CepVmxjWMehjtkW7aAjpPhgV40FrsN26hhkMKMNiMskrzrO4pMkGPZbaO6x5hTWVWdA28jTY7Fn/Pet+" +
                "Kr1hC1tv7GD3auAgc9wXkxRBG7iCiJ6Sg8HPzrmgChyClYO5JPoOu4kxKUhK7qfxeEm0CIbqoUj/SDJP" +
                "iwq6OPF6WUO/FlsUn8GmelmMaJpsGD5oSBua0d5JBiegozEGwoKnjre2tyGxsu3tyNNxcTRxviRW5eIp" +
                "dcRym73LoqBXMuDkPp2462ZBvwGo4fwuEA68LiaI5drWhwUzLHMKAI1bYnYyd6doIV3gGsv2KbHqIJHu" +
                "AUrJEOzUTgwdObNko8yofPm8hA+UV9xxQ6hgDCzakNYZw9iINsc7zVSNQUlHYvJsboWmwRZqN31USeNH" +
                "iH1zh4Yu2RjLVftQunpW3oJ0AE6m7H0CI4oTE0dU39AQkNAkGW4E+a+bFNYlRYI4CkoIVdd+H1edMV8b" +
                "z/nCLkALDkMIvE9TeamN5h0YTbNlFPmhO8m6cghZQYmQY0ydRlsGVogKijRLg/wR7xqeJUewCKBYNU0K" +
                "CzKiHGOPm1YT9Zx0cu0xd3KYeQ/95AeqNmo51TpOisosZgUAuvVpxfIJa7olGmsIlwablbMOy6lRAfpM" +
                "d44bST2dja5tNHcXDFHKGZ2q/DeIe0yZsUuBE+0aifeI5afhb88DIQ4eiCOupyFNs4nmAQ1SWUEox7ZJ" +
                "p84mhKrwj1OT+GkbIXpkDB89UgqsR6Ys6x7KljMzhlSTYuVVDJCB3PoKOTyXjbIrmFjCe7S1RgWWlt4Y" +
                "hEpxa0IbVFkM6wW9sXESBlReVpsM1IdNz3Acoy+2RZmXq+5giYjNqkFrsKDYSN5u0j5XWR1EAN1LH7d5" +
                "C7kkcioZXsNW6CcvuBveIS8yYSxXrH8oDCcvGNudJa/P9l+IWHtKNb4BAxke4l16y4SXRg1hDOtLMrEE" +
                "yUMiL8ZOCYl/yhxQtC83deO9mJVsYdCwp5ERktwOEzKYcAOH/5dk65Vkt/Tprj9aklnz/0uSbJUgUzOU" +
                "3auW83dhLIxWnp2XGt1CNbEB/229+0HIhJdKr3W5jh7vLudRdLuXLD5xclssFQVULf+y1zNvOPIFe39Z" +
                "oEM5owwwj21d8wxDd3rJkO7IM5ukb8yFv34NiJMUH3IE/V+3a1tGcpSfmYnfKhiSzSldlsVbptdn4p1X" +
                "dJHoJlAgIxooeQMJ+fT9Mrom4bdrt64J6vboWjssiC5SmN8WlDujZ5RNnCOty4+bpQALP9UzWE+QcIVP" +
                "ahnc1mMfb/dunUt2j/N35sBo6ArUogdsoX/+bZF/IWUja4io6IJJrOsUdURCKQa6QjqrhUXvvlrosc/N" +
                "ZjLgfYRiXE2HrBgnKyCRGlGfuo+Ij8aOYQwwruM87ftd8MzvVrfETyNKPHAA5raahFKgFvVzLp9ENkLe" +
                "0o9nUQTGKqKgaV0sIJKRFLzz0e2HHjFFQ6KsKHsa3UWpyKhDUCICbKCxFZp8IampWIm7BDuMGbV2MRZW" +
                "Q0xHn5ZXhDV0NyUMPxWAdlE3SDdmw2FQayhwOIEBzvgbskCyFxwUF9S3MWbZkOoYKTOOViKGLjVdLgTs" +
                "BubysfbHBXSizFlw1kOSDcQgdgM3hC3KbYaCNm/AtBYDsapx8nZW3Hp/wbVfz7bs2I67zjCQ8OFIqOOD" +
                "PWbly7bpjnGD6d1MHQ03hIEEFhaQpTqHiFFFJULaz5YaJX7KFwxvXKZatyIE8htI/0Wd5rC40iIAnYzO" +
                "4YIQCCbkTS2K5oSu5LmW7TvbttoNGSQVDtwsXSHzONhnBDhHdcjIDzNk3GQKBG9ALBE6iqe0emWv6DKG" +
                "Zu1AbdV4T9bSioXjrLpuQuUTtJ3qi044fBdAPOf+sDws42sooXAWQpzVTS5dhFWamdeiBkUoVcGKjUay" +
                "FhCwHGIzxo11HTIRGWnFJPkuYLc7ovcXMYZS3edvJJ4oAfGoEXgUSflFBcs4ewebgegjsqcqVWROv3d5" +
                "B7Nud39/5zGHORO52hhpXBZ0NGGGz27ysphJJQODRhASyO6hOqFEwaRuBQkH19jPCiHKbY42daSzg+PT" +
                "Nwc7X8mc5nOKKvq0xs3OC3ayVZB2DsOH5mp5Cu1k88QqhEm+enVwsr/zxMnhMGb3cDIKyhOyW8f5bqkl" +
                "YYKKBjhabt3MsbG6v0k2rtVpodMMgYbMOWkF0prECAIV4UpQcqQoCm2eEsFTzeJrWgQw8ZPGqDV0Sf5P" +
                "aVJ/WKxAPP7d/yWnz/90sHfBWuG/v7P7j/TZe3/6Q+SmRAXHovacLIMkY/SCTg1sg6qVeK2LK42oe39S" +
                "Q7tgFQbQG6bF28zHa+MRtuWJ9g+xVUnICcdAaM2S0aXJe0AxgKPLGBWnZiWg8qfz05NHrMdwUZafdo+P" +
                "GFhiiVqy67kYktbvgShTR1ltVAmRJFXtplNQpiS2A6OyS6suW0m8/KJ4C6vlbbadfPFfD0jhB9sP9mjf" +
                "7D9/sJU8KFGthyfXdT3ffvQIrkg6AbXrB//9hU6RVRJSYCchnpkTjrp6zsbh4kRUoP2Y1w/QiRVv2Ahv" +
                "s8wVkI8n2K1IV8PdsTLADoZlnkOJaJnH/efKGwKEs+LWdyNrcITM5arGXKhMytMZOnOTlTC/gNlOPAHk" +
                "GUmAZ20SbH/93bfPtAW1ryZa0W4Z4wdupPO/HCGtASuBuQ2/To2Bz3+dfG8tFLYMlTy4vaqefqNPmEza" +
                "Tr5+9vSJ/ETrkg1gRBe3rgU0PyrzRq3HNFI4ERvA8mL6FsUpiwnfS3K1LuYPjKHB2p8uhrvKZLgXShIk" +
                "o1/NyWxbyfAONraYbuC4LHFRKHN3wBmWuQFnWfQJds6lGQIARrFPxS6bUS3ox1v4PwQFeNLh2+T56Y9Q" +
                "Zvr3+avvD84OoGD0595PR4cn+wdnEOjuwenJwc4z2/AmokTXECfXSm01kwpIfMC/cBnb0DREzkMLX+uA" +
                "/D7RjztEzbY1ICi1bkw0WhUi25Jc70xYPQh9HqiKk6yocw4xcUFV3Ygft5KfNMb71xhnElk8p2x2BZPR" +
                "anNbYoj+k58fiN4PtB38CLsk/PrJ05q//kpdHqGk9HdYSXyQy07JiX9d3gsCVPGEUKM0drWDVjvs/B3l" +
                "IMND4Q7OdvcPX5/TTorGtEUWmFxgPZWjVFHWkVCEVF+YkSgRejfUXxOUprB4y5ddNOAOvj84fPn9RbJB" +
                "2O7HZpiTJnYjioc5XTf8LNsLyQb3wqaOR1Fn4+js3Dj6Ixpn1Sgsx2jUXZuL0j0mtLZGBewVS5K9td/e" +
                "k8wGuIpvrYpF4iXwkNCU/el1ao3Ulst9fOmIapu0RUzPUq3J0yoNO3WpcSAMGq4pEkZnQL3Qcik/Rau0" +
                "HQqTBaORoO81BU2CRxFPROS14sWn5qJQbdRufXMEMj7Y14hQxVl0hEYsNRZm/IHQ7Dp0ER1Nr4EibOld" +
                "UlLA2dZdWOYIRMKa+H0kal0FqRTPSa2rL9DBNJkHg+GDqu0eB7lwACTp4GBxgCiUZz3MFYMV6CoHBZsO" +
                "uks6VjutjVo2kS6q2cxg9Hm89Ezhz08V1ezdQIKDa0JYfPXuBLEmULHr1OkPQQEfOUjfJV8yLPhlMvwN" +
                "/zNKdhJxs9Nkewecno1/fvwLI43+51f8OfQ/n/DnyP98+ovPRfz87Bd59ulo8IHoXvuAbWfarNXHOE7L" +
                "/D8b6l7gSE1MdFpABUwod3HF1H5T/rwV1YbjR6Mu/BeuVdFsrWUNv/iIejSW6jc9YKZnJMU49RGTZm25" +
                "z4uyNgZZjaqV4BR50ZpmH3PvdVRiVMulGCwRCw8b01ou0hgtLDIBg2DA8BBr9cq1hWj9GY33lVVPVpwW" +
                "jI54GM3jgyQWwvGnZ9wZG8mp28kIH/CXmnrLGGvhi7J8QNJviMbZklduKRrtbH2aTU+DQm60jhR1s8Mb" +
                "BHfVMW60v/GPG83XsmYtmnDd/K8OPe/zWZcavpD4t6VNxDszkgdZY4pQXru+dniKtvfPfgjkchCOQ6J6" +
                "mA0uwf63W2H4L6N3CCbdZL94Y6Jdx9Ru2fFcoEug0yUxwnGUkNMJi5FsIF5UsIYOkVUsLBxSKwfV6IdW" +
                "jsYcnOwhu6OWw29ZWbgzjLALqlBJGo7KKBJrWfFlBl+9Wd0xkoYZ4FckzHa5UkPdXa10lqRhm6BiVjGu" +
                "15UP1ZjReMwkIU8W+dSj1DhsBpmdlggLOt1QRO1uWWkYhaXRYcUhAQWBYD65VYc0BNyJjpWYJz3LS7zR" +
                "lqHRQE/7/a9jsPXIlCZZohBQajTFmj7dV+q4RHC1Mh+1bDDbwrCgWOCyPi5laLeYbX5s+sotssucxTWz" +
                "La+XoUKYAR+d7zoMCSa9LsGgbbmMl89WeIOCaZaRT7L7IJc/UnSFFZcD3stTbObSVk/KDf3BCUWJt7Xw" +
                "SqcO/bsEUrNk6MNCyWV1Gr1c5CKSV2FxhLdC4VGLbCv8839WAFoVoARLHorblWRlScFhOizKfIYTl5dy" +
                "EjQbvBuw48A3Xm5x98EWv7Vb/HtKsy6jzYqBoimHI3xS3IlHNImCaaf1waO8GmqGUhXP5lJlib/bRzaA" +
                "tJcSxkYIL/WApcoFtpgmzxCEY2ZVU0rgxyumjeyiCJZZE/CxQ05ETMCPoxEUfGI0XQhPhk3h81fFpRZO" +
                "qyQO3YkGoZ9g3bejiJHRRwCcnF4AuFaKTYEBz/1Fx0wxW2GRKrocwjD3Nc5GOh5PLxUsAsYGNTIL7PhP" +
                "qK4lLW5yZI1DNFmpAr5eNotETm9I7RVogHTKnauH3bTj4cL7FZJMyXxRitDs+53fCL3KMooeCxclGI/Q" +
                "WSEZscODoapWibvWIpLqMcDfm69px5JFx1jRS2NoHgRDnfkMotQdFvUrpLrIqGRHRptXF3gptqWHNjT3" +
                "1qFAzt0dVt6qqkRqvLA6BSW3cWZd3Kalq52wNfUoK9OnbRaLOEtrveZYHHCP1MdosGfKexIkcdGqqpdT" +
                "j9rmmTaQYwF6VGcm8kzsDMUoGtjd3UC/JZnfgUa5RAUEET1d7ZY1naDsmdybPBW+RsrAzt8T4YEM2lhP" +
                "f064uROVdwOLWCJVFwiWkhswnBNt6S8ACMrfkV1Eg9Z09TvOOPh2PqOsJ09ZvF03FokyCzQOB1ONGAJM" +
                "VJfKQwQ+0VuOwJWLTAt0ouqb5T3HrIGmfqGEJO0nZeUV72pazW7uCImx2wEz+UytX10HfqI1sbTjlN9a" +
                "LGZ7gtUzSTXVe64us2HKQ6pWMbZsPMgoneVXTsYove2WEsCM9hLbimRykN6ClXjuUEt8UuFWobsu6pB6" +
                "oAt1Rz7W7sBYRuZUAitUrXJhgDMpNWUj46lpER2Vcyd0GbiWV04SO3kJkQuC2Myi0WX2JqA9Y9ih53Gn" +
                "ADCm1i0kLnm76t2UhBltAWfd26lU35DYfMOTe8IxCtEqUh87jYvSEsVH7WCQnmWpc3Fos2rTIDJRg9Q5" +
                "i0xM2bmRu8G/yh890RFi6MCLJVQqQgE7Ehb+VKLMmHVIbrJODUYLsyQH3E1DXoukDqYT4Cg+knLyrMRj" +
                "Vp4+3hL89ED2Y1/lWy+tf6SaR627HdBuIO1Ucsl1Ta6XaMEKHg4myVPgRauH7PgJPaDmFBXc/Q49b8ph" +
                "iYeMuWiUuhqRYobaWIXMXHVLucS2gFNsynYR03Eu9K78DR+iIz3VnNHRIo/UjdkVHA1SSfeYVjG6sbAt" +
                "fBV1a8OYhWIhDm/8/IgExROk8vHPV1tITTNd4XLgByfnp2dIubcehIy8e/Cjr39wAlPWyY/9b2Tfr1Ql" +
                "mtDlAxPldnVG+6SN8qNdndlyNQWAqKO1uCidtyeZj6IHvsBXjVuZCh8sjw4lhhxI62aqtYelW/cd3gtF" +
                "N+NxA3nbJ80K1GTDHOhNf3XiubzwdxFJOz0ur0WnwuNjuVggXKMjVovL4uqRSNSWL0rmMWbxFYR6Q1Pr" +
                "GkPKPu83uSuXXCdu9UQP7bPrgCUxfwceUkNgM/4P9HNQWafeuGVGzDhXHZVsyMkP3fsVPE86QXCdqiyb" +
                "OhElRbozMZzi4k3CvEkh4+W2RVZyOnMgqjvWyXC8gY4XT8cLWw0e37xnMmEqqhhXTEZzP6ZOwq1ZdrPV" +
                "7230SNJL8S/8H0j35fuRQgWSUB3dRQCM7hCSyYdSCDgb51c8LaEGfTTVxn1aGJWrJnpjHLcSNe8WMqaJ" +
                "6NCUlm+sIYe0/mzydpscy//bma7AhWLUWw2O5thcJ4yidxVzmWfR+IhJsqyJPCF1aaWcUtbKarL0LMvk" +
                "rgCA9dTrP1at3j0zKHM3kyZ9SZObPO0iqBGhoUqrdJwN/GZBYoz3oPr5CXKwyQvNr0q2T1xAZEA5Ed/R" +
                "qs395YCuoND8Pnchp92pJssWtii2MtOn3u6SbZnNSGy9mo1W/GImbJyKHazbQGr4fRRwmUv1vTC8sU7i" +
                "2EpfLd3Q1r6ZDe5dypnNvAnVBbPzWjY3wgiq+2690lxEMPWS3Z7XlNrNbC0pCwM7ui0nvv9RHjfvf4yv" +
                "5ItvamteCsNUkBtHYUgjC7xGzqk7T8utN1rMJ3bVTD1ONrq8vJBrkhzVqmPJzs6ATpUDggO9PDo6qdy7" +
                "L9O2nR+OBOlZ/J674M4fhzrWI/p21164Lse1534GPAkKzDm92VXVO8KvV/oDmEjk2b1rtOclbZm25pV9" +
                "mbWV5437f1wN8tbyRUCotFB3HGsD02Kaok6wbExrLi6rVPnSfS0m4kKFU09K4KnbgmrONO49mfZ7evIB" +
                "RClKJuC1eFtBWfq5MWTTKvihKPWW1MloTbcLree6nvfzYMcx5fgCC/HZl6xeNXD7/s7z+8b/Sy2R4Udx" +
                "emjYzumEm5Xs7nGCQas/pMk17O6dL1yt/m3+Nu+XRdUvyqtH9fiLP9bjPzxK/whuHr4FILlc4RxVh/Qx" +
                "R8UQDpZFZvTmPCnT8cbMUlrICYMmuonyjA+3WWkrG+lTXllqV33YX2s6oNwpAczct+oaEAHhK1+ApKaF" +
                "SARfDiZNXDmY602uv8lHLFjk2zx0XimQ4HgjO8ai/upuqtlbd3NfsyoqHq09hQO+8xhpfVVHLrW9jUXy" +
                "YdmzyRhDEsfKHRdomSBKDDVE/LVDgUL9yLJYnqfZ4r8uMhAkGGO5qHKl8MYMTvlMAgbm69OW1vNHjoIr" +
                "MQ9Gqs9LLI8eWXwWosM4ji91KkBCbhwTNJYuLxXGkJCTmHNQfdcJOshB8CcSsmpgK81dDbtS0SUX3A38" +
                "gpIzmvTI+yzZPdmPjr8FRnMABjELUAIuvXIr/1m2kfAgd1Hr8rawFNB0w7fOx1IjcGTTsJ9rwTzS3sBX" +
                "zB5zg1fdMGCqP9wOGd8Q4AupnA2wtlmIVfHRc3B3xr5/DmqbrKd2IRgdJn3DgT/ORoP1EEy6GRmjR9vl" +
                "E+X+hl639aWN+waNgD97+XzXvfj0X73xI95TotIp8X9d+b8u/V/pZ/AixGQj1ZcMzvbFExK+6rhD8iKy" +
                "SEWWxvfQBDcgwOdRv57r4RhAf/wA8ScRP/fyEx78fc/oGrp8ui8WNf1CGGN6ZwTUjYQ10KHMsu6qmDhl" +
                "jFF4iqPLy3HXDqpd1JGRcyF277Xq2QcHkEXPXTP4PHT7x+kl/oFUnzEIzg+baNcNOdjGEzCPiuFwMYfy" +
                "3aQWEYdOniA+fmfU2Ohf1o/6NBLyiV3gqYDkEPUEHlBseWoFFF0t7d6UImfMl2rMgj7/dNOXfTB/ySvi" +
                "XbcZvF1fOK5pVr05nskYnQbC9DkE7W8+u6ld9ZJevQNOjpr1JRglh3RpRNyWyKK5thWDGd9SvXPPfM4P" +
                "jukH0z7ik2MMK6BggxEFlFwsQXMfXlPjZ50felK87vVan0VzeRzFVn/wCoXDk5eJ/YcMEf43unjuGjx9" +
                "5wtjUDfBe2WC69X47pODubt3cfjmwINk8qkJk2agHskG6+vt9B8F+NXZwcHxq4uDfQ/4SRMwYnIZAsXg" +
                "LYYHhojj2IdT0jFPbjJwrbG5KGIcEF3+j6ejyaekgn5czBK9PHFaRVG/jYusROIZSEisadOyaK/39g4O" +
                "9iOUnzZR5qe1fGE7yoJIBe79u05CrBpm9/npWaALh3nWMcylfNxhKdzZPdJokX2QND5MMUagn0HZFeid" +
                "HfCSiIDfTvL1MnplRjW7ggP8vQctdtn6MI5WhVHHgy0QdKBslOyE3AGkH1RYMQHHeX6n7CTfrIHzPOvR" +
                "peAmDMznF89TeG/36Cjs5J3kdx+LoLsrugvDj6Guu4uiuVpNpBGy51UnajH5ZdBCMh5NGjUmEbPJt/+C" +
                "SXwcmckUje2nA/BC6RU8cXR6fhGD2km+E4C7/mt47kuBjFSOeAMGgDgXPPUkIJRmgQbpdvkRe08quKBy" +
                "3AedkJHv+hYfdLQ4rd5T4qURza/loVJIzQIx3iK9JmHs7HJxdRV9fqbO3iGp8nnUsynm5W/fueyppVE/" +
                "xyfbDAmxEWeu8noI04kFAXJJiISF5aOHWLlb5ECvxGY4rA/Ydo9WlnZDFHrkUxRiCfpvV/jLzEPGwm7W" +
                "ZpP2J94an79yz9zxPgfe5+81MN76GJj/PJTWnZnQEaihgsDdbwVLcxCDW5GH4ochmZXV75mKQNNQYzjb" +
                "F6ILjc8ArY3r4iW555JsLI5ibAubismOy4yVcnCu9a0o+nNeJaC/X+weHr0+O9j5jv/13MNXR7snJxDS" +
                "A7492N95aK0PT97sHh3uD45PLw5PTwZst/PwiXsZPRy4hrtQpoPnPw0OTt4cnp2eHB+cXAz2vt89eXmw" +
                "8/Cp67Z3enJxdnrkx3rmnr8+2X1+dDC4OB3s/uX14dnBwFX6AOjuzsOvXauLw2MMcfr6YufhN4a9mV87" +
                "D38n4aGQf/c3U/vvhCojV0adi92ziwH+9+IAUxjsnUJXnWNSoMDjjiZvDk+P8O/54NXuxfdofXJ+cbZ7" +
                "eHJxjvZfGTFfnu4etYE9id+9D8rTuGH0yjpxbZ71Wqvz8uz09avBye4xqPzV1+2XLUho8k2rydnp81M3" +
                "Rbz9XesttPefDfi3rXd6y5a9/U4CW3qbeIPML87QYAAETs5fnJ4dD4wJHz4xRvPEArsc7P2ZvAh+eIN2" +
                "ZAo0NApGuPJ/5Z0RzTHM4cmLU/9O7sGJ2KCB18np4PDPg/PTo9fkZLDoJ7xwY+l7K/EZ9w9dcakHUKOS" +
                "pnaHxmXzUcf2104iEGs7Fr8CNwszujCMFY/QdHcRxs5UnJVOhmo2Ed4dR9/tAHnHlZld95y4Lzs8BLHs" +
                "aHCAtaEfd7DvWrSvn9+0U/7tTxpGIKTE1n38TMwhq2XzB+YfhWPyj5qH410mhuaraFESwl125B55aNFh" +
                "fb2BqNFJM1jh835457I08SF/AYZEUudlCyuW83NduvA+dPzlC+2VpelrXBbniTd4KrOAEwWAmx93G7ty" +
                "cekr7dNwP3ZIWxl3No8xKS4r7nOPrkxYGmK26tt+/8A4TUb7zLcuHHcHvl6472b/86Evg+Q/xf15phUm" +
                "5K91EAP6Xu9/AMoRMcdUgQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
