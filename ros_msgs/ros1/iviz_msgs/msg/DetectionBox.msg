uint8 ACTION_ADD = 0
uint8 ACTION_REMOVE = 1
uint8 ACTION_REMOVEALL = 2

std_msgs/Header header
byte action
string id

BoundingBox bounds

string[] classes
float64[] scores

sensor_msgs/PointCloud2 point_cloud


