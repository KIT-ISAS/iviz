// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/RobotConfiguration.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/robot_configuration__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `source_parameter`
// Member `saved_robot_name`
// Member `frame_prefix`
// Member `frame_suffix`
// Member `id`
#include "rosidl_runtime_c/string_functions.h"
// Member `tint`
#include "std_msgs/msg/detail/color_rgba__functions.h"

bool
iviz_msgs__msg__RobotConfiguration__init(iviz_msgs__msg__RobotConfiguration * msg)
{
  if (!msg) {
    return false;
  }
  // source_parameter
  if (!rosidl_runtime_c__String__init(&msg->source_parameter)) {
    iviz_msgs__msg__RobotConfiguration__fini(msg);
    return false;
  }
  // saved_robot_name
  if (!rosidl_runtime_c__String__init(&msg->saved_robot_name)) {
    iviz_msgs__msg__RobotConfiguration__fini(msg);
    return false;
  }
  // frame_prefix
  if (!rosidl_runtime_c__String__init(&msg->frame_prefix)) {
    iviz_msgs__msg__RobotConfiguration__fini(msg);
    return false;
  }
  // frame_suffix
  if (!rosidl_runtime_c__String__init(&msg->frame_suffix)) {
    iviz_msgs__msg__RobotConfiguration__fini(msg);
    return false;
  }
  // attached_to_tf
  // render_as_occlusion_only
  // tint
  if (!std_msgs__msg__ColorRGBA__init(&msg->tint)) {
    iviz_msgs__msg__RobotConfiguration__fini(msg);
    return false;
  }
  // metallic
  // smoothness
  // id
  if (!rosidl_runtime_c__String__init(&msg->id)) {
    iviz_msgs__msg__RobotConfiguration__fini(msg);
    return false;
  }
  // visible
  return true;
}

void
iviz_msgs__msg__RobotConfiguration__fini(iviz_msgs__msg__RobotConfiguration * msg)
{
  if (!msg) {
    return;
  }
  // source_parameter
  rosidl_runtime_c__String__fini(&msg->source_parameter);
  // saved_robot_name
  rosidl_runtime_c__String__fini(&msg->saved_robot_name);
  // frame_prefix
  rosidl_runtime_c__String__fini(&msg->frame_prefix);
  // frame_suffix
  rosidl_runtime_c__String__fini(&msg->frame_suffix);
  // attached_to_tf
  // render_as_occlusion_only
  // tint
  std_msgs__msg__ColorRGBA__fini(&msg->tint);
  // metallic
  // smoothness
  // id
  rosidl_runtime_c__String__fini(&msg->id);
  // visible
}

