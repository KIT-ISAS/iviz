// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/GetCaptureResolutions.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Struct defined in srv/GetCaptureResolutions in the package iviz_msgs.
typedef struct iviz_msgs__srv__GetCaptureResolutions_Request
{
  uint8_t structure_needs_at_least_one_member;
} iviz_msgs__srv__GetCaptureResolutions_Request;

// Struct for a sequence of iviz_msgs__srv__GetCaptureResolutions_Request.
typedef struct iviz_msgs__srv__GetCaptureResolutions_Request__Sequence
{
  iviz_msgs__srv__GetCaptureResolutions_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__GetCaptureResolutions_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
#include "rosidl_runtime_c/string.h"
// Member 'resolutions'
#include "iviz_msgs/msg/detail/vector2i__struct.h"

// Struct defined in srv/GetCaptureResolutions in the package iviz_msgs.
typedef struct iviz_msgs__srv__GetCaptureResolutions_Response
{
  bool success;
  rosidl_runtime_c__String message;
  iviz_msgs__msg__Vector2i__Sequence resolutions;
} iviz_msgs__srv__GetCaptureResolutions_Response;

// Struct for a sequence of iviz_msgs__srv__GetCaptureResolutions_Response.
typedef struct iviz_msgs__srv__GetCaptureResolutions_Response__Sequence
{
  iviz_msgs__srv__GetCaptureResolutions_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__GetCaptureResolutions_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__STRUCT_H_
