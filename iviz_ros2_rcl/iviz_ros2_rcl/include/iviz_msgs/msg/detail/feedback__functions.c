// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Feedback.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/feedback__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `viz_id`
// Member `id`
#include "rosidl_runtime_c/string_functions.h"
// Member `position`
#include "geometry_msgs/msg/detail/point__functions.h"
// Member `orientation`
#include "geometry_msgs/msg/detail/quaternion__functions.h"
// Member `scale`
#include "geometry_msgs/msg/detail/vector3__functions.h"
// Member `trajectory`
#include "iviz_msgs/msg/detail/trajectory__functions.h"

bool
iviz_msgs__msg__Feedback__init(iviz_msgs__msg__Feedback * msg)
{
  if (!msg) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    iviz_msgs__msg__Feedback__fini(msg);
    return false;
  }
  // viz_id
  if (!rosidl_runtime_c__String__init(&msg->viz_id)) {
    iviz_msgs__msg__Feedback__fini(msg);
    return false;
  }
  // id
  if (!rosidl_runtime_c__String__init(&msg->id)) {
    iviz_msgs__msg__Feedback__fini(msg);
    return false;
  }
  // type
  // entry_id
  // angle
  // position
  if (!geometry_msgs__msg__Point__init(&msg->position)) {
    iviz_msgs__msg__Feedback__fini(msg);
    return false;
  }
  // orientation
  if (!geometry_msgs__msg__Quaternion__init(&msg->orientation)) {
    iviz_msgs__msg__Feedback__fini(msg);
    return false;
  }
  // scale
  if (!geometry_msgs__msg__Vector3__init(&msg->scale)) {
    iviz_msgs__msg__Feedback__fini(msg);
    return false;
  }
  // trajectory
  if (!iviz_msgs__msg__Trajectory__init(&msg->trajectory)) {
    iviz_msgs__msg__Feedback__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Feedback__fini(iviz_msgs__msg__Feedback * msg)
{
  if (!msg) {
    return;
  }
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // viz_id
  rosidl_runtime_c__String__fini(&msg->viz_id);
  // id
  rosidl_runtime_c__String__fini(&msg->id);
  // type
  // entry_id
  // angle
  // position
  geometry_msgs__msg__Point__fini(&msg->position);
  // orientation
  geometry_msgs__msg__Quaternion__fini(&msg->orientation);
  // scale
  geometry_msgs__msg__Vector3__fini(&msg->scale);
  // trajectory
  iviz_msgs__msg__Trajectory__fini(&msg->trajectory);
}

bool
iviz_msgs__msg__Feedback__are_equal(const iviz_msgs__msg__Feedback * lhs, const iviz_msgs__msg__Feedback * rhs)
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
  // viz_id
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->viz_id), &(rhs->viz_id)))
  {
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
  // entry_id
  if (lhs->entry_id != rhs->entry_id) {
    return false;
  }
  // angle
  if (lhs->angle != rhs->angle) {
    return false;
  }
  // position
  if (!geometry_msgs__msg__Point__are_equal(
      &(lhs->position), &(rhs->position)))
  {
    return false;
  }
  // orientation
  if (!geometry_msgs__msg__Quaternion__are_equal(
      &(lhs->orientation), &(rhs->orientation)))
  {
    return false;
  }
  // scale
  if (!geometry_msgs__msg__Vector3__are_equal(
      &(lhs->scale), &(rhs->scale)))
  {
    return false;
  }
  // trajectory
  if (!iviz_msgs__msg__Trajectory__are_equal(
      &(lhs->trajectory), &(rhs->trajectory)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Feedback__copy(
  const iviz_msgs__msg__Feedback * input,
  iviz_msgs__msg__Feedback * output)
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
  // viz_id
  if (!rosidl_runtime_c__String__copy(
      &(input->viz_id), &(output->viz_id)))
  {
    return false;
  }
  // id
  if (!rosidl_runtime_c__String__copy(
      &(input->id), &(output->id)))
  {
    return false;
  }
  // type
  output->type = input->type;
  // entry_id
  output->entry_id = input->entry_id;
  // angle
  output->angle = input->angle;
  // position
  if (!geometry_msgs__msg__Point__copy(
      &(input->position), &(output->position)))
  {
    return false;
  }
  // orientation
  if (!geometry_msgs__msg__Quaternion__copy(
      &(input->orientation), &(output->orientation)))
  {
    return false;
  }
  // scale
  if (!geometry_msgs__msg__Vector3__copy(
      &(input->scale), &(output->scale)))
  {
    return false;
  }
  // trajectory
  if (!iviz_msgs__msg__Trajectory__copy(
      &(input->trajectory), &(output->trajectory)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Feedback *
iviz_msgs__msg__Feedback__create()
{
  iviz_msgs__msg__Feedback * msg = (iviz_msgs__msg__Feedback *)malloc(sizeof(iviz_msgs__msg__Feedback));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Feedback));
  bool success = iviz_msgs__msg__Feedback__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Feedback__destroy(iviz_msgs__msg__Feedback * msg)
{
  if (msg) {
    iviz_msgs__msg__Feedback__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Feedback__Sequence__init(iviz_msgs__msg__Feedback__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Feedback * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Feedback *)calloc(size, sizeof(iviz_msgs__msg__Feedback));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Feedback__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Feedback__fini(&data[i - 1]);
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
iviz_msgs__msg__Feedback__Sequence__fini(iviz_msgs__msg__Feedback__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Feedback__fini(&array->data[i]);
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

iviz_msgs__msg__Feedback__Sequence *
iviz_msgs__msg__Feedback__Sequence__create(size_t size)
{
  iviz_msgs__msg__Feedback__Sequence * array = (iviz_msgs__msg__Feedback__Sequence *)malloc(sizeof(iviz_msgs__msg__Feedback__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Feedback__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Feedback__Sequence__destroy(iviz_msgs__msg__Feedback__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Feedback__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Feedback__Sequence__are_equal(const iviz_msgs__msg__Feedback__Sequence * lhs, const iviz_msgs__msg__Feedback__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Feedback__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Feedback__Sequence__copy(
  const iviz_msgs__msg__Feedback__Sequence * input,
  iviz_msgs__msg__Feedback__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Feedback);
    iviz_msgs__msg__Feedback * data =
      (iviz_msgs__msg__Feedback *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Feedback__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Feedback__fini(&data[i]);
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
    if (!iviz_msgs__msg__Feedback__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
