// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Node.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__NODE__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__NODE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'name'
#include "rosidl_runtime_c/string.h"
// Member 'transform'
#include "iviz_msgs/msg/detail/matrix4__struct.h"
// Member 'meshes'
#include "rosidl_runtime_c/primitives_sequence.h"

// Struct defined in msg/Node in the package iviz_msgs.
typedef struct iviz_msgs__msg__Node
{
  rosidl_runtime_c__String name;
  int32_t parent;
  iviz_msgs__msg__Matrix4 transform;
  rosidl_runtime_c__int32__Sequence meshes;
} iviz_msgs__msg__Node;

// Struct for a sequence of iviz_msgs__msg__Node.
typedef struct iviz_msgs__msg__Node__Sequence
{
  iviz_msgs__msg__Node * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Node__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__NODE__STRUCT_H_
