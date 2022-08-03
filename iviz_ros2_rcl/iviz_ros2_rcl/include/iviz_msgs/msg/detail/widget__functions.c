// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Widget.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/widget__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `id`
// Member `caption`
#include "rosidl_runtime_c/string_functions.h"
// Member `pose`
#include "geometry_msgs/msg/detail/pose__functions.h"
// Member `color`
// Member `secondary_color`
#include "std_msgs/msg/detail/color_rgba__functions.h"
// Member `boundary`
#include "iviz_msgs/msg/detail/bounding_box__functions.h"
// Member `secondary_boundaries`
#include "iviz_msgs/msg/detail/bounding_box_stamped__functions.h"

bool
iviz_msgs__msg__Widget__init(iviz_msgs__msg__Widget * msg)
{
  if (!msg) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  // action
  // id
  if (!rosidl_runtime_c__String__init(&msg->id)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  // type
  // pose
  if (!geometry_msgs__msg__Pose__init(&msg->pose)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  // color
  if (!std_msgs__msg__ColorRGBA__init(&msg->color)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  // secondary_color
  if (!std_msgs__msg__ColorRGBA__init(&msg->secondary_color)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  // scale
  // secondary_scale
  // caption
  if (!rosidl_runtime_c__String__init(&msg->caption)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  // boundary
  if (!iviz_msgs__msg__BoundingBox__init(&msg->boundary)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  // secondary_boundaries
  if (!iviz_msgs__msg__BoundingBoxStamped__Sequence__init(&msg->secondary_boundaries, 0)) {
    iviz_msgs__msg__Widget__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Widget__fini(iviz_msgs__msg__Widget * msg)
{
  if (!msg) {
    return;
  }
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // action
  // id
  rosidl_runtime_c__String__fini(&msg->id);
  // type
  // pose
  geometry_msgs__msg__Pose__fini(&msg->pose);
  // color
  std_msgs__msg__ColorRGBA__fini(&msg->color);
  // secondary_color
  std_msgs__msg__ColorRGBA__fini(&msg->secondary_color);
  // scale
  // secondary_scale
  // caption
  rosidl_runtime_c__String__fini(&msg->caption);
  // boundary
  iviz_msgs__msg__BoundingBox__fini(&msg->boundary);
  // secondary_boundaries
  iviz_msgs__msg__BoundingBoxStamped__Sequence__fini(&msg->secondary_boundaries);
}

bool
iviz_msgs__msg__Widget__are_equal(const iviz_msgs__msg__Widget * lhs, const iviz_msgs__msg__Widget * rhs)
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
  // type
  if (lhs->type != rhs->type) {
    return false;
  }
  // pose
  if (!geometry_msgs__msg__Pose__are_equal(
      &(lhs->pose), &(rhs->pose)))
  {
    return false;
  }
  // color
  if (!std_msgs__msg__ColorRGBA__are_equal(
      &(lhs->color), &(rhs->color)))
  {
    return false;
  }
  // secondary_color
  if (!std_msgs__msg__ColorRGBA__are_equal(
      &(lhs->secondary_color), &(rhs->secondary_color)))
  {
    return false;
  }
  // scale
  if (lhs->scale != rhs->scale) {
    return false;
  }
  // secondary_scale
  if (lhs->secondary_scale != rhs->secondary_scale) {
    return false;
  }
  // caption
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->caption), &(rhs->caption)))
  {
    return false;
  }
  // boundary
  if (!iviz_msgs__msg__BoundingBox__are_equal(
      &(lhs->boundary), &(rhs->boundary)))
  {
    return false;
  }
  // secondary_boundaries
  if (!iviz_msgs__msg__BoundingBoxStamped__Sequence__are_equal(
      &(lhs->secondary_boundaries), &(rhs->secondary_boundaries)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Widget__copy(
  const iviz_msgs__msg__Widget * input,
  iviz_msgs__msg__Widget * output)
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
  // type
  output->type = input->type;
  // pose
  if (!geometry_msgs__msg__Pose__copy(
      &(input->pose), &(output->pose)))
  {
    return false;
  }
  // color
  if (!std_msgs__msg__ColorRGBA__copy(
      &(input->color), &(output->color)))
  {
    return false;
  }
  // secondary_color
  if (!std_msgs__msg__ColorRGBA__copy(
      &(input->secondary_color), &(output->secondary_color)))
  {
    return false;
  }
  // scale
  output->scale = input->scale;
  // secondary_scale
  output->secondary_scale = input->secondary_scale;
  // caption
  if (!rosidl_runtime_c__String__copy(
      &(input->caption), &(output->caption)))
  {
    return false;
  }
  // boundary
  if (!iviz_msgs__msg__BoundingBox__copy(
      &(input->boundary), &(output->boundary)))
  {
    return false;
  }
  // secondary_boundaries
  if (!iviz_msgs__msg__BoundingBoxStamped__Sequence__copy(
      &(input->secondary_boundaries), &(output->secondary_boundaries)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Widget *
iviz_msgs__msg__Widget__create()
{
  iviz_msgs__msg__Widget * msg = (iviz_msgs__msg__Widget *)malloc(sizeof(iviz_msgs__msg__Widget));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Widget));
  bool success = iviz_msgs__msg__Widget__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Widget__destroy(iviz_msgs__msg__Widget * msg)
{
  if (msg) {
    iviz_msgs__msg__Widget__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Widget__Sequence__init(iviz_msgs__msg__Widget__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Widget * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Widget *)calloc(size, sizeof(iviz_msgs__msg__Widget));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Widget__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Widget__fini(&data[i - 1]);
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
iviz_msgs__msg__Widget__Sequence__fini(iviz_msgs__msg__Widget__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Widget__fini(&array->data[i]);
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

iviz_msgs__msg__Widget__Sequence *
iviz_msgs__msg__Widget__Sequence__create(size_t size)
{
  iviz_msgs__msg__Widget__Sequence * array = (iviz_msgs__msg__Widget__Sequence *)malloc(sizeof(iviz_msgs__msg__Widget__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Widget__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Widget__Sequence__destroy(iviz_msgs__msg__Widget__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Widget__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Widget__Sequence__are_equal(const iviz_msgs__msg__Widget__Sequence * lhs, const iviz_msgs__msg__Widget__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Widget__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Widget__Sequence__copy(
  const iviz_msgs__msg__Widget__Sequence * input,
  iviz_msgs__msg__Widget__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Widget);
    iviz_msgs__msg__Widget * data =
      (iviz_msgs__msg__Widget *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Widget__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Widget__fini(&data[i]);
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
    if (!iviz_msgs__msg__Widget__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
