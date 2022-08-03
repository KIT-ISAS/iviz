// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/BoundingBox.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'center'
#include "geometry_msgs/msg/detail/pose__struct.h"
// Member 'size'
#include "geometry_msgs/msg/detail/vector3__struct.h"

// Struct defined in msg/BoundingBox in the package iviz_msgs.
typedef struct iviz_msgs__msg__BoundingBox
{
  geometry_msgs__msg__Pose center;
  geometry_msgs__msg__Vector3 size;
} iviz_msgs__msg__BoundingBox;

// Struct for a sequence of iviz_msgs__msg__BoundingBox.
typedef struct iviz_msgs__msg__BoundingBox__Sequence
{
  iviz_msgs__msg__BoundingBox * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__BoundingBox__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__STRUCT_H_
