// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/WidgetArray.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'dialogs'
#include "iviz_msgs/msg/detail/dialog__struct.h"
// Member 'widgets'
#include "iviz_msgs/msg/detail/widget__struct.h"

// Struct defined in msg/WidgetArray in the package iviz_msgs.
typedef struct iviz_msgs__msg__WidgetArray
{
  iviz_msgs__msg__Dialog__Sequence dialogs;
  iviz_msgs__msg__Widget__Sequence widgets;
} iviz_msgs__msg__WidgetArray;

// Struct for a sequence of iviz_msgs__msg__WidgetArray.
typedef struct iviz_msgs__msg__WidgetArray__Sequence
{
  iviz_msgs__msg__WidgetArray * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__WidgetArray__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__STRUCT_H_
