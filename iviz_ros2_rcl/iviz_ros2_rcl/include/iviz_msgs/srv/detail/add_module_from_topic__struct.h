// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/AddModuleFromTopic.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'topic'
// Member 'id'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/AddModuleFromTopic in the package iviz_msgs.
typedef struct iviz_msgs__srv__AddModuleFromTopic_Request
{
  rosidl_runtime_c__String topic;
  rosidl_runtime_c__String id;
} iviz_msgs__srv__AddModuleFromTopic_Request;

// Struct for a sequence of iviz_msgs__srv__AddModuleFromTopic_Request.
typedef struct iviz_msgs__srv__AddModuleFromTopic_Request__Sequence
{
  iviz_msgs__srv__AddModuleFromTopic_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__AddModuleFromTopic_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'message'
// Member 'id'
// already included above
// #include "rosidl_runtime_c/string.h"

// Struct defined in srv/AddModuleFromTopic in the package iviz_msgs.
typedef struct iviz_msgs__srv__AddModuleFromTopic_Response
{
  bool success;
  rosidl_runtime_c__String message;
  rosidl_runtime_c__String id;
} iviz_msgs__srv__AddModuleFromTopic_Response;

// Struct for a sequence of iviz_msgs__srv__AddModuleFromTopic_Response.
typedef struct iviz_msgs__srv__AddModuleFromTopic_Response__Sequence
{
  iviz_msgs__srv__AddModuleFromTopic_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__AddModuleFromTopic_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__STRUCT_H_
