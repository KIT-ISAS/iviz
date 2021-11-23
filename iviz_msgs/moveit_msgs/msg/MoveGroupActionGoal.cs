/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupActionGoal : IDeserializable<MoveGroupActionGoal>, IActionGoal<MoveGroupGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public MoveGroupGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new MoveGroupGoal();
        }
        
        /// Explicit constructor.
        public MoveGroupActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, MoveGroupGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal MoveGroupActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new MoveGroupGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupActionGoal(ref b);
        
        MoveGroupActionGoal IDeserializable<MoveGroupActionGoal>.RosDeserialize(ref Buffer b) => new MoveGroupActionGoal(ref b);
    
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "df11ac1a643d87b6e6a6fe5af1823709";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09/XPbNpa/66/g1DNna6sqbtLd6Xk3O5PGbpNO87Gxt1+ZjAYiIYk1RagEaUW9uf/9" +
                "3icAUnLT3q29d7PX7dQiCTwAD+/7PWCfWVPYJlvRn5HJ29LVVTmfrf3SP/jKmer5ebaEP7OyGL1wN/ar" +
                "xnUbfE9vR4//wf+MXlx+dZb5tuAJPONpHWWXrakL0xTZ2ramMK3JFg5mXS5Xtvmksje2gk5mvbFFRl/b" +
                "3cb6KXS8WpU+g3+XtraNqapd1nlo1Losd+t1V5e5aW3Wlmvb6w89yzoz2cY0bZl3lWmgvWuKssbmi8as" +
                "LUKHf739ubN1brPn52fQpvY279oSJrQDCHljjS/rJXzMRl1Zt48eYofR0dXWfQKPdgm4D4Nn7cq0OFn7" +
                "ftNYj/M0/gzG+AMvbgqwATkWRil8dkLvZvDoxxkMAlOwG5evshOY+etdu3I1ALTZjWlKM68sAs4BAwD1" +
                "GDsdjxPINYGuTe0UPEOMY/wWsHWAi2v6ZAV7VuHqfbcEBELDTeNuygKazncEJK9KW7cZEFxjmt0Ie/GQ" +
                "o6MvEcfQCHrRjsBf473LS9iAItuW7Wrk2wah024gfd4RNR5kCiItmWzmV66rCnhwjaV10UJgL7erEjaE" +
                "FoHskm2NzxokGA+LQAJ6TvtNJAkoMbUMBpvc3ABpbFe2zso2g4Vaj0QLdGHXmzYDhENvhOmZarYWhg6g" +
                "s7ld4FxMltumNbBzOKMUvzL/stA9AfTC9HY4SMBztrC2mJv8GmZWQA8gyq5qgQe9N0tLm5D5jc3LRZnz" +
                "AmUGfirQkUG4AUxq3fkWZpYB10Grqe4f7twdbd0aJFbZ8r71pNcIhBki+nVl6jc8Z507TAbf1jC1Vxts" +
                "A2QrzzPHL+5nuoP5qTBD0iiBNYFNcWc9EVxhF2VdEumgYDS6GNxO/L4maACClgKUBRtKH1zXbroWaUzp" +
                "AEnjtUGeam3jCRw23Lrm2m9Mbpne8JXCEvLHFiDzPAAZfaetE0gBwmwTXo5YtoOQRTHRIs11G+B3EN7Z" +
                "80Wg3p8cCEuvA81p8ThOY5HCYDob50veKwczwxkbFlN51zQoYlxt/QTfeJs0ZoAAAkB624IabOzojZu7" +
                "9pLm4nFqM5qXcgx09iXKPaJ3+hSRtHYFaCLkU0ANvp1mFwYkgK3smmaxQNkIDU3TAK/RrhHnCfMsYVbM" +
                "OvQCZV6+KkG94RzLBa8IJt42hhDCe50oKQYB4GHupi098t3oaezx9h1r8wQILuylE+QDKk29g0XCFxAy" +
                "DnaFdtoAp5eMUaCTosuRnYmmeKnbsqqym9JVpE0Jy+k8T0gEms2mEjkG8osHgU2pXZv9hKIBxA+/G6dT" +
                "psGHE74aIAInxtIJCQne/mRzkMY7ljmMit3oKrxP4cfWh0apUQ25RcJFge5hHSC5iVBrxw2RP1XeTZA0" +
                "cY8NCc1+X0IYykIQ/SoI5Rupsv3Blyi78EGYAYAl6kXhwk+3AUsH8aBwqecMYQW43XoOjREyGh8RBPaH" +
                "ZRGHrUEwgJbKLleuaVGWeFd1IkZ0+g2YHA3pMjZuAPAsSEvTtqisAirX5n257taZWbuOmIH15AHMIrFU" +
                "lduypabMRCaI2Cfj0aJypv3TZ9owDkuqDuUKmCVkHxjcXGYWFuBkB4Ls6PJSCFynRrjNwHoDBdgirbLi" +
                "M3kOLIxYBQKZZtkTmdyNqTpsBOwGUzs5nXz6Dr5e3QZwdwBcoh+FwRqUPyJKEPIaqRo5R8ym0iKfYze0" +
                "MwGcLBBGBkoEBYICEDuCbEchiUDLBucK2r4x9dKiMbfGjTN1W+0mtP1k8eZVh5bA3JI4tqQ+TqenY5Kc" +
                "Mg7RuA2aRembUIF7+un0lGCBeGdEn5RTO53chhEA2P+SImc8DdsMjWbaaeZ5a2c8o16btPuw3X3o7QOa" +
                "TzW32k1BcxtCJOAsKkQUCIsORH9NvkVL8qzbsMkN/Af8cmKyuXs/RnJRAaBE0+cbnFbqAwlosGV3bFkq" +
                "yxBrrN28rCwpE49zEk3FgMFu3dqqmiJfnZPWYopoREA1dgGqEz0GVYUwRVhoU+P6n/WcTJUGJQgL2HVu" +
                "pFIOuk3YsBQSVzrjeXi1FUAN29HSOsAayG3C/LckxB8h4BkDHQqef/xQQHA61N2Q1sFxdU9B+sLULWoD" +
                "AxzF3AmiEpyEjGhwmgUfg3Z9bYHjcZmhJ2qosrHkdZDl0ZDnMEGLsHAgC0A7A4y1uUYLCRw80t6gyEGW" +
                "oeasfcViDF5DlxM7XQKvE3VRK0QgudPkgIOb0JRLEEPUEwZah84mk8WBrFk8ZAVDc+bBYF/QAXGtyAUU" +
                "dTvXgc0Aa4Afjfj9pLt0XuSfts5NkPQFRB+hr0nkKGMCT7ZAo0DlKlHeh1+78OuXe5Ai0Qa9VXiUdcSf" +
                "mYOs79Nui3sIgpdttmgeL2CbvNoQ6AWAgLz2I9xb1/DgX+NHNoCpXTSAv3ZibYGduAYDdmVugn6y2fmr" +
                "L9nyDBqN7OkU9AtsC+2SIaj7rHCL2WCwJ20L1i9AyV1VlR7X6eZoqYH6MvoNNtzDhtIqMpco0PFI+z/V" +
                "7q+oN5jA2nsWIM8EMo77ZWXAKa0LDA0h+QIxi0+Bmi8HOo5WEqk5YKWWYjXETIvFnlChGbJjQv0Tmbx2" +
                "vsVQkapjjmtp4IRcBV3q3BVokZzofKAhms4YyqqsaQ41hoFgw42Q1tkZyCp7dpY4OBInIJ+LfPGWJ98m" +
                "JDcezZ1DZ2SGi7srQXeYABNMmcACRH4rVxU+MD1YLXlTzlkrsStDCxf9CnIF3GHincZRrIoZAOVjjORw" +
                "J4sOG5siJ41FnYvvG/B7So/slo9xNuy6oUWGAbrsDz02Uw2jUExBZs54EptG+2fY9IGnxg/8mNgzdrEL" +
                "2Ks2BAnZmwpWowB4ucbeL8FwwoVdxLWg/qe4C1JbAZKfORQsSpQH5BwwIlhLByb2BwNaOp74vxJ9AvIq" +
                "0GMA83ivDUdUwQ0P20jCg9lIu2YSWfAU+yspyHpAAPrSs41iovAhjxq8SRwBeYi2EY2ZPoZpMqQWUy8b" +
                "jSXUcNRJ2sPakcLUY5hm36FSQ/3G+kZEKK2idgBQ9mcQpURY6wnpqhzcGODUG5tuJ0cC0E/aCTUi9ng1" +
                "vLfJ2iUAslIYhCdf/gKyHpYMiGQ4CddQjIcMPo7tBRqIQb6IHHI3ddJoLaFhgHYo7SCoxaElxw4mSFSi" +
                "H1GQ8KiMkLxSgk9eMQbuQ6Dsqx1Y1Ru1gYzyLMsApB7ZXMJFILHCLsGyIsJDE6twsK/ofjk0uVVcA0q6" +
                "vO0akiVxPCZkNsUA9WDHF8zJRvkT0w1+BwbImjHvNxSHJ9MomEvcZ2nbyP/oT4ZAzTUIJRJSWb4CK2Ga" +
                "fYms8N6sYf4TDBWBxW8aFRaGKOzvb86/JJn2CBX4CRjF4BLuzBbj+BwmBAOYPyIFI5Ul+Yl0doxI+NOU" +
                "AIX7Ikf3vpM1iS0UGjD0jW0oao2hZlhwbw7/L8buV4xt0Y9b/WYxps3/L4mx26QYG6DY3Q8cvislYWgV" +
                "yHmv0RY2FBvg38G37whN8JHxdT/uYpj1AYeR2CGIlblttxboot26vUSnH/iUo5G6v4n/N/pbZzDigwJA" +
                "vbT7WWQc+JBbDLzalEHG9xaCTz/HWSMePuj56a/tPW0gUZIsS6Wuj8Zjfz3zxl1jvrAmX9yjT4R+Acph" +
                "Uy8pP0DRnWnYQGkSn6Xd/ayOeeLArsFW8PbExU1An2OUrCWLt0VB9RuXSMDiI7sC9xEJvMX/FJk9eBsi" +
                "6sGDIwFmQH6/V3eF41Mlrfpag/v4W2P7hMdeXtCAKYJpqpXZcDCbolkxYbU3N1ZkqXuNzWjAo+cLTU/T" +
                "duFaCWTtxDGfloVEhyeU4VGn+ugQPHWxWXuHZSSpBRwAs1d9RDFQDe2Jg0dBjJiZDONpwADDEklktHUd" +
                "Ed58F+LXn4SJzTSTY6oGNMQuSTYmHaLWIGAzDqOgjRfTljwrsirA8MrZWevl62A3yFbk/cAwHS+SQnRr" +
                "hBGWAqAlugZCzQLawXzmkF9eOSoyMY3riBEEioTtdYza5qh/mx2N1tiKa1MkzisD4/ZhGYPEbpLcWPTM" +
                "YxoNkIGzm8kQuilbWy5XwWIZbMYEE5nXtdvWUZpS+/vgyX1efCJmwISrMBYUQpWgjtr0xDOHo9hA8bJM" +
                "QeAJUQ/Bgt3DooPn7TgtdeB+us+7jWWiwEjG3HhyGwg7gXv47wyNyyXn+HktvIQrhIBgYlpUo2UibSmN" +
                "tW/NKc9yt7IRyYCccigongb1FAGXDlNKOkyOIZJ1iUkpPyKJw/OkVq/1E3qHsdkwGut732eMeBjphfWr" +
                "PlR8A23X/OEgHPwWQXyBzKFpVoyjWVT+Is1i0jabd7G6waqPwkYELL5jToMdK4qSTWpC3DidG1Zt0EJo" +
                "pFsWid/i7J4UhU/JSLAeMjQUN6Sod9IIaPSmdJ0HO9i+L7Hwh+L2rExJ4ExH8x3YcU/Ozx+fjsjnRW7o" +
                "jbRo3JoDEvVN2biaChXQsWrQvj6x4JvtQDQRK1DYtwVm9gOaKIsxj/Tm4sWrby8ef0pr2mxQTqEHW4d1" +
                "kc8rgpUm7UNhwa+uVZMR3EnXCbsQF/n69cXL88cPRQjHMQ8PR6NMQCpuhfJlqykrckIZd9k3dWO0fqmy" +
                "i5ZdlDGXZXhXIa4AtSoxojQtrC+xTIWmSLh5hBN8tdH8L2tceEQDVBs6/XxXQvHDQmV09Lv/yV598fXF" +
                "0yusdvz9neUfRM7TX89xkNAkt3lBCk8EGYgxDFSgC+MtO9ZJXrV1Sw6bB9eRQ7hAJxgl7xkV1zbEZdMR" +
                "zugN94/Bh0YJagkSq86KuQp7gKIAi3k6FVGwFDv5+vLVywdYayEBlR+evPgmYwDT7EkgYRCzgQGSXBwK" +
                "asVKDBqxUleFMs0uyGoo6wObTnxEDr1z12CvXNuz7KP/OEYMH58dP0XL5vyL40l23DjXwptV227OHjwA" +
                "98NUgO32+D8/4iU2ZDHVjqM5tUhG3j2xbnBzEiyg5Vi2x9AJq9mAC66tlRLYRQWsOi+rUoMA9hC95lTh" +
                "SNVTkls8/4Jpg4DgqpDvZWSOgyBxSUWYRMWowBajZLJYCucTmLMsIIDeIQrg3RAFZ3/8988/4xaoejmV" +
                "Cu32Z3wsI13+7ZsMts1bzGGEfeoNfPlz9UxbMGwaKjveLv2jP/EbzBidZX/87NFDeoTWDTYo0cyVFqD2" +
                "t+DMD16jhYIL0QE0+cVf167oKvxO6dPWbY6VoIG07ypWe5u1EMsNKFvvN0hpkyzfgWlNRluOgTKJNqmX" +
                "09iQngGy0igTWDhzNQEAGAp8VOnEiWw4n07gf9MRFWp/nn3x6ntQY/z78vWzizcXoFr48ekP3zx/eX7x" +
                "BkS5vHj18uLxZ8rtKp9Iy+CcpBVbaSoSSlC0XnOysWkMj8cWoY7BGlKQaYek2RkH/qiIDVOJWl6IbRFd" +
                "71VSHcc+x6zcRkKa+BUWTlNl7+H7SfYDx3J/TOdspNK5svWyDcHGoQzyVFgc6y2mEbez78EiiU8/BFzj" +
                "04+oxZMpMf5lVhQHxG1HsQl/61BDPRGhQqJYigJNUXY+Fl4yBU17+zp78+T8+d8v0UJKxtRNJpi4wXyo" +
                "gLHCpEPhByquUPOQIvEy1I+ZAYOD68G4qqIHd/bs4vlXz66yE4QtD+O4Jk7dJhiPa1r13CvlhewEeWHM" +
                "46Gc03F4dTIOPyTj3DYKVlso7nj7xDk5POZTV3MwQD9B/2jnD3kSo/5lQy4wl7u25SbSEOEU+6OzyfVP" +
                "E8lxfCxIHQ04UfAXSGqweLRHI6fuNY6IwYZ3I+L2nQByPpu9JBQao8PYF+0Wmgf8nZPMiO0kuDnNRlzN" +
                "EpJvSUg2aXdfCyzrELnshaTSJLmRSuPecj8Qgr17FYSupSqeZKroTqKAAO+ama8pTb0EC+LPiYSVilCq" +
                "h6Pa1VB5A2vENBcYO/7tuxGOcSUAKKcgsEYiPCRwpz3U97q2WgxIszmAc0q1cqd7QpUu4wDKdFnHPk6K" +
                "j0G9fcTztO9nFAe8l9mSX34w88uZUTsR/z76/yFIYN5nH2P47+Ms/wX+U2SPM/KoTXb2GAjcLt6evsOI" +
                "Ynj8FB/z8PgQH4vw+OhdSDW8/ewdvbsrBHwghjeIax1Mhg26KKFxtf4/ad4qYSg5nFT8s0SJeV8piA6M" +
                "+HaS1HfDQ6+2+x3ukuu35jKFdyFmnozFqsy+xzN/fKCL7NAQFunXh4dUJyaJG3Rd+jlLkhGDVU5h6aMD" +
                "lRV+v7QC673iy96y9osuik7DD6D7ZxgDmlHB6/0EYeMpi9tLo1XMpkcDkDeTMxqK8PQkiAZpwvEXOSRD" +
                "OXI92hDi+VQUrxlgLh9gYo9zDKzQOxzyWvah1043p9/0VdS9vdaJTu53+Lb04v322t+E1/3md79hA4xw" +
                "hIYfDqj0kKuac4CCwtuaEiEXTPEdRYyqPfosffXoExrYb8MQn8BXpOs6t7M5EP52Eof/OPlm5rCAd8Fu" +
                "GFYkDVseeE/QKY4pCYp4mCTma+JOZCeFrV1Lyh9z4uB1alUnxze4ADQl3+xpBVYd2Qm/2MaRf+fBpfI+" +
                "FoSOh1mSu9/ufdq+lU1xz4qB0g/bEZe6X3PBDi1XK1M2cIhNsqAwbHco0ckhocUCs38nQoQEhQoWxlFU" +
                "m2ZpW1EJLmm3tSSU0wMQt5T4M4gZgZjxkDoBOY9x68yzkeYcvuWWsdGMD+r976OueyCvPlJihMcoQmFD" +
                "H50zaiS9629NNO0bxrorTgO8JxQj7NAPGv/WvNQoHPZE9Z0WjA2cWgwDgur/zYms5zFzxOe5FdpEUlkh" +
                "DRGMCMyfFCF1HmJY4TTQErZ7gtbE/hL7SbLbFyVDf3BBaUbt7gnloOL8PaKoX/zzYXEkuZpeL4lKJJIq" +
                "7gwRViwhGt1WmNR3v/+Hok8r+SgQ8gm5V5ltGhQZqrqSfGY8Jjmn45t29n6GHWeh8X6L3Qdb/DJs8a8o" +
                "xw6ZaSEHH9Ybz91RdWZJ1JrYclzgW5Q+56Qj65vxXqVIuHOEqJ/aUw1iLzZnAuAdx9G2nBIrN5Qs5USR" +
                "wyNYgbC5TBoBv5DJkXCJ88PREBT4vtC0a9Q/ZgIOWSk358pnlsGxO04Dob+ETT9LAkKKHwLw8tUVAOey" +
                "rzXMAA/rJQdDYbUt03WoUwwzD0XKijo8UN4w2LINUBNrQE/uxPJYxMVNabdJmJixAkS9bwyRhD6hWirA" +
                "gZlXOyloHeuBbiJ832HlbteQuJwGtu/FVGkbSYPFqw2URtA7KfmqgWicsjHCQFJ5ngL8s3qWepD4RqPW" +
                "w5Z0hitf4ZFgPeEZdoi1kGJJz3n2LxsIImzCtcqcUTugOi7lbp1gTHkSGV9q6QGjWymzdVvTSDmE7mmY" +
                "MhO9GZJYQllWTsc0DqjHxKKNNd5sQBmJQVk8nVbkNp9xgwmffjUcmQRhRhaGH9K23LaAvkq22QGOyiJw" +
                "KJ+Hlm011RYLCKDhI6JrhykIPjGPE555vXgj7Gc43NvnRKbdSCKaHuUNAhtJBoznOwfKCwBEtS9oJ9HA" +
                "NVrxTpp4SCG0C3liPjGK1ddtb5NQZgGO44FSRQYB22ixTec7OhhOp9eaznLNTVJQs89zGCjghC5ooBux" +
                "uUuACni8ndzkCIiS28UNSQ7XLVeRntCU2OO4ySEppjyBBTGZX/MVPHObmy5y1QHLgUY5WFElMobxrfeK" +
                "jI5SXsK2JJkE0jWQEh4Z5KodQ9Q6icSSGz7nujd1QR+W44CZvDJUqIJ1gzTCqRiTnIuh8diuSA66ycla" +
                "DE3TJ5HEIi9rTPRXurJkdFq9CuhAGHpYeXFQAChRMwuRGz6sXFcloRZbnDPztqGCGkQ2fpnQLANTaoXp" +
                "qWjcrZP5sAUMqMcy0w35sdaPFSImYSrbYumIKjsZ+TD41+WDhzxCCh3mhVVRLELH01RYhDOFtGIsLZLF" +
                "ihpMNmZPDsjdQEGLGIEpAtxtuDDcNvAaK0lPJzQ/Pkh9Gqp22739T1RzMbiNAdrNOPIf+Fx7kRb04NvA" +
                "Iqsdmz5pD+L4Cn2f/hI1kbCv51U57NGQEhdapFL54eqaqn45jTlULqktIIqNyS4hOlwL+lXhTg7SkQFr" +
                "YnQM0EOlYHppRg9V1D3FVTrdVNi6UBU9YBi1UDSyEYyf77PH2cNJ9gP8+XSS/UhpCUluX7y8fPVm9uPj" +
                "wYuYapcX34fCBhGYtE9h7H8Z4/5WRUIIwGeV43rZxfCoDBOj3uc3cDIJAOuiu5l/6pwcvOpISY/OagFF" +
                "9W5QciEunpwnjLmOwS1S9xyBHtzIFotoFovezJU9+rWk2Yk6zeNwt9slfQiXBlE7OeB+xKWnQNoLugog" +
                "3ndDxoqkZ/nAUWPbrqnpoBGngejOCb5KSUQHFcR4FnnBXZK7kaQTcnjGx+yx6wxLXH7HPKgsQFf8b9BP" +
                "oNbLwY0wZL1JtVN2Qgc4mOU9OJzo+4DH5K1di2Sictua7KW0EhNh3hgQ7bhUKssUKyCpIObF4HgzHi9d" +
                "TpCxHCq++ZXFxKWwPrxlMZzjUS0Sr7fSK6j+rKMnAp7KeMHtOZ1o7Ci5yChWFBHWR0fM+sWuBis/p6q+" +
                "elEu8dAD2/HJUnsXXz1nnUDqYpG2Iu0uG5nipONztYLSeHWNb8Pi9do3LOQfZrQiFU7jmqm3uG94k5uX" +
                "q1MzuSdPx5+Qa0I0QXVmDZ0u5hppJOnaWjoZC2AD9qanrMwPrwx0uKykj1/EyU1pDiFUkdDToN4s7Cww" +
                "ywwXNIrro8mBKe44iUpZPfL8Co5XhI5aNx5u8ZMCQXX3CBDOhS8/o22LLNpYypEGc4vY0taIbL5DDY33" +
                "rs5Z1qD5y2xA1fgh8rdPpfydCF5JJxOy4k97V6kNr1ADr86U5K2q5XQI5sH702SEAjT27j5F+aXchKJ3" +
                "3PVFdj8li2gFozq53Ca9pZFe929pTC/OS+9T69/hglkfGYdhUCONtCYOqZyDRb4ruk2lN8O0i+zkkGcX" +
                "00qUjrrtLLFYF6BN6ZDfjC+yTY4Xj45o2cr28WQPH6AfyTV04VDTCz5XrzfixdttpD0yM8CjQMAGl1cv" +
                "/egbeHrNDzATCjXLt157vErNcmu8WM9qW3rfu65Hqokn+/f2TDJ7I0a9A6NibTYoYtJlbchNpZJddFld" +
                "RW5TPLzECF4L/7Eh0zviv56O+AjDU+yLWXYuw2ZQmmbuDdk3Cb5zDd9lWhX3dBnQfdyu8+sUqO5bPGGc" +
                "XjlBTvqepctG7TRcvnykxL/XMl+VlRI3NhxmcOItSHoPMoKBVn8x2Qps7ccfScn9trwup43zU9csH7SL" +
                "j/7aLv7ywPwVSDm/BkB0HcKltXQwuHB5tw6hmIUE3VIzZi8JJJKgP93sKAmAxsN+1Ijfjq7i5RzhvP19" +
                "HC8+yPwi/7R4BjDQ7GJxEVsU1C7UeVETqfOS3kjvN2WBBYj4tYydbxVF4Gb/3Bmszfe7NWdp5XK9fsVT" +
                "OtpwBRf4LcyIa6cO5EyHDEwyD/bcVosJJfYr74IISS0PRgbbH+FujYihaWJQ7K9TTfCfO9uUiQ1WkgZn" +
                "DJ/U4ILXFB5Qzx5NaD5DJBi8debRNg1ZiP3RE0NPA3ImVILzUmASdDUYTWPvclEiDAowkRUHSm+VQQc6" +
                "xv2QAlS92VJzKUVnLEoqQa4CpymJrcQH1uvsycvz9PxaIDQBMEtJAGXf3ifd+fvnIaJAtPX7V6zFfQAF" +
                "l1+LX8WGX6Fr0Md7mHaiskdHZOqo33vbzQCq7uPtjenJ/lAkpXr/fpZAdsRvXYBc5frrCxBj5O6nn9gY" +
                "Wo0UDurhUjgcv9Uyjh2bMvvHwMOtucLu1KYIZ9Vd8+arL57Ih7v+P9sI4zE60fkIv5bh1zz8MvfuLZBt" +
                "xnZh37AcXhJBAaoDVzteJZYnSc70qpho7kf4eDhvJD1k5/nhOxB2FNCTj3d2TvdXxqaw5KNzMpvR8wOj" +
                "iy93AM1CgQto31h7uNYlzQU7ancwSSXXaLH9cyDVJrHz4JfysQUBiLXLhxbwz0DafxtZ5AFQNRmGtkHn" +
                "S9cTOoeGx1YeuDzvNqBkx6gwyGWjN6bOd4qKk+m8fTBFY6Cs9EZNBkQHniuDAe9oXjpNSEr3vuR4Y/Uq" +
                "dnLp1+NQzIFZSbyqXbrVrohF4Zw87TSkL8vwYM6AZP0l5Cy5K1+Zy1ez0cmw6YovJTEcFNk2ZauE4zFW" +
                "8TmqceSW0X8BG4BOFVNoAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
