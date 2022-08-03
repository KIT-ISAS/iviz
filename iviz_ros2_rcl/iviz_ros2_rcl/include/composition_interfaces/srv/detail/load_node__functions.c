// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from composition_interfaces:srv/LoadNode.idl
// generated code does not contain a copyright notice
#include "composition_interfaces/srv/detail/load_node__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

// Include directives for member types
// Member `package_name`
// Member `plugin_name`
// Member `node_name`
// Member `node_namespace`
// Member `remap_rules`
#include "rosidl_runtime_c/string_functions.h"
// Member `parameters`
// Member `extra_arguments`
#include "rcl_interfaces/msg/detail/parameter__functions.h"

bool
composition_interfaces__srv__LoadNode_Request__init(composition_interfaces__srv__LoadNode_Request * msg)
{
  if (!msg) {
    return false;
  }
  // package_name
  if (!rosidl_runtime_c__String__init(&msg->package_name)) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
    return false;
  }
  // plugin_name
  if (!rosidl_runtime_c__String__init(&msg->plugin_name)) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
    return false;
  }
  // node_name
  if (!rosidl_runtime_c__String__init(&msg->node_name)) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
    return false;
  }
  // node_namespace
  if (!rosidl_runtime_c__String__init(&msg->node_namespace)) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
    return false;
  }
  // log_level
  // remap_rules
  if (!rosidl_runtime_c__String__Sequence__init(&msg->remap_rules, 0)) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
    return false;
  }
  // parameters
  if (!rcl_interfaces__msg__Parameter__Sequence__init(&msg->parameters, 0)) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
    return false;
  }
  // extra_arguments
  if (!rcl_interfaces__msg__Parameter__Sequence__init(&msg->extra_arguments, 0)) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
    return false;
  }
  return true;
}

void
composition_interfaces__srv__LoadNode_Request__fini(composition_interfaces__srv__LoadNode_Request * msg)
{
  if (!msg) {
    return;
  }
  // package_name
  rosidl_runtime_c__String__fini(&msg->package_name);
  // plugin_name
  rosidl_runtime_c__String__fini(&msg->plugin_name);
  // node_name
  rosidl_runtime_c__String__fini(&msg->node_name);
  // node_namespace
  rosidl_runtime_c__String__fini(&msg->node_namespace);
  // log_level
  // remap_rules
  rosidl_runtime_c__String__Sequence__fini(&msg->remap_rules);
  // parameters
  rcl_interfaces__msg__Parameter__Sequence__fini(&msg->parameters);
  // extra_arguments
  rcl_interfaces__msg__Parameter__Sequence__fini(&msg->extra_arguments);
}

bool
composition_interfaces__srv__LoadNode_Request__are_equal(const composition_interfaces__srv__LoadNode_Request * lhs, const composition_interfaces__srv__LoadNode_Request * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // package_name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->package_name), &(rhs->package_name)))
  {
    return false;
  }
  // plugin_name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->plugin_name), &(rhs->plugin_name)))
  {
    return false;
  }
  // node_name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->node_name), &(rhs->node_name)))
  {
    return false;
  }
  // node_namespace
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->node_namespace), &(rhs->node_namespace)))
  {
    return false;
  }
  // log_level
  if (lhs->log_level != rhs->log_level) {
    return false;
  }
  // remap_rules
  if (!rosidl_runtime_c__String__Sequence__are_equal(
      &(lhs->remap_rules), &(rhs->remap_rules)))
  {
    return false;
  }
  // parameters
  if (!rcl_interfaces__msg__Parameter__Sequence__are_equal(
      &(lhs->parameters), &(rhs->parameters)))
  {
    return false;
  }
  // extra_arguments
  if (!rcl_interfaces__msg__Parameter__Sequence__are_equal(
      &(lhs->extra_arguments), &(rhs->extra_arguments)))
  {
    return false;
  }
  return true;
}

