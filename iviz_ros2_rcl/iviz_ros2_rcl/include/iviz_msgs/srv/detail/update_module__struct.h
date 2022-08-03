// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/UpdateModule.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'id'
// Member 'fields'
// Member 'config'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/UpdateModule in the package iviz_msgs.
typedef struct iviz_msgs__srv__UpdateModule_Request
{
  rosidl_runtime_c__String id;
  rosidl_runtime_c__String__Sequence fields;
  rosidl_runtime_c__String config;
} iviz_msgs__srv__UpdateModule_Request;

// Struct for a sequence of iviz_msgs__srv__UpdateModule_Request.
typedef struct iviz_msgs__srv__UpdateModule_Request__Sequence
{
  iviz_msgs__srv__UpdateModule_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__UpdateModule_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
// already included above
// #include "rosidl_runtime_c/string.h"

// Struct defined in srv/UpdateModule in the package iviz_msgs.
typedef struct iviz_msgs__srv__UpdateModule_Response
{
  bool success;
  rosidl_runtime_c__String message;
} iviz_msgs__srv__UpdateModule_Response;

// Struct for a sequence of iviz_msgs__srv__UpdateModule_Response.
typedef struct iviz_msgs__srv__UpdateModule_Response__Sequence
{
  iviz_msgs__srv__UpdateModule_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__UpdateModule_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__STRUCT_H_
