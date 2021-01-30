# The representation of a solution to a planning problem, including intermediate data

# The starting state considered for the robot solution path
RobotState trajectory_start

# The group used for planning (usually the same as in the request)
string group_name

# Multiple solution paths are reported, each reflecting intermediate steps in the trajectory processing

# The list of reported trajectories
RobotTrajectory[] trajectory

# Description of the reported trajectories (name of processing step)
string[] description

# The amount of time spent computing a particular step in motion plan computation 
float64[] processing_time

# Status at the end of this plan
MoveItErrorCodes error_code
