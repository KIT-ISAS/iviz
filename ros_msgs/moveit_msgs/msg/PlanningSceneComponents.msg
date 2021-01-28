# This message defines the components that make up the PlanningScene message.
# The values can be used as a bitfield to specify which parts of the PlanningScene message
# are of interest

# Scene name, model name, model root
uint32 SCENE_SETTINGS=1

# Joint values of the robot state
uint32 ROBOT_STATE=2

# Attached objects (including geometry) for the robot state
uint32 ROBOT_STATE_ATTACHED_OBJECTS=4

# The names of the world objects
uint32 WORLD_OBJECT_NAMES=8

# The geometry of the world objects
uint32 WORLD_OBJECT_GEOMETRY=16

# The maintained octomap 
uint32 OCTOMAP=32

# The maintained list of transforms
uint32 TRANSFORMS=64

# The allowed collision matrix
uint32 ALLOWED_COLLISION_MATRIX=128

# The default link padding and link scaling
uint32 LINK_PADDING_AND_SCALING=256

# The stored object colors
uint32 OBJECT_COLORS=512

# Bitfield combining options indicated above
uint32 components
