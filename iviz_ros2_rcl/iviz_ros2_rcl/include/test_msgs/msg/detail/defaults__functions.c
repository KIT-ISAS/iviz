// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from test_msgs:msg/Defaults.idl
// generated code does not contain a copyright notice
#include "test_msgs/msg/detail/defaults__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


bool
test_msgs__msg__Defaults__init(test_msgs__msg__Defaults * msg)
{
  if (!msg) {
    return false;
  }
  // bool_value
  msg->bool_value = true;
  // byte_value
  msg->byte_value = 50;
  // char_value
  msg->char_value = 100;
  // float32_value
  msg->float32_value = 1.125f;
  // float64_value
  msg->float64_value = 1.125l;
  // int8_value
  msg->int8_value = -50;
  // uint8_value
  msg->uint8_value = 200;
  // int16_value
  msg->int16_value = -1000;
  // uint16_value
  msg->uint16_value = 2000;
  // int32_value
  msg->int32_value = -30000l;
  // uint32_value
  msg->uint32_value = 60000ul;
  // int64_value
  msg->int64_value = -40000000ll;
  // uint64_value
  msg->uint64_value = 50000000ull;
  return true;
}

void
test_msgs__msg__Defaults__fini(test_msgs__msg__Defaults * msg)
{
  if (!msg) {
    return;
  }
  // bool_value
  // byte_value
  // char_value
  // float32_value
  // float64_value
  // int8_value
  // uint8_value
  // int16_value
  // uint16_value
  // int32_value
  // uint32_value
  // int64_value
  // uint64_value
}

bool
test_msgs__msg__Defaults__are_equal(const test_msgs__msg__Defaults * lhs, const test_msgs__msg__Defaults * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // bool_value
  if (lhs->bool_value != rhs->bool_value) {
    return false;
  }
  // byte_value
  if (lhs->byte_value != rhs->byte_value) {
    return false;
  }
  // char_value
  if (lhs->char_value != rhs->char_value) {
    return false;
  }
  // float32_value
  if (lhs->float32_value != rhs->float32_value) {
    return false;
  }
  // float64_value
  if (lhs->float64_value != rhs->float64_value) {
    return false;
  }
  // int8_value
  if (lhs->int8_value != rhs->int8_value) {
    return false;
  }
  // uint8_value
  if (lhs->uint8_value != rhs->uint8_value) {
    return false;
  }
  // int16_value
  if (lhs->int16_value != rhs->int16_value) {
    return false;
  }
  // uint16_value
  if (lhs->uint16_value != rhs->uint16_value) {
    return false;
  }
  // int32_value
  if (lhs->int32_value != rhs->int32_value) {
    return false;
  }
  // uint32_value
  if (lhs->uint32_value != rhs->uint32_value) {
    return false;
  }
  // int64_value
  if (lhs->int64_value != rhs->int64_value) {
    return false;
  }
  // uint64_value
  if (lhs->uint64_value != rhs->uint64_value) {
    return false;
  }
  return true;
}

bool
test_msgs__msg__Defaults__copy(
  const test_msgs__msg__Defaults * input,
  test_msgs__msg__Defaults * output)
{
  if (!input || !output) {
    return false;
  }
  // bool_value
  output->bool_value = input->bool_value;
  // byte_value
  output->byte_value = input->byte_value;
  // char_value
  output->char_value = input->char_value;
  // float32_value
  output->float32_value = input->float32_value;
  // float64_value
  output->float64_value = input->float64_value;
  // int8_value
  output->int8_value = input->int8_value;
  // uint8_value
  output->uint8_value = input->uint8_value;
  // int16_value
  output->int16_value = input->int16_value;
  // uint16_value
  output->uint16_value = input->uint16_value;
  // int32_value
  output->int32_value = input->int32_value;
  // uint32_value
  output->uint32_value = input->uint32_value;
  // int64_value
  output->int64_value = input->int64_value;
  // uint64_value
  output->uint64_value = input->uint64_value;
  return true;
}

test_msgs__msg__Defaults *
test_msgs__msg__Defaults__create()
{
  test_msgs__msg__Defaults * msg = (test_msgs__msg__Defaults *)malloc(sizeof(test_msgs__msg__Defaults));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(test_msgs__msg__Defaults));
  bool success = test_msgs__msg__Defaults__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
test_msgs__msg__Defaults__destroy(test_msgs__msg__Defaults * msg)
{
  if (msg) {
    test_msgs__msg__Defaults__fini(msg);
  }
  free(msg);
}


bool
test_msgs__msg__Defaults__Sequence__init(test_msgs__msg__Defaults__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  test_msgs__msg__Defaults * data = NULL;
  if (size) {
    data = (test_msgs__msg__Defaults *)calloc(size, sizeof(test_msgs__msg__Defaults));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = test_msgs__msg__Defaults__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        test_msgs__msg__Defaults__fini(&data[i - 1]);
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
test_msgs__msg__Defaults__Sequence__fini(test_msgs__msg__Defaults__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      test_msgs__msg__Defaults__fini(&array->data[i]);
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

test_msgs__msg__Defaults__Sequence *
test_msgs__msg__Defaults__Sequence__create(size_t size)
{
  test_msgs__msg__Defaults__Sequence * array = (test_msgs__msg__Defaults__Sequence *)malloc(sizeof(test_msgs__msg__Defaults__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = test_msgs__msg__Defaults__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
test_msgs__msg__Defaults__Sequence__destroy(test_msgs__msg__Defaults__Sequence * array)
{
  if (array) {
    test_msgs__msg__Defaults__Sequence__fini(array);
  }
  free(array);
}

bool
test_msgs__msg__Defaults__Sequence__are_equal(const test_msgs__msg__Defaults__Sequence * lhs, const test_msgs__msg__Defaults__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!test_msgs__msg__Defaults__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
test_msgs__msg__Defaults__Sequence__copy(
  const test_msgs__msg__Defaults__Sequence * input,
  test_msgs__msg__Defaults__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(test_msgs__msg__Defaults);
    test_msgs__msg__Defaults * data =
      (test_msgs__msg__Defaults *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!test_msgs__msg__Defaults__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          test_msgs__msg__Defaults__fini(&data[i]);
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
    if (!test_msgs__msg__Defaults__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