bool
composition_interfaces__srv__LoadNode_Request__copy(
  const composition_interfaces__srv__LoadNode_Request * input,
  composition_interfaces__srv__LoadNode_Request * output)
{
  if (!input || !output) {
    return false;
  }
  // package_name
  if (!rosidl_runtime_c__String__copy(
      &(input->package_name), &(output->package_name)))
  {
    return false;
  }
  // plugin_name
  if (!rosidl_runtime_c__String__copy(
      &(input->plugin_name), &(output->plugin_name)))
  {
    return false;
  }
  // node_name
  if (!rosidl_runtime_c__String__copy(
      &(input->node_name), &(output->node_name)))
  {
    return false;
  }
  // node_namespace
  if (!rosidl_runtime_c__String__copy(
      &(input->node_namespace), &(output->node_namespace)))
  {
    return false;
  }
  // log_level
  output->log_level = input->log_level;
  // remap_rules
  if (!rosidl_runtime_c__String__Sequence__copy(
      &(input->remap_rules), &(output->remap_rules)))
  {
    return false;
  }
  // parameters
  if (!rcl_interfaces__msg__Parameter__Sequence__copy(
      &(input->parameters), &(output->parameters)))
  {
    return false;
  }
  // extra_arguments
  if (!rcl_interfaces__msg__Parameter__Sequence__copy(
      &(input->extra_arguments), &(output->extra_arguments)))
  {
    return false;
  }
  return true;
}

