// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Texture.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/texture__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `path`
#include "rosidl_runtime_c/string_functions.h"

bool
iviz_msgs__msg__Texture__init(iviz_msgs__msg__Texture * msg)
{
  if (!msg) {
    return false;
  }
  // path
  if (!rosidl_runtime_c__String__init(&msg->path)) {
    iviz_msgs__msg__Texture__fini(msg);
    return false;
  }
  // index
  // type
  // mapping
  // uv_index
  // blend_factor
  // operation
  // wrap_mode_u
  // wrap_mode_v
  return true;
}

void
iviz_msgs__msg__Texture__fini(iviz_msgs__msg__Texture * msg)
{
  if (!msg) {
    return;
  }
  // path
  rosidl_runtime_c__String__fini(&msg->path);
  // index
  // type
  // mapping
  // uv_index
  // blend_factor
  // operation
  // wrap_mode_u
  // wrap_mode_v
}

bool
iviz_msgs__msg__Texture__are_equal(const iviz_msgs__msg__Texture * lhs, const iviz_msgs__msg__Texture * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // path
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->path), &(rhs->path)))
  {
    return false;
  }
  // index
  if (lhs->index != rhs->index) {
    return false;
  }
  // type
  if (lhs->type != rhs->type) {
    return false;
  }
  // mapping
  if (lhs->mapping != rhs->mapping) {
    return false;
  }
  // uv_index
  if (lhs->uv_index != rhs->uv_index) {
    return false;
  }
  // blend_factor
  if (lhs->blend_factor != rhs->blend_factor) {
    return false;
  }
  // operation
  if (lhs->operation != rhs->operation) {
    return false;
  }
  // wrap_mode_u
  if (lhs->wrap_mode_u != rhs->wrap_mode_u) {
    return false;
  }
  // wrap_mode_v
  if (lhs->wrap_mode_v != rhs->wrap_mode_v) {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Texture__copy(
  const iviz_msgs__msg__Texture * input,
  iviz_msgs__msg__Texture * output)
{
  if (!input || !output) {
    return false;
  }
  // path
  if (!rosidl_runtime_c__String__copy(
      &(input->path), &(output->path)))
  {
    return false;
  }
  // index
  output->index = input->index;
  // type
  output->type = input->type;
  // mapping
  output->mapping = input->mapping;
  // uv_index
  output->uv_index = input->uv_index;
  // blend_factor
  output->blend_factor = input->blend_factor;
  // operation
  output->operation = input->operation;
  // wrap_mode_u
  output->wrap_mode_u = input->wrap_mode_u;
  // wrap_mode_v
  output->wrap_mode_v = input->wrap_mode_v;
  return true;
}

iviz_msgs__msg__Texture *
iviz_msgs__msg__Texture__create()
{
  iviz_msgs__msg__Texture * msg = (iviz_msgs__msg__Texture *)malloc(sizeof(iviz_msgs__msg__Texture));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Texture));
  bool success = iviz_msgs__msg__Texture__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Texture__destroy(iviz_msgs__msg__Texture * msg)
{
  if (msg) {
    iviz_msgs__msg__Texture__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Texture__Sequence__init(iviz_msgs__msg__Texture__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Texture * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Texture *)calloc(size, sizeof(iviz_msgs__msg__Texture));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Texture__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Texture__fini(&data[i - 1]);
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
iviz_msgs__msg__Texture__Sequence__fini(iviz_msgs__msg__Texture__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Texture__fini(&array->data[i]);
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

iviz_msgs__msg__Texture__Sequence *
iviz_msgs__msg__Texture__Sequence__create(size_t size)
{
  iviz_msgs__msg__Texture__Sequence * array = (iviz_msgs__msg__Texture__Sequence *)malloc(sizeof(iviz_msgs__msg__Texture__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Texture__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Texture__Sequence__destroy(iviz_msgs__msg__Texture__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Texture__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Texture__Sequence__are_equal(const iviz_msgs__msg__Texture__Sequence * lhs, const iviz_msgs__msg__Texture__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Texture__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Texture__Sequence__copy(
  const iviz_msgs__msg__Texture__Sequence * input,
  iviz_msgs__msg__Texture__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Texture);
    iviz_msgs__msg__Texture * data =
      (iviz_msgs__msg__Texture *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Texture__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Texture__fini(&data[i]);
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
    if (!iviz_msgs__msg__Texture__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
