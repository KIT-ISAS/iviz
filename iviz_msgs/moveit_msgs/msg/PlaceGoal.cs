/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceGoal")]
    public sealed class PlaceGoal : IDeserializable<PlaceGoal>, IGoal<PlaceActionGoal>
    {
        // An action for placing an object
        // which group to be used to plan for grasping
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // the name of the attached object to place
        [DataMember (Name = "attached_object_name")] public string AttachedObjectName { get; set; }
        // a list of possible transformations for placing the object
        [DataMember (Name = "place_locations")] public PlaceLocation[] PlaceLocations { get; set; }
        // if the user prefers setting the eef pose (same as in pick) rather than 
        // the location of the object, this flag should be set to true
        [DataMember (Name = "place_eef")] public bool PlaceEef { get; set; }
        // the name that the support surface (e.g. table) has in the collision world
        // can be left empty if no name is available
        [DataMember (Name = "support_surface_name")] public string SupportSurfaceName { get; set; }
        // whether collisions between the gripper and the support surface should be acceptable
        // during move from pre-place to place and during retreat. Collisions when moving to the
        // pre-place location are still not allowed even if this is set to true.
        [DataMember (Name = "allow_gripper_support_collision")] public bool AllowGripperSupportCollision { get; set; }
        // Optional constraints to be imposed on every point in the motion plan
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints { get; set; }
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId { get; set; }
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of placing;
        // CAREFUL: If the object name 'all' is used, collisions with all objects are disabled during the approach & retreat.
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects { get; set; }
        // The maximum amount of time the motion planner is allowed to plan for
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime { get; set; }
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
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
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public PlaceGoal(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceGoal(ref b);
        }
        
        PlaceGoal IDeserializable<PlaceGoal>.RosDeserialize(ref Buffer b)
        {
            return new PlaceGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(GroupName);
            b.Serialize(AttachedObjectName);
            b.SerializeArray(PlaceLocations, 0);
            b.Serialize(PlaceEef);
            b.Serialize(SupportSurfaceName);
            b.Serialize(AllowGripperSupportCollision);
            PathConstraints.RosSerialize(ref b);
            b.Serialize(PlannerId);
            b.SerializeArray(AllowedTouchObjects, 0);
            b.Serialize(AllowedPlanningTime);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += BuiltIns.UTF8.GetByteCount(AttachedObjectName);
                foreach (var i in PlaceLocations)
                {
                    size += i.RosMessageLength;
                }
                size += BuiltIns.UTF8.GetByteCount(SupportSurfaceName);
                size += PathConstraints.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(PlannerId);
                size += 4 * AllowedTouchObjects.Length;
                foreach (string s in AllowedTouchObjects)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e3f3e956e536ccd313fd8f23023f0a94";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08/W8bx5W/E9D/sIiAk9TQtGO5RU6pC9iWkjiIP2q5SRzDIIbcIbnVcofZ2RVFH+5/" +
                "v/c5M7ukbOeuZu/Qa4ta3J15M+/N+35vdjAY+KYuqnk2r127GldmaQfhmWkaM13YfOwmf7fTRt++LM3U" +
                "/uimpilc9fZdtsLf41IeeBgxmDhXynNrZ4MI0rerlaubsW/rGb4VkDLDlKVbj+d1sVrZeqxjp64sCw+g" +
                "YdgTWKCpTVE1PluZZgEvw4NkGVi6qgBEkQ/iU9gqLQAINa6dLgQtnDcrnWn+9CC8p/kwZdwUijL9frEi" +
                "FLPw3q0E54f/4P8Mnl1+d5Yt3bUtmvHSz/3dDtkHh9mjDImXzVydNYvCwwkavzpQChT5weAARr1e2AyI" +
                "Y+vKwIk437S1zdwMpthsYapc5ludfoiDCsYSX9sZDABiG5jWegtQgdpINldveF8/OID/OjykRcZ89rJe" +
                "3InC1h3YKr8DK9DM7k6y2paA6bXNGpcZ+DWzta2mgG+NWCPA42ZhmgwwN+XabHzmV3ZazAoLmy69XS9g" +
                "wjCrHAypmEJL672Z25ODwdy6pW0Ug5fO28vGLFcwNWw82bVZrWoHkgDHgZs/GHzHHApIV76k88hWtRWk" +
                "dXScX8NSFrb6oemRaDKapxsgFbEYnB4IQYOEcxMPcllanxEB1niQQCdvl6ZqiimgC5RcMlwzcW3DgHIe" +
                "PgWQE6Rqi6J9d9V6/AcZLWdC2Wzq2toTl9BRADsdfESEDj6XAHyE2QbfW5PbOlvQP3GTf8dhpFz8oDfl" +
                "Jf5EreVYZ3yeffsm5w3zBuEAgMGq3NQ5cGFjctMY4vdFMQc+vVPaa1vCJOZBettsVtaPiIOAdVG+LWg0" +
                "oP6G5BDFYuqWy7YqQCPAeYKi6syHmXCcBrRkDUzRlqaG8a7OiwqHkxQhdPift7+1JFpPz88y1Kd22qLk" +
                "wUpFNQVe9KhQnp5ngxZIdnofJwwOX6/dHVQscyB/WDxTkbQ3IBAe92n8GazxB0ZuBLCBOBZWyX12TM/G" +
                "8NOfZLAIbMGuHMjZMez85aZZOObHa1MXZlJaBDwFCgDUI5x0dJJArgh0ZSqn4BliXONTwFYBLuJ0BzVk" +
                "yXZrDgSEgSDb10UOQycblpWysFUDsjmpDfAjzuIlB4ffkqYizUMngprKezct4ADybF00C1XWdBpkrD4P" +
                "N35EikgkAOUL1HFNqsnheVCrcKiwZTjtYCDeDjPgEkCogbfww0yntgQepZfv3gFE1x3NxuRd0PTJWsDO" +
                "oJXsDfKezZEzH5VlotKvTdlaNkOipTxSFxgadmQ8PSGpJ6vocVAPyxGgHiw9KQDZWPIsopM87KCVPGds" +
                "BnnLr4hjxrPaLcfAAPDiMx3mrbaLfAL8TbyVmMy+3LMxUJnt6VACgPZvf/unjdcWNQbIklEHATeB5zir" +
                "LZz2CmzjEFUePs7lPTsTiI6rC507ygbE0WHA4K+tQQ+I4MZx+0KQhYvUOCimBnxVZtfUGTIibB10g1t6" +
                "E/7ahL/e72f7kXSKQzgoT95JpGd38/jrt0h3dElGg49gpH+t9+FPb3thgGFuZ0WFegZ1U/DOomdKU4Zs" +
                "gQHBVTG9alek5tB3yxrjrzy6WzjD3oB0leyhl8WsoZAKKEYuExw6nnpDBggGqNeIg0g10ouZQCYXDOHi" +
                "q7yoAUDiRSdb7bu2P5HuO1XvNkxNoFkPD/MOvjl4mgY0xwGfCJh8GTWOb3T+sojj2fwvW/BTJ5ZcCTCV" +
                "CHyGTgQiNLGAEo6zOJ89fXQXrm2F4aZdrhqMMXRZAJ4suReG71JsF9NnMuR/pGYVxjX9u1fUduLE++iK" +
                "8Ai9w6fkz7kKvMGlhQADLXWYCRMDS40w1qnpfIdZ0WS5AzmC8AtgLM0VxifgXFE0t1oBsK6EwWOYcmxH" +
                "89Ewg9Ct4lEkMrgLcn4htqmLeSG8GoMcihAFuWHWzO7DyYDzQHvmxYDfAEjtWFGdjLKns2zj2myNCMEf" +
                "tfjc5ITovkgCG+dI3AXEDt2uYSXQzjdwuB/Vcp/nqFPVlmRJ9LB1k8ECmRBPclgachpZklIZHYgjljwL" +
                "8u2BmJ6cM1RTJps7CFKZhInog+PGuQjxddE9w5/kncWNhqAtWQmHvRQj2RmplrM/+EU0Rp3xiZHqT/mp" +
                "AMVUlEWz6cy4Do+7E/ZweD3CwAGEHzvcBvZ5meqojyjmm9oaDzmD4L/KA+VjUBy1N42Q6Wh9LKUBfPY2" +
                "rHIH3qL3CxpuPAH3eD2MO/gyeWcmgMO7g8DnOig+6Y3d9YIW4N09ytYWgmOymjOTJIjYjRLqZMe5BR1j" +
                "fcwXFUvMGrI5cpmjkCVl6exJCR5kjS/f29qRVvNZCfIRpjYncXO8i33k97Y4/VbZZbsNnkrHf5RDibiO" +
                "kJIds5PkpNzEYdhaXW2RlCwaLOIC5+CwhHFwPrgv3jYUUuNuCBBZvZMY3Zl6bhtxbF0ybm0pjgsKBybc" +
                "4rcIjDHBGPOacQ/Xrmw1wt61/+xg8Bj5G9b4iYfGUePazoMf9L+L1/aiZbqEARqck+eLngCTCg729FzJ" +
                "g0cNXOYd6HK1gWCNV3WxLJAQkookO9+uOLsih+PEp86ODRqdFqMBQNkvzMryVi4R6ksFheo9gO1kjtEt" +
                "b8A5KDBLgY/i6shOVIBA53E7wExhUm5XAD8FZZnnhToREeAQZW5hvWZLYx4CfY2c/gnqE7lGsD02czj4" +
                "ISYkdiD6DGDCVhj0B1GT1T+OFg5UjD5XKvO2k4pMM3E3Q6ARZ9ynGxDyHKMkkASbaSYHoTBGzA9EQ0pc" +
                "A8JFHUkJwJDwFosWmD+syUTdG8J/wbXCFOTX2eMXvzz8Sv6+fPn9xauLh/fl55M3Pz59fn7x6uGpPnjx" +
                "/OLhg4EQG9OqGjfRnmQUPh/ooByc9spTDaQzNOZ+4gidg6KN208nJMPOMovZNVIO6HRqoNlQfSa3N5rW" +
                "OopzjgD52mxwhW9FpwLitNUh/fplmL0ZUpjxa7pnwxEW6JlqDhGK7GjqanDaV46ojGlfcNACfkD0UaTt" +
                "+JeH95JfbwKt8devQOp0S0x/2RX53HjslKipMEQAHYrhMu8TnPy56AuwQSYvWtyCOCPMQaPOuY5fPTp/" +
                "+rdL2E+6ph4ywcQD5nQ5U4VZB00NpXbZ+UNOKh0hjmN+zcxNAfo5xiwduOPvL55+9/3r7Bhhy4+TiBNm" +
                "NmcpxSNOC1LhgeYiC9kxysIJr4cena7D2Mk6/CNZ57ZVMJZR2vHxGW9vX/MJHghSSl9hla+rPBOZRPe9" +
                "qKliMGKRKVaRh4imVCWEQ0J+b1dDpmz2pRB10JNEoV9gqR7ywFyJpG4NjoTBgZ9dxaGOVsWWeFeoZ5EB" +
                "NWYC82eqORjybxIJlhw1+R9kT0IlFWT72mIdxvq37wa4xmsBALokwBoIc4IH0kIopTO2bSvtZkcoShls" +
                "nrQnUikaO0imaB35uCkuIL095X3amzEQ7nPuNvV4dgaHv8fF7iY7P83N1uxaOlNkKfHAo3tBPlJMm/at" +
                "/u5E9j/Gqw91ZhTiO3R0ma1r1LganyU19ui0mgm4hW1jxzdjnDkOo3cM2Xx8yPutIf+iHvquzIQcc4Ix" +
                "W9dZS5mXJT7CwD+mLzg5lhd+iv0SGpSdbNWGuSB8oPJAEzDD5jv2xQTIG7YF6wUWadBCFOTC4mCHSbrA" +
                "5pj2czVBfibbI4c57hCXQ1iTTQZj21pT4MzMmkaktoe6nfIayXTcB4F/Did/FjBIaEQgnr94DeABpSnl" +
                "rItlu8S8+xK4Df/ULLMH+9esra2Szcf+CaUf9pXUDBc8GgWbBL1sR8WlA3kpqWJ1Xdg1/KvJFyENV+16" +
                "sT9FHse46groYCbgVvmFa8v8BAFT/QEFwLfgVK7amkKAUaIIOu4BnSZZEYaBIJRXMEVXcHI+ychw2M1Q" +
                "upFKCvMbrbzyUSH3ig/WHwnLwUktQKtYnLO2ZRlPimMsJRalzjmlEdkzKrZhWlv5UCdPSB1oK8+3tVsm" +
                "dFc+bdza1LnvnG7YNouA6fNbymXkZFL5BjiJSvstNUwsTbVhJ3tEfqtsWdLbPOYBDxhmzBKGC4Cg38jc" +
                "+z6rc86CcnXZagOEKvIosuR96AFLSxSMPCUuBwcXRnL7Bu55TOv2ThbW5hC0K5zMy5FfRjqJj6rIddFY" +
                "FujZN4AQY1uhPukL7i8kgFxnMN5DHJ2nA5E9SueupNKAOf2mc1ioyoDUsRARSELQVlybRD3ZUvsMKhPQ" +
                "JHASk7YJCQAwfNtSSLU83ADg5cpryTUVABbJeTvv8dYT3ru4Jo3i2vkiMhf6HVtSONyl31RGwGsCnloa" +
                "lp6JnZo2ytkOH4OWkTIVmu5GExeqepjqsE9au1+5hMGksgTUFbDV0oEDjE0OwILIQ8PINVPDZZKtzSsR" +
                "LWgz8GAh6KcyQ+m4HnRPsiccbdCC7H2gYejEJeQc0ytR0qpJK6wFlYpcsj4RQHV3LJxKtWu2UyME/maJ" +
                "opx0WiyTqiebDfHv4q5Z2EE+bY19jfRmyPsMQkrlKcDnntjktZMdcTAG5McC8YriB+tPFCTGhaVtsDFL" +
                "jaEsfQv8l8Xd+7xECh52trLod6NmPRml2kPJzTgj1ELwFTOZnM6WWmBA0cIYASqK3cGi3IoKj6l/896Q" +
                "dsjluHu4lFfl2+WCxHajslArBZsa48AxDVRlhnKvE8lOeoh0AdNywy5SOoc0QImRcBdNAXi4wxlQs7HF" +
                "TMpl6L5Kq6WrKuyO1HJ33+6kDoOYPWbAhPsQG8wpKhVrMqGBdOqa9IiUFTPK2aJ09QhGALoUS/ec6mCX" +
                "UXhG7YX9vgM6IM30Rzfpl+xhdn+YvYF/vhpmv8I/9w40n3Px/PLFq/GvD/tP3jz8qvfkl4f39YloUjqy" +
                "XvfCv2BM0OtDj2FmMZtxRyjXXcPJhBqLn1rMwWuQB9tXWJf0JnS008AxApQc+YwpOivNXESUWJcMqGQr" +
                "6EgOsc+5ralvXBr5qIKOkAP3UgLSs9wFz14aP2QSMhioHr1CUI0xp/i79kLNYYr4v+FEgVzNuV2cOphx" +
                "JvkVkmHOjpHSwnAeIiTy08G999YuRTpwt8BNaMXTLmsEem1AySC+oeUaN2Sr66J21RIMh6CES455yS5S" +
                "Qd45bXT9AZQiPqKib0GJ6/Kq1Kp2OQHWQNeaCe6/Cesn6qa0swZ99HtDTYKYtmzie0nmEvVxPrkX+aYC" +
                "b3Q6rrH3dlbMqeuf3c0E4bEuHDDHEyT9NUvHkdWRQ00p03JDcWg9EsSm6JMoCRhnvBuy3YsQuXKUYE7T" +
                "JeCAWbAOd2XjkVfJDobkRhODUKK/JgNpsgo0ITF5ZW2OdhDgBiKO7rGN2Y0cWJbYh5XSGelyXZhddFVC" +
                "dNW6NzM7DgI0RpwS9hIpBIfRcXMsdWVQsJJzwB2mUu9O4hNqmSZEKAQJ97NctZRWr/JEcmtLza/BHSBR" +
                "tRXS3BMtycdsqynrIfTRRDDAwwLI0SZtsy2PqORqEjFSplzG73oMRqZtCUTnnG3B9ci1KSjOUtu+CyzW" +
                "taWPPNH0skgOhmSzV31POhoQqiQ27qr1rd4apDB4gByBwXk08PQV/rjEv/n5WJ4rnRR0Gs4Dosz/bD3w" +
                "ONHMy2oMhkbFNGISTpFLQ1OAyVYlOhKU2pllx7tCktgOQG0E/fDqtfZ8SYz19l02K25sPubm+dASxkdP" +
                "+KtKCBfIgKdg6zcHg0f85om+eEbPw92WMGGsE0jSy5LD2RUiWc1hpR/h50v+BfuhhKq87E3xU1NamXCJ" +
                "f+tweiH+jITD0iHqh8nGwyN7Ld6oA4dnaaj3NMVvRZEW1VUp7HIluf3x3gFTeyniSQXGpMWfXh0MXtBy" +
                "T3Aytk/x9T+GFXuHOsv2/ImfXQ1BwBr/X5IvZLLZncRDXi9swzo2ZS9Nm0GQAOFWowlxcm/6POMdMiwB" +
                "ESkgNVJ4cV72IJ5Rpm6tI2xdxkoEjCQQ9O0IgsYm6eyivB2GJNSxxffwkFmA9uIOx0Yxlmhu7Uok+gee" +
                "KZWjDTPEEuxLgXH9+YtvKRKMdQesgnahP8PBMDBZheaPczcbb60XeHeLZbNjTfOEQyOJkIYgogQIuwII" +
                "IsksiNcv9BZqlMpw62xfnBVvYWH+Ay9GaRzAt7j0mhA5bIruxOUoUse6IRhIqSdw00tr6l2DD7iybITL" +
                "zs6m4HGcnSW6Wxqo21XO2IIpo+33LjjuRRR2M2NCLBPkgRhx4crch07b3PppXUhWh1iJUZdeIwjnfmtZ" +
                "kGpHl7NYGrApWUxWmEStFdwJeVzbayokUVN+XXiUvelJmkuabPBGWvaHjsyp6VMoJqe86ckwDpXLQZvt" +
                "oXc9Db4LUQPKapzCV4RiZL0CMxjvWQqA51SIeH4yIsQuIi4FdREBFZDhIIptWFYnG1IOdHWaCcFVx64a" +
                "Z3en01XEUIlqmIKvLblxNfms2KPTHzOgK4SYwdNj5H4dEiWdqtlYuo6Fa47kfLra0Be+8cLbqobI+Pgh" +
                "rUBpR0Qdk/5dCtNmqBcdcaUuYxZoKonTJBlPhRYfKpmj7Gf0sLGpnJu8RZ8SFpXDuJDPp3ctj6zgkBrE" +
                "KflspUspDCejiY7iRrgRqcfY9C8baz5MO52YTr54j80yNd15IziJ1JB1xz6ataGUVeCBsM2EOOS26aa5" +
                "x2eKZUI+wdFg0Lt3EO/IEv9sX4fbvg232b7ztgeFsm1/AKtXW5fEWAcg98jhEi0Ci+V2Xltuc8J7DbmD" +
                "c6UkN3p6qrE5lSoX4+N6zMj9uoRpQtc11VD9xkPMkHRXeS65BoeU58xtE+WfPEMnq1+BUiIlhfUxTP51" +
                "ikWG/GJTq7IwxGF/e3X+Lem0UzTlxzfArPA/sz4JlUrMrdNLqRykF3LT3TEh2c0dxlvi3feDQxmh0ECg" +
                "qYEDlJGZUn2ns4f/V2P7VWNrvIC0+GQ1psP/L6mx27RYetP/loCRWqw0OuwPWsOB4gD8t/fuZyITvGR6" +
                "7eeOVti1UrJXeIpqJZQm1m7r6pnvXeQahPtmyaWr9FqsXo3aE5JEbUFQNZOPDlb3IumkdldcOnKkMbCx" +
                "03C9xVRzag9AaQMuUSRlSPwt4/aDHfPNjvPjLpHejWZvYfMks4ggZZw/CUUCFn+yu7yPoPeWSE30Wu9p" +
                "KEWGQIeE3HDuJt5eIl2DceEt3Wnbt6elSVL7yrFRhvrotfLR2wjAkFtOSTSK43jRw6czzbTTmVEpAIFW" +
                "TgLZUZGHCiWmdUIMergLYqickZ0LuARy8Ar5waBPLye71bScREMU/otpTsNGDbIxmk+KAPRxFqpfbkJi" +
                "+U7YHG+FygY16NNNmiKPE5JvvvC3XiQFIUllvWPGOyMzTHeg863WMv3qUCjOyQ1oukiK9eOIDpWF+RKo" +
                "9AuErwNhuwClaDmJHi5Sn4SGI1qkslO0WPWGlqttyZ8v0ZyVLI0HCUBD4uNj3zTKLW5w+3tG6063d+9U" +
                "hljVuKrcuvqn1Aq35fORmM9h7B8LCRH1heUuyS3trEUemk2YkMfESnod/hks/xSzOTs+QqWHjvcviEMw" +
                "D6B9hUSlKFKScETfbM4NuNLCS89fIwiEE4HHrJNeTrJbH1tJtq0dHkWtl29qu7tNJkmQRUpc3noN6tPv" +
                "NX36JaVncl3o9otEH70ZhGD0whfFE5iWsthmLAov4Mk9UZqOD31H4Q4Ft62ZKm2bwkW6F50wFWz5y3DV" +
                "7cjiy3SHj3Lpwov6YbuEPdKClgwC3r0uXOvBr7Q34FUgClzAomINN4xMNuAYPTo/x9aDAwojqVUxhRNa" +
                "gZKKbYaxSo0u67HFzqtGvs5AOdVmuhAIkUmK/EQWe3Xx7MVPF9jYQJitsOGG4sLw/QeOJEUB09a95oU+" +
                "jHEom9MkxRbOI0H15cuL5+fYRCHKOi67e0VaaMiVTBIIvfeGVKAGIj1CjQ/0EjgVOsn3p16Ugi4JIsmA" +
                "wqpTotKVdinZJpHolDf5YmXrcJt/Qk1K6OWGsU7ffzbd+XGlMzj83f/JXjz+4eLJa/x61u+fLP9hAj35" +
                "cEVB77XhZ8XQPoqiAy1HXXYQI3jLkSu6m3CUfBlgzqnpEJvJXU1D/Rx9X+TKhtxnusgZPWEQMcCvlbfw" +
                "1lKV5ZNgFQBMckFikm5IjDKlKH64fPH8LlaZJW/x5tGzHzMGAXF+YGjQxEEiku9MoDZX2nRuvuHCanpG" +
                "2QX5GkW14/RJskLzaVlc2bPsi/84QkIfnR09QZfo/PHRMDuqnWvgyaJpVmd37+J9zRKI3hz95xeCJNfs" +
                "K8dpk0rLo3SK4hXRR5ciHfiq3BFMKrhJ6cpa+bjarATR5e7GUde2dlgXCwdMR/10xvljZpLwFUBUBLI0" +
                "ZxyIzVqgFao+TkB5aqjHjJQgTL8zgnSWBSrwQyQEPOwT4uyP//71AxmChpp7uGDg9raPdLXLv/6Ywfl5" +
                "izWDcF7dxS9/K7/XIQKelsuO1nN/+id5hHWas+yPD07v82+YUOOQAr1lHQOuwhoi6P5zdG4QIV1FC0/y" +
                "eunytsQB1FLSuNVR4HFk989/A4vs7M60KKcN7VAMdjToweqbm+xL9Pa/zKbv4f9ybNUbUD/L2UM4Jjt7" +
                "ew+/4DYJP7/Cn9Pw8z7+zMPP03fx22oP3tGzPSdMel/siWmFNBdLZn7rQz3s3I3CZ/gO1ffYGjldFKW2" +
                "HODAfoowVgjJJ5aPBMKoP5tsUdvZwy9EOtbFVTGqnR+5en63mX3xl2b257vmL8CK0ysARHnCS2spG5C7" +
                "absMpzuTfv7UEGwlyIQNu9vNOAYKbft6qRMH8dPB65i1DomofeQUdvZjiHbTO5dAAfA/wuf9uAOM2zNC" +
                "pEpjOB2Yfv82L66LHNMF+L6I82/vEDnMPEgK3mf3myX7+UMOEbrfHews2EfjAl+GXfH10J1fHui3U1Cu" +
                "HY7elrMhfS6j9C42daTtYkwUbhoLyedIqVGnCWwbXe2kBNNdxy/5rExBDVdM6+PqK3ANuddYK5lobdkH" +
                "CN+MvWX3sbcwXnzaXj/JQGirvwlxNaMD26ASOm8kcbr1yyLkkAKC5DBPHBwVzKAczn0K8Dr7peFSbhda" +
                "ytUl0BpIWNqUtLhxzqrKHj0/Tx3RhO8ExLjDDliE33qnXPBPkCpiRywXdBsS4mlASDO9ki5Z7tnLAxbh" +
                "9x42nrRVDQ6Tr1zbD6UItS0rfmKsk+EL3yXS/qw94UHdXr8DC2wI+zgW0ja2j2vTsRWsm8kpWFtwXW6t" +
                "YfCGW852poBUDagO4GF5SFe5+tV3jx/pm8/9ZeawYPjaYB3+moe/JuEvs/eGT2qi4ya+bk/VVtr47bvs" +
                "lu6o10mrIOnU9Kt/MYcTl0Bf+2AgU4QF+MfPoAXp67Dy8vNF4h9YnNKYp+fU6Yg9vOCcca4XzA51pcP4" +
                "2tp+vXD7/qqjcTuzflKHZj9pR/JKrukoVKlECED8yvAuBBSrfRLtv00s6qmjHi28OgMegUw9poIulq/u" +
                "uum0XYH9PUEzQu229MRU042S4ng0ae6O0FMoSnvCPWkMCNd4Uhq8UBPdUKc3IWV6VCEDinHoAwHIotic" +
                "vTwJF8zxOqRFF5GnVS6Pn2/mi5stlzYPFQ0IAQvQsu9DuMRT+UOP3NtAX1wZLbhIYbjLfV0XjTKOx77z" +
                "r9G6g7wMBv8FRQxn90ljAAA=";
                
    }
}
