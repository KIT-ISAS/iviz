/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        public MoveGroupSequenceGoal(ref ReadBuffer b)
        {
            Request = new MotionSequenceRequest(ref b);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveGroupSequenceGoal(ref b);
        
        public MoveGroupSequenceGoal RosDeserialize(ref ReadBuffer b) => new MoveGroupSequenceGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Request.RosSerialize(ref b);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Request is null) BuiltIns.ThrowNullReference();
            Request.RosValidate();
            if (PlanningOptions is null) BuiltIns.ThrowNullReference();
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength => 0 + Request.RosMessageLength + PlanningOptions.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/MoveGroupSequenceGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "12fc6281edcaf031de4783a58087ebf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+09a3Mjt5Hf51dMWVUnMqa58q6TyinZVK1Xcrwu7yMrxa+tLRY4A5JjDQf0PETRV/ff" +
                "r58AZkh57buIuauck7I4M0ADaPS7G3Dy0rWFq67sT52tMvsW/zZtWvPfJEnelKaqimr5eoPtmnQjzzPH" +
                "L5Kn/+B/kpdXfz1P1+7WFu1s3SybRwenmJykXxcwU7eAtvjdz0wnn26LdpWadF7aKp/VJi+6Jl24OrUm" +
                "W02h/4sqbVcWmjcbWIhFUPhMcGydmrKUV/BtW8DT3Kb2zmZdawBmaprUVTZtZF7TpD/PF61dv3ufFvCn" +
                "SY6OJRwdlngt6/E4wfW3q6KheRES2hSe/Lpj/Pm2jV2ubdUqgnTFE0RBsQBQ6dbWgJC0cWXRmnonW6Io" +
                "QRqKKCvBiTneF8Bpu7W28kAZYRMaSDZ2bXaI+mbtHLzN067BWZo0K+qsK03tR+P1MlzZb5kyNoUt08Fo" +
                "VQZa4bfK3rVp5tZreDFJtytcylm6tgaovZJpwoDTZFE60/7hsx5BHW9nIyTSQmlf6tsCUJa5qjVFxduY" +
                "20VRFYQ43EDjt7N1EVIBhKdzwYPr2k3X4m5uandb5LbBjXpjarO2ra0boQfgBVffNBsDA7cr0/Z4plm5" +
                "rsypRQoTAiDJt9o6guQhzDb+JQ52BcTT4uY2rWlt2m1y+NNM0xeLNLM1rjH90RVV2+hAc1o8jlPbHADA" +
                "dDauKVhUIbXgjA3zedbVNVFxZZm+gK1DYwYIIJDSbJsiGSRv3dy1VzSXBqc2o3klyliuaQqUBEtnSp5y" +
                "QNLa5bZEnBP7wdtpeglyJ7WlFV4CKNjQ1DXQN+0adDcMrLZLpGgahl4gvWarwt7SMguhaph4WxtCCO/1" +
                "BvHHTMEgADzM3bRFsyig6/PQA2QTQp5FQHBhr5wgH1Bpqh0sEr6ALHSwK7TTBoRqwRgFOsm7DDgyksAs" +
                "KW8LVyIQxnI8zxHz3mZTFrBcwA+KWRoENqVybfpjh6Lb7PjdOJ4yDT6c8PUAESLSu5IICd7+aLPWoUxC" +
                "wIyKXXLt38fwQ+tDo1RAqSpQYp2DROZAKlkiVBAZ1BD5c2OzAvE+QdLEPTYwrWFfVS0AIE9gTJy2fJsV" +
                "+aHBl7XrNvggzADAtqsCiItwq3Dhp9vY2iAeFC71nCEsD7dbz6ExQi7WuCMKgtSCYw5bg2Cw+TS9Wrm6" +
                "RVkCgr4TMaLTr+0GP+bTBOb05DECnnljwbQg1TcBlWtzV6y7dWrWrhPFAqMfwiwSS1m6LVBZxEzpqECV" +
                "AZuUA4moYJaGYVgESnIlMyUuf2Fwc5lZ2H6BLjuYOpBxIQSuUyPcpre2dBmICEv6Arcxy4CFEatAINM0" +
                "fSaTuzVlh42A3WBqo7PJp+/h6/V9AHcHwAV6UQarUf6IKEHIa6Rq5ByQfDvcKluT+QI0W9xaACcLhJGB" +
                "EkGBoADEjiDbUUgi0KLGuRaoIaslzHhUrHHjTNWWuwltPwqYKis70ACwsSSOLamPs+nZmDUzj0M0br1m" +
                "UfomVOCefjo9I1gg3hnRo2Jqp5P7MAIA+19i5IyD/oVGM+00a3hrZzyjXpu4+7DdMfT2Ac2nmhs4rTHL" +
                "SHMbQiTgLChEFAiLDkQ/chl8JXkGfI+ovgX+A34ZgXXr7sZILioAlGj6fIPTmurghQftKtgUsHgi4xlZ" +
                "Y+3mRWlJmZAFJZqKAYPNt7VlOUW+uiCtxRRRi4Cq7QJUJ1pyqgphirDQusL1f2kN6GrQrvjHS4MChAXs" +
                "OjdSKQfdJmzDC4krnfE8GrUVQA3bZGkdYA3kNmH+GxLiTxDwjIEOBc8/figgOB3qYUiraXMeknHI5lKV" +
                "mxr0r20NmEqGUL4qlsD2n5RgK5BVst4ADdDXdrcBayoig6WF+RJnovLBRaMd3FVFRsobFULcn5i5b2Vk" +
                "ztVgHWNzogCETuQq1vyLi3PSzug1gYiCkUC01NaQCf/iIk061hbQITm53rpPUFosUaPq4GxkwmTt3QZ2" +
                "B+dpmnMY43e8uCnAPlddkI7o3Qwem3EKg8AUQC8BZ6DCeLNrV44l662pC/LiADBIhhKgnmKn03EEuSLQ" +
                "lamcgmeIYYxfA7bycHFNn4A5mpMyarqlIREq5rbIdBAKIOBB+pXFvAZfKiHNSEMmJ18Ql5GOph1BLm8a" +
                "kISwATlRsGp62g2yHx6GGg9ygZIW2AKwVRZtEwPynXUFKO7awkpQIk5T73qSDEJvi5jO90R7qahRsYkd" +
                "DHLF1eBzgn+SO4uuGfpBa3OD9jq672hLglkJmhXtuKopWanCa+gystPldMKyjlqxCwkQiAeKLK2LJShF" +
                "6gkDrX1nk8riQPMtHrO5Q3PmwWDDAEjtWtFSqHh3rgMLFtYAP2phPbKkdF5EIq1zE+Q7AdFH6BtSgKom" +
                "QEO0wPQgc1W/3flfO//r5yPotOAR3avKiirgz8zB8uhL0hb3EMwA9iCCs4ZxhEYtWvRJgUlumgT31tU8" +
                "+Ff4kd0xahfcsa+c2P4gKjBcsDK33lqy6cXrL9gP8vYVeXcx6JfYFtpFQ1D3We4Ws8Fgz9oWfDGAkrmy" +
                "LBpcp5uj3wDiwOg32PAGNpRWkbrInBsn2v+5dn9NvcEh094zD3kmkHHcL0qzBOzmKJ2RfIGYxcNFOywD" +
                "Og42OxldwEotiUtipsViT8XRDNlNpv6Rali7pkVprcYhqxaVXeS46lLnLkf7eKTzgYboyKE2Ka2pDzWG" +
                "gWDDjZDW+TloTnt+Hrnbc+J2jgDggIWE89qI5MbJ3Dl0jWe4uAdTuwcJMMKU8SxA5LdyZd54pgcbOquL" +
                "OdtI7FjTwsXaA7kCipJ4p3akLpgBUD6KxeI7YdhSDONRbdECxPc1qI+iQXbLxjgbDiSgLkEdCaosZjO1" +
                "dxSKycnoHk9C02CND5s+aqjxI1CsyJ6hi13AXrVeT7Nv730YAfBqjb1fgRmPC7sMa0FrtCoAC0htOUh+" +
                "5lDQhSgPyFVlRLDN6JmYPX1SjjBuMH8ZqkRjKC4JWgSMFPRfwVnba8NGDUZydRtJeDAbaddU4lwNqd+C" +
                "7JwDArApGraYTRA+FN9pJjSCRp/JgupjmCZDajGO+aDpjhqOOkl7WDtSmPqv0/RbVGqo31jfiAilVVQO" +
                "AMr+DAwFhLWekK7KwKkGTr218XZyXAq99p1QI2KPV8N7G61dwnErhUF4aoqfQdbDkjFuTnAirqGII7kf" +
                "MAqGT5QG/DQj5FDwQyeNtjsaBmhm0g6CWhz6FWwEgUQl+hEFCY/KCNErJfjoFWPgGAJlX+3Aqt6qDWSU" +
                "Z1kGIPXI5hIuPInldgmWFREemli5g33FYIBDB1DFNaCky9quJlkSxmNCZlMMUA9eZc6cbJQ/0eJvdmCA" +
                "rBnzmC3JOXQXzCXus7Rt4H+Mbviw4Q0IJRJSabYCK2GafoGscAdGbYl5BPI/Ta3CwhCF/f3txRck056g" +
                "Ah+Bi7aD/5stmtIctAa7nD8iBVN2JrgI8eyM5BGAKgAK90WO7n0naxJbKDRg6FuLIa10brIbXHBvDv8v" +
                "xo4rxrYYVVj9ajGmzf8vibH7pBgboNi9GYQfrpWEoZUn571GW9hQbIB/B9++JTTBR8bXcdxFP+sDDiOx" +
                "gxcrPmO3dXuxhmbgUyaJBmMi/y/5W2cw/ogCQL204ywyDHzILQZerQsv43sLwaefwqwRDx/0/PTX9kgb" +
                "SJQky1Kp2wTjsb+eee1uLC6SfPEGfSL0C1AOm2pJcSSKNU79BkqT8CztjrM65okDuwZbwdsTFjcBfY4x" +
                "25Ys3hYF1a9cIgELj+wKHCMufY//KTJ78Nbnd7wHJ2UVi+JO3RWOlha06hsNQOFvzTQRHntZagOmCCZN" +
                "V2bDqRWKrYb06d7cWJHF7jU2owFPXiwQHvERbheulUBWThzzaZFLrmLCZR3iVJ8cgqcuNmtvv4wo0YUD" +
                "YC61jygGqoFmcfAoiBHy5H48DRhgWCKK07euI8Kb73w25RM/sZnmFU1Zg4bYRanvqEPQGgRsxmEUtPFC" +
                "Ep1nRVYFGF4ZO2u9uC7sBtmKvgaEJ8whujXC8EsB0BJdA6GGpRdgPnPILysdl2rUriNGECiSRNIxKpuh" +
                "/q13NFptSw4PS9ZBBsbtA5gau4kytcEzD0ldQAbObiZD6KZsbbFceYtlsBkTTKvfVG5bBWlK7Y/Bk/u8" +
                "+EzMgAnH5hcUQpWgjtr0xDOHcypA8bJMQeCIqIdgwe69hLFftGNl1pBn1n3ebSwTBUYy5obLowg7nnv4" +
                "7wyNyyVXnPBaeAnXCAHBhCS9RstE2lJSdd+aU57lbkUtkgE55VCKJg7qKQKuHCY4dZgMQyTrAvMPTUIS" +
                "h+dJrd7oJ/QOQ7NhNLbpfZ8x4mGkl7ZZ9aHiG2i75g8H4eC3AOJzZA5N+mMczaLyF2kWSgjSeRdqbaz6" +
                "KGxEhKIo2LE8L9ikJsSN47lhDREthEa6Z5H4LczuWZ43MRkJ1n2+kOKGFPWOGgGN3haua8AOtndgKeD0" +
                "i5alMwucaTLfgR337OLi6VlCPi9yQ2+kRe3WHJCobovaVVQ2g45Vjfb1yIJvtgPRRKxAYd8WmLkZ0ESR" +
                "j3mkt5cvX39z+fRTWtNmg3IKPdjKr4t8XhGsNOnGl7n84lo1GcGddJ2wC2GRb95cvrp4+liEcBjz8HA0" +
                "ygSk4lYoX7aasiIjqv+QfVM3hipboEVpFy27KGMuEmpcibgC1KrECNI0t02BRVM0RcLNE5zg641WI7DG" +
                "hUc0QLWh088PJRQ/LFSSk9/8T/r6868un19jwvG3d5Z/EDnPfznHQUKT3OYFKTwRZCDGMFCBLkxj2bGO" +
                "svytW3LY3LuOHMIFOsEoec+ouLE+LhuPcE5vuH8IPtRKUEuQWFWaz1XYAxQFmM/jqYiCpdjJV1evXz3C" +
                "yh8JqHz/7OXXKQOYps88CYOY9QwQ5eJQUCtWQtCIlboqlGl6SVZDUR3YdOIjcuiduwF75caepx/9xyli" +
                "+PT89DlaNhefn07S09q5Ft6s2nZz/ugRuB+mBGy3p//5ES+xJoupchzNqUQy8u6JdYObE2EBLceiPYVO" +
                "WFsJXHBjrWShFyWw6rwoCw0C2EP0iskMRqLmFi8+Z9ogIBmV4xqNX3EcBIlL6hMlKkY5boySyWIpnE9g" +
                "zlOPAHqHKIB3QxSc//7f//gZt0DVy6lUaLc/41MZ6epvX6ewbY3FHIbfp97AVz+VX2oLhk1DpafbZfPk" +
                "D/wGM0bn6e8/e/KYHqF1jQ0KNHOlBaj9LTjzg9dooeBCdABNfvHXtcu7Er9T+rR1m1MlaCDth4rV3mct" +
                "hOIXqh1pNkhpkzTbgWlNRluGgTKJNqmXU1ufngGy0igTWDhzNQEAGAp8VOnEiWw4n03gf9OEaiX+mH7+" +
                "+jtQY/z76s2Xl28vQbXw4/Pvv37x6uLyLYhyefH61eXTz5TbVT6RlsE5SSu20lQkFKBoG83JhqYhPB5a" +
                "+Koaa0hBxh2iZucc+KOSSkwlarErtkV03amkOg19Tlm5JUKa+BUWTlNl7+G7Sfo9x3J/iOeMSCaHyVbL" +
                "1gcbhzII3aYmqv6ZBtzOvgOLJDx973GNTz+gFo+mxPiXWVEcELcdxSb8lahwg9YPCxUSxVKiqvXp4uYw" +
                "BU17+zp7++zixd+v0EKKxtRNJpi4wVzXw1hh0qHwAxVXqHlIkXgZ6ofUgMHB1YlcVdGDO/vy8sVfv7xO" +
                "RwhbHsZhTZy6jTAe1rTquVfKC+kIeWHM46Gc03F4dTIOP0Tj3DcKVlv0avvVOTk85nNXcTBAP0H/YOcP" +
                "eXJu/akCLr5ui02gIcIp9kdnk6vxJpLj+FiQmgw4UfDnSWqweLRHA6fuNQ6IwYYPI+L2nQByPuu9JBQa" +
                "o8PYF+0Wmgf8nZPMiO0ouDlNE65m8cm3KCQbtTvWAovKRy57Iak4SW6k7r233A+EYB9eBaFrqYonmiq6" +
                "kyggSjkPBUrRVEuwIP4USVipT6bqTKqk9pU3sEZMc4Gx07x7n+AY1wKAcgoCKxHhIYE77aG+143V0lSa" +
                "zQGcU6qVOx0JVbqMAyjTZZ02YVJcifjuCc/T3s0oDniU2ZJffjDzy5lROxH/Pvj/Pkhg7tKPMfz3cZr9" +
                "DP/K06cpedQmPX8KBG4X787eY0TRP36Kj5l/fIyPuX988t6nGt599p7ePRQCPhDDG8S1DibDBl2U0Pjs" +
                "yD9p3iphKDkcnT9hiRLyvlKe7xnx3SQ6bQAPvZMG73GXXL81lym89zHzaCxWZXxYEeMQYof6sEj/tIJP" +
                "dWKSuEbXpZ+zJBkxWOUUlp4cqKxo9ksrsN4rvOwta7/oIu80/AC6f4YxoBmVXx8nCBvO/NxfqF/ec+w0" +
                "OjGkCI/PJWmQxh/GkiNblCPXgzY+nk9HNDQDzOUDTOxhjp4VekeV3sg+9Nrp5vSbvg66t9c60sn9Dt8U" +
                "jXi/vfa3/nW/+cNv2AAjHKHhhwMq3eeq5hygoPC2pkTIBVN8BxGjao8+S189iIcG9js/xCfwFem6yuxs" +
                "DoS/nYThP46+mTks4L23G4YVScOWB94TdIpjSoIiHG0K+ZqwE+kot5VrSfljThy8Tq3q5PgGF4DG5Js+" +
                "L8GqIzvhZ1s7OQwLVkATCkLHwyzJw2/3Pm3fy6a4Z/lA6fvtCEvdr7lgh5arlSkbOMQmWVAYtjuU6OSQ" +
                "0GKB2b+RECFBoYKFcRDVpl7aVlSCi9ptLQnl+DjOPQdOGMSMQMx4SJ2AnA66d+ZpojmHb7hlaDTjY6P/" +
                "+6jrCOTVR0qI8BhFKGzokwtGjaR3m3sTTfuGse6K0wDviGKEHfpB41+bl0r80WNU33HB2MCpxTAgqP5f" +
                "nch6ETJH5B54aBNJZfk0hDciMH+S+9S5j2H5s2lL2G66I2B/if0k2f2LkqE/uKA4o/bwhHJQcf4WUdQv" +
                "/vmwOJJcTa+XRCUiSRV2hggrlBAl9xUm9d3v/6Ho00o+CoR8Qu5VausaRYaqriifGQ7tzukwsZ3dzbDj" +
                "zDfeb7H7YIufhy3+FeXYITPN5+D9esMpUKrOLIhaI1uOC3zzosk46cj6ZrxXKeKP/RH1U3uqQezF5owH" +
                "vOM42pZTYsWGkqWcKHJ4BMsTNpdJI+CXMjkSLmF+OBqCAt8Xmna1+sdMwD4r5eZc+cwyOHTHaSD0V7Dp" +
                "51FASPFDAF69vgbgXPa1hhng0dHomDKstmW6DjeL6Mx9kbKiDq83qBls0XqokTWgJ3dCeSzi4raw2yhM" +
                "zFgBot43hkhCj6iWCnBg5uVOClrHer0AEX7TYeVuV5O4nHq278VUaRtJg4WLNpRG0Dsp+OKLYJyyMaJ3" +
                "ogR5HgP8k3qWeqz9VqPWw5Z0hitb4QF1PW/sd4i1kGJJTx33r77wImzCtcqcUTugOq7keKs3phoSGV9o" +
                "6QGjWymzdVtTSzmE7qmfMhO9GZJYRFlWTsfUDqjHhKKNNd6zQRmJQVk8nVbkNp9xgwmfxTYcmQRhRhZG" +
                "M6RtufsDfZV0swMcFbnnUD6dL9tqyi0WEEDDJ0TXDlMQfCIXJzxr9BoYv5/+qHmfE5l2A4loepQ3CGwk" +
                "GTCc7xwoLwAQ1L6gnUQD12iFq4rCIQXfzueJ+cQoVl+3vU1CmQU4DgdKFRkEbKPFNl3T0WFoOr1Wd5Zr" +
                "bqKCmn2ew0ABJ3RBA92KzV0AVMDj/eQmR0CU3C5vSXK4brkK9ISmxB7HTQ5JMeUJLIhJm7UhZpnbzHSB" +
                "qw5YDjTKwYoqkTGMb73lJjmJeQnbkmQSSDdASnhkkKt2DFHrJBBLZvic697UBX1YjgNm8spQoQrWDdII" +
                "Z2JMci6GxmO7IjroJidrMTRNn0QSi7ysMNFf6sqi0Wn1KqA9Yehh5cVBAaBEzSxEbviwcl2VhFpsYc7M" +
                "24YKahDZ+GVCs/RMqRWmZ6Jxt07mwxYwoB7LTDfkx9pmrBAxCVPaFktHVNnJyIfBvykePeYRYugwL6yK" +
                "YhE6nsbCwp8ppBVjaZEsVtRgtDF7ckBuqvJaxAhMEeBuw4XhtobXWEl6NqH58UHqM1+12+7tf6Sa88Hd" +
                "INBuxpF/z+fai7RgA74NLLLcsekT9yCOL9H36S9REwn7el6Vwx4NKXGhRSqVH66qqOqX05hD5RLbAqLY" +
                "mOwiosO1oF/lb4ghHemxJkbHAD1UCqZXuPRQRd1jXMXTjYWt81XRA4ZRC0UjG974+S59mj6epN/Dn08n" +
                "6Q+UlpDk9uWrq9dvZz88HbwIqXZ58Z0vbBCBSfvkx/6XMe7vVSSEAHxWOa5XrwyPyjAx6pUaAyeTALAu" +
                "epj5x87JwYu3lPTorBZQVO8+L+fj4tF5wpDrGNxpduQI9OB6zFBEs1j0Zq7s0a8lTUfqNI/9RZtX9MFf" +
                "YUXt5ID7CZeeAmkv6CqAcPsSGSuSnuUDR7Vtu7qig0bRnZV8sdfg3ksUed5dkpu6pBNyeMrH7LHrDEtc" +
                "fsM8qCxAV/xv0E+gVsvB/URkvUm1UzqiAxzM8g04nOj7gMfUWLsWyUTlthXZS3ElJsK8NSDa6XpOLMsU" +
                "KyCqIObF4HgzHi9ejpexHCq+/YXFhKWwPrxnMZzjUS0SLlvTC9H+pKNHAp7KeMHtOZto7Ci6VitUFBHW" +
                "kxNm/XxXgZWfUVVftSiWeOiB7fhoqb1r2F6wTiB1sYhbkXaXjYxx0vG5WkFpuEipaf3i9RJCLOQfZrQC" +
                "FU7Dmqm3uG94r2Ajtxelcmujjj8h14RogurMajpdzDXSSNKVtXQyFsB67E3PWJkfXhnocFlJH7+Ik9vC" +
                "HEKoIqGnQRuzsDPPLDNcUBLWR5MDU9xxEpWyeuT55Ryv8B21btzfKSkFguruySWuehUfbVtg0dpSjtSb" +
                "W8SWtkJk841+aLx3VcayBs1fZgOqxveRv30q5e9E8Eo6qZAVf9q72G94oR94daYgb1Utp0MwD97mJyPk" +
                "oLF3xxTlV3ITit642BfZ/ZQsohWM6uhym/jOUHrdvzM0vsYxvt2vf4cLZn1kHIZBjTTSGjmkcg4W+S7v" +
                "NqXeDNMu0tEhzy6klSgddd9ZYrEuQJvSIb8Z3yUVHS9OTmjZyvbhZA8foE/kUkR/qOkln6vX+xnD7TbS" +
                "HpkZ4FEgYIPLq5ZN8jU8veEHmAmFmuVbrz1e7Ge5NV7zaLUtve9d1yPVxJP9e3smqb0Vo96BUbE2GxQx" +
                "8bI25KZSyS66rK4ktykcXmIEr4X/2JDpHfFfTxM+wvAc+2KWncuwGZSmmXtD9k2Cb13NN+uW+ZEuAzrG" +
                "7Tq/TIHqvoUTxvGVE+Sk71m6bNRO/f1nJ0r8ey2zVVEqcWPDYQYn3IJEqk9uZ4NWfzbpCmztpx9Jyf22" +
                "uCmmtWumrl4+ahcf/aVd/PmR+QuQcnYDgOg6hCtr6WBw7rJu7UMxCwm6xWbMXhJIJEF/uulJFAANh/2o" +
                "Eb9NrsPlHP68/TGOFx9kfpF/WjwDGKh3obiILQpq5+u8qInUeUlvpPfbIscCRPxahM73iiJws3/qDNbm" +
                "N7s1Z2nlqsd+xVM82nAFl/jNz4hrpw7kTIcMTDIP9tyWiwkl9svGeRESWx6MDLY//N0aAUPTyKDYX6ea" +
                "4D91ti4iG6wgDc4YHlXgglcUHlDPHk1oPkMkGLx35sE29VmI/dEjQ08DcsZXgvNSYBJ0NRhNY++qWyIM" +
                "CjCRFQdKb5VCBzrG/ZgCVL3ZUnMpRWcsSiqBr37mKYmtxAfWq/TZq4v4/JonNAEwi0kAZd/eJ9354/MQ" +
                "USDa+v0r1sI+gILLbsSvYsMv1zXo4xGmHans5IRMHfV777sZQNV9uL0xPtnvi6RU7x9nCWRH/NoFyMXC" +
                "v7wAMUYefvqRjaHVSP6gHi6Fw/FbLePYsSmzfwzc3+Es7E5tcn9W3dVv//r5M/nw0Pfd+vEYneh8+F9L" +
                "/2vuf5mjewtkm7Fd2Dcsh5dEUIDqwNWO15HlSZIzviommPsBPh7OS6SH7Dw/fAvCjgJ68vHBzun+wtgU" +
                "lnxyQWYzen5gdPHlDqBZKHAB7WtrD9e6xLlgR+0OJqnkGi22fw6k2iR27v1SPrYgALF2+dAC/hlI+28j" +
                "izwAqibD0Db+h26464jOoeGxlUcuy7oNKNkxKgxy2eiNqbKdomI0nbePpmgMFKXeqMmA6MBzaTDgHcxL" +
                "pwlJ6d6XHG+t/ocByKVfj30xB2Yl8T8cIN0ql4eicE6edhrSl2U0YM6AZP3Z5yy5K1+Zy1ez0cmw6Yov" +
                "JTEcFNnWRauE02Cs4o+oxpFbkv8C+smhMOxoAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
