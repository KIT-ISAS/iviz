namespace Iviz.Msgs.sensor_msgs
{
    public class SetCameraInfo : IService
    {
        public sealed class Request : IRequest
        {
            // This service requests that a camera stores the given CameraInfo 
            // as that camera's calibration information.
            //
            // The width and height in the camera_info field should match what the
            // camera is currently outputting on its camera_info topic, and the camera
            // will assume that the region of the imager that is being referred to is
            // the region that the camera is currently capturing.
            
            public sensor_msgs.CameraInfo camera_info; // The camera_info to store
        
            public int GetLength()
            {
                int size = 0;
                size += camera_info.GetLength();
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                camera_info = new sensor_msgs.CameraInfo();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                camera_info.Deserialize(ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                camera_info.Serialize(ref ptr, end);
            }
        
            public Response Call(IServiceCaller caller)
            {
                SetCameraInfo s = new SetCameraInfo(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
            public bool success; // True if the call succeeded
            public string status_message; // Used to give details about success
        
            public int GetLength()
            {
                int size = 5;
                size += status_message.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                status_message = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out success, ref ptr, end);
                BuiltIns.Deserialize(out status_message, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(success, ref ptr, end);
                BuiltIns.Serialize(status_message, ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "sensor_msgs/SetCameraInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "bef1df590ed75ed1f393692395e15482";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsVZbW/byBH+LiD/YWF/sJSTZDu+FqgLf2hq5M7ItQkSF2lrGMKKXEp7prg8LmmZ+fV9" +
            "ZnaXXEpKmgOc1riLJHJ3Znbmmdc9FrdrbYVV1aNOlKjUb42ytRX1WtZCikRuVCWFrU2l6KESK/2oCvFX" +
            "fn5TZEaMjoX0693qE4svuV5WstamEBqLqg1/n4+OsfoWVLY6rddCFqlYK71a11jF1B2FBe0RmVZ5Kuza" +
            "NPgAgWQttsQF60DFSwbZk6aqVFHnrTBNXTZ1rYuVIMY4RkyvNqVOpsy0ZwVKW53nOIJtNsodg95WakXC" +
            "m4x/6Y1cqcq9BcelIhaVyhQ4g5rBQxCK9nV0DomZyLJuKpCYj0ZWFdZUi41d2dNIqbHcTmPDkziLjGaz" +
            "2WhpTC5skyTKWtH9YVPVQHAnP+zh16hUpSNbE3fQkHVjFxtsxPGw5R/WnYZsLFJVS51bIZdQa2AwejG6" +
            "eua/F6O/ffzpUhxWxAvGC7QXhExVpgtAcQPpYmgJfOvwOhc3dQDOEkooBNnZm6LAhy0lwE52IkyIo0i5" +
            "RwwQmSRmU8pCQyHLVjQlqSWDWkCHweB2WqaWXjKuhXuzqORWzAT9m0oImVVmE0MhrUCmmorSWKuXgMNr" +
            "2QJbqkgM2aaj09tSgNzGFCZZg5SailST9et48SIxORTgF/OPw+sqldQHidILDZfbXewpd0Sjdd6X3dpS" +
            "lyqHabCuAHAKK6DjB7yxYuxXVAbuZ2tVKbPoH01AhoyH72mTEC5JWZlp+BGBDjYY6Jw12usa9uqsyyac" +
            "s1gbBfNRJMhi7ZeywhfIAGBXBCebVHpJHAoQcZgX8N11XZeXp6fb7XZeGTs31ep0qx/06fCwEVL72OaW" +
            "rBTUWldtUAMd5lGDH1DaINzOskqrIoX9dQFxMgJkbeggZgO7CFOCMCHbYrnTit6HPZCeidY0YisLBBwz" +
            "FWq+mk9BBux+JVtLcZECa2BCjIzXZGJMlepC1spOxVbBKpUpVhAG5gV/CAYSju/wOO6YYDoMbk0RQr5K" +
            "p/wO8lXIKFZcT8XbqfgwFe8jl8xVVoPOZ1UZ6B4BBgcpyDi1TppcAmdJDvUggm9kOwjOb+/O7sXVlTib" +
            "n5EvFqlO6BRAwUCIEAlGo+Pn+WO32P+7YXXK5LdGW93lu4NLXVx+NnkIbRpaAb71rhDTYJrezvAaPBI3" +
            "16OflUwRcNbug3OFf1SDHpLCpowsFR+tjvmN9s8W6DCrhU4jMqaEZWXupQANn34PEDGVXiFkY41bvE8k" +
            "ATLA5qtUfngKGx34AX3Oz3G18cVz/NAON6dmW/z3TZ+Hm9jdaEeZS8TFuJQYfW9YokDrK7D3fcz7/rDk" +
            "GGgVR9dMP8ETU651OkT2gnGc1pV4lDmKTleHwcqkJaJjyfiUu/HYp38KNDUC9DD4IGZ1fj8Xfze1DxXu" +
            "XKgs8mzWsaW6p0US2rjQchRtPgLgqP6lIm2+p58v6u73/HU5ggBcAMpRPkoQwnD8xhdhW1mViN6U4cZd" +
            "Jp94Xyc6tbl8FnmEOJ+Lv1D47Nh4LmNSh6bK/5qroreTr9N5BTp9gbBPxGWCyTfox2cvQi8nkidkD//M" +
            "iotr52G2d7EdpvPnDbPBbVFRITVaTspbjQZmu9boSiI0btEKDcGIRJ3nLXcHeojxrMF3vw/qMXnj84co" +
            "4Te5nY8anO/ilW+Rwi9unYJY3mKsJ5SPOaNnLj42ZekMyU9drZNjbSh04mq7p7Fwq+fruXiDimxjbOjp" +
            "UCUclXmzWS6WZnmEapAKaqs3Za48Y4S3SqYakOYGSxYr+BH9jESckbfaJst0Qsl9HhqRXQkOnK73E5SQ" +
            "+jNVbiWqFN/q1QdUgUqFTxHJ7SqTP+wUgZdi/HA+FQ+v8BqfNT4fLibzUZYbWf/xx7t7cU0C3RQkrEW7" +
            "4G3mgUm1K0PwkKPCiC5w3GVPQpyJ5OkeD96KK3GHXxl6wfY+rMAD+u+cHrw/hPUYaHuJHY7w6tohh4vI" +
            "rr7rykcqqil75qpY1WuU5dnTFCJM2GAlzpbokiISsQOJcYLXSRsp4k/3AqIfi4unC1GZ7WwjfyWUsBpI" +
            "Qx+cE6Iei/x27Ar+gCNYK2+p5EegMPVgJdxmxRX74WO6qB3yOCppSQf11J17ukRrjQv/ClW6QS0J4FPH" +
            "CP0tDXw23sH2p2IZgMhzwkx81g9fOas3EMQ/HcChN/cJWzPB5y0b/b03OlR+Ar3i8dD0/P85fdLj14CG" +
            "KTBqceWcK/6dnmypEop1bh6jO1iOnSCTSBJfc/R91LgLlB6glIN5qOF8g+pyPjOFD3AmIuBMrwoOZV3a" +
            "7djuusFOJCbPqQ/H7t+B528F9Akh+uRLkCZMYwWUP0EsqrlOoSIg1RlGOX2n7gsSSPg2RBFulakz6QPi" +
            "7RMsetvin7M+0E/3V3LQJ+4yBzTXErOVD9jkUQwD1y3L+/7u/PJiiv/R3/SMZQBsKTXaotAbA8hoyJtN" +
            "Ie4gB6Q4uz9xtVDO7Y9zE+KKOYOr3j0Y9otoemrR90GGzrqOka4s660b7LFt5uKTCj3Z7Wc6P/kcO1c4" +
            "MuUbT4TKOKKx76muT2cmgbHMt7K1UJIdqheaIBp9BT92Ak/CRhxDijVah880fsiHSnNU/KDAEZ6Rg74U" +
            "r9H9rrFSvA4wX0rr5hhLVW+VikHKwfwnnn/KDsji7p/iX+Lf98BVPSxcxoifiK7BB3kxhKMe/5CnkABu" +
            "uLpsMVBCVGjEo9jCrFeIHS89H6SHk6gWpaM04lRso2d02MfwjIdna5OnsB3QxEbyoY9VFumpD37nr+7B" +
            "koLfjweC3/duXd6FyQfM+H9pXdyMse/WwlBXJnVDPu4HuG40GMWwvoR1Mz5Uw3mNjnDFJSKcHFEGExme" +
            "oofpUHBKnlw7dgyklulQcEK1mKypnEqpPVFgT6UnzWT71mWYNYe9y7OWwq91UXSzbwCLXAehRhZtd1Wg" +
            "3ADelcYYJi05+xLYcQiKi2T3gkrapanWxgCZUIIreV1BjzU0bT+yDZAw88XwEaUR6LyhmZIPbVHVfECP" +
            "bpwmxu6u4VQsneyLpwncZuzvHfrHVOn4slNlssnrkAa6ffCrbjEHFJrqoxdAFOdrgCjYyS9vOxfjAvP7" +
            "Zom1JSLNCnx9ad9t2X3QuvIqXElw94hJDaJgs9xiBma2PC452FBMpj6scOHvFkWKGzcFcelbyyjTUjvX" +
            "z+TEh3c3Lo1xjEbqQqftgia36r0g3pi+MO+AQS0HjSbJn2SV5nRhgdX+kAE5dtcMAVHUYRgtxjQP8KY5" +
            "m3zBBjDAwcOCwNzh4co1Uhho45EHw5VvtWARp+x32U1QNZZ9x8uPOnW9mBug0a3HR7hLCi3xyNddJCAG" +
            "ryEefCJXj2i5eFRHAxZ6W7el6zY44lMuUQXUTuEqTBRostsUPC/tR31hPw9TkRAie+/WZESde0eakWBc" +
            "fXN9ybpXCbQLgWiUnVRKcnl2cy0CirFhdHy7NTPC7WowZww3auqphJZJTmkp+b10h5uDNl0NUapHfcfP" +
            "FvhpJ2gCSQRV4gIDFwwYcrUIig5wj7LSconOlLBBdX0qTmjTySSiTGJf4vqmMIG8o9jz+BayRUeXzjRD" +
            "nE7JpeHeQDgt9FP/PlXwWFtw2K7aEU9UmeXo+I2re8l8bBF8osgyieaCjoJ+6JfDfPV/cxu36wp7d3I0" +
            "//cQc70JUl10gdpFKzoCYazoWgNQ+rRGaNrdTppCtIldPx6y0JY+O2PmQvqXD4obJRWulKmmdr7ON8k2" +
            "TGcVxAAI3Y3ycD3fBkc7QlMT2YF5/hlD6rDNBxJKCIMLie4KeDcIdVJ3dQRuKbyrPC1MluHUNE/+Bb0Y" +
            "j2A4nAYdUBDer4HEGNmoW0B+mDd019T1dCpdDUfQk8CyjVjemvI5OOKe7isMvd6YyM/uO9YRl3jC5bl8" +
            "4u/+PQHP32rjHhWjFnDlMsjX0QyZ7rYAPkpxjIzWdXZHmNIcUS5lkcOdGsMY3UiL60WOmPy8p/QGnVt3" +
            "l87mdFN8mLs3ohgTUeRERjOyLuGvF7fP1Lt7+KYNO+buKj81fO8KN4B7/wcq0lQUnyEAAA==";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SetCameraInfo()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetCameraInfo(Request request)
        {
            this.request = request;
        }
        
        public IResponse CreateResponse() => new Response();
        
        public IRequest GetRequest() => request;
        
        public void SetResponse(IResponse response)
        {
            this.response = (Response)response;
        }
    }

}
