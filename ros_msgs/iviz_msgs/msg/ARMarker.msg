byte TYPE_ARUCO = 0
byte TYPE_QRCODE = 1

Header header
byte type
string code
geometry_msgs/Vector3[4] corners		# pixel position with z = 0
float64[9] camera_intrinsic			# row major intrinsic
geometry_msgs/Pose camera_pose

bool has_extrinsic_pose
float64 marker_size_in_mm			
geometry_msgs/Pose pose_relative_to_camera	
