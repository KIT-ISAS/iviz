// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Dialog.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__DIALOG__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__DIALOG__STRUCT_H_

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
  iviz_msgs__msg__Dialog__ACTION_ADD = 0
};

/// Constant 'ACTION_REMOVE'.
enum
{
  iviz_msgs__msg__Dialog__ACTION_REMOVE = 1
};

/// Constant 'ACTION_REMOVEALL'.
enum
{
  iviz_msgs__msg__Dialog__ACTION_REMOVEALL = 2
};

/// Constant 'TYPE_PLAIN'.
enum
{
  iviz_msgs__msg__Dialog__TYPE_PLAIN = 0
};

/// Constant 'TYPE_SHORT'.
enum
{
  iviz_msgs__msg__Dialog__TYPE_SHORT = 1
};

/// Constant 'TYPE_NOTICE'.
enum
{
  iviz_msgs__msg__Dialog__TYPE_NOTICE = 2
};

/// Constant 'TYPE_MENU'.
enum
{
  iviz_msgs__msg__Dialog__TYPE_MENU = 3
};

/// Constant 'TYPE_BUTTON'.
enum
{
  iviz_msgs__msg__Dialog__TYPE_BUTTON = 4
};

/// Constant 'TYPE_ICON'.
enum
{
  iviz_msgs__msg__Dialog__TYPE_ICON = 5
};

/// Constant 'BUTTONS_OK'.
enum
{
  iviz_msgs__msg__Dialog__BUTTONS_OK = 0
};

/// Constant 'BUTTONS_YESNO'.
enum
{
  iviz_msgs__msg__Dialog__BUTTONS_YESNO = 1
};

/// Constant 'BUTTONS_OKCANCEL'.
enum
{
  iviz_msgs__msg__Dialog__BUTTONS_OKCANCEL = 2
};

/// Constant 'BUTTONS_FORWARD'.
enum
{
  iviz_msgs__msg__Dialog__BUTTONS_FORWARD = 3
};

/// Constant 'BUTTONS_FORWARDBACKWARD'.
enum
{
  iviz_msgs__msg__Dialog__BUTTONS_FORWARDBACKWARD = 4
};

/// Constant 'BUTTONS_BACKWARD'.
enum
{
  iviz_msgs__msg__Dialog__BUTTONS_BACKWARD = 5
};

/// Constant 'ICON_NONE'.
enum
{
  iviz_msgs__msg__Dialog__ICON_NONE = 0
};

/// Constant 'ICON_CROSS'.
enum
{
  iviz_msgs__msg__Dialog__ICON_CROSS = 1
};

/// Constant 'ICON_OK'.
enum
{
  iviz_msgs__msg__Dialog__ICON_OK = 2
};

/// Constant 'ICON_FORWARD'.
enum
{
  iviz_msgs__msg__Dialog__ICON_FORWARD = 3
};

/// Constant 'ICON_BACKWARD'.
enum
{
  iviz_msgs__msg__Dialog__ICON_BACKWARD = 4
};

/// Constant 'ICON_DIALOG'.
enum
{
  iviz_msgs__msg__Dialog__ICON_DIALOG = 5
};

/// Constant 'ICON_UP'.
enum
{
  iviz_msgs__msg__Dialog__ICON_UP = 6
};

/// Constant 'ICON_DOWN'.
enum
{
  iviz_msgs__msg__Dialog__ICON_DOWN = 7
};

/// Constant 'ICON_INFO'.
enum
{
  iviz_msgs__msg__Dialog__ICON_INFO = 8
};

/// Constant 'ICON_WARN'.
enum
{
  iviz_msgs__msg__Dialog__ICON_WARN = 9
};

/// Constant 'ICON_ERROR'.
enum
{
  iviz_msgs__msg__Dialog__ICON_ERROR = 10
};

/// Constant 'ICON_DIALOGS'.
enum
{
  iviz_msgs__msg__Dialog__ICON_DIALOGS = 11
};

/// Constant 'ICON_QUESTION'.
enum
{
  iviz_msgs__msg__Dialog__ICON_QUESTION = 12
};

/// Constant 'ALIGNMENT_DEFAULT'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_DEFAULT = 0
};

/// Constant 'ALIGNMENT_LEFT'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_LEFT = 1
};

/// Constant 'ALIGNMENT_CENTER'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_CENTER = 2
};

/// Constant 'ALIGNMENT_RIGHT'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_RIGHT = 4
};

/// Constant 'ALIGNMENT_JUSTIFIED'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_JUSTIFIED = 8
};

/// Constant 'ALIGNMENT_FLUSH'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_FLUSH = 16
};

/// Constant 'ALIGNMENT_GEOMETRYCENTER'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_GEOMETRYCENTER = 32
};

/// Constant 'ALIGNMENT_TOP'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_TOP = 256
};

/// Constant 'ALIGNMENT_MID'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_MID = 512
};

/// Constant 'ALIGNMENT_BOTTOM'.
enum
{
  iviz_msgs__msg__Dialog__ALIGNMENT_BOTTOM = 1024
};

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.h"
// Member 'id'
// Member 'title'
// Member 'caption'
// Member 'menu_entries'
#include "rosidl_runtime_c/string.h"
// Member 'lifetime'
#include "builtin_interfaces/msg/detail/duration__struct.h"
// Member 'background_color'
#include "std_msgs/msg/detail/color_rgba__struct.h"
// Member 'tf_offset'
// Member 'dialog_displacement'
// Member 'tf_displacement'
#include "geometry_msgs/msg/detail/vector3__struct.h"

// Struct defined in msg/Dialog in the package iviz_msgs.
typedef struct iviz_msgs__msg__Dialog
{
  std_msgs__msg__Header header;
  uint8_t action;
  rosidl_runtime_c__String id;
  builtin_interfaces__msg__Duration lifetime;
  double scale;
  uint8_t type;
  uint8_t buttons;
  uint8_t icon;
  std_msgs__msg__ColorRGBA background_color;
  rosidl_runtime_c__String title;
  rosidl_runtime_c__String caption;
  uint16_t caption_alignment;
  rosidl_runtime_c__String__Sequence menu_entries;
  uint8_t binding_type;
  geometry_msgs__msg__Vector3 tf_offset;
  geometry_msgs__msg__Vector3 dialog_displacement;
  geometry_msgs__msg__Vector3 tf_displacement;
} iviz_msgs__msg__Dialog;

// Struct for a sequence of iviz_msgs__msg__Dialog.
typedef struct iviz_msgs__msg__Dialog__Sequence
{
  iviz_msgs__msg__Dialog * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Dialog__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__DIALOG__STRUCT_H_
