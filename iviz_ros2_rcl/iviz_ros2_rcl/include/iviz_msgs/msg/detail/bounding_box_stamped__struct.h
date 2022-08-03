// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/BoundingBoxStamped.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__STRUCT_H_

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
// Member 'boundary'
#include "iviz_msgs/msg/detail/bounding_box__struct.h"

// Struct defined in msg/BoundingBoxStamped in the package iviz_msgs.
typedef struct iviz_msgs__msg__BoundingBoxStamped
{
  std_msgs__msg__Header header;
  iviz_msgs__msg__BoundingBox boundary;
} iviz_msgs__msg__BoundingBoxStamped;

// Struct for a sequence of iviz_msgs__msg__BoundingBoxStamped.
typedef struct iviz_msgs__msg__BoundingBoxStamped__Sequence
{
  iviz_msgs__msg__BoundingBoxStamped * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__BoundingBoxStamped__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__STRUCT_H_
