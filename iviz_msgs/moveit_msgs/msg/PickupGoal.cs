/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PickupGoal : IDeserializable<PickupGoal>, IGoal<PickupActionGoal>
    {
        // An action for picking up an object
        // The name of the object to pick up (as known in the planning scene)
        [DataMember (Name = "target_name")] public string TargetName;
        // which group should be used to plan for pickup
        [DataMember (Name = "group_name")] public string GroupName;
        // which end-effector to be used for pickup (ideally descending from the group above)
        [DataMember (Name = "end_effector")] public string EndEffector;
        // a list of possible grasps to be used. At least one grasp must be filled in
        [DataMember (Name = "possible_grasps")] public Grasp[] PossibleGrasps;
        // the name that the support surface (e.g. table) has in the collision map
        // can be left empty if no name is available
        [DataMember (Name = "support_surface_name")] public string SupportSurfaceName;
        // whether collisions between the gripper and the support surface should be acceptable
        // during move from pre-grasp to grasp and during lift. Collisions when moving to the
        // pre-grasp location are still not allowed even if this is set to true.
        [DataMember (Name = "allow_gripper_support_collision")] public bool AllowGripperSupportCollision;
        // The names of the links the object to be attached is allowed to touch;
        // If this is left empty, it defaults to the links in the used end-effector
        [DataMember (Name = "attached_object_touch_links")] public string[] AttachedObjectTouchLinks;
        // Optionally notify the pick action that it should approach the object further,
        // as much as possible (this minimizing the distance to the object before the grasp)
        // along the approach direction; Note: this option changes the grasping poses
        // supplied in possible_grasps[] such that they are closer to the object when possible.
        [DataMember (Name = "minimize_object_distance")] public bool MinimizeObjectDistance;
        // Optional constraints to be imposed on every point in the motion plan
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints;
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId;
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping;
        // CAREFUL: If the object name 'all' is used, collisions with all objects are disabled during the approach & lift.
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects;
        // The maximum amount of time the motion planner is allowed to plan for
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// Constructor for empty message.
        public PickupGoal()
        {
            TargetName = "";
            GroupName = "";
            EndEffector = "";
            PossibleGrasps = System.Array.Empty<Grasp>();
            SupportSurfaceName = "";
            AttachedObjectTouchLinks = System.Array.Empty<string>();
            PathConstraints = new Constraints();
            PlannerId = "";
            AllowedTouchObjects = System.Array.Empty<string>();
            PlanningOptions = new PlanningOptions();
        }
        
        /// Constructor with buffer.
        public PickupGoal(ref ReadBuffer b)
        {
            b.DeserializeString(out TargetName);
            b.DeserializeString(out GroupName);
            b.DeserializeString(out EndEffector);
            b.DeserializeArray(out PossibleGrasps);
            for (int i = 0; i < PossibleGrasps.Length; i++)
            {
                PossibleGrasps[i] = new Grasp(ref b);
            }
            b.DeserializeString(out SupportSurfaceName);
            b.Deserialize(out AllowGripperSupportCollision);
            b.DeserializeStringArray(out AttachedObjectTouchLinks);
            b.Deserialize(out MinimizeObjectDistance);
            PathConstraints = new Constraints(ref b);
            b.DeserializeString(out PlannerId);
            b.DeserializeStringArray(out AllowedTouchObjects);
            b.Deserialize(out AllowedPlanningTime);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PickupGoal(ref b);
        
        public PickupGoal RosDeserialize(ref ReadBuffer b) => new PickupGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TargetName);
            b.Serialize(GroupName);
            b.Serialize(EndEffector);
            b.SerializeArray(PossibleGrasps);
            b.Serialize(SupportSurfaceName);
            b.Serialize(AllowGripperSupportCollision);
            b.SerializeArray(AttachedObjectTouchLinks);
            b.Serialize(MinimizeObjectDistance);
            PathConstraints.RosSerialize(ref b);
            b.Serialize(PlannerId);
            b.SerializeArray(AllowedTouchObjects);
            b.Serialize(AllowedPlanningTime);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (TargetName is null) BuiltIns.ThrowNullReference();
            if (GroupName is null) BuiltIns.ThrowNullReference();
            if (EndEffector is null) BuiltIns.ThrowNullReference();
            if (PossibleGrasps is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < PossibleGrasps.Length; i++)
            {
                if (PossibleGrasps[i] is null) BuiltIns.ThrowNullReference(nameof(PossibleGrasps), i);
                PossibleGrasps[i].RosValidate();
            }
            if (SupportSurfaceName is null) BuiltIns.ThrowNullReference();
            if (AttachedObjectTouchLinks is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < AttachedObjectTouchLinks.Length; i++)
            {
                if (AttachedObjectTouchLinks[i] is null) BuiltIns.ThrowNullReference(nameof(AttachedObjectTouchLinks), i);
            }
            if (PathConstraints is null) BuiltIns.ThrowNullReference();
            PathConstraints.RosValidate();
            if (PlannerId is null) BuiltIns.ThrowNullReference();
            if (AllowedTouchObjects is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < AllowedTouchObjects.Length; i++)
            {
                if (AllowedTouchObjects[i] is null) BuiltIns.ThrowNullReference(nameof(AllowedTouchObjects), i);
            }
            if (PlanningOptions is null) BuiltIns.ThrowNullReference();
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 42;
                size += BuiltIns.GetStringSize(TargetName);
                size += BuiltIns.GetStringSize(GroupName);
                size += BuiltIns.GetStringSize(EndEffector);
                size += BuiltIns.GetArraySize(PossibleGrasps);
                size += BuiltIns.GetStringSize(SupportSurfaceName);
                size += BuiltIns.GetArraySize(AttachedObjectTouchLinks);
                size += PathConstraints.RosMessageLength;
                size += BuiltIns.GetStringSize(PlannerId);
                size += BuiltIns.GetArraySize(AllowedTouchObjects);
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "458c6ab3761d73e99b070063f7b74c2a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a3MbN5Lf+StQdtVJTGjasbNbOWW9VY6lJE7Fj7W8eblcLJADkhMNB8xgRhR9df/9" +
                "+glgSMpObk/au9pLUhFnBmigG41+A4PBILRNWS9Ma5uFaye1XblBfLlofLfeeefqYuLmczdrfQNvv2ls" +
                "WL99Z9Y+hHJauckCX4RB6hC69do37SR0zdzOnIIbDKbeV8ZWld9Ap3K9ds1E2858VZWh9HWCA2PYtrWz" +
                "pSsmfvorjD9pfTdbTqqyvqDxBOKqrMtV+d5pq6IMra1nOOZTXwMwW9ZtMGvbLmGc+CKb8bqydQ2zKYtB" +
                "f3ycKwzP4zJ47DevvG3//Hn8Tv2hy6QtCdVX8vxy3QJKwcTvnl8MBo//h/8ZPD//5sSs/KUr28kqLMJ9" +
                "WqfBXfNmWQazciHYhTOAfgvIB2NN4cIMFgHnY/wcXtA6mnZpW7PxXVWYqTNdcAXA2JTtElqsbdOWs66y" +
                "DTLFPWUK03rpbAEUEWlkynpWdQUSd+k30AKg2PW68bCepoTvyAD0w7WzsenPsvAumNq3Ol2Au4X+ZT33" +
                "zcrSjO3Udy1M6Q4PvPawonfMsUW2LKnFyxeAi5P5DMfQ/0dAzV26xiwcLDb8DtAiGzYsCe2lvXSwso2z" +
                "xRZmsJqWNRFhf/hs7MA02m+Dc1g4v3JtgzgApfHNLvlgmHXXOm6NUCeAhwNwvRmOBwDhicH9ZObYEb9R" +
                "c2VkZGBccuzauqa2FRKk7RqnIy9tXUhnZ9aNu7cQPvF1tY3UAwZpZPVht/xK89wyX32H6L6JLxHGJE4Z" +
                "R/pDM9DRs4HhK5AGhMIfmMTBCURWOET03gSUAcvAZEHa+zmAwac7wPaubkns3DkEa0S8amdtB/Jgm0MQ" +
                "xsWe5pOy/mSvKw68XZcz6Qjjx62Xg4HfAGYF6BkSbpXZNPCXAU8d4OISaY/zAYbUJkCjha3HA+VEpuIr" +
                "AH/e2tXaFRnXKflcAGkGu6SAJfZTOy2rst3ibEI3mwFD7rDgyMBjAPDGw1QanK+zQdYdZcCdhffFHdjz" +
                "QOZxFKE87m9AOoCuQ0dJUZQNoIFrCJuktRdOsQWaXSDDR4kDeok0CjBFHSregYk1FaAO0AARnG0PwLdz" +
                "4NooDZcWSedq2qCVQ2Ic83jIK6qehgcHh9WS0WW03cFXXkeGrig3zGYJQzWuAsKRio7y60smdCZeUCCx" +
                "kKwdroaFXdDjaSB0cNV8ZKYgha4FPBImzwXXpqwq5MAIeEzTZqi4HfMpW1qKbs3zAdS6BsSlsYG0hW6X" +
                "xoWuAmEf/B4iRMhLW1YWjAlCAXeMxVWAscbXkhZ0KtgWGWlxnJW9KlfdihUHLBKAmzmcLoACIpSVoIFE" +
                "OP7L4wf4CXYUDj1klnz0EIFMBMCEACB0ZDRSlrD5Ktx7gJmfwl6cVaRHUGs61h3BrWwNinJfGxCcglvP" +
                "ACJuczQtXHF/3QX8gxq8YLmP2rprWADopD9qmtyMXfER8Tv4FnQlrNeS/qQ5/orNyPwLg50ur/CRrEg2" +
                "xW5m3qEteMI8QSA/iLu6sE0Bwqm1hW0tcdyyXIDIuleBdVBBJ5aI9LXdrl0YqxmFso5sBxTXuhNANKy6" +
                "GmQ46m+w/3r9yW7om04z7xswjLD5vAHaIHT4L7jfOgdWq3l2eoL8G9ysa0uY0BZtqUZ27rNTM+iAZMCm" +
                "0GFw983G30NFuwDyx8GZwWCy7gpEYAi0H09gjE8YuTHABuI4GKUI5pjeTeAxDA0MAlNwaw/C9xhm/mrb" +
                "Lj1z46VtStqjABgVFkA9wk5HwwxyTaBrW3sFzxDTGL8HbB3hIk73UK1V7FksgIDQEKT5ZVlA0ynr21lV" +
                "goqGjTltQF4NsBcPObj7NdKYpQ6tCIqbEPysJNWGVpsaT7Qa5APcDDd+ZBfRlgCUz1DxtZmBhe9NWLtZ" +
                "OS9B1oCFD8yaLKa3IwNcAgi18BUeLGjnCu1b/PjuHZp2/dZsXb2L6iIbC9gZZJK7Qt5zBXLmE9AFOnYB" +
                "i1V1ju0ykVEBqQsMjUqTrSfa9WSkopw3O1iOAfXoQLEbyRPL3iV0spc9tLL3jM2g6PgTccxk3vjVBBgA" +
                "PtzQYl5rSZGJjs/sETRu7hra17v7nlWB7tkdGUoA2Bq7rfnTxBuHEgP2klXLWR2ReeNgtdegdEdkDXkU" +
                "K/ydrWxExzel9h2bAXF0bDD4W4davSa4qd1tIcibi8R49ILbHS/BymbroRtN1av4axt/vb+d6SfSKQ5x" +
                "oQKZJome/cnj02+J7miPjAcfwUh/bW4nTLFr3gGGhZuD041RijYz+5J5S11GrIEBQbZAScyhUQiGfLgI" +
                "AAY7uCuLljv9rsp523MaYM1x0VvSP9BA3QS1kdtokyJgtL/Ezkx+gxi42Tx3nKwfSO49Uj8r9oygXIBX" +
                "RQ/TGMFSk1QaZaEtsXfL1Ji1/qoD23RKkZ4AGhIhz9F2QESSpwjdxUkAfXIJvgG4Mm61BpmfrOCyzoa7" +
                "GUb4EKUOMbqRJv+QaFUYlxLRvEXUDuLE8+hv2zFahM/IhqO4DLjSIJZAO8eeGMFTViL/rKHFHaF/rfEz" +
                "9MHRp4X2gbwg4G8A1t9V8Bq6HLvxYjxiP5Fa0T7BWZDBC85MUy5K4dLk1SBMXZSRaecP2XmkOfNgzGyN" +
                "Z+E0HJtnc7P1ndkgQvCjETubDA+dF2271nva4sqv+/I8Ru5AlrewuB+VbDez1Lk4ywLOH4i9qgMpEQAN" +
                "D5ssOq22V/Yq7u0AtAxkj6FosmbhwSllCmbbHmw1tGXFuOUgPBljaY7RR+uFxV+JRuy1UzXZb/oy6Z1e" +
                "60wf9Tv8UIIookhSr/1lfN1vfvMLtkMRoHp8OGAesG3LpEYZRL7dzDUUpQYPvy6U3sn3VVlNn6Uv6hhH" +
                "nn4wb+MQ9+Armrgg0iZTsIE3ozT8p9k3OwUE3kW+jgaWvthpeeA9Qedo8saB80tqcW6zyCibSUIVc1w4" +
                "kCcOJReKjUvYcivM27De8Rzy67GveVqBhUjB7feu8STBgqkwbqhd22EyNWgSt7Dc+7x97TZl5QyGSM88" +
                "lOVIqILQ6esXjfL5qZcI7S41SXEFTIoIs2CryCvYGeyS4FpylXEaBIU02zB5bZS/E4PVZ+02jvyzKFSg" +
                "w2GTRFOABGLCQ+oELn3Vqdt8aOZm8BWyM8D/gVumRpPGLcS++d/FXbfAXn2iAAVOyZJFLc9kggV9dMqk" +
                "wQXGaLoHMa3aDfTsuilXJRJBooqkwbs1x0pkVbxYyObYojrp0LYfDsLSrh3P4xyBvlJIKL4j1Cw7ghY2" +
                "xoZLDDhQOigOjRwETjjGSA4kDXKAlDMgqM9AGBZFGYPaEdoIN9fSBY15pngC2g8F/YkSEnlF8Dy2C1ju" +
                "EQYW9lF8DiBhHgz5A0jJ0B9FCNspLjfDKNeuT+KTqb8aAXnQmgMvewv7uUBHB1jfGQ3GIBTGh5mAyEeB" +
                "Z0C3bBIVARjS3GEeDkOADSmgByP4F4QWRhG/MF+9/OnxZ/L7/NW3Z6/PHj+Ux6c/f//sxenZ68eP9MXL" +
                "F2ePP1dSY2RUvR+ak7TC9wNtVIANXgfK6/WapvBNaqF9cC/j9PMOWbMT4zBARtIAbUj1FTmLUbgrjUwd" +
                "pT5HgHxjKb30tYhPQJymOqKnn0bm5xF5Db/kc7aSV6tcvQCHQ2Y08w3Y4GtPVMbILRhcET8g+jjRdvLT" +
                "4wfZ08+R1vj0C5A6nxLTX2ZFJjQuO8VaarT4QWiix8vzBJt9IUICdI0tyi6klCVz0Li3rpPXT06f/f0c" +
                "5pOPqYtMMHGBOeLNVGHWQa1C0Vk26pCTKk+IY5tfjL0qAyVQxQXpwZ18e/bsm2/fmGOELQ/DhBNnxDOK" +
                "J5yWJLQjzWUvmGPcC0MeD401HYexk3H4IRvnulHQNVHa8fLZmHE9MOZTXBCklH7CzHVfaGZ7Es3xsqGg" +
                "P2fQ2nKdeIhoSplvWCTk9249YsqaT4Wog52dKPSLLLWDPDBXtlP3GifCYMMbF3Eon1WwZRYUitkB5cTZ" +
                "BQKdZ+sFaO4vsx0sYWayNkiVxOoA2NuXDlMpLrx9N8Ax3ggAkCURlprcnJOPPfYVKs3mgGdJQWjudEuk" +
                "UjQOkEzROgppUpwDevuI5+muJkC4m5xtbuQcdPr+iBndj1d+3JTWEFneSxPLycpOVgUZRSnqObgultoP" +
                "Q/+DZnvMDuPWvUcLZlzTUH2PuF0hzTGVkE3BAOxaN7maYMdJbLzfYvvRFu93W/wr2uCHQgyyuBm+rEzn" +
                "HQVOVvgKXfgUh+DQVlGGmTlO7tZwL5sbU7jE/dQew2Ohp01sBLxlyb9ZYlYF9QGVkXBBBkbYImNjzM43" +
                "CPi5TI4M4zQ/HA1BTbcGmnaNxqyZgWMJHdYoNN2Mh8i64zQQ+gtY9JMsMaL0IQAvXr4B4FxEQVWW3QrD" +
                "5FploeFhrJFpN1gmk2YeSx2UdFgl1TBYsF0UaubJssYU4w32SEXppcvSbTLDhqnCGbYdR568i2MclOuV" +
                "wH7iir6hVjIS44cOrMd115CpP47bvmcF0DKSspCawKmLPIKRtZLj6Smwwo40A8l9kRzgl5og5RVClhU7" +
                "a7cljAULtAQZ4rDPxlVVXCH2oJRKFOzmyERiySTCRnkK5APFXzEQwNVfXzd+lZFbObP1G9sUobemccrM" +
                "9HaXxTLOIhuSMixXUlfWUUnDytZbtqHHZJbKdCUYzW0+5wYjQ3xgOUOHBT6ozMMub3MAguJsZr0FGpVF" +
                "3KFkWuiy2mpjt8i95hHxtUejmasrcMITGrS3njAuu5b9nci8m1hkLH14gcC/lwFT+H5HeQGA5LIK2bMK" +
                "s3FMB9gQwDUu8nbIE5X3F5IQwNB721skrqDN8gVKDAK21iqsLnABI4oNkBmOS8fUpQfNtr/n0IDF4QEp" +
                "X11KvKgEqO5DtYY878huZ5ckOXy3WCZ+QlNib8eNDkkx3RNgCAEfrSxtlqmb2S7tqgOWA40ieSRUy61G" +
                "IVTGML21zH0nnQhtSTIJpAtgJSrPxMID4DpknFFilpnlNMbe1IV8DoRWh9WGl5QFqDynax5IIIS9BxqP" +
                "7QqU/T0/g4xd+iSSWORljZmaSjHLRifsVUCnbKbkouYHBYAyNW8hCiHnmSzJR7JmEIstzZn3tqW6QSQ2" +
                "fuHiw7gpte7wgWjcjZf5sGcFpMeM7ZqcAReGClFLM9FfFmUnIx8G/6q8/5BHyKHDvNYObWgUocNxLiyU" +
                "1IwxFzrmxYbZwuzJAYaTtIgVmCLAPYzJhdLwGiAfPxjR/DhP9gBHCipm++ufqeYiVdNi6SK2m7AvEfe5" +
                "9iItGMBhBSSrLZs+eQ/a8RU6tH0U1TXZ1/OqHPZ4SJkLLVKpePR1jVWK4njvKpfcFhDFxmyXMR3igjFB" +
                "pV9DOjJSTYyOHfKYck7BVtxRfVJR95xW+XRzYesN+VdWSvR76X9aF43KR+PnJ/PYPByZn+HPZyPzC/xR" +
                "R/z87MX5y9eTXx7vvEjBIXnxUwzFicCkdeoVEPzLGfc7J2tSnHE+51pMTn+mEw6aBQkzh/Fy9dKG8YzO" +
                "OX2IJ3So3QThUUh7zrScV3Yhm5E4lbSjRBi4ZIUrn6kYh8vnKIeNYCOvUsww8B6L9rkUXkgnZCnDp5qw" +
                "6wSjgH9gHlSPpRj/G/QTqDV1kpJh7EjmggSEzTESWHgsgIeDxjaY6MG5lWwFnChwECroayu3tcAZZ+Pq" +
                "y7Lx9QojfIQMjjfh8XJ04qbmAM/lB5BJqLAAvgYZToir2Kq71RSYAQ1kJnP4UkfPJErl5i3a2Q9GGqyw" +
                "XdWm7xJ0JaoP5DBUsQW/v5xNGqxynZcLPHbChmOG6kRHFZxx1Ug+zfNWpE5kIXOadFy3u1fZDlaGIs/Y" +
                "4sG2/fR/4sJxwpl6i7+Ax5WClD7jMtfZ+COyhYknKBRPRfnW1CDpiKVr5wrUbgA2Um/8gLXHYcxAacSC" +
                "p5y+SJPL0h4iqBKhJ7KDnbtJ3CwTRGiQ8KPJge3nufiUSiDI1SjYQY4dR3wOJZp3mkNR/4IA4VzoXFbB" +
                "p5LiFm0c1ZZG/U7b0tVIbD5ThtZiV89Y1qC9xdsArCWAGxMde1zK34nhlXWMsBV/6nEUqasVEJoDqSVn" +
                "Bje2JPdIVfUhmJhXlvrsJMRlhAJUxPY2RTkJYECnFl+2L7L79StIVrDi2HGCNWjd4DX+Psef/HrCr4U+" +
                "CjT3uwFDZnXWCSWdZNJxGAY10tBe5gGRXcInn4puXaFFQHGXuTk+5EqkHDzl7ndcojdaSyV+0dt3Zl5e" +
                "uWLCheix1AoXm9DWbR8PywILwayvBk/4w1N9/5xexzMisf1E2uNmrir2PNeIXr0Ig+/h6RU/wEwotinf" +
                "eu3DzGKEHVuf409tS+/JJhGfVWoswyibb3zlLsWK9GC0rCwVb+Zorckvoqwm+ki+Ijs91e0zgVey/yi7" +
                "l5XI06fBSxrsKfbFkiQ+HMygNEHQG7JvEvzoGzDZN/h/iomQ5mUjEFd0s3QtC86ckTSGBQY9+EWtxqLJ" +
                "PNllkHRWTjidREQZxPi4+Q2Yds61wfvD51mz7QdydAy+XZtVSlEUDR2IX/lgLIpOPrItZmyqu+Jty6VS" +
                "cdt+x90kV7NlJliBxijR8T59+TX5aynaj3nHHujn2BbaZUNQ90nh55OdwSKz7vGoOdbgS1ws4n8ptSEa" +
                "DAfaP249Zrr87HraffGU1q2wUzqzhJEJPEakhjufedJDNWRxKapTX+AGOtb5QEMKB4FpXTnbHGrMOVwr" +
                "rHVyMgPL4eQkE8tSdtytC8YV9BNNPj/aPLwV7j/MgBmlbNwCxH5LXxUhFqjyiXmJthAHMeJSyAPO128d" +
                "753G0zkm3gBYyyvaKHaiEgYuJjxu3CVlb6h+vSkDbrfZMI/xTLcYoTef9LaZajWFYgsKYQ5Hqamco9nu" +
                "N70fqPF9MPdxe6YufJomOcBrUHLpQKIAeEFZgBd4qH4g55X4S0mFOkAF5DbwOVveodMtyQNSp0wIzu/1" +
                "JTZbML3aHYZKVMMweOPIJGvI9MRamN02Azpth4E1XUaui6FtpF01Okonl3DMsaxPXwCGMrRBOFuFD+mZ" +
                "MKIRKBqIqOfnXLPJUAk34krVubyZKfVMnaQ9pTlCzB2OzY9oKGMtNtdGiwglLGq8Q0HWZ+cEGym8EdVV" +
                "UzDYSTVQbE76EW2/rXAjUo+x2b1koHcNQqRTKN87OuGNx8MITrZrSI9jvcrGUkwp8kCcZkYcssh00lxL" +
                "M0O3ildwPNjLAcfjpMQ/+yfH9g+ObfePh92CQNlXO4DV673zVCwDkHt+zS6OiCxWuEXjuJwIjwMUfkVJ" +
                "j7lHW07FNUc55Wx9Go8ZeTdPYNtYuEzJy7AN4AZkVUyBc53R3uQ+C9em/U8GoJfRL0AokZDCHBXG53pZ" +
                "G0tWr21UWFjisL+/Pv2aZNojVODHV8Cs8J/daMiupYQofZRwfn52NZ8dE5IN2VE6Tt3/PrgrLRQabGgq" +
                "lABhZGeUbunN4f/F2O2KsQ2e21n+bjGmzf8vibHrpFh+KP4af5BKmaLzt9NoAwuKDfDvzrcfiUzwkel1" +
                "O0eb4qyVkjspoSRWYvJg4/dObIWd80+DeEwrP1mXle7oiaJbQpKoLQiqZArJwOqfuZw2/oLzOp4kBhZQ" +
                "Ws6I2HpBOXrcbcAliqQ0Sc/S7nawY745sH5co7Fz+Dc4mDztWUSQYsW/C0W+SiM+srl8G37uNT6ayLWd" +
                "tzFNGL0cufyKQjPpABDJGnQIDxWD7Z8ylkpELd7GIhUqVdc8xd7c5JxQ5oJiMxrw7rO5hshpuSh8jyBr" +
                "L87ruCxi6hADN+p43j0EL6a2SMNFNCIheIBisEsouXdHXRt2gvi2IdbIuauoTjW67lnonm4vwbziNoaF" +
                "78WJ8TQo1q+XccXoduqQJGvvdjgKCevJLJ4VaV46IVzslXHpHVUxeyYHhOnI5YruIFNUME3LpyUlcx/v" +
                "lMLEPcVZOfodjxkPY5kPjdG/sCfeyiMhqXS9UACYGt/42AVYhcPZ7d6AtenVUO8sxgjzEBe139T/hDze" +
                "/l58IqpylEq1YuBD7V4+nnGwRrQsYq0HE/CYuEePiD+HsZ+1w/170nSd8TQDMQV6+1q3R9SJu0ciiGiA" +
                "LbiaVeph6f0bhMCHH7TYUCNKerjH7V09kk1YqivKRg+xNO5weUoW+FICnF97huj3Hgr6vWd8nvNxm2vP" +
                "4Xz0aM1do2ekyE/AWJPDUl2RZhE7Lj7SIHos8olnELguzNZ5fRIO0TsjhAFdQoRGugZJ/JZm96SQCrck" +
                "APbzyGPNN0kj4NHL0ncBbEV3BZYCTp/zS5RUoRKN6RZsnSenp3gAA/1Cqv/LgcSimyx7atD5aNAGPcab" +
                "C7etXE1AodF2thQIiSfKYsgjvT57/vKHM6z0B5zWWNpCXl68+YD9QhGsNOmgUZ4P4xoz19RJ8YRVSEi+" +
                "enX24hQPt5AQTmMeHo5GGXFukThfT4kh/lSno+umpr6eg6bUI5nx6EaWdJYOaQWk7d9yxsE0KkniKRJt" +
                "HuEEX65dE8+yT53ea6YNvX6+KaH4caEyuPuH/zEvv/ru7OkbvC3qj3eWf5A4Tz+cB9AjYHiJFio8EWQg" +
                "xqiADcz84Nj5RIsRlpDL5xccWo7ulZxltOi1942KCxdjl/kIJ/SG+ycHvVGGwtM9tSmmKuwBSqwTmeZT" +
                "EQVL8YXvzl++uI/pXgk6/Pzk+feGAYCTHlkYxGzcANndCiiolSq7x8NUoYzNGVkNZX1g0WkfxULOqrxw" +
                "J+bOfxwhhY9Ojp6iZXP61dHIHDXet/Bm2bbrk/v38URjBdRuj/7zDqPIafPac8Sj1rwlrZ5YN3S1UKIC" +
                "nyY7gk4lVwNdOCdXiM0r2KpcMTju6csev2LAn4mod0WcfsW8ES+6w30vI3OsAJmraxoScRw5ogvKMJIk" +
                "yFLIm8CcmEgAeockgHe7JDj5079/8Tm3QNXLRVLQbn/GRzLS+d++N7BswWGcP65Tb+Dz36pvtQXDpqHM" +
                "0WYRHv2Z32BW5cT86fNHD+kRWjfYoEQzV1qA2t+Aw7vzGi0UREQH0AQRf135oqvwOxVxtH59pAwNrH3z" +
                "h5JIdx6MYHKEz41EBycdHRW5vTKfoon+qZm9h/8VVP1GFSQnj2Fx3PztA7yXbBofP8PHWXx8iI9FfHz0" +
                "Lt0Y9vk7enfLsY2dO2lSBCAPm5IC37uKhq20cbxc7q6aFHstZ8uy0uQ/NtyN5qVMHlm1cvUdtPqLNcvG" +
                "zR/fkS2xKS/KcePD2DeL++38zl/b+V/u278CG84u8Bph7HMODj067oWfdau4unOpgs8F/l4sS7iwP13D" +
                "3ksseNdzjtiI3w7epABzjBndhvt/sDhCxJkeQwQKgH0RL63jiitqF91LaiKBO3U6QMZflgV69vi1TJ2v" +
                "LdW4awLsEjzeHbYrNtdHbOf3b9LLR9vF4Ay/xRnxYckDB/B3CxwoHq63wuItEVXwscQir8xiYnB9VowP" +
                "JwqNs4KrfTy1RBF0c5OuqFnbkiqcmMLH9Wdg8FG9rmYaUaGyjo+Xn14z81S7F48F7Y+ehQq0Qt5GR5hR" +
                "gUlQepumkVnQepUG2ZiAHRnAUw8rBB0ozPKQnLPebKk5Z8KFinK2h2++5SlJLRkHlGrz5MVpbl9GRhMA" +
                "k5wFMDu+90lX/vb3EHEgxvH7ZQJpHcA14QuaUVpSYVyhOOjjLUw7K2ka3M2uTXfXRu60HCrdlpVH3uKN" +
                "O1oXdTsoUJ3V70UA67A+ioAUa9389LMarH7ApWTJwEmyjbqwWy712g/T6IbX7U5tihhL8s3rb756Ih9u" +
                "+jLhOF68K6+Jvxbx1zT+srdeTUm1a1w31y9q2g3ivn1nDpYnvckq80hy5jfWpYBLgo/G80B6yMrzw48g" +
                "7OgyU/l4Y370B8am6OKjUyorxMpYMLo4+AqahQq7oX3j3OHAYn4401O7g2E5SQWz/XMg0iSHWWLdLucD" +
                "BCDeiXsIgX8G0f7bxKKSNiqTwrMmoPOl6zHlVDGDdN/PZt0alOwQFQaVtNIbW8+2Sorj8bS9P0ZjoKy0" +
                "KowBUUCisngCJZmXXk8ISve+5Hjt6GA8MiiWPK+G8XQ1HhN0aPpxt9oX6bJhPs3Y6RkbQQO8uhIk6/vo" +
                "BnFXvqKQywvocpHxkpMGlovGN03ZKuMErOX+AtU47pbBfwEMmJlQjGcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
