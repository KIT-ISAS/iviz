// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from lifecycle_msgs:msg/State.idl
// generated code does not contain a copyright notice
#include "lifecycle_msgs/msg/detail/state__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `label`
#include "rosidl_runtime_c/string_functions.h"

bool
lifecycle_msgs__msg__State__init(lifecycle_msgs__msg__State * msg)
{
  if (!msg) {
    return false;
  }
  // id
  // label
  if (!rosidl_runtime_c__String__init(&msg->label)) {
    lifecycle_msgs__msg__State__fini(msg);
    return false;
  }
  return true;
}

void
lifecycle_msgs__msg__State__fini(lifecycle_msgs__msg__State * msg)
{
  if (!msg) {
    return;
  }
  // id
  // label
  rosidl_runtime_c__String__fini(&msg->label);
}

bool
lifecycle_msgs__msg__State__are_equal(const lifecycle_msgs__msg__State * lhs, const lifecycle_msgs__msg__State * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // id
  if (lhs->id != rhs->id) {
    return false;
  }
  // label
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->label), &(rhs->label)))
  {
    return false;
  }
  return true;
}

bool
lifecycle_msgs__msg__State__copy(
  const lifecycle_msgs__msg__State * input,
  lifecycle_msgs__msg__State * output)
{
  if (!input || !output) {
    return false;
  }
  // id
  output->id = input->id;
  // label
  if (!rosidl_runtime_c__String__copy(
      &(input->label), &(output->label)))
  {
    return false;
  }
  return true;
}

lifecycle_msgs__msg__State *
lifecycle_msgs__msg__State__create()
{
  lifecycle_msgs__msg__State * msg = (lifecycle_msgs__msg__State *)malloc(sizeof(lifecycle_msgs__msg__State));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(lifecycle_msgs__msg__State));
  bool success = lifecycle_msgs__msg__State__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
lifecycle_msgs__msg__State__destroy(lifecycle_msgs__msg__State * msg)
{
  if (msg) {
    lifecycle_msgs__msg__State__fini(msg);
  }
  free(msg);
}


bool
lifecycle_msgs__msg__State__Sequence__init(lifecycle_msgs__msg__State__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  lifecycle_msgs__msg__State * data = NULL;
  if (size) {
    data = (lifecycle_msgs__msg__State *)calloc(size, sizeof(lifecycle_msgs__msg__State));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = lifecycle_msgs__msg__State__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        lifecycle_msgs__msg__State__fini(&data[i - 1]);
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
lifecycle_msgs__msg__State__Sequence__fini(lifecycle_msgs__msg__State__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      lifecycle_msgs__msg__State__fini(&array->data[i]);
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

lifecycle_msgs__msg__State__Sequence *
lifecycle_msgs__msg__State__Sequence__create(size_t size)
{
  lifecycle_msgs__msg__State__Sequence * array = (lifecycle_msgs__msg__State__Sequence *)malloc(sizeof(lifecycle_msgs__msg__State__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = lifecycle_msgs__msg__State__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
lifecycle_msgs__msg__State__Sequence__destroy(lifecycle_msgs__msg__State__Sequence * array)
{
  if (array) {
    lifecycle_msgs__msg__State__Sequence__fini(array);
  }
  free(array);
}

bool
lifecycle_msgs__msg__State__Sequence__are_equal(const lifecycle_msgs__msg__State__Sequence * lhs, const lifecycle_msgs__msg__State__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!lifecycle_msgs__msg__State__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
lifecycle_msgs__msg__State__Sequence__copy(
  const lifecycle_msgs__msg__State__Sequence * input,
  lifecycle_msgs__msg__State__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(lifecycle_msgs__msg__State);
    lifecycle_msgs__msg__State * data =
      (lifecycle_msgs__msg__State *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!lifecycle_msgs__msg__State__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          lifecycle_msgs__msg__State__fini(&data[i]);
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
    if (!lifecycle_msgs__msg__State__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
