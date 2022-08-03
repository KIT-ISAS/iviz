// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from grid_map_msgs:srv/ProcessFile.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__STRUCT_H_
#define GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'file_path'
// Member 'topic_name'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/ProcessFile in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__ProcessFile_Request
{
  rosidl_runtime_c__String file_path;
  rosidl_runtime_c__String topic_name;
} grid_map_msgs__srv__ProcessFile_Request;

// Struct for a sequence of grid_map_msgs__srv__ProcessFile_Request.
typedef struct grid_map_msgs__srv__ProcessFile_Request__Sequence
{
  grid_map_msgs__srv__ProcessFile_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__ProcessFile_Request__Sequence;


// Constants defined in the message

// Struct defined in srv/ProcessFile in the package grid_map_msgs.
typedef struct grid_map_msgs__srv__ProcessFile_Response
{
  bool success;
} grid_map_msgs__srv__ProcessFile_Response;

// Struct for a sequence of grid_map_msgs__srv__ProcessFile_Response.
typedef struct grid_map_msgs__srv__ProcessFile_Response__Sequence
{
  grid_map_msgs__srv__ProcessFile_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} grid_map_msgs__srv__ProcessFile_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__STRUCT_H_
