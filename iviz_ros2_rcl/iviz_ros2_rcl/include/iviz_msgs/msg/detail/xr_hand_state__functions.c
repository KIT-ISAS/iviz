// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/XRHandState.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/xr_hand_state__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `palm`
// Member `thumb`
// Member `index`
// Member `middle`
// Member `ring`
// Member `little`
#include "geometry_msgs/msg/detail/transform__functions.h"

bool
iviz_msgs__msg__XRHandState__init(iviz_msgs__msg__XRHandState * msg)
{
  if (!msg) {
    return false;
  }
  // is_valid
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    iviz_msgs__msg__XRHandState__fini(msg);
    return false;
  }
  // palm
  if (!geometry_msgs__msg__Transform__init(&msg->palm)) {
    iviz_msgs__msg__XRHandState__fini(msg);
    return false;
  }
  // thumb
  if (!geometry_msgs__msg__Transform__Sequence__init(&msg->thumb, 0)) {
    iviz_msgs__msg__XRHandState__fini(msg);
    return false;
  }
  // index
  if (!geometry_msgs__msg__Transform__Sequence__init(&msg->index, 0)) {
    iviz_msgs__msg__XRHandState__fini(msg);
    return false;
  }
  // middle
  if (!geometry_msgs__msg__Transform__Sequence__init(&msg->middle, 0)) {
    iviz_msgs__msg__XRHandState__fini(msg);
    return false;
  }
  // ring
  if (!geometry_msgs__msg__Transform__Sequence__init(&msg->ring, 0)) {
    iviz_msgs__msg__XRHandState__fini(msg);
    return false;
  }
  // little
  if (!geometry_msgs__msg__Transform__Sequence__init(&msg->little, 0)) {
    iviz_msgs__msg__XRHandState__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__XRHandState__fini(iviz_msgs__msg__XRHandState * msg)
{
  if (!msg) {
    return;
  }
  // is_valid
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // palm
  geometry_msgs__msg__Transform__fini(&msg->palm);
  // thumb
  geometry_msgs__msg__Transform__Sequence__fini(&msg->thumb);
  // index
  geometry_msgs__msg__Transform__Sequence__fini(&msg->index);
  // middle
  geometry_msgs__msg__Transform__Sequence__fini(&msg->middle);
  // ring
  geometry_msgs__msg__Transform__Sequence__fini(&msg->ring);
  // little
  geometry_msgs__msg__Transform__Sequence__fini(&msg->little);
}

bool
iviz_msgs__msg__XRHandState__are_equal(const iviz_msgs__msg__XRHandState * lhs, const iviz_msgs__msg__XRHandState * rhs)
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
  // palm
  if (!geometry_msgs__msg__Transform__are_equal(
      &(lhs->palm), &(rhs->palm)))
  {
    return false;
  }
  // thumb
  if (!geometry_msgs__msg__Transform__Sequence__are_equal(
      &(lhs->thumb), &(rhs->thumb)))
  {
    return false;
  }
  // index
  if (!geometry_msgs__msg__Transform__Sequence__are_equal(
      &(lhs->index), &(rhs->index)))
  {
    return false;
  }
  // middle
  if (!geometry_msgs__msg__Transform__Sequence__are_equal(
      &(lhs->middle), &(rhs->middle)))
  {
    return false;
  }
  // ring
  if (!geometry_msgs__msg__Transform__Sequence__are_equal(
      &(lhs->ring), &(rhs->ring)))
  {
    return false;
  }
  // little
  if (!geometry_msgs__msg__Transform__Sequence__are_equal(
      &(lhs->little), &(rhs->little)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__XRHandState__copy(
  const iviz_msgs__msg__XRHandState * input,
  iviz_msgs__msg__XRHandState * output)
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
  // palm
  if (!geometry_msgs__msg__Transform__copy(
      &(input->palm), &(output->palm)))
  {
    return false;
  }
  // thumb
  if (!geometry_msgs__msg__Transform__Sequence__copy(
      &(input->thumb), &(output->thumb)))
  {
    return false;
  }
  // index
  if (!geometry_msgs__msg__Transform__Sequence__copy(
      &(input->index), &(output->index)))
  {
    return false;
  }
  // middle
  if (!geometry_msgs__msg__Transform__Sequence__copy(
      &(input->middle), &(output->middle)))
  {
    return false;
  }
  // ring
  if (!geometry_msgs__msg__Transform__Sequence__copy(
      &(input->ring), &(output->ring)))
  {
    return false;
  }
  // little
  if (!geometry_msgs__msg__Transform__Sequence__copy(
      &(input->little), &(output->little)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__XRHandState *
iviz_msgs__msg__XRHandState__create()
{
  iviz_msgs__msg__XRHandState * msg = (iviz_msgs__msg__XRHandState *)malloc(sizeof(iviz_msgs__msg__XRHandState));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__XRHandState));
  bool success = iviz_msgs__msg__XRHandState__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__XRHandState__destroy(iviz_msgs__msg__XRHandState * msg)
{
  if (msg) {
    iviz_msgs__msg__XRHandState__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__XRHandState__Sequence__init(iviz_msgs__msg__XRHandState__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__XRHandState * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__XRHandState *)calloc(size, sizeof(iviz_msgs__msg__XRHandState));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__XRHandState__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__XRHandState__fini(&data[i - 1]);
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
iviz_msgs__msg__XRHandState__Sequence__fini(iviz_msgs__msg__XRHandState__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__XRHandState__fini(&array->data[i]);
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

iviz_msgs__msg__XRHandState__Sequence *
iviz_msgs__msg__XRHandState__Sequence__create(size_t size)
{
  iviz_msgs__msg__XRHandState__Sequence * array = (iviz_msgs__msg__XRHandState__Sequence *)malloc(sizeof(iviz_msgs__msg__XRHandState__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__XRHandState__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__XRHandState__Sequence__destroy(iviz_msgs__msg__XRHandState__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__XRHandState__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__XRHandState__Sequence__are_equal(const iviz_msgs__msg__XRHandState__Sequence * lhs, const iviz_msgs__msg__XRHandState__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__XRHandState__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__XRHandState__Sequence__copy(
  const iviz_msgs__msg__XRHandState__Sequence * input,
  iviz_msgs__msg__XRHandState__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__XRHandState);
    iviz_msgs__msg__XRHandState * data =
      (iviz_msgs__msg__XRHandState *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__XRHandState__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__XRHandState__fini(&data[i]);
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
    if (!iviz_msgs__msg__XRHandState__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
