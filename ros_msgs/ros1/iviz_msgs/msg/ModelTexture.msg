uint8 TYPE_NONE = 0
uint8 TYPE_DIFFUSE = 1
uint8 TYPE_SPECULAR = 2
uint8 TYPE_AMBIENT = 3
uint8 TYPE_EMISSIVE = 4
uint8 TYPE_HEIGHT = 5
uint8 TYPE_NORMALS = 6
uint8 TYPE_SHININESS = 7
uint8 TYPE_OPACITY = 8
uint8 TYPE_DISPLACEMENT = 9
uint8 TYPE_LIGHTMAP = 10
uint8 TYPE_REFLECTION = 11
uint8 TYPE_UNKNOWN = 12

uint8 MAPPING_FROM_UV = 0
uint8 MAPPING_SPHERE = 1
uint8 MAPPING_CYLINDER = 2
uint8 MAPPING_BOX = 3
uint8 MAPPING_PLANE = 4
uint8 MAPPING_UNKNOWN = 5

uint8 OP_MULTIPLY = 0
uint8 OP_ADD = 1
uint8 OP_SUBTRACT = 2
uint8 OP_DIVIDE = 3
uint8 OP_SMOOTH_ADD = 4
uint8 OP_SIGNED_ADD = 5

uint8 WRAP_WRAP = 0
uint8 WRAP_CLAMP = 1
uint8 WRAP_MIRROR = 2
uint8 WRAP_DECAL = 3

string path
int32 index
uint8 type
uint8 mapping
int32 uv_index
float32 blend_factor
uint8 operation
uint8 wrap_mode_u
uint8 wrap_mode_v
