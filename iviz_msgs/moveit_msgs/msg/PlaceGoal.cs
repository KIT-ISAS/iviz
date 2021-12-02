/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceGoal : IDeserializable<PlaceGoal>, IGoal<PlaceActionGoal>
    {
        // An action for placing an object
        // which group to be used to plan for grasping
        [DataMember (Name = "group_name")] public string GroupName;
        // the name of the attached object to place
        [DataMember (Name = "attached_object_name")] public string AttachedObjectName;
        // a list of possible transformations for placing the object
        [DataMember (Name = "place_locations")] public PlaceLocation[] PlaceLocations;
        // if the user prefers setting the eef pose (same as in pick) rather than 
        // the location of the object, this flag should be set to true
        [DataMember (Name = "place_eef")] public bool PlaceEef;
        // the name that the support surface (e.g. table) has in the collision world
        // can be left empty if no name is available
        [DataMember (Name = "support_surface_name")] public string SupportSurfaceName;
        // whether collisions between the gripper and the support surface should be acceptable
        // during move from pre-place to place and during retreat. Collisions when moving to the
        // pre-place location are still not allowed even if this is set to true.
        [DataMember (Name = "allow_gripper_support_collision")] public bool AllowGripperSupportCollision;
        // Optional constraints to be imposed on every point in the motion plan
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints;
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId;
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of placing;
        // CAREFUL: If the object name 'all' is used, collisions with all objects are disabled during the approach & retreat.
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects;
        // The maximum amount of time the motion planner is allowed to plan for
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// Constructor for empty message.
        public PlaceGoal()
        {
            GroupName = string.Empty;
            AttachedObjectName = string.Empty;
            PlaceLocations = System.Array.Empty<PlaceLocation>();
            SupportSurfaceName = string.Empty;
            PathConstraints = new Constraints();
            PlannerId = string.Empty;
            AllowedTouchObjects = System.Array.Empty<string>();
            PlanningOptions = new PlanningOptions();
        }
        
        /// Explicit constructor.
        public PlaceGoal(string GroupName, string AttachedObjectName, PlaceLocation[] PlaceLocations, bool PlaceEef, string SupportSurfaceName, bool AllowGripperSupportCollision, Constraints PathConstraints, string PlannerId, string[] AllowedTouchObjects, double AllowedPlanningTime, PlanningOptions PlanningOptions)
        {
            this.GroupName = GroupName;
            this.AttachedObjectName = AttachedObjectName;
            this.PlaceLocations = PlaceLocations;
            this.PlaceEef = PlaceEef;
            this.SupportSurfaceName = SupportSurfaceName;
            this.AllowGripperSupportCollision = AllowGripperSupportCollision;
            this.PathConstraints = PathConstraints;
            this.PlannerId = PlannerId;
            this.AllowedTouchObjects = AllowedTouchObjects;
            this.AllowedPlanningTime = AllowedPlanningTime;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// Constructor with buffer.
        internal PlaceGoal(ref Buffer b)
        {
            GroupName = b.DeserializeString();
            AttachedObjectName = b.DeserializeString();
            PlaceLocations = b.DeserializeArray<PlaceLocation>();
            for (int i = 0; i < PlaceLocations.Length; i++)
            {
                PlaceLocations[i] = new PlaceLocation(ref b);
            }
            PlaceEef = b.Deserialize<bool>();
            SupportSurfaceName = b.DeserializeString();
            AllowGripperSupportCollision = b.Deserialize<bool>();
            PathConstraints = new Constraints(ref b);
            PlannerId = b.DeserializeString();
            AllowedTouchObjects = b.DeserializeStringArray();
            AllowedPlanningTime = b.Deserialize<double>();
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceGoal(ref b);
        
        PlaceGoal IDeserializable<PlaceGoal>.RosDeserialize(ref Buffer b) => new PlaceGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(GroupName);
            b.Serialize(AttachedObjectName);
            b.SerializeArray(PlaceLocations);
            b.Serialize(PlaceEef);
            b.Serialize(SupportSurfaceName);
            b.Serialize(AllowGripperSupportCollision);
            PathConstraints.RosSerialize(ref b);
            b.Serialize(PlannerId);
            b.SerializeArray(AllowedTouchObjects);
            b.Serialize(AllowedPlanningTime);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (AttachedObjectName is null) throw new System.NullReferenceException(nameof(AttachedObjectName));
            if (PlaceLocations is null) throw new System.NullReferenceException(nameof(PlaceLocations));
            for (int i = 0; i < PlaceLocations.Length; i++)
            {
                if (PlaceLocations[i] is null) throw new System.NullReferenceException($"{nameof(PlaceLocations)}[{i}]");
                PlaceLocations[i].RosValidate();
            }
            if (SupportSurfaceName is null) throw new System.NullReferenceException(nameof(SupportSurfaceName));
            if (PathConstraints is null) throw new System.NullReferenceException(nameof(PathConstraints));
            PathConstraints.RosValidate();
            if (PlannerId is null) throw new System.NullReferenceException(nameof(PlannerId));
            if (AllowedTouchObjects is null) throw new System.NullReferenceException(nameof(AllowedTouchObjects));
            for (int i = 0; i < AllowedTouchObjects.Length; i++)
            {
                if (AllowedTouchObjects[i] is null) throw new System.NullReferenceException($"{nameof(AllowedTouchObjects)}[{i}]");
            }
            if (PlanningOptions is null) throw new System.NullReferenceException(nameof(PlanningOptions));
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 34;
                size += BuiltIns.GetStringSize(GroupName);
                size += BuiltIns.GetStringSize(AttachedObjectName);
                size += BuiltIns.GetArraySize(PlaceLocations);
                size += BuiltIns.GetStringSize(SupportSurfaceName);
                size += PathConstraints.RosMessageLength;
                size += BuiltIns.GetStringSize(PlannerId);
                size += BuiltIns.GetArraySize(AllowedTouchObjects);
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e3f3e956e536ccd313fd8f23023f0a94";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1c+3Mbx5H+HX/Flll1JGOKkiUn5aOjVMkibctlPSIqfpYKtQAG4IaLXXh3QQq+uv/9" +
                "vq97emZ2AUr25cjcVS5JRcTuPLp7+t09OxqN2q4pqkW2aOr1alzlSzcKz/Kuy6cXbjauJ393087evirz" +
                "qfu2nuZdUVc/v81W/D0u/YMWI0aTui79c+fmo7hku16t6qYbt+tmzrd+ST8jL8v6erxoitXKNWMbO63L" +
                "smixNIY9xQZdkxdV12arvLvAy/Ag2QZbVxWWKGaj+BSgygZAqKvX0wuPFufNyzrv/vRpeC/zMWXcFYay" +
                "/H65EhSz8L7WB6PR4//h/4yen391ki3rK1d042W7aO/3yD7ay55kJF42r5usuyhanGDerowARHwve3Ph" +
                "MlDGNVWO46jbbt24rJ5jvMsu8mrmJzs/d49jCsWQb90c70HoHLPWrZuNQGhSrG42CtI3NVZ/Ex7KFqQd" +
                "DtbvZlDYwra7q2b3sLzM60ORNa4Eilcu6+osx6+5a1w1BaIN0cV6B91F3mXAOC+v802btSs3LeaFA8Bl" +
                "664vMP4oq2oMqZQyS9e2+cIdjhauXrrOoH9Vt+68y5crzAxAB4jz1aqpwf44AyH4V8qVwLZqCSBwWTWY" +
                "IhNtsE1usIsDkO+ZG0nlB3NuDgIJR+G8wPMdyVVPWohh6VoggyWveXSgTuuWedUVU2AJ+i111XxSrztZ" +
                "Z6ajp1hxQlKC393s/mrd8h+y1UzJ47JpvW5aYQuhP7jng/JyO8z+Ae4afe3ymWuyC/knwvh3DhNF0o4G" +
                "U17xJzUU/701uNtupgArgCA/uKqa5c0MnNfls7zLhcUvigV4817prlyJScp48rbbrFx7LLwDdqUsO2gv" +
                "EH8jckdJmNbL5boqIP04TSil3nzMxGHm0IgNOGJd5g3G182sqDhcBIer43+t+2Ut0vTs9ARjqtZN1xQ2" +
                "7FRUU7BhS+3x7DQbrUGyRw85YbT35rq+Rz2yAPnD5spgANa9gyC0hDNvT7DHHxS5Y6wN4jjsMmuzA3k2" +
                "xs/2MMMmAMGtaojXASB/tekuauXGq7wp8kkJvdWCecsSq+5z0v5hsjLBPoH6q2pbXleMe/yWZauwLnG6" +
                "R4VYqo1agIAYCJm+KmYYOtmopJSFqzoI5qTJwY+cpVuO9r4U5STaRk6E2qlt62mBA5hl10V3YZpZTkMM" +
                "0z9FikQkgPIZVVscqyISVCkOFSDjtINB+PkoA5cAoQ5v8SOfTl0JHpWXb99ixbo/Wo3H26Dck73AztBJ" +
                "7h15z83ImU/KMlHjV3m5dmp2vI5qSV0wNCDKqQedSr1YQAhMlQ2wPAbqwaqLAvCAJc8iOsnDHlrJc8Vm" +
                "NFvrK+GY8bypl2MwAF7c0mHeaLDE/vO38FZiJYdyr6bAZHagQ2UBNXp3Bb8A3jhqDMiSEhOmh0DwHOeN" +
                "w2mvYBaPqPL4eObfq/9AdOqmsLnHGZAgJ9iA0V/XwL2pZN047q4QVOESNQ7F1MEvVXZN/R/oaQG5h25w" +
                "Qd+Fvzbhr1/vBvxIOsMhHBQdwpSefeD565dId/ojx6MPYGR/Xd+F77ztgAHDmZsXFfUMdVNwzKIzKlOO" +
                "1AIDwVUxvVyvRM3Rbcu6vL1ssQwnuHcQLlgX/l0W806iJxBMHCacOQ+9E/uDAeYscpBoRnkx9wvT/6JJ" +
                "wotZ0WB64jUncA582e9E7z0ydzbMDEu5Fo9mPUxncDBz6Aw9Cth6P2gcXvjJS2Bvz9TqL9fwTaHD6UHA" +
                "QnLlOX0HIjJxQIXjKOvq0tNJuHIQ3q5zyxV0ftgSSyfb3Q4jvI9Suxg980P+IdVqa1zJv3eK2k6cFI6+" +
                "2EJ57mXPxIerK3iAS4eIgtY5zMTEwErHjGwaOdyjrOiyWQ3ZQZiFNZb5JQMSOFQStK1WWKwvVXiMKQfu" +
                "eHF8lCFEq3SUyAmhEIcXwUxTLArPpTGqkUDQI3eUdfOHOBk4DAKzbqbM1tSqnA6Ps2fzbFOvs2sihD8a" +
                "72eL42Fwidh1dS0ibvy6rc8tfATt2g6H+0HNdvvqLMmC2GEbkMHq5CGA1CA05CxEaP1s872SR0G2W9Cy" +
                "FX+MqinPFjWCUqVgIvbw1ejLeudW0znijEUYQ4zWy9W88haxN87MZH/oy2h3eqMTe9Sf8B3SRZOiLLpN" +
                "b/xVeNwffvsHNqAIqB5+7HAP1LdVUlMHSWw3dQ0PNkOEX82M3jH2NV0tr/1c2hjoXVivNvs5bHEPb+ni" +
                "QqWNJ/CBr4/i9h8n75BLuHJvA18HB8seDEbueC6rE7AnyFog+BWziLRfzPmom+Spkh3MHPQJtEpIARXw" +
                "ABtvd+qslpAkZd/saQkPESvV2a+uqUWDtRlyJW2Y2h1GV0OAuItU3RZv3yimapzhiPTcQ38cEVUonb59" +
                "sURTPUGiC2Hr5RY1xXBhg9qYhaMCr3Ay/JLWgeyeCWUVsWyHMWrLmwWGqMNaJ+OQhmJ8FpQKJux2SfwS" +
                "yK2RVXVLA+CqLtcWNu+CPBt9QXbG+t/pyDgIWbOF92/+d3HXHbBXnyigwKl4srTySiYc6KNTJQ0PGEzV" +
                "1lDTZt1gZ1dNsQSPXVlWUSw4vNsuOZXae8jZAWLuPEP2CaZ11F7kK6dwnHPRV7YS1XdYNcn70sNG2vKi" +
                "YMKBj+LW5CCpG8AR2w4V0wUlNSurPoMynM1UPgBiXO2IwnWBVX3OM+YT6D/M5J+gIckrHs+DfIHjRkaj" +
                "3YHicywJOHTl9yDlt/4gQhxnuNwOo9x4PpFPJvW7I5BHE+XTDeQZGoXxNt5ZMoarKD7KBEI+STwD3aKJ" +
                "VMRipDkMsfgDeSMG6MER/gulxSziZ9kXL394/In/+/zV12evzx4/9D+f/vjtsxenZ68fP7IHL1+cPf7U" +
                "SM3MqEU/ApMfxecjGzSDDw6HhGWL3tCYvokjbA5lmeCnE5JhJ5ljgky0AX1IixU5luR6Z5mp/ThnH8g3" +
                "+YY7fOnVJxAXUI/k1w9H2Y/gM5DnpxRmEln0qqsWCDg8RNO6gQ++QnaTESEyt3C4An4g+nGk7fiHxw+S" +
                "Xz8GWvPXTyB1CpLS30MlLjSPXXItFT1+KE1GvAonfPaFVxKwNfmsWBME72ooBxkcuu749ZPTZ387Bzzp" +
                "nnbIsiYPWDPeShVlHVoVyc6qU0dOKmtBnGN+yvJ3BRRyDEF6646/Pnv21ddvsgOu7X8cRpyYnJynFI84" +
                "XYjSDjT3spAdUBYOdT86a7aPYuf30R/JPjftwtDEaKfHl2vBZfee8Bo0trRXLMz1lWYik3THi0aS/hKh" +
                "ISJdRR4SmkphD4dEfl+vjpSy2ceeqCakA2IGlhogD+ZKJHVrcCQMB966iqN+NsWWeFBUs2RAC4Fg8/Jq" +
                "Acv9eSLBPs0s3oaYklD8hGxfwd0upkh1vx1xjzd+AeiSsJa53HA51giNbMa2QRVodkSWkoTWSXdEKkNj" +
                "B8kMrf02AqU1oJ8fKZzu3RiEu01oUydnZ9D3e9zofr7yw660pcjSWV6OEi87ehXiFMWs5+CAb0pD/4Nu" +
                "e6gOU3TvyYFlrmmoZy3sSgrisa9hAgcQxZbxuzEnjsPg7RGbD474dTjiX9EH35Vi8Ieb4KvGdL6WxMmS" +
                "jxjCxzyEpraQAJ2yq8HCrcOtam4o4Qr3y3imx9qeNcnDwhvV/NcXrKrQHrAciYwCYz5m2AJjM2dXN1z4" +
                "uQdOHOMIH3fjUqiAYuhaGDJKhKUApUehWSPVLfFDnE4wuPoLHPpJUhgx+sgCL16+weLABzyAfHCxXC+Z" +
                "Jl+Cy/inpYdbmLru2iF1GCEPrQ5GOjZ+NLosfBdbNYlk1WJ65w0yUkp56apw14ljo1TRCtsgkJfo4oCb" +
                "Iok/QQpxAx1br8vZIdeVWgEZv0W/RLZaN+LqHwex73kBcoxiLHQFLmA8wswayQjxjokVDaR1kTQWSRf8" +
                "3AqkekJkWe9nDUdiLxwQqt7Qo5hz7coynJBGUEYlSXZrZiKyZFRhR2kJ5D09NiERoE02X6J6mpDbOLOr" +
                "r9E50fbONICsTJ8PWSzhLPEhpcIC7pHi+1paGtAss1Ef+ljcUg+uT0brmE91wBGDIG1uqESZiTFXiJKN" +
                "NQEhebZstQGNCillCyDiWtix+jYlDHwkfA3v9Xik3RUEeCyb9s4T+2po2ZdE5d3IIrqdHRDie79hTN8P" +
                "jBcWiCGrJ7uoBu3zOw7lAPQuIDSepePIE2VdX/qCAFPvXe+QqLNA41gvMGLIYmK3VB/CWcJsqg3oDJzA" +
                "ZN2FkB6WbVvm6MByeyBVlxKBSpdMuwYdb2Y3hTuw2xkLUOgHWS8uIj/RldiSOOW3AYuZTMARAh8tgQLW" +
                "nLhpvo5StcNzkF18HYlmWRRQqmOU3gBSth6UEzFWNJNf6RKstETfGA4copMLtwrd9VCntAO7QPfkQ88d" +
                "PVIE8VIFoF3lDg98IkSjB9lP/Qrq/l6cIc6uvPKa2OtLqFwQxDBLdhfsTUHHaqavRc13KgBjahUhSSGn" +
                "lSxfj1TL4D22CLPKNuTRNewtlDdHAmUQSikdAZkH3uJe1x4ejaxAelZs0XkKLnTtoa3IIK90HRulzNj5" +
                "nXcv/6q4/1B3SFcHXCscg6pQrJ0oCyO1YsyOLo+sN4PJwWzpAV0nWpHcr+kVeI09tQ8Uj9lA+eBI4NM6" +
                "2QPuhDShsX16/olphnYwQwSQxhw31lgiyLnNEivYImAFkizO1YMZIvElA9o+ihaabNt5Mw5bPGTMRY/U" +
                "dzzW6PxFl6IPvIfGJfUFvGFTtkuYjrgwJ2j0a8RGBqp5p2NAnqxAllUdjAd9Usn0lFYpuKmyRY2UvCEt" +
                "fsPyv5yLZeWD8/ND9jh7iKwS/vnkCFmSx5kF4udnL85fvkb2Z/AgJof8gx9CKs4rTDmnXgPBv5xzP2j3" +
                "jnlGNHJIL6aWP8N5hCpIi2QWOMWitMPQOH4uL0LbuIwDkedzSWnPlZbzMl94YRROFevoMwzasoJW4XUj" +
                "3dm+fU5q2Fw28KrkDFuVseCf+8YLP4kslYUe/WrMLODvgEP6sQzjf8M8vyrbzltrGeZEcRd8Qjg7IIE9" +
                "j6EFtKCzDRe9dW7pRYGAgoNooNOeZq55lUOXEFVrcNYe8quiqSvk2zpFhvuNdb8UnSDUmuC5eg8yERVV" +
                "wDcgowVxU1vVejkBM9BBVjK3n9vuiUYp3Rzn0CIzbsmKfF128b1PugrVMV18htkGcX8xRa0LLDcvFmyo" +
                "V8cxQXVsu3qceWqin+bpKDEn/iBTmoiuzulhpZp4Si/DkFdsedtiu/wfuVCcR0s7atbNT8Iu2vrMY66S" +
                "/VGxYSaXPCGp+IZmL88qaDph6co5dN+KkQrUO36g1mM3ZjAaoeEppS9pclXkuwhqROip7Dafu3EQFnQL" +
                "tFKz9PgJcPD94HkyVygtEBJqoD2UiISJ0ieTuHdWQ7H4QhYiLMsVO2H10kUQUYgye0uDfRexdBWJTYmR" +
                "vO58XQkb5+JvqRjAW8K6odCxxaX6XhjeWCfzbKWvehwl5moJQmsilT3PDCNyYlYFU71rTdaVfX92VOJ+" +
                "hxlMxOYuVbkoYKAjd2ZSmRdV3O9fIVnhxWnghDPo3Og1/0ZkAdMsj8f62NPHFk3jbmCorK42gecHk+33" +
                "0TVkkKX2kghI/BIZD45alfQIJO8yzw52hRKxBi+1+0FIJD2P1KY+LkLSeV68Q5efNqKHVisetqBtYh+u" +
                "XYGFAPW70RN98dSeP5fH4Y5IGD/24ynMWE8izxXRqxbt6Fv8eqU/AInkNv273vh2mjPDztHn/NPGynPx" +
                "SXzM6nssEbRHeMMjaToUDxhOyzKX5s0UrZXERVLVZIxUl+Knx759JfDSy59U95IWeXk1eimbgSh1w5Yk" +
                "vSmnS1mBoLdl3yX4vm7gsl/z/yUnIpZXnUCeKJIT4jT1GclyWHDoEReJ4mBSRNyTIYO04AXveSmni4pA" +
                "+ladj9sXwCg5Nybvt24wJYIkcgY9eozYLu2UkiwaAwhpgdL7auSQduTd2Nh3pWKrrVJBbOVNqNVslAmW" +
                "sBgFA+/Tl19KvBaz/aw79pZ+zrEYl2wh08ezej4ebBaYdYtHkUq0d3ZYwv++1UZocDiy+UH0lOl4X8Gu" +
                "aEbpC7e07oSd4p0lZiZ4jcgcd73zZJdqxOMyVCf1jAJ0YPBgoKSD4FqXDrnFHYO1hpt71jo5QXncnZwk" +
                "atm3Ha9XMMXiinYKfP8G4F1w/24GTCiVBxEQ9ruoS+Q7rUEVSY1pU/hsi3CQIu4beRB8/bJW2Wlw5KCP" +
                "CgB7eb01CpOkhUGbCQ8axywPn6PNvCmQwUIi9zDN8Uw2zNBnf+iJmVk1WyVnfSxbHh7Fof4ezWZ76H1W" +
                "87Plfbj7FM84RW/TxAAYPbyUMR/C+AVeSBXgxSEcS6bvIi502KoCVCC3IebsVEJRoqA+EHOqhND6Xl9j" +
                "qwfT693RVYVqTIODieiSNeJ6shdmOIahAQgD4bFj1L4YESObatlRubnEPRWNoQJsUdwQVZZH5SN2BtaL" +
                "O0g2kKgz896nsAAjLdzEVbpzVZil9CyT/Hgpc2AXH5UeZ9/TUWYvtvZGexUqWFQ1Azo9n8ENNjF4R9JX" +
                "Lclg57uBwnCxj/T9Np4bST3FZngX15JV1lGkdGqLX9mUgu4i59dJpEbsOPtVkE0nmoEHApgJccQjM6C1" +
                "l2bKsEpPEAWZYQ04XCcV/tm+ObZ9cWyzfT3sDhTKttkBVq+37lOpDiD3+MMVWgQWm7lF47SdiNcBZjXO" +
                "VXLP9OVMXWuW018Zj/spIw/rBGB3a1yW4mW7aREGJF1Mwpr+UgDNvc5BGi7KvziAtd/9EkpJlBRrVMzP" +
                "9ao2CC3hRsFUeGUBi4Vt//b69EvRaY9owA/Q5bbB//JrS9kh/Y/MiLz06fz07moKnRJSHVlthRJw+++x" +
                "qo6w1SDQ0igBZZRPpdzSg+H/1djdqrFr3tu5+M1qzIb/X1JjN2mx9FL8DfGgtDKF4G8w6Bp2iQP47+Dd" +
                "90ImvFR63c3VpgC1UXJQEopqJRQPUNgZ3thqB/efRuGaVnqzLmndsRtFd4SkUNsjaJqpjQ5W/87lpKkv" +
                "ta7Du1VIMEJhQiFSV6HWIDV6Shu4xJD0Q+JvP+5usFO+2XF+2qMxuPzbOgAvMksEJVf8m1CUxeJPdZfv" +
                "Is69IUbzem3wNJQJQ5QjQp5raiZeABJdw4BwVzPY9i1j34lozdtsUpFWdatTbMHm7wklISiHyYZ7SHb6" +
                "FLkcl6TvuWSFSo5MP0bHgZUOmbixwHNv13qhtCWOekAjEEI3mI2GhNJFLdHmgyAJ9L1FTkNFC6oZuiep" +
                "e/l6CeuKm5AWvhcAUzAk199Ai27S7HacEDWrfgpFUw2aErabWQqVBBByQ1gCml7PjH2CJ1TP/AVhuXLJ" +
                "em5EhWVavS3pK/fhazks3EueVbPf4ZrxYWjzkT1QBKWNQrcCd0NJTr/t4VNSfmMeH9a0/MaHvu8zc4Ru" +
                "+G0frcuZlR4cBlI38+yyqq+rf0Idb1sWn3hT6a91kzQh8WF+r17P2NkjCo63Xg8l4IFwj10Rf469nyFf" +
                "s/URJjtn3mYQpmC0b317Qp0gPT6DyDhyod2svh9Wnr/hCnr5wZoNLaNkl3so+dsej8msTiuEF30z+c72" +
                "lCTxZQQ4v/EO0W+9FPRb7/iwVbm/av8ezgev1uxldkdK4gTmmhxbdb02C9hp85El0UOTT7iDoH1heJhk" +
                "57lF744QE7qCiOx0A5J8F6F7MvMdblEBbNeRpfCVDAKPXhX1uoWv6NA/APisviRFFWnRmGzg6zw5PeUF" +
                "DMaF0v+XLhKabpLqKf7uUO/HugfImDcb3jhdWGq0gzC3A54oZoe60+uz5y+/O2OnP3BasbVForzw5QON" +
                "C71iFaC9C/0hXEPlWiYZnjiFiOSrV2cvTnm5RZRw3HP3drLLkdYWhfPtlhjxlz4dOzdz9e0etJQexY1n" +
                "GMkuDzQ+87Z8LX1UfW3qW5IURKHNIwL4Eo0/4S471mR/EpxVG1jb69tSih9WKqO93/2f7OUX35w9fcOv" +
                "Rf3+yf4/JM7T99cB7AoYP6JFg+cVGdSYNLDBzYdXIKkMeow4Qm2fX2hqOYRX/i4jCqN7A6fi0oXcZbrD" +
                "iTzR+THPKJ9QEHaBxkJD0sSUPVYJfSKTFBRvYCW/8M35yxf3We71SYcfnzz/NtMFkGsMLAw1GwQg+bYC" +
                "FbVRZXg9zAzKcXYmXgMzlFuHLnIUGjnL4tKdZB/9xz4pvH+y/5SezekX+0fZflPXHZ5cdN3q5P593mgs" +
                "Qe1u/z8/UhS1bA5PUDIeldUt5fS8dyOfFopU0Ntk+5hUaDfQpXP+E2LzEqKqHYPW/LeDX5nwVyLatyJO" +
                "v1DeCB+6o9z7nTVXQOZag05UcZo5kg+UMZPkkZWUtyxzkgUCyDOSAM+GJDj5479/9qmOoOnVJimM24Z4" +
                "3+90/tdvkeKHi8A8fzin3sbnv5Rf2whdW7bK9q8X7aM/6RNWVU6yP3766KH8xOiGA+A+19d+BMw+Komz" +
                "wWN6KETENrACkb5FDXpd8r00cXT1at8YGqx9+5eSxHbuzGBqhg8qW21wtNHBkOfvso/pon+cTX/F/82k" +
                "+006SE4e43Dc/OcH/C7ZJPz8hD+n4edD/pyFn4/exi+GffpWnt1xbmPwTZqYAUjTpmLAtz5Fo17acfi4" +
                "3J65FFsjpxcFeCAOHBYlYiVPvFr/6TuM+nOeXTRu/vgjLxLXxWVx3NTtcd0s7nfzj/7Szf98P/8L2HB6" +
                "iYUkpXeOgJ6B+6yeIk9sp0sFIZ0/icLfymV5LuyDm2n0Ehre7Z4jB+nTUaBmpNmdhP87myO8OrNriKAA" +
                "/Ivw0TrtuJJxIbyUIT5xZ0EHdPxVMWNkz7dFnHxjqwYyhJASXu9uN0t114/Uz+9/SS/dbYjBGd8FiPSy" +
                "5I4L+MMGB8mH48xdOceWhLH1F8cHnVlKDO3PCmWuSCG5NOOdwm08rUURthkEiT1qCGxAZ6XwQYWe2Er6" +
                "da3SSIOqNj58/PQGyGPvXrgWtL17kiqwDnnsEz5xS1QAhJS3BYzEg7ZPaYiPCezEAUYx+yLDBEmzPJTg" +
                "rAetDNdKuKeiv9sDPUGSCki+l0wTSlX25MVp6l8GRvMLjFMWYHV865Wd/N3LkHAg8/j9NoF4DghNpqgT" +
                "yVloY9zMcLCfdwB20tI02ku+yewTabuucfqWp/i1rDTzFr64Y31Rd4OC9Fn9VgTYh/VBBHyz1u2Dn/Rg" +
                "9RMuMBFERWv9UEYqgMzxcehWmsYE3sRdxviP+8rqr7/64ol/cdsfEw77hW/lNeGvRfhrEv7K77ybUnrX" +
                "tG+u39Q0TOJCHHe2J71JOvNEc6ZfrIsJl7g+neeRn+FPXn98D2UnHzP1L28tjn7P3pJdfHQqbYXsjIXT" +
                "pclXWBZp7MZ4FDh2JxbTy5nYhIWQXWk539HiP4W+nWnyl1lC367WA/yC/CbuLgT+GUT7bxNLWtqkTYp3" +
                "TWDz/dQDqamygnS/nk7XKxjZQxoMaWmVJ7iGgjhfSXFwPOnuH9MZKErrCtOFJCFRIpJK3UtNUDCnrdP7" +
                "muM1ryVqyzZbnpfY19+u5jVBNHHbtAr9vuGjwnqbUaZxEY8GoroCmvXXEAbpVP1EobYXyMdFjqUXXwJe" +
                "OgvXDS6r+bEte7k/oxmntIz+C5TA8GDjYQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
