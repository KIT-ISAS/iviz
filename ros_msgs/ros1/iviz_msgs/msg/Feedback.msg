uint8 TYPE_EXPIRED = 0
uint8 TYPE_BUTTON_CLICK = 1
uint8 TYPE_MENUENTRY_CLICK = 2
uint8 TYPE_POSITION_CHANGED = 3
uint8 TYPE_ORIENTATION_CHANGED = 4
uint8 TYPE_SCALE_CHANGED = 5
uint8 TYPE_TRAJECTORY_CHANGED = 6
uint8 TYPE_COLLIDER_ENTERED = 7
uint8 TYPE_COLLIDER_EXITED = 8

std_msgs/Header header
string viz_id
string id
uint8 type
int32 entry_id
float64 angle
geometry_msgs/Point position
geometry_msgs/Quaternion orientation
geometry_msgs/Vector3 scale
Trajectory trajectory
string collider_id