composition_interfaces__srv__LoadNode_Request *
composition_interfaces__srv__LoadNode_Request__create()
{
  composition_interfaces__srv__LoadNode_Request * msg = (composition_interfaces__srv__LoadNode_Request *)malloc(sizeof(composition_interfaces__srv__LoadNode_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(composition_interfaces__srv__LoadNode_Request));
  bool success = composition_interfaces__srv__LoadNode_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
composition_interfaces__srv__LoadNode_Request__destroy(composition_interfaces__srv__LoadNode_Request * msg)
{
  if (msg) {
    composition_interfaces__srv__LoadNode_Request__fini(msg);
  }
  free(msg);
}


bool
composition_interfaces__srv__LoadNode_Request__Sequence__init(composition_interfaces__srv__LoadNode_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  composition_interfaces__srv__LoadNode_Request * data = NULL;
  if (size) {
    data = (composition_interfaces__srv__LoadNode_Request *)calloc(size, sizeof(composition_interfaces__srv__LoadNode_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = composition_interfaces__srv__LoadNode_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        composition_interfaces__srv__LoadNode_Request__fini(&data[i - 1]);
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
composition_interfaces__srv__LoadNode_Request__Sequence__fini(composition_interfaces__srv__LoadNode_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      composition_interfaces__srv__LoadNode_Request__fini(&array->data[i]);
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

composition_interfaces__srv__LoadNode_Request__Sequence *
composition_interfaces__srv__LoadNode_Request__Sequence__create(size_t size)
{
  composition_interfaces__srv__LoadNode_Request__Sequence * array = (composition_interfaces__srv__LoadNode_Request__Sequence *)malloc(sizeof(composition_interfaces__srv__LoadNode_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = composition_interfaces__srv__LoadNode_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
composition_interfaces__srv__LoadNode_Request__Sequence__destroy(composition_interfaces__srv__LoadNode_Request__Sequence * array)
{
  if (array) {
    composition_interfaces__srv__LoadNode_Request__Sequence__fini(array);
  }
  free(array);
}

bool
composition_interfaces__srv__LoadNode_Request__Sequence__are_equal(const composition_interfaces__srv__LoadNode_Request__Sequence * lhs, const composition_interfaces__srv__LoadNode_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!composition_interfaces__srv__LoadNode_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
composition_interfaces__srv__LoadNode_Request__Sequence__copy(
  const composition_interfaces__srv__LoadNode_Request__Sequence * input,
  composition_interfaces__srv__LoadNode_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(composition_interfaces__srv__LoadNode_Request);
    composition_interfaces__srv__LoadNode_Request * data =
      (composition_interfaces__srv__LoadNode_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!composition_interfaces__srv__LoadNode_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          composition_interfaces__srv__LoadNode_Request__fini(&data[i]);
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
    if (!composition_interfaces__srv__LoadNode_Request__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}


// Include directives for member types
// Member `error_message`
// Member `full_node_name`
// already included above
// #include "rosidl_runtime_c/string_functions.h"

bool
composition_interfaces__srv__LoadNode_Response__init(composition_interfaces__srv__LoadNode_Response * msg)
{
  if (!msg) {
    return false;
  }
  // success
  // error_message
  if (!rosidl_runtime_c__String__init(&msg->error_message)) {
    composition_interfaces__srv__LoadNode_Response__fini(msg);
    return false;
  }
  // full_node_name
  if (!rosidl_runtime_c__String__init(&msg->full_node_name)) {
    composition_interfaces__srv__LoadNode_Response__fini(msg);
    return false;
  }
  // unique_id
  return true;
}

void
composition_interfaces__srv__LoadNode_Response__fini(composition_interfaces__srv__LoadNode_Response * msg)
{
  if (!msg) {
    return;
  }
  // success
  // error_message
  rosidl_runtime_c__String__fini(&msg->error_message);
  // full_node_name
  rosidl_runtime_c__String__fini(&msg->full_node_name);
  // unique_id
}

bool
composition_interfaces__srv__LoadNode_Response__are_equal(const composition_interfaces__srv__LoadNode_Response * lhs, const composition_interfaces__srv__LoadNode_Response * rhs)
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
  // full_node_name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->full_node_name), &(rhs->full_node_name)))
  {
    return false;
  }
  // unique_id
  if (lhs->unique_id != rhs->unique_id) {
    return false;
  }
  return true;
}

bool
composition_interfaces__srv__LoadNode_Response__copy(
  const composition_interfaces__srv__LoadNode_Response * input,
  composition_interfaces__srv__LoadNode_Response * output)
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
  // full_node_name
  if (!rosidl_runtime_c__String__copy(
      &(input->full_node_name), &(output->full_node_name)))
  {
    return false;
  }
  // unique_id
  output->unique_id = input->unique_id;
  return true;
}

composition_interfaces__srv__LoadNode_Response *
composition_interfaces__srv__LoadNode_Response__create()
{
  composition_interfaces__srv__LoadNode_Response * msg = (composition_interfaces__srv__LoadNode_Response *)malloc(sizeof(composition_interfaces__srv__LoadNode_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(composition_interfaces__srv__LoadNode_Response));
  bool success = composition_interfaces__srv__LoadNode_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
composition_interfaces__srv__LoadNode_Response__destroy(composition_interfaces__srv__LoadNode_Response * msg)
{
  if (msg) {
    composition_interfaces__srv__LoadNode_Response__fini(msg);
  }
  free(msg);
}


bool
composition_interfaces__srv__LoadNode_Response__Sequence__init(composition_interfaces__srv__LoadNode_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  composition_interfaces__srv__LoadNode_Response * data = NULL;
  if (size) {
    data = (composition_interfaces__srv__LoadNode_Response *)calloc(size, sizeof(composition_interfaces__srv__LoadNode_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = composition_interfaces__srv__LoadNode_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        composition_interfaces__srv__LoadNode_Response__fini(&data[i - 1]);
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
composition_interfaces__srv__LoadNode_Response__Sequence__fini(composition_interfaces__srv__LoadNode_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      composition_interfaces__srv__LoadNode_Response__fini(&array->data[i]);
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

composition_interfaces__srv__LoadNode_Response__Sequence *
composition_interfaces__srv__LoadNode_Response__Sequence__create(size_t size)
{
  composition_interfaces__srv__LoadNode_Response__Sequence * array = (composition_interfaces__srv__LoadNode_Response__Sequence *)malloc(sizeof(composition_interfaces__srv__LoadNode_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = composition_interfaces__srv__LoadNode_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
composition_interfaces__srv__LoadNode_Response__Sequence__destroy(composition_interfaces__srv__LoadNode_Response__Sequence * array)
{
  if (array) {
    composition_interfaces__srv__LoadNode_Response__Sequence__fini(array);
  }
  free(array);
}

bool
composition_interfaces__srv__LoadNode_Response__Sequence__are_equal(const composition_interfaces__srv__LoadNode_Response__Sequence * lhs, const composition_interfaces__srv__LoadNode_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!composition_interfaces__srv__LoadNode_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
composition_interfaces__srv__LoadNode_Response__Sequence__copy(
  const composition_interfaces__srv__LoadNode_Response__Sequence * input,
  composition_interfaces__srv__LoadNode_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(composition_interfaces__srv__LoadNode_Response);
    composition_interfaces__srv__LoadNode_Response * data =
      (composition_interfaces__srv__LoadNode_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!composition_interfaces__srv__LoadNode_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          composition_interfaces__srv__LoadNode_Response__fini(&data[i]);
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
    if (!composition_interfaces__srv__LoadNode_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
