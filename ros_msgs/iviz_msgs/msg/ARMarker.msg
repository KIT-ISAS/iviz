byte TYPE_ARUCO = 0
byte TYPE_QRCODE = 1

Header header
byte type
string code					# Text code if QR, integer as string if Aruco.
geometry_msgs/Vector3[4] corners		# Corner pixel positions, with z = 0.
float64[9] camera_intrinsic			# Camera intrinsic matrix, row major.
geometry_msgs/Pose camera_pose			# Pose of the camera in relation to the frame in the header. Y points down, Z forward.

bool has_reliable_pose				# If true, the next two fields use a user-given size. If false, they were estimated using 3d data.
float64 marker_size_in_mm			# Marker size in mm.
geometry_msgs/Pose pose_relative_to_camera	# Pose relative to the camera_pose field. Y points down, Z forward.
