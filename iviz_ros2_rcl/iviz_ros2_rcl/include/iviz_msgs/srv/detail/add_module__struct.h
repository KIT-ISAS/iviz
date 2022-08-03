// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/AddModule.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__ADD_MODULE__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__ADD_MODULE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'module_type'
// Member 'id'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/AddModule in the package iviz_msgs.
typedef struct iviz_msgs__srv__AddModule_Request
{
  rosidl_runtime_c__String module_type;
  rosidl_runtime_c__String id;
} iviz_msgs__srv__AddModule_Request;

// Struct for a sequence of iviz_msgs__srv__AddModule_Request.
typedef struct iviz_msgs__srv__AddModule_Request__Sequence
{
  iviz_msgs__srv__AddModule_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__AddModule_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
// Member 'id'
// already included above
// #include "rosidl_runtime_c/string.h"

// Struct defined in srv/AddModule in the package iviz_msgs.
typedef struct iviz_msgs__srv__AddModule_Response
{
  bool success;
  rosidl_runtime_c__String message;
  rosidl_runtime_c__String id;
} iviz_msgs__srv__AddModule_Response;

// Struct for a sequence of iviz_msgs__srv__AddModule_Response.
typedef struct iviz_msgs__srv__AddModule_Response__Sequence
{
  iviz_msgs__srv__AddModule_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__AddModule_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__ADD_MODULE__STRUCT_H_
