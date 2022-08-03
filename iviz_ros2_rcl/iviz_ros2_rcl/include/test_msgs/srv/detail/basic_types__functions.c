// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from test_msgs:srv/BasicTypes.idl
// generated code does not contain a copyright notice
#include "test_msgs/srv/detail/basic_types__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

// Include directives for member types
// Member `string_value`
#include "rosidl_runtime_c/string_functions.h"

bool
test_msgs__srv__BasicTypes_Request__init(test_msgs__srv__BasicTypes_Request * msg)
{
  if (!msg) {
    return false;
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
  // string_value
  if (!rosidl_runtime_c__String__init(&msg->string_value)) {
    test_msgs__srv__BasicTypes_Request__fini(msg);
    return false;
  }
  return true;
}

void
test_msgs__srv__BasicTypes_Request__fini(test_msgs__srv__BasicTypes_Request * msg)
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
  // string_value
  rosidl_runtime_c__String__fini(&msg->string_value);
}

bool
test_msgs__srv__BasicTypes_Request__are_equal(const test_msgs__srv__BasicTypes_Request * lhs, const test_msgs__srv__BasicTypes_Request * rhs)
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
  // string_value
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->string_value), &(rhs->string_value)))
  {
    return false;
  }
  return true;
}

bool
test_msgs__srv__BasicTypes_Request__copy(
  const test_msgs__srv__BasicTypes_Request * input,
  test_msgs__srv__BasicTypes_Request * output)
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
  // string_value
  if (!rosidl_runtime_c__String__copy(
      &(input->string_value), &(output->string_value)))
  {
    return false;
  }
  return true;
}

