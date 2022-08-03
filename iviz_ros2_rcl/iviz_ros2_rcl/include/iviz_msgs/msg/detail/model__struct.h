// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Model.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MODEL__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__MODEL__STRUCT_H_

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
// Member 'filename'
// Member 'orientation_hint'
#include "rosidl_runtime_c/string.h"
// Member 'meshes'
#include "iviz_msgs/msg/detail/mesh__struct.h"
// Member 'materials'
#include "iviz_msgs/msg/detail/material__struct.h"
// Member 'nodes'
#include "iviz_msgs/msg/detail/node__struct.h"

// Struct defined in msg/Model in the package iviz_msgs.
typedef struct iviz_msgs__msg__Model
{
  rosidl_runtime_c__String name;
  rosidl_runtime_c__String filename;
  rosidl_runtime_c__String orientation_hint;
  iviz_msgs__msg__Mesh__Sequence meshes;
  iviz_msgs__msg__Material__Sequence materials;
  iviz_msgs__msg__Node__Sequence nodes;
} iviz_msgs__msg__Model;

// Struct for a sequence of iviz_msgs__msg__Model.
typedef struct iviz_msgs__msg__Model__Sequence
{
  iviz_msgs__msg__Model * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Model__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__MODEL__STRUCT_H_
