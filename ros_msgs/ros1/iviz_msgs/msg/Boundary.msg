uint8 ACTION_ADD = 0
uint8 ACTION_REMOVE = 1
uint8 ACTION_REMOVEALL = 2

uint8 TYPE_SIMPLE = 0
uint8 TYPE_COLLIDER = 1
uint8 TYPE_COLLIDABLE = 2

std_msgs/Header header
byte action
string id
byte type
geometry_msgs/Pose pose
geometry_msgs/Vector3 scale
std_msgs/ColorRGBA color
std_msgs/ColorRGBA second_color
string caption
