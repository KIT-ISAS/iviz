// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from grid_map_msgs:msg/GridMapInfo.idl
// generated code does not contain a copyright notice
#include "grid_map_msgs/msg/detail/grid_map_info__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `pose`
#include "geometry_msgs/msg/detail/pose__functions.h"

bool
grid_map_msgs__msg__GridMapInfo__init(grid_map_msgs__msg__GridMapInfo * msg)
{
  if (!msg) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    grid_map_msgs__msg__GridMapInfo__fini(msg);
    return false;
  }
  // resolution
  // length_x
  // length_y
  // pose
  if (!geometry_msgs__msg__Pose__init(&msg->pose)) {
    grid_map_msgs__msg__GridMapInfo__fini(msg);
    return false;
  }
  return true;
}

void
grid_map_msgs__msg__GridMapInfo__fini(grid_map_msgs__msg__GridMapInfo * msg)
{
  if (!msg) {
    return;
  }
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // resolution
  // length_x
  // length_y
  // pose
  geometry_msgs__msg__Pose__fini(&msg->pose);
}

bool
grid_map_msgs__msg__GridMapInfo__are_equal(const grid_map_msgs__msg__GridMapInfo * lhs, const grid_map_msgs__msg__GridMapInfo * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__are_equal(
      &(lhs->header), &(rhs->header)))
  {
    return false;
  }
  // resolution
  if (lhs->resolution != rhs->resolution) {
    return false;
  }
  // length_x
  if (lhs->length_x != rhs->length_x) {
    return false;
  }
  // length_y
  if (lhs->length_y != rhs->length_y) {
    return false;
  }
  // pose
  if (!geometry_msgs__msg__Pose__are_equal(
      &(lhs->pose), &(rhs->pose)))
  {
    return false;
  }
  return true;
}

bool
grid_map_msgs__msg__GridMapInfo__copy(
  const grid_map_msgs__msg__GridMapInfo * input,
  grid_map_msgs__msg__GridMapInfo * output)
{
  if (!input || !output) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__copy(
      &(input->header), &(output->header)))
  {
    return false;
  }
  // resolution
  output->resolution = input->resolution;
  // length_x
  output->length_x = input->length_x;
  // length_y
  output->length_y = input->length_y;
  // pose
  if (!geometry_msgs__msg__Pose__copy(
      &(input->pose), &(output->pose)))
  {
    return false;
  }
  return true;
}

grid_map_msgs__msg__GridMapInfo *
grid_map_msgs__msg__GridMapInfo__create()
{
  grid_map_msgs__msg__GridMapInfo * msg = (grid_map_msgs__msg__GridMapInfo *)malloc(sizeof(grid_map_msgs__msg__GridMapInfo));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(grid_map_msgs__msg__GridMapInfo));
  bool success = grid_map_msgs__msg__GridMapInfo__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
grid_map_msgs__msg__GridMapInfo__destroy(grid_map_msgs__msg__GridMapInfo * msg)
{
  if (msg) {
    grid_map_msgs__msg__GridMapInfo__fini(msg);
  }
  free(msg);
}


bool
grid_map_msgs__msg__GridMapInfo__Sequence__init(grid_map_msgs__msg__GridMapInfo__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  grid_map_msgs__msg__GridMapInfo * data = NULL;
  if (size) {
    data = (grid_map_msgs__msg__GridMapInfo *)calloc(size, sizeof(grid_map_msgs__msg__GridMapInfo));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = grid_map_msgs__msg__GridMapInfo__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        grid_map_msgs__msg__GridMapInfo__fini(&data[i - 1]);
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
grid_map_msgs__msg__GridMapInfo__Sequence__fini(grid_map_msgs__msg__GridMapInfo__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      grid_map_msgs__msg__GridMapInfo__fini(&array->data[i]);
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

grid_map_msgs__msg__GridMapInfo__Sequence *
grid_map_msgs__msg__GridMapInfo__Sequence__create(size_t size)
{
  grid_map_msgs__msg__GridMapInfo__Sequence * array = (grid_map_msgs__msg__GridMapInfo__Sequence *)malloc(sizeof(grid_map_msgs__msg__GridMapInfo__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = grid_map_msgs__msg__GridMapInfo__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
grid_map_msgs__msg__GridMapInfo__Sequence__destroy(grid_map_msgs__msg__GridMapInfo__Sequence * array)
{
  if (array) {
    grid_map_msgs__msg__GridMapInfo__Sequence__fini(array);
  }
  free(array);
}

bool
grid_map_msgs__msg__GridMapInfo__Sequence__are_equal(const grid_map_msgs__msg__GridMapInfo__Sequence * lhs, const grid_map_msgs__msg__GridMapInfo__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!grid_map_msgs__msg__GridMapInfo__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
grid_map_msgs__msg__GridMapInfo__Sequence__copy(
  const grid_map_msgs__msg__GridMapInfo__Sequence * input,
  grid_map_msgs__msg__GridMapInfo__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(grid_map_msgs__msg__GridMapInfo);
    grid_map_msgs__msg__GridMapInfo * data =
      (grid_map_msgs__msg__GridMapInfo *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!grid_map_msgs__msg__GridMapInfo__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          grid_map_msgs__msg__GridMapInfo__fini(&data[i]);
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
    if (!grid_map_msgs__msg__GridMapInfo__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
