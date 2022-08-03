// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:srv/GetCaptureResolutions.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/srv/detail/get_capture_resolutions__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

bool
iviz_msgs__srv__GetCaptureResolutions_Request__init(iviz_msgs__srv__GetCaptureResolutions_Request * msg)
{
  if (!msg) {
    return false;
  }
  // structure_needs_at_least_one_member
  return true;
}

void
iviz_msgs__srv__GetCaptureResolutions_Request__fini(iviz_msgs__srv__GetCaptureResolutions_Request * msg)
{
  if (!msg) {
    return;
  }
  // structure_needs_at_least_one_member
}

bool
iviz_msgs__srv__GetCaptureResolutions_Request__are_equal(const iviz_msgs__srv__GetCaptureResolutions_Request * lhs, const iviz_msgs__srv__GetCaptureResolutions_Request * rhs)
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
iviz_msgs__srv__GetCaptureResolutions_Request__copy(
  const iviz_msgs__srv__GetCaptureResolutions_Request * input,
  iviz_msgs__srv__GetCaptureResolutions_Request * output)
{
  if (!input || !output) {
    return false;
  }
  // structure_needs_at_least_one_member
  output->structure_needs_at_least_one_member = input->structure_needs_at_least_one_member;
  return true;
}

iviz_msgs__srv__GetCaptureResolutions_Request *
iviz_msgs__srv__GetCaptureResolutions_Request__create()
{
  iviz_msgs__srv__GetCaptureResolutions_Request * msg = (iviz_msgs__srv__GetCaptureResolutions_Request *)malloc(sizeof(iviz_msgs__srv__GetCaptureResolutions_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__srv__GetCaptureResolutions_Request));
  bool success = iviz_msgs__srv__GetCaptureResolutions_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__srv__GetCaptureResolutions_Request__destroy(iviz_msgs__srv__GetCaptureResolutions_Request * msg)
{
  if (msg) {
    iviz_msgs__srv__GetCaptureResolutions_Request__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__init(iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__srv__GetCaptureResolutions_Request * data = NULL;
  if (size) {
    data = (iviz_msgs__srv__GetCaptureResolutions_Request *)calloc(size, sizeof(iviz_msgs__srv__GetCaptureResolutions_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__srv__GetCaptureResolutions_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__srv__GetCaptureResolutions_Request__fini(&data[i - 1]);
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
iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__fini(iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__srv__GetCaptureResolutions_Request__fini(&array->data[i]);
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

iviz_msgs__srv__GetCaptureResolutions_Request__Sequence *
iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__create(size_t size)
{
  iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * array = (iviz_msgs__srv__GetCaptureResolutions_Request__Sequence *)malloc(sizeof(iviz_msgs__srv__GetCaptureResolutions_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__destroy(iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * array)
{
  if (array) {
    iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__are_equal(const iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * lhs, const iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__srv__GetCaptureResolutions_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__srv__GetCaptureResolutions_Request__Sequence__copy(
  const iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * input,
  iviz_msgs__srv__GetCaptureResolutions_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__srv__GetCaptureResolutions_Request);
    iviz_msgs__srv__GetCaptureResolutions_Request * data =
      (iviz_msgs__srv__GetCaptureResolutions_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__srv__GetCaptureResolutions_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__srv__GetCaptureResolutions_Request__fini(&data[i]);
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
    if (!iviz_msgs__srv__GetCaptureResolutions_Request__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}


// Include directives for member types
// Member `message`
#include "rosidl_runtime_c/string_functions.h"
// Member `resolutions`
#include "iviz_msgs/msg/detail/vector2i__functions.h"

bool
iviz_msgs__srv__GetCaptureResolutions_Response__init(iviz_msgs__srv__GetCaptureResolutions_Response * msg)
{
  if (!msg) {
    return false;
  }
  // success
  // message
  if (!rosidl_runtime_c__String__init(&msg->message)) {
    iviz_msgs__srv__GetCaptureResolutions_Response__fini(msg);
    return false;
  }
  // resolutions
  if (!iviz_msgs__msg__Vector2i__Sequence__init(&msg->resolutions, 0)) {
    iviz_msgs__srv__GetCaptureResolutions_Response__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__srv__GetCaptureResolutions_Response__fini(iviz_msgs__srv__GetCaptureResolutions_Response * msg)
{
  if (!msg) {
    return;
  }
  // success
  // message
  rosidl_runtime_c__String__fini(&msg->message);
  // resolutions
  iviz_msgs__msg__Vector2i__Sequence__fini(&msg->resolutions);
}

bool
iviz_msgs__srv__GetCaptureResolutions_Response__are_equal(const iviz_msgs__srv__GetCaptureResolutions_Response * lhs, const iviz_msgs__srv__GetCaptureResolutions_Response * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // success
  if (lhs->success != rhs->success) {
    return false;
  }
  // message
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->message), &(rhs->message)))
  {
    return false;
  }
  // resolutions
  if (!iviz_msgs__msg__Vector2i__Sequence__are_equal(
      &(lhs->resolutions), &(rhs->resolutions)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__srv__GetCaptureResolutions_Response__copy(
  const iviz_msgs__srv__GetCaptureResolutions_Response * input,
  iviz_msgs__srv__GetCaptureResolutions_Response * output)
{
  if (!input || !output) {
    return false;
  }
  // success
  output->success = input->success;
  // message
  if (!rosidl_runtime_c__String__copy(
      &(input->message), &(output->message)))
  {
    return false;
  }
  // resolutions
  if (!iviz_msgs__msg__Vector2i__Sequence__copy(
      &(input->resolutions), &(output->resolutions)))
  {
    return false;
  }
  return true;
}

iviz_msgs__srv__GetCaptureResolutions_Response *
iviz_msgs__srv__GetCaptureResolutions_Response__create()
{
  iviz_msgs__srv__GetCaptureResolutions_Response * msg = (iviz_msgs__srv__GetCaptureResolutions_Response *)malloc(sizeof(iviz_msgs__srv__GetCaptureResolutions_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__srv__GetCaptureResolutions_Response));
  bool success = iviz_msgs__srv__GetCaptureResolutions_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__srv__GetCaptureResolutions_Response__destroy(iviz_msgs__srv__GetCaptureResolutions_Response * msg)
{
  if (msg) {
    iviz_msgs__srv__GetCaptureResolutions_Response__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__init(iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__srv__GetCaptureResolutions_Response * data = NULL;
  if (size) {
    data = (iviz_msgs__srv__GetCaptureResolutions_Response *)calloc(size, sizeof(iviz_msgs__srv__GetCaptureResolutions_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__srv__GetCaptureResolutions_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__srv__GetCaptureResolutions_Response__fini(&data[i - 1]);
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
iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__fini(iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__srv__GetCaptureResolutions_Response__fini(&array->data[i]);
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

iviz_msgs__srv__GetCaptureResolutions_Response__Sequence *
iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__create(size_t size)
{
  iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * array = (iviz_msgs__srv__GetCaptureResolutions_Response__Sequence *)malloc(sizeof(iviz_msgs__srv__GetCaptureResolutions_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__destroy(iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * array)
{
  if (array) {
    iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__are_equal(const iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * lhs, const iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__srv__GetCaptureResolutions_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__srv__GetCaptureResolutions_Response__Sequence__copy(
  const iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * input,
  iviz_msgs__srv__GetCaptureResolutions_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__srv__GetCaptureResolutions_Response);
    iviz_msgs__srv__GetCaptureResolutions_Response * data =
      (iviz_msgs__srv__GetCaptureResolutions_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__srv__GetCaptureResolutions_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__srv__GetCaptureResolutions_Response__fini(&data[i]);
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
    if (!iviz_msgs__srv__GetCaptureResolutions_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
