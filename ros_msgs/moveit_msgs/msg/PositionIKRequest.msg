# A Position IK request message

# The name of the group which will be used to compute IK
# e.g. "right_arm", or "arms" - see IK specification for multiple-groups below
# Information from the SRDF will be used to automatically determine which link 
# to solve IK for, unless ik_link_name is also specified
string group_name

# A RobotState consisting of hint/seed positions for the IK computation and positions 
# for all the other joints in the robot. Additional state information provided here is 
# used to specify starting positions for other joints/links on the robot.  
# This state MUST contain state for all joints to be used by the IK solver
# to compute IK. The list of joints that the IK solver deals with can be 
# found using the SRDF for the corresponding group
moveit_msgs/RobotState robot_state

# A set of constraints that the IK must obey; by default, this set of constraints is empty
Constraints constraints

# Find an IK solution that avoids collisions. By default, this is false
bool avoid_collisions

# (OPTIONAL) The name of the link for which we are computing IK
# If not specified, the link name will be inferred from a combination 
# of the group name and the SRDF. If any values are specified for ik_link_names,
# this value is ignored
string ik_link_name

# The stamped pose of the link, when the IK solver computes the joint values
# for all the joints in a group. This value is ignored if pose_stamped_vector
# has any elements specified.
geometry_msgs/PoseStamped pose_stamped

# Multi-group parameters

# (OPTIONAL) The names of the links for which we are computing IK
# If not specified, the link name will be inferred from a combination 
# of the group name and the SRDF
string[] ik_link_names

# (OPTIONAL) The (stamped) poses of the links we are computing IK for (when a group has more than one end effector)
# e.g. The "arms" group might consist of both the "right_arm" and the "left_arm"
# The order of the groups referred to is the same as the order setup in the SRDF
geometry_msgs/PoseStamped[] pose_stamped_vector

# Maximum allowed time for IK calculation
duration timeout

# Maximum number of IK attempts (if using random seeds; leave as 0 for the default value specified on the param server to be used)
int32 attempts

