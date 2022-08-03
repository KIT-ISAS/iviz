// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/RobotConfiguration.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'source_parameter'
// Member 'saved_robot_name'
// Member 'frame_prefix'
// Member 'frame_suffix'
// Member 'id'
#include "rosidl_runtime_c/string.h"
// Member 'tint'
#include "std_msgs/msg/detail/color_rgba__struct.h"

// Struct defined in msg/RobotConfiguration in the package iviz_msgs.
typedef struct iviz_msgs__msg__RobotConfiguration
{
  rosidl_runtime_c__String source_parameter;
  rosidl_runtime_c__String saved_robot_name;
  rosidl_runtime_c__String frame_prefix;
  rosidl_runtime_c__String frame_suffix;
  bool attached_to_tf;
  bool render_as_occlusion_only;
  std_msgs__msg__ColorRGBA tint;
  float metallic;
  float smoothness;
  rosidl_runtime_c__String id;
  bool visible;
} iviz_msgs__msg__RobotConfiguration;

// Struct for a sequence of iviz_msgs__msg__RobotConfiguration.
typedef struct iviz_msgs__msg__RobotConfiguration__Sequence
{
  iviz_msgs__msg__RobotConfiguration * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__RobotConfiguration__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__STRUCT_H_
