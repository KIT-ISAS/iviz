// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Vector2i.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/vector2i__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


bool
iviz_msgs__msg__Vector2i__init(iviz_msgs__msg__Vector2i * msg)
{
  if (!msg) {
    return false;
  }
  // x
  // y
  return true;
}

void
iviz_msgs__msg__Vector2i__fini(iviz_msgs__msg__Vector2i * msg)
{
  if (!msg) {
    return;
  }
  // x
  // y
}

bool
iviz_msgs__msg__Vector2i__are_equal(const iviz_msgs__msg__Vector2i * lhs, const iviz_msgs__msg__Vector2i * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // x
  if (lhs->x != rhs->x) {
    return false;
  }
  // y
  if (lhs->y != rhs->y) {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Vector2i__copy(
  const iviz_msgs__msg__Vector2i * input,
  iviz_msgs__msg__Vector2i * output)
{
  if (!input || !output) {
    return false;
  }
  // x
  output->x = input->x;
  // y
  output->y = input->y;
  return true;
}

iviz_msgs__msg__Vector2i *
iviz_msgs__msg__Vector2i__create()
{
  iviz_msgs__msg__Vector2i * msg = (iviz_msgs__msg__Vector2i *)malloc(sizeof(iviz_msgs__msg__Vector2i));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Vector2i));
  bool success = iviz_msgs__msg__Vector2i__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Vector2i__destroy(iviz_msgs__msg__Vector2i * msg)
{
  if (msg) {
    iviz_msgs__msg__Vector2i__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Vector2i__Sequence__init(iviz_msgs__msg__Vector2i__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Vector2i * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Vector2i *)calloc(size, sizeof(iviz_msgs__msg__Vector2i));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Vector2i__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Vector2i__fini(&data[i - 1]);
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
iviz_msgs__msg__Vector2i__Sequence__fini(iviz_msgs__msg__Vector2i__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Vector2i__fini(&array->data[i]);
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

iviz_msgs__msg__Vector2i__Sequence *
iviz_msgs__msg__Vector2i__Sequence__create(size_t size)
{
  iviz_msgs__msg__Vector2i__Sequence * array = (iviz_msgs__msg__Vector2i__Sequence *)malloc(sizeof(iviz_msgs__msg__Vector2i__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Vector2i__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Vector2i__Sequence__destroy(iviz_msgs__msg__Vector2i__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Vector2i__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Vector2i__Sequence__are_equal(const iviz_msgs__msg__Vector2i__Sequence * lhs, const iviz_msgs__msg__Vector2i__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Vector2i__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Vector2i__Sequence__copy(
  const iviz_msgs__msg__Vector2i__Sequence * input,
  iviz_msgs__msg__Vector2i__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Vector2i);
    iviz_msgs__msg__Vector2i * data =
      (iviz_msgs__msg__Vector2i *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Vector2i__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Vector2i__fini(&data[i]);
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
    if (!iviz_msgs__msg__Vector2i__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
