uint8 ACTION_ADD = 0
uint8 ACTION_REMOVE = 1
uint8 ACTION_REMOVEALL = 2

uint8 TYPE_ROTATIONDISC = 0
uint8 TYPE_SPRINGDISC = 1
uint8 TYPE_SPRINGDISC3D = 2
uint8 TYPE_TRAJECTORYDISC = 3
uint8 TYPE_TOOLTIP = 4
uint8 TYPE_TARGETAREA = 5
uint8 TYPE_POSITIONDISC = 6
uint8 TYPE_POSITIONDISC3D = 7
uint8 TYPE_BOUNDARYCHECK = 8

std_msgs/Header header
byte action
string id
byte type
geometry_msgs/Pose pose
std_msgs/ColorRGBA color
std_msgs/ColorRGBA secondary_color
float64 scale
float64 secondary_scale
string caption
BoundingBox boundary
BoundingBoxStamped[] secondary_boundaries
