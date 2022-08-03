// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/CaptureScreenshot.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Struct defined in srv/CaptureScreenshot in the package iviz_msgs.
typedef struct iviz_msgs__srv__CaptureScreenshot_Request
{
  bool compress;
} iviz_msgs__srv__CaptureScreenshot_Request;

// Struct for a sequence of iviz_msgs__srv__CaptureScreenshot_Request.
typedef struct iviz_msgs__srv__CaptureScreenshot_Request__Sequence
{
  iviz_msgs__srv__CaptureScreenshot_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__CaptureScreenshot_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
#include "rosidl_runtime_c/string.h"
// Member 'header'
#include "std_msgs/msg/detail/header__struct.h"
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__struct.h"
// Member 'data'
#include "rosidl_runtime_c/primitives_sequence.h"

// Struct defined in srv/CaptureScreenshot in the package iviz_msgs.
typedef struct iviz_msgs__srv__CaptureScreenshot_Response
{
  bool success;
  rosidl_runtime_c__String message;
  std_msgs__msg__Header header;
  int32_t width;
  int32_t height;
  int32_t bpp;
  double intrinsics[9];
  geometry_msgs__msg__Pose pose;
  rosidl_runtime_c__octet__Sequence data;
} iviz_msgs__srv__CaptureScreenshot_Response;

// Struct for a sequence of iviz_msgs__srv__CaptureScreenshot_Response.
typedef struct iviz_msgs__srv__CaptureScreenshot_Response__Sequence
{
  iviz_msgs__srv__CaptureScreenshot_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__CaptureScreenshot_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__STRUCT_H_
