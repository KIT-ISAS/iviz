// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/ARMarkerArray.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'markers'
#include "iviz_msgs/msg/detail/ar_marker__struct.h"

// Struct defined in msg/ARMarkerArray in the package iviz_msgs.
typedef struct iviz_msgs__msg__ARMarkerArray
{
  iviz_msgs__msg__ARMarker__Sequence markers;
} iviz_msgs__msg__ARMarkerArray;

// Struct for a sequence of iviz_msgs__msg__ARMarkerArray.
typedef struct iviz_msgs__msg__ARMarkerArray__Sequence
{
  iviz_msgs__msg__ARMarkerArray * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__ARMarkerArray__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__STRUCT_H_
