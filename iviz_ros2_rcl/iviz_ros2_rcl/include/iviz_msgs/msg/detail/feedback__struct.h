// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Feedback.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__FEEDBACK__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__FEEDBACK__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

/// Constant 'TYPE_EXPIRED'.
enum
{
  iviz_msgs__msg__Feedback__TYPE_EXPIRED = 0
};

/// Constant 'TYPE_BUTTON_CLICK'.
enum
{
  iviz_msgs__msg__Feedback__TYPE_BUTTON_CLICK = 1
};

/// Constant 'TYPE_MENUENTRY_CLICK'.
enum
{
  iviz_msgs__msg__Feedback__TYPE_MENUENTRY_CLICK = 2
};

/// Constant 'TYPE_POSITION_CHANGED'.
enum
{
  iviz_msgs__msg__Feedback__TYPE_POSITION_CHANGED = 3
};

/// Constant 'TYPE_ORIENTATION_CHANGED'.
enum
{
  iviz_msgs__msg__Feedback__TYPE_ORIENTATION_CHANGED = 4
};

/// Constant 'TYPE_SCALE_CHANGED'.
enum
{
  iviz_msgs__msg__Feedback__TYPE_SCALE_CHANGED = 5
};

/// Constant 'TYPE_TRAJECTORY_CHANGED'.
enum
{
  iviz_msgs__msg__Feedback__TYPE_TRAJECTORY_CHANGED = 6
};

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.h"
// Member 'viz_id'
// Member 'id'
#include "rosidl_runtime_c/string.h"
// Member 'position'
#include "geometry_msgs/msg/detail/point__struct.h"
// Member 'orientation'
#include "geometry_msgs/msg/detail/quaternion__struct.h"
// Member 'scale'
#include "geometry_msgs/msg/detail/vector3__struct.h"
// Member 'trajectory'
#include "iviz_msgs/msg/detail/trajectory__struct.h"

// Struct defined in msg/Feedback in the package iviz_msgs.
typedef struct iviz_msgs__msg__Feedback
{
  std_msgs__msg__Header header;
  rosidl_runtime_c__String viz_id;
  rosidl_runtime_c__String id;
  uint8_t type;
  int32_t entry_id;
  double angle;
  geometry_msgs__msg__Point position;
  geometry_msgs__msg__Quaternion orientation;
  geometry_msgs__msg__Vector3 scale;
  iviz_msgs__msg__Trajectory trajectory;
} iviz_msgs__msg__Feedback;

// Struct for a sequence of iviz_msgs__msg__Feedback.
typedef struct iviz_msgs__msg__Feedback__Sequence
{
  iviz_msgs__msg__Feedback * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Feedback__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__FEEDBACK__STRUCT_H_
