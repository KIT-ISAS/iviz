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
        [DataMember (Name = "voltage")] public float Voltage; // Voltage in Volts (Mandatory)
        [DataMember (Name = "temperature")] public float Temperature; // Temperature in Degrees Celsius (If unmeasured NaN)
        [DataMember (Name = "current")] public float Current; // Negative when discharging (A)  (If unmeasured NaN)
        [DataMember (Name = "charge")] public float Charge; // Current charge in Ah  (If unmeasured NaN)
        [DataMember (Name = "capacity")] public float Capacity; // Capacity in Ah (last full capacity)  (If unmeasured NaN)
        [DataMember (Name = "design_capacity")] public float DesignCapacity; // Capacity in Ah (design capacity)  (If unmeasured NaN)
        [DataMember (Name = "percentage")] public float Percentage; // Charge percentage on 0 to 1 range  (If unmeasured NaN)
        [DataMember (Name = "power_supply_status")] public byte PowerSupplyStatus; // The charging status as reported. Values defined above
        [DataMember (Name = "power_supply_health")] public byte PowerSupplyHealth; // The battery health metric. Values defined above
        [DataMember (Name = "power_supply_technology")] public byte PowerSupplyTechnology; // The battery chemistry. Values defined above
        [DataMember (Name = "present")] public bool Present; // True if the battery is present
        [DataMember (Name = "cell_voltage")] public float[] CellVoltage; // An array of individual cell voltages for each cell in the pack
        // If individual voltages unknown but number of cells known set each to NaN
        [DataMember (Name = "cell_temperature")] public float[] CellTemperature; // An array of individual cell temperatures for each cell in the pack
        // If individual temperatures unknown but number of cells known set each to NaN
        [DataMember (Name = "location")] public string Location; // The location into which the battery is inserted. (slot number or plug)
        [DataMember (Name = "serial_number")] public string SerialNumber; // The best approximation of the battery serial number
    
        /// Constructor for empty message.
        public BatteryState()
        {
            CellVoltage = System.Array.Empty<float>();
            CellTemperature = System.Array.Empty<float>();
            Location = string.Empty;
            SerialNumber = string.Empty;
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal BatteryState(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new BatteryState(ref b);
        
        BatteryState IDeserializable<BatteryState>.RosDeserialize(ref Buffer b) => new BatteryState(ref b);
    
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
            b.SerializeStructArray(CellVoltage);
            b.SerializeStructArray(CellTemperature);
            b.Serialize(Location);
            b.Serialize(SerialNumber);
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
                size += BuiltIns.GetStringSize(Location);
                size += BuiltIns.GetStringSize(SerialNumber);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/BatteryState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4ddae7f048e32fda22cac764685e3974";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1W34/aRhB+918xEg/hKuWSNG0SnZQHBBygcoAO36VRVaHFHvAq6126u+aO/77f2uYw" +
                "SeCSqn4Be7/55ufOTNSirtHOC+0dCcuUZMaxJm8oFz7JyGdMrIvckdTli5K6eKQvbDWrqEUpr6TmNJxK" +
                "nagi5Vcl4tXGPLBduGKzUbvLjIQjs6ItWyeNpreX7yEbg85oplSuVmxZJ0zS0cpYcn6nmCwLB+NKtUnT" +
                "SsgKpQjcbBPhmLTxlMtH2BFeLyMAZkE/VfrBJ3zhDiRRIbX/QLPpp/7tYn43m40/L+ZxJ76bL+4mf0ym" +
                "nyb0kV6fQXWHndvBaDIA7M0ZWG80byB/PYOcTOMm6dsz0Ou78RiQ377xMmOhfHbey2G/M46Hz3lZowbT" +
                "ae+UhzVket+/xd/4lHs1rNfv9E651WC6n47jzqBfenfO+Pms311cd0bju9sA/v0MuDsdB83vzkA+deLu" +
                "sDcdLOLRDQ77f85GJe37MzLzznU//vy1xIdvcuI5ybRRZr2jdpJxLp23u4vzOYr73eFkOp4OPj+XpwZy" +
                "MroZnspVAzYeTSencnUEm01P5esIdn0yWUe2dXun0nTEdhNsexdFQxYpYhgqGr/RShnh3/5KW6O8WDM9" +
                "PS26rz+h/4S/jto3QqfCG0T5Sc5zjk6BFoAGV8vFjU+Q7fHaMjvqsnISnaI9WlGhczQgAFKaiMmBLSks" +
                "mpVvWjHhtfByy/SQoXmm0iWZsGup19TuXNB5toBsuAS2bq2gPoJ5newZErERifS7Jsn+UyXeVsJ5WhVo" +
                "m3vwM4al7ORaLw7U33JWkB8lDM0abh0SCMLKw8YJhsPrMH3ekBU6IL/HWJURUXPGLOoeX6c3jIx9DuoT" +
                "jCDLG2M9p5d0L1SBfO/Hl1iaLX+ft+6qB96l8J7tU7vN2VuZ/AxjoyccMz41iBNsS2NUsGNj2X1VgbEt" +
                "UCmrclbu6TBMa2S0T8Jff1PCSi0ON6lFHY2ZasUuDGipU7mVaSFUidvfuGoqs8BCUH6udwGk/UvUKN3j" +
                "p0WjI8InrkJ/0eZB07LwhNViiZsO1YHYUXXi2FfaUAnI+NfWH93n8w40oP/Jie/4cUT5874gv6EolUnQ" +
                "MlDuzSSG/Wr/HZVj0E9kvYQ1kiq146qK206Zg15LG1WsL/YaAJJCLerTRv0yOoHYbKx5lHmlCyY3dVSS" +
                "NW8Uffyfn+hmPrjCrUwXuVu7V1Wzx+CcYyKmwqbhSgn0cFEmLJPrjO1LxVtW4Soj+CmVp363YXdZLpIh" +
                "Ko7WrJEYhbFbOIAQv8TkeaElQsrkZc5H8pBEDQhUgPUyKZSwwBubSh3gKytyrJKtAHP8T1Hup6PeVTm7" +
                "OSlCv4cmbL1hTw0BH/WovPDodRCIWvGDeYlXXiP8T8oRaOGDsfwYLmewU7gr6Pilcu4S3AgOQ0uKOVR+" +
                "W+DVXRCUwAS0MJREG5bPdj5D8kLmtgIZW2JnBnGCCID1RRB6cdFgDmZfkRYaK35FXzEedPwIbWCpeINP" +
                "LzPkDBs/yq1YI4BlzzG4KYAusf6EPqxkaFZKLq2wuyhIVSqj1nWIMUCQKjOCX+GcSSQSkNKD9Nm+mMts" +
                "LGQaRf8CT1y4CrMMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
