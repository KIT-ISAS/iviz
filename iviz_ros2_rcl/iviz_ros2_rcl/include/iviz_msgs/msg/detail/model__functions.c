// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Model.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/model__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `name`
// Member `filename`
// Member `orientation_hint`
#include "rosidl_runtime_c/string_functions.h"
// Member `meshes`
#include "iviz_msgs/msg/detail/mesh__functions.h"
// Member `materials`
#include "iviz_msgs/msg/detail/material__functions.h"
// Member `nodes`
#include "iviz_msgs/msg/detail/node__functions.h"

bool
iviz_msgs__msg__Model__init(iviz_msgs__msg__Model * msg)
{
  if (!msg) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__init(&msg->name)) {
    iviz_msgs__msg__Model__fini(msg);
    return false;
  }
  // filename
  if (!rosidl_runtime_c__String__init(&msg->filename)) {
    iviz_msgs__msg__Model__fini(msg);
    return false;
  }
  // orientation_hint
  if (!rosidl_runtime_c__String__init(&msg->orientation_hint)) {
    iviz_msgs__msg__Model__fini(msg);
    return false;
  }
  // meshes
  if (!iviz_msgs__msg__Mesh__Sequence__init(&msg->meshes, 0)) {
    iviz_msgs__msg__Model__fini(msg);
    return false;
  }
  // materials
  if (!iviz_msgs__msg__Material__Sequence__init(&msg->materials, 0)) {
    iviz_msgs__msg__Model__fini(msg);
    return false;
  }
  // nodes
  if (!iviz_msgs__msg__Node__Sequence__init(&msg->nodes, 0)) {
    iviz_msgs__msg__Model__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Model__fini(iviz_msgs__msg__Model * msg)
{
  if (!msg) {
    return;
  }
  // name
  rosidl_runtime_c__String__fini(&msg->name);
  // filename
  rosidl_runtime_c__String__fini(&msg->filename);
  // orientation_hint
  rosidl_runtime_c__String__fini(&msg->orientation_hint);
  // meshes
  iviz_msgs__msg__Mesh__Sequence__fini(&msg->meshes);
  // materials
  iviz_msgs__msg__Material__Sequence__fini(&msg->materials);
  // nodes
  iviz_msgs__msg__Node__Sequence__fini(&msg->nodes);
}

bool
iviz_msgs__msg__Model__are_equal(const iviz_msgs__msg__Model * lhs, const iviz_msgs__msg__Model * rhs)
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
  // filename
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->filename), &(rhs->filename)))
  {
    return false;
  }
  // orientation_hint
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->orientation_hint), &(rhs->orientation_hint)))
  {
    return false;
  }
  // meshes
  if (!iviz_msgs__msg__Mesh__Sequence__are_equal(
      &(lhs->meshes), &(rhs->meshes)))
  {
    return false;
  }
  // materials
  if (!iviz_msgs__msg__Material__Sequence__are_equal(
      &(lhs->materials), &(rhs->materials)))
  {
    return false;
  }
  // nodes
  if (!iviz_msgs__msg__Node__Sequence__are_equal(
      &(lhs->nodes), &(rhs->nodes)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Model__copy(
  const iviz_msgs__msg__Model * input,
  iviz_msgs__msg__Model * output)
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
  // filename
  if (!rosidl_runtime_c__String__copy(
      &(input->filename), &(output->filename)))
  {
    return false;
  }
  // orientation_hint
  if (!rosidl_runtime_c__String__copy(
      &(input->orientation_hint), &(output->orientation_hint)))
  {
    return false;
  }
  // meshes
  if (!iviz_msgs__msg__Mesh__Sequence__copy(
      &(input->meshes), &(output->meshes)))
  {
    return false;
  }
  // materials
  if (!iviz_msgs__msg__Material__Sequence__copy(
      &(input->materials), &(output->materials)))
  {
    return false;
  }
  // nodes
  if (!iviz_msgs__msg__Node__Sequence__copy(
      &(input->nodes), &(output->nodes)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Model *
iviz_msgs__msg__Model__create()
{
  iviz_msgs__msg__Model * msg = (iviz_msgs__msg__Model *)malloc(sizeof(iviz_msgs__msg__Model));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Model));
  bool success = iviz_msgs__msg__Model__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Model__destroy(iviz_msgs__msg__Model * msg)
{
  if (msg) {
    iviz_msgs__msg__Model__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Model__Sequence__init(iviz_msgs__msg__Model__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Model * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Model *)calloc(size, sizeof(iviz_msgs__msg__Model));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Model__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Model__fini(&data[i - 1]);
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
iviz_msgs__msg__Model__Sequence__fini(iviz_msgs__msg__Model__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Model__fini(&array->data[i]);
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

iviz_msgs__msg__Model__Sequence *
iviz_msgs__msg__Model__Sequence__create(size_t size)
{
  iviz_msgs__msg__Model__Sequence * array = (iviz_msgs__msg__Model__Sequence *)malloc(sizeof(iviz_msgs__msg__Model__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Model__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Model__Sequence__destroy(iviz_msgs__msg__Model__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Model__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Model__Sequence__are_equal(const iviz_msgs__msg__Model__Sequence * lhs, const iviz_msgs__msg__Model__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Model__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Model__Sequence__copy(
  const iviz_msgs__msg__Model__Sequence * input,
  iviz_msgs__msg__Model__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Model);
    iviz_msgs__msg__Model * data =
      (iviz_msgs__msg__Model *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Model__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Model__fini(&data[i]);
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
    if (!iviz_msgs__msg__Model__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
