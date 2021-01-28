# name of planning scene
string name

# full robot state
RobotState robot_state

# The name of the robot model this scene is for
string robot_model_name

#additional frames for duplicating tf (with respect to the planning frame)
geometry_msgs/TransformStamped[] fixed_frame_transforms

#full allowed collision matrix
AllowedCollisionMatrix allowed_collision_matrix

# all link paddings
LinkPadding[] link_padding

# all link scales
LinkScale[] link_scale

# Attached objects, collision objects, even the octomap or collision map can have 
# colors associated to them. This array specifies them.
ObjectColor[] object_colors

# the collision map
PlanningSceneWorld world

# Flag indicating whether this scene is to be interpreted as a diff with respect to some other scene
bool is_diff
