/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/BatteryState")]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "voltage")] public float Voltage { get; set; } // Voltage in Volts (Mandatory)
        [DataMember (Name = "temperature")] public float Temperature { get; set; } // Temperature in Degrees Celsius (If unmeasured NaN)
        [DataMember (Name = "current")] public float Current { get; set; } // Negative when discharging (A)  (If unmeasured NaN)
        [DataMember (Name = "charge")] public float Charge { get; set; } // Current charge in Ah  (If unmeasured NaN)
        [DataMember (Name = "capacity")] public float Capacity { get; set; } // Capacity in Ah (last full capacity)  (If unmeasured NaN)
        [DataMember (Name = "design_capacity")] public float DesignCapacity { get; set; } // Capacity in Ah (design capacity)  (If unmeasured NaN)
        [DataMember (Name = "percentage")] public float Percentage { get; set; } // Charge percentage on 0 to 1 range  (If unmeasured NaN)
        [DataMember (Name = "power_supply_status")] public byte PowerSupplyStatus { get; set; } // The charging status as reported. Values defined above
        [DataMember (Name = "power_supply_health")] public byte PowerSupplyHealth { get; set; } // The battery health metric. Values defined above
        [DataMember (Name = "power_supply_technology")] public byte PowerSupplyTechnology { get; set; } // The battery chemistry. Values defined above
        [DataMember (Name = "present")] public bool Present { get; set; } // True if the battery is present
        [DataMember (Name = "cell_voltage")] public float[] CellVoltage { get; set; } // An array of individual cell voltages for each cell in the pack
        // If individual voltages unknown but number of cells known set each to NaN
        [DataMember (Name = "cell_temperature")] public float[] CellTemperature { get; set; } // An array of individual cell temperatures for each cell in the pack
        // If individual temperatures unknown but number of cells known set each to NaN
        [DataMember (Name = "location")] public string Location { get; set; } // The location into which the battery is inserted. (slot number or plug)
        [DataMember (Name = "serial_number")] public string SerialNumber { get; set; } // The best approximation of the battery serial number
    
        /// <summary> Constructor for empty message. </summary>
        public BatteryState()
        {
            Header = new StdMsgs.Header();
            CellVoltage = System.Array.Empty<float>();
            CellTemperature = System.Array.Empty<float>();
            Location = "";
            SerialNumber = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public BatteryState(StdMsgs.Header Header, float Voltage, float Temperature, float Current, float Charge, float Capacity, float DesignCapacity, float Percentage, byte PowerSupplyStatus, byte PowerSupplyHealth, byte PowerSupplyTechnology, bool Present, float[] CellVoltage, float[] CellTemperature, string Location, string SerialNumber)
        {
            this.Header = Header;
            this.Voltage = Voltage;
            this.Temperature = Temperature;
            this.Current = Current;
            this.Charge = Charge;
            this.Capacity = Capacity;
            this.DesignCapacity = DesignCapacity;
            this.Percentage = Percentage;
            this.PowerSupplyStatus = PowerSupplyStatus;
            this.PowerSupplyHealth = PowerSupplyHealth;
            this.PowerSupplyTechnology = PowerSupplyTechnology;
            this.Present = Present;
            this.CellVoltage = CellVoltage;
            this.CellTemperature = CellTemperature;
            this.Location = Location;
            this.SerialNumber = SerialNumber;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public BatteryState(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Voltage = b.Deserialize<float>();
            Temperature = b.Deserialize<float>();
            Current = b.Deserialize<float>();
            Charge = b.Deserialize<float>();
            Capacity = b.Deserialize<float>();
            DesignCapacity = b.Deserialize<float>();
            Percentage = b.Deserialize<float>();
            PowerSupplyStatus = b.Deserialize<byte>();
            PowerSupplyHealth = b.Deserialize<byte>();
            PowerSupplyTechnology = b.Deserialize<byte>();
            Present = b.Deserialize<bool>();
            CellVoltage = b.DeserializeStructArray<float>();
            CellTemperature = b.DeserializeStructArray<float>();
            Location = b.DeserializeString();
            SerialNumber = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BatteryState(ref b);
        }
        
        BatteryState IDeserializable<BatteryState>.RosDeserialize(ref Buffer b)
        {
            return new BatteryState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
            b.SerializeStructArray(CellVoltage, 0);
            b.SerializeStructArray(CellTemperature, 0);
            b.Serialize(Location);
            b.Serialize(SerialNumber);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (CellVoltage is null) throw new System.NullReferenceException(nameof(CellVoltage));
            if (CellTemperature is null) throw new System.NullReferenceException(nameof(CellTemperature));
            if (Location is null) throw new System.NullReferenceException(nameof(Location));
            if (SerialNumber is null) throw new System.NullReferenceException(nameof(SerialNumber));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                size += 4 * CellVoltage.Length;
                size += 4 * CellTemperature.Length;
                size += BuiltIns.UTF8.GetByteCount(Location);
                size += BuiltIns.UTF8.GetByteCount(SerialNumber);
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
                "H4sIAAAAAAAACq1X328iNxB+R+J/GCkPl1S65H60vVOke0BAAJUACptco6pCZndgrRib2l4S/vt+3l1g" +
                "uQtwPZUX2PU338z4G8+Yeu2MmkY7L7R3JCxTnBrHmryhhfBxSj5lYp0tHEmdPyipsxd6YqtZ1WGe8Exq" +
                "TsKy1LHKEr7KIVdL88x24rLlUq0vUxKOzIxWbJ00mj5efgrGEQiNZkrkbMaWdcwkHc2MJefXismycAgv" +
                "dxxX4wzGQikCO9tYOCZtPC3kCyIJj5f1WoCMQgxUxABK4TO346nXMqn9ZxoNv7bvJuP70aj/OBlHjeh+" +
                "PLkf/DEYfh3QF3p3DNbsNu46vUEHuPfHcK3euAL9cAw6GEZV2o/HsDf3/T4wv76SbMpC+fREst12ox91" +
                "TyZbwjrDYetgoiVm+NC+w8/oYJYlrtVutA5mV+F6GPajRqddJHksg/Go3ZzcNHr9+7uA/u0YujnsB+e/" +
                "H8N8bUTNbmvYmUS9Wyy2/xz1cuJPx4zGjZt29PityedX5PEcp9ooM1/TeZzyQjpv1xcn5Iraze5g2B92" +
                "Hk9KVoEOerfdg7JVcP3ecHBQtj3caHhQuj3czWHd9uJrtg4qtsd3OyhUq9e6LBJsZqhyfNdrM2WE//iB" +
                "VkZ5MWfafs7ooXyF/hR+Ojq/FToR3mC/d4aeF2gk6A9ogaVhVHkF4xbPLbOjJisn0UbOezPK9AINCoCE" +
                "BmJQoYszi27mq3EMeC68XDE9p+iviXRxKuxc6jmdNy7oBF2AVrICXbP0UC4hwEZ6ikUsRSz9usqyeVXY" +
                "nyvhPM0yNNYN+FRoCTs515Md9/ekBeSHGUNDR2Y7GcFYJFlZwQh5F4bUe7JCB+SrlEVBEVVH0aQcA6XI" +
                "YbBshChXMKksL431nFzSg1AZVN9MOTE1Kz5AXLbcHfFUeM9224sX7K2M/xNlpU3sU257xiG6qTEqRLK0" +
                "7L6pxMhmKJhZPlQ3fJi6JTIcr1KKv/6mmJWa7E7VGTU0xq8V6zDMpU7kSiaZUDluc/qKAc4Ct4f8dXlx" +
                "gPpP9VqliPc/Z9TbY9ySZfpJm2dN08wTLiJTnHv4DsyOihXHvnCHgoDw38W/d7iPp1CB/lwar2Syx/kT" +
                "2UDmUJ3KxGggKPyqlOFCtnmPAjLoLrK8tVWkldpxUc7nTpmdY0tLlc0vti6AkkJNyuVKITPaglgurXmR" +
                "i8IZgq46KSxL4lBCX/7nT712O+5c44gmk4Wbu6tiAoS5OsbATIRNwvkSaOwi1y2V85TtW8UrVuFgQ4KE" +
                "8lW/XrLD/TCkFvbG0Zw19FEYy5kDCrsYm8Ui0xIby+TlgvcIgimKQaAUrJdxpoSFgbGJ1AE/s2IR7p9n" +
                "OdDxP1l+r+21rvPxznEWxgCc4boc7rdh43stKjoA+h8sYBg9m7d45jmE2EaALRc+RMwv4bSGYIW7Dm5+" +
                "KXK8BD02ieEowYjK303w6C4IfhAF+lqMlozwR2ufQscg4kpAvCmu22COsQ+gfROM3qAydtQh9GvSQuMP" +
                "QsFfUO6c/AhvYCmJQ1pvU4iH/wuovWyOfcz7kMHBAXaKa1Joz0qGBqbk1Aq7rteCWeEUJDdhswGDXa4N" +
                "voVzJpZQIqFn6dNtcee6TGQSqvNfQt2vG/gMAAA=";
                
    }
}
