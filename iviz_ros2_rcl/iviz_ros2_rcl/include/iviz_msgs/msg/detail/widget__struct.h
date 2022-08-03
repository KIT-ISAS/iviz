// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Widget.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__WIDGET__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__WIDGET__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

/// Constant 'ACTION_ADD'.
enum
{
  iviz_msgs__msg__Widget__ACTION_ADD = 0
};

/// Constant 'ACTION_REMOVE'.
enum
{
  iviz_msgs__msg__Widget__ACTION_REMOVE = 1
};

/// Constant 'ACTION_REMOVEALL'.
enum
{
  iviz_msgs__msg__Widget__ACTION_REMOVEALL = 2
};

/// Constant 'TYPE_ROTATIONDISC'.
enum
{
  iviz_msgs__msg__Widget__TYPE_ROTATIONDISC = 0
};

/// Constant 'TYPE_SPRINGDISC'.
enum
{
  iviz_msgs__msg__Widget__TYPE_SPRINGDISC = 1
};

/// Constant 'TYPE_SPRINGDISC3D'.
enum
{
  iviz_msgs__msg__Widget__TYPE_SPRINGDISC3D = 2
};

/// Constant 'TYPE_TRAJECTORYDISC'.
enum
{
  iviz_msgs__msg__Widget__TYPE_TRAJECTORYDISC = 3
};

/// Constant 'TYPE_TOOLTIP'.
enum
{
  iviz_msgs__msg__Widget__TYPE_TOOLTIP = 4
};

/// Constant 'TYPE_TARGETAREA'.
enum
{
  iviz_msgs__msg__Widget__TYPE_TARGETAREA = 5
};

/// Constant 'TYPE_POSITIONDISC'.
enum
{
  iviz_msgs__msg__Widget__TYPE_POSITIONDISC = 6
};

/// Constant 'TYPE_POSITIONDISC3D'.
enum
{
  iviz_msgs__msg__Widget__TYPE_POSITIONDISC3D = 7
};

/// Constant 'TYPE_BOUNDARY'.
enum
{
  iviz_msgs__msg__Widget__TYPE_BOUNDARY = 8
};

/// Constant 'TYPE_BOUNDARYCHECK'.
enum
{
  iviz_msgs__msg__Widget__TYPE_BOUNDARYCHECK = 9
};

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.h"
// Member 'id'
// Member 'caption'
#include "rosidl_runtime_c/string.h"
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__struct.h"
// Member 'color'
// Member 'secondary_color'
#include "std_msgs/msg/detail/color_rgba__struct.h"
// Member 'boundary'
#include "iviz_msgs/msg/detail/bounding_box__struct.h"
// Member 'secondary_boundaries'
#include "iviz_msgs/msg/detail/bounding_box_stamped__struct.h"

// Struct defined in msg/Widget in the package iviz_msgs.
typedef struct iviz_msgs__msg__Widget
{
  std_msgs__msg__Header header;
  uint8_t action;
  rosidl_runtime_c__String id;
  uint8_t type;
  geometry_msgs__msg__Pose pose;
  std_msgs__msg__ColorRGBA color;
  std_msgs__msg__ColorRGBA secondary_color;
  double scale;
  double secondary_scale;
  rosidl_runtime_c__String caption;
  iviz_msgs__msg__BoundingBox boundary;
  iviz_msgs__msg__BoundingBoxStamped__Sequence secondary_boundaries;
} iviz_msgs__msg__Widget;

// Struct for a sequence of iviz_msgs__msg__Widget.
typedef struct iviz_msgs__msg__Widget__Sequence
{
  iviz_msgs__msg__Widget * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Widget__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__WIDGET__STRUCT_H_
