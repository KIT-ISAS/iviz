// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from composition_interfaces:srv/UnloadNode.idl
// generated code does not contain a copyright notice
#include "composition_interfaces/srv/detail/unload_node__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

bool
composition_interfaces__srv__UnloadNode_Request__init(composition_interfaces__srv__UnloadNode_Request * msg)
{
  if (!msg) {
    return false;
  }
  // unique_id
  return true;
}

void
composition_interfaces__srv__UnloadNode_Request__fini(composition_interfaces__srv__UnloadNode_Request * msg)
{
  if (!msg) {
    return;
  }
  // unique_id
}

bool
composition_interfaces__srv__UnloadNode_Request__are_equal(const composition_interfaces__srv__UnloadNode_Request * lhs, const composition_interfaces__srv__UnloadNode_Request * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // unique_id
  if (lhs->unique_id != rhs->unique_id) {
    return false;
  }
  return true;
}

bool
composition_interfaces__srv__UnloadNode_Request__copy(
  const composition_interfaces__srv__UnloadNode_Request * input,
  composition_interfaces__srv__UnloadNode_Request * output)
{
  if (!input || !output) {
    return false;
  }
  // unique_id
  output->unique_id = input->unique_id;
  return true;
}

composition_interfaces__srv__UnloadNode_Request *
composition_interfaces__srv__UnloadNode_Request__create()
{
  composition_interfaces__srv__UnloadNode_Request * msg = (composition_interfaces__srv__UnloadNode_Request *)malloc(sizeof(composition_interfaces__srv__UnloadNode_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(composition_interfaces__srv__UnloadNode_Request));
  bool success = composition_interfaces__srv__UnloadNode_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
composition_interfaces__srv__UnloadNode_Request__destroy(composition_interfaces__srv__UnloadNode_Request * msg)
{
  if (msg) {
    composition_interfaces__srv__UnloadNode_Request__fini(msg);
  }
  free(msg);
}


bool
composition_interfaces__srv__UnloadNode_Request__Sequence__init(composition_interfaces__srv__UnloadNode_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  composition_interfaces__srv__UnloadNode_Request * data = NULL;
  if (size) {
    data = (composition_interfaces__srv__UnloadNode_Request *)calloc(size, sizeof(composition_interfaces__srv__UnloadNode_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = composition_interfaces__srv__UnloadNode_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        composition_interfaces__srv__UnloadNode_Request__fini(&data[i - 1]);
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
composition_interfaces__srv__UnloadNode_Request__Sequence__fini(composition_interfaces__srv__UnloadNode_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      composition_interfaces__srv__UnloadNode_Request__fini(&array->data[i]);
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

composition_interfaces__srv__UnloadNode_Request__Sequence *
composition_interfaces__srv__UnloadNode_Request__Sequence__create(size_t size)
{
  composition_interfaces__srv__UnloadNode_Request__Sequence * array = (composition_interfaces__srv__UnloadNode_Request__Sequence *)malloc(sizeof(composition_interfaces__srv__UnloadNode_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = composition_interfaces__srv__UnloadNode_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
composition_interfaces__srv__UnloadNode_Request__Sequence__destroy(composition_interfaces__srv__UnloadNode_Request__Sequence * array)
{
  if (array) {
    composition_interfaces__srv__UnloadNode_Request__Sequence__fini(array);
  }
  free(array);
}

bool
composition_interfaces__srv__UnloadNode_Request__Sequence__are_equal(const composition_interfaces__srv__UnloadNode_Request__Sequence * lhs, const composition_interfaces__srv__UnloadNode_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!composition_interfaces__srv__UnloadNode_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
composition_interfaces__srv__UnloadNode_Request__Sequence__copy(
  const composition_interfaces__srv__UnloadNode_Request__Sequence * input,
  composition_interfaces__srv__UnloadNode_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(composition_interfaces__srv__UnloadNode_Request);
    composition_interfaces__srv__UnloadNode_Request * data =
      (composition_interfaces__srv__UnloadNode_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!composition_interfaces__srv__UnloadNode_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          composition_interfaces__srv__UnloadNode_Request__fini(&data[i]);
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
    if (!composition_interfaces__srv__UnloadNode_Request__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}


// Include directives for member types
// Member `error_message`
#include "rosidl_runtime_c/string_functions.h"

bool
composition_interfaces__srv__UnloadNode_Response__init(composition_interfaces__srv__UnloadNode_Response * msg)
{
  if (!msg) {
    return false;
  }
  // success
  // error_message
  if (!rosidl_runtime_c__String__init(&msg->error_message)) {
    composition_interfaces__srv__UnloadNode_Response__fini(msg);
    return false;
  }
  return true;
}

void
composition_interfaces__srv__UnloadNode_Response__fini(composition_interfaces__srv__UnloadNode_Response * msg)
{
  if (!msg) {
    return;
  }
  // success
  // error_message
  rosidl_runtime_c__String__fini(&msg->error_message);
}

bool
composition_interfaces__srv__UnloadNode_Response__are_equal(const composition_interfaces__srv__UnloadNode_Response * lhs, const composition_interfaces__srv__UnloadNode_Response * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // success
  if (lhs->success != rhs->success) {
    return false;
  }
  // error_message
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->error_message), &(rhs->error_message)))
  {
    return false;
  }
  return true;
}

bool
composition_interfaces__srv__UnloadNode_Response__copy(
  const composition_interfaces__srv__UnloadNode_Response * input,
  composition_interfaces__srv__UnloadNode_Response * output)
{
  if (!input || !output) {
    return false;
  }
  // success
  output->success = input->success;
  // error_message
  if (!rosidl_runtime_c__String__copy(
      &(input->error_message), &(output->error_message)))
  {
    return false;
  }
  return true;
}

composition_interfaces__srv__UnloadNode_Response *
composition_interfaces__srv__UnloadNode_Response__create()
{
  composition_interfaces__srv__UnloadNode_Response * msg = (composition_interfaces__srv__UnloadNode_Response *)malloc(sizeof(composition_interfaces__srv__UnloadNode_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(composition_interfaces__srv__UnloadNode_Response));
  bool success = composition_interfaces__srv__UnloadNode_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
composition_interfaces__srv__UnloadNode_Response__destroy(composition_interfaces__srv__UnloadNode_Response * msg)
{
  if (msg) {
    composition_interfaces__srv__UnloadNode_Response__fini(msg);
  }
  free(msg);
}


bool
composition_interfaces__srv__UnloadNode_Response__Sequence__init(composition_interfaces__srv__UnloadNode_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  composition_interfaces__srv__UnloadNode_Response * data = NULL;
  if (size) {
    data = (composition_interfaces__srv__UnloadNode_Response *)calloc(size, sizeof(composition_interfaces__srv__UnloadNode_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = composition_interfaces__srv__UnloadNode_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        composition_interfaces__srv__UnloadNode_Response__fini(&data[i - 1]);
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
composition_interfaces__srv__UnloadNode_Response__Sequence__fini(composition_interfaces__srv__UnloadNode_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      composition_interfaces__srv__UnloadNode_Response__fini(&array->data[i]);
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

composition_interfaces__srv__UnloadNode_Response__Sequence *
composition_interfaces__srv__UnloadNode_Response__Sequence__create(size_t size)
{
  composition_interfaces__srv__UnloadNode_Response__Sequence * array = (composition_interfaces__srv__UnloadNode_Response__Sequence *)malloc(sizeof(composition_interfaces__srv__UnloadNode_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = composition_interfaces__srv__UnloadNode_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
composition_interfaces__srv__UnloadNode_Response__Sequence__destroy(composition_interfaces__srv__UnloadNode_Response__Sequence * array)
{
  if (array) {
    composition_interfaces__srv__UnloadNode_Response__Sequence__fini(array);
  }
  free(array);
}

bool
composition_interfaces__srv__UnloadNode_Response__Sequence__are_equal(const composition_interfaces__srv__UnloadNode_Response__Sequence * lhs, const composition_interfaces__srv__UnloadNode_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!composition_interfaces__srv__UnloadNode_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
composition_interfaces__srv__UnloadNode_Response__Sequence__copy(
  const composition_interfaces__srv__UnloadNode_Response__Sequence * input,
  composition_interfaces__srv__UnloadNode_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(composition_interfaces__srv__UnloadNode_Response);
    composition_interfaces__srv__UnloadNode_Response * data =
      (composition_interfaces__srv__UnloadNode_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!composition_interfaces__srv__UnloadNode_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          composition_interfaces__srv__UnloadNode_Response__fini(&data[i]);
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
    if (!composition_interfaces__srv__UnloadNode_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