bool
iviz_msgs__msg__RobotConfiguration__are_equal(const iviz_msgs__msg__RobotConfiguration * lhs, const iviz_msgs__msg__RobotConfiguration * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // source_parameter
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->source_parameter), &(rhs->source_parameter)))
  {
    return false;
  }
  // saved_robot_name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->saved_robot_name), &(rhs->saved_robot_name)))
  {
    return false;
  }
  // frame_prefix
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->frame_prefix), &(rhs->frame_prefix)))
  {
    return false;
  }
  // frame_suffix
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->frame_suffix), &(rhs->frame_suffix)))
  {
    return false;
  }
  // attached_to_tf
  if (lhs->attached_to_tf != rhs->attached_to_tf) {
    return false;
  }
  // render_as_occlusion_only
  if (lhs->render_as_occlusion_only != rhs->render_as_occlusion_only) {
    return false;
  }
  // tint
  if (!std_msgs__msg__ColorRGBA__are_equal(
      &(lhs->tint), &(rhs->tint)))
  {
    return false;
  }
  // metallic
  if (lhs->metallic != rhs->metallic) {
    return false;
  }
  // smoothness
  if (lhs->smoothness != rhs->smoothness) {
    return false;
  }
  // id
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->id), &(rhs->id)))
  {
    return false;
  }
  // visible
  if (lhs->visible != rhs->visible) {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__RobotConfiguration__copy(
  const iviz_msgs__msg__RobotConfiguration * input,
  iviz_msgs__msg__RobotConfiguration * output)
{
  if (!input || !output) {
    return false;
  }
  // source_parameter
  if (!rosidl_runtime_c__String__copy(
      &(input->source_parameter), &(output->source_parameter)))
  {
    return false;
  }
  // saved_robot_name
  if (!rosidl_runtime_c__String__copy(
      &(input->saved_robot_name), &(output->saved_robot_name)))
  {
    return false;
  }
  // frame_prefix
  if (!rosidl_runtime_c__String__copy(
      &(input->frame_prefix), &(output->frame_prefix)))
  {
    return false;
  }
  // frame_suffix
  if (!rosidl_runtime_c__String__copy(
      &(input->frame_suffix), &(output->frame_suffix)))
  {
    return false;
  }
  // attached_to_tf
  output->attached_to_tf = input->attached_to_tf;
  // render_as_occlusion_only
  output->render_as_occlusion_only = input->render_as_occlusion_only;
  // tint
  if (!std_msgs__msg__ColorRGBA__copy(
      &(input->tint), &(output->tint)))
  {
    return false;
  }
  // metallic
  output->metallic = input->metallic;
  // smoothness
  output->smoothness = input->smoothness;
  // id
  if (!rosidl_runtime_c__String__copy(
      &(input->id), &(output->id)))
  {
    return false;
  }
  // visible
  output->visible = input->visible;
  return true;
}

iviz_msgs__msg__RobotConfiguration *
iviz_msgs__msg__RobotConfiguration__create()
{
  iviz_msgs__msg__RobotConfiguration * msg = (iviz_msgs__msg__RobotConfiguration *)malloc(sizeof(iviz_msgs__msg__RobotConfiguration));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__RobotConfiguration));
  bool success = iviz_msgs__msg__RobotConfiguration__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__RobotConfiguration__destroy(iviz_msgs__msg__RobotConfiguration * msg)
{
  if (msg) {
    iviz_msgs__msg__RobotConfiguration__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__RobotConfiguration__Sequence__init(iviz_msgs__msg__RobotConfiguration__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__RobotConfiguration * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__RobotConfiguration *)calloc(size, sizeof(iviz_msgs__msg__RobotConfiguration));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__RobotConfiguration__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__RobotConfiguration__fini(&data[i - 1]);
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
iviz_msgs__msg__RobotConfiguration__Sequence__fini(iviz_msgs__msg__RobotConfiguration__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__RobotConfiguration__fini(&array->data[i]);
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

iviz_msgs__msg__RobotConfiguration__Sequence *
iviz_msgs__msg__RobotConfiguration__Sequence__create(size_t size)
{
  iviz_msgs__msg__RobotConfiguration__Sequence * array = (iviz_msgs__msg__RobotConfiguration__Sequence *)malloc(sizeof(iviz_msgs__msg__RobotConfiguration__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__RobotConfiguration__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__RobotConfiguration__Sequence__destroy(iviz_msgs__msg__RobotConfiguration__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__RobotConfiguration__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__RobotConfiguration__Sequence__are_equal(const iviz_msgs__msg__RobotConfiguration__Sequence * lhs, const iviz_msgs__msg__RobotConfiguration__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__RobotConfiguration__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__RobotConfiguration__Sequence__copy(
  const iviz_msgs__msg__RobotConfiguration__Sequence * input,
  iviz_msgs__msg__RobotConfiguration__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__RobotConfiguration);
    iviz_msgs__msg__RobotConfiguration * data =
      (iviz_msgs__msg__RobotConfiguration *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__RobotConfiguration__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__RobotConfiguration__fini(&data[i]);
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
    if (!iviz_msgs__msg__RobotConfiguration__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
