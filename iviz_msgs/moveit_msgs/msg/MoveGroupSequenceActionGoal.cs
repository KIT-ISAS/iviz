/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceActionGoal")]
    public sealed class MoveGroupSequenceActionGoal : IDeserializable<MoveGroupSequenceActionGoal>, IActionGoal<MoveGroupSequenceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public MoveGroupSequenceGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new MoveGroupSequenceGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, MoveGroupSequenceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new MoveGroupSequenceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionGoal(ref b);
        }
        
        MoveGroupSequenceActionGoal IDeserializable<MoveGroupSequenceActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionGoal(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "33db6638fb44f932dc55788fa9d72325";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a28bR5LfCeg/DGLgJG4YWrGziz3tagHHUmIH8WMtbV6GQTRnmuSshtPMPEQxi/vv" +
                "V8/unuHITg5r7h32HCPSzHRXV1dX17vbz6zJbJWs6MfIpE3uyiKfz9b1sn74tTPF84tkCT9meTZ64W7t" +
                "15VrN1f259aWqcXv9HV0/k/+M3px9fVZUjcZI/KM0XuQXDWmzEyVJWvbmMw0Jlk4wD5frmz1WWFvbQGd" +
                "zHpjs4S+NruNrafQ8XqV1wn8XdrSVqYodklbQ6PGJalbr9syT01jkyZf205/6JmXiUk2pmrytC1MBe1d" +
                "leUlNl9UZm0ROvythSbJ84szaFPWNm2bHBDaAYS0sqbOyyV8TEZtXjaPH2GH0YPrrfsMHu0S1sAPnjQr" +
                "0yCy9m5T2RrxNPUZjPE7ntwUYANxLIyS1ckJvZvBYz1OYBBAwW5cukpOAPPXu2blSgBok1tT5WZeWASc" +
                "AgUA6jF2Oh5HkEsCXZrSKXiGGMb4NWBLDxfn9NkK1qzA2dftEggIDTeVu80zaDrfEZC0yG3ZJMB4lal2" +
                "I+zFQ44efIU0hkbQi1YEfpq6dmkOC5Al27xZjeqmQui0GsinH4kbBzcHsZYgm9Qr1xYZPLjK0rxoIrCW" +
                "21UOC0KTwO2SbE2dVMgwNUwCGeg5rTexJJDElDIYLHJ1C6yxXdkyyZsEJmprZFrgC7veNAkQHHojzJq5" +
                "ZmthaA86mdsF4mKS1FaNgZVDjGL6Cv55pmsC5AX0djiIp3OysDabm/QGMMugBzBlWzSwB+vaLC0tQlJv" +
                "bJov8pQnKBjUU4GOG4QbAFLrtm4AswR2HbSa6vrhyn2kpVuD5MobXrdBKTYC4YYE13dvGH+dByD2ujBl" +
                "CWi+2mA7YGF5njl+cRDMB1AEyn6bA6ZuAW2JZxQzRZ7XxySwSctsVpksb2sSm9akq+kRch9vZVjWDQou" +
                "hIXPBAi4D5hMXtW42PAEi2fvUMDRzgdmdqX1EhBAdjF93tj123fAv3ZdHx1mifvDC5vjjDxZkAS0JREx" +
                "pgPtQj/1mIa+cW2Xa5RVQiOd9ASpkC9wk9IWNEntirwBaSbL4qmCjBSx19EIR752vDxA2WZrbRnUCVFt" +
                "QmPJ+q5hd8IC1Gvn4G0GUgMRhS2eV6yh/IA8aQYs6y5oY1tYOR2NZgZ7m76V9q4hrQgvJih6YDqnsNkN" +
                "cH0peMKIAH9RONP84YsOZx1wgSNKqoZHeZkD1UB3objjxczsIi9zoh0uo/GLCjIu0BXJ5TleSOHaZtM2" +
                "uKgqHXm5XhvUNY2tamEM2Beuuqk3JrUsh+P9I2oBW4AtUAOYo9H32jwC5UHMNv4ljwdmD9gfqEEbFMft" +
                "BlQh2DXJ84UX7H93YEfUOtacSIBDVTZDCIDSxtU5yy7kG8Ta8L5P26oili4tcxps89CYISIMZDrbJMgQ" +
                "R6M3bu6aK0KnRuxmhJpw84oA1DlKB9IH9DEQa+0ysNRQj+GGhLfT5BKEUWILK5sLwWBLU1XA7rSCpJpE" +
                "uyyRwWkceoHcm65ysP8I0VyYHNBvKkN04YWPzDiGAfABf9PkNWqmo9HT0AUkFhm+ERSe3UsnywA0NeUO" +
                "ZgrfQEg6WB9adgPiNmfSAttkbYo6L8hmFqG3uSvI5CRyx6ie8G7cbApR9ih/aRBYndI1yd9Rf4KO5nfj" +
                "DtY0+j7O1z1yiLgHLY5cBW//blOwWnasm5kgu6PRtf8QDxGaDw9UosUmgibWSchzDo0cYlwQJdQQd62a" +
                "BhNiVVxuQwZGt7OqHjSTjtRqkI9g9w0jsERVjw+yQQBeZI0paPjVbcAxQHJ40NR1hsAi0O16Du0ROJrr" +
                "AQppDscbbw1CA+y65GrlqgYFDeiCVmWMzqICK73CZkcjdggA9MxbFaZp0MCLqLo2d/m6XSdm7VrRP/na" +
                "DhEZuaco3Jb9G91iZLiLVT8OsltahpERqkgdsOfJsDa42LyH2NohBwokS5vmwvSKHVE5AbcHLMcG+RdE" +
                "KS1qmsLmRgIDx0yT5IkgeGuKFlvBNgT0Tk4nn7+Dr9f3QdwNwYtMS9l2FQonkTIIeo2MjvtJPI7cVmTt" +
                "ABeDi4bwZI4wNrAm6BkUkNgTNABKUYSaV4htjqq0XFp0hNa4gqZsit2EGIFkT5kWLZrRc0sC25KaOZ2e" +
                "jlmJ80DE9dZrIGV4ogYu7efTUwYGOoCpfZJP7XRyH1UAYvdLTJ9xpKqh1Ux7zWpe4Bnj1G0UA9hreBAl" +
                "P6AiVc2r5+HVvCFqAuGC5kQxsWhBN9Cug88k6toNe62wIWHznIBV7O7GyDYqFJR5upsI8RKDiuMIAhz8" +
                "wR17Z95ixF2ydvO8sKRuyORSbcagwVLc2qKY8i67IN3GvFGJ4KrsApQsmn+qMQFNmG1VkknwLA7aRBIi" +
                "BxECHMDtVABCzwm7AMLxynSMTK2mBShs2AlL64B8INlpCb4jMf8YIc8Y6r48+gijAfvpaB+JzwYH1uUF" +
                "yQzIW9QWBrYY71eQoeBzJ8SQ08Q7C8QAaBvTRH1PVGJ5ZcmJJzulIkd8gqZk5iwa0mizrs0N2lToc6Ge" +
                "B5UPAg71a1kXLNrgNXQ5sdPldMKMRq3Y4kfXH+NZ4HVX+RIkE/WEgda+s0lkciB9Fo9Y+RDOPBisDPrz" +
                "rhFBgdJv51qwLmAO8EslYTRSbIoXOX2NcxPcBQKiS9DXJIN0l8IGbYBVpyMvYO78bzv/2y8HECnBaL1X" +
                "kuRloJ+Zg/jvcm+DawiSmK27YFGj51erjYHuA8jLG9iquLiu4tG/wa9sMlPD2GT+xoldBkYlOngrc+vV" +
                "lk0uXn3FlqpXdGyEx9BfYGNoGI1C/WeZW8z2xnvSNGAxA6DUFUVe42zdHK060GtGv8Gy17CsNJfERaoV" +
                "7AcF8FT7v6LuYDVr95kHPRPQPPRXhVkCmTMMuSIfA1eLP4JKMQWGDrYUKUDYUw3FQGlXLRZ78oWQZKeG" +
                "+seCeu3qBmOwqqs5YKwRSXIxdLpzl6HFcqIIQUM0tzFGXFhTDTXGkWDtjXDZ2RnILXt2FjlHEoEjl42i" +
                "XBKPaSLuA3LOnUM3Zobz+2hSb5gZI2IZvx+IEVeuyGovAcCoSat8zqqKPSCaumheEDLgVdNGqhzFgXk3" +
                "oLAMUVLuhJEnMVROKovKGN9X4C7lNe69dIzYsNeHJhsGv5PfdfacKhyFYjIygsaT0DRYR/2mD2tq/LAe" +
                "014NXewCVqvxAXh2wrxZKQBerrH3y/GUJnYZ5oJ2AcU0keEyUAO8V8HkROGAjoQQgjW33871YLBYxxPf" +
                "WSK7wGAZ+hZgQO+14WwFxuJ0GUmM8FbSrolEJ2qKq+eUwBiQhnVes+1ighgiZxycUBxBA4hk5HQpTMiQ" +
                "jowddLSiUN1RJ2kPc0cOU6dimnyPGg6VHSsfkac0i9IBQFmfXgYAYa0npLhScHZgs97aeDk5iID+1E64" +
                "EanHs+G1jeYuEZSVwiA61fkvIPhhyhj5JDjRrqFIERmCHDf3PBAC6IE45Jsq0mg8oZWABiqtIOjIrnU3" +
                "EmcU5Crxj2hLeNSNEL1Sho9eMQUOIVD29Q/M6o0aREb3LMsA5B5ZXKKFZ7HMLsHMIsZDeytzsK7onjm0" +
                "xVViA0natGkrkiVhPGZktsuA9GDgZ7yTje5PTOXVO7BG1kx5jHdnHGMJthP3Wdom7H90OH185waEEgmp" +
                "JF2ByTBNvsKtcGfWgP8Eg0zgCZhKhYUhDvvbm4uvSKY9RlV+AjYyuIw7s8UcGQcbwR7mj8jBFF8Pub8Y" +
                "OyMxYOAKgMJ9cUd3vpNpiS0UGmzoW1tRRgjTODDhDg7/L8YOK8a26NytfrUY0+b/l8TYfVKMLVHsXvfc" +
                "v2tlYWjl2Xmv0RYWFBvgz96374lM8JHpdRjf0WM94D3SdvBixSdbtm6viKDuOZijkTrDkTM4+mtrMB6E" +
                "AkBdtsNMMgw85CPDXq1yL+M7E8GnnwPWSIcPuoH62/ZAC0icJNNSqVsH47E7n3nlbjAXX5JjXqNrhK4B" +
                "ymFTLimzQFGfqV9AaRKepd1hZsd7YmDVYCl4ecLkJqDPMX7WkMXboKD6lVMkYOGRXYFDxAjv8UJFZvfe" +
                "+tC7d+IkM77I79Rd4XBVTrO+8ZkAfAiJACJlJ8NowBrBLNfKbDjkTfGtkO/qIQIwWJnFnja240EfPF9o" +
                "/QetGU6YgJZOnPRpnkkEecLZefWvHwxBVGebdbifS5SJwBEo+9WllxNsNeQnnh6FNkKa0w+pAQSMVESx" +
                "08a16Yryhzsf6P7MIzfTJJApKtAVuyhvGXU4CgqEwM0kvEJZ+5AEZczIxAArLGXPrZP2g3Uhw9Hn8xlp" +
                "Dt6tEYafDsKWwBuIOMyhgzHN0cC0cJx0r1xL20LASIxfByltitq42tFwlS24CkyjwTI0LiRWDGlQJ0qv" +
                "BU89ZOKAJIjgTAYJq7O1+XLlbZjeqkwwKXpTum0Zwvzc4SCx/P39+URMgwlXPS0oxirBHrXzaRPdG+6G" +
                "PSBzFUKeECsROFhHrPN5jpEqX14Uuuqi7zaWOQRjHHPDpS9EpbCl+JcZ2p1LriLgKfFMrhEEwonzrBpR" +
                "E1FMabB9U0/3svTLKxEauH2GIuhx8C9Q4sphSkpHSjGEss4xq4XBR5RHjC01e63f0H2M2vWDt3WnwUyW" +
                "AUd7YetVDzK+guZr+TIICz/GYL7EbaPZWwy5WTQSROCFbHAyb0MlhVVfho2NUPgC65dlOZveRMJxBz+s" +
                "EqH50FD3TRY/xhg+ybI65i1ZA5/loTgjhcujRsC7t7lra7CZ7V2OBXgU8GfFS/IIFnu+A6PvycXF+SmP" +
                "9Iakb2ewReXWHL8ob/PKlVQTgX5Yheb4iQVXbgfCi3YJxYsb2Ol1j0nybCyDvbl88eq7y/PPZWabDcoy" +
                "9HlLPzvykkUAE+q1r2B474w1l8GddLawHtFUX7++fHlx/sgL6zDs8Ig00ASE51Y2hKw75VVOKKcvS6i+" +
                "jxYUFnbRsF8zljKQ2hVIMqCwypQgdDNb51QdQ2gSiR4zkq82mllmNQ2PaLn6tk6/fzTZ+WGhM3rwm/8k" +
                "r7785vLpNRYh//bO8ocJ9PT92RKSq+RzL0g/iqADKYdRDvR/asteeZSubdySw+7e7+T4L/ALRdl7tsiN" +
                "9XHdeJAzesMgQvCiUt5agjwrk2zutQKACTCzeYyQKGUKv3xz9erlQyzskJjMj09efJswiGnyxDM0SGK/" +
                "I6LcHkpzpU2IO4kloKpnmlySrZGXA6tPO4uCAs7dgJlzY8+ST/5xjIQ+Pjt+iibRxZfHk+S4cq6BN6um" +
                "2Zw9fAgujCmA6M3xf30ik6zI2Codh4RKEZu8imIV4SJFdEDLM2+OoRNW1sGOuLFWatQXBWzdeV6AnzTt" +
                "6tYO66ZUg0zlW5KuvPiSmYSgpFSUaTQKxtEUYjMpTJPgWn1G6SPAUSZMzwlBOks8FfglEgJe9glx9vv/" +
                "/OMX0gQVNadooeE+2sc62tVfv01g/WqL+RC/Xt3Br34unmkTAU/DJcfbZf34D/IKc1Bnye+/ePyIn6FD" +
                "hU1ytJa1DZgKW1dl/fdo3OCEdBRNqsnntcvaAhtQerZxm2PP48juHysAfJ+FEQobqCKg3iDnTZJ0ByY6" +
                "WX0pRt8khKV+U2V9zgfYTENXYBjN1V4AYKgQUP/T3mT7+3QC/01HdLLij8mXr344/1x+v3r97PLN5fkj" +
                "eXz647fPX15cvjl/rC9evbw8/0Kr4FVukRZCnKQVvh9poywHdVxr1jc0DTH30EL7YIEAoh93iJqdcTSR" +
                "iukwRanljtgWyXWn4us49Dlm5TcSHsWvMHFClZ2QHybJjxwg/inG2cjRhMKWy8ZHMDtSCcN4dBIg1HRM" +
                "A21nP5yfRk8/elrj009A6hglpr9gRcFFXHYUpPCz9IceJiJkSD5LdaLWK4uzxBw07azr7M2Ti+d/uwJ8" +
                "4jF1kQkmLjCfAmKqMOtQTIPKN9SWpPC+DPVTYsAg4TI0rtvowJ09u3z+9bPr5ARhy8M4zIkzwhHFw5xW" +
                "HQ9N90JygnthzOOh1NNxeHYyDj9E49w3CtZzdGq91a8ZHvOpKzm4oJ+gf/AN+ntybn2ZOZffNvkm8BDR" +
                "FPujx8rVVhNJnHwqRB31dqLQz7NUb/LAXNFO3WscCIMNP46I2/cXyHut9jJbaKz2A2q0Wmgw8HfOXCO1" +
                "o4jpNBlxvYzP6EVx3qjdoSaYlz4c2glyxZl3I1XPnel+IK778VUQuqKqeCJU0ftEAVHIORnQiqZcgj3x" +
                "p0jCSiEqVd5R2ayv7YE5Yu4MjJ/67bsRjnEtAChRIbBGIjwkFKg91EO7sVp6SNgM0Jzyt9zpQKTSaQyQ" +
                "TKd1XAek+Nzi28eMp72bUVjxINiSDz+YTuZ0q51IMCAEC3xEwdwln2Ik8dMk/QX+lyXnyemIyszPzoHB" +
                "7eLt6TsMTvrHz/Ex9Y+P8DHzj4/f+fzF2y/e0buPRYAPBAJ7KbbBDFuvizIanxz4F+GtEoYyztHRA5Yo" +
                "IZksVdh+I76dRHXl8NApKX+Hq+S6rbn24Z2PwkdjsSrjM2x8ApPsUB886Zal+/wpZp4r9GS6iVCSEb1Z" +
                "TmHqo4FyjXq/XgOmE73sTGu/kiNrNTQBun+GkaIZFdUeJorrT3u8pxK7uOc4YnRWhJxFpHh8JkWjOP44" +
                "jhzaocy7Hq3wuQEqxUfXXZwczc7QMgRE/X7onVR5LcvRaalr1G/8KijhTvtIOfe7fJfX4hh3etz6190O" +
                "B1i8HmE4isMPA+rdJ8PmHL6gKLmmWcgd85QP8oYjKewytKUajHoyC+3tt36Uz+ArsnmZ2tkc9sF2EjD4" +
                "NPpm5jCHdyFpoY3Cm17boQ80gIQ/Je0RDraEXFBYkuQks6VryCDA5Dt4olpByjEQrjaNWTp5WoClR7bD" +
                "L7ZycmASLIM6FJ+O+8mXA6z7Pqffu3dx5bKeJeAXJcyVDi7ck5ThQmnKOvZJSqYVxvnuSaty9GixwCzj" +
                "iTAlAaIKiXEQ46Za2kbUhYvabS0J7Pgkxn1HDBjGjGDMeMyAgxwOuRf/5GikCYzvuGloNeNzhf8bee0g" +
                "UqZLmBAFMkpVWNjHF0oeSSjX9+aw9s1nXRyn4eETCiy26C2Nf33KKzqmino+Llfreb8YOmww9P/rs2TP" +
                "Q0qKL2tQgBNJk/mUhjc4MB2T+ay9j3f5c0pLWHg6Yz4w0V4G7j1Tk9E/PK04YXcArhlUsL9FTHVLkH6d" +
                "qJL8T6enhDIiKRaWiPgsFDP1KTfstP9zJKNWFlIM5TPyzBJbVShLVMdFedPocOecTp7a2d0Me85864Em" +
                "uw83+WWvyb+plBuy7nz23884HBOkotGc2DcyAbnuOMvrlNOarJTGezUrfM2QP9RCHag2shPeMx7yjkNx" +
                "W8625RtKyXL2yeE5Mc/mXL5NkF8IeiR0AoY4HMIC/xnatpX62MzMPtfl5lySzRI6dEc8pnxmvuGEyXWP" +
                "KwjEy1fXAJ4r0taAAx4rjE60woQbZvFwX4Uif+QLqJV+eEa+Yrh548FGhoOeLAqlu0iP29xuo3sHhDTA" +
                "3Pv2E0nvE6ryAjqYebGTatuxnk6nDVC3WFbcViRGp5Eg6ERnaTVJy4WrG5RX0M3J+RaFyKpl00Vv24il" +
                "fQzzT+qm6lnoWw2B91vSebN0hYea9WhqWCnWU0osPZ/avUnBC7YJl1Nztm5Is1zJ3Vre/KpFhnylFQ9M" +
                "d+XTxm1NJYUYuroebd4Cps9vMZdZOcRTOeAkE2pG1nhvA+U4etX7dMKS23zBDSZyeNdwsBPkG5kjdZ/V" +
                "5T4J9HeSzQ4IlWdhy/K5bllgU2yxaAFaPiYud5X1dwAgzrM6XDTiV9afUO5uTublwC8+C8tLBVaVDBpO" +
                "pvb0G0AI9oFQn+QFl49F9+KEIxW+oc9I82FXrBVvOouFogxIHc7CepIQtI3W/LR1S6fc6bxd1Vou/Ynq" +
                "evZ3IWWIOXUM6ulW7PUcwCI57+c9ObMSeO/yliSKa5erwFxod+ztwsmQfNM9gkU5Sb02vHvmNjVt2GcD" +
                "NgYNM1jnJaKHqa73qNAlBYFVsTGJLAF1A2yFRx25dsgQ704C16SGT+ruIa9ExJIgsLBXhgplsL6RhjgV" +
                "C5STPTQgWx/R8Tw5HIyxb/okQlolaYmlBYVOLhqfCKCy23OIHrheDEoEz9+8o8iv71fcqwZR+y5gzZvd" +
                "UD0PEhy/TBhPv0m1KPZUdPLWCUZsNgP5sTB2Q26xrccKEhM9hW2wbEWVoQx9D/zX+cNHPEQMHjDD6iyW" +
                "rONpLD38aUiaM9U3yXxFTUarsycW5HYkr2GMABXB7jZc024reE11r6cTwpBPhJ/6YuNmjwsi3Z3175mA" +
                "hjNqqMKMbpmSjqQna/CMYKbFjk2kuA9JgAI9p+40BeCDAWNA1cYeMymXofkqNSeuLKlYGV7z/RBdvRMb" +
                "DKL2mAEj7sPZoF/mbx0hFepJp6ZJj0hUlqa3gnQJRgC6FItxjmWw80Xdve2jpoxGS4KZ9ENynjyaJD/C" +
                "j88nyU+YBznSdPrly6tXb2Y/nfff/IhVg503P2AlH78RSUpL5hH4t/IJ7tUyRAJ8Vgmv93n0D/4wa+rN" +
                "n72UDgFARXUIn2bwriflQTp5BqzVuUPK+Xh8dDoyJFn6t2kdOuDdu7Ex+P14hUCMvG6VbqlrcqJeN/CT" +
                "wrqiL/6uJGooZ/alupVYfEFXHIQrfsiikeywXANV2aatSjo9FV2lyDdJ9a5jREHoXS25GEo64Y5P5PYA" +
                "7DvDGpvfhAtVJujE/wM7CuRy2bsDhww9qbhKTuhQCkuAGlxWcpzA36qtXYu4orLgksyquEwUgd4akPp0" +
                "dSTWjKqlEFU7y5RwyBkP2Z2UF8Acnr59z5TCfERn3jMlTjaplgn3fOlNXH/y40fynyqOwWk6nWhUKrrH" +
                "KRQ3EfX58i+QBtmuBPcgpVrDcpEv6RQH2//RhHtXgD1nrUEKZRG3IzNAFjWmTMtnh5W44bqeuvEk0Fvx" +
                "8EBCP8EWuHIazZy6iweI19zVcvlyIrcJKgYT8muIQajwrSKLxXBVNzJ5aS0dAAa4nojTU1b6w5MDVa+T" +
                "6dIZ6XKbmyG6KiG6erY2CzvzG2iGc4rYS3YhWPCOU7uUaiTvMeMIiO+qBe/+skMpW/Quo1w4qlfC0fKF" +
                "nVtZSt16+4y2qi2R5nyzHBn9bZmyHEKjWTYGnSeIgov7bMstaAsoIyXKZfxt4I65/t1y4BuanBxfNbaG" +
                "wA5fLCeDZKDZdweV9ySjYUJ6C2BXrO8ljJHCYJJHN/x0Lrek9/3LLeMLBuO75rpX2FD+SUZjMNQqxHUj" +
                "/1ZOAeOGzNpNoXfjNIvkZMhHDDkuyo31/V1/KFnMEdC+dMZxxndkh9PVhAbNX0VCOL3ENwgcjeSqPn+E" +
                "6wXfLKAXB4ZbfrQD7XQASfGFDU6yXMJI38Lja34CfCjCLR97XfC2OSsd8A5Cq83pQ+/+IqmCnuxfZDRJ" +
                "7K24Bw4MkrXZoBiK57ch15fqjMkPdgX5YeGoFlN7LduTraDObQd4bzCfyXiKnbEmgOvIGVZIiHeG7dkT" +
                "37uKr4ctssPdkHSQC4fez5XqEoZD1/EtHBQA2DOX2TKe+rveH+iO2GuZrvJCGR4b9tNJ4W4ovXYdwUCr" +
                "P5tkBQb7+SdydmCb3+TTytVTVy0fNotP/tIs/vzQ/AU4O70BQHRDxJW1dFY6c2m79pGehYT3Yvtn72oE" +
                "ERBddJMHUcA1HHSkRvx2dB3uK/FXEBzixPWgNBC5qKU/QIFqF0qj2P5g4eDL1KgNl6lFZzqB6W/zDCso" +
                "8Xse+t8vn8B5/7k1eLqg3q05gyxXEHZrtjoD9qdxiR89Vlz+NZjM7W9mkoWw9LZYTKgCoahdECmxscJE" +
                "YZPFXzsSKDXtmCD701U7/ufWVnlku+Wk7pnWJyV49iWHHjRkgGY4n5ASSt6LfbBsQx5kf/zIRNTIn/E1" +
                "7TwdQIMuT2NE9m5nJS6hMBaZf6AYVwn0oBPujygM1sGXmh9JWT3TUjIZ8u8QEFJiYPGJ/jJ58vIiPqYX" +
                "8Z2AmHXYAaXh3jflgn/BriJ2RH+hexVdWA3QfemN+GhsMWZ+Fv75AIhHSn30gMwi9abfc4GCGgXh0svO" +
                "/Qe+1EutgwPNg2yN3zALuRX3A7MQo+UQVRTBENFqKn8sEefDqYCtlp3s2OAZPCDvLyIWGcDNMn+Y31Vv" +
                "vv7yiX752P/8jx+QiYoejP9t6X+b+9/Mwd0NMuHYhOwaoXuXalAgbPBezOvIUCWZGt+xEzyFMASeRDwa" +
                "SRdhAX74HqQgBQ/l48c7p/yewSkG+viC7Gz0IME445swQO1QTATaV9b2b4raT2c7ajeYK5MbyNhOGkj6" +
                "SdReocrhDAGIFdpDE9BZHZJo/2Nika9A1XAYScd/5oW7ntBpOzyc89ClabsB/TtGNULOHr0xZbpTUpxM" +
                "583DKVoKeWHH7BwwIBzjaWEwvh7MUKeJUeke/xM+eEpD77un0MB67OtNMDuK9+FLtxLc4ToKGGg3BCLT" +
                "qMHYASn7i0+dcle+ephvtaPzb9MVX+FiOMayrfJGGafGqMcfUbvDfhmN/hvoUnlY8mwAAA==";
                
    }
}
