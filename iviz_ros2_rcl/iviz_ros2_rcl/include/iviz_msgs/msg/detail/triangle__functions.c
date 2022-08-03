// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Triangle.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/triangle__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


bool
iviz_msgs__msg__Triangle__init(iviz_msgs__msg__Triangle * msg)
{
  if (!msg) {
    return false;
  }
  // a
  // b
  // c
  return true;
}

void
iviz_msgs__msg__Triangle__fini(iviz_msgs__msg__Triangle * msg)
{
  if (!msg) {
    return;
  }
  // a
  // b
  // c
}

bool
iviz_msgs__msg__Triangle__are_equal(const iviz_msgs__msg__Triangle * lhs, const iviz_msgs__msg__Triangle * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // a
  if (lhs->a != rhs->a) {
    return false;
  }
  // b
  if (lhs->b != rhs->b) {
    return false;
  }
  // c
  if (lhs->c != rhs->c) {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Triangle__copy(
  const iviz_msgs__msg__Triangle * input,
  iviz_msgs__msg__Triangle * output)
{
  if (!input || !output) {
    return false;
  }
  // a
  output->a = input->a;
  // b
  output->b = input->b;
  // c
  output->c = input->c;
  return true;
}

iviz_msgs__msg__Triangle *
iviz_msgs__msg__Triangle__create()
{
  iviz_msgs__msg__Triangle * msg = (iviz_msgs__msg__Triangle *)malloc(sizeof(iviz_msgs__msg__Triangle));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Triangle));
  bool success = iviz_msgs__msg__Triangle__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Triangle__destroy(iviz_msgs__msg__Triangle * msg)
{
  if (msg) {
    iviz_msgs__msg__Triangle__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Triangle__Sequence__init(iviz_msgs__msg__Triangle__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Triangle * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Triangle *)calloc(size, sizeof(iviz_msgs__msg__Triangle));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Triangle__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Triangle__fini(&data[i - 1]);
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
iviz_msgs__msg__Triangle__Sequence__fini(iviz_msgs__msg__Triangle__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Triangle__fini(&array->data[i]);
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

iviz_msgs__msg__Triangle__Sequence *
iviz_msgs__msg__Triangle__Sequence__create(size_t size)
{
  iviz_msgs__msg__Triangle__Sequence * array = (iviz_msgs__msg__Triangle__Sequence *)malloc(sizeof(iviz_msgs__msg__Triangle__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Triangle__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Triangle__Sequence__destroy(iviz_msgs__msg__Triangle__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Triangle__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Triangle__Sequence__are_equal(const iviz_msgs__msg__Triangle__Sequence * lhs, const iviz_msgs__msg__Triangle__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Triangle__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Triangle__Sequence__copy(
  const iviz_msgs__msg__Triangle__Sequence * input,
  iviz_msgs__msg__Triangle__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Triangle);
    iviz_msgs__msg__Triangle * data =
      (iviz_msgs__msg__Triangle *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Triangle__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Triangle__fini(&data[i]);
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
    if (!iviz_msgs__msg__Triangle__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
