uint8 ACTION_ADD = 0
uint8 ACTION_REMOVE = 1
uint8 ACTION_REMOVEALL = 2

std_msgs/Header header
uint8 action
string id

BoundingBox bounds
geometry_msgs/Vector3 velocity
std_msgs/ColorRGBA color

string[] classes
float64[] scores

sensor_msgs/PointCloud2 point_cloud


