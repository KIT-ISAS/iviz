# The plan request for this item.
# It is the planning request for this segment of the sequence, as if it were a solitary motion.
MotionPlanRequest req

# To blend between sequence items, the motion may be smoothed using a circular motion.
# The blend radius of the circle between this and the next command, where 0 means no blending.
float64 blend_radius
