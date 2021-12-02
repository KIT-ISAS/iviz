/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class InteractiveMarkerControl : IDeserializable<InteractiveMarkerControl>, IMessage
    {
        // Represents a control that is to be displayed together with an interactive marker
        // Identifying string for this control.
        // You need to assign a unique value to this to receive feedback from the GUI
        // on what actions the user performs on this control (e.g. a button click).
        [DataMember (Name = "name")] public string Name;
        // Defines the local coordinate frame (relative to the pose of the parent
        // interactive marker) in which is being rotated and translated.
        // Default: Identity
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        // Orientation mode: controls how orientation changes.
        // INHERIT: Follow orientation of interactive marker
        // FIXED: Keep orientation fixed at initial state
        // VIEW_FACING: Align y-z plane with screen (x: forward, y:left, z:up).
        public const byte INHERIT = 0;
        public const byte FIXED = 1;
        public const byte VIEW_FACING = 2;
        [DataMember (Name = "orientation_mode")] public byte OrientationMode;
        // Interaction mode for this control
        // 
        // NONE: This control is only meant for visualization; no context menu.
        // MENU: Like NONE, but right-click menu is active.
        // BUTTON: Element can be left-clicked.
        // MOVE_AXIS: Translate along local x-axis.
        // MOVE_PLANE: Translate in local y-z plane.
        // ROTATE_AXIS: Rotate around local x-axis.
        // MOVE_ROTATE: Combines MOVE_PLANE and ROTATE_AXIS.
        public const byte NONE = 0;
        public const byte MENU = 1;
        public const byte BUTTON = 2;
        public const byte MOVE_AXIS = 3;
        public const byte MOVE_PLANE = 4;
        public const byte ROTATE_AXIS = 5;
        public const byte MOVE_ROTATE = 6;
        // "3D" interaction modes work with the mouse+SHIFT+CTRL or with 3D cursors.
        // MOVE_3D: Translate freely in 3D space.
        // ROTATE_3D: Rotate freely in 3D space about the origin of parent frame.
        // MOVE_ROTATE_3D: Full 6-DOF freedom of translation and rotation about the cursor origin.
        public const byte MOVE_3D = 7;
        public const byte ROTATE_3D = 8;
        public const byte MOVE_ROTATE_3D = 9;
        [DataMember (Name = "interaction_mode")] public byte InteractionMode;
        // If true, the contained markers will also be visible
        // when the gui is not in interactive mode.
        [DataMember (Name = "always_visible")] public bool AlwaysVisible;
        // Markers to be displayed as custom visual representation.
        // Leave this empty to use the default control handles.
        //
        // Note: 
        // - The markers can be defined in an arbitrary coordinate frame,
        //   but will be transformed into the local frame of the interactive marker.
        // - If the header of a marker is empty, its pose will be interpreted as 
        //   relative to the pose of the parent interactive marker.
        [DataMember (Name = "markers")] public Marker[] Markers;
        // In VIEW_FACING mode, set this to true if you don't want the markers
        // to be aligned with the camera view point. The markers will show up
        // as in INHERIT mode.
        [DataMember (Name = "independent_marker_orientation")] public bool IndependentMarkerOrientation;
        // Short description (< 40 characters) of what this control does,
        // e.g. "Move the robot". 
        // Default: A generic description based on the interaction mode
        [DataMember (Name = "description")] public string Description;
    
        /// Constructor for empty message.
        public InteractiveMarkerControl()
        {
            Name = string.Empty;
            Markers = System.Array.Empty<Marker>();
            Description = string.Empty;
        }
        
        /// Explicit constructor.
        public InteractiveMarkerControl(string Name, in GeometryMsgs.Quaternion Orientation, byte OrientationMode, byte InteractionMode, bool AlwaysVisible, Marker[] Markers, bool IndependentMarkerOrientation, string Description)
        {
            this.Name = Name;
            this.Orientation = Orientation;
            this.OrientationMode = OrientationMode;
            this.InteractionMode = InteractionMode;
            this.AlwaysVisible = AlwaysVisible;
            this.Markers = Markers;
            this.IndependentMarkerOrientation = IndependentMarkerOrientation;
            this.Description = Description;
        }
        
        /// Constructor with buffer.
        internal InteractiveMarkerControl(ref Buffer b)
        {
            Name = b.DeserializeString();
            b.Deserialize(out Orientation);
            OrientationMode = b.Deserialize<byte>();
            InteractionMode = b.Deserialize<byte>();
            AlwaysVisible = b.Deserialize<bool>();
            Markers = b.DeserializeArray<Marker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
            IndependentMarkerOrientation = b.Deserialize<bool>();
            Description = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new InteractiveMarkerControl(ref b);
        
        InteractiveMarkerControl IDeserializable<InteractiveMarkerControl>.RosDeserialize(ref Buffer b) => new InteractiveMarkerControl(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(ref Orientation);
            b.Serialize(OrientationMode);
            b.Serialize(InteractionMode);
            b.Serialize(AlwaysVisible);
            b.SerializeArray(Markers);
            b.Serialize(IndependentMarkerOrientation);
            b.Serialize(Description);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
            if (Description is null) throw new System.NullReferenceException(nameof(Description));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetArraySize(Markers);
                size += BuiltIns.GetStringSize(Description);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerControl";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b3c81e785788195d1840b86c28da1aac";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1YbXMaRxL+HH7FlFWpSBdAkuX4Eu74gAWyqUigA+zYlUpRAzvARMvOZmZXCP/6e7pn" +
                "dlmEFN+Hs22VtDs70+/9dPcciZFKrXIqyZyQYm6SzJpYZCuZCe1EZsRMiUi7NJZbFeF9qbKVsmKjs5WQ" +
                "idBJpqycZ/peibW0d8rWakeiH4GeXmx1shQus/RnYSyogmRg0cS2TyYXiWKyQjqnlwlEyBP9V67EvYzx" +
                "Gx/4EP5aNVfEZYEDMzm/Ewtr1viqxNv3fRAzidiQ1CSMSRx/yR1ETZUF87WjHVUJxLFqLpvgOMuzDN/m" +
                "sZ7fnTRrQeBErlWNlOmqhU6UJxibuYxBwNhIJzKDNBbbxLFVsWQbsMBKpMYpYRb+WVpYA4QObXWCNUit" +
                "5ysy9kwRX2syEI5gXNjFysSBsoqaXhCZx1krmDfb1pbKrFVmt9O1W7rT/+TYaRNoL4zV2AKRTMI6DHfv" +
                "Ym0i1SqM4MTKbKrbxXwlk6VyxLA/eNcb9SctcWXi+NE2KPeE74/EVf9jr9sSvyqV7u1f6AdSClGV6EzD" +
                "iI7UxIEP/d5v06vOZX/wtiU6MQXBtvFZIOAS5cPMza1SiTh+aFEUbaSN6mLbitUiq4vPrTyFz3LI8nMh" +
                "r2iLMxGWWBwsnIf3CjesvqyF5YqkU7IPB3GhXrDZQQhjD34Gw0GvJSbVwMKjSeKtWCuZZHzsXrtcxvoz" +
                "c/iXSAzvVQ8Z9iQ52fqmN3jfEtf6TjHFOkWlsHq5yhocmLyRKHuD05E37yeT4aAlerHCx0zMkY/IVjKM" +
                "P+PD5mb4oTftfOyPIWURT0LGBrHmw/mhIR+0K7feXndYo3IvYtRvLP1Ce0fDSWdSEB5x0AppTY6wfZKs" +
                "398Sl2Y943zaMeNYr9Ar/EmGqDqTbFTxpdef3Ri+F5pi7aI8tGPTFq/CYoUZVn+qbvWfsPoakr+46L7Y" +
                "xXkIBCc2xt752KQEXxvgzI/jd/2ryY+Xk9E1osl/vOiKeW6dsTsrXHSrll0gsBEnMDC2ulTOq5alrcGu" +
                "h/uEnBkECLFH7C41J6RHGo9Jj+zO1K7yOBavG93hFVOMgJ+EUUEcUo8cwQDELyULr0XgVHgn6ANL/XPf" +
                "qrz286FN/YdfipyrmDXkHCUdyZOrumeLHJGIlSjACwyvoYGMHZclJJWexYQhmxXwgU4sc005khjCmX2A" +
                "AodmbWaQnjLeyK2bFseJ7U2g/7jeSSR17jIYymcwqlColmwiMvK1koT7lP9qnWZbooF4YHEij9glMABa" +
                "o5ixlYDDZMBhPDSAHgWCuiKLcZQ1hxpYkHam4Se7Pag9dRAQDBZsG5xkf1LF49OhIPmU9NUq1KVD+CZ1" +
                "GuwCfF4pGaF6YrMMn0WhYl1odAtc4wqmTAym4crlSCshvlwUn5TB++L3PwqL+LhI9qCbvFkXTmVle0BR" +
                "I/RCbNFTRCb5AfYg8OX8DHSOgnsBxEsybZnAc1jFSrhYbSAkZGrueYR1dFQn8xREoB6cUtSaSlzpJFKp" +
                "wq8km/qz08d1eLwyNoNvUdJ0ykl2/G/x6oyKLpkB3E7IQtzI7PUqkVGOXM0ty4sbwyGnkKszk71oimp3" +
                "0BFLlSir53t8ZtJBZW6B1AGiFT1P5UCt1v4//6vdjFHhn2tZoAHX0DLBUOn2+hgyOqFWwD+8/bXrdyjc" +
                "m7XaIjYye/1KPJRP2/Lpc/m0+Vq67VV5r6APZvI8JF9lWdo6Pd1sNk1rXNPY5elG3+lTe68/n3Y96Ey2" +
                "qSqOMRz/7aFJnsFEAMRwwn1/0fn+5dkb6fQcf8crCWrcgayNJaORoUIDlwCmyOkcZ2vlnFyGhotIFyjd" +
                "GY2Gv7XPwtvl+ze9dlF/x7dIgV67qL6Xn677g25v1L4IC3jtTceTUf+2/aq6dN0fT9o/VSj6ldd7ZP1a" +
                "UVluh/3BZNwuisqk93EyreBB+5eyQxi/m4564+H70SUELcSGDJ3B2+tA9Py8VK7bLVW7GXb7V5/K127v" +
                "ujfZKedfO9fX0K72zkNjQMhn/h0V37lt1Gt16tG34oNy1HDPEfGEBjjnox4AhiChB3Kamf2p5gSizWaT" +
                "ag5XCyDGn3nic5u9qXm+Qg/NHVoxX9F4EzpaFQjVoOnFS9r/3Xd/I03g2u8Sy0UeP8mUqCal2ByAMtFp" +
                "ThUBGlNgRypW/LKTQFBXZIMcGTLheUkAF/QdYLknfAC150+dCRlFpwA9jKeMMHy6Ls7FMdAbQyZNWyd1" +
                "8dLLh+ypbLrYLaIk+FX3aAq7pSrHpe6Q+W2lAgax9w9/wJKxFxh5ZLxPAAjCa3unxXkd/3nOcGWzcZwT" +
                "CqFdpA8wp3AASqtOEG2R53JpYmNHb9904Dc8PeLDX8XvZ82zxnnz7I9alFuPGLFewGEImycN+w71kYeK" +
                "ingomnmMeUC6DIUXUaDCkCuBW5QDcxbUGzVqCnjH60Jb7xEJXFc5b6boYDDQPM2bWxZCMQ+bgS1KPR9t" +
                "+KPoWpqqiQJz0CBRN+OTk5iig4OSLlMpKvaQ5jifWt7wHJUuVXO90FhdoRtw8CC3fCGd1rhZQJX2mFWv" +
                "wGBdIFFPDuIFMqDb4cbDfR2OR0m+nvlmjj0OS6GrFQqZilXY6YxmFoU4iYtmbXeiEGwwpPFNxulKFj32" +
                "Fg0YifpEaEEjz4raHn/UlJqx2Dz9htYsACGtPbd/D9ofH4S/VlP0DSa3c+WjhpdweqqgSBQpCEjgQsXy" +
                "a9X/0gq+QFDVz4B0uLCgTJSRzCSrssJYr2wjRrTxPcg6hY78lXzNwyL3QvjhVo6ThA1BQG7Wa2A44VQI" +
                "1Mp5vmUCyKO9RnIBbe3BwEDU8ePgbZUAm/sYDAHfTs1zasN5zKRiwb0BQJ4LIJAVB2pHk41pUPO4RGiU" +
                "zMvbQvVAvRvJKV0LPP7hlWuCNoyjwCVy4pjXpnhFrwsmEEGlBjdgx5D8dputQo96L+ErzGdEmGACVH+g" +
                "Qz+cVCiT2C0UmsQU5D3FHY//hWxS0iWdGjyl8dVlvoQBsTG15l4jhMRs6weGmNpSAOKMZrIagyKzrB1d" +
                "MY4wGrFH6MbGOTPXfKlHtbGIWQ9rOvo2nTaVHijYeTTB+gQHkux113WKMlqOwndd3g5UOnKMHQxe5Yba" +
                "czeQ30ZBiFJkTrg58Je2pfw8yzKc7av7hbnhm4gfKv9TY5C452/7MvPM188e3TXyPXk4iYORRkPjXYWB" +
                "1nIFpuGdx0lCcNBYS1w5Yj9dWeAaPk1BTO5dCnGF9LfldX/Vwru4jpMUxbhJt0Phzno3ZhBNEZTDrc7i" +
                "pR+nWWbPDC4CkeLW6aRJ1ZymeJ7ft0D0AI08uxdycQpnxtSLuxaW4yAeyrkGsZABlL84I34dVx+WR88S" +
                "sGrLp2X5NCufZK32X0Lrd36iGQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
