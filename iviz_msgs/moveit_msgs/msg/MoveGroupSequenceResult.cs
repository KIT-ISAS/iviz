/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceResult : IDeserializable<MoveGroupSequenceResult>, IResult<MoveGroupSequenceActionResult>
    {
        // Response comprising information on all sections of the sequence
        [DataMember (Name = "response")] public MotionSequenceResponse Response;
    
        /// Constructor for empty message.
        public MoveGroupSequenceResult()
        {
            Response = new MotionSequenceResponse();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceResult(MotionSequenceResponse Response)
        {
            this.Response = Response;
        }
        
        /// Constructor with buffer.
        internal MoveGroupSequenceResult(ref Buffer b)
        {
            Response = new MotionSequenceResponse(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupSequenceResult(ref b);
        
        MoveGroupSequenceResult IDeserializable<MoveGroupSequenceResult>.RosDeserialize(ref Buffer b) => new MoveGroupSequenceResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Response.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Response is null) throw new System.NullReferenceException(nameof(Response));
            Response.RosValidate();
        }
    
        public int RosMessageLength => 0 + Response.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "039cee462ada3f0feb5f4e2e12baefae";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRhL+zl+xqIGz3cpKYydpq4M/KLacqLUlV5LTpkFAUOJKYk1xVZKyrB7uv98z" +
                "M7skRctNi7v4cMClhWOSs7Pz/rYbz7syeWSSof5tpZOJHuhsaZJMq9T+4nmn/+E/3tXwTUstzJ2Ocn+R" +
                "zbJnu0nw9lQ7UTpNTaomJiSSprGe5FEyU+t5kKu1TvAjNckMTNzpbt4h2DOAZrLMp2Ue8IzmWk1Xcayy" +
                "PEgZAX7JtTJTleNTasYmV8BIDwzivmSWJm9AIENe5N75DOnQ52nwK4gzaYTdcyKP1i/jIEl0qpapCVcT" +
                "HaopmNH3erIijgXryK3cfPhoF4R+FZ3bIliYVSKkRQutImxhzC1+QDyLZaxBG+25YGkyJm8amyB/9ULQ" +
                "gm+fVj6NSrcV4kVJfnKs7oKYuMHHNIA6xnoe3EUmtV+HN2dnneHw9Ll9vmh3L28GndPv6I9nX15ftnu9" +
                "bu+NT18756dHDrrbe9e+7J77V/1Rt9/zCe706Nh+rLz0LWB71Dn3X7/3O7133UG/d9Xpjfyzt+3em87p" +
                "0YlddtbvjQb9y2KvF/b9Ta/9+rLjj/p++8eb7qDjDzu9YX/gA2n79OilhRp1r7BF/2Z0evTKUT/odK6u" +
                "sfPp0TckCacY9Td1GyV6EeTRJIOlw8SyXKzYyW44ag9GPn6OOmDBP+tfXnaHYAoS+HoHyLtu/xJ/D/3r" +
                "9ugtoHvD0aDd7Y2GgH/uhPmm376sIzuufvsjLCdVwMont4h088KraefNoH9z7ffaV5Dy85f1jzVMAHlV" +
                "Axn0X/cti/j6Te3rZbf3g0P+be1b//X3nbOR+wp72lPZJsv1YlvMFwMA+CCgN7zoD658Z4RHx87QCmHB" +
                "XDpnP5Atwh7eAY6MAoBOghVa6Sd/c0KzBtPtXfSLbxDWXtUMtujq9f3uD/6wf3lDlgwTff4UflzGPQ5C" +
                "UaYWOsuCmUbMSfIgSjIVJQhqRDGCTjA2q7wSUznMNlTU1E2JhyaLCDCjKBblmfrVgLtMBUmo4ii5zbxM" +
                "JxkiN2/+PX2UoMtwFHFBCCjhLzbOLoKNQhhB4FvFeYQ4qM77FypIEamXehJNI4TduU71FuorggVcZQte" +
                "7odm6tc2a+d5MJkDy8TEcZQRn2ZM0TlTB4H7hiicGURl4kIBopDBoefWn7nlfV6NaO9WI1HZT77FTPte" +
                "xMEM0g2jSWCTngbWFKihhmyiE+QAyMAgjAIs1+kyRQ4IVQB5qjCaTtU6yuecyYGyoNAwEl7vdEpqNVke" +
                "b1S0WJo0D5BmKFPNoRcwNGNuClbHJqQkd+DoAWACXVMeinWQ7gKmmD8FVWJardbEpLrVqiTVscZ+Wq2W" +
                "ofCK5MbE5xWTO/TGxsQg1ifmPpf17zbAiqSCwgXY/OYmDjMFsgOSAFLdJI2gEFtKSJERAKskbpMisrPv" +
                "pFA55CMO0FTens3yxSINAcpndZDqOxOv6D3VElHGAeKQqAn1FAEDct60gEB9ueVmroxxWIKQECwOGyXo" +
                "nY7NJMo3D0GfZQz8LDtk9yyX6Cl0lQv3JI/lMiYfi5Iqgt6CVvcOm8xYp+QFK1ZJBCmQtYUo4cRDx3iC" +
                "QyUB6hMRxFwHIRmqdWLydi2VD9V+8wgYy/1YahlKQhhRqmFeoQ6bqo0aow4D7KDUwHmcGjl4iBu5pUUx" +
                "CHJ4T2GjHgCzKAPNYtku+ARpGmyyBu/A1R6xvqzUlBViSO3M68wEZNhExSK41bLIwoN3sjCzJI0GcVP9" +
                "NNeoi5uzptqYVepCKHORGCC0+gmyDJrFLqFzJb1o0BI1CRIFTyWuS3Uy3UovlvnGWiNJT7gR3VZ4z+Zm" +
                "FSOsOhwspyz6HbEeLEOQgqfiNQRlEuh8jV3AZmEDBZkV4ZAVFERD0Cn0klO+Zg02Pc97K8YhNuJ5WZ4i" +
                "aiCisv3YqpfKaesIlVfO4CuvRAKfLaDkoUQToRlsIKIkYZCGEGcecOTgYBvNEE2PYg0KidPFEpqTuLJZ" +
                "EtelMGeI3VRAb9Qqk9yDDmABkSJTiL1urReTD9gKo8kqRoyeGNh5lBD4NIXMCDsJ2PY2qnveYgPnTgUE" +
                "wTuTSaqDjIJz91x5K6lKsMDbG63NESWgGSUnt3kRH/Q98lJGdAYZxagvhbkmcFO0xS6w7gN+5+MR4Qab" +
                "gAS9NHCCA1B+vcnnNqXeBWkUjOFmQDyBBIB1nxbtH1YwE9ktmEJiHHrBWO7xZ9AmBV7i6ajIhdlqBgEC" +
                "EE3dHSIYxy62U4RBGG8cjdMg3XgcqnhLb++CZCwuxBqhsLntntaERRt+FD5FentYBIHZgSZ1gRGp6BCE" +
                "JCORidpQw55ZBLxQz1KN2AvIKX4JDaIM8EyR38zaFQ/gbjXJV4jOACv3k7DalTySZasFWTPZTeCyBdmt" +
                "LdM5DtBsAAqFycMv0iDJqPiUNTOdl9kIaIPY2N2LmlpN5qhZm+qC23CoJkbECrgLg05t6kL9hG1vBucX" +
                "nGFPqJw8uEfoxP/BmgyC8iFsB2MS/kjxlGJexdCr1Ikg8VcaAYuspfyy9R1YBcJhg+GiSybzGAcT7vK3" +
                "aPh/Un3apLpOERfnfzqpOvD/paT6WE6VdoiWZ95Mo4XI040EkJEzYUAV5vwAaI0qiQDo79q3n1hM+Cjy" +
                "+lxB7xGqnSRTF/JsjVmElbHO1xp2ka/Ng4zJ+qOAhwo1mCCSee94YHci62Px6h9XWJAmFABSIyH1aZi0" +
                "xOxgMUAJRN9q9KsiELNFLTQ1gbCpYiU3lWQz4KFJDpZyz4buPlehgTzQBHIUg6tRluGamsIxzLEqE3qN" +
                "JQfkbA1qahOBolTBtQpXN4jVaTSLwnoY5cBvmWuofHoMk4ZLMc2yGVQIJE7ah03VnbKDrokhdm7XrFGb" +
                "Zuni5I9xaoMqKotiW6DX7ETOVzH4yOEn0Lobsd4XvxWlpfr9SVRd2tgubSOFY4js0vmWzunpt9JAScif" +
                "ZMj9tn4iX+WgYdlyCTYru9ZtfsapuYU5QVFkYhkNY2ggQSk3SGZc+FLSQLBzvmpBymcL9zTcSfjboTWo" +
                "QtRTMteAU4F4Tj3EIBX1f45FRlY+ygziKYaHjwy+bHquvRU/HldGR5yr0BtF925OQk7LKZOmbK5ipt85" +
                "O2FmxnIsJpM8iELViTIgmwfooVhM6AnxG5Wz9P0BbRIXqnM9AuMN9xBJgI/9iNRFvDLKxNiJYBMBi4Zz" +
                "OkehR2HJTvP2duFzsz3pIAo2CkHIBqFXF5QgdadCdrLE09Py4KnYz00qaR4Kisza0mBWbHjYGBOkALW8" +
                "OioIEzKoJI/R8oUbGXWgKBBK7YKyQGBkvsxvKYtM1QSVK1RgqeKpDGpsOgCrN6LQBrcFog/KPsIkZ54F" +
                "4ShYAWqbNBDUNMSOvl0y2SRGn0/pI8UJGTmCxXLYcOMw3iPREwrf6YZ3SzXKNVpGHR4VUrIxqQ843dC4" +
                "OIizLsu9S3lkB2EQdb7dwillrdHMF8VpTRnImFN1m5h1eT4n8E/hkw99sW0rPk59ckJZTJNd+8Y+Uy8Q" +
                "hVVYvGXTCvCArYdxQXtyEHjonJUaW1nn9IzZhhgFZeVxgOxrrHQK75G/fRrOzRKe5wgvwsKIMBAah7kc" +
                "09toS8PDHYW781lZFrEtEiQspT4453K8cprgBDA0Mfh320xoNrsAgXcQFkccoZOhrt0nGkuVYPUiI9v6" +
                "TnYlB8BXOptvY6U3gF3Ih5146FuJ4jU5BymB2l0a4GPYY6uCrOSuocb2CIfBXDsqRQSYX4mnQWNhyLpA" +
                "dKUtDqu0XdNSe5z9KJP0raSuHVJbXzEMkXpxQMoHFlzMVYBgozg/XmUo+/Q9KgUiH7WlJFMOOE1vvEHJ" +
                "3j4/P/2athlwUN3aaZoamiCgv0ruIlwnWFCxS4NpRIgNpIQ2HJMicQU+b8rhzFnNJqLwUHYadK767zo4" +
                "wyaelkuKU1SzOmu24w0bWJlo2wl+ildXY8sixye0UDJ5fd3pnZ8e2yBc7rl7O96lgai4tpZvVc3F/gFB" +
                "OL25jnWxwqE0IGI9zaUbpWkIollmYpIVROsiRhlNcSQCSYZCIsvmhAjsLzHCdCU9cOKRClAHaNznzxUU" +
                "Px1UvL2//EfJITNNSP/6YvuHhHP2x4erHDT52GHKCc8GMoQxmklRt4qqgCdyVDFChTqlMchMzuuKKYGc" +
                "HcFO6Hhuq6i41cWBUHWHFr+R9eXhDXeSbC6IWIkKxy7YA4tDGI6rpNgEy2Oy74f93jO6wGJnZ+/bV5dK" +
                "EOAApzBhhNnCASotJgVqJ5VyPihJ3SWUpupw1UDHPg+Uzn7Esxu6SxNHt7qlvvjHPkl4v7V/RpXN+ev9" +
                "htpPjcnxZp7ny9azZ2g/ghjSzvf/+YWwiKwBY0clyIO7xEZG0Z6tbkg5FSlQ5Rjl+1gUodiHF9xqbcfm" +
                "uOh0H42jGC2OTU+77JVOUUWIrmU+fy22wUiIK/J7u7OMvMi4VpAThTgZgPJQngaillk+R2Q0LVUIgN+R" +
                "CPCuLoLWy+++fSEQlHplQgC4hxTv252GP17i3BQlAh2eFnra2nj4W/zWQQhu3krtr2fZySt5Q0fVLfXy" +
                "xckxPwI6JQCUz2ZtIZD215jb1F5ThUKMuA3cqbt8XeCuVkzfeSqQm+W+M2iY9ucayz9WLYCic3HTscH8" +
                "N1uSpTXUZIPSmos2mJtWdrDouhyYhTsXhlm5gSIqnLErAYCMAj6ldPZEKZy/buA/jADocOdb9br/M9KY" +
                "/D68ftvBJZhj+3j2HjdtzjsDhHL7ot/rnL4oLsPZ+MRZhmiyUFKluZCA8xG0FfYySAlansuVEG4NTaWI" +
                "/OqCClhLZrzUrvAdBhGCpGoS172LVPvlmn1JbnzhwvaEYJxJle7h54Z6L2P7X6o0k5C5YdLJDMWipage" +
                "g6htKviD0JulbP2fUZGUT+8LWdPTL5TFKySJ/C1VPOwitVPYxN/2VB3RU+hERKNQLHzjJD9aEQm2zREL" +
                "cnQIXn/QPu/eDKlCquzplMw4ScFyEClSEdPh8QPPDF15yIcudqtfVICCo6nKYeEWXv9tp/vm7UgdEG77" +
                "cFjyJHdGKhIveZpvtVfOF9QB+cKh7Edxzu0j3Nl95KGyz2O70BDRyU7UZ5uT3XsiZcswwH2i231FnV/3" +
                "STrgiVJugXmWijPGZWlDLFNaT80m2ftq2bDHWV9ZoTonrQmzMKka81SPlp76ALgUDAF+nhD3sAng5jN9" +
                "cN5IxWh99sXaovJAvsvtFpJ2ZbiJEbYMaYtT/8r0vQL3VAyCFDfa2xpJVW/nYBbizjlLdj8xgv38KYha" +
                "S5d4KqRSO0kBAt21OB8OzZMZKoi/VyIsrvji7ipd1qbbAKZy5Q880okmip3sw0eP9hhZBHx8ZHHRBpXB" +
                "nVvhei9Ufisqkvj6zvxBZwlZ8h0PWfREonJs7BCZYwtVXkGUXJ34cCJ06nuf54BPQi335TsP+eUQHJ4m" +
                "/X3Z/xdDguBefUXjv6/U5Hf8CNWp4o46UK1TGLiefvj6I00Ui8fn9DgpHo/pMSweTz4WRw0fXnzkd59L" +
                "AJ+Y4dXmWjvPPWtLnKGx82b/JbpdhOHLdSWsjSjlvTnM/qjtKxzxQ8Odn+ArHoLJBKNQ6bazj6Qlsw0t" +
                "96M+FjPzyl6SyuRfNNAcwtahxVjERgPKfm7qQNNBumSHQ4usdjzNMaLGZROsezuudGUP73TRRdPy5RZb" +
                "D297hSs3fkDu92kG5P5BxxPdqq4Y4KeGzGKJ+eMLtm7yVBbW7zRXUDyRzT5C2fYdT1u98hVlNh17oap2" +
                "5G7vzvC/BEJBSVe6+JLVDrt02t0xtN5Vctj7m0eQlDtpKnEdyBVOd3u1frPn0LmgQJS3jioo+EgHd9Di" +
                "VUhc2HshlVome1ba8LNty92z94ytt7D/2KbDviqwVTxJOoGtReJ0DoxG4tQ31j2QkaG83BkGH9Hmfycc" +
                "/hExTid1tdJMxJlY9QLZARUVRr2ifztw+OduuXhu7GPHpnSvwp5Kl/HPmSbKzxnNn6q3lR65J1MJZg+2" +
                "gOVUbOPf22fbyv4gHv4Lphzp0yQ4AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
