
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
                
    }
}
