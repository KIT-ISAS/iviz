// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from test_msgs:msg/Constants.idl
// generated code does not contain a copyright notice
#include "test_msgs/msg/detail/constants__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


bool
test_msgs__msg__Constants__init(test_msgs__msg__Constants * msg)
{
  if (!msg) {
    return false;
  }
  // structure_needs_at_least_one_member
  return true;
}

void
test_msgs__msg__Constants__fini(test_msgs__msg__Constants * msg)
{
  if (!msg) {
    return;
  }
  // structure_needs_at_least_one_member
}

bool
test_msgs__msg__Constants__are_equal(const test_msgs__msg__Constants * lhs, const test_msgs__msg__Constants * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // structure_needs_at_least_one_member
  if (lhs->structure_needs_at_least_one_member != rhs->structure_needs_at_least_one_member) {
    return false;
  }
  return true;
}

bool
test_msgs__msg__Constants__copy(
  const test_msgs__msg__Constants * input,
  test_msgs__msg__Constants * output)
{
  if (!input || !output) {
    return false;
  }
  // structure_needs_at_least_one_member
  output->structure_needs_at_least_one_member = input->structure_needs_at_least_one_member;
  return true;
}

test_msgs__msg__Constants *
test_msgs__msg__Constants__create()
{
  test_msgs__msg__Constants * msg = (test_msgs__msg__Constants *)malloc(sizeof(test_msgs__msg__Constants));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(test_msgs__msg__Constants));
  bool success = test_msgs__msg__Constants__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
test_msgs__msg__Constants__destroy(test_msgs__msg__Constants * msg)
{
  if (msg) {
    test_msgs__msg__Constants__fini(msg);
  }
  free(msg);
}


bool
test_msgs__msg__Constants__Sequence__init(test_msgs__msg__Constants__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  test_msgs__msg__Constants * data = NULL;
  if (size) {
    data = (test_msgs__msg__Constants *)calloc(size, sizeof(test_msgs__msg__Constants));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = test_msgs__msg__Constants__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        test_msgs__msg__Constants__fini(&data[i - 1]);
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
test_msgs__msg__Constants__Sequence__fini(test_msgs__msg__Constants__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      test_msgs__msg__Constants__fini(&array->data[i]);
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

test_msgs__msg__Constants__Sequence *
test_msgs__msg__Constants__Sequence__create(size_t size)
{
  test_msgs__msg__Constants__Sequence * array = (test_msgs__msg__Constants__Sequence *)malloc(sizeof(test_msgs__msg__Constants__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = test_msgs__msg__Constants__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
test_msgs__msg__Constants__Sequence__destroy(test_msgs__msg__Constants__Sequence * array)
{
  if (array) {
    test_msgs__msg__Constants__Sequence__fini(array);
  }
  free(array);
}

bool
test_msgs__msg__Constants__Sequence__are_equal(const test_msgs__msg__Constants__Sequence * lhs, const test_msgs__msg__Constants__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!test_msgs__msg__Constants__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
test_msgs__msg__Constants__Sequence__copy(
  const test_msgs__msg__Constants__Sequence * input,
  test_msgs__msg__Constants__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(test_msgs__msg__Constants);
    test_msgs__msg__Constants * data =
      (test_msgs__msg__Constants *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!test_msgs__msg__Constants__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          test_msgs__msg__Constants__fini(&data[i]);
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
    if (!test_msgs__msg__Constants__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
