// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/XRGazeState.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__XR_GAZE_STATE__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__XR_GAZE_STATE__STRUCT_H_

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
// Member 'transform'
#include "geometry_msgs/msg/detail/transform__struct.h"

// Struct defined in msg/XRGazeState in the package iviz_msgs.
typedef struct iviz_msgs__msg__XRGazeState
{
  bool is_valid;
  std_msgs__msg__Header header;
  geometry_msgs__msg__Transform transform;
} iviz_msgs__msg__XRGazeState;

// Struct for a sequence of iviz_msgs__msg__XRGazeState.
typedef struct iviz_msgs__msg__XRGazeState__Sequence
{
  iviz_msgs__msg__XRGazeState * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__XRGazeState__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__XR_GAZE_STATE__STRUCT_H_
