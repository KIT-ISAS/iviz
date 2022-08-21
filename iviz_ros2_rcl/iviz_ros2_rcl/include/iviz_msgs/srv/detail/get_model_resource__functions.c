// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:srv/GetModelResource.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/srv/detail/get_model_resource__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

// Include directives for member types
// Member `uri`
#include "rosidl_runtime_c/string_functions.h"

bool
iviz_msgs__srv__GetModelResource_Request__init(iviz_msgs__srv__GetModelResource_Request * msg)
{
  if (!msg) {
    return false;
  }
  // uri
  if (!rosidl_runtime_c__String__init(&msg->uri)) {
    iviz_msgs__srv__GetModelResource_Request__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__srv__GetModelResource_Request__fini(iviz_msgs__srv__GetModelResource_Request * msg)
{
  if (!msg) {
    return;
  }
  // uri
  rosidl_runtime_c__String__fini(&msg->uri);
}

bool
iviz_msgs__srv__GetModelResource_Request__are_equal(const iviz_msgs__srv__GetModelResource_Request * lhs, const iviz_msgs__srv__GetModelResource_Request * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // uri
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->uri), &(rhs->uri)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__srv__GetModelResource_Request__copy(
  const iviz_msgs__srv__GetModelResource_Request * input,
  iviz_msgs__srv__GetModelResource_Request * output)
{
  if (!input || !output) {
    return false;
  }
  // uri
  if (!rosidl_runtime_c__String__copy(
      &(input->uri), &(output->uri)))
  {
    return false;
  }
  return true;
}

iviz_msgs__srv__GetModelResource_Request *
iviz_msgs__srv__GetModelResource_Request__create()
{
  iviz_msgs__srv__GetModelResource_Request * msg = (iviz_msgs__srv__GetModelResource_Request *)malloc(sizeof(iviz_msgs__srv__GetModelResource_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__srv__GetModelResource_Request));
  bool success = iviz_msgs__srv__GetModelResource_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__srv__GetModelResource_Request__destroy(iviz_msgs__srv__GetModelResource_Request * msg)
{
  if (msg) {
    iviz_msgs__srv__GetModelResource_Request__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__srv__GetModelResource_Request__Sequence__init(iviz_msgs__srv__GetModelResource_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__srv__GetModelResource_Request * data = NULL;
  if (size) {
    data = (iviz_msgs__srv__GetModelResource_Request *)calloc(size, sizeof(iviz_msgs__srv__GetModelResource_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__srv__GetModelResource_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__srv__GetModelResource_Request__fini(&data[i - 1]);
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
iviz_msgs__srv__GetModelResource_Request__Sequence__fini(iviz_msgs__srv__GetModelResource_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__srv__GetModelResource_Request__fini(&array->data[i]);
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

iviz_msgs__srv__GetModelResource_Request__Sequence *
iviz_msgs__srv__GetModelResource_Request__Sequence__create(size_t size)
{
  iviz_msgs__srv__GetModelResource_Request__Sequence * array = (iviz_msgs__srv__GetModelResource_Request__Sequence *)malloc(sizeof(iviz_msgs__srv__GetModelResource_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__srv__GetModelResource_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__srv__GetModelResource_Request__Sequence__destroy(iviz_msgs__srv__GetModelResource_Request__Sequence * array)
{
  if (array) {
    iviz_msgs__srv__GetModelResource_Request__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__srv__GetModelResource_Request__Sequence__are_equal(const iviz_msgs__srv__GetModelResource_Request__Sequence * lhs, const iviz_msgs__srv__GetModelResource_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__srv__GetModelResource_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__srv__GetModelResource_Request__Sequence__copy(
  const iviz_msgs__srv__GetModelResource_Request__Sequence * input,
  iviz_msgs__srv__GetModelResource_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__srv__GetModelResource_Request);
    iviz_msgs__srv__GetModelResource_Request * data =
      (iviz_msgs__srv__GetModelResource_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__srv__GetModelResource_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__srv__GetModelResource_Request__fini(&data[i]);
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
    if (!iviz_msgs__srv__GetModelResource_Request__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}


// Include directives for member types
// Member `model`
#include "iviz_msgs/msg/detail/model__functions.h"
// Member `message`
// already included above
// #include "rosidl_runtime_c/string_functions.h"

bool
iviz_msgs__srv__GetModelResource_Response__init(iviz_msgs__srv__GetModelResource_Response * msg)
{
  if (!msg) {
    return false;
  }
  // success
  // model
  if (!iviz_msgs__msg__Model__init(&msg->model)) {
    iviz_msgs__srv__GetModelResource_Response__fini(msg);
    return false;
  }
  // message
  if (!rosidl_runtime_c__String__init(&msg->message)) {
    iviz_msgs__srv__GetModelResource_Response__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__srv__GetModelResource_Response__fini(iviz_msgs__srv__GetModelResource_Response * msg)
{
  if (!msg) {
    return;
  }
  // success
  // model
  iviz_msgs__msg__Model__fini(&msg->model);
  // message
  rosidl_runtime_c__String__fini(&msg->message);
}

bool
iviz_msgs__srv__GetModelResource_Response__are_equal(const iviz_msgs__srv__GetModelResource_Response * lhs, const iviz_msgs__srv__GetModelResource_Response * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // success
  if (lhs->success != rhs->success) {
    return false;
  }
  // model
  if (!iviz_msgs__msg__Model__are_equal(
      &(lhs->model), &(rhs->model)))
  {
    return false;
  }
  // message
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->message), &(rhs->message)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__srv__GetModelResource_Response__copy(
  const iviz_msgs__srv__GetModelResource_Response * input,
  iviz_msgs__srv__GetModelResource_Response * output)
{
  if (!input || !output) {
    return false;
  }
  // success
  output->success = input->success;
  // model
  if (!iviz_msgs__msg__Model__copy(
      &(input->model), &(output->model)))
  {
    return false;
  }
  // message
  if (!rosidl_runtime_c__String__copy(
      &(input->message), &(output->message)))
  {
    return false;
  }
  return true;
}

iviz_msgs__srv__GetModelResource_Response *
iviz_msgs__srv__GetModelResource_Response__create()
{
  iviz_msgs__srv__GetModelResource_Response * msg = (iviz_msgs__srv__GetModelResource_Response *)malloc(sizeof(iviz_msgs__srv__GetModelResource_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__srv__GetModelResource_Response));
  bool success = iviz_msgs__srv__GetModelResource_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__srv__GetModelResource_Response__destroy(iviz_msgs__srv__GetModelResource_Response * msg)
{
  if (msg) {
    iviz_msgs__srv__GetModelResource_Response__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__srv__GetModelResource_Response__Sequence__init(iviz_msgs__srv__GetModelResource_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__srv__GetModelResource_Response * data = NULL;
  if (size) {
    data = (iviz_msgs__srv__GetModelResource_Response *)calloc(size, sizeof(iviz_msgs__srv__GetModelResource_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__srv__GetModelResource_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__srv__GetModelResource_Response__fini(&data[i - 1]);
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
iviz_msgs__srv__GetModelResource_Response__Sequence__fini(iviz_msgs__srv__GetModelResource_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__srv__GetModelResource_Response__fini(&array->data[i]);
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

iviz_msgs__srv__GetModelResource_Response__Sequence *
iviz_msgs__srv__GetModelResource_Response__Sequence__create(size_t size)
{
  iviz_msgs__srv__GetModelResource_Response__Sequence * array = (iviz_msgs__srv__GetModelResource_Response__Sequence *)malloc(sizeof(iviz_msgs__srv__GetModelResource_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__srv__GetModelResource_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__srv__GetModelResource_Response__Sequence__destroy(iviz_msgs__srv__GetModelResource_Response__Sequence * array)
{
  if (array) {
    iviz_msgs__srv__GetModelResource_Response__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__srv__GetModelResource_Response__Sequence__are_equal(const iviz_msgs__srv__GetModelResource_Response__Sequence * lhs, const iviz_msgs__srv__GetModelResource_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__srv__GetModelResource_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__srv__GetModelResource_Response__Sequence__copy(
  const iviz_msgs__srv__GetModelResource_Response__Sequence * input,
  iviz_msgs__srv__GetModelResource_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__srv__GetModelResource_Response);
    iviz_msgs__srv__GetModelResource_Response * data =
      (iviz_msgs__srv__GetModelResource_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__srv__GetModelResource_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__srv__GetModelResource_Response__fini(&data[i]);
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
    if (!iviz_msgs__srv__GetModelResource_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}