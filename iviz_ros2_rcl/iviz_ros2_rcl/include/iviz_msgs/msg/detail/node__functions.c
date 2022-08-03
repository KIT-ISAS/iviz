// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Node.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/node__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `name`
#include "rosidl_runtime_c/string_functions.h"
// Member `transform`
#include "iviz_msgs/msg/detail/matrix4__functions.h"
// Member `meshes`
#include "rosidl_runtime_c/primitives_sequence_functions.h"

bool
iviz_msgs__msg__Node__init(iviz_msgs__msg__Node * msg)
{
  if (!msg) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__init(&msg->name)) {
    iviz_msgs__msg__Node__fini(msg);
    return false;
  }
  // parent
  // transform
  if (!iviz_msgs__msg__Matrix4__init(&msg->transform)) {
    iviz_msgs__msg__Node__fini(msg);
    return false;
  }
  // meshes
  if (!rosidl_runtime_c__int32__Sequence__init(&msg->meshes, 0)) {
    iviz_msgs__msg__Node__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Node__fini(iviz_msgs__msg__Node * msg)
{
  if (!msg) {
    return;
  }
  // name
  rosidl_runtime_c__String__fini(&msg->name);
  // parent
  // transform
  iviz_msgs__msg__Matrix4__fini(&msg->transform);
  // meshes
  rosidl_runtime_c__int32__Sequence__fini(&msg->meshes);
}

bool
iviz_msgs__msg__Node__are_equal(const iviz_msgs__msg__Node * lhs, const iviz_msgs__msg__Node * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->name), &(rhs->name)))
  {
    return false;
  }
  // parent
  if (lhs->parent != rhs->parent) {
    return false;
  }
  // transform
  if (!iviz_msgs__msg__Matrix4__are_equal(
      &(lhs->transform), &(rhs->transform)))
  {
    return false;
  }
  // meshes
  if (!rosidl_runtime_c__int32__Sequence__are_equal(
      &(lhs->meshes), &(rhs->meshes)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Node__copy(
  const iviz_msgs__msg__Node * input,
  iviz_msgs__msg__Node * output)
{
  if (!input || !output) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__copy(
      &(input->name), &(output->name)))
  {
    return false;
  }
  // parent
  output->parent = input->parent;
  // transform
  if (!iviz_msgs__msg__Matrix4__copy(
      &(input->transform), &(output->transform)))
  {
    return false;
  }
  // meshes
  if (!rosidl_runtime_c__int32__Sequence__copy(
      &(input->meshes), &(output->meshes)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Node *
iviz_msgs__msg__Node__create()
{
  iviz_msgs__msg__Node * msg = (iviz_msgs__msg__Node *)malloc(sizeof(iviz_msgs__msg__Node));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Node));
  bool success = iviz_msgs__msg__Node__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Node__destroy(iviz_msgs__msg__Node * msg)
{
  if (msg) {
    iviz_msgs__msg__Node__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Node__Sequence__init(iviz_msgs__msg__Node__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Node * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Node *)calloc(size, sizeof(iviz_msgs__msg__Node));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Node__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Node__fini(&data[i - 1]);
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
iviz_msgs__msg__Node__Sequence__fini(iviz_msgs__msg__Node__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Node__fini(&array->data[i]);
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

iviz_msgs__msg__Node__Sequence *
iviz_msgs__msg__Node__Sequence__create(size_t size)
{
  iviz_msgs__msg__Node__Sequence * array = (iviz_msgs__msg__Node__Sequence *)malloc(sizeof(iviz_msgs__msg__Node__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Node__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Node__Sequence__destroy(iviz_msgs__msg__Node__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Node__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Node__Sequence__are_equal(const iviz_msgs__msg__Node__Sequence * lhs, const iviz_msgs__msg__Node__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Node__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Node__Sequence__copy(
  const iviz_msgs__msg__Node__Sequence * input,
  iviz_msgs__msg__Node__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Node);
    iviz_msgs__msg__Node * data =
      (iviz_msgs__msg__Node *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Node__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Node__fini(&data[i]);
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
    if (!iviz_msgs__msg__Node__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
