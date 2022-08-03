// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from grid_map_msgs:srv/SetGridMap.idl
// generated code does not contain a copyright notice
#include "grid_map_msgs/srv/detail/set_grid_map__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

// Include directives for member types
// Member `map`
#include "grid_map_msgs/msg/detail/grid_map__functions.h"

bool
grid_map_msgs__srv__SetGridMap_Request__init(grid_map_msgs__srv__SetGridMap_Request * msg)
{
  if (!msg) {
    return false;
  }
  // map
  if (!grid_map_msgs__msg__GridMap__init(&msg->map)) {
    grid_map_msgs__srv__SetGridMap_Request__fini(msg);
    return false;
  }
  return true;
}

void
grid_map_msgs__srv__SetGridMap_Request__fini(grid_map_msgs__srv__SetGridMap_Request * msg)
{
  if (!msg) {
    return;
  }
  // map
  grid_map_msgs__msg__GridMap__fini(&msg->map);
}

bool
grid_map_msgs__srv__SetGridMap_Request__are_equal(const grid_map_msgs__srv__SetGridMap_Request * lhs, const grid_map_msgs__srv__SetGridMap_Request * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // map
  if (!grid_map_msgs__msg__GridMap__are_equal(
      &(lhs->map), &(rhs->map)))
  {
    return false;
  }
  return true;
}

bool
grid_map_msgs__srv__SetGridMap_Request__copy(
  const grid_map_msgs__srv__SetGridMap_Request * input,
  grid_map_msgs__srv__SetGridMap_Request * output)
{
  if (!input || !output) {
    return false;
  }
  // map
  if (!grid_map_msgs__msg__GridMap__copy(
      &(input->map), &(output->map)))
  {
    return false;
  }
  return true;
}

