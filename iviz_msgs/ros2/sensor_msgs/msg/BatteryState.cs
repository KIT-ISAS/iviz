/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class BatteryState : IDeserializableRos2<BatteryState>, IMessageRos2
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
        public BatteryState(ref ReadBuffer2 b)
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
            b.DeserializeStructArray(out CellVoltage);
            b.DeserializeStructArray(out CellTemperature);
            b.DeserializeString(out Location);
            b.DeserializeString(out SerialNumber);
        }
        
        public BatteryState RosDeserialize(ref ReadBuffer2 b) => new BatteryState(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
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
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Voltage);
            WriteBuffer2.AddLength(ref c, Temperature);
            WriteBuffer2.AddLength(ref c, Current);
            WriteBuffer2.AddLength(ref c, Charge);
            WriteBuffer2.AddLength(ref c, Capacity);
            WriteBuffer2.AddLength(ref c, DesignCapacity);
            WriteBuffer2.AddLength(ref c, Percentage);
            WriteBuffer2.AddLength(ref c, PowerSupplyStatus);
            WriteBuffer2.AddLength(ref c, PowerSupplyHealth);
            WriteBuffer2.AddLength(ref c, PowerSupplyTechnology);
            WriteBuffer2.AddLength(ref c, Present);
            WriteBuffer2.AddLength(ref c, CellVoltage);
            WriteBuffer2.AddLength(ref c, CellTemperature);
            WriteBuffer2.AddLength(ref c, Location);
            WriteBuffer2.AddLength(ref c, SerialNumber);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/BatteryState";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
