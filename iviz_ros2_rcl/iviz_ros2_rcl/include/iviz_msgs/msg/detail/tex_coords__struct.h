// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/TexCoords.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TEX_COORDS__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__TEX_COORDS__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'coords'
#include "iviz_msgs/msg/detail/vector3f__struct.h"

// Struct defined in msg/TexCoords in the package iviz_msgs.
typedef struct iviz_msgs__msg__TexCoords
{
  iviz_msgs__msg__Vector3f__Sequence coords;
} iviz_msgs__msg__TexCoords;

// Struct for a sequence of iviz_msgs__msg__TexCoords.
typedef struct iviz_msgs__msg__TexCoords__Sequence
{
  iviz_msgs__msg__TexCoords * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__TexCoords__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__TEX_COORDS__STRUCT_H_
