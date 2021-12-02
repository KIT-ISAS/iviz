/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PickupActionResult : IDeserializable<PickupActionResult>, IActionResult<PickupResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public PickupResult Result { get; set; }
    
        /// Constructor for empty message.
        public PickupActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new PickupResult();
        }
        
        /// Explicit constructor.
        public PickupActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PickupResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal PickupActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new PickupResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PickupActionResult(ref b);
        
        PickupActionResult IDeserializable<PickupActionResult>.RosDeserialize(ref Buffer b) => new PickupActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4c148688ab234ff8a8c02f1b8360c1bb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbXPbRpL+zl+BsqpOUiLRjuQ4ie70gZZoW4lEKhTtTeJysUBiSGIFEgwAmlau7r/f" +
                "83TPDECQip3alVJXdd6sJRIz3T09/d4NvzFhZLJgKj8a4aiI03kSDwezfJI/fZ2GyU0RFss8yOVH4zoe" +
                "3S4XPZMvkyLI5Efj9N/8p3F18/oECCMl4o2SthOAknkUZlEwM0UYhUUYjFNQHk+mJjtMzEeTkMrZwkSB" +
                "PC3uFiZvYmN/GucB/puYucnCJLkLljkWFWkwSmez5TwehYUJinhm1vZjZzwPwmARZkU8WiZhhvVpFsVz" +
                "Lh9n4cwQOv7Lze9LMx+Z4OL8BGvmuRktixgE3QHCKDNhHs8neBg0lvG8OD7ihsZOf5Ue4qOZgP8eeVBM" +
                "w4LEmk8L8Jd0hvkJcHylh2sCNphjgCXKgz35boCP+X4AJCDBLNLRNNgD5dd3xTSdA6AJPoZZHA4TQ8Aj" +
                "cABQd7lpd78CmWSfBPNwnjrwCrHE8SVg5x4uz3Q4xZ0lPH2+nICBWLjI0o9xhKXDOwEySmIzLwIIXRZm" +
                "dw3uUpSNnVfkMRZhl9wIfoZ5no5iXEAUrOJi2siLjNDlNgZx1HggabxXMRr8FTc7wQ/i5wV/77RFP1y3" +
                "O+cXndeB+3MaPMPfFEsj24JpmAd3pqBADg35M9KLtwxS3Ljz7CP0QGG2zvoX79pBBeY36zB5I8ssA2ch" +
                "hENDHn0R4Oteu3113W+fe8BH64AzMzIQbYglrhziwW8g/XkRhOMCkhwXPH3GCzKfRA/mk0ZJ6OafHfwf" +
                "QiJcUIGDVi4SQwhxkTsoIHSvb7IZtC+hKSjMviX55u3ZWbt9XiH5eJ3kFSCHo2kMExFBDkfkwnhJO7CN" +
                "Efehab3s9kq+EM3zLWiGqRw9WopYlrRvxRQtzWdZQ6nIU6jBOIyTZWbuI6/X/rF9VqHvNPh2k7zM/NOM" +
                "SN9WcqhQ6bKoi8vB52kcmlEImyowPbIl7GQRglJaCFjqeP4xTOLovgNYyfOachq8eATJ86I3TwtRwlL4" +
                "/OV5Dp+1Li9LTT4NvvtSAocGrspspfBLuIs72bytdaLn4zib0anRffhrELtMSky0doiqmHz/bzjEl7GZ" +
                "QrGmfoqAbuMembjs3vSroE6DHwRga+6YYb0HIAURbo1AjDIh9CwglKZGATkEPImEb8Mv0L2csFNymyxd" +
                "xTg+NAe41k1nY6eVJOlK4hEuhCrgl7R0ViDGOirqWFCJq7glMsPlZEI22kWF+VQ0HtGVXZxLlGT9rmNS" +
                "XvC6eR7xyWDpahojthB/XDEpIh0mYix0IaGLRFdb+IT9Zk75wSlNTgYhxDGzBe4qSbCbMHO9vJUBag/a" +
                "iR5E0mQ0KUJRNVSw9MO62PACphjk3a3fwtiYaBiObimN2KHxK8LJPA8nRq8mX5hRPI5HThmEgrxpoTPW" +
                "0wUgarYUpYCdi7Gq6S6PQcgDXd0MohgXem/VSBwIr/DoomhnWZqdpTy+4a+DEX7H0146TAvRLPA+pA9I" +
                "s7uBqLF72vffv/9QWzQxecOebv0Z8IyyeMErxorG6yzMF8GEf+PTOEnD4sXzYJGE8zm2DuTOHp4xdU40" +
                "NOSG76GUUJkhbbi3afgxTjP7VGKIm5vTb+znV62Ly7e99ukP/NOwX15ftjod2P8Bn7bPTw/d6ovOu9bl" +
                "xfngqtu/6HYGXHd6eGQfVr4c2IUt+OnBy18H7c67i163c9Xu9Adnb1qd1+3Tw2O77azb6fe6lx7Xc/v9" +
                "207r5WV70O8OWj+/vei1Bzftzk23NwDQ1unht3ZV/+IKKLpv+6eHLxz1LrI7PfyOnHD3EvxHcAujOQuR" +
                "5Hh1VvFxvLvpt3r9Af7ut3GEwVkXbvAGhwIHnm1Z8u6ie4mfN4PrVv8NVndu+r3WRad/g/XfOGa+7rYu" +
                "68COqs/+DMpxdWHlkdvEu3neqN3O61737fWg07oCl7/5tv6wBglLXtSW9Lovu/aIePpd7SkCg58c8O9r" +
                "z7ovGZu5p5AnmJE7GLbZOptf9bBgAAI6N6+6vauBE8LDIydonlkQl/bZT5RFyMM7rKNQYKHjYIVW/i3P" +
                "HNOswFx0XnX9MzBrpyoGa3R1uoOLnwY33cu3lGSIKC7x4fW4tFkuhXeGGtEOnQBy+jncAimGi0HkbcOk" +
                "jBslBTMHQdw0Tfl2keaxGKogHUte8c8Up8slMEGCeps34OxzmExB/iMfqsGUdTSDIASUyBPrpWZwMDAj" +
                "Bq4gKWKEjcF591UQwlOVXgLFCbMG+oprsa6CQrYPonQ8qCFrIYAeTQFllCZJnPOc6ZDWF6l46J65BIGn" +
                "CGxmLjzYb7j9Z257V3bDirvd8BD20cBCJt5XSQhHNo9YGZGoZWoAlcENY6gR6ij085qvsoSRwZHTTzMD" +
                "CqJ4PFZnCgcLPhSewlSAyP5KWWaW5kxS49kCuVOIgEvKOq5uIIGvO+owjRg57Dl6sJChHCs5iUF5Ycti" +
                "2vwxqFLROjkZIZA4Oak4RBtbLBcoMYj/LpT4oiJy+41hmjKWHPBwDyX92wWwwqnQq4CI3zRNUKTRWlca" +
                "qDPGhZAJIkF68Bz5AX6Bw4ZlF93JcOXgjypAEwWsMvrTTQYM1MfBXmY+psmS32eIreJcDMQ+qYnMGAaD" +
                "kR7rUygjVdVMUOKzgxJGBDDbPyiXoj6GuKy421z6NJfFT1HUonqWW8wYd1X4Glm4WCD8R5lgXgXQmXF3" +
                "Zx8BKXa2y7MwNJVYjdIWIbRXDUWkSnswRwXJMkKroV6J861BsMMnXMtdxArxQm2rGSAh2FijBcUUyuOu" +
                "UYyHqpHbShRyg1L6Ak49Rt0A5jHCUivZzviEWRbe5QeCgTok18jq5TqHhRhe+1q1CFTMwlujm+x6nJ0S" +
                "lkqEFybN4B8M4E1z0gzu0mXmTKicYp4CoL2fWpGOsGYH3CJ5GjSVpy6vU+gOmAjcWWkk9/Q0ereVs9vk" +
                "RDnn+JTHf8DW48hgpMKpaA1XIeG5c/mAl4EyMSiZQynwRIPRKKGNCvprucEm4tr1inkZF4v82KAXH50i" +
                "VL5yAl/5SjnwGAZl0+3gVD3DIjM0QX0orl1tAKXHXq7wwotYZCZI2UTwxvglSnGvgDOGRUlXzlyDJctR" +
                "wVoVlpX4VJA1RQTrl75cETr9ZLXdBkbC+XwhZWjmcEw75jndve6ZmKLUf4ANk9Ri91FMMJoiSmgGr6gK" +
                "n5AlJpCRUOJeuAprLOCxgPZt7/yV2LRjOvC9TxBW/BeuWMamBUJdDNUteUgJppRVyvNV6pSR+JHFgKJ7" +
                "qdFrzwFVVzhoUGjkJZLpMj3Fgddo+H8z9rhmbIW6ObZ/qRlzy/8vmbH7rJgGoNyeNyYGQVuBPF8MSN+J" +
                "sNYA9PeNRSv4JS7gz9qzfwib8FD59VBG7x6qHSczZ/KsV/dmZWiKlYFcFKt0o88n90eDh5ggHMGSNd5J" +
                "CeRY9yeq1T8vsSGb0wBkqZrUxzmkJWbLEUM4HT6r0R94QywSNTMMuyFTfqeE8ZQZnEGKX5lEyciniiBK" +
                "wQ+E3WLFoGr0MhLF0BxDHKs84dfYskdlO9D6n6yiq5AOq/RkYauzeILiXc2MiuG3hzsIivERRBoqJTQr" +
                "MlwhC3mW2/vN4GIsCrrigUS5XXjMwNjSJQ2JIk0PWKm0INYZei1K5HQVqWYBPWmWNa1P/jfvzIM/HuWq" +
                "Sxnbdttw4SitOne+duf89HspoGTyZw/kfls9kq6K0bDHcg42L/OE9fMMs/SW5eS5iFjO9JcpIF1uOJ9I" +
                "u55OA8bO6apdUn626x7ndGr+ttwarkKvpzzcAZQKxIvr4QGZOXzZEQVY+VGzvsco19xTarDuufat6vGw" +
                "kqyLr8JER/zJZaZUWnGZrGu44jp/F++EKoXw0deCbINsiTAgn4aY/BA2IQrHb64vs0Gb2oVqJYXLBOEO" +
                "LIntXsh18awCcp7aGkwTBovlEFMg0KNZsvWTnW3wXDVF801/DM8IRRA16oxSoK6/YXN5qVeVrT6Pz9WG" +
                "WIECRenK0pAuRfCAGDl7yJ7HoSdMyWBInmBQJbrT5BJBgVJqN1TK/wQ20IoZvcjY92SUKsmDEWOPNC9f" +
                "G5/BbUhaoPdB76OHFM8zIwx/FIC2TgNGzYDtyJTUk40SZFZ0H1m6FEWwUPYPXAFCcMzRx4T5zu4EW4YW" +
                "qEzhcC6FgZQi5vWxy2XLdJXWRlmEKRsj7DCCuoFF4S5lZTCC5IPT2mXAY46D23m68omgXf8YOrmpiy0b" +
                "8Ynri4Q1vn7n0jfRmXqA6Ptr9piWgXsiPQILt6etl/1qJ0z3uXvGRJYKBb3yMIT3TS13vPbozwHLIZO5" +
                "ZNB6Fj1CnxAIxkEuC6PW2rJcsyVwdzqr22KRRa6EpNRLlRKOV+q3jgE3KacXHJoRq2EzEPgRzBKLo3TK" +
                "qmv3iIWAclk9yMjXnlOu2GnbCa5MPl2Hym+wdqYPtsLhsxLESyqHjHEg3WXJFH0dGxXk5ekO/GyBLHPp" +
                "qAYROPxSNQ03FkVyF7CuRLFfpe2aW3kQwXTPIfmspK4VMa2vCIZy3bekpEQswVxlEWQUHbtljrDPfEKk" +
                "QPIRW6ozFYPTbAzvELK3zs9PnxFNT4zqGqZxlrKCgPxq/jHO0vmMwS5LgbAQd+AS0nDMt6kqSIW/gDLn" +
                "NZmIo33F1Gtfdd+10TXkmRYL2inGrE6abXnDGlYh2maCnzuri7F1kzsnbqE85DXnyk6PrBEucW5HJ1gO" +
                "YBVXVvLtVUuwvycDI/beXMbq2tuJGReajbIaAmuWpwl5BdY6i1FaUxShwclISRTeHJPA7gJdVxfSc8bN" +
                "ZAxA3cLUPX4oo/h5o9LY+ct/Am3rca7zr2+2f8icsz9vZ4nRlELvWByeNWQwY6xJMVtFVCAVOUaMuEKT" +
                "sQwy0Q6JrxJotR5ywobIWlBxa3wJvorhREdZZH9ZLpdMUsQFFgszN0Nn7AHFAYyGVVKsg5Uy2Y833c5T" +
                "TljZ2tmvratLOxmDkrkXYZhZrwCVFNPNp/mShtQH1ak7h9IM2hI1sNC+cemiR1K7SdNbxCu35iR48t+7" +
                "5PDuye4ZI5vzl7sHwW6WpgW+mRbF4uTpU6QfYQJuF7v/80SPyDEokqeFu7m1jHp7Nrrh5VS4wMgxLnax" +
                "KUawDy24NcYO+44TqOowTpDiWPe0TV7Zt1ImupT5/KXKhgDhqaj3FrOWvChcOgTqCqAySsyCqD2sdG4E" +
                "zEngGSDfkQX4rs6Ck29/+P65rqDr1QoB1m1SvGsx3fx8iU4VQgS2q/w9rSG++T1541YobEEV7K4m+fEL" +
                "/YbNwZPg2+fHR/KRI1ZcgPA5XdkVcPsr1G1qXzNC4UEcAtfn1KezNFomfC5VgSJd7DqBhmg/VFn+vmgB" +
                "FJ2rmg5T1H/zBSXtIBjdIbSWoA3iZgJbWHRZDsTCdeIgVq6giAhn6EIAAKPBp0sXTdTA+dkB/ocSgE7a" +
                "vez+AjdmB2qv37QxdnBkP579itmG83YPptx+0e20T2VioF+xT+JlSJNdpVGaMwloZCGtsO33cmnZCSlX" +
                "uD2sSpH86obKshOt8TJdka6xMkFdNdn1yVmq3XLPrjo3aXHbnBAHF1I1e/jlIPhVy/a/VWkmkyVhMvMJ" +
                "gkVLUd0GMW3y5wPTmyVvB78gIik//ep5zU+/0YtXSFL+W6qk2MVrp9nET9vH5FyuNSpiivXc6J3GGCpM" +
                "xzbNUQlydCjcQa91fvH2hhFSBae7ZIHJC9bXJ5QrKjpSfpCaoQsPpeliUf0WhAg4mkFZLFyDO3jTvnj9" +
                "ph/sEbb9sF+eSbv0FY6XZ5qupVdOF4I96sK+4qOdc3j0dBaPfqjguQ8Li4iOd3p9NjnZjhMuW4sB7hHn" +
                "qXycX9dJNnjiTFJgHSREL62UIeEp9zPZpLwvFwe2nfW1ZapT0hozvUjVDs94tNTUjcUlY7jwYUzcZhIg" +
                "yWe20W9kMFqvfcltMTzQ5zpPQG5XipsoYWuR1vdZK9X3yrrHOiBIcaW9tZJUdR4CtRDX5yyP+5kS7MO7" +
                "IKaWzvFUSGU6SQOB7FqVL4tRc0QE8Z8VC4uhSkwL4lrHOvBcDlnhjOxoItjJ339oEEffApD2kYVFBJXC" +
                "ndvhci9EfksGSTIwMd3ILMFL6arrpkdilTvGFpa5YyHK80TpC1/vj5VO82kgdcBHoVby8q1Nfm2CQ9M0" +
                "vy/zf18kCD8FX7P893Uw+gN/RXxpiZcVBienEHAzfv/sAyuK/uM3/DjyH4/4MfIfjz/4VsP75x/ku4di" +
                "wGdqeLW61ta+Z22LEzRR3vxvottZGBlnKtdai1JOKqH2x7TPK+L7A9c/wVN8CEd8G0Sz7fwDbyldX60T" +
                "KR98zbyCS12ZvvSh8/4Sh/qyiLUG9H6u6sDqIMea0LTIa+1psRG1UzZx9MaWIZp8c4qGo33ll2vH2pyv" +
                "iZau/ADfP2ANyA2/P9Ica0UAP1dkVkks7t+wNslT2VifIq2AeCSZvYey9ak694IIh0JFdOxroLWWu52d" +
                "Mepc5O1amcDbIpfudrcUrbeFHHZi7hCccp2mEtaeDs25ecH6ZM++U0FdUU4dVUBISyeej5JlxFPYuZBK" +
                "LJM/LWX46brk7tjJTqstoj826bBfeWgVTdJMYG2TKp1bxpI488a6BgowhJdbzeA9t/n3mMM/I8bdSf1a" +
                "WRNxIlYdINtjUJEGLzitvf9lUy6+7GPLpmHZlS7tnxNNhJ8T1p+q00r3zMlUjNkGCr49VcrGv4ZnXcr+" +
                "VnsobwndO9KP1lD5UpFqq7xQpHqxkqEpvnMFC8JBLe0aV1qMKMEf+lYhLlk3++r4gVVMhj3TdIUVDGoW" +
                "iyylAMV4PgFu+cUUI8kuK1T6oqQlF3Bp3DbrtmHwRBGLkjzBmGGZCHQ7lZo5h5Qxjgarx/fj9B8o4DTx" +
                "vbNiZZd2NmR+uRV9Bbcd4NxeWnayotm3dkXW2cdUjMPfsppQpYdTn2ZrqqmVXqDv7ep7YButQGk32j6S" +
                "mxGVNJ8mzEk2TPah7t/RAkiZWDC6kNv/nB8FjIEnudqv/SIKHPYKYm1CI5z4C0RsJaA+I7/G9DUCnADa" +
                "8T/hvS/uPIHYw69JL/7JNlgHIquaW+mslINgBVcGE76K519tbCXiuwUGFnQj8HvVq4LRyglf4EBVBRYk" +
                "wWwf80UBbF+s8KzdqyLYt+MCQzMJ580tTcMb+09xlFLn2GdyWC1p7kJth6FWzmV2Wd8tromgDFSWr5+Q" +
                "XowC2HunDXgySdPoibbYmj71VrzIhwjdofaWwg/HSeuSiao9LXh2a3umtsvyGgYF/a1+ZSiuFE0H0CFA" +
                "Gx76XWyBr++4O2tIXz/ktGL5uvpe2YVzMwj7W5Hjtix2i62OfJY6zLZPp4N7foaiYr9YDKB8luaFBkmN" +
                "5PoEhpdpTjKZZKyt53sB8z0RrTOUhsvNynjAWk1TqC6mdCTD4sobskoPjrbMWI4D36qjxPoKMErtdtao" +
                "io6M/BjGiYwL8gjUGNaYiKt5L2ulFV1lrdQvUMebLWfqONiGlpkvTvGyrzWNE3sMMmHvv06fyQs9cU7U" +
                "+yqSx0cEMrAABjrnJePpfrDZ12rSIXRxhPqK9Zr2RZMcMzYoMY42vYEfc7edJKo5m0EmerpYYuQgeirT" +
                "SS6tG2GwUg2AI7qMIO3c0UDnhPw7ZQ8TV9xrNsQf8bOb7nA5xPY0w/3rO7UEQgCo6XmMuKguTyyh2KbG" +
                "+lBtqU+yxQ7z4HJU5CWlpxTCcuQY0+L7Gf4VCPk9icdSai6nBtKxVLDcv9Xh7JJTSn0wtoB54VawS0Nl" +
                "Nao6EL11StkZdr/Tg9LZgbWTilfBvXkdsIsG/oFTMJzefWfLh3Z+oTLPNqaZ4UFK18TRX7VKeYAYTCbQ" +
                "OOwAEfJqF88r6B5GEP6MU1vHut2E578i3g6Gjog3Gv8Lw9ZVGQxMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
