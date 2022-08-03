// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/ColorChannel.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'colors'
#include "iviz_msgs/msg/detail/color32__struct.h"

// Struct defined in msg/ColorChannel in the package iviz_msgs.
typedef struct iviz_msgs__msg__ColorChannel
{
  iviz_msgs__msg__Color32__Sequence colors;
} iviz_msgs__msg__ColorChannel;

// Struct for a sequence of iviz_msgs__msg__ColorChannel.
typedef struct iviz_msgs__msg__ColorChannel__Sequence
{
  iviz_msgs__msg__ColorChannel * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__ColorChannel__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__STRUCT_H_
