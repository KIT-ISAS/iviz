// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Vector2f.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__VECTOR2F__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__VECTOR2F__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Struct defined in msg/Vector2f in the package iviz_msgs.
typedef struct iviz_msgs__msg__Vector2f
{
  float x;
  float y;
} iviz_msgs__msg__Vector2f;

// Struct for a sequence of iviz_msgs__msg__Vector2f.
typedef struct iviz_msgs__msg__Vector2f__Sequence
{
  iviz_msgs__msg__Vector2f * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Vector2f__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__VECTOR2F__STRUCT_H_
