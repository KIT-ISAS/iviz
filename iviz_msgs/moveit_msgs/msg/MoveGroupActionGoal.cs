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
                "H4sIAAAAAAAACu1d+3Mbx5H+HX/FllV1JGMakiUnlWOiVMmibCtlPSIqfsSlQi2ABbkhsAvvLgjRV/e/" +
                "3/f1Y2Z2AVr2XcjcVc5xhdjdmZ6Znn53z/irIp8XTXYhf0b5rCvrallOJ6v2vL3/ZZ0vn59m5/gzKeej" +
                "F/VV8WVTb9Z8L29Hj//B/4xenH15krXdXCfwlU7rXnbW5dU8b+bZqujyed7l2aLGrMvzi6L5ZFlcFUt0" +
                "ylfrYp7J1+56XbRjdHx7UbYZ/j0vqqLJl8vrbNOiUVdns3q12lTlLO+KrCtXRa8/epZVlmfrvOnK2WaZ" +
                "N2hfN/OyYvNFk68KQse/bfHjpqhmRfb89ARtqraYbboSE7oGhFlT5G1ZneNjNtqUVffoITuM7r3d1p/g" +
                "sTgH7sPgWXeRd5xs8X7dFC3nmbcnGOM3urgxYAM5BUaZt9mhvJvgsT3KMAimUKzr2UV2iJm/vu4u6goA" +
                "i+wqb8p8uiwIeAYMAOoBOx0cJZA57ZOsyqvawSvEOMYvAVsFuFzTJxfYsyVX327OgUA0XDf1VTlH0+m1" +
                "AJkty6LqMhBckzfXI/bSIUf3viCO0Qi9ZEfwN2/belZiA+bZtuwuRm3XELrsBunzlqhxL1MIadlks/ai" +
                "3izneKgbTlnpKcNebi9KbIgsguySbfM2a0gwLRZBAnou+y0kCZTklQ2GTW6uQBrbi6LKyi7DQouWRAu6" +
                "KFbrLgPC0ZswW6WabYGhA+hsWoA/MIVsVjRdjp3jjFL82vzLue8J0IvpYVvqiOdsURTzaT67xMzm6AGi" +
                "3Cw78GDb5ueFbELWrotZuShnukCbQTs26GQQbYBJrTZth5ll4Dq0Gvv+ceduaetWkFhlp/vWk14jCDMi" +
                "+vUyr97onH3umAzfVpjaqzXbgGzteVLri7uZ7mB+LsxIGiVYE2zKneX+F9m8WJRVKaRDwZj7Yrid/L4S" +
                "aAAhSwFlYUPlQ73p1hsQYRfogKTxOidPdUXTCjg23NbNZbvOMbDQG185LCN/toDMawFk9K23TiAFCJN1" +
                "eDlS2Q4hSzHRkeY2a/A7hHf2fBGo9+81hGXrA4GEKGgxTlOQwjCddd3K6tusxsw4Y/CSSJhN01DE1FXR" +
                "HvNNWySNFSBAAGRbdFCDTTF6U0/r7kzmghk13UTm5RyDzm1JuSf0Lp8iklb1HJqIfArU8O04e5ZDAhTL" +
                "YiWzWFA2omHeNOA12TXhPGOec8xKWUdeUObNLkqoN86xXOiKMPGuyQUhuteJklIQAI+5513Zku9GT2OP" +
                "H96pNk+AcGEva0M+UJlX11gkvkDI1NgV2ekcnE4pwt9NPd/MyM5CU7rUbblcZldlvRRtKlhO53koIjBf" +
                "r5cmxyC/dBBsSlV32d8pGiB+9N1ROmUZfDhhoigdgBNT6URCwtu/FzNI42uVOYqK69Hb8D6FH1vvG6Wi" +
                "GqoV9cmKSfdYByS3EGpVa0Pyp8u7Y5Im9zgXodnvKwijLITod0Fo30SV7Q5+TtnFB2MGAEvUi8PFz3oN" +
                "S4d4cLjSc0JYAe5mNUVjQqbxEUGwP5YlHLaCYICWys4u6gZEDjTWy42JEZ9+A5OjEV2mxg0AT4K0zLuO" +
                "yiqgcpW/L1ebVZav6o0wg+rJPZglsSyX9VYtNWcmMUHMPjkaLZZ13v3uM28YhxVVR7kCs0TsA+jU2iSZ" +
                "CnCxAyE7NjOhlmRqgtsM1hsUYEdaVcWXz2ZgYWIVBDLOsic2uat8CSkr7IapHT44/vQdvr69CeD1HnCJ" +
                "fjQGayh/TJQQ8opUTc4xs6kEgkDr6EY7E+BsgRgZlAgFQgHIjpDtFJIEWjacK7R9k1fnmPFhueLG5VW3" +
                "vKZYLFuxeGfLDS2BaSHiGAIR2H8wfnAkktPGERrXT6Z5hL4FFdzTT8cPBBbEuyL6sBwX4+ObMAKA/S8p" +
                "co7GYZvRaOKdJq1u7URn1GuTdh+2uwu9vUfzueZ2uylo7lwQCZxFhUiBsNhA9JPL8FXkGfheTG7wH/jl" +
                "MM+m9fsjkosLACeaPt9wWqkPZKBhy16rZeksI6yxqqcl1BqVCWTIgr5NBAy7dVssl2Py1aloLaUIMdLR" +
                "uCkWUJ30GFwVYopYaANubkd9J9OlQQlhgV3XRi7l0O1YDUsjcacznYcoZJGVcFRG50UNrEFuC+a/ESH+" +
                "iIAnCnQoeP7xQ4HgfKjbIa294/qeQvpi6jAsSEpX8o1UsYCTkAkNjrPgY8iurwpwPJcZelJDlQ1FiVke" +
                "jXgOx7QI5zVkAbQzYKzyS1pIcPBEe0ORQ5ZRc1btUsUYXqPLYTE+B68LdUkrIlDcaXHA4SY05TnEkPTE" +
                "QKvQOc9scZA1i4eqYGTOOhj2hQ5I3ZlcoKi7rjewGbAG/GjM7xfd5fMS/7Sr62OSvoHoI/S1iBxnTPBk" +
                "BxoFlbtEeR9+XYdfP92BFIk26I3Co6wi/vIpZH2fdjvuIQSv2mzRPF5gm1q3IegFQEBetiPubd3o4H/m" +
                "RzWApV00gOWLeZwrGLAX+VXQT0V2+uoLtTyDRhN7OgX9gm3RLhlCuk/m9WIyGOxJ18H6BZRZvVyWLddZ" +
                "T2mpQX3l/g0b3mJDZRU0iQIOjkbe/6l3fyW9YQJ7b/CufZoYZI77xTKHU1rNGRoi+YKYzaeg5puBjqOV" +
                "JGoOrESfGzKSzLRY7AgVmaE6JtI/kcmruoUKzoI61riWB07EVfClTus5LZJDnw8a0nRmKGtZwPbf0xgD" +
                "YcNzI62TE8iq4uQkcXAsTiA+l/jinU6+S0juaDStazojEy7utgTdfgJMMJUHFhDyu6iXiFI508NqmTUl" +
                "WV+DMliaLNz0K+QK3GHhnQZbDvwoA1A+xkiOdirosKkpctgU1Ll838DvKVuy2wyaFwOLEqRFxgAd4mgp" +
                "m7mGcSj5XMyco+PYNNo/w6b3W2l8H1E9smfsUiywV10IEqo3FaxGA/Byxd4vYThxYeJ86hfqf4m7kNrm" +
                "kPzKobAoKQ/EOVBEqJYOTNzuDWj5eOb/WvQJ5IXg3pjm8U4bjajCDQ/bKMJD2ci7cgjZQYn9YUxdxlAA" +
                "tiVCTEbZLnzEo4Y3yRHIQ7KNNGb6GJbJiFpMvWwaS9Rw0snaY+2kMPcYxtm3VGrUb6pvTITKKqoaAG1/" +
                "BlFKwlodi66awY0Bp3LVcTs1EkA/6dqokdjT1ejeJmu3AIhizvHUlj9B1mPJQKTCSbhGYjxi8GlsL9BA" +
                "DPJF5Ii76ZOmtUTDgHao7CDU4tCSUwcTElXoxxQkHp0RkldO8MkrxcBdCJRdtYNVvXEbSHUotl1lAKnH" +
                "NldwEUhsXpzDshLCo4k1r7GvdL8gUeqti2ugZDPrNnS/FlkcTwlZTTGgHnY8428Mtzh/Mt3QXsMAETuV" +
                "2kPi8GIaBXNJ+5wXXeR/+pMhUHMJoSRCKptdwEoYZ1+QFd4j4rsEjeRi8UNVmLCAxsKwf31z+oXItEdU" +
                "4IcwiuESXudbxvE1TAgDWD+SgkllSX4inZ0iEn+aElC0Lzm6912sSbZwaGBoxLolas1QMxbcm8P/i7G7" +
                "FWNb+nEXv1iMefP/S2LsJimmBii7twOH762TMFoFct5ptIVeYgP+HXz7VtCEj4qvu3EXw6z3OIyi1YNY" +
                "mRbdtgBddNt6J9HZDnzK0cjd38T/G/1lgw5NRQHgXtrdLDIOvM8thlxH7sxlfG8hfPoxzpp4+KDn57+2" +
                "d7SBQkm2LJe6bTQe++uZNvUl84WV+OItfSL6BZTDiPpJfkCiO+OwgdYkPlu7u1md8sSeXcNW6PbExR1D" +
                "nzNKRnnEBdKc/GVLFGDxUV2Bu4gE3uB/mswevA0R9eDBiQBDnUP53t0VjU8BVXR2PbjP3x7bFzz28oKI" +
                "e26YprrIUQ8haGI0Kyasduam9njqXrOZDHgPIRdLT8t2ca0CEskPdZ/HiOxodBjan/Ebc6rv7YPnLrY6" +
                "IWEZSWqBAzB71UeUAvXQnjl4EsSImckwngcMGJZIIqNdvRHCw8Aev/4kTEynIaFUlG/Mr5NkY9Ihag0B" +
                "NtEwCm28mLbUWYlzBMOLObNhUQl2Q2xF3Q+G6XSREqJbEUZYCkBbdA1CrQDaYT5ryG+2hLnNOBvyPMII" +
                "BsXC9j5GVcyof5EU42gNouRSm2JxXhuY28cyBovdJLmx6JnHNBqQwdlNbAjflG2BwpxgsQw2A2GpRXZZ" +
                "1dvgHVj7u+DJXV58YmaAxAjngpoQ1HGbXnhmfxQbFG/LNAQeCvUILOweiw6eIxaVlDpoP99n1CkpUTCS" +
                "Mc1haNeGncA9+ndCH/lcc/y6Fl3CW0IgmJgW9WiZSVtJY+1ac86z2g0JIpUM5JR9QfE0qOcIOKuZUvJh" +
                "ZgyRrDDBKyBLJI7OU1q99k/0DmOzYTS27X0nXWlBwouivehD5Ru0XemHvXD4LYL4nMzhaVbG0VAhYVZB" +
                "mrTNphZJlWbuo6gRgcVvlNOwY/O57AWkK4c4SufGqg1ZiIx0wyL5Lc7uyZy+XkIYivWQoZG4oUS9k0ag" +
                "UeTcNy3s4OI9LAVOHxE8VaYicMaj6TXsuCenp48fcJg3IlR7Iy2amm4ljO7qqmzqSgoVGB+ChEDyDsUH" +
                "Daq+lBUk7NuBmRVCkrqcH+lIb569ePXNs8efyprWa8operBOzebzmmCVSZt78KG1ejJCO/k6sQtxka9f" +
                "P3t5+vihCeE45v7hZBRUHxRbo3zbasmKoGABbpXtm7sxXr+0LBaduih0kSHNkBgnroBalxhRmiIyCUzO" +
                "dYqCm0ec4CtN0mvuAzDxSAPUG1oO//Zs6A8LldG9X/1P9urzPz97+pbVjr++s/1D5Dz9+RyHCE2J/i1E" +
                "4ZkggxhjoIIuDKyCdpBX7epzDZsH11FDuKATRsl7RsVlEeKy6Qgn8kb7xxiqpNyEXCCxqmw+dWEPKA5w" +
                "Pk2nYgpWYid/Pnv18j5rLSyg8v2TF19nCgBx1EDCELOBAZJcHAW1YyUGjVSpu0JBCZJYDYy+7my68JE4" +
                "9HV9CXvlsjjJPvqPA2L44OTgKS2b088PjrODpq47vLnouvXJ/ftwP/IlsN0d/OdHukRWQHB6Gs2pTDLq" +
                "7pl1w81JsEDLsewO0InVbOCCy6KwEtjFEqyKbDRcHFNP++iVyQxFoucWTz9X2hAgXBX53kbWOAiJyyrC" +
                "LComBbaMktliJZwvYE6ygAB5RxTg3RAFJ7/9999/pi2oejWVina7Mz6wkc7+8jXSFzARmMMI+9Qb+OzH" +
                "5VfeQmHLUNnB9rx99Dt9w4zRSfbbzx49lEe0btgA5nO9tRZQ+6i6mw9e00LhQnwAT37pVxSebJb8LunT" +
                "rl4fOEGDtG8rVnuTtRDLDSRb365JacfZ7BqmtRhtILcis2iTezkgC0/PgKw8ygQLZ+omAIBR4FOlCyeq" +
                "4fzgGP9DCICF2r/PPn/1HdSY/j57/dWzN8+gWvTx6fdfP395+uwNRLm9ePXy2ePPnNtdPomW4ZyslVpp" +
                "LhKQ3YBbYTnZ2DSGx2OLUMeA9D2nn3ZImp1o4E+K2JhK9PJCtiW63rukOoh9DlS5Sd7TfEIsXKaq3sN3" +
                "x9n3Gsv9WzpnIlkcpqI6h7FoMxrKILpNYX1A+jjidvIdLJL49H3ANZ/+Ri2eTEnxb7OSOCC3nWITfy25" +
                "Bemp84REoyi2osB8Xm44BXNzlIJ8Hgp38ubJ6fO/ntFCSsb0TRaY3GA9VKBYUdKR8IMUV7h5KJF4G+pv" +
                "GcpOWJUVqip6cCdfPXv+5Vdvs0PCtoejuCZN3SYYj2u66LlXzgvZIXnhSMejnPNxdHU2jj4k49w0Cqst" +
                "HHe6feac7B8TKluDAf6JtcbBzh/yJKP+ZSMusJa7IsESaUhwyv50NrX+6dhyHB8bUp1JB8gMJDVYPO3R" +
                "yKk7jSNi2PB2RNyuEyDOZ7OThKIxOox9yW7RPNDvmmQmtpPgJsLuWs0Skm9JSDZpd1cLxFQ8tNcLSaVJ" +
                "csRCPPkVl/uBEOztqyC6lq54kqnSnaSAgHetzNeUiDnCgvhDImGtIlTq4aR2NVTeYI1Mc8HYQRX2iGO8" +
                "NQCSUzBYHCAJ3HkP971g+VkxoMxmD84l1aqd7ghVvow9KPNlwcoLk9JjUD880nkW7ycSB7yT2Ypfvjfz" +
                "q5lRcJr699H/D0GC/H32McN/H2ezn/B/8+xxJh51np08BoEXix8evGNEMTx+ysdZeHzIx3l4fPQupBp+" +
                "+OydvLstBHwghjeIa+1Nhg26OKFptf4/ad4uYaTGJan4V4kSy1esIDow4g/HSX03Hnq13e+4S3W/tZYp" +
                "vAsx82QsVWXFe5750wNdYoeGsEi/PjykOlnrgqRFO8hZiowYrHKMpY/2VFa0u6UVrPeKL3vL2i26mG88" +
                "/ADdP2EMiIV3zR0FYeMpi5tLo13MpkcDyJvJGQ1HeHoSxIM04fiLHZKRHLkfbQjxfCmK9wywVrEoscc5" +
                "BlboHQ55bfvQa+eb02/6KureXutEJ/c7fIPwrXq/vfZX4XW/+e1v2AAjGqHRhz0qPeSqphqgkPC2p0TE" +
                "BXN8RxHjak8+W18/+kQD+4cwBPI0iLYh8TwrJlMQ/vY4Dv9x8g3hoqviXbAbhhVJw5Z73gt0iWNagiIe" +
                "Jon5mrgT2SEiQjWr4RA4xa7C6/SqTo1vaAFoSr7ZU2Ru1E74qWhq8e+Q3QMvxILQo2GW5Pa3e5e2b2RT" +
                "OwHSU/phO+JSd2su1KHVamXJBg6xKRYUw3b7Ep0aElosmP3joaCQU5SChaMoqvMGUT9TCXXSbsuCwSTk" +
                "jA43lPgrCATqSao6pE/AzmPcOPNs5DmHb7RlbDTRg3r/+6jrDsirj5QY4ckdodjQR6eKGkvvtjcmmnYN" +
                "Y98VFgULXJa55Qzb1tXRL81L2Q5bSiytex04tQwDQvX/4kTW85g50vPcDu3YUlkhDRGMCOZP5iF1HmJY" +
                "4TTQObYbZky7Z4n9JNnNi7KhP7igNKN2+4SyV3H+GlHUL/75sDiyXE2vl0UlEkkVd0YIK5YQDXB2k/v9" +
                "PxR9XskngZBPxL3KiqahyHDVleQz4zHJqRzfLCbvJ+w4CY13W1x/sMVPwxb/inJsn5kWcvBhvfHcnVRn" +
                "4hXNoGjLaYHvvGxnmnRUfXO0UykS7hwR6pf2UoPYi83lAbBUrcD+0pQYomtMlmqiCMR4zmSQBZGlTJqA" +
                "X9jkRLjE+XE0goLvi6YbIcjIESErVU+18lllcOzOaRD6S2z6SRIQcvwIgJev3gK4ln2tMAMe1ksOhmK1" +
                "Qh9trFMMMw9Fyo46HihvFCwiwQ41sQb85E4sjyUurkokgmOYWLECot41hkRCH0otFXCAJMm1FbQe+YFu" +
                "IfwWqaNsvWlEXI4D2/diqrKNosHi1QZOI/ROiEawdzRO1RhRIKk8TwH+wT1LP0gs2sXrWHpD8wwXCsUr" +
                "yFE74Rl2SLWQY8nPefYvGwgi7FiPXGhGbY/qOLO7dYIxhegnQ+1eeqDodsrs6m3eWDmE72mYshJ9PiSx" +
                "hLK0dmuNzQH1SMmLBnVWvNlAMhKDsng5rahtPtMGUtevB20qEWZiYeiMkoHttgX6Ktn6GjgqJQYgE9Hz" +
                "0Lat+RJ1y6Te7JHQNXIBfmKeE57IoL39DId7+5yotBtJxNOjukGwkWzAeL5zoLwAIKp9Q7uIBq3RGu85" +
                "pBDahTyxnhhl9XXX2yTKLOA4Hih1ZAgw0VsqDxHaRG85vdZsCq25SQpqdnmOgQJN6EIDST5P6sJbXiNz" +
                "M7nZERAnt2fMzzNhfn4R6YmmxA7HKb0NSMx5ggUxWbvSK3imxSzn4VIvAtu1HGSUvRVVJmMU336vCGAm" +
                "vMS2IpkM0iVIiUcGtWonF2oVvOumzqgH9k3d0MdyHJjJSIlKJIV6VY74mzGpuRgZT+2K5KCbnaxlaFo+" +
                "mSQ2eQmRC4T4ypLRZfUuoANh+GHlxV4B4EStLCRu+LBy3ZWEW2xxzsrbuRTUENn8wnN3QjEK0StMH5jG" +
                "RcGIzkctYKCeZaZr8WOL9sghMgmDhDhLR1zZ2cj7wb8u7z/UEVLomBerolSEAnYiLMKZQlkxS4tssaYG" +
                "k43ZkQN2N1DQIrnBNAGOeiIpDC8avGYl6YNjmZ8epH4Qqna7nf1PVPN8cBsD2k008h/43HuJFmzh22CR" +
                "PL1dD3oIxy/p+/SX6ImEXT3vymGHhpy4aJFa5UddodZVITMJPVAuqS1gik3JLiE6roV+VbiTQ3RkwJoZ" +
                "HQP0SCmYX5rRQ5V0T3GVTjcVtnWoih4wjFsoHtkIxs93SEQ8RI4efz49Rs6ZaQlLbj97efbqDXLpgxcx" +
                "1W4vvguFDSYwZZ/C2P8yxv2NikQQwGeX437ZxfCojBKj3+c3cDIFgOqi23dO9l515KQnZ7VAUb0blOoQ" +
                "F0/OE8Zcx+AWqTuOQA9uZItFNItFb+bOHv1a0uzQneajcLfbmXwIlwZJOzvgLuWjQtoLuQog3ncjxoql" +
                "Z/XAEarENw3zFZWlgeTOCb1KyUSHFMS0KvKCu2R3I1kncnimx+zZdcISl18xDykL8BX/G/oZVFac926E" +
                "EevNqp2yQznAoSzfwuGk7wOPqS2KlUkmKbetxF5KKzEJ8yqHaOdSpSzTrICkglgXw/EmOl66nCBjNVR8" +
                "9TOLiUtRfXjDYjTH41okXm/lV1D9wUdPBLyU8cLtgVDfvcgoVhQJ1tFdWH9+jTBMOZOqvmpRnvPQg9rx" +
                "yVJ7F19hVO6aqItF2kq0u21kihNRnTl5NVWMMxp9vni/9o2F/MOMVqRCseW9pkZzadYJo+jVqdzmKhkf" +
                "QUiWKZEmpM6skdPFWiNNkq6KQg74A2zA3viBKvP9K4MOt5X08UucXJX5PoQ6EnoatM0XxSQwCxJgvKgx" +
                "rE8mB1O81iSqZPXE80OakwsJHb1uPNziZwWC7u4JIM5FLz+TbYssClZmjjSYW8KWRUVk6x1qNN43lZBx" +
                "LuavsoFU44fI3y6V6ncheCedzMhKP+1cpTa8Qg1eXc6VVcFy2gdz7/1pNsIcGvv6LkW5CGAsx++464vs" +
                "fkqWaIVRnVxuk97SKK/7tzSmF+el96n173Bh1sfGURjSyCOtiUNq52DJd/PNeuk3w3SL7HCfZxfTSpKO" +
                "uukssVkX0KZyyG+iF9kmx4tH92TZzvbxZI8eoB/ZNXThUNMLPVfvN+LF222sPZkZ8CQQsObyqvN29DWe" +
                "XusDZiKhZvvWa8+r1AptzYv1Cm8r73vX9Vg18fHuvT2opVAXHHsDo2KVo+iv6S1rLW6qlOzSZa2X4jbF" +
                "w0uK4JXxnxoyvZtKVuORHmEAUuqGWXYtw1ZQnmbuDdk3Cb6tG73LdDm/o8uA7uJ2nZ+nwN0TxumVE+Kk" +
                "71i6atSOw+XL95z4d1oijY8a89hwmMGJtyD5PcgEg1Z/zLML2NqPP7KS+215WY6buh3Xzfn9bvHRn7rF" +
                "H+/nfwIpzy4BSK5DOEMVIZ3KeT2DR+WhGL3fTqpwghmzkwQySdCfbqYEE+JrXqTKRvqWt4r65RzhvP1d" +
                "HC/ey/wm/7x4BhhAsCoUF6lFIe1CnZc0sTov6016vyrnLEDk1zJ2vlEUwc1GIoy1+e31SrO0drlev+Ip" +
                "HW24gmf8FmaktVN7cqZDBhaZhz0vlgsMyTm2VvU/sDwUGWp/hCuCIobGiUGxu043wX/cFEBItMFK0eCK" +
                "4cMKLngl4QH37GlC6xkiw+CNM4+2achC7I6eGHoekMM4RpS6FExCrgaTaexcLiqEIQEmseKg9C4ydJBj" +
                "3A8lQNWbrTS3UnTFoqUS7CpwmZLZSnpgvcqevDxNz68FQjMAk5QEKPt2PvnO3z0PCQXS1u9fsRb3AQpu" +
                "dml+lRp+c1+DP97BtBOVPbonpo77vTfdDODqPt7emJ7sD0VSrvfvZgliR/zSBdhVrj+/ADNGbn/6iY3h" +
                "1UjhoB6XouF4CCNlQEbh2XTnGHi4NdfYXdrYf/xCoL/58vMn9uG2/2MbYTxFJ52P8Os8/JqGX/mdewti" +
                "m6ld2Dcsh5dESIBqz9WObxPLUyRnelVMNPcjfB7OG1kP23l9+BbCTgJ69vHWzun+zNgSlnx0KmYzPT8Y" +
                "XXq5AzSLBC7QvimK/bUuaS4Yg/D0xT5Xxm4DVPtnT6rNYufBL9VjCwaQtcv7FvDPQNp/G1niAUg1GUPb" +
                "0PnW9VDOofHYyv16NtusoWSPqDDEZZM3iHojzKqoOBxPu/tjGgO4ZNlu1FRAcuB5CR8nNS+1qInOlHbv" +
                "S443zIJqSIIu/QrjWjEHs5K8qt26VfBnQ/G3Jk/1BnemWHQZCL6XkKw/hZyldtUrc/VqNjkZNpZYkxyo" +
                "pbGwbZAbs7YtYxW/pxont4z+CxuAThVTaAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
