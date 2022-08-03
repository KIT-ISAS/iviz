// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/ARMarker.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__AR_MARKER__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__AR_MARKER__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

/// Constant 'TYPE_ARUCO'.
enum
{
  iviz_msgs__msg__ARMarker__TYPE_ARUCO = 0
};

/// Constant 'TYPE_QRCODE'.
enum
{
  iviz_msgs__msg__ARMarker__TYPE_QRCODE = 1
};

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.h"
// Member 'code'
#include "rosidl_runtime_c/string.h"
// Member 'corners'
#include "geometry_msgs/msg/detail/vector3__struct.h"
// Member 'camera_pose'
// Member 'pose_relative_to_camera'
#include "geometry_msgs/msg/detail/pose__struct.h"

// Struct defined in msg/ARMarker in the package iviz_msgs.
typedef struct iviz_msgs__msg__ARMarker
{
  std_msgs__msg__Header header;
  uint8_t type;
  rosidl_runtime_c__String code;
  geometry_msgs__msg__Vector3 corners[4];
  double camera_intrinsic[9];
  geometry_msgs__msg__Pose camera_pose;
  bool has_reliable_pose;
  double marker_size_in_mm;
  geometry_msgs__msg__Pose pose_relative_to_camera;
} iviz_msgs__msg__ARMarker;

// Struct for a sequence of iviz_msgs__msg__ARMarker.
typedef struct iviz_msgs__msg__ARMarker__Sequence
{
  iviz_msgs__msg__ARMarker * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__ARMarker__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__AR_MARKER__STRUCT_H_
