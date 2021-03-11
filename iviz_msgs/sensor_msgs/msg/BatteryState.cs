/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/BatteryState")]
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
            CellVoltage = System.Array.Empty<float>();
            CellTemperature = System.Array.Empty<float>();
            Location = string.Empty;
            SerialNumber = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public BatteryState(in StdMsgs.Header Header, float Voltage, float Temperature, float Current, float Charge, float Capacity, float DesignCapacity, float Percentage, byte PowerSupplyStatus, byte PowerSupplyHealth, byte PowerSupplyTechnology, bool Present, float[] CellVoltage, float[] CellTemperature, string Location, string SerialNumber)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAACq1W328aORB+X6n/w0g8lJzU9Oe1VaQ+ICCAjgAKm/Sq0wmZ3YG1amzO9pLw39/n3SUs" +
                "aSDt6fYFdv3NNzP+xjOOGtQ22nmhvSNhmZLMONbkDa2ETzLyGRPrfOVI6uJFSZ3f03e2mlXUoJQXUnMa" +
                "VqVOVJ7y6wLxem3u2M5cvl6r7XlGwpFZ0Iatk0bT+/NPsI1BZzRTKhcLtqwTJuloYSw5v1VMloVDcIXb" +
                "pB4lbIVSBG62iXBM2nhayXvEEV7PIwAmwT+V/sEnfO72JFEutf9Mk/HX7vVsejOZDL/NpnErvpnObkZ/" +
                "jMZfR/SF3pxAtfut695g1APs7QlYZzCtId+dQI7GcZ30/Qno5c1wCMiHH7LMWCifnc6y320N4/5zWVao" +
                "3njcOZZhBRnfdq/xNz6WXgXrdFudY2nVmG7Hw7jV6xbZnQp+Oum2Z5etwfDmOoB/PwFuj4fB88cTkK+t" +
                "uN3vjHuzeHCFxe6fk0FB++mEzbR12Y2/Pbb4/IMmnpNMG2WWW2omGa+k83Z7dlqjuNvuj8bDce/bczrV" +
                "kKPBVf+YVjXYcDAeHdPqADYZH9PrAHZ5VKyD2NqdYzIdsF2F2D5GUZ9Fij0MFY3faKGM8O/f0cYoL5ZM" +
                "D0+DbqtP6D/hr6PmldCp8Aa7/GDneYVOgRaABlfZxbVPsO3w0jI7arNyEp2iOVhQrldoQACkNBKjPVuS" +
                "WzQrX49ixEvh5YbpLkPzTKVLMmGXUi+p2Tqj02wBWUsJbO3KQbWE8FrZMyRiLRLpt3WS3afSvKmE87TI" +
                "0TZ34GcCS9nJpZ7tqX/kLCE/SxiaNdLaCwjCMsPaCobDmzB93pIVOiCfYizLiKg+Y2ZVj6/kDSNjp0G1" +
                "ghFkeW2s5/ScboXKofdufIm52fDTvFVX3fPOhfdsH9rtir2Vya8w1nrCIeNDgzjCNjdGhTjWlt2jCoxt" +
                "jkpZFLNyR4dhWiGjnQh//U0JKzXbn6QGtTRmqhXbMKClTuVGprlQBW534sqpzAIXguJzdReA7N+jWuke" +
                "Pg0aHBA+cOX6uzZ3mua5J1wt5jjpcB2IHZUrjn3pDZUAxR9Hf3CeTydQg/6nJJ7I44Dy13OBvqEolUnQ" +
                "MlDudRHD/Wr3HZVj0E9kdQmriSq147KKm06ZvV9La5Uvz3YeAJJCzarVWv0yOoFYr625l6vSF0Ku+ygt" +
                "K97oRfTlf35eRFfT3gXOZTpbuaV7Xbb7F5idUwzFVNg0nCqBNi4KzTK5zNi+UrxhFU4z9j+lYtVv1+zO" +
                "i7tk2BhHS9bQRmHy5g4gbGFiVqtcS+wqk5crPrCHJcpAoAisl0muhAXe2FTqAF9YscJtshFgjv/Jiyvq" +
                "oHNRjG9O8tDy4QkX33BVDXs+6FBx5tHuYBA14jvzCq+8hAIPzrHXwodg+T6czxCncBfw8VuZ3Dm4sTsM" +
                "LylGUfFthld3RnCS4EK+NqiKJiKfbH0G/YJ4GwHR5rg2gzjBDoD1ZTB6eVZjDmFfkBYat/ySvmTc+/gZ" +
                "2sBS8oacXmXQDJd+VFy+xAYWbcfgsAA6xw0otGIlQ79Scm6F3UbBqnQZNS7DHgMEq0IR/ArnTCIhQEp3" +
                "0me7ei7UmMkUBfkve7Dz2LcMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