test_msgs__srv__BasicTypes_Request *
test_msgs__srv__BasicTypes_Request__create()
{
  test_msgs__srv__BasicTypes_Request * msg = (test_msgs__srv__BasicTypes_Request *)malloc(sizeof(test_msgs__srv__BasicTypes_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(test_msgs__srv__BasicTypes_Request));
  bool success = test_msgs__srv__BasicTypes_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
test_msgs__srv__BasicTypes_Request__destroy(test_msgs__srv__BasicTypes_Request * msg)
{
  if (msg) {
    test_msgs__srv__BasicTypes_Request__fini(msg);
  }
  free(msg);
}


bool
test_msgs__srv__BasicTypes_Request__Sequence__init(test_msgs__srv__BasicTypes_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  test_msgs__srv__BasicTypes_Request * data = NULL;
  if (size) {
    data = (test_msgs__srv__BasicTypes_Request *)calloc(size, sizeof(test_msgs__srv__BasicTypes_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = test_msgs__srv__BasicTypes_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        test_msgs__srv__BasicTypes_Request__fini(&data[i - 1]);
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
test_msgs__srv__BasicTypes_Request__Sequence__fini(test_msgs__srv__BasicTypes_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      test_msgs__srv__BasicTypes_Request__fini(&array->data[i]);
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

test_msgs__srv__BasicTypes_Request__Sequence *
test_msgs__srv__BasicTypes_Request__Sequence__create(size_t size)
{
  test_msgs__srv__BasicTypes_Request__Sequence * array = (test_msgs__srv__BasicTypes_Request__Sequence *)malloc(sizeof(test_msgs__srv__BasicTypes_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = test_msgs__srv__BasicTypes_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
test_msgs__srv__BasicTypes_Request__Sequence__destroy(test_msgs__srv__BasicTypes_Request__Sequence * array)
{
  if (array) {
    test_msgs__srv__BasicTypes_Request__Sequence__fini(array);
  }
  free(array);
}

bool
test_msgs__srv__BasicTypes_Request__Sequence__are_equal(const test_msgs__srv__BasicTypes_Request__Sequence * lhs, const test_msgs__srv__BasicTypes_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!test_msgs__srv__BasicTypes_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
test_msgs__srv__BasicTypes_Request__Sequence__copy(
  const test_msgs__srv__BasicTypes_Request__Sequence * input,
  test_msgs__srv__BasicTypes_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(test_msgs__srv__BasicTypes_Request);
    test_msgs__srv__BasicTypes_Request * data =
      (test_msgs__srv__BasicTypes_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!test_msgs__srv__BasicTypes_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          test_msgs__srv__BasicTypes_Request__fini(&data[i]);
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
    if (!test_msgs__srv__BasicTypes_Request__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}


// Include directives for member types
// Member `string_value`
// already included above
// #include "rosidl_runtime_c/string_functions.h"

bool
test_msgs__srv__BasicTypes_Response__init(test_msgs__srv__BasicTypes_Response * msg)
{
  if (!msg) {
    return false;
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
  // string_value
  if (!rosidl_runtime_c__String__init(&msg->string_value)) {
    test_msgs__srv__BasicTypes_Response__fini(msg);
    return false;
  }
  return true;
}

void
test_msgs__srv__BasicTypes_Response__fini(test_msgs__srv__BasicTypes_Response * msg)
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
  // string_value
  rosidl_runtime_c__String__fini(&msg->string_value);
}

bool
test_msgs__srv__BasicTypes_Response__are_equal(const test_msgs__srv__BasicTypes_Response * lhs, const test_msgs__srv__BasicTypes_Response * rhs)
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
  // string_value
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->string_value), &(rhs->string_value)))
  {
    return false;
  }
  return true;
}

bool
test_msgs__srv__BasicTypes_Response__copy(
  const test_msgs__srv__BasicTypes_Response * input,
  test_msgs__srv__BasicTypes_Response * output)
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
  // string_value
  if (!rosidl_runtime_c__String__copy(
      &(input->string_value), &(output->string_value)))
  {
    return false;
  }
  return true;
}

test_msgs__srv__BasicTypes_Response *
test_msgs__srv__BasicTypes_Response__create()
{
  test_msgs__srv__BasicTypes_Response * msg = (test_msgs__srv__BasicTypes_Response *)malloc(sizeof(test_msgs__srv__BasicTypes_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(test_msgs__srv__BasicTypes_Response));
  bool success = test_msgs__srv__BasicTypes_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
test_msgs__srv__BasicTypes_Response__destroy(test_msgs__srv__BasicTypes_Response * msg)
{
  if (msg) {
    test_msgs__srv__BasicTypes_Response__fini(msg);
  }
  free(msg);
}


bool
test_msgs__srv__BasicTypes_Response__Sequence__init(test_msgs__srv__BasicTypes_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  test_msgs__srv__BasicTypes_Response * data = NULL;
  if (size) {
    data = (test_msgs__srv__BasicTypes_Response *)calloc(size, sizeof(test_msgs__srv__BasicTypes_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = test_msgs__srv__BasicTypes_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        test_msgs__srv__BasicTypes_Response__fini(&data[i - 1]);
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
test_msgs__srv__BasicTypes_Response__Sequence__fini(test_msgs__srv__BasicTypes_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      test_msgs__srv__BasicTypes_Response__fini(&array->data[i]);
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

test_msgs__srv__BasicTypes_Response__Sequence *
test_msgs__srv__BasicTypes_Response__Sequence__create(size_t size)
{
  test_msgs__srv__BasicTypes_Response__Sequence * array = (test_msgs__srv__BasicTypes_Response__Sequence *)malloc(sizeof(test_msgs__srv__BasicTypes_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = test_msgs__srv__BasicTypes_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
test_msgs__srv__BasicTypes_Response__Sequence__destroy(test_msgs__srv__BasicTypes_Response__Sequence * array)
{
  if (array) {
    test_msgs__srv__BasicTypes_Response__Sequence__fini(array);
  }
  free(array);
}

bool
test_msgs__srv__BasicTypes_Response__Sequence__are_equal(const test_msgs__srv__BasicTypes_Response__Sequence * lhs, const test_msgs__srv__BasicTypes_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!test_msgs__srv__BasicTypes_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
test_msgs__srv__BasicTypes_Response__Sequence__copy(
  const test_msgs__srv__BasicTypes_Response__Sequence * input,
  test_msgs__srv__BasicTypes_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(test_msgs__srv__BasicTypes_Response);
    test_msgs__srv__BasicTypes_Response * data =
      (test_msgs__srv__BasicTypes_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!test_msgs__srv__BasicTypes_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          test_msgs__srv__BasicTypes_Response__fini(&data[i]);
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
    if (!test_msgs__srv__BasicTypes_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
