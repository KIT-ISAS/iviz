// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/UpdateRobot.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__STRUCT_H_

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
// Member 'valid_fields'
#include "rosidl_runtime_c/string.h"
// Member 'configuration'
#include "iviz_msgs/msg/detail/robot_configuration__struct.h"

// Struct defined in srv/UpdateRobot in the package iviz_msgs.
typedef struct iviz_msgs__srv__UpdateRobot_Request
{
  int32_t operation;
  rosidl_runtime_c__String id;
  iviz_msgs__msg__RobotConfiguration configuration;
  rosidl_runtime_c__String__Sequence valid_fields;
} iviz_msgs__srv__UpdateRobot_Request;

// Struct for a sequence of iviz_msgs__srv__UpdateRobot_Request.
typedef struct iviz_msgs__srv__UpdateRobot_Request__Sequence
{
  iviz_msgs__srv__UpdateRobot_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__UpdateRobot_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
// already included above
// #include "rosidl_runtime_c/string.h"

// Struct defined in srv/UpdateRobot in the package iviz_msgs.
typedef struct iviz_msgs__srv__UpdateRobot_Response
{
  bool success;
  rosidl_runtime_c__String message;
} iviz_msgs__srv__UpdateRobot_Response;

// Struct for a sequence of iviz_msgs__srv__UpdateRobot_Response.
typedef struct iviz_msgs__srv__UpdateRobot_Response__Sequence
{
  iviz_msgs__srv__UpdateRobot_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__UpdateRobot_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__STRUCT_H_
