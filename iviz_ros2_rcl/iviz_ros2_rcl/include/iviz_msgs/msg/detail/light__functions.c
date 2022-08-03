// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Light.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/light__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `name`
#include "rosidl_runtime_c/string_functions.h"
// Member `diffuse`
#include "iviz_msgs/msg/detail/color32__functions.h"
// Member `position`
// Member `direction`
#include "iviz_msgs/msg/detail/vector3f__functions.h"

bool
iviz_msgs__msg__Light__init(iviz_msgs__msg__Light * msg)
{
  if (!msg) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__init(&msg->name)) {
    iviz_msgs__msg__Light__fini(msg);
    return false;
  }
  // type
  // cast_shadows
  // diffuse
  if (!iviz_msgs__msg__Color32__init(&msg->diffuse)) {
    iviz_msgs__msg__Light__fini(msg);
    return false;
  }
  // range
  // position
  if (!iviz_msgs__msg__Vector3f__init(&msg->position)) {
    iviz_msgs__msg__Light__fini(msg);
    return false;
  }
  // direction
  if (!iviz_msgs__msg__Vector3f__init(&msg->direction)) {
    iviz_msgs__msg__Light__fini(msg);
    return false;
  }
  // inner_angle
  // outer_angle
  return true;
}

void
iviz_msgs__msg__Light__fini(iviz_msgs__msg__Light * msg)
{
  if (!msg) {
    return;
  }
  // name
  rosidl_runtime_c__String__fini(&msg->name);
  // type
  // cast_shadows
  // diffuse
  iviz_msgs__msg__Color32__fini(&msg->diffuse);
  // range
  // position
  iviz_msgs__msg__Vector3f__fini(&msg->position);
  // direction
  iviz_msgs__msg__Vector3f__fini(&msg->direction);
  // inner_angle
  // outer_angle
}

bool
iviz_msgs__msg__Light__are_equal(const iviz_msgs__msg__Light * lhs, const iviz_msgs__msg__Light * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->name), &(rhs->name)))
  {
    return false;
  }
  // type
  if (lhs->type != rhs->type) {
    return false;
  }
  // cast_shadows
  if (lhs->cast_shadows != rhs->cast_shadows) {
    return false;
  }
  // diffuse
  if (!iviz_msgs__msg__Color32__are_equal(
      &(lhs->diffuse), &(rhs->diffuse)))
  {
    return false;
  }
  // range
  if (lhs->range != rhs->range) {
    return false;
  }
  // position
  if (!iviz_msgs__msg__Vector3f__are_equal(
      &(lhs->position), &(rhs->position)))
  {
    return false;
  }
  // direction
  if (!iviz_msgs__msg__Vector3f__are_equal(
      &(lhs->direction), &(rhs->direction)))
  {
    return false;
  }
  // inner_angle
  if (lhs->inner_angle != rhs->inner_angle) {
    return false;
  }
  // outer_angle
  if (lhs->outer_angle != rhs->outer_angle) {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Light__copy(
  const iviz_msgs__msg__Light * input,
  iviz_msgs__msg__Light * output)
{
  if (!input || !output) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__copy(
      &(input->name), &(output->name)))
  {
    return false;
  }
  // type
  output->type = input->type;
  // cast_shadows
  output->cast_shadows = input->cast_shadows;
  // diffuse
  if (!iviz_msgs__msg__Color32__copy(
      &(input->diffuse), &(output->diffuse)))
  {
    return false;
  }
  // range
  output->range = input->range;
  // position
  if (!iviz_msgs__msg__Vector3f__copy(
      &(input->position), &(output->position)))
  {
    return false;
  }
  // direction
  if (!iviz_msgs__msg__Vector3f__copy(
      &(input->direction), &(output->direction)))
  {
    return false;
  }
  // inner_angle
  output->inner_angle = input->inner_angle;
  // outer_angle
  output->outer_angle = input->outer_angle;
  return true;
}

iviz_msgs__msg__Light *
iviz_msgs__msg__Light__create()
{
  iviz_msgs__msg__Light * msg = (iviz_msgs__msg__Light *)malloc(sizeof(iviz_msgs__msg__Light));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Light));
  bool success = iviz_msgs__msg__Light__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Light__destroy(iviz_msgs__msg__Light * msg)
{
  if (msg) {
    iviz_msgs__msg__Light__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Light__Sequence__init(iviz_msgs__msg__Light__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Light * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Light *)calloc(size, sizeof(iviz_msgs__msg__Light));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Light__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Light__fini(&data[i - 1]);
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
iviz_msgs__msg__Light__Sequence__fini(iviz_msgs__msg__Light__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Light__fini(&array->data[i]);
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

iviz_msgs__msg__Light__Sequence *
iviz_msgs__msg__Light__Sequence__create(size_t size)
{
  iviz_msgs__msg__Light__Sequence * array = (iviz_msgs__msg__Light__Sequence *)malloc(sizeof(iviz_msgs__msg__Light__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Light__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Light__Sequence__destroy(iviz_msgs__msg__Light__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Light__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Light__Sequence__are_equal(const iviz_msgs__msg__Light__Sequence * lhs, const iviz_msgs__msg__Light__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Light__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Light__Sequence__copy(
  const iviz_msgs__msg__Light__Sequence * input,
  iviz_msgs__msg__Light__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Light);
    iviz_msgs__msg__Light * data =
      (iviz_msgs__msg__Light *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Light__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Light__fini(&data[i]);
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
    if (!iviz_msgs__msg__Light__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
