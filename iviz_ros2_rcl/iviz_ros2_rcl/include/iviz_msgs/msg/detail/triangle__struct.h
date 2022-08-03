// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Triangle.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TRIANGLE__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__TRIANGLE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Struct defined in msg/Triangle in the package iviz_msgs.
typedef struct iviz_msgs__msg__Triangle
{
  uint32_t a;
  uint32_t b;
  uint32_t c;
} iviz_msgs__msg__Triangle;

// Struct for a sequence of iviz_msgs__msg__Triangle.
typedef struct iviz_msgs__msg__Triangle__Sequence
{
  iviz_msgs__msg__Triangle * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Triangle__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__TRIANGLE__STRUCT_H_
