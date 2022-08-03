// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Trajectory.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TRAJECTORY__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__TRAJECTORY__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'poses'
#include "geometry_msgs/msg/detail/pose__struct.h"
// Member 'timestamps'
#include "builtin_interfaces/msg/detail/time__struct.h"

// Struct defined in msg/Trajectory in the package iviz_msgs.
typedef struct iviz_msgs__msg__Trajectory
{
  geometry_msgs__msg__Pose__Sequence poses;
  builtin_interfaces__msg__Time__Sequence timestamps;
} iviz_msgs__msg__Trajectory;

// Struct for a sequence of iviz_msgs__msg__Trajectory.
typedef struct iviz_msgs__msg__Trajectory__Sequence
{
  iviz_msgs__msg__Trajectory * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Trajectory__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__TRAJECTORY__STRUCT_H_
