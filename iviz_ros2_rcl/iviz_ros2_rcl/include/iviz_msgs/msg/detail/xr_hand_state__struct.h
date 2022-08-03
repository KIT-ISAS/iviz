// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/XRHandState.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.h"
// Member 'palm'
// Member 'thumb'
// Member 'index'
// Member 'middle'
// Member 'ring'
// Member 'little'
#include "geometry_msgs/msg/detail/transform__struct.h"

// Struct defined in msg/XRHandState in the package iviz_msgs.
typedef struct iviz_msgs__msg__XRHandState
{
  bool is_valid;
  std_msgs__msg__Header header;
  geometry_msgs__msg__Transform palm;
  geometry_msgs__msg__Transform__Sequence thumb;
  geometry_msgs__msg__Transform__Sequence index;
  geometry_msgs__msg__Transform__Sequence middle;
  geometry_msgs__msg__Transform__Sequence ring;
  geometry_msgs__msg__Transform__Sequence little;
} iviz_msgs__msg__XRHandState;

// Struct for a sequence of iviz_msgs__msg__XRHandState.
typedef struct iviz_msgs__msg__XRHandState__Sequence
{
  iviz_msgs__msg__XRHandState * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__XRHandState__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__STRUCT_H_
