// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from grid_map_msgs:msg/GridMapInfo.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__STRUCT_H_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__STRUCT_H_

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
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__struct.h"

// Struct defined in msg/GridMapInfo in the package grid_map_msgs.
typedef struct grid_map_msgs__msg__GridMapInfo
{
  std_msgs__msg__Header header;
  double resolution;
  double length_x;
  double length_y;
  geometry_msgs__msg__Pose pose;
} grid_map_msgs__msg__GridMapInfo;

// Struct for a sequence of grid_map_msgs__msg__GridMapInfo.
typedef struct grid_map_msgs__msg__GridMapInfo__Sequence
{
  grid_map_msgs__msg__GridMapInfo * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__msg__GridMapInfo__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__STRUCT_H_
