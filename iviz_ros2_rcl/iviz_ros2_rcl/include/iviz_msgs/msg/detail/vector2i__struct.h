// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Vector2i.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__VECTOR2I__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__VECTOR2I__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Struct defined in msg/Vector2i in the package iviz_msgs.
typedef struct iviz_msgs__msg__Vector2i
{
  int32_t x;
  int32_t y;
} iviz_msgs__msg__Vector2i;

// Struct for a sequence of iviz_msgs__msg__Vector2i.
typedef struct iviz_msgs__msg__Vector2i__Sequence
{
  iviz_msgs__msg__Vector2i * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Vector2i__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__VECTOR2I__STRUCT_H_
