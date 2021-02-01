/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupAction")]
    public sealed class PickupAction : IDeserializable<PickupAction>,
		IAction<PickupActionGoal, PickupActionFeedback, PickupActionResult>
    {
        [DataMember (Name = "action_goal")] public PickupActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public PickupActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public PickupActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupAction()
        {
            ActionGoal = new PickupActionGoal();
            ActionResult = new PickupActionResult();
            ActionFeedback = new PickupActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupAction(PickupActionGoal ActionGoal, PickupActionResult ActionResult, PickupActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupAction(ref Buffer b)
        {
            ActionGoal = new PickupActionGoal(ref b);
            ActionResult = new PickupActionResult(ref b);
            ActionFeedback = new PickupActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupAction(ref b);
        }
        
        PickupAction IDeserializable<PickupAction>.RosDeserialize(ref Buffer b)
        {
            return new PickupAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "966c9238fcaad4ba8d20e116b676ccc1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a3PbRpLf+StQdtVZSij6ISebKKurkiXaUVYStZLsPFwuFEgMSUQgwOAhirm6/379" +
                "nBmApO3srpi72stuJSIw09PT09PvGVwmo9t6fjSqkjx7k0dpENGf4QT+7lx6L69MWaeVvi7oV6PBa2Pi" +
                "YTS61SZj+d05/Bf/0zm/fnMQzPI7k1ThrJyUTy9bk+h8b6LYFMGU/tNhhNJkyK2xxelJgDMMk1jmQHOn" +
                "ST8MumUV8+iMWudxcF1FWRwVcTAzVRRHVRSMc0A5mUxNsZeaO5NCp2g2N3FAb6vl3JQ96HgzTcoA/j8x" +
                "mSmiNF0GdQmNqjwY5bNZnSWjqDJBlcxMoz/0TLIgCuZRUSWjOo0KaJ8XcZJh83ERzQxCh/+X5rfaZCMT" +
                "nJ4cQJusNKO6SgChJUAYFSYqk2wCL4NOnWTV/gvs0Hl8s8j34KeZAOHt4EE1jSpE1tzPgWcQz6g8gDG+" +
                "4Mn1ADYQx8AocRns0LMQfpa7AQwCKJh5PpoGO4D55bKa5hkANMFdVCTRMDUIeAQUAKhPsNOTXQ9yRqCz" +
                "KMsVPEN0Y3wO2MzCxTntTWHNUpx9WU+AgNBwXuR3SQxNh0sCMkoTk1UBcFsRFcsO9uIhO49fI42hEfSi" +
                "FYH/RmWZjxJYgDhYJNW0U1YFQqfVQOZ8IG5cuyOItQTZoJzmdRrDj7wwNC+aCKzlYprAgtAkcLsEi6gM" +
                "CmSYEiaBDHRK600sCSSJMhkMFrm4A9ZYTE0WJFUAEzUlMi3whZnNQbKkKfRGmCVzzcLA0BZ0MDRjxCUK" +
                "RqaoIlg5xMinr+CfxLomQF5Ab4mDWDoHYyumshh6sCCDPViW0cTQIgTl3IyScTLiCQoGZU+g4wbhBoDU" +
                "rC4rwCyAXQeterp+uHJbk3sk8eAfGbuKiompwgw4yD2cFHk9bz0zWRya8diMYI3h6ZsiKufvPwTzvCwT" +
                "2AXhBB+UHuSyns/zogrLuhhHI6PgOp1hnqe4fvkCOiXzuSlCbTvK0zQpYf0dHBgjqqpoNDVxmA9/hfHD" +
                "Kq9H0xA21i2NJxBnSZbMkt+NtooTWGjYwvD+GKRSVQAXVLADo2oajtwDD+N5GmUgJGknNcZHXGF4HpfB" +
                "Y79xmkfV1y/te+oPXULitU7nUn4P5sjSZWDf5/xgG4tO66RqQPkWpo97Ajke+H4Ei0B7Lh/DA1pH2VK0" +
                "qZFfYXsCDGL3hkoApthTpsCNw51hFzORuqgB0jpG4k7zBbQAKNEcNhysJ2zrboAMQH+YatQLmljGOezJ" +
                "LK8UXYC7JLUEO3sWEcbRMK9BFgSPeOB5Div6KNiJkC0TajG4IHnE+OyixPkRpmZQtrBGrFCu+MOKLJtG" +
                "dyA+UtBf8RJV5TDJiAirw3tjl0yj1TaIw8TkoLwLnANQGp+0yQfDzOuKJShBDWEeBhWxj2EPZddRgPuJ" +
                "jACWrrTOnkAR8QYqtshALgGgqgaBKCOjZpLOBmXq3kT4BOT80lIPGKSQ1Yfd8ivhuWS++gGne2MfIozQ" +
                "oowj/SEMdHRvYHgLpAGh8AeQWIuAZYV1RG8goAyYlEwWpH0+RmUOvx4B24OqJrHzaB2sLvEqqK+arCwP" +
                "gjAu9gy+SLIvVrriwMt5MpKOML7dej4Y+BvAzGB6AQk3UKcF/JcBi8KzpN3xB9ilNiU0mkRZr6OcKHoB" +
                "wF+L7ee4TskHyiyZkckB23YYDZM0qZaITVmPRsCQLRbsBvCzBPBBDqig5ToDC1DWHWXAo0mex49Qpydg" +
                "n6oI5XF/A9IBdB3aSoo4KQybBbBJqujW6GyBZrfI8FbigF4ijQJMkZUp70DHmgpQByiACCaq1sCPxsC1" +
                "VhpOIyQdWCK4QVODxNjh8cgqE/W0u3ZwWC0ZXUZrDz7LdWToinKDjZ7CpGI6O/n1HRPaEy8okFhIZgZX" +
                "A4zIJk8DoUuTjrvBsK42A+4Kk/uCa5GkKXKgBcwmDUNVH0JRjmgp6jnjA1Ori4ysd9IWul3YfOoCf6xM" +
                "hAh5FyUpmdQ4BdwxEa4CjNXbSFrQqWBbeKTFcWbRfTKrZ6w4YJEAHJjygC6AQpM0lWkgEXb+evgMX8GO" +
                "wqF3mSXBTQEgoQAICQBCR0YjZQmbL8W9BzPLh7AXR6mxhijrjtLMogwU5ao2IDgxtx4BRNzmaFqY+Om8" +
                "LvE/qMFjlvuoreuCBYAi/UnT5GHsik+I35YfbXH8FZuR+Vd2Wl0u8SdZkWyK/Sl4ExKwJn0UNZWn0vC5" +
                "td7BKU1Qojkd9b4bgJcLjkIFb+FHBPIwRYsCX374gMq02Zr12Qe7Qb2xgAGBC8w9+s7sGB3B7nOew12U" +
                "1oY1oXBFieofHHIUU6yviM5kFuDOClqz7MHUrcnKhjsj5j1z0/EeNqblPefZdOKaX5GDFY6LfBbCdoAX" +
                "D7SYG3UXGUX4m22wwozBI0QXvh234M2nMYcW1xIA1n/bwp8QLwxGPMDAiNRWUdNvDB4vcAKIuS7pnxxF" +
                "L79nuwankxeJ9u0FHeJo26Dz9xrlaEZwXbttTZA3FxlW1u+oWnZZJJutMV1rHNzbv5b2r9+3g74jnc7B" +
                "LlRJysDRs4k8/vrN0R01QK/ziRnpX4vtOIZthQozjM0Y3Bz0CytP0TqDgrp0WfvDBFnnk5hDNQymUwku" +
                "+WPqYO4jtJXo7zQZVw0zDdYcF70iZQ8N1DBTq6SyVgACRo0nmt1ZamJSeHi2zNp3JPf21bK1PS0oU8Kj" +
                "uDFTGzNQI0AaecEEsTAS15gVucZ2MLSQgCQByGO0s3AizjbHmBWbZaBP7sAaA+MR41kgvqzdkWTecA/D" +
                "CB+j1DpGD6TJPyVaFcadxJC2OLW1c2I8mtu2F9iYJHnC4LyAWALtbHtizERZiSzigha3ix6NRizQ60Ev" +
                "AtqXZHcCfwOw5q6Cx9Blx/QmvS5b5tSK9gliQeEJMB+LZJIIlzo7EmHqonSDavyCzXXCmQdjZityFk67" +
                "veB0HCzzOljghOCPQvIEZHgoXrTtqjynLa78uirPbawEZHkFi/tJyfYwS+2LMy/E95Fol5rs4nNpQC7w" +
                "4oFqe3mP7N4ugZYl2WMomiKO+DIFvW0PthoGYCQWw2FPMsYcjtYqbgQiL0UjNtqpmmw2HTi902jt6aNm" +
                "h3cJiCLy3Rvt7+zjZvOHX7AWRYDq9sca84BtWyY1yiDKTWloH3yqLFZ6O29DZTW9lr6oYwz5VmXw3g6x" +
                "B2/RxAWRFg7BBl503fBfeu/Ae7szHyxfWwNLH7RarnlO0Dl+tzDJZEpqcRx5sSg2k4QqwU5sQJ4YlFwo" +
                "NsCtTGYYKWe9k3OQpcG+wXEKFiKFE383RU4SrAxSjNRo12rXmRqExDaSDyu8vXGbsnIGQ6RhHspyuKmC" +
                "0GnqF42r5MNcYmJtapLiwjSSMgu2sryCncEuKU1FqT5Eg6CQZtt1XhtlTMRgzb124Pijf2aFCnRYb5Jo" +
                "0oVAhDykInCXp7Wm/dZhHnReITsD/Hfc0jUKCzMR++Z/F3dtgb2aRAEKnJAli1qeyQQLun/CpMEFxvhl" +
                "DmJatRvo2XmRzBIkgsRxSIPXc871yqrkYiEHOxGqkxpt+91OOY3mhvG4RqCXCgnFt4XqxaPRwna5UeMP" +
                "jRwETjgGXNeEaX2AFKUlqKcgDOM4sWFEC62Lm2tqSo0yuXgC2g8x/cdKSOQVmedONIHl7mJgYXWK5wAS" +
                "8GDIH5mUDP3JCWE7ncvDMMrG9XF8Mszvu0AetObAy17Cfo7R0QHWN4EGYxAKz4eZgMhHoT6YblI4KgIw" +
                "pLnBzAcGQQtSQM+68D8QWlgF8U3wavDT4XP5+/ry+/5V//CF/Dz++ez04qR/dbivDwYX/cOXSmqs7FDv" +
                "h3CSVvi8o41isMGzkjIpjaYufONaaB/cy4i+38FrdhAYDJCRNEAbUn1FjhvH5l4jU09cnycw+SKigP5r" +
                "EZ8wcUK1S79+6gY/d8lr+MXHOZJMRmqyCTgcgtEoL8AGn+dEZUysU9ZeXgLRe4624U+Hz7xfP1ta469f" +
                "gNQ+Skx/wYpMaFx2irVkaPFLgQLjCTb7RIQE6JooTurSJYmYg3qNdQ2vjk5O314DPv6YusgEExeYK3aY" +
                "Ksw6qFWouoSNOuSkNKeJY5tfgug+KSllJS5IA274ff/0zfc3wQ7Clh+7bk6cg/Qo7uY0JaFtaS57IdjB" +
                "vbDL46GxpuPw7GQc/uGNs2kUdE2Udrx8kc1xrRnzGBcEKaWvMFfYFJrenkRzPCkoQ805iyqZOx4imlKu" +
                "ERYJ+b2ed5mywZdC1E5rJwr9LEu1Jg/M5e3UlcaOMNjwwUUcymcVbJ4FhWK2Q1lIdoFA50XZBDT3d94O" +
                "ljAzWRukSmw+Fvb2ncG8vynff+jgGDcCAGSJhaUmN2dBbY9VhUrYrPEsKQjNnbZEKp3GGpLptJ6UDimu" +
                "YXu/z3ia+xAI95DY+kbOWqfvj5jRzXjlp01pDZH5vTSV56xsZ1WQUeSinp1NsdRmGPqfNNttPg637h4t" +
                "WGCKgioqxO0qHY6uaGcIBmBdmfA+xI6hbbzaYvnJFr+3W/w72uDrQgyyuN58WZmOawqczPARuvAuDsGh" +
                "rTgpR8GOc7d2V6pRbQkqcT+1x/BY2dAmkQW8ZMm/mGJWBfUBJe45BY4RNsvYGLPLCwR8LsiRYezww9EQ" +
                "1HAZQNO60Jg1M7AtWsKscFGPeAivO6KB0C9g0Q+8xIjShwBcDG4AOKetqa6tnmGYXPPaGh7GqoRqgYUJ" +
                "DnObXFbSYV1KwWDBdlGonifLGlOMN9gjKaWX7hKz8Awbpgpn2FqOPHkXOzgoV4iA/cQ1VLtaO0aMX9Zg" +
                "Pc7rgkz9nt32DSuAlpGUhVRhDY3lEYysJRxPd4EVdqQZiO+L+AC/0wQprxCyrNhZ7ZYwFizQFGSIwT4L" +
                "Q0WmvELsQSmVKNjNkQnHkk6Edf0UyEfKbWwggOttXhf5zCO3cmaVL6IiLhtralFmpo/aLOZxFtmQlGG5" +
                "l0qemso/ZlG2ZBu6R2apoCvBaG7zkht0A+KDiDN0WFKByrxs8zYHICjOFsyXQKMktjuUTAtd1ihdREvk" +
                "3mCf+DpHo5mrwxHhkAZtrCeMy65lcycy7zoW6UkfXiDw72VAF75vKS8A4FxWIbtX0+NKlKOyBNc49tsh" +
                "T6R5fisJAQy9V41F4ppFL1+gxCBgc617qUsuGUOxATLDcLGOuvSg2Vb3HBqwODxMKk/vJF6UAFTzseou" +
                "xtuyW/+OJEdeT6aOn9CUWNlx3XVSTPcEGELARzOuyB6aUVS7XbXGcqBRJI+EarnSKITKGKa3Fha30onQ" +
                "liSTQLoFVqKCOCw8AK5Dxuk6ZhlFnMZYQV3IZ0Bo1VjfdUdZgDTndM0zCYSw90DjsV2Bsr/hZ5CxS69E" +
                "Eou8zDBTk+rMvNFp9iqgXTZTclHjtQJAmZq3EIWQ/UyW5CNZM4jF5nDmvR1RpRYSG99wuZfdlFrp9Uw0" +
                "7iIXfNizAtJjxnZOzoApdxWiFsOhvyzKTkZeD/4yefqCR/ChA15zgzY0itDdni8slNQ8Yy4t88u7vIVZ" +
                "kQMMx2mRSGCKAM/neK4AvWx4DJB3nnUJP86TPcORShWzzfX3VHPs6hexWAzbhexL2H2uvUgLluCwwiTT" +
                "JZs+fg/a8Sk6tM0pqmuyqudVOazwkDIXWqRSY5ZnGdaFiePdVi6+LSCKjdnOYzqcC8YElX4F6UhLNTE6" +
                "WuQJkjEFW3FHNUlF3X1a+ej6wjYPyL+KpCi6kf6nddGovDV+fgoOgxfd4Gf4z/Nu8Av8Rx3x6/7F9eAq" +
                "/OWw9cAFh+TBTzYUJwKT1qlRQPBvZ9y3zjK4OON4zJXrnP50NeWaBSlHBuPl6qXt2lMR1/TCnomgdiHC" +
                "o5D2mGk5TqOJbEbiVNKOEmHgkhWuNaViHC6foxw2grW8SjHDkveYtc+l8EI6IUsFfI4Eu4YYBfwDeFA9" +
                "ls74P6CfQM2okxRpYkcyFyQgHOwggYXHSvBw0NgGE700ZiZbAREFDkIFvbFWVktKERuT3SVFns0wwkeT" +
                "wfFCHs+fjt3UHOC5+8hk3FRYAG+YDCfEVWxl9WwIzIAGMpO5/E5H9yRKasYV2tnPuhqsiPB0lX0vQVei" +
                "ekeOn8RL8PuTUVjgKb1xMsFCfzYcvamGOqrMGVeN5NPYb0XqRBbSp0nN5w5XaonLyk6eZ4tHiVbT/44L" +
                "e27O1Fv8BTwgUsrRTVzmzBu/S7Yw8QSF4qkMOgoykHTE0pkxMWo3AGup13vG2mP9zEBp2IInn75Ik7sk" +
                "WkdQJUJDZJfR2IR2s4Q4oY6bHyEHtl/OxadUAkGuRswOsu3Y5cp/a95pDkX9CwKEuNBJmJjPgdgtWhiq" +
                "LbX6nbalyZDYfIoHrcU6G7GsQXuLtwFYSwDXJjpWuJTfE8Mr6wTCVvyqwVGkrmZAaA6kJpwZXEQJuUeq" +
                "qtfBxLyynC91QlxGiEFFLLcpykkAw3Qy8WWbIrtZv4JkBSuOHSdYg8p0rvDva/yTH4f8WOijQH2/G2bI" +
                "rM46IaGzIzoOw6BGGtrzPCCyS/isSVzPU7QIKO4yDnbWuRIuB0+5+5ZLdKO1VOIXvf8QjJN7E4d8kNaW" +
                "WuFi07R129vjicBCgPV954hfHOvzc3psq/Jt+1Da42ZOU/Y85zi9bFJ2zuDXJf8ATCi2Ke8a7ctRhBF2" +
                "bH2Nf2pbek42ifisUmNZdj187SNzJ1ZkDkbLLKLiTX9ac/KLKKuJPlKekp3uzh0zgWey/yi755XI06vO" +
                "gAY7xr5YksTHMRmUJggaQzZNgh/zAkz2Bf6bYiKkedkIxBVdTE3FgtNnJI1hgUEPflGlsWgyT9oM4k4n" +
                "CaeTiEhKMT4efgO6nbMxeL/+BKG3/UCO9sC3q7xKKYqioQPxKx9FRNHJh2TFjHV1V7xtuVTKbtsfuJvk" +
                "apbMBDPQGAk63ieD1+SvuWg/5h0boM+xLbTzhqDuYZyPw9ZglllXeDTY0eCLXSzifym1IRrsdrS/3XrM" +
                "dP5pYbf77LmYrbCTu3MBIxN4DYIa7nxng14KQBaXTnWYx7iBdhQfaEjhIDCtUxMV6xpzDjcS1jo4GIHl" +
                "cHDgiWUpO67nMc8V9BMh7x8m3d0K969nQI9Skd0CxH7TPI1LW6DKZ5Ql2kIcxBOXQh5wvn6ree8UOd3D" +
                "wBsAa3k79pYC7kQlDFxMuFOYO8reUP16kZS43Ua7foxnuMQIffBFY5upVlMoUUwhzN2uayrnaJarTZ+W" +
                "1PgpmPu4PV0XPk3jHOA5KDl3BEwAXFAW4AKPMXfkvBK/SUq5UwC5DXzOinfocEnygNQpE4Lze02JvXJZ" +
                "g45HVCv1ZoURHjYCwwtrYdptOnRbCAbWdBm5Loa2kXbV6CidXMIxe7I+TQFYJmVVCmer8CE9U3ZpBIoG" +
                "4tT9k4UeMlTCjXOl6lzezJR6pk7SntIcpc0d9oIf0VDGWmyujRYRSrPI8NS6rE/rBg5SeF2qq6ZgsJFq" +
                "INuc9CPafkvhRqQez6Z9rLtx8NzSqUx+N3SmFo+HERxv15Aex3oVubfC8oC7wMIRhywyRZpraUboVvEK" +
                "9jorOWB7gI/4Z/Xk2OrBseXq8bAtCJRVtQOzulo5T8UyALnnV++ovmWx2EwKw+VEeBwgzmeU9BjnaMup" +
                "uOYop5xmduMxI7fzBFFlC5cpeVkuS3ADvCqmknOd1t7kPhNTuf1PBmAuo9+CUCIhhTkqjM81sjYRWb1R" +
                "ocIiIg57e3XymmTaPirwnXtgVvh/tNCQXUUJUXop4Xz/7h0fOyYkG7Jdd4C1+b7zWFooNNjQVCgBwgiv" +
                "UYEJN3D4fzG2XTG2wHM7088WY9r8/5IY2yTF/GPIG/xBKmWyzl+r0QIWFBvgf1vvfiQywUum13aONlms" +
                "lZKtlJATKzZ5sMhXTmyVrfNPHXtMyz9Z55Xu6ImiLU2SqC0TVMlUOgOreeZyWOS3nNfJSWJgAWXEGZEo" +
                "m1COHncbcIlOUpq439JuO7Njvlmzflyj0Tr8WxpAnvYsTpBixZ81Rb68wP5kc3kbfu4GH03kWuupTRNa" +
                "L0euG6LQjDsARLIGHcJ1xWCrp4ylElGLt7FIhUrVNU+xgpucE/JcUGxGAz4+HWuInJaLwvcIMsvFee0l" +
                "sU0dYuBGHc/H6+DZ1BZpODsNSwgeIO60CSU3nahrw04Q3+/CGtl3FdWpRtfdC93TfRGYV1zasPCeRYzR" +
                "oFi/Xn9ko9uug5Osjfu4KCSsJ7MYK9K8dEI4Xinj0luBbPZMDgjTkcsZ3fqkU8E0LZ+WlMy9vcUHE/cU" +
                "Z+Xotz1mvGvLfGiM5hUp9h4UCUm5C13oGjuJb3zqyqHYIHbtO4cWjRrq1mJ0MQ9xm+WL7E/I463uxSNR" +
                "lV1XqmUDH2r38vGMtTWiSWxrPZiAO8Q9ekT8HMY+rXZXb6bSdcbTDMQU6O1r3R5Rx+4eiSCiATbhalap" +
                "h6XnNwiBDz9osaFGlPRwj1m5OtFDWKorkkIPsRRmfXmKF/hSAlxvPEP0uYeCPveMzzkft9l4DueTR2se" +
                "B3pGivwEjDUZLNUVaWZnx8VHGkS3RT72DALXhUWZX5+EQzTOCGFAlyZCI22YJL5z2B3FUuHmBMBqHrmn" +
                "+SZpBDx6l+R1CbaiuU/w4kfNL1FShUo0hkuwdY5OTvAABvqFVP/nA7FFN172NEDno0AbdAfviltWcjUB" +
                "hUar0VQgOJ5I4l0e6ap/PnjXx0p/mNMcS1vIy7M3H7BfKIKVkC41yvPxudrMNXXSecIquEleXvYvTvBw" +
                "CwlhN+b64WiULucWifP1lBjOn+p0dN3U1Ndz0JR6JDMe3ciEztIhrYC0zXulOJhGJUmMItFmHxEczE1h" +
                "z7IPjd4kpQ1zff1QQvHTQqXz+A//Ewxe/dA/vsHbbv94Z/kHiXP88TyAHgHDS4BR4YkgAzFGBWxg5peG" +
                "nU+0GGEJuXx+wqFl617JWcYIvfamUXFrbOzSH+GAnnB/56AXylB4uicL4qEKe4Bi60SGPiqiYCm+8MP1" +
                "4OIppnsl6PDz0flZwADASbcsDGLWbgDvbgUU1EqV9vEwVSi9oE9WQ5KtWXTaR7aQM01uzUHw6L+eIIWf" +
                "HDw5Rsvm5NWTbvCkyPMKnkyran7w9CmeaEyB2tWT/37EU+S0eZZzxCPTvCWtnlg3dLWQowKfJnsCnRKu" +
                "Bro1Rm7eHKewVblisNfQlw1+HdENt0hEvSvi5BXzhr1aDPe9jMyxAmSuuihIxHHkiC5YxkiSTJZC3gTm" +
                "ILAEoGdIAnjWJsHBV99+85JboOrlIilot4rxExnp+u9nASxbaTDOb9epMfD1b+n32oJh01DBk8Wk3P+a" +
                "n2BW5SD46uX+C/oJrQtskKCZKy1A7S/A4W09RgsFJ6IDaIKI387yuE7xPRVxVPn8iTI0sPbDH0oi3bk2" +
                "gskRPtMVHex0tFXk0X3wJZroXwaj3+FfMVW/UQXJwSEsjhm/f4b3kg3tz+f4c2R/vsCfsf25/8HdGPby" +
                "Az3bcmyjdSeNiwD4YVNS4CtX0bCV1rOXYz9Wk2Kl5WiapJr8x4btaJ7L5Ok91QgGWv01CqaFGR8+ki2x" +
                "SG6TXpGXvbyYPK3Gj/6zGv/1afSfwIajW7y4Fftcg0OPjnucj+qZXd2xVMH7An8lliVc2EQ3YO/FFrzr" +
                "OUdsxE87Ny7AbGNG23D/1xZHiDjTY4hAAbAv7KV1XHFF7ax7SU0kcKdOB8j4uyRGzx7fJq7zxlKNx0EJ" +
                "uwSPd5fLGZvrXbnAu3GTnj9aewZ9fGcx4sOSaw7gtwscKB6u93DiLRFpmdsSC78yi4nB9Vk2Puwo1PMK" +
                "rlbnqSWKoJsLd0XNPEqowokpvJM9B4OP6nU104gKlXW8vW5yA+auds8eC1od3QsVaIV8ZB1hngogQelt" +
                "QsOzoPUqDbIxYXZkAA9zWCHoQGGWF+ScNbCl5pwJFyrK2R65qp1QkloyDihlwdHFiW9fWkYTAKHPApgd" +
                "X3mlK7/9PUQciHH8ZpmAWwdwTfhKXJSWVBgX6xz05xbQ9kqaOo+9i6rNxsidlkO527L8yJu9cUfrorYz" +
                "Baqz+twJYB3WJycgxVoPj75Xg9UMuCQsGThJtlAXdsmlXqthGt3wut2pTWxjSXlx9ebVkbx46I+h2PHs" +
                "XXmF/Wti/xrav6KtV1NS7RrXzTWLmtpB3PcfgrXlSTdeZR5JTv/GOhdwcfDReO5ID1l5/vEjCDu6zFRe" +
                "Ppgf/ZGxKbq4f0JlhVgZC0YXB19Bs1BhN7QvjFkfWPQPZ+bUbm1YTlLBbP+siTTJYRZbt8v5AAGId+Ku" +
                "m8CfQbR/mFhU0kZlUnjWBHS+dN2hnCpmkJ7mo1E9ByW7iwqDSlrpSZSNlkqKnd6wetpDYyBJtSqMAVFA" +
                "Io3wBIozL3M9ISjdm5LjytDBeGRQLHme7drT1XhM0KDpx92yPHaXDfNpxlrP2Mg0wKtLQLL+bt0g7spX" +
                "FHJ5AV0u0pty0iDiovFFkVTKOCXWcn+Dahx3y5Y/McWfwvr0R6awkqQuKY9dl/KdKfmMlnw/62Hw3ohK" +
                "p/XpKzkSxe/4B8YbTy/eBPoPuJvwb69uAW/OX9oz5vMiH3GUR3yoxsd+BObR8c3pu37gwXzehIkmHYcw" +
                "gL2HhiLanwP48qrfP7+86Z9YwC+agAszMgleDhhh7GRk7Nd85HMACZVmUDGLOxQTfOQfjCYgLyIV+HNS" +
                "7gMC6AO6gw07N6aYJSjaqaBoVw+kvT0+7vdPPJT3myjj95TsnYbyRQbc38u1hNg0zNGrwZWjCw7zcs0w" +
                "w7xYd6Jj/UhxbT5JGls9O46SFBN0G9C76mNQ1eF3GHy1il5hUIlu4AAbJ2yxS/fTOOqB5sofrM4wp5dk" +
                "en+xnHjZNAHhPLtTDoOvt8B5lvXQQ8BN6JjPLp6l8PHR2ZnbyYfBXz4XQSkyXofh51BXYrfN1WoinY2T" +
                "wlbUVb4UIExM3JiEzybf/Asm8XlkRqZobD8eAIuRN/DE2eD6xgd1GHxLAI/sJ9Dk23AYxo0xYjzDYL2U" +
                "FSoJEErzrDPSbfgZe4+yPPmdYa9mkRRm3QfYQA+TC2rdn1oOp7rSrEii9WydeZqMDuuYYT2ZuE9WBJW5" +
                "r7b4tTNRvp0O56H7eC/QMdocfEVQOIK/4a1XNO8l/OU7BvzW5fy5SsxrNMFYgKuGcO+8j2uV+s00+daR" +
                "uzG5+dWwhydMmxJyzg3PwWMc5Y6+EgkshJcigFPHb0kRXeMFZvz79dHp2dur/uG3+E9HHl6eHV1cgBAJ" +
                "8W3/5HBPW59evDs6Oz0Jzwc3p4OLENsd7r2Ql97DUBoegbAPX/0c9i/enV4NLs77Fzfh8fdHF2/6h3v7" +
                "0u14cHFzNTizY72U528vjl6d9cObQXj097enV/1QDnUD0KPDva+k1c3pOQwxeHtzuPe1Yq/mweHeXygg" +
                "4Y7A2sJb+/FCZh+l3fXN0dVNCP++6cMUwuMByNJrmBRQ4NmaJu9OB2fw3+vw8ujme2h9cX1zdXR6cXMN" +
                "7Z8rMd8Mjs7awF747z4GZd9v6L3STrg2Lzut1XlzNXh7GV4cnQOVn3/VftmCBE2+bjW5GrwayBTh7V9a" +
                "b0G7/E2Bf9N6x1lTffstRVO4WLpB5tdX0CAEBC6uXw+uzkNlwr0Xzy1TCLGAXfrHf0NeBH54B+2QKaCh" +
                "UtDDFf9N75RowjCnF68H9h3dvumxQQOvi0F4+rfwenD29obWaf/BrvlbOUXmfX3nU5VKXP9abe7QqKP3" +
                "OrbPcHkgtvTBng2YNYvB9eQ2GpUS01qb7dH7MdxXDKhwfM1HefTTNmsqn9Z9sEUKvfeAUlqu6GDtcK23" +
                "lrm36+p3tRCeW7iafw8E1QXyBxUNKWqqyva+BFM+dR/wedr8bI/E+6dyExalPORyVXlkoXmfEepaE9J2" +
                "4jyJNsO6Kk1i+J8fImC9DcXXG1bzz/kW1MeQ0TVpLytaZMpifh5yBy+3zMG2B3gbzxw3a8xt7YDU3kSu" +
                "tNllRpQ1mxfVMS4bqtS9LzmtDJE1Pun0z43T5LKtfwxqU8xFvy7+j0Vd7LfJt/5Rcou3+4ounWf6H5nQ" +
                "yKN4fQAA";
                
    }
}
