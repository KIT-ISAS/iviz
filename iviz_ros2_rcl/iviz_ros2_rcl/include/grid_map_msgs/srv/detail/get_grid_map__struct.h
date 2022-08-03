// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from grid_map_msgs:srv/GetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__STRUCT_H_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'frame_id'
// Member 'layers'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/GetGridMap in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__GetGridMap_Request
{
  rosidl_runtime_c__String frame_id;
  double position_x;
  double position_y;
  double length_x;
  double length_y;
  rosidl_runtime_c__String__Sequence layers;
} grid_map_msgs__srv__GetGridMap_Request;

// Struct for a sequence of grid_map_msgs__srv__GetGridMap_Request.
typedef struct grid_map_msgs__srv__GetGridMap_Request__Sequence
{
  grid_map_msgs__srv__GetGridMap_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__GetGridMap_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'map'
#include "grid_map_msgs/msg/detail/grid_map__struct.h"

// Struct defined in srv/GetGridMap in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__GetGridMap_Response
{
  grid_map_msgs__msg__GridMap map;
} grid_map_msgs__srv__GetGridMap_Response;

// Struct for a sequence of grid_map_msgs__srv__GetGridMap_Response.
typedef struct grid_map_msgs__srv__GetGridMap_Response__Sequence
{
  grid_map_msgs__srv__GetGridMap_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__GetGridMap_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__STRUCT_H_
