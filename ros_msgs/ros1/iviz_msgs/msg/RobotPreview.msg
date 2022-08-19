uint8 ACTION_ADD = 0
uint8 ACTION_REMOVE = 1
uint8 ACTION_REMOVEALL = 2

std_msgs/Header header
byte action
string id

string[] joint_names
float32[] joint_values

string robot_description
string source_node
string source_parameter
string saved_robot_name
std_msgs/ColorRGBA tint
float32 metallic
float32 smoothness
bool attached_to_tf
bool render_as_occlusion_only
