// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Material.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/material__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `name`
#include "rosidl_runtime_c/string_functions.h"
// Member `ambient`
// Member `diffuse`
// Member `emissive`
#include "iviz_msgs/msg/detail/color32__functions.h"
// Member `textures`
#include "iviz_msgs/msg/detail/texture__functions.h"

bool
iviz_msgs__msg__Material__init(iviz_msgs__msg__Material * msg)
{
  if (!msg) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__init(&msg->name)) {
    iviz_msgs__msg__Material__fini(msg);
    return false;
  }
  // ambient
  if (!iviz_msgs__msg__Color32__init(&msg->ambient)) {
    iviz_msgs__msg__Material__fini(msg);
    return false;
  }
  // diffuse
  if (!iviz_msgs__msg__Color32__init(&msg->diffuse)) {
    iviz_msgs__msg__Material__fini(msg);
    return false;
  }
  // emissive
  if (!iviz_msgs__msg__Color32__init(&msg->emissive)) {
    iviz_msgs__msg__Material__fini(msg);
    return false;
  }
  // opacity
  // bump_scaling
  // shininess
  // shininess_strength
  // reflectivity
  // blend_mode
  // textures
  if (!iviz_msgs__msg__Texture__Sequence__init(&msg->textures, 0)) {
    iviz_msgs__msg__Material__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__Material__fini(iviz_msgs__msg__Material * msg)
{
  if (!msg) {
    return;
  }
  // name
  rosidl_runtime_c__String__fini(&msg->name);
  // ambient
  iviz_msgs__msg__Color32__fini(&msg->ambient);
  // diffuse
  iviz_msgs__msg__Color32__fini(&msg->diffuse);
  // emissive
  iviz_msgs__msg__Color32__fini(&msg->emissive);
  // opacity
  // bump_scaling
  // shininess
  // shininess_strength
  // reflectivity
  // blend_mode
  // textures
  iviz_msgs__msg__Texture__Sequence__fini(&msg->textures);
}

bool
iviz_msgs__msg__Material__are_equal(const iviz_msgs__msg__Material * lhs, const iviz_msgs__msg__Material * rhs)
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
  // ambient
  if (!iviz_msgs__msg__Color32__are_equal(
      &(lhs->ambient), &(rhs->ambient)))
  {
    return false;
  }
  // diffuse
  if (!iviz_msgs__msg__Color32__are_equal(
      &(lhs->diffuse), &(rhs->diffuse)))
  {
    return false;
  }
  // emissive
  if (!iviz_msgs__msg__Color32__are_equal(
      &(lhs->emissive), &(rhs->emissive)))
  {
    return false;
  }
  // opacity
  if (lhs->opacity != rhs->opacity) {
    return false;
  }
  // bump_scaling
  if (lhs->bump_scaling != rhs->bump_scaling) {
    return false;
  }
  // shininess
  if (lhs->shininess != rhs->shininess) {
    return false;
  }
  // shininess_strength
  if (lhs->shininess_strength != rhs->shininess_strength) {
    return false;
  }
  // reflectivity
  if (lhs->reflectivity != rhs->reflectivity) {
    return false;
  }
  // blend_mode
  if (lhs->blend_mode != rhs->blend_mode) {
    return false;
  }
  // textures
  if (!iviz_msgs__msg__Texture__Sequence__are_equal(
      &(lhs->textures), &(rhs->textures)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Material__copy(
  const iviz_msgs__msg__Material * input,
  iviz_msgs__msg__Material * output)
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
  // ambient
  if (!iviz_msgs__msg__Color32__copy(
      &(input->ambient), &(output->ambient)))
  {
    return false;
  }
  // diffuse
  if (!iviz_msgs__msg__Color32__copy(
      &(input->diffuse), &(output->diffuse)))
  {
    return false;
  }
  // emissive
  if (!iviz_msgs__msg__Color32__copy(
      &(input->emissive), &(output->emissive)))
  {
    return false;
  }
  // opacity
  output->opacity = input->opacity;
  // bump_scaling
  output->bump_scaling = input->bump_scaling;
  // shininess
  output->shininess = input->shininess;
  // shininess_strength
  output->shininess_strength = input->shininess_strength;
  // reflectivity
  output->reflectivity = input->reflectivity;
  // blend_mode
  output->blend_mode = input->blend_mode;
  // textures
  if (!iviz_msgs__msg__Texture__Sequence__copy(
      &(input->textures), &(output->textures)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__Material *
iviz_msgs__msg__Material__create()
{
  iviz_msgs__msg__Material * msg = (iviz_msgs__msg__Material *)malloc(sizeof(iviz_msgs__msg__Material));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Material));
  bool success = iviz_msgs__msg__Material__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Material__destroy(iviz_msgs__msg__Material * msg)
{
  if (msg) {
    iviz_msgs__msg__Material__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Material__Sequence__init(iviz_msgs__msg__Material__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Material * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Material *)calloc(size, sizeof(iviz_msgs__msg__Material));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Material__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Material__fini(&data[i - 1]);
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
iviz_msgs__msg__Material__Sequence__fini(iviz_msgs__msg__Material__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Material__fini(&array->data[i]);
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

iviz_msgs__msg__Material__Sequence *
iviz_msgs__msg__Material__Sequence__create(size_t size)
{
  iviz_msgs__msg__Material__Sequence * array = (iviz_msgs__msg__Material__Sequence *)malloc(sizeof(iviz_msgs__msg__Material__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Material__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Material__Sequence__destroy(iviz_msgs__msg__Material__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Material__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Material__Sequence__are_equal(const iviz_msgs__msg__Material__Sequence * lhs, const iviz_msgs__msg__Material__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Material__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Material__Sequence__copy(
  const iviz_msgs__msg__Material__Sequence * input,
  iviz_msgs__msg__Material__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Material);
    iviz_msgs__msg__Material * data =
      (iviz_msgs__msg__Material *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Material__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Material__fini(&data[i]);
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
    if (!iviz_msgs__msg__Material__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
