// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Include.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__INCLUDE__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__INCLUDE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'uri'
// Member 'package'
#include "rosidl_runtime_c/string.h"
// Member 'pose'
#include "iviz_msgs/msg/detail/matrix4__struct.h"
// Member 'material'
#include "iviz_msgs/msg/detail/material__struct.h"

// Struct defined in msg/Include in the package iviz_msgs.
typedef struct iviz_msgs__msg__Include
{
  rosidl_runtime_c__String uri;
  iviz_msgs__msg__Matrix4 pose;
  iviz_msgs__msg__Material material;
  rosidl_runtime_c__String package;
} iviz_msgs__msg__Include;

// Struct for a sequence of iviz_msgs__msg__Include.
typedef struct iviz_msgs__msg__Include__Sequence
{
  iviz_msgs__msg__Include * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Include__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__INCLUDE__STRUCT_H_
