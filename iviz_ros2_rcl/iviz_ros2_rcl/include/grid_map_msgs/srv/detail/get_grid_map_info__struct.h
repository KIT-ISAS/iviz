// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from grid_map_msgs:srv/GetGridMapInfo.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__STRUCT_H_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Struct defined in srv/GetGridMapInfo in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__GetGridMapInfo_Request
{
  uint8_t structure_needs_at_least_one_member;
} grid_map_msgs__srv__GetGridMapInfo_Request;

// Struct for a sequence of grid_map_msgs__srv__GetGridMapInfo_Request.
typedef struct grid_map_msgs__srv__GetGridMapInfo_Request__Sequence
{
  grid_map_msgs__srv__GetGridMapInfo_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__GetGridMapInfo_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'info'
#include "grid_map_msgs/msg/detail/grid_map_info__struct.h"

// Struct defined in srv/GetGridMapInfo in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__GetGridMapInfo_Response
{
  grid_map_msgs__msg__GridMapInfo info;
} grid_map_msgs__srv__GetGridMapInfo_Response;

// Struct for a sequence of grid_map_msgs__srv__GetGridMapInfo_Response.
typedef struct grid_map_msgs__srv__GetGridMapInfo_Response__Sequence
{
  grid_map_msgs__srv__GetGridMapInfo_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__GetGridMapInfo_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__STRUCT_H_
