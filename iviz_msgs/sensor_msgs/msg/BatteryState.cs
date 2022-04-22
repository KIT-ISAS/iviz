/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class BatteryState : IDeserializable<BatteryState>, IMessage
    {
        // Constants are chosen to match the enums in the linux kernel
        // defined in include/linux/power_supply.h as of version 3.7
        // The one difference is for style reasons the constants are
        // all uppercase not mixed case.
        // Power supply status constants
        public const byte POWER_SUPPLY_STATUS_UNKNOWN = 0;
        public const byte POWER_SUPPLY_STATUS_CHARGING = 1;
        public const byte POWER_SUPPLY_STATUS_DISCHARGING = 2;
        public const byte POWER_SUPPLY_STATUS_NOT_CHARGING = 3;
        public const byte POWER_SUPPLY_STATUS_FULL = 4;
        // Power supply health constants
        public const byte POWER_SUPPLY_HEALTH_UNKNOWN = 0;
        public const byte POWER_SUPPLY_HEALTH_GOOD = 1;
        public const byte POWER_SUPPLY_HEALTH_OVERHEAT = 2;
        public const byte POWER_SUPPLY_HEALTH_DEAD = 3;
        public const byte POWER_SUPPLY_HEALTH_OVERVOLTAGE = 4;
        public const byte POWER_SUPPLY_HEALTH_UNSPEC_FAILURE = 5;
        public const byte POWER_SUPPLY_HEALTH_COLD = 6;
        public const byte POWER_SUPPLY_HEALTH_WATCHDOG_TIMER_EXPIRE = 7;
        public const byte POWER_SUPPLY_HEALTH_SAFETY_TIMER_EXPIRE = 8;
        // Power supply technology (chemistry) constants
        public const byte POWER_SUPPLY_TECHNOLOGY_UNKNOWN = 0;
        public const byte POWER_SUPPLY_TECHNOLOGY_NIMH = 1;
        public const byte POWER_SUPPLY_TECHNOLOGY_LION = 2;
        public const byte POWER_SUPPLY_TECHNOLOGY_LIPO = 3;
        public const byte POWER_SUPPLY_TECHNOLOGY_LIFE = 4;
        public const byte POWER_SUPPLY_TECHNOLOGY_NICD = 5;
        public const byte POWER_SUPPLY_TECHNOLOGY_LIMN = 6;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> Voltage in Volts (Mandatory) </summary>
        [DataMember (Name = "voltage")] public float Voltage;
        /// <summary> Temperature in Degrees Celsius (If unmeasured NaN) </summary>
        [DataMember (Name = "temperature")] public float Temperature;
        /// <summary> Negative when discharging (A)  (If unmeasured NaN) </summary>
        [DataMember (Name = "current")] public float Current;
        /// <summary> Current charge in Ah  (If unmeasured NaN) </summary>
        [DataMember (Name = "charge")] public float Charge;
        /// <summary> Capacity in Ah (last full capacity)  (If unmeasured NaN) </summary>
        [DataMember (Name = "capacity")] public float Capacity;
        /// <summary> Capacity in Ah (design capacity)  (If unmeasured NaN) </summary>
        [DataMember (Name = "design_capacity")] public float DesignCapacity;
        /// <summary> Charge percentage on 0 to 1 range  (If unmeasured NaN) </summary>
        [DataMember (Name = "percentage")] public float Percentage;
        /// <summary> The charging status as reported. Values defined above </summary>
        [DataMember (Name = "power_supply_status")] public byte PowerSupplyStatus;
        /// <summary> The battery health metric. Values defined above </summary>
        [DataMember (Name = "power_supply_health")] public byte PowerSupplyHealth;
        /// <summary> The battery chemistry. Values defined above </summary>
        [DataMember (Name = "power_supply_technology")] public byte PowerSupplyTechnology;
        /// <summary> True if the battery is present </summary>
        [DataMember (Name = "present")] public bool Present;
        /// <summary> An array of individual cell voltages for each cell in the pack </summary>
        [DataMember (Name = "cell_voltage")] public float[] CellVoltage;
        // If individual voltages unknown but number of cells known set each to NaN
        /// <summary> An array of individual cell temperatures for each cell in the pack </summary>
        [DataMember (Name = "cell_temperature")] public float[] CellTemperature;
        // If individual temperatures unknown but number of cells known set each to NaN
        /// <summary> The location into which the battery is inserted. (slot number or plug) </summary>
        [DataMember (Name = "location")] public string Location;
        /// <summary> The best approximation of the battery serial number </summary>
        [DataMember (Name = "serial_number")] public string SerialNumber;
    
        /// Constructor for empty message.
        public BatteryState()
        {
            CellVoltage = System.Array.Empty<float>();
            CellTemperature = System.Array.Empty<float>();
            Location = "";
            SerialNumber = "";
        }
        
        /// Constructor with buffer.
        public BatteryState(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Voltage);
            b.Deserialize(out Temperature);
            b.Deserialize(out Current);
            b.Deserialize(out Charge);
            b.Deserialize(out Capacity);
            b.Deserialize(out DesignCapacity);
            b.Deserialize(out Percentage);
            b.Deserialize(out PowerSupplyStatus);
            b.Deserialize(out PowerSupplyHealth);
            b.Deserialize(out PowerSupplyTechnology);
            b.Deserialize(out Present);
            CellVoltage = b.DeserializeStructArray<float>();
            CellTemperature = b.DeserializeStructArray<float>();
            Location = b.DeserializeString();
            SerialNumber = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new BatteryState(ref b);
        
        public BatteryState RosDeserialize(ref ReadBuffer b) => new BatteryState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Voltage);
            b.Serialize(Temperature);
            b.Serialize(Current);
            b.Serialize(Charge);
            b.Serialize(Capacity);
            b.Serialize(DesignCapacity);
            b.Serialize(Percentage);
            b.Serialize(PowerSupplyStatus);
            b.Serialize(PowerSupplyHealth);
            b.Serialize(PowerSupplyTechnology);
            b.Serialize(Present);
            b.SerializeStructArray(CellVoltage);
            b.SerializeStructArray(CellTemperature);
            b.Serialize(Location);
            b.Serialize(SerialNumber);
        }
        
        public void RosValidate()
        {
            if (CellVoltage is null) BuiltIns.ThrowNullReference();
            if (CellTemperature is null) BuiltIns.ThrowNullReference();
            if (Location is null) BuiltIns.ThrowNullReference();
            if (SerialNumber is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                size += 4 * CellVoltage.Length;
                size += 4 * CellTemperature.Length;
                size += BuiltIns.GetStringSize(Location);
                size += BuiltIns.GetStringSize(SerialNumber);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/BatteryState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4ddae7f048e32fda22cac764685e3974";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61WbW/iOBD+nl8xEh+WnlS6L3e3q0r7AQEFdDRBJW2vOp0ikwyJVcfmbIeWf3/jvEBo" +
                "C909XVSpJH7mmRnP+Bl7HRgoaSyT1gDTCHGmDEqwCnJm4wxshoCyyA1wWb4ILotneEQtUXgdSHDFJSZu" +
                "lctYFAlelIiLtXpCHZlivRbbXgbMgFrBBrXhSsKX3leyDYlOSYSEr1aoUcYI3MBKaTB2KxA0MkPBlW7j" +
                "dpRky4QA4kYdM4MglYWcP1Mc7rXnEWDu/EPln/iYLcyexCu4tN9gHtyPbqLF7Xw+e4gWYT+8XUS3/h9+" +
                "cO/Dd/h4AjWY9G/GU39MsE8nYMPpooX8fALpB2Gb9MsJ6NXtbEaQX19lmSETNjud5WTUn4WT97KsUeMg" +
                "GB7LsIYEd6Mb+hkeS6+GDUf94bG0Wkx3wSzsj0dldqeCX8xHg+iqP53d3jjwbyfAg2DmPP9+AnLfDweT" +
                "YTCOwuk1LY7+nE9L2q8nbBb9q1H48NLi26uaWIwzqYRKt9CNM8y5sXp7drpG4Wgw8YNZMH54r04tpD+9" +
                "nhyrVQs2mwb+sVodwObBsXodwK6OFusgtsHwWJkO2K79slTeBFlCe+g6mv57K6GY/fIZNkpYliLsng7c" +
                "1Z9If9xPA91rJhNmFe3yzs5iTkpBEqCxsQtbn8h2iKlGNDBAYTgpRXe6gkLmJEAESMBn/p4tLjSJlW1H" +
                "4WPKLN8gPGUkngk3ccZ0ymUK3f4ZnGZzyFZKxDaoHdRLFF4/e4eErVnM7bZN0nyqzLuCGQurgmSzAb8T" +
                "WIKGpzLaU7/mrCA/SujEmtLaF5AIqwxbKzQcPrrp8wk0kw75FmPVRgDtGRPVGl+X142Mpgb1Co0gjWul" +
                "LSY9uGOioHo344st1Qbf5q1Vdc+7ZNai3sltjlbz+GcYW5pwyLgTiCNsS6WEi2Ot0bzowFAX1CmrclY2" +
                "dDRMa6TXFOGvvyFGIaL9SepAX9JM1WzrBjSXCd/wpGCixDUnrprKyOhCUH6u7wJU9kcPjj0dmB4Q7rgK" +
                "+SjVk4RlYYGuFks66eTaERuoVgzayht1AlX8ZfQH5/l0Ai3of0rijTwOKH8+F6qva0qhYpIMavd2Ed39" +
                "qvlOnaNIT3h9CWsVlUuDVRd3jVB7vxrWokjPGg8E4kxE9Wqrf5GUgK3XWj3zvPKlDhunsqx5Pe/7//x4" +
                "14vxJZ3KJMpNai4qsafBubBOunXijhQjDWdlwTKeZqjPBW5QuKNMm59AuWq3azS98iLpdsVAipIKI2js" +
                "FoZAtH+xyvNCctpSBMtzPLAnS+oBRh2gLY8LwTThlU64dPCVZjk6dvoz+E9R3k+nw8tydmNcOL0XTgpj" +
                "d091Gz4dQnngSevIwOuET+qcXjGl7d85p41m1gWLz+5wujiZuSQfv1TJ9YibNgfJS0JzqPwW0as5A3JC" +
                "IZCEUUt0KfL51maqauINo4otRXmBjmkHiPWDM/pw1mKWJbVkUjX0FePex4/Qyh2vy+k8o5qJst2KlDaw" +
                "1BxFJ4Wgy211dRfciZXgS8301nNWlUuvc+X2mEBkVVaEO5U2KuZUgASeuM2aZi6rEfHE8/4FT1y4CrMM" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
