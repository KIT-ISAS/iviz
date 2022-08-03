// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from lifecycle_msgs:srv/ChangeState.idl
// generated code does not contain a copyright notice
#include "lifecycle_msgs/srv/detail/change_state__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

// Include directives for member types
// Member `transition`
#include "lifecycle_msgs/msg/detail/transition__functions.h"

bool
lifecycle_msgs__srv__ChangeState_Request__init(lifecycle_msgs__srv__ChangeState_Request * msg)
{
  if (!msg) {
    return false;
  }
  // transition
  if (!lifecycle_msgs__msg__Transition__init(&msg->transition)) {
    lifecycle_msgs__srv__ChangeState_Request__fini(msg);
    return false;
  }
  return true;
}

void
lifecycle_msgs__srv__ChangeState_Request__fini(lifecycle_msgs__srv__ChangeState_Request * msg)
{
  if (!msg) {
    return;
  }
  // transition
  lifecycle_msgs__msg__Transition__fini(&msg->transition);
}

bool
lifecycle_msgs__srv__ChangeState_Request__are_equal(const lifecycle_msgs__srv__ChangeState_Request * lhs, const lifecycle_msgs__srv__ChangeState_Request * rhs)
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
  return true;
}

bool
lifecycle_msgs__srv__ChangeState_Request__copy(
  const lifecycle_msgs__srv__ChangeState_Request * input,
  lifecycle_msgs__srv__ChangeState_Request * output)
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
  return true;
}

