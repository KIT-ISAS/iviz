
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class BatteryState : IMessage 
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
        
        public std_msgs.Header header;
        public float voltage; // Voltage in Volts (Mandatory)
        public float temperature; // Temperature in Degrees Celsius (If unmeasured NaN)
        public float current; // Negative when discharging (A)  (If unmeasured NaN)
        public float charge; // Current charge in Ah  (If unmeasured NaN)
        public float capacity; // Capacity in Ah (last full capacity)  (If unmeasured NaN)
        public float design_capacity; // Capacity in Ah (design capacity)  (If unmeasured NaN)
        public float percentage; // Charge percentage on 0 to 1 range  (If unmeasured NaN)
        public byte power_supply_status; // The charging status as reported. Values defined above
        public byte power_supply_health; // The battery health metric. Values defined above
        public byte power_supply_technology; // The battery chemistry. Values defined above
        public bool present; // True if the battery is present
        
        public float[] cell_voltage; // An array of individual cell voltages for each cell in the pack
        // If individual voltages unknown but number of cells known set each to NaN
        public float[] cell_temperature; // An array of individual cell temperatures for each cell in the pack
        // If individual temperatures unknown but number of cells known set each to NaN
        public string location; // The location into which the battery is inserted. (slot number or plug)
        public string serial_number; // The best approximation of the battery serial number

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/BatteryState";

        public IMessage Create() => new BatteryState();

        public int GetLength()
        {
            int size = 48;
            size += header.GetLength();
            size += 4 * cell_voltage.Length;
            size += 4 * cell_temperature.Length;
            size += location.Length;
            size += serial_number.Length;
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public BatteryState()
        {
            header = new std_msgs.Header();
            cell_voltage = new float[0];
            cell_temperature = new float[0];
            location = "";
            serial_number = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out voltage, ref ptr, end);
            BuiltIns.Deserialize(out temperature, ref ptr, end);
            BuiltIns.Deserialize(out current, ref ptr, end);
            BuiltIns.Deserialize(out charge, ref ptr, end);
            BuiltIns.Deserialize(out capacity, ref ptr, end);
            BuiltIns.Deserialize(out design_capacity, ref ptr, end);
            BuiltIns.Deserialize(out percentage, ref ptr, end);
            BuiltIns.Deserialize(out power_supply_status, ref ptr, end);
            BuiltIns.Deserialize(out power_supply_health, ref ptr, end);
            BuiltIns.Deserialize(out power_supply_technology, ref ptr, end);
            BuiltIns.Deserialize(out present, ref ptr, end);
            BuiltIns.Deserialize(out cell_voltage, ref ptr, end, 0);
            BuiltIns.Deserialize(out cell_temperature, ref ptr, end, 0);
            BuiltIns.Deserialize(out location, ref ptr, end);
            BuiltIns.Deserialize(out serial_number, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(voltage, ref ptr, end);
            BuiltIns.Serialize(temperature, ref ptr, end);
            BuiltIns.Serialize(current, ref ptr, end);
            BuiltIns.Serialize(charge, ref ptr, end);
            BuiltIns.Serialize(capacity, ref ptr, end);
            BuiltIns.Serialize(design_capacity, ref ptr, end);
            BuiltIns.Serialize(percentage, ref ptr, end);
            BuiltIns.Serialize(power_supply_status, ref ptr, end);
            BuiltIns.Serialize(power_supply_health, ref ptr, end);
            BuiltIns.Serialize(power_supply_technology, ref ptr, end);
            BuiltIns.Serialize(present, ref ptr, end);
            BuiltIns.Serialize(cell_voltage, ref ptr, end, 0);
            BuiltIns.Serialize(cell_temperature, ref ptr, end, 0);
            BuiltIns.Serialize(location, ref ptr, end);
            BuiltIns.Serialize(serial_number, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4ddae7f048e32fda22cac764685e3974";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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

    }
}
