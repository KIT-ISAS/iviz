/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
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
    
        public BatteryState()
        {
            CellVoltage = System.Array.Empty<float>();
            CellTemperature = System.Array.Empty<float>();
            Location = "";
            SerialNumber = "";
        }
        
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
            b.DeserializeStructArray(out CellVoltage);
            b.DeserializeStructArray(out CellTemperature);
            b.DeserializeString(out Location);
            b.DeserializeString(out SerialNumber);
        }
        
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
        
        public BatteryState RosDeserialize(ref ReadBuffer b) => new BatteryState(ref b);
        
        public BatteryState RosDeserialize(ref ReadBuffer2 b) => new BatteryState(ref b);
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                size += 4 * CellVoltage.Length;
                size += 4 * CellTemperature.Length;
                size += WriteBuffer.GetStringSize(Location);
                size += WriteBuffer.GetStringSize(SerialNumber);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4;  // Voltage
            c += 4;  // Temperature
            c += 4;  // Current
            c += 4;  // Charge
            c += 4;  // Capacity
            c += 4;  // DesignCapacity
            c += 4;  // Percentage
            c += 1;  // PowerSupplyStatus
            c += 1;  // PowerSupplyHealth
            c += 1;  // PowerSupplyTechnology
            c += 1;  // Present
            c = WriteBuffer2.Align4(c);
            c += 4;  // CellVoltage length
            c += 4 * CellVoltage.Length;
            c += 4;  // CellTemperature length
            c += 4 * CellTemperature.Length;
            c = WriteBuffer2.AddLength(c, Location);
            c = WriteBuffer2.AddLength(c, SerialNumber);
            return c;
        }
    
        public const string MessageType = "sensor_msgs/BatteryState";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4ddae7f048e32fda22cac764685e3974";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61WbW/iOBD+nl8xEh+WntR2u3t3u6q0HxBQQEcTVNL2qtUqMsmQWHVsznZo+fc3zguE" +
                "ttDd06FKBfuZZ2Y842fsdaCvpLFMWgNMI8SZMijBKsiZjTOwGQLKIjfAZflDcFk8wyNqicLrQIJLLjFx" +
                "u1zGokjwvEScr9QT6sgUq5XYnGXADKglrFEbriR8PvtCtiHRKYmQ8OUSNcoYgRtYKg3GbgSCRmYouNJt" +
                "3I6SbJkQQNyoY2YQpLKQ82eKw/088wgwc/6h8k98zBZmR+IVXNqvMAvuhzfR/HY2mz5E87AX3s6jW/8v" +
                "P7j34Rt8PILqj3s3o4k/ItjFEdhgMm8hPx1B+kHYJv18BHp1O50S5PdXWWbIhM2OZzke9qbh+L0sa9Qo" +
                "CAaHMqwhwd3whr6Gh9KrYYNhb3AorRbTXTANe6Nhmd2x4OezYT+66k2mtzcO/McRcD+YOs9/HoHc98L+" +
                "eBCMonByTZvDv2eTkvbLEZt572oYPry0+PqqJhbjTCqh0g104wxzbqzenByvUTjsj/1gGowe3qtTC+lP" +
                "rseHatWCTSeBf6hWe7BZcKhee7Crg8Xai60/OFSmPbZrvyyVN0aW0Bm6jqb/3lIoZj9/grUSlqUI208H" +
                "7uol0h/31UD3msmEWUWnvLWzmJNSkARobOzC1hLZDjDViAb6KAwnpehOllDInASIAAn4zN+xxYUmsbLt" +
                "KHxMmeVrhKeMxDPhJs6YTrlMods7geNsDtlKidj6tYN6i8LrZe+QsBWLud20SZqlyrwrmLGwLEg2G/A7" +
                "gSVoeCqjHfVrzgrys4ROrCmtXQGJsMqwtUPD4aObPhegmXTItxirNgJoz5io1vi6vG5kNDWod2gEaVwp" +
                "bTE5gzsmCqp3M77YQq3xbd5aVXe8C2Yt6q3c5mg1j3+FsaUJ+4xbgTjAtlBKuDhWGs2LDgx1QZ2yLGdl" +
                "Q0fDtEZ6TRG+/4AYhYh2N6kDPUkzVbONG9BcJnzNk4KJEtfcuGoqI6MHQblcvwWo7I8eHPp0YLJHuOUq" +
                "5KNUTxIWhQV6WizoppNrR2yg2jFoK2/UCVTxl9Hv3efjCbSg/ymJN/LYo/z1XKi+rimFikkyqN3bRXTv" +
                "q2adOkeRnvD6EdYqKpcGqy7uGqF2fjWsRJGeNB4IxJmI6t1W/yIpAVuttHrmeeVL7TdOZVnzet63//nj" +
                "Xc9Hl3Qrkyg3qTmvxJ4G59w66daJu1KMNJyVBct4mqE+FbhG4a4yHX4C5a7drNCclQ9JdyoGUpRUGEFj" +
                "tzAEovOLVZ4XktORIlie4549WVIPMOoAbXlcCKYJr3TCpYMvNcvRsdOfwX+K8n06GVyWsxvjwum9cFIY" +
                "u3eqO/DJAMoLT1pHBnTa32+UufjhdcIndUrrmFIdtlHQiTProsZnd0tdwMxckrPfqizPyAmdEpK7hAZS" +
                "uRbRT3MC5I1iIS2j3uhSCrONzVTVzWtGpVuI8iUd01EQ6wdn9OGkxSxLasmkaugrxp2Pn6GVW16X02lG" +
                "xRNl3xUpnWQpPoquDEEXm+oNL7hTLcEXmumN56wql17nyh02gciqLA13cm1UzKkSCTxxmzVdXZYl4onn" +
                "/Qt9Fo7EvAwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