lifecycle_msgs__srv__ChangeState_Request *
lifecycle_msgs__srv__ChangeState_Request__create()
{
  lifecycle_msgs__srv__ChangeState_Request * msg = (lifecycle_msgs__srv__ChangeState_Request *)malloc(sizeof(lifecycle_msgs__srv__ChangeState_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(lifecycle_msgs__srv__ChangeState_Request));
  bool success = lifecycle_msgs__srv__ChangeState_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
lifecycle_msgs__srv__ChangeState_Request__destroy(lifecycle_msgs__srv__ChangeState_Request * msg)
{
  if (msg) {
    lifecycle_msgs__srv__ChangeState_Request__fini(msg);
  }
  free(msg);
}


bool
lifecycle_msgs__srv__ChangeState_Request__Sequence__init(lifecycle_msgs__srv__ChangeState_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  lifecycle_msgs__srv__ChangeState_Request * data = NULL;
  if (size) {
    data = (lifecycle_msgs__srv__ChangeState_Request *)calloc(size, sizeof(lifecycle_msgs__srv__ChangeState_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = lifecycle_msgs__srv__ChangeState_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        lifecycle_msgs__srv__ChangeState_Request__fini(&data[i - 1]);
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
lifecycle_msgs__srv__ChangeState_Request__Sequence__fini(lifecycle_msgs__srv__ChangeState_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      lifecycle_msgs__srv__ChangeState_Request__fini(&array->data[i]);
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

lifecycle_msgs__srv__ChangeState_Request__Sequence *
lifecycle_msgs__srv__ChangeState_Request__Sequence__create(size_t size)
{
  lifecycle_msgs__srv__ChangeState_Request__Sequence * array = (lifecycle_msgs__srv__ChangeState_Request__Sequence *)malloc(sizeof(lifecycle_msgs__srv__ChangeState_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = lifecycle_msgs__srv__ChangeState_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
lifecycle_msgs__srv__ChangeState_Request__Sequence__destroy(lifecycle_msgs__srv__ChangeState_Request__Sequence * array)
{
  if (array) {
    lifecycle_msgs__srv__ChangeState_Request__Sequence__fini(array);
  }
  free(array);
}

bool
lifecycle_msgs__srv__ChangeState_Request__Sequence__are_equal(const lifecycle_msgs__srv__ChangeState_Request__Sequence * lhs, const lifecycle_msgs__srv__ChangeState_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!lifecycle_msgs__srv__ChangeState_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
lifecycle_msgs__srv__ChangeState_Request__Sequence__copy(
  const lifecycle_msgs__srv__ChangeState_Request__Sequence * input,
  lifecycle_msgs__srv__ChangeState_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(lifecycle_msgs__srv__ChangeState_Request);
    lifecycle_msgs__srv__ChangeState_Request * data =
      (lifecycle_msgs__srv__ChangeState_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!lifecycle_msgs__srv__ChangeState_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          lifecycle_msgs__srv__ChangeState_Request__fini(&data[i]);
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
    if (!lifecycle_msgs__srv__ChangeState_Request__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}


bool
lifecycle_msgs__srv__ChangeState_Response__init(lifecycle_msgs__srv__ChangeState_Response * msg)
{
  if (!msg) {
    return false;
  }
  // success
  return true;
}

void
lifecycle_msgs__srv__ChangeState_Response__fini(lifecycle_msgs__srv__ChangeState_Response * msg)
{
  if (!msg) {
    return;
  }
  // success
}

bool
lifecycle_msgs__srv__ChangeState_Response__are_equal(const lifecycle_msgs__srv__ChangeState_Response * lhs, const lifecycle_msgs__srv__ChangeState_Response * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // success
  if (lhs->success != rhs->success) {
    return false;
  }
  return true;
}

bool
lifecycle_msgs__srv__ChangeState_Response__copy(
  const lifecycle_msgs__srv__ChangeState_Response * input,
  lifecycle_msgs__srv__ChangeState_Response * output)
{
  if (!input || !output) {
    return false;
  }
  // success
  output->success = input->success;
  return true;
}

lifecycle_msgs__srv__ChangeState_Response *
lifecycle_msgs__srv__ChangeState_Response__create()
{
  lifecycle_msgs__srv__ChangeState_Response * msg = (lifecycle_msgs__srv__ChangeState_Response *)malloc(sizeof(lifecycle_msgs__srv__ChangeState_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(lifecycle_msgs__srv__ChangeState_Response));
  bool success = lifecycle_msgs__srv__ChangeState_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
lifecycle_msgs__srv__ChangeState_Response__destroy(lifecycle_msgs__srv__ChangeState_Response * msg)
{
  if (msg) {
    lifecycle_msgs__srv__ChangeState_Response__fini(msg);
  }
  free(msg);
}


bool
lifecycle_msgs__srv__ChangeState_Response__Sequence__init(lifecycle_msgs__srv__ChangeState_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  lifecycle_msgs__srv__ChangeState_Response * data = NULL;
  if (size) {
    data = (lifecycle_msgs__srv__ChangeState_Response *)calloc(size, sizeof(lifecycle_msgs__srv__ChangeState_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = lifecycle_msgs__srv__ChangeState_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        lifecycle_msgs__srv__ChangeState_Response__fini(&data[i - 1]);
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
lifecycle_msgs__srv__ChangeState_Response__Sequence__fini(lifecycle_msgs__srv__ChangeState_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      lifecycle_msgs__srv__ChangeState_Response__fini(&array->data[i]);
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

lifecycle_msgs__srv__ChangeState_Response__Sequence *
lifecycle_msgs__srv__ChangeState_Response__Sequence__create(size_t size)
{
  lifecycle_msgs__srv__ChangeState_Response__Sequence * array = (lifecycle_msgs__srv__ChangeState_Response__Sequence *)malloc(sizeof(lifecycle_msgs__srv__ChangeState_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = lifecycle_msgs__srv__ChangeState_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
lifecycle_msgs__srv__ChangeState_Response__Sequence__destroy(lifecycle_msgs__srv__ChangeState_Response__Sequence * array)
{
  if (array) {
    lifecycle_msgs__srv__ChangeState_Response__Sequence__fini(array);
  }
  free(array);
}

bool
lifecycle_msgs__srv__ChangeState_Response__Sequence__are_equal(const lifecycle_msgs__srv__ChangeState_Response__Sequence * lhs, const lifecycle_msgs__srv__ChangeState_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!lifecycle_msgs__srv__ChangeState_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
lifecycle_msgs__srv__ChangeState_Response__Sequence__copy(
  const lifecycle_msgs__srv__ChangeState_Response__Sequence * input,
  lifecycle_msgs__srv__ChangeState_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(lifecycle_msgs__srv__ChangeState_Response);
    lifecycle_msgs__srv__ChangeState_Response * data =
      (lifecycle_msgs__srv__ChangeState_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!lifecycle_msgs__srv__ChangeState_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          lifecycle_msgs__srv__ChangeState_Response__fini(&data[i]);
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
    if (!lifecycle_msgs__srv__ChangeState_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