grid_map_msgs__srv__SetGridMap_Request *
grid_map_msgs__srv__SetGridMap_Request__create()
{
  grid_map_msgs__srv__SetGridMap_Request * msg = (grid_map_msgs__srv__SetGridMap_Request *)malloc(sizeof(grid_map_msgs__srv__SetGridMap_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(grid_map_msgs__srv__SetGridMap_Request));
  bool success = grid_map_msgs__srv__SetGridMap_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
grid_map_msgs__srv__SetGridMap_Request__destroy(grid_map_msgs__srv__SetGridMap_Request * msg)
{
  if (msg) {
    grid_map_msgs__srv__SetGridMap_Request__fini(msg);
  }
  free(msg);
}


bool
grid_map_msgs__srv__SetGridMap_Request__Sequence__init(grid_map_msgs__srv__SetGridMap_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  grid_map_msgs__srv__SetGridMap_Request * data = NULL;
  if (size) {
    data = (grid_map_msgs__srv__SetGridMap_Request *)calloc(size, sizeof(grid_map_msgs__srv__SetGridMap_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = grid_map_msgs__srv__SetGridMap_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        grid_map_msgs__srv__SetGridMap_Request__fini(&data[i - 1]);
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
grid_map_msgs__srv__SetGridMap_Request__Sequence__fini(grid_map_msgs__srv__SetGridMap_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      grid_map_msgs__srv__SetGridMap_Request__fini(&array->data[i]);
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

grid_map_msgs__srv__SetGridMap_Request__Sequence *
grid_map_msgs__srv__SetGridMap_Request__Sequence__create(size_t size)
{
  grid_map_msgs__srv__SetGridMap_Request__Sequence * array = (grid_map_msgs__srv__SetGridMap_Request__Sequence *)malloc(sizeof(grid_map_msgs__srv__SetGridMap_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = grid_map_msgs__srv__SetGridMap_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
grid_map_msgs__srv__SetGridMap_Request__Sequence__destroy(grid_map_msgs__srv__SetGridMap_Request__Sequence * array)
{
  if (array) {
    grid_map_msgs__srv__SetGridMap_Request__Sequence__fini(array);
  }
  free(array);
}

bool
grid_map_msgs__srv__SetGridMap_Request__Sequence__are_equal(const grid_map_msgs__srv__SetGridMap_Request__Sequence * lhs, const grid_map_msgs__srv__SetGridMap_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!grid_map_msgs__srv__SetGridMap_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
grid_map_msgs__srv__SetGridMap_Request__Sequence__copy(
  const grid_map_msgs__srv__SetGridMap_Request__Sequence * input,
  grid_map_msgs__srv__SetGridMap_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(grid_map_msgs__srv__SetGridMap_Request);
    grid_map_msgs__srv__SetGridMap_Request * data =
      (grid_map_msgs__srv__SetGridMap_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!grid_map_msgs__srv__SetGridMap_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          grid_map_msgs__srv__SetGridMap_Request__fini(&data[i]);
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
    if (!grid_map_msgs__srv__SetGridMap_Request__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}


bool
grid_map_msgs__srv__SetGridMap_Response__init(grid_map_msgs__srv__SetGridMap_Response * msg)
{
  if (!msg) {
    return false;
  }
  // structure_needs_at_least_one_member
  return true;
}

void
grid_map_msgs__srv__SetGridMap_Response__fini(grid_map_msgs__srv__SetGridMap_Response * msg)
{
  if (!msg) {
    return;
  }
  // structure_needs_at_least_one_member
}

bool
grid_map_msgs__srv__SetGridMap_Response__are_equal(const grid_map_msgs__srv__SetGridMap_Response * lhs, const grid_map_msgs__srv__SetGridMap_Response * rhs)
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
grid_map_msgs__srv__SetGridMap_Response__copy(
  const grid_map_msgs__srv__SetGridMap_Response * input,
  grid_map_msgs__srv__SetGridMap_Response * output)
{
  if (!input || !output) {
    return false;
  }
  // structure_needs_at_least_one_member
  output->structure_needs_at_least_one_member = input->structure_needs_at_least_one_member;
  return true;
}

grid_map_msgs__srv__SetGridMap_Response *
grid_map_msgs__srv__SetGridMap_Response__create()
{
  grid_map_msgs__srv__SetGridMap_Response * msg = (grid_map_msgs__srv__SetGridMap_Response *)malloc(sizeof(grid_map_msgs__srv__SetGridMap_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(grid_map_msgs__srv__SetGridMap_Response));
  bool success = grid_map_msgs__srv__SetGridMap_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
grid_map_msgs__srv__SetGridMap_Response__destroy(grid_map_msgs__srv__SetGridMap_Response * msg)
{
  if (msg) {
    grid_map_msgs__srv__SetGridMap_Response__fini(msg);
  }
  free(msg);
}


bool
grid_map_msgs__srv__SetGridMap_Response__Sequence__init(grid_map_msgs__srv__SetGridMap_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  grid_map_msgs__srv__SetGridMap_Response * data = NULL;
  if (size) {
    data = (grid_map_msgs__srv__SetGridMap_Response *)calloc(size, sizeof(grid_map_msgs__srv__SetGridMap_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = grid_map_msgs__srv__SetGridMap_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        grid_map_msgs__srv__SetGridMap_Response__fini(&data[i - 1]);
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
grid_map_msgs__srv__SetGridMap_Response__Sequence__fini(grid_map_msgs__srv__SetGridMap_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      grid_map_msgs__srv__SetGridMap_Response__fini(&array->data[i]);
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

grid_map_msgs__srv__SetGridMap_Response__Sequence *
grid_map_msgs__srv__SetGridMap_Response__Sequence__create(size_t size)
{
  grid_map_msgs__srv__SetGridMap_Response__Sequence * array = (grid_map_msgs__srv__SetGridMap_Response__Sequence *)malloc(sizeof(grid_map_msgs__srv__SetGridMap_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = grid_map_msgs__srv__SetGridMap_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
grid_map_msgs__srv__SetGridMap_Response__Sequence__destroy(grid_map_msgs__srv__SetGridMap_Response__Sequence * array)
{
  if (array) {
    grid_map_msgs__srv__SetGridMap_Response__Sequence__fini(array);
  }
  free(array);
}

bool
grid_map_msgs__srv__SetGridMap_Response__Sequence__are_equal(const grid_map_msgs__srv__SetGridMap_Response__Sequence * lhs, const grid_map_msgs__srv__SetGridMap_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!grid_map_msgs__srv__SetGridMap_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
grid_map_msgs__srv__SetGridMap_Response__Sequence__copy(
  const grid_map_msgs__srv__SetGridMap_Response__Sequence * input,
  grid_map_msgs__srv__SetGridMap_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(grid_map_msgs__srv__SetGridMap_Response);
    grid_map_msgs__srv__SetGridMap_Response * data =
      (grid_map_msgs__srv__SetGridMap_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!grid_map_msgs__srv__SetGridMap_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          grid_map_msgs__srv__SetGridMap_Response__fini(&data[i]);
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
    if (!grid_map_msgs__srv__SetGridMap_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
