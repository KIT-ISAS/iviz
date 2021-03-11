/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionSequenceResponse")]
    public sealed class MotionSequenceResponse : IDeserializable<MotionSequenceResponse>, IMessage
    {
        // An error code reflecting what went wrong
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
        // The full starting state of the robot at the start of the sequence
        [DataMember (Name = "sequence_start")] public RobotState SequenceStart { get; set; }
        // The trajectories that the planner produced for execution
        [DataMember (Name = "planned_trajectories")] public RobotTrajectory[] PlannedTrajectories { get; set; }
        // The amount of time it took to complete the motion plan
        [DataMember (Name = "planning_time")] public double PlanningTime { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionSequenceResponse()
        {
            ErrorCode = new MoveItErrorCodes();
            SequenceStart = new RobotState();
            PlannedTrajectories = System.Array.Empty<RobotTrajectory>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionSequenceResponse(MoveItErrorCodes ErrorCode, RobotState SequenceStart, RobotTrajectory[] PlannedTrajectories, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.SequenceStart = SequenceStart;
            this.PlannedTrajectories = PlannedTrajectories;
            this.PlanningTime = PlanningTime;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionSequenceResponse(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            SequenceStart = new RobotState(ref b);
            PlannedTrajectories = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < PlannedTrajectories.Length; i++)
            {
                PlannedTrajectories[i] = new RobotTrajectory(ref b);
            }
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceResponse(ref b);
        }
        
        MotionSequenceResponse IDeserializable<MotionSequenceResponse>.RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            SequenceStart.RosSerialize(ref b);
            b.SerializeArray(PlannedTrajectories, 0);
            b.Serialize(PlanningTime);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (SequenceStart is null) throw new System.NullReferenceException(nameof(SequenceStart));
            SequenceStart.RosValidate();
            if (PlannedTrajectories is null) throw new System.NullReferenceException(nameof(PlannedTrajectories));
            for (int i = 0; i < PlannedTrajectories.Length; i++)
            {
                if (PlannedTrajectories[i] is null) throw new System.NullReferenceException($"{nameof(PlannedTrajectories)}[{i}]");
                PlannedTrajectories[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += SequenceStart.RosMessageLength;
                foreach (var i in PlannedTrajectories)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceResponse";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3b9d4e8079db4576e4829d30617a3d1d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRrb+LiD/YbAGru2trTR2mu3qwh8UW07U2pIrydmmQUBQ4kjimuKoHMqyurj/" +
                "/T7nnBmSouWmi914cYGbFo5Jnjlz3t9msqfaqdJZZjI1MZFWmZ4mepLH6Uyt52Gu1jrFj8yks8a1udfd" +
                "vEOw5wC1siygZY3GnhrNtZqukkTZPMwYAX7JtTJTleNTZsYmV8BIDwziv1j960qnE90YEMiQF/l3AUN6" +
                "9HkW/h3EmSzG7jmRR+uXSZimOlPLzESriY7UFMzoBz1Z5bFJBevIr9x8+uwWREEVnd8iXJhVKqTFC61i" +
                "bGHMHX5APItlokEb7bkwhJsxNaaJCfM3rwUt+A5oZeNF4+zf/OdF43r4roWt73WcBws7sy/rKnnRiNP8" +
                "9ETdhwkxhK9ZCI2M9Ty8j03mvg5vz887w+HZK/d82e5e3Q46Z3+lPw338uaq3et1e+8C+tq5ODv20N3e" +
                "h/ZV9yK47o+6/V5AcGfHJ+5j5WXgANujzkXw9mPQ6X3oDvq9605vFJy/b/fedc6OT92y835vNOhfFXu9" +
                "du9ve+23V51g1A/aP912B51g2OkN+4MASNtnx985qFH3Glv0b0dnx2889YNO5/oGO58d/4Uk4XWj/kvd" +
                "xalehHk8sTB2WJnNxZCtl86oPRgF+DnqgIXgvH911R2CKUjg2x0gH7r9K/w9DG7ao/eA7g1Hg3a3NxoC" +
                "/pUX5rt++6qO7KT67fewnFYBK5/8ItLN60ZNO+8G/duboNe+hpRffVf/WMMEkDc1kEH/bd+xiK9/qX29" +
                "6vZ+9Mi/r33rv/2hcz7yX2FPe8pubK4X22K+HAAgAAG94WV/cB14Izw+8YZWCAvm0jn/kWwR9vABcGQU" +
                "APQSrNBKP/mbF5ozmG7vsl98g7D2qmawRVevH3R/DIb9q1uyZJjoq+dx5TL4veBQFFu10NaGM43Ik+Zh" +
                "nFoVpwhtRDRCTzg2q7wSWTnYHqm4qZsSFY2NCdBSLItzq/5uwKBVYRqpJE7vbMPq1CJ+8+4/0EcJvQxH" +
                "cTfnsM5fXLRdhBuFSILwt0ryGNFQXfQvVZghXi/1JJ7GCL5znekt1NcEC7jKFrw8iMw0qG3WzvNwMgeW" +
                "iUmS2BKfZkwx2qqD0H9DLLYGsZm4UIAoZHDY8OvP/fI+r0bM96uRrtynwGGmfS+TcAbpRvEkdKlPA2sG" +
                "1FCDnegUmQAyMIikAMt1tsyQCSIVQp4qiqdTtY7zOeIJySEvKDSMhNd7nZJajc2TjYoXS5PlIZIN5as5" +
                "9AKGZsxNwerYRJTqDjw9AEyha8pGiQ6zXcAU9qegSmyr1ZqYTLdaldQ61thPq9UyEl6R4pj4vGJyh42x" +
                "MQmIDYi5r+cAu02wcAD8HxZewBY4N0lkFSgPSQjIeJMshk5cTSHVRgi0ksFNhvjO7pNB6xCR+EBTNfZc" +
                "ui8WachQPquDTN+bZEXvqaiILYeJQ6Im0lOEDYh60wIC9ectT/P1jMcSRoRgcXhUgt7rxEzifPMY9KVl" +
                "4Jf2kD20XKKnUFcu3JM8lsuE3CxOqwh6C1rdO2wyY52SF6xYpTGkQAYXoZYTJx3jCT6VhihURBBzHUZk" +
                "q86PyeG1lEBUBM5jYCz3Y6lZ1Iawo0zDwiIdNVUblUYdBthBqYH/eDVy/BBP8kuLqhDk8J7CRj0G2tiC" +
                "ZjFuH3/CLAs39oh34LKPWF9WissKMaR25nVmQrJtomIR3mlZ5ODBO1mYWZJGw6Sp/jbXKJCbs6bamFXm" +
                "oyhzkRogdPoJrYVmsUvkvUkvjmiJmoSpgrMS16U6mW6lF8t846yRpCfciG4rvNu5WSWIrB4Hy8nGvyHc" +
                "g2UIUvBUvIagTAqdr7EL2CxsoCCzIhyygoJoCDqDXnLK2qzBZqPReC/GITbSaNg8Q+BAUGX7ceUv1dXO" +
                "ESqvvMFXXokEvmJMySMJKEI1BRNElTQKswgSzUMOHhxy4xli6nGiQSQxu1hCeRJaNktivJTnDBGcKumN" +
                "WlnJQOgGFpAq8oWY7NZ6sfqQDTGerBJE6omBqccpgU8ziI2wk4xdn6O6Fy22ce5aQBAcNJ1kOrQUorsX" +
                "qrGS8gQLGnujtTmmNDSjFOU3L0KEfkB2skRnaClM/VmYawI3RVzsAgM/4HcBHhFxsAlI0EsDPzgA5Teb" +
                "fO4S632YxeEYngbEE0gAWPdp0f5hBTOR3YI1pMajF4zlHn8EbVrgJZ6Oi4xoVzMIEIBo8O4RxDh8saki" +
                "EsJ+k3ichdmmwdGKt2zsXZKMxYtYIxQ5tz3UWbFoI4ij50lyj4shss+BJo2BFyntEIokL5GVuoDD/lmE" +
                "vUjPMo0IDMgpfokMYg3wTJHlzNpXEWBwNclXiNEAKzeU4NqVbGLtakEGTaYT+pxBputKdo4Gdsk6hdXD" +
                "NbIwtVSFypqZzsucBLRhYtzuRX2tJnMUr011yV05tJMgboXckUGtLoGhkMK2t4OLS86zp1RXHjwggOL/" +
                "cE02QVkR5mO1fKSoSpGvYutV6kSQ+CuLgUXWUpbZ+g6sAuGxwXbRMZOFjMMJN/1bNPx/an3e1LrOEBrn" +
                "fzi1evD/S6n1qcwqfREtt42ZRi+RZxuJICNvwoAqzPkR0Bq1EgHQ37Vvf2Mx4aPI6+vFvSfoLqr7zEc9" +
                "V2wWkWWs87WGaeRr8yhvsgop5qFUDScIZo0PPMI7lfWJOPZPKyzIUooBmZGo+lx8OnJ2cRmiHKKPNRZU" +
                "EY7ZrhaaekJYVrGSe0yyHLDRJDfLuIVDs5+ryEAk6Ak5lsHhKNlwfU1BGUZZFQu9xpIDcrkj6nFTgaKE" +
                "wUULlzmI2Fk8i6N6MOXw77g7Uvn0BIYNx2KaZTNoEUi8wA+bqjtlN10TQ+zivnGjls3RxVUAZqxHVFo5" +
                "FNsSvWFX8h6LOUgOb4Hi/dz1ofitKDPVb8+k7dLQdiocuRzDZZ/Xt9ROT7+WZkpy/hJPxW/rZ3Naih8F" +
                "Zz7Z2rKP3WZpnJk7GBXURYZmaUJDUwpKv2E64zqYEggCn3daB1I+O7jnYlCC4S7dQSGipJK/I3gX6OdM" +
                "RDxSmf/HuGRk5aMMJp5nrvjESExY1qr2Wnx6XJkqcfZCwxQ/+PkJOTAnURrA+TKafud8hXEai7IYWvKM" +
                "CnUoCgM7D9FYsaTQK+I3KnDzx1Q09iRGVEd+BMYb7iGqAB87FGmMmGWUqXHDwiaCF83tdI7Sj0KUG/Tt" +
                "7cLnx37SVhRsFIKQDaJGXVCC1B8buYkTD1bLk6liPz/EpFEpKDJrR4NZIQHv0caYLIWo7tVxQZiQQUV6" +
                "gj4w2sgIBGWCUOoWlCUDIwtktEsZZaomqGWhAkcVT2tQddMJWb07hTa4URB9UCYSJjkLLQhHwQpQuwSC" +
                "6KYhdvTzktUmCfp/SiUZjtDIFxyWwyM/JuM9Uj2hUJ5teLdMo4CjZdT2UWklG5P6gNPPk4uTOue23M2U" +
                "Z3oQBlEXuC28UtYaHX5RrtaUgew5VXepWZcHeAL/PG65wx3brgzkTCinmMWs2fd07Db1qlG4hdE7Tp0M" +
                "D9iAGBcUKEeFh95f0fC6dV7VmHmIXVCSHodIxsYJqHAg+Tugud0s5VGPMCM8jAgDofGYyyG+C7o0V9xR" +
                "zXu3lWUxmyNBwljqY3Wu0StnDV4AQ5OAf7/NhMa2CxB4D2Fx0BE6GerGf6KJVQlWrzns1ncyLTkkvtZ2" +
                "vo2V3gB2IR924qFvJYq35B+kBOqBabyPIZCrEGzJ3ZEauwMeBvM9qhQUYH4lzgaNRRHrAgGWtjis0nZD" +
                "S92R95NM0reSunZEvX7FMETqxQkqH2dwbVcBgo3igHllUQXqB9QMRD5KTUmpHHOajfEGRXz74uLsW9pm" +
                "wHF1a6dpZmisgKYrvY9x5WBBtS/NrBEkNpASenNMkMQV+DQqhz/bmk3E0aHsNOhc9z90cMhNPC2XFKqo" +
                "hPXW7GYeLrYy0a49/BKvvuSWRZ5PaKFk8uam07s4O3FxuNxz93a8yxEC49pZvlM11/4HBOH15tvYxQqn" +
                "1oBI9DSXFpVGJAho1iQkK4jWR4wyoOK0BJKMhESWzSkR2F9itOkrfODEIxWjHtD4z18vLn45rCA8/tN/" +
                "lBxE0/D0n1/s/pB8zn//9JXjJh9KTDntuViGSEazKmphURvwpI5KR2hRZzQemcmBXjE9kJMlmAqd322V" +
                "Fne6OC6q7tDiN7K+PNrh3pItBkErVdHYx3tg8QijcZUUl2Z5fPbDsN97Sfdc3EztY/v6SgkCHO8UVoxI" +
                "W/hApemkWO2lUs4NJbX7nNJUHa4d6FDokdbZlXimQ1dukvhOt9Sf/rFPEt5v7Z9TfXPxdv9I7WfG5Hgz" +
                "z/Nl6+VLtCJhAmnn+//zJ2ERiQP2jnqQB3qpC46iPVfjkHIqUqD6Mc73sShG1Q9HuNPaTdRxH+ohHscJ" +
                "2h2XoXYZLB2zihB9E33xVmyDkRBX5PpuZxmFkXGtICeKcjIY5Xk9DUods3zKyGhaqhAAvyMR4F1dBK3v" +
                "/vr9a4Gg7CszA8A9pnjf7TT86QqnqqgS6Gi10NPWxsNfk/ceQnDzVmp/PbOnb+QNnWW31HevT0/4EdAZ" +
                "AaCINmsHgcy/xjCn9pqKFGLEb+CP5eXrAle6EvrOc4LcLPe9QcO0v97E/qmSgcq0C/HUscFo2C7J2I7U" +
                "ZIMam0s3WJxWbubo2x1Yhj84hmX5WSPqnLEvBICMwj4ldnZGqaC/PcJ/GArQ0c/36m3/ZyQz+X14876D" +
                "uzIn7vH8Iy7kXHQGCOjuRb/XOXtdXJtzIYpzDdHkoKRW81EBpyfoL9yFkRK0PLgrIfwaGlUR+dUFFbCW" +
                "jH+pb+F7DiIESdgkrgcfrPbLNfuS4vhShmsOwTiTKm3Ez0fqo0z0f6nSTELmzkmnM5SMjqJ6GKL+qeAP" +
                "Qm+Wsg1+Rl1SPn0sZE1Pv1Aur5Ak8ndU8QSM1E6RE3+7Y3cEUKETQY2isfCNo/54RSS4fkcsyNMheINB" +
                "+6J7O6Q6qbKnVzLjJAXLMaVIRUyHRxE8SPRFIp/HuK1+USHKjqYqJ4hbeIP3ne679yN1QLjdw2HJk9wr" +
                "qUi85Gm+1Wd5X1AH5AuHsh+FOr+PcOf2kYfKPk/tQpNFLztRn2tRdu+JrC1TAf+JLgEW1X7dJ+nsJ864" +
                "F+YBK04gl6UNsUxpPXWdZO+r5ZE76frGCdU7aU2YhUnVmKeqtPTUR8ClYAD4TJMwagakC80enUZSVVof" +
                "hbHCqEiQ73IDhgRemXhitC3D2+JmQGUwX4F7Ph5BTDHs25pQVS/xYDTiD0JLjr8wmn2OXESNZpGBKtRS" +
                "d0mRAs22eCHO1tMZqon/roRaXAnGXVe6302XBkzlfiDYpFNPFD720+cGbTJyCPiIyeGiDSqjPL/Ct2Ko" +
                "AldUMPFFn/mjRhPi5NsgsujZpOUZ2SU1zxmKvoIuuWTx6VRI1Q8BDwefiWDu1XdfB5DjcnidNP3lUKCY" +
                "HIQP6hsaC36jJr/hR6TOFLfZoWqdwdL19NO3n2nSWDy+osdJ8XhCj1HxePq5OIv49Pozv/t6MvjCdO9F" +
                "bd6185C0tsZbHDuy/Y+RXgQcvpJXArsAU962w1yQ2sHCKT8d+TMWfMVDOJlgUCqNuP1MujLb0HKr6nMx" +
                "Ua/sJflN/kEEjShccVpMTFxkoJToBxI0OKSreTjVsLXjbI4XNTab4L2x4yKYfXwTjG6oli+32Hp8Ryxa" +
                "+ckECoKAxkPu34M8343sqhl+aQot9lhCPVqwdfunsrB+H7qC4tks9wnaXmzfD3WFLd9wZgNyN7Fqp/Tu" +
                "xg3/cyLUmnQXjG9n7bBOr+MdU+1dpYi7+3kMYfnTqBLXgVz/9Ddf6/eBDr0jCkR5V6mCgo99cHktWUXE" +
                "hbtNUqlx7MvSkl9u2++eu6PsfIa9yPUj7lWBreJP0iRsLRLX82A0M6eWsu6HjAyV5854+IQ6/1Nx8ffI" +
                "KeJjXbM0NPFWVr15dkCVhlFv6F8fHP6x6zENPxdyo1W6iuGOsMtA6K0TlemMBlTVa05PXLCpRLVHW8B4" +
                "Kubxr+2zbWi/Fxj/F2RmMcbRNwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
