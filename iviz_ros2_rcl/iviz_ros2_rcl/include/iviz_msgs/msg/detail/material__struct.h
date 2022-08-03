// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Material.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MATERIAL__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__MATERIAL__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

/// Constant 'BLEND_DEFAULT'.
enum
{
  iviz_msgs__msg__Material__BLEND_DEFAULT = 0
};

/// Constant 'BLEND_ADDITIVE'.
enum
{
  iviz_msgs__msg__Material__BLEND_ADDITIVE = 1
};

// Include directives for member types
// Member 'name'
#include "rosidl_runtime_c/string.h"
// Member 'ambient'
// Member 'diffuse'
// Member 'emissive'
#include "iviz_msgs/msg/detail/color32__struct.h"
// Member 'textures'
#include "iviz_msgs/msg/detail/texture__struct.h"

// Struct defined in msg/Material in the package iviz_msgs.
typedef struct iviz_msgs__msg__Material
{
  rosidl_runtime_c__String name;
  iviz_msgs__msg__Color32 ambient;
  iviz_msgs__msg__Color32 diffuse;
  iviz_msgs__msg__Color32 emissive;
  float opacity;
  float bump_scaling;
  float shininess;
  float shininess_strength;
  float reflectivity;
  uint8_t blend_mode;
  iviz_msgs__msg__Texture__Sequence textures;
} iviz_msgs__msg__Material;

// Struct for a sequence of iviz_msgs__msg__Material.
typedef struct iviz_msgs__msg__Material__Sequence
{
  iviz_msgs__msg__Material * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Material__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__MATERIAL__STRUCT_H_
