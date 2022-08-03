// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from grid_map_msgs:srv/SetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__STRUCT_H_
#define GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'map'
#include "grid_map_msgs/msg/detail/grid_map__struct.h"

// Struct defined in srv/SetGridMap in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__SetGridMap_Request
{
  grid_map_msgs__msg__GridMap map;
} grid_map_msgs__srv__SetGridMap_Request;

// Struct for a sequence of grid_map_msgs__srv__SetGridMap_Request.
typedef struct grid_map_msgs__srv__SetGridMap_Request__Sequence
{
  grid_map_msgs__srv__SetGridMap_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__SetGridMap_Request__Sequence;


// Constants defined in the message

// Struct defined in srv/SetGridMap in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__SetGridMap_Response
{
  uint8_t structure_needs_at_least_one_member;
} grid_map_msgs__srv__SetGridMap_Response;

// Struct for a sequence of grid_map_msgs__srv__SetGridMap_Response.
typedef struct grid_map_msgs__srv__SetGridMap_Response__Sequence
{
  grid_map_msgs__srv__SetGridMap_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__SetGridMap_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__STRUCT_H_
