// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Scene.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__SCENE__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__SCENE__STRUCT_H_

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
#include "rosidl_runtime_c/string.h"
// Member 'includes'
#include "iviz_msgs/msg/detail/include__struct.h"
// Member 'lights'
#include "iviz_msgs/msg/detail/light__struct.h"

// Struct defined in msg/Scene in the package iviz_msgs.
typedef struct iviz_msgs__msg__Scene
{
  rosidl_runtime_c__String name;
  rosidl_runtime_c__String filename;
  iviz_msgs__msg__Include__Sequence includes;
  iviz_msgs__msg__Light__Sequence lights;
} iviz_msgs__msg__Scene;

// Struct for a sequence of iviz_msgs__msg__Scene.
typedef struct iviz_msgs__msg__Scene__Sequence
{
  iviz_msgs__msg__Scene * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Scene__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__SCENE__STRUCT_H_
