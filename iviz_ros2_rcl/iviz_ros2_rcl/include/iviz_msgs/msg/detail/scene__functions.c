// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Scene.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/scene__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `name`
// Member `filename`
#include "rosidl_runtime_c/string_functions.h"
// Member `includes`
#include "iviz_msgs/msg/detail/include__functions.h"
// Member `lights`
#include "iviz_msgs/msg/detail/light__functions.h"

bool
iviz_msgs__msg__Scene__init(iviz_msgs__msg__Scene * msg)
{
  if (!msg) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__init(&msg->name)) {
    iviz_msgs__msg__Scene__fini(msg);
    return false;
  }
  // filename
  if (!rosidl_runtime_c__String__init(&msg->filename)) {
    iviz_msgs__msg__Scene__fini(msg);
    return false;
  }
  // includes
  if (!iviz_msgs__msg__Include__Sequence__init(&msg->includes, 0)) {
    iviz_msgs__msg__Scene__fini(msg);
    return false;
  }
  // lights
  if (!iviz_msgs__msg__Light__Sequence__init(&msg->lights, 0)) {
    iviz_msgs__msg__Scene__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Scene__fini(iviz_msgs__msg__Scene * msg)
{
  if (!msg) {
    return;
  }
  // name
  rosidl_runtime_c__String__fini(&msg->name);
  // filename
  rosidl_runtime_c__String__fini(&msg->filename);
  // includes
  iviz_msgs__msg__Include__Sequence__fini(&msg->includes);
  // lights
  iviz_msgs__msg__Light__Sequence__fini(&msg->lights);
}

bool
iviz_msgs__msg__Scene__are_equal(const iviz_msgs__msg__Scene * lhs, const iviz_msgs__msg__Scene * rhs)
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
  // includes
  if (!iviz_msgs__msg__Include__Sequence__are_equal(
      &(lhs->includes), &(rhs->includes)))
  {
    return false;
  }
  // lights
  if (!iviz_msgs__msg__Light__Sequence__are_equal(
      &(lhs->lights), &(rhs->lights)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Scene__copy(
  const iviz_msgs__msg__Scene * input,
  iviz_msgs__msg__Scene * output)
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
  // includes
  if (!iviz_msgs__msg__Include__Sequence__copy(
      &(input->includes), &(output->includes)))
  {
    return false;
  }
  // lights
  if (!iviz_msgs__msg__Light__Sequence__copy(
      &(input->lights), &(output->lights)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Scene *
iviz_msgs__msg__Scene__create()
{
  iviz_msgs__msg__Scene * msg = (iviz_msgs__msg__Scene *)malloc(sizeof(iviz_msgs__msg__Scene));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Scene));
  bool success = iviz_msgs__msg__Scene__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Scene__destroy(iviz_msgs__msg__Scene * msg)
{
  if (msg) {
    iviz_msgs__msg__Scene__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Scene__Sequence__init(iviz_msgs__msg__Scene__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Scene * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Scene *)calloc(size, sizeof(iviz_msgs__msg__Scene));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Scene__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Scene__fini(&data[i - 1]);
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
iviz_msgs__msg__Scene__Sequence__fini(iviz_msgs__msg__Scene__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Scene__fini(&array->data[i]);
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

iviz_msgs__msg__Scene__Sequence *
iviz_msgs__msg__Scene__Sequence__create(size_t size)
{
  iviz_msgs__msg__Scene__Sequence * array = (iviz_msgs__msg__Scene__Sequence *)malloc(sizeof(iviz_msgs__msg__Scene__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Scene__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Scene__Sequence__destroy(iviz_msgs__msg__Scene__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Scene__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Scene__Sequence__are_equal(const iviz_msgs__msg__Scene__Sequence * lhs, const iviz_msgs__msg__Scene__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Scene__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Scene__Sequence__copy(
  const iviz_msgs__msg__Scene__Sequence * input,
  iviz_msgs__msg__Scene__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Scene);
    iviz_msgs__msg__Scene * data =
      (iviz_msgs__msg__Scene *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Scene__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Scene__fini(&data[i]);
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
    if (!iviz_msgs__msg__Scene__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
