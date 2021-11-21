/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupResult")]
    public sealed class MoveGroupResult : IDeserializable<MoveGroupResult>, IResult<MoveGroupActionResult>
    {
        // An error code reflecting what went wrong
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public MoveitMsgs.RobotState TrajectoryStart;
        // The trajectory that moved group produced for execution
        [DataMember (Name = "planned_trajectory")] public MoveitMsgs.RobotTrajectory PlannedTrajectory;
        // The trace of the trajectory recorded during execution
        [DataMember (Name = "executed_trajectory")] public MoveitMsgs.RobotTrajectory ExecutedTrajectory;
        // The amount of time it took to complete the motion plan
        [DataMember (Name = "planning_time")] public double PlanningTime;
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new MoveitMsgs.RobotState();
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory();
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupResult(MoveItErrorCodes ErrorCode, MoveitMsgs.RobotState TrajectoryStart, MoveitMsgs.RobotTrajectory PlannedTrajectory, MoveitMsgs.RobotTrajectory ExecutedTrajectory, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.PlannedTrajectory = PlannedTrajectory;
            this.ExecutedTrajectory = ExecutedTrajectory;
            this.PlanningTime = PlanningTime;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MoveGroupResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new MoveitMsgs.RobotState(ref b);
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupResult(ref b);
        }
        
        MoveGroupResult IDeserializable<MoveGroupResult>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            PlannedTrajectory.RosSerialize(ref b);
            ExecutedTrajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (PlannedTrajectory is null) throw new System.NullReferenceException(nameof(PlannedTrajectory));
            PlannedTrajectory.RosValidate();
            if (ExecutedTrajectory is null) throw new System.NullReferenceException(nameof(ExecutedTrajectory));
            ExecutedTrajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += TrajectoryStart.RosMessageLength;
                size += PlannedTrajectory.RosMessageLength;
                size += ExecutedTrajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "34098589d402fee7ae9c3fd413e5a6c6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRrb+LiD/YbAGru2trTR2mu36wh8UW07U2pIrydmmQUBQ5EjimuKoHMqyu7j/" +
                "/T7nnBmSouWmi914cYGbFrFJnjlz3t9m0mpdmTvdK7p5bvIzE2urNP0aRPi91Wot8DUpgoWd2ZdDMzHF" +
                "qAgLrYo8/LuOCpM/BLYI82Ib5LiEUcs0zDIdB9WyLyzQ9zpaFc0V09SExZvXgi7JZkGRLHTrRev03/zn" +
                "Retq9O5E1QlsSulFK8mK4yN1F6at1o7C1zxMUzXR8/AuMbn7Oro5O+uORqev3PNFp3d5M+ye/pX+tNzL" +
                "68tOv9/rvwvoa/f89NBD9/ofOpe98+BqMO4N+gHBnR4euY+1l4ED7Iy758Hbj0G3/6E3HPSvuv1xcPa+" +
                "03/XPT08dsvOBv3xcHBZ7vXavb/pd95edoPxIOj8dNMbdoNRtz8aDAMg7Zwefuegxr0rbDG4GZ8evvHU" +
                "D7vdq2vsfHr4F5KE1436L3WbZHoRFklkVa5/XWlbiG1ZL51xZzgO8Pe4CxaCs8HlZW8EpiCBb7eAfOgN" +
                "LvFzFFx3xu8B3R+Nh51efzwC/CsvzHeDzmUT2VH92+9hOa4D1j75RaSb162Gdt4NBzfXQb9zBSm/+q75" +
                "sYEJIG8aIMPB24FjEV//0vh62ev/6JF/3/g2ePtD92zsv8KedpR9sIVebIr5YgiAAAT0RxeD4VXgjfDw" +
                "yBtaKSyYS/fsR7JF2MMHwJFRANBLsEYr/c3fvNCcwfT6F4PyG4S1UzeDDbr6g6D3YzAaXN6QJcNEXz2P" +
                "K1dh7AWoG88Tqxba2nCmVWSyIkwyq5JsanIi2mQqnJhVoYq5VjmtVAh4hT5QSVu3+e3S2IQArTJTlRRW" +
                "/d2AQavCLFZpkt3altWZRUjl3X+gjxJEGY7iZ4FIu6P4iwXKsFCL8EEhkmi1WKVFsky1Oh9cqDDXyi51" +
                "lEwTHau5zvUG6iuCBVxtC14exGYaNDbrFEUYzYElMmmaWOLTTCjSWrUX+m+FUdYsNHOhAFHKYL/l15/5" +
                "5QNe/emz8quRQdynwGGmfS/ScAbpxkkE4SJKrOcaWHOghhpspDOt8As2nuCXrND5MtfIAyqEPFWcTKdq" +
                "nRRzxBOSQ1FSaBgJr/c6JbUaW6QPKlksTV6EWaGgVIg1i8HQjLkpWZ2YOEHu2/P0ADAzjD9KdZhvA6aw" +
                "PwVVYlsnJ5HJ9clJLUlONPbTarWMhdekEOKLmsnttybGpCA2IOa+ngNsN8HSAfB/WHoBW+DcpLFVoDwk" +
                "ISDjRXkCnZAc2IiEd6sL+gX5GfGd3SeH1iEi8YG2au3wFrVFGjKUz2ov13cmRZ4ncS/zxHKY2CdqYj1F" +
                "2ICoH06AQP15w9N4Szx7LGFMCBb7BxXonU5NlBQPj0FfWgZ+affZQ6slegp1FcI9yWO5TMnNkqyOoL+g" +
                "1f39NjPWrXjBilWWQApkcLHOCnHSCZ7gU1mIQkUEMddhTLbq/JgcHmJFIQP7gjskwFjtx1Kzag1Xh83D" +
                "wmIdt1UHlUYTBthBqYH/eDVy/BBP8ktpC9YgyOE9hY1mDLSJBc1i3D7+hHkePtgD3oHciNW4ROm3KWEm" +
                "htTOvM5MSLZNVCzCWy2LHDx4JwszS9JomLbV3+Y6U7o9a6sHs8p9FGUuMgOETj+htdAsdom9N+nFAS1R" +
                "UZgpOCtxXamT6VZ6sSwenDWS9IQb0W2Ndzs3qxSR1eNgOdnkN4R7sAxBCp6a1xCUyaDzNXYBm6UNlGTW" +
                "hENWUBINQefQS0FZmzXYRoX7XoxDbKTVskWOwIGgyvbjyl88ekeovfIGX3slEviKMaWIJaAI1RRMEFWy" +
                "OMxjSLQIOXhwyE1miKmHqQaRxOxiCeVJaHlYEuOVPGeI4FRJP6iVlQwUmcUCUkW+EJPdWC9WH7IhJtEq" +
                "RaSODEw9yQh8mkNshJ1kTBVoFmnVOz9hG6cGIwFBcNAsynVoKUT3zlVrJeUJFrR2xmtzSGloRinKb16G" +
                "CH2P7GSJztBSmPqzMNcGboq42AUGvsfvAjwi4mATkKCXBn6wB8qvH4q5S6x3YZ6EE3gaEEeQALDu0qLd" +
                "/RpmIvsE1pAZj14wVnv8EbRZiZd4Oiwzol3NIEAALnNzhyDG4YtNFZEQ9psmkzxEJ8bRirds7VyQjMWL" +
                "WCMUOTc91FmxaCNI4udJco+LIbLPoSaNgRcp7RCKJC+RlbqAw/5Zhr1Yz3KNCAzIKX6JDWIN8EyR5cza" +
                "VxFgcBUVK8RogFUbSnDtSTaxdrUggybTCX3OINN1JTtHA7tkncLq4Rp5mFmqQmXNTBdVTgLaMDVu97K+" +
                "VtEcxWtbXVB4vod2UsStkDsyqNUlMBRS2PZmeH7BefaY6sq9ewRQ/B+uySYoK8J8rJaPFFUp8tVsvU6d" +
                "CBI/8gRYZC1lmY3vwCoQHhtsFx0zWcgkjG6J4Q0a/j+1Pm9qXecIjfM/nFo9+P+l1PpUZpW+iJbb1kyj" +
                "lygw0+IIMvYmDKjSnB8BrVErEQD9bHz7G4sJH0VeXy/uPUF3Wd3nPuq5YrOMLBNdrDVMo1ibR3mTVUgx" +
                "D6VqGCGYtT7wIO5Y1qfi2D+tsCDPKAbkRqLqc/HpyNnGZYhyiD42WFBlOGa7WmjqCWFZ5UruMclywEab" +
                "3CznFg7NfqFiA5GgJ+RYBoejZMP1NQVlGGVdLPQaS/bI5Q6ox80EihIGFy1c5iBi58ksiZvBlMO/4+5A" +
                "FdMjGDYci2mWzaBFIPEC32+r3pTddE0MsYv7xo1aNkcXVwGFMQdUWjkUmxK9ZlfyHos5SAFvaVdz1/vy" +
                "t7LMVL89k7YrQ9uqcOTynGoUkeCG2unp18pMSc5f4qn8bf1sTkvxo+TMJ1tb9bGbLE1ycwujgrrI0CxN" +
                "aGhKQek3zGZcB1MCQeDzTutAqmcH91wMSjDcpjsoRJRU8XcA7wL9nImIRyrz/xiXjKx6lMHE88wVnxiJ" +
                "CctaNV6LT09qUyXOXmiYkns/PyEH5iRKAzhfRtPvnK8wTmNRlkNLnlGhDkVhYOchGiuWFHpF/EYFbvGY" +
                "itaOxIj6yI/AeMMdRBXgY4cijRGzjDIzbljYRvCiuZ0uUPpRiHKDvp1t+PzYT9qKko1SELJB3GoKSpC2" +
                "/BxJJk48WHWlZn0054eYNCoFRWbtaDArJOAd2hiTpRDVvTosCRMyqEhP0QfGDzICQZkglLoFVcnAyAIZ" +
                "7VJGmaoItSxU4KjiaQ2q7kimRxvdKbTBjYLogzKRMMlZaEE4SlaA2iUQRDcNsaOfl6wWpej/KZXkZsW+" +
                "4LDsH/gxGe+R6YhCOY7TaLdco4CjZdT2UWklG5P6gNPPk2sHe9WssHYwF6OvB/NuC6+UtUaHX5arDWUg" +
                "e07VbWbW5bjCwT+PW25xx44rAzkTxiydctbsezp2m2bVKNzC6B2nToZ7bECMCwqUo8J9769oeN06r2rM" +
                "PMQuKElPQiRj4wRUOpD8DGhuN8t41CPMCA9jwkBoPOZqiO+CLs0Vt1Tz3m1lWcLmSJAwluZYnWv02lmD" +
                "F8DIpODfbxPR2HYBAu8gLA46QidDXftPNLGqwJo1h934TqYFIOx0pe18Eyu9AexCPmzFQ98qFG/JP0gJ" +
                "1APTeB9DIFch2Iq7AzVxBzwM5ntUKSjA/EqcDRqLY9YFAixtsV+n7ZqWEiO80xNM0reKuk5MvX7NMETq" +
                "5QkqH2dwbVcDgo3igHllUQXqe9QMRD5KTUmpHHParckDivjO+fnpt7TNkOPqxk7T3NBYAU1XdpfkJltQ" +
                "7UszawSJB0gJvTkmSOIKfBpVwJ9twyaSeF92GnavBh+6OOQmnpZLClVUwnprdjMPF1uZaNcefolXX3LL" +
                "Is8ntFAxeX3d7Z+fHrk4XO25fTve5QCBce0s36maa/89gvB6823sYoVTa0CkelpIi0ojEgQ0a1KSFUTr" +
                "I0YVUHFaAknGQiLL5pgIHCwx2vQVPnDikYpRD2j8568XF78cVhAe/+k/Sg6iaXj6zy92f0g+Z79/+spx" +
                "kw8lppz2XCxDJKNZFbWwqA14UkelI7SocxqPzORAr5weyMkSTIXO7zZKi1tdHhfVdzjhN7K+Otrh3pIt" +
                "BkErU/HEx3tg8QjjSZ0Ul2Z5fPbDaNB/icm2n6l97FxdKkGA453SihFpSx+oNZ0Uq71UqrmhpHafU9qq" +
                "y7UDHQo90jq7Es90jLlF1XKrT9Sf/rFLEt492T2j+ub87e6B2s2NKfBmXhTLk5cv0YqEKaRd7P7Pn4RF" +
                "JA7YO+pBHuhlLjiK9lyNQ8qpSYHqx6TYxaIEVT8c4VZrN1GfpvDWSZKi3XEZapvB0jGrCNE30edvxTYY" +
                "CXFFru92llEYGdcKcqIoJ4NRntfToNQxy6eMjOZElQLgdyQCvGuK4OS7v37/WiAo+8rMAHCPKd51O41+" +
                "usSpKqoEOlot9bSx8ejX9L2HENy8ldpdz+zxG3lDZ9kn6rvXx0f8COicAFBEm7WDQOZfY5jTeE1FCjHi" +
                "N/DH8vJ1YeJVSt95TlCY5a43aJj215vYP1UyUJl2Lp46MRgN2yUZ24GKHlBjc+kGi9PKzRx9uwPL8AfH" +
                "sCw/a0SdM/GFAJBR2KfEzs4oFfS3B/gPQwE6+vlevR38jGQmv4+u33dxV+bIPZ59xIWc8+4QAd29GPS7" +
                "p3zNZVwLUZxriCYHJbWajwo4PUF/4S6MVKDVwV0F4dfQqIrIry+ogZ3I+Jf6Fr7nIEKQhE3iuvfBarda" +
                "syspji9luOYQjDOp0kb8fKA+ykT/lzrNJGTunHQ2Q8noKGqGIeqfSv4g9HYl2+Bn1CXV08dS1vT0C+Xy" +
                "Gkkif0cVT8BI7RQ58dMduyOACp0IahSNhW8c9ScrIsH1O2JBng7BGww7572bEdVJtT29khknKViOKUUq" +
                "Yjo8iuBBoi8S+TzGbfWLClF2tFU1QdzAG7zv9t69H6s9wu0e9iue5F5JTeIVT/ONPsv7gtojX9iX/SjU" +
                "+X2EO7ePPNT2eWoXmix62Yn6XIuyfU9kbZkK+E90CbCs9ps+SWc/Sc69MA9YcQK5rGyIZUrrqeske18t" +
                "D9xJ1zdOqN5JG8IsTarBPFWllac+Aq4EA8BnmoRRMyBdaP7oNJKq0uYojBVGRYJ8lxswJPDaxBOjbRne" +
                "ljcDaoP5Gtzz8QhiymHfxoSqfokHoxF/EFpx/IXR7HPkImo0ywxUo5a6S4oUaLbFC3G2ns1QTfx3LdTi" +
                "SjDuukK5U7o0YGr3A8EmnXqi8LGfPrdok7FDwEdMDhdtUBvl+RW+FUMVuKKCiS/6zB81mhAn3waRRc8m" +
                "Lc/INql5zlD0lXTJJYtPx0Kqvg94OPhMBHOvvv06gByXw+uk6a+GAuXkILxX39BY8BsV/Ya/YnWquM0O" +
                "1ckpLF1PP337mSaN5eMreozKxyN6jMvH48/lWcSn15/53deTwRemey8a866th6SNNd7i2JHtf4z0MuDw" +
                "lbwK2AWY6rYd5oLUDpZO+enAn7HgKx7CKMKgVBpx+5l0ZTah5VbV53KiXttL8pv/twttX5yWExMXGSgl" +
                "+oEEDQ7pah5ONWzjOJvjRYPNNnhvbbkIZh/fBKMbqtXLDbYe3xGLV34ygYIgoPGQ+3cdz3cju26GX5pC" +
                "iz3W/m1Ic8HG7Z/awuZ96BqKZ7PcJ2h7sXk/1BW2fMOZDcjdxGqc0rsbN1rSDV9w49tZW6zT63jLVHtb" +
                "KeLufh5CWP40qsK1J9c//c3X5n2gfe+IAlHdVaqh4GMfXF5LVzFx4W6T1Goc+7Ky5Jeb9rvj7ig7n2Ev" +
                "cv2Ie1Viq/mTNAkbi8T1PBjNzKmlbPohI0PluTUePqHO/1Rc/D1yyvjY1CwNTbyV1W+e7VGlYdQb+tcH" +
                "+3/sekzLz4XcaJWuYrgj7CoQeutEZTqjAVX9mtMTF2xqUe3RFjCemnn8a/tsGtrvBcb/BSnsJpc7NwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
