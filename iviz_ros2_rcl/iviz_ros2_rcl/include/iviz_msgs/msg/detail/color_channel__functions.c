// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/ColorChannel.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/color_channel__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `colors`
#include "iviz_msgs/msg/detail/color32__functions.h"

bool
iviz_msgs__msg__ColorChannel__init(iviz_msgs__msg__ColorChannel * msg)
{
  if (!msg) {
    return false;
  }
  // colors
  if (!iviz_msgs__msg__Color32__Sequence__init(&msg->colors, 0)) {
    iviz_msgs__msg__ColorChannel__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__ColorChannel__fini(iviz_msgs__msg__ColorChannel * msg)
{
  if (!msg) {
    return;
  }
  // colors
  iviz_msgs__msg__Color32__Sequence__fini(&msg->colors);
}

bool
iviz_msgs__msg__ColorChannel__are_equal(const iviz_msgs__msg__ColorChannel * lhs, const iviz_msgs__msg__ColorChannel * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // colors
  if (!iviz_msgs__msg__Color32__Sequence__are_equal(
      &(lhs->colors), &(rhs->colors)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__ColorChannel__copy(
  const iviz_msgs__msg__ColorChannel * input,
  iviz_msgs__msg__ColorChannel * output)
{
  if (!input || !output) {
    return false;
  }
  // colors
  if (!iviz_msgs__msg__Color32__Sequence__copy(
      &(input->colors), &(output->colors)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__ColorChannel *
iviz_msgs__msg__ColorChannel__create()
{
  iviz_msgs__msg__ColorChannel * msg = (iviz_msgs__msg__ColorChannel *)malloc(sizeof(iviz_msgs__msg__ColorChannel));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__ColorChannel));
  bool success = iviz_msgs__msg__ColorChannel__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__ColorChannel__destroy(iviz_msgs__msg__ColorChannel * msg)
{
  if (msg) {
    iviz_msgs__msg__ColorChannel__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__ColorChannel__Sequence__init(iviz_msgs__msg__ColorChannel__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__ColorChannel * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__ColorChannel *)calloc(size, sizeof(iviz_msgs__msg__ColorChannel));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__ColorChannel__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__ColorChannel__fini(&data[i - 1]);
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
iviz_msgs__msg__ColorChannel__Sequence__fini(iviz_msgs__msg__ColorChannel__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__ColorChannel__fini(&array->data[i]);
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

iviz_msgs__msg__ColorChannel__Sequence *
iviz_msgs__msg__ColorChannel__Sequence__create(size_t size)
{
  iviz_msgs__msg__ColorChannel__Sequence * array = (iviz_msgs__msg__ColorChannel__Sequence *)malloc(sizeof(iviz_msgs__msg__ColorChannel__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__ColorChannel__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__ColorChannel__Sequence__destroy(iviz_msgs__msg__ColorChannel__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__ColorChannel__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__ColorChannel__Sequence__are_equal(const iviz_msgs__msg__ColorChannel__Sequence * lhs, const iviz_msgs__msg__ColorChannel__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__ColorChannel__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__ColorChannel__Sequence__copy(
  const iviz_msgs__msg__ColorChannel__Sequence * input,
  iviz_msgs__msg__ColorChannel__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__ColorChannel);
    iviz_msgs__msg__ColorChannel * data =
      (iviz_msgs__msg__ColorChannel *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__ColorChannel__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__ColorChannel__fini(&data[i]);
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
    if (!iviz_msgs__msg__ColorChannel__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
