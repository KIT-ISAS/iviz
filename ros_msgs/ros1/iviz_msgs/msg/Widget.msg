uint8 ACTION_ADD = 0
uint8 ACTION_REMOVE = 1
uint8 ACTION_REMOVEALL = 2

uint8 TYPE_TOOLTIP = 0
uint8 TYPE_ROTATIONDISC = 1
uint8 TYPE_SPRINGDISC = 2
uint8 TYPE_SPRINGDISC3D = 3
uint8 TYPE_TRAJECTORYDISC = 4
uint8 TYPE_TRAJECTORYDISC3D = 5
uint8 TYPE_TARGETAREA = 6
uint8 TYPE_POSITIONDISC = 7
uint8 TYPE_POSITIONDISC3D = 8

std_msgs/Header header
uint8 action
string id
uint8 type
geometry_msgs/Pose pose
std_msgs/ColorRGBA color
std_msgs/ColorRGBA second_color
float64 scale
float64 second_scale
string caption
string second_caption
