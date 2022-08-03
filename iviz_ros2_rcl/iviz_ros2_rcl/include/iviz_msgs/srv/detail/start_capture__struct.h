// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/StartCapture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Struct defined in srv/StartCapture in the package iviz_msgs.
typedef struct iviz_msgs__srv__StartCapture_Request
{
  int32_t resolution_x;
  int32_t resolution_y;
  bool with_holograms;
} iviz_msgs__srv__StartCapture_Request;

// Struct for a sequence of iviz_msgs__srv__StartCapture_Request.
typedef struct iviz_msgs__srv__StartCapture_Request__Sequence
{
  iviz_msgs__srv__StartCapture_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__StartCapture_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/StartCapture in the package iviz_msgs.
typedef struct iviz_msgs__srv__StartCapture_Response
{
  bool success;
  rosidl_runtime_c__String message;
} iviz_msgs__srv__StartCapture_Response;

// Struct for a sequence of iviz_msgs__srv__StartCapture_Response.
typedef struct iviz_msgs__srv__StartCapture_Response__Sequence
{
  iviz_msgs__srv__StartCapture_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__StartCapture_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__STRUCT_H_
