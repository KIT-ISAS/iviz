// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Light.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__LIGHT__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__LIGHT__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

/// Constant 'POINT'.
enum
{
  iviz_msgs__msg__Light__POINT = 0
};

/// Constant 'DIRECTIONAL'.
enum
{
  iviz_msgs__msg__Light__DIRECTIONAL = 1
};

/// Constant 'SPOT'.
enum
{
  iviz_msgs__msg__Light__SPOT = 2
};

// Include directives for member types
// Member 'name'
#include "rosidl_runtime_c/string.h"
// Member 'diffuse'
#include "iviz_msgs/msg/detail/color32__struct.h"
// Member 'position'
// Member 'direction'
#include "iviz_msgs/msg/detail/vector3f__struct.h"

// Struct defined in msg/Light in the package iviz_msgs.
typedef struct iviz_msgs__msg__Light
{
  rosidl_runtime_c__String name;
  uint8_t type;
  bool cast_shadows;
  iviz_msgs__msg__Color32 diffuse;
  float range;
  iviz_msgs__msg__Vector3f position;
  iviz_msgs__msg__Vector3f direction;
  float inner_angle;
  float outer_angle;
} iviz_msgs__msg__Light;

// Struct for a sequence of iviz_msgs__msg__Light.
typedef struct iviz_msgs__msg__Light__Sequence
{
  iviz_msgs__msg__Light * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Light__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__LIGHT__STRUCT_H_
