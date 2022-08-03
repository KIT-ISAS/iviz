// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from lifecycle_msgs:msg/TransitionDescription.idl
// generated code does not contain a copyright notice
#include "lifecycle_msgs/msg/detail/transition_description__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `transition`
#include "lifecycle_msgs/msg/detail/transition__functions.h"
// Member `start_state`
// Member `goal_state`
#include "lifecycle_msgs/msg/detail/state__functions.h"

bool
lifecycle_msgs__msg__TransitionDescription__init(lifecycle_msgs__msg__TransitionDescription * msg)
{
  if (!msg) {
    return false;
  }
  // transition
  if (!lifecycle_msgs__msg__Transition__init(&msg->transition)) {
    lifecycle_msgs__msg__TransitionDescription__fini(msg);
    return false;
  }
  // start_state
  if (!lifecycle_msgs__msg__State__init(&msg->start_state)) {
    lifecycle_msgs__msg__TransitionDescription__fini(msg);
    return false;
  }
  // goal_state
  if (!lifecycle_msgs__msg__State__init(&msg->goal_state)) {
    lifecycle_msgs__msg__TransitionDescription__fini(msg);
    return false;
  }
  return true;
}

void
lifecycle_msgs__msg__TransitionDescription__fini(lifecycle_msgs__msg__TransitionDescription * msg)
{
  if (!msg) {
    return;
  }
  // transition
  lifecycle_msgs__msg__Transition__fini(&msg->transition);
  // start_state
  lifecycle_msgs__msg__State__fini(&msg->start_state);
  // goal_state
  lifecycle_msgs__msg__State__fini(&msg->goal_state);
}

bool
lifecycle_msgs__msg__TransitionDescription__are_equal(const lifecycle_msgs__msg__TransitionDescription * lhs, const lifecycle_msgs__msg__TransitionDescription * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // transition
  if (!lifecycle_msgs__msg__Transition__are_equal(
      &(lhs->transition), &(rhs->transition)))
  {
    return false;
  }
  // start_state
  if (!lifecycle_msgs__msg__State__are_equal(
      &(lhs->start_state), &(rhs->start_state)))
  {
    return false;
  }
  // goal_state
  if (!lifecycle_msgs__msg__State__are_equal(
      &(lhs->goal_state), &(rhs->goal_state)))
  {
    return false;
  }
  return true;
}

bool
lifecycle_msgs__msg__TransitionDescription__copy(
  const lifecycle_msgs__msg__TransitionDescription * input,
  lifecycle_msgs__msg__TransitionDescription * output)
{
  if (!input || !output) {
    return false;
  }
  // transition
  if (!lifecycle_msgs__msg__Transition__copy(
      &(input->transition), &(output->transition)))
  {
    return false;
  }
  // start_state
  if (!lifecycle_msgs__msg__State__copy(
      &(input->start_state), &(output->start_state)))
  {
    return false;
  }
  // goal_state
  if (!lifecycle_msgs__msg__State__copy(
      &(input->goal_state), &(output->goal_state)))
  {
    return false;
  }
  return true;
}

lifecycle_msgs__msg__TransitionDescription *
lifecycle_msgs__msg__TransitionDescription__create()
{
  lifecycle_msgs__msg__TransitionDescription * msg = (lifecycle_msgs__msg__TransitionDescription *)malloc(sizeof(lifecycle_msgs__msg__TransitionDescription));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(lifecycle_msgs__msg__TransitionDescription));
  bool success = lifecycle_msgs__msg__TransitionDescription__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
lifecycle_msgs__msg__TransitionDescription__destroy(lifecycle_msgs__msg__TransitionDescription * msg)
{
  if (msg) {
    lifecycle_msgs__msg__TransitionDescription__fini(msg);
  }
  free(msg);
}


bool
lifecycle_msgs__msg__TransitionDescription__Sequence__init(lifecycle_msgs__msg__TransitionDescription__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  lifecycle_msgs__msg__TransitionDescription * data = NULL;
  if (size) {
    data = (lifecycle_msgs__msg__TransitionDescription *)calloc(size, sizeof(lifecycle_msgs__msg__TransitionDescription));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = lifecycle_msgs__msg__TransitionDescription__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        lifecycle_msgs__msg__TransitionDescription__fini(&data[i - 1]);
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
lifecycle_msgs__msg__TransitionDescription__Sequence__fini(lifecycle_msgs__msg__TransitionDescription__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      lifecycle_msgs__msg__TransitionDescription__fini(&array->data[i]);
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

lifecycle_msgs__msg__TransitionDescription__Sequence *
lifecycle_msgs__msg__TransitionDescription__Sequence__create(size_t size)
{
  lifecycle_msgs__msg__TransitionDescription__Sequence * array = (lifecycle_msgs__msg__TransitionDescription__Sequence *)malloc(sizeof(lifecycle_msgs__msg__TransitionDescription__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = lifecycle_msgs__msg__TransitionDescription__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
lifecycle_msgs__msg__TransitionDescription__Sequence__destroy(lifecycle_msgs__msg__TransitionDescription__Sequence * array)
{
  if (array) {
    lifecycle_msgs__msg__TransitionDescription__Sequence__fini(array);
  }
  free(array);
}

bool
lifecycle_msgs__msg__TransitionDescription__Sequence__are_equal(const lifecycle_msgs__msg__TransitionDescription__Sequence * lhs, const lifecycle_msgs__msg__TransitionDescription__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!lifecycle_msgs__msg__TransitionDescription__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
lifecycle_msgs__msg__TransitionDescription__Sequence__copy(
  const lifecycle_msgs__msg__TransitionDescription__Sequence * input,
  lifecycle_msgs__msg__TransitionDescription__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(lifecycle_msgs__msg__TransitionDescription);
    lifecycle_msgs__msg__TransitionDescription * data =
      (lifecycle_msgs__msg__TransitionDescription *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!lifecycle_msgs__msg__TransitionDescription__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          lifecycle_msgs__msg__TransitionDescription__fini(&data[i]);
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
    if (!lifecycle_msgs__msg__TransitionDescription__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
