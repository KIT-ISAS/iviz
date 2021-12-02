/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceGoal : IDeserializable<MoveGroupSequenceGoal>, IGoal<MoveGroupSequenceActionGoal>
    {
        // A list of motion commands - one for each section of the sequence
        [DataMember (Name = "request")] public MotionSequenceRequest Request;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// Constructor for empty message.
        public MoveGroupSequenceGoal()
        {
            Request = new MotionSequenceRequest();
            PlanningOptions = new PlanningOptions();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceGoal(MotionSequenceRequest Request, PlanningOptions PlanningOptions)
        {
            this.Request = Request;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// Constructor with buffer.
        internal MoveGroupSequenceGoal(ref Buffer b)
        {
            Request = new MotionSequenceRequest(ref b);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupSequenceGoal(ref b);
        
        MoveGroupSequenceGoal IDeserializable<MoveGroupSequenceGoal>.RosDeserialize(ref Buffer b) => new MoveGroupSequenceGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Request.RosSerialize(ref b);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Request is null) throw new System.NullReferenceException(nameof(Request));
            Request.RosValidate();
            if (PlanningOptions is null) throw new System.NullReferenceException(nameof(PlanningOptions));
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength => 0 + Request.RosMessageLength + PlanningOptions.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "12fc6281edcaf031de4783a58087ebf1";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1d+3Mbx5H+HX/FllV1JM80JEtOKsdEVyWLdKyU9Yio+BGVCrUEFuCGwC68u+DDV/e/" +
                "3/f1Y2Z2AVr2XcjcVc6pCrG7Mz3TPT39ntHoZd2VdXVa/Lgpqmnxln/bLmv072g0erPMq6qsFq/XbNdm" +
                "a3ue1Ppi9PTv/N/o5ekfj7JVfVmU3WTVLtqHO6c4epB9U2Km9Rxt+T3MzCefXZXdeZZnZ8uimk2afFZu" +
                "2mxeN1mRT8/H6P+iyrrzAs3bNRApCIrPAqdosny5tFf4dlXi6azIiutiuulywMzyNqurImttXuNRf54v" +
                "umL1/kNW4k87uncqcXSg+M7wCTQh/t152cq8hAhdhqeAd0q/0LYtFquiElqzoWN8SBKUc4DKrooGBMna" +
                "ell2eXNjS+IkIQ8lnDXixGpdF9C0uyqKKgBVgh3KQLawq/yGpG9XdY23s2zTcpZ5Ni2b6WaZN2E0xVfh" +
                "2nrblNkUS+aDCVY5WvFbVVx32bRerfDiMLs6JyqPslWRg9srmyYGHI/myzrvfvtFj6Hub2UTIgqisi7N" +
                "ZQmSTeuqy0tMl+jMinlZlUI4LmAelrOrE6ICROBzo0O96dYbcEOXrZv6spwVQO5B9iZv8lXRFY3uHTa8" +
                "qpuLdp1j4O4873p7pj2vN8uZtMgwIQAZfeetE0gBwmQdXnKwUzBPx8Vtu7wrss16hj/tOHsxz6ZFQxyz" +
                "v9Vl1bU+0Jkgz3GaYgYAmM66bgV7rD1mxhmD/YUHNk0jXFwVyl/Y1rGxAgQIclrRZWSD0dv6rO5OZS6Y" +
                "UdNNZF7Cv0S6btuSkmBR50udciTSqp4VS9Jcth/ejrMTyJ2sWBa2lwCFDfOmAX/LqqF7rsCaYkGOlmHk" +
                "Bfl1el4Wl4ImNp1ghIl3TS4E0bUGObtSN4WCAHjMPe/Kdl6i6/PYA7KJkCcJECL2qjbig5R5dQMk8QWy" +
                "sMaqyErnEKpYB/nd1LPNFDsykcAqKS/LekkgSuV0nvu699brZQl0QR+KWRkEi1LVXfa3DUV3fqPvDtIp" +
                "y+DDCZNE6QAm0jdLYSS8/Vsx7WrKJAJWUtyM3oX3KfzYetcoFTjVBUqqc8hkNaRSIYwKkSENuT/XxbQk" +
                "3Q/JmlzjHNMa9nXVAgCzEcbktO3bpJztGnzR1Js1H2wzANjVeQnmEto6XPys10UDhKuFw5WeE8IKcDer" +
                "MzQm5HLFFXEQohYg/kjdFQRDMRtnp+d1AyYHGevlxsSIT78p1vw4G48wpyePCXgSjIW8g1RfR1Ku8uty" +
                "tVll+aremGLB6LsoS2ZZLusrcFmymbJ9sGBbYJFmYBEXzNYwDkugIlem+ZLoz3Murm4WtV/Q5QZTBxsL" +
                "tyRTE9pml8WynkJEcGtWImGmU2xhUhUMMs6yZza5y3wJKSvbDVPbf3T4+Qd8fXcbwJsd4CK/+AZrKH9M" +
                "lBDyilzNnQPJd8OlAoFovoBny8sC4AxBjAxOhAKhAGRHyHYKSQItG861pIasFpjxfrniwuVVt7yhWCxb" +
                "CphqutxAA2BhRRxDIIL6j8aPDlQz6zjC4/rJNI/wt5CCa/r5+JHAgnhXQu+X42J8eBtFALD/JSXOQdS/" +
                "aDTxTpNWl3aiM+q1SbsP292H3t6h+VxzY6e1+SLR3LkQEjSLCpECYb6B6Ocuw1eRZ9j3JPUl9h/2yz6s" +
                "2/r6gOziAsCZpr9vOC21j2j6BdB1hUWBxZMYz9waq/qshFqjMhELyjSVAobNd1Usl2Puq2PRWsoRnDYb" +
                "N8UcqpOWnKtCTBGINtjN7ejrIoeuhnblnyANSggLrLo2cimHbodqwxuLO5/pPEQhi6ys22K0KGpQDXJb" +
                "KP+tCPEnBDxRoEPB8/cfCgznQ90Na7XdTIdUGqq5VM3yBvq36HKYSrmQ/LxcYNt/toStIFbJag0ekK/d" +
                "zRrWVMIGiwLzlZ1J5UOkaQdvqnIqypsKIe0vm7lvZUzruoF1zObCAYQu7GrW/IvjI9HO9JogojASREtT" +
                "5GLCvzjORhvVFugwevDuqv6M0mJBjeqDq5GJyRbXa6wO55m3RxjjXxW5MWAfuS7I9uXdBI/tQYZBMAXo" +
                "JewMKow3N905xKxsoLwpxYsDYEiGJaDusdPeQQKZ0z6C5q1qB68Q4xi/BGwV4BKnz2COzkQZtZsFCEht" +
                "q+a2yXQIBQh4SL9ledbAlxqJZpQhRw++kl0mOlpWhLu8bSEJsQAz4WDX9LIaYj/cDTfu3AXOWrAFsFRA" +
                "goLtUr6Rc+ZNAUwoEcdZcD1FBtHbkk0XetJeKhsqNrODIVfqBj4n/JNZDc0EWxEwVvkF7XW677QlYVZC" +
                "s9KOq9qlKlW8Rpf9YryA5hFZJ63UhQQE2QPlNGvKBZSi9MRAq9A5zww5aL75YzV3ZM46GBYMQJq6My1F" +
                "xXtTb2DBAgf8aGzriSXl8xIW6er6kPvOQPQJ+kYUoKsJaIgOmx4y1/Xbdfh1E379dA86LXpEt6qysor0" +
                "y89gefQlacc1hBmgHkR01hhHaN2ipU+KTXLRjri2daOD/4kf1R2TdtEdky+tigqGC87zy2AtFdnx66/U" +
                "Dwr2lXh3KeiXbIt2yRDSfTKr55PBYM+6Dr4YoEzr5bJsiWd9Rr8B4iD3b1jwFgsqWNBADzQ4GHn/5979" +
                "tfSGQ+a9oUns08Qgc9yvlvkC1J1ROpN9wczm4dIOm4KPo80uRhe2EsUCNDY303y+peJkhuomS/9ENazq" +
                "FgZhFoxDVS0uu8RxdVTP6hnt432fDxrSkaM2WRYQcTsaYyAseG6sdXQEzVkcHSXu9pnsdo0AcEDGmDj5" +
                "LmG5g9FZXdM1nhC5O1O7OxkwoVQetoCw33m9hKLwTQ8betqU3PoMnAlqgrhZe5ArUJSydxosOeijG4Dy" +
                "0SyW0IlhSzOM95uCFiDfN1AfZcvtNoUdiIHFJKMuoY6EKku3mds7DiWfidF9cBibRmt82PRhK40fQrFy" +
                "e8YuxRxr1QU9rb598GEMwKsVe7+CGU/EJBSiX2iNViWoQG6bQfLrDoUupDwQV1UJoTZj2MTq6YtyxLjR" +
                "/FWoFo2RuCS0CIwU+q9w1rbaqFHDSK4vowgP3UbelUPICor6xZiKxlAAtohIiyjLo/CR+A5iGxzBo89i" +
                "QfUpLJMRtZjGfGi6U8NJJ2sP3Mlh7r+Os++o1KjfVN+YCBUsqhoAbX0GhgJhrQ5FV03hVGOnEuu4nBqX" +
                "otd+Y9xI6ik2urYJ7haOU8o5ndryJ8h6oMy4ucBJdo1EHMX9wCgMnzgPhGkmxJHgh0+atjsNA5qZsoJQ" +
                "i0O/Qo0gSFThH1OQePSNkLxyhk9eKQXuQ6Bsqx1g9dZtINWhWHaVAeQeW1yhRWCxWbGAZSWMRxNrVmNd" +
                "GQyARKmvXFyDJJtpt2EwYJ7F8ZSR1RQD6eFVMhrM4J/vT1r87Q0MEPGaNFsC/hPTKJhL2mdRdHH/M7oR" +
                "woYXEEoipLLpOayEcfYVt8I1jNol8wjif0JVmLBgXqHK/vL2+CuRaU+owPfhoiFAcZNf0ZTWoDXscv1I" +
                "DpbsTHQR0tkpIfGnKQFF+3JH976LNckWDg0b+hJRZwqjfHpBhHtz+H8xdr9i7IpRhfNfLMa8+f8lMXab" +
                "FFMDlN3bQfjhnbMwWgV23mp0Bb3EBvw7+PadkAkflV734y6GWe9wGEWrB7ESMnZX9VasoR34lKORB2MS" +
                "/2/05w06NBUFgHtp94NkHHiXWwy53tDNV9HUQ4RPP8ZZkw4f9fz819U9LaBwkqHlUreNxmMfn7OmvsAa" +
                "gofpi7f0iegXUA4jBi1xJIk1jsMCWpP4bO3uBzvdEztWDUuhyxORO4Q+Z8yW8ogI0pz8ZSgKsPiorsB9" +
                "xKVv8T9NZg/ehvxO8OCsrGJeXru7otFSkIrOrgeg+NszTULHXpYaUfgNk6bnOUKSQibGVmP6dGtuao+n" +
                "7jWbyYAPEHIBPNlHXC7iKiCRilP3eYzIjuYqoP0ZvzGn+sEueO5iqxMS0EgSXRyAudQ+oRSoB5rNwZMg" +
                "RsyTh/E8YMCwRBKn7+qNMB4G9mzKZ2FiOg0J7COCOrtJUt9Jh6g1BNhEwyi08WISXWclzhEML2Zwh3Fd" +
                "rIbYiqEGRCesIboVYQRUANqiaxBqLL2A+awhv+kS5jbjbMg6ykYwKJZE8jGqYkr9ixQtR2uQs5HwsGUd" +
                "bGAuH2B67CbJ1EbPPCZ1QQzObmJD+KJcFYiNB4tlsBgIS82zi6q+Ct6Btb+PPbm9F5+ZGSAxwpmQJgR1" +
                "3KaXPbM7pwKONzSNgPvCPQILq/cSY79ALMo2a8wz+zojVaBMwUjGWa7lUUKdsHv074Q+8kIrThQXReEd" +
                "IRBMTNJ7tMykrSRVt60537PaDelKlQzcKbtSNGlQzwlwijqkWRhmyhDJChO8BLFE4ug8pdUb/0TvMDYb" +
                "RmPb3nfylZbHvCza8z5UvkHblX7YCYffIogvuTk86c84GnInZhWkJQTZmUVSpZn7KGpExKIorNhsJmsB" +
                "6cohDtK5sYZIEJGRbkGS3+Lsns3o6yWMoVQP+UKJG0rUO2kEHkUFyKaFHVxcw1Lg9BHBU2UqAmc8OruB" +
                "Hffs+PjpIw7zVoRqb6R5U9OthNFdXZZNXUnZDONDkBBIJSO91SDxoltBwr4dNrNCSBLpswMd6e3Jy9ff" +
                "njz9XHBarymn6ME6N5vPa4JVJm3uwcdw9WSEdnI8sQoRyTdvTl4dP31sQjiOuXs4GQW1MMWVcb4ttWRF" +
                "UD4Dt8rWzd0YqWxBi2Ux79RFoYsMaYYyDdIKpHWJEaUpIpOg5EynKLR5wgm+1pIRzX0AJh5pgHpDqyi5" +
                "Oxv640Jl9OBX/5e9/vJPJ8/fMeH46zvbfyTO85/PcYjQlOjfXBSeCTKIMQYq6MLAKmgHWf6uXmjYPLiO" +
                "GsIFnzBK3jMqLooQl01HOJI32j/GUCXlJuwCiVVlszMX9oDiAGdn6VRMwUrs5E+nr189ZOWPBVR+ePby" +
                "G8aQWAmZPQssDDEbNkCSi6OgdqrEoJEqdVcoKIgTq4HR161Fl30kDn1dX8BeuSiOsk/+Y48U3jvae07L" +
                "5vjLvcNsr0FRKN6cd9366OFDuB/5EtTu9v7zE0WR9ThSxynRnMoko66eWTdcnIQKtBzLbg+dWFuJXXBR" +
                "FJaFni+xVVEbARfHq0138CuTGUpEzy0ef6m8IUCIFfe9jaxxEDKX1SdaVExy3IySGbISzhcwR1kggLwj" +
                "CfBuSIKj3/zb777QFlS9mkpFu+0Z79lIp3/+BukLmAjMYYR16g18+uPya2+hsGWobO9q0T75rb5hxugo" +
                "+80XTx7LI1o3bADzub6yFlD7qAGdDV7TQiEiPoAnv/QryqA2S36X9GlXr/ecocHadxWrvc1aiMUvUjvS" +
                "rslph9n0Bqa1GG1gtyKzaJN7OWALT8+ArTzKBAvnzE0AAKPAp0qXnaiG86ND/A8hANZK/C778vX3UGP6" +
                "+/TN1ydvT6Ba9PH5D9+8eHV88hai3F68fnXy9Avf7S6fRMtwTtZKrTQXCchuwK2wnGxsGsPjsUWoqkH6" +
                "ntNPOyTNjjTwJyWVTCV6sSvbklzXLqn2Yp89VW6S9zSfEIjLVNV7+P4w+0FjuX9N50wii8NUVAsYi17/" +
                "PZBBdJsCfiD6ONJ28j0skvj0Q6A1n/5KLZ5MSelvs5I4IJedYhN/LbkF6anzhESjKLYSVa9PNzdHOcjn" +
                "oXAnb58dv/jLKS2kZExfZIHJBda6HqWKso6EH6S4ws1DicTbUH/NUATFGsFQVdGDO/n65MUfv36X7RO2" +
                "PRxEnDR1m1A84nTec698L2T73AsHOh7lnI+j2Nk4+pCMc9sorLbo1fa7c7J7TKhsDQb4J/SPdv5wTzLq" +
                "b6cKtPgaCZbIQ0JT9qezqdV4h5bj+NSI6pt0QMzAUgPkaY/GnbrVOBKGDe9GxG07AeJ8NltJKBqjw9iX" +
                "rBbNA/2uSWZSOwluIuyu1Swh+ZaEZJN294UgpuKhvV5IKk2SIxbiya+I7kdCsHevguhauuJJpkp3kgIC" +
                "3rVuPtSgVQtYEL9PJKzVJ0t1plRSh8ob4Mg0F4yd9v2HEcd4ZwAkp2CwOEASuPMe7nvB8rPSVJnNDppL" +
                "qlU73ROpHI0dJHO0YOWFSWkl4vsnOs/ieiJxwHuZrfjlOzO/mhnFTlP/Pvr/IUiQX2efMvz3aTb9Cf83" +
                "y55m4lHn2dFTMHgxf//oAyOK4fFzPk7D42M+zsLjkw8h1fD+iw/y7q4I8JEY3iCutTMZNujijKZnR/5B" +
                "83YJIzUuyfkTlSixfMXK88NGfH+YnDbAQ++kwQeuUt1vrWUKH0LMPBlLVZkeVmQcwuzQEBbpn1YIqU7W" +
                "uiBp0Q5yliIjBliOgfpoR2VFu11awXqv+LKH1nbRxWzj4Qfo/gljQCy8a+4pCBvP/NxeqL+85dhpcmLI" +
                "CZ6eS/IgTTiMZUe2JEfuB21CPF+OaHgGWKtYlNnjHMNW6B1VemPr0Gvni9Nv+jrq3l7rRCf3O3yL8K16" +
                "v732l+F1v/ndL9iAIhqh0YcdKj3kqs40QCHhbU+JiAvm9I4ixtWefLa+fhCPBvb7MATyNIi2IfE8LSZn" +
                "YPyrwzj8p8k3hIsuiw/BbhhWJA1b7ngv0CWOaQmKeLQp5mviSmT7iAjVrIZD4BSrCq/Tqzo1vqEFoCn7" +
                "Zs+RuVE74aeiqe0wLKyANhaEHgyzJHe/3Nu8fes2tfNIPaUfliOiul1zoQ6tVitLNnBITbGgGLbblejU" +
                "kNB8zuwfjwaEnKIULBxEUZ03iPqZSqiTdlcsGExCzuhwy4ETBYFAPVlVh/QJ2OmgW2eejTzn8K22jI0m" +
                "emz0fx933QN79YkSIzy5ExQL+uRYSWPp3fbWRNO2YeyrwqJggcsyt5xh27o6+KV5KVthS4mlda8Dp5Zh" +
                "QKj+X5zIehEzR+IeBGiHlsoKaYhgRDB/Mgup8xDDCmfTFlhuuSNgG8V+kux2pGzojyKUZtTunlF2Ks5f" +
                "I4r6xT8fF0eWq+n1sqhEIqniyghjxRKiAc1uc7//h6LPK/kkEPKZuFdZ0TQUGa66knxmPLR7JoeJi8n1" +
                "hB0nofF2i5uPtvhp2OKfUY7tMtNCDj7gG0+BSnUmXtEMiracFvjOynaqSUfVNwdblSLh2J9wv7SXGsRe" +
                "bC4PgKVqBfaXpsQQXWOyVBNFYMYFk0F+ywjLpAn4pU1OhEucH0cjKPi+aLoRhow7ImSl6jOtfFYZHLtz" +
                "GoT+Cot+lASEnD4C4NXrdwCuZV8rzIBHR5NjysBW+KNNbhbxmYciZScdrzdoFCwiwQ41sQb85E4sjyUt" +
                "LkskgmOYWKkCpt42hkRC70stFWiAJMmNFbQe+PUCwvgtUkfZetOIuByHbd+LqcoyigaLF204j9A7IRmx" +
                "vaNxqsaI34kS5XkK8PfuWfqxdtEuXsfSG5pnuFAoXkGO2nnjsEKqhZxKfuq4f/VFEGGHeuRCM2o7VMep" +
                "HW8NxhSinwy1e+mBkts5s6uv8sbKIXxNw5SV6fMhiyWcpbVbaywOuEdKXjSos+I9G5KRGJTFy2lFbfOF" +
                "NpC6fj1oU4kwEwtDZ5QMbHd/0FfJ1jegUSkxAJmIns63Zc2XqFsm92ZPhK+RC/D7GzjhiQzaW89w1Ly/" +
                "E5V3I4t4elQXCDaSDRjPdw6UFwBEtW9kF9GgNVrjHYcUQruQJ9YTo6y+7nqLRJkFGscDpU4MASZ6S+Uh" +
                "QpvoLafXmk2hNTdJQc32nmOgQBO60ECSz5O68HYDOt7ObnYExNnthPl5JswX55GfaEps7TjltwGL+Z5g" +
                "QUzWroAC433FNOfhUi8C27YcZJSdFVUmY5TefssNYCZ7iW1FMhmkC7ASjwxq1U4u3Cp010WdUg/smrqR" +
                "j+U4MJOREpVICvWqXDhhxqTmYmQ8tSuSg252spahaflkktjkJUQuCOKYJaML9i6gA2P4YeX5TgHgTK1b" +
                "SNzwYeW6Kwm32OKcdW/nUlBDYvMLz90JxyhErzB9ZBoXBSM6H7WAQXqWma7Fjy3aA4fIJAwS4iwdcWVn" +
                "I+8G/6Z8+FhHSKFjXqyKUhEK2ImwCGcKBWOWFhmypgaThdmSA3ZTVdAiucE0AY56IikMLxq8ZiXpo0OZ" +
                "nx6kfhSqdrut9U9U82xwNwjaTTTyH/a59xIt2MK3AZI8vV0PesiOX9L36aPoiYRtPe/KYYuHnLlokVrl" +
                "R12h1lUhMwk9UC6pLWCKTdkuYTriQr8q3BAjOjJQzYyOAXmkFMyvcOmRSrqntEqnmwrbOlRFDzaMWyge" +
                "2QjGz/dIRDxGjh5/Pj9EzplpCUtun7w6ff0WufTBi5hqtxffh8IGE5iyTmHsfxrj/lZFIgTgs8txv3pl" +
                "eFRGmdGv1Bg4mQJAddHdOyc7L95y1pOzWuCo3n1edYiLJ+cJY65jcKfZPUegB9djxiKa+bw3c98e/VrS" +
                "bN+d5oNw0eapfAhXWEk7O+Au5aPC2nO5CiDeviTGiqVn9cARqsQ3DfMVVXpnpV7sNbj3kiIvuEt2U5d1" +
                "4g7P9Jg9u05Y4vIr5iFlAY7xv6CfQWXFee9+IrHerNop25cDHLrlWzic9H3gMbVFsTLJJOW2ldhLaSUm" +
                "YV7mEO1yPSfLMs0KSCqIFRmON9HxUnSCjNVQ8eXPIBNRUX14CzKa43EtEi9b8wvRfu+jJwJeynjh9kCo" +
                "b1+rFSuKhOroLlt/doMwTDmVqr5qXi546EHt+ATV3jVsGJWrJupinrYS7W4LmdJEVGfOvZoqximNPkfe" +
                "LyFkIf8woxW5UGx5r6nRXJp1wih6exGXuUrGRxCSZUrkCakza+R0sdZIk6WropAD/gAbqDd+pMp8N2bQ" +
                "4YZJn76kyWWZ7yKoE6GnQdt8XkzCZkECjLfmBvxkcjDFa02iSlZPPD+kOYlI6Oh14+FOSSsQdHfPLnH1" +
                "q/hk2eIWxVZmjjSYW7Iti4rE1hv9aLxvKmHjXMxf3QZSjR8if9tcqt+F4Z11MmMr/bR1sd/wQj94dTkx" +
                "q4LltAvmztv8bIQZNPbNfYpyEcBAx29c7IvsfkqWZIVRnVxuk94ZKq/7d4am1zimt/v173Bh1sfGURjS" +
                "yCOtiUNq52C572ab9dJvhunm2f4uzy6mlSQdddtZYrMuoE3lkN9E75JKjhePHgjavu3jyR49QD+ySxHD" +
                "oaaXeq7e72eMt9tYe25mwJNAwJroVYt29A2e3ugDZiKhZvvWa8+L/QptzWseC28r73vX9Vg18eH2vT2o" +
                "pVAXHGsDo2KVo+iv6aG1FjdVSnbpstZLcZvi4SUl8Mr2nxoyvZtKVuORHmEAUeqGWXYtw1ZQnmbuDdk3" +
                "Cb6rG71Zdzm7p8uA7uN2nZ/nwO0TxumVE+Kkb1m6atSOw/1nD5z5t1oijY8a89hwmMGJtyCJ6rPb2dDq" +
                "D3l2Dlv76SdWcn9VXpTjpm7HdbN42M0/+fdu/oeH+b+DlacXACTXIZyiipBO5ayewqPyUIzetihVOMGM" +
                "2UoCmSToTzdThgnxNS9SZSN9yztu/XKOcN7+Po4X79z8Jv+8eAYUQLAqFBepRSHtQp2XNLE6L+tNfr8s" +
                "ZyxA5Ncydr5VFMHNRiKMtfntzUqztHbVY7/iKR1tiMEJv4UZae3UjpzpcAOLzMOaF8s5huQcW6v6H1ge" +
                "Sgy1P8IVQZFC48Sg2MbTTfAfNwUIEm2wUjS4Uni/ggteSXjAPXua0HqGyCh468yjbRqyENujJ4aeB+Qw" +
                "jjGlooJJyNVgMo2tq26FMSTAJFYclN55hg5yjPuxBKh6s5XmVoquVLRUgl79rFMyW0kPrFfZs1fH6fm1" +
                "wGgGYJKyAGXf1idf+fvfQ8KBtPX7V6zFdYCCm16YX6WG38xx8Md7mHaiskcPxNRxv/e2mwFc3cfbG9OT" +
                "/aFIyvX+/aAgdsQvRcAuFv55BMwYufvpJzaGVyOFg3pERcPxEEa6ARmFZ9OtY+DhDmfb7tLG7p8V6G//" +
                "+OUz+3DX992G8ZScdD7Cr0X4dRZ+5ffuLYhtpnZh37AcXhIhAaodVzu+SyxPkZzpVTHR3I/weThvZD1s" +
                "5fXhOwg7CejZxzs7p/szY0tY8smxmM30/GB06eUO0CwSuED7pih217qkuWAMwtMXu1wZuw1Q7Z8dqTaL" +
                "nQe/VI8tGEDWLu9C4B9BtP82scQDkGoyhrb5D91o1305h8ZjKw/r6XSzhpI9oMIQl03eIOqNMKuSYn98" +
                "1j0c0xjAld92o6YCkgPPS/g4qXmpRU10prR7X3K8ZRZUQxJ06VcY14o5mJXkPxxg3Sr4s6H4W5On+u8J" +
                "MMWiaCD4XkKy/hRyltpVr8zVq9nkZNhYYk1yoJbGwlWD3Ji1bRmr+B3VOHfL6L8A+smhMOxoAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
