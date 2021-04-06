uint8 MARKER_ARUCO = 0
uint8 MARKER_QR = 1

Header header
uint8 marker_type
uint32 aruco_id
string qr_code
geometry_msgs/Vector3[4] corners
float64[9] intrinsic
geometry_msgs/Pose camera_pose
float64 marker_size
geometry_msgs/Pose marker_pose


