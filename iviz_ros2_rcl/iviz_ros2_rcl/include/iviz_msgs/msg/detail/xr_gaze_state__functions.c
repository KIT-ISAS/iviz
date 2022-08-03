// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/XRGazeState.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/xr_gaze_state__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `transform`
#include "geometry_msgs/msg/detail/transform__functions.h"

bool
iviz_msgs__msg__XRGazeState__init(iviz_msgs__msg__XRGazeState * msg)
{
  if (!msg) {
    return false;
  }
  // is_valid
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    iviz_msgs__msg__XRGazeState__fini(msg);
    return false;
  }
  // transform
  if (!geometry_msgs__msg__Transform__init(&msg->transform)) {
    iviz_msgs__msg__XRGazeState__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__XRGazeState__fini(iviz_msgs__msg__XRGazeState * msg)
{
  if (!msg) {
    return;
  }
  // is_valid
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // transform
  geometry_msgs__msg__Transform__fini(&msg->transform);
}

bool
iviz_msgs__msg__XRGazeState__are_equal(const iviz_msgs__msg__XRGazeState * lhs, const iviz_msgs__msg__XRGazeState * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // is_valid
  if (lhs->is_valid != rhs->is_valid) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__are_equal(
      &(lhs->header), &(rhs->header)))
  {
    return false;
  }
  // transform
  if (!geometry_msgs__msg__Transform__are_equal(
      &(lhs->transform), &(rhs->transform)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__XRGazeState__copy(
  const iviz_msgs__msg__XRGazeState * input,
  iviz_msgs__msg__XRGazeState * output)
{
  if (!input || !output) {
    return false;
  }
  // is_valid
  output->is_valid = input->is_valid;
  // header
  if (!std_msgs__msg__Header__copy(
      &(input->header), &(output->header)))
  {
    return false;
  }
  // transform
  if (!geometry_msgs__msg__Transform__copy(
      &(input->transform), &(output->transform)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__XRGazeState *
iviz_msgs__msg__XRGazeState__create()
{
  iviz_msgs__msg__XRGazeState * msg = (iviz_msgs__msg__XRGazeState *)malloc(sizeof(iviz_msgs__msg__XRGazeState));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__XRGazeState));
  bool success = iviz_msgs__msg__XRGazeState__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__XRGazeState__destroy(iviz_msgs__msg__XRGazeState * msg)
{
  if (msg) {
    iviz_msgs__msg__XRGazeState__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__XRGazeState__Sequence__init(iviz_msgs__msg__XRGazeState__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__XRGazeState * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__XRGazeState *)calloc(size, sizeof(iviz_msgs__msg__XRGazeState));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__XRGazeState__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__XRGazeState__fini(&data[i - 1]);
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
iviz_msgs__msg__XRGazeState__Sequence__fini(iviz_msgs__msg__XRGazeState__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__XRGazeState__fini(&array->data[i]);
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

iviz_msgs__msg__XRGazeState__Sequence *
iviz_msgs__msg__XRGazeState__Sequence__create(size_t size)
{
  iviz_msgs__msg__XRGazeState__Sequence * array = (iviz_msgs__msg__XRGazeState__Sequence *)malloc(sizeof(iviz_msgs__msg__XRGazeState__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__XRGazeState__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__XRGazeState__Sequence__destroy(iviz_msgs__msg__XRGazeState__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__XRGazeState__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__XRGazeState__Sequence__are_equal(const iviz_msgs__msg__XRGazeState__Sequence * lhs, const iviz_msgs__msg__XRGazeState__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__XRGazeState__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__XRGazeState__Sequence__copy(
  const iviz_msgs__msg__XRGazeState__Sequence * input,
  iviz_msgs__msg__XRGazeState__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__XRGazeState);
    iviz_msgs__msg__XRGazeState * data =
      (iviz_msgs__msg__XRGazeState *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__XRGazeState__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__XRGazeState__fini(&data[i]);
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
    if (!iviz_msgs__msg__XRGazeState__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
