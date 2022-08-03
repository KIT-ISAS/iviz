// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Dialog.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/dialog__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `id`
// Member `title`
// Member `caption`
// Member `menu_entries`
#include "rosidl_runtime_c/string_functions.h"
// Member `lifetime`
#include "builtin_interfaces/msg/detail/duration__functions.h"
// Member `background_color`
#include "std_msgs/msg/detail/color_rgba__functions.h"
// Member `tf_offset`
// Member `dialog_displacement`
// Member `tf_displacement`
#include "geometry_msgs/msg/detail/vector3__functions.h"

bool
iviz_msgs__msg__Dialog__init(iviz_msgs__msg__Dialog * msg)
{
  if (!msg) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // action
  // id
  if (!rosidl_runtime_c__String__init(&msg->id)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // lifetime
  if (!builtin_interfaces__msg__Duration__init(&msg->lifetime)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // scale
  // type
  // buttons
  // icon
  // background_color
  if (!std_msgs__msg__ColorRGBA__init(&msg->background_color)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // title
  if (!rosidl_runtime_c__String__init(&msg->title)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // caption
  if (!rosidl_runtime_c__String__init(&msg->caption)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // caption_alignment
  // menu_entries
  if (!rosidl_runtime_c__String__Sequence__init(&msg->menu_entries, 0)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // binding_type
  // tf_offset
  if (!geometry_msgs__msg__Vector3__init(&msg->tf_offset)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // dialog_displacement
  if (!geometry_msgs__msg__Vector3__init(&msg->dialog_displacement)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  // tf_displacement
  if (!geometry_msgs__msg__Vector3__init(&msg->tf_displacement)) {
    iviz_msgs__msg__Dialog__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Dialog__fini(iviz_msgs__msg__Dialog * msg)
{
  if (!msg) {
    return;
  }
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // action
  // id
  rosidl_runtime_c__String__fini(&msg->id);
  // lifetime
  builtin_interfaces__msg__Duration__fini(&msg->lifetime);
  // scale
  // type
  // buttons
  // icon
  // background_color
  std_msgs__msg__ColorRGBA__fini(&msg->background_color);
  // title
  rosidl_runtime_c__String__fini(&msg->title);
  // caption
  rosidl_runtime_c__String__fini(&msg->caption);
  // caption_alignment
  // menu_entries
  rosidl_runtime_c__String__Sequence__fini(&msg->menu_entries);
  // binding_type
  // tf_offset
  geometry_msgs__msg__Vector3__fini(&msg->tf_offset);
  // dialog_displacement
  geometry_msgs__msg__Vector3__fini(&msg->dialog_displacement);
  // tf_displacement
  geometry_msgs__msg__Vector3__fini(&msg->tf_displacement);
}

bool
iviz_msgs__msg__Dialog__are_equal(const iviz_msgs__msg__Dialog * lhs, const iviz_msgs__msg__Dialog * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__are_equal(
      &(lhs->header), &(rhs->header)))
  {
    return false;
  }
  // action
  if (lhs->action != rhs->action) {
    return false;
  }
  // id
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->id), &(rhs->id)))
  {
    return false;
  }
  // lifetime
  if (!builtin_interfaces__msg__Duration__are_equal(
      &(lhs->lifetime), &(rhs->lifetime)))
  {
    return false;
  }
  // scale
  if (lhs->scale != rhs->scale) {
    return false;
  }
  // type
  if (lhs->type != rhs->type) {
    return false;
  }
  // buttons
  if (lhs->buttons != rhs->buttons) {
    return false;
  }
  // icon
  if (lhs->icon != rhs->icon) {
    return false;
  }
  // background_color
  if (!std_msgs__msg__ColorRGBA__are_equal(
      &(lhs->background_color), &(rhs->background_color)))
  {
    return false;
  }
  // title
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->title), &(rhs->title)))
  {
    return false;
  }
  // caption
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->caption), &(rhs->caption)))
  {
    return false;
  }
  // caption_alignment
  if (lhs->caption_alignment != rhs->caption_alignment) {
    return false;
  }
  // menu_entries
  if (!rosidl_runtime_c__String__Sequence__are_equal(
      &(lhs->menu_entries), &(rhs->menu_entries)))
  {
    return false;
  }
  // binding_type
  if (lhs->binding_type != rhs->binding_type) {
    return false;
  }
  // tf_offset
  if (!geometry_msgs__msg__Vector3__are_equal(
      &(lhs->tf_offset), &(rhs->tf_offset)))
  {
    return false;
  }
  // dialog_displacement
  if (!geometry_msgs__msg__Vector3__are_equal(
      &(lhs->dialog_displacement), &(rhs->dialog_displacement)))
  {
    return false;
  }
  // tf_displacement
  if (!geometry_msgs__msg__Vector3__are_equal(
      &(lhs->tf_displacement), &(rhs->tf_displacement)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Dialog__copy(
  const iviz_msgs__msg__Dialog * input,
  iviz_msgs__msg__Dialog * output)
{
  if (!input || !output) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__copy(
      &(input->header), &(output->header)))
  {
    return false;
  }
  // action
  output->action = input->action;
  // id
  if (!rosidl_runtime_c__String__copy(
      &(input->id), &(output->id)))
  {
    return false;
  }
  // lifetime
  if (!builtin_interfaces__msg__Duration__copy(
      &(input->lifetime), &(output->lifetime)))
  {
    return false;
  }
  // scale
  output->scale = input->scale;
  // type
  output->type = input->type;
  // buttons
  output->buttons = input->buttons;
  // icon
  output->icon = input->icon;
  // background_color
  if (!std_msgs__msg__ColorRGBA__copy(
      &(input->background_color), &(output->background_color)))
  {
    return false;
  }
  // title
  if (!rosidl_runtime_c__String__copy(
      &(input->title), &(output->title)))
  {
    return false;
  }
  // caption
  if (!rosidl_runtime_c__String__copy(
      &(input->caption), &(output->caption)))
  {
    return false;
  }
  // caption_alignment
  output->caption_alignment = input->caption_alignment;
  // menu_entries
  if (!rosidl_runtime_c__String__Sequence__copy(
      &(input->menu_entries), &(output->menu_entries)))
  {
    return false;
  }
  // binding_type
  output->binding_type = input->binding_type;
  // tf_offset
  if (!geometry_msgs__msg__Vector3__copy(
      &(input->tf_offset), &(output->tf_offset)))
  {
    return false;
  }
  // dialog_displacement
  if (!geometry_msgs__msg__Vector3__copy(
      &(input->dialog_displacement), &(output->dialog_displacement)))
  {
    return false;
  }
  // tf_displacement
  if (!geometry_msgs__msg__Vector3__copy(
      &(input->tf_displacement), &(output->tf_displacement)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Dialog *
iviz_msgs__msg__Dialog__create()
{
  iviz_msgs__msg__Dialog * msg = (iviz_msgs__msg__Dialog *)malloc(sizeof(iviz_msgs__msg__Dialog));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Dialog));
  bool success = iviz_msgs__msg__Dialog__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Dialog__destroy(iviz_msgs__msg__Dialog * msg)
{
  if (msg) {
    iviz_msgs__msg__Dialog__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Dialog__Sequence__init(iviz_msgs__msg__Dialog__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Dialog * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Dialog *)calloc(size, sizeof(iviz_msgs__msg__Dialog));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Dialog__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Dialog__fini(&data[i - 1]);
      }
      free(data);
      return false;
    }
  }
  array->data = data;
  array->size = size;
  array->capacity = size;
  return true;
}

void
iviz_msgs__msg__Dialog__Sequence__fini(iviz_msgs__msg__Dialog__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Dialog__fini(&array->data[i]);
    }
    free(array->data);
    array->data = NULL;
    array->size = 0;
    array->capacity = 0;
  } else {
    // ensure that data, size, and capacity values are consistent
    assert(0 == array->size);
    assert(0 == array->capacity);
  }
}

iviz_msgs__msg__Dialog__Sequence *
iviz_msgs__msg__Dialog__Sequence__create(size_t size)
{
  iviz_msgs__msg__Dialog__Sequence * array = (iviz_msgs__msg__Dialog__Sequence *)malloc(sizeof(iviz_msgs__msg__Dialog__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Dialog__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Dialog__Sequence__destroy(iviz_msgs__msg__Dialog__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Dialog__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Dialog__Sequence__are_equal(const iviz_msgs__msg__Dialog__Sequence * lhs, const iviz_msgs__msg__Dialog__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Dialog__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Dialog__Sequence__copy(
  const iviz_msgs__msg__Dialog__Sequence * input,
  iviz_msgs__msg__Dialog__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Dialog);
    iviz_msgs__msg__Dialog * data =
      (iviz_msgs__msg__Dialog *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Dialog__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Dialog__fini(&data[i]);
        }
        free(data);
        return false;
      }
    }
    output->data = data;
    output->capacity = input->size;
  }
  output->size = input->size;
  for (size_t i = 0; i < input->size; ++i) {
    if (!iviz_msgs__msg__Dialog__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
