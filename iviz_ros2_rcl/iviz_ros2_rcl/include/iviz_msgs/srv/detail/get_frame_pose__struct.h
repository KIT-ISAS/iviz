// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/GetFramePose.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_FRAME_POSE__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__GET_FRAME_POSE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'frames'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/GetFramePose in the package iviz_msgs.
typedef struct iviz_msgs__srv__GetFramePose_Request
{
  rosidl_runtime_c__String__Sequence frames;
} iviz_msgs__srv__GetFramePose_Request;

// Struct for a sequence of iviz_msgs__srv__GetFramePose_Request.
typedef struct iviz_msgs__srv__GetFramePose_Request__Sequence
{
  iviz_msgs__srv__GetFramePose_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__GetFramePose_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'is_valid'
#include "rosidl_runtime_c/primitives_sequence.h"
// Member 'poses'
#include "geometry_msgs/msg/detail/pose__struct.h"

// Struct defined in srv/GetFramePose in the package iviz_msgs.
typedef struct iviz_msgs__srv__GetFramePose_Response
{
  rosidl_runtime_c__boolean__Sequence is_valid;
  geometry_msgs__msg__Pose__Sequence poses;
} iviz_msgs__srv__GetFramePose_Response;

// Struct for a sequence of iviz_msgs__srv__GetFramePose_Response.
typedef struct iviz_msgs__srv__GetFramePose_Response__Sequence
{
  iviz_msgs__srv__GetFramePose_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__GetFramePose_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_FRAME_POSE__STRUCT_H_
