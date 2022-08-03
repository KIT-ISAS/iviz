// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/LaunchDialog.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'dialog'
#include "iviz_msgs/msg/detail/dialog__struct.h"

// Struct defined in srv/LaunchDialog in the package iviz_msgs.
typedef struct iviz_msgs__srv__LaunchDialog_Request
{
  iviz_msgs__msg__Dialog dialog;
} iviz_msgs__srv__LaunchDialog_Request;

// Struct for a sequence of iviz_msgs__srv__LaunchDialog_Request.
typedef struct iviz_msgs__srv__LaunchDialog_Request__Sequence
{
  iviz_msgs__srv__LaunchDialog_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__LaunchDialog_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
#include "rosidl_runtime_c/string.h"
// Member 'feedback'
#include "iviz_msgs/msg/detail/feedback__struct.h"

// Struct defined in srv/LaunchDialog in the package iviz_msgs.
typedef struct iviz_msgs__srv__LaunchDialog_Response
{
  bool success;
  rosidl_runtime_c__String message;
  iviz_msgs__msg__Feedback feedback;
} iviz_msgs__srv__LaunchDialog_Response;

// Struct for a sequence of iviz_msgs__srv__LaunchDialog_Response.
typedef struct iviz_msgs__srv__LaunchDialog_Response__Sequence
{
  iviz_msgs__srv__LaunchDialog_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__LaunchDialog_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__STRUCT_H_
