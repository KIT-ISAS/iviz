// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:srv/GetModelTexture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_MODEL_TEXTURE__STRUCT_H_
#define IVIZ_MSGS__SRV__DETAIL__GET_MODEL_TEXTURE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'uri'
#include "rosidl_runtime_c/string.h"

// Struct defined in srv/GetModelTexture in the package iviz_msgs.
typedef struct iviz_msgs__srv__GetModelTexture_Request
{
  rosidl_runtime_c__String uri;
} iviz_msgs__srv__GetModelTexture_Request;

// Struct for a sequence of iviz_msgs__srv__GetModelTexture_Request.
typedef struct iviz_msgs__srv__GetModelTexture_Request__Sequence
{
  iviz_msgs__srv__GetModelTexture_Request * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__GetModelTexture_Request__Sequence;


// Constants defined in the message

// Include directives for member types
// Member 'image'
#include "sensor_msgs/msg/detail/compressed_image__struct.h"
// Member 'message'
// already included above
// #include "rosidl_runtime_c/string.h"

// Struct defined in srv/GetModelTexture in the package iviz_msgs.
typedef struct iviz_msgs__srv__GetModelTexture_Response
{
  bool success;
  sensor_msgs__msg__CompressedImage image;
  rosidl_runtime_c__String message;
} iviz_msgs__srv__GetModelTexture_Response;

// Struct for a sequence of iviz_msgs__srv__GetModelTexture_Response.
typedef struct iviz_msgs__srv__GetModelTexture_Response__Sequence
{
  iviz_msgs__srv__GetModelTexture_Response * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__srv__GetModelTexture_Response__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_MODEL_TEXTURE__STRUCT_H_
