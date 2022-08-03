// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Include.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/include__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `uri`
// Member `package`
#include "rosidl_runtime_c/string_functions.h"
// Member `pose`
#include "iviz_msgs/msg/detail/matrix4__functions.h"
// Member `material`
#include "iviz_msgs/msg/detail/material__functions.h"

bool
iviz_msgs__msg__Include__init(iviz_msgs__msg__Include * msg)
{
  if (!msg) {
    return false;
  }
  // uri
  if (!rosidl_runtime_c__String__init(&msg->uri)) {
    iviz_msgs__msg__Include__fini(msg);
    return false;
  }
  // pose
  if (!iviz_msgs__msg__Matrix4__init(&msg->pose)) {
    iviz_msgs__msg__Include__fini(msg);
    return false;
  }
  // material
  if (!iviz_msgs__msg__Material__init(&msg->material)) {
    iviz_msgs__msg__Include__fini(msg);
    return false;
  }
  // package
  if (!rosidl_runtime_c__String__init(&msg->package)) {
    iviz_msgs__msg__Include__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Include__fini(iviz_msgs__msg__Include * msg)
{
  if (!msg) {
    return;
  }
  // uri
  rosidl_runtime_c__String__fini(&msg->uri);
  // pose
  iviz_msgs__msg__Matrix4__fini(&msg->pose);
  // material
  iviz_msgs__msg__Material__fini(&msg->material);
  // package
  rosidl_runtime_c__String__fini(&msg->package);
}

bool
iviz_msgs__msg__Include__are_equal(const iviz_msgs__msg__Include * lhs, const iviz_msgs__msg__Include * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // uri
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->uri), &(rhs->uri)))
  {
    return false;
  }
  // pose
  if (!iviz_msgs__msg__Matrix4__are_equal(
      &(lhs->pose), &(rhs->pose)))
  {
    return false;
  }
  // material
  if (!iviz_msgs__msg__Material__are_equal(
      &(lhs->material), &(rhs->material)))
  {
    return false;
  }
  // package
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->package), &(rhs->package)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Include__copy(
  const iviz_msgs__msg__Include * input,
  iviz_msgs__msg__Include * output)
{
  if (!input || !output) {
    return false;
  }
  // uri
  if (!rosidl_runtime_c__String__copy(
      &(input->uri), &(output->uri)))
  {
    return false;
  }
  // pose
  if (!iviz_msgs__msg__Matrix4__copy(
      &(input->pose), &(output->pose)))
  {
    return false;
  }
  // material
  if (!iviz_msgs__msg__Material__copy(
      &(input->material), &(output->material)))
  {
    return false;
  }
  // package
  if (!rosidl_runtime_c__String__copy(
      &(input->package), &(output->package)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Include *
iviz_msgs__msg__Include__create()
{
  iviz_msgs__msg__Include * msg = (iviz_msgs__msg__Include *)malloc(sizeof(iviz_msgs__msg__Include));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Include));
  bool success = iviz_msgs__msg__Include__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Include__destroy(iviz_msgs__msg__Include * msg)
{
  if (msg) {
    iviz_msgs__msg__Include__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Include__Sequence__init(iviz_msgs__msg__Include__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Include * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Include *)calloc(size, sizeof(iviz_msgs__msg__Include));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Include__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Include__fini(&data[i - 1]);
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
iviz_msgs__msg__Include__Sequence__fini(iviz_msgs__msg__Include__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Include__fini(&array->data[i]);
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

iviz_msgs__msg__Include__Sequence *
iviz_msgs__msg__Include__Sequence__create(size_t size)
{
  iviz_msgs__msg__Include__Sequence * array = (iviz_msgs__msg__Include__Sequence *)malloc(sizeof(iviz_msgs__msg__Include__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Include__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Include__Sequence__destroy(iviz_msgs__msg__Include__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Include__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Include__Sequence__are_equal(const iviz_msgs__msg__Include__Sequence * lhs, const iviz_msgs__msg__Include__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Include__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Include__Sequence__copy(
  const iviz_msgs__msg__Include__Sequence * input,
  iviz_msgs__msg__Include__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Include);
    iviz_msgs__msg__Include * data =
      (iviz_msgs__msg__Include *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Include__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Include__fini(&data[i]);
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
    if (!iviz_msgs__msg__Include__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
