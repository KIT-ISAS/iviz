// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/ARMarker.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/ar_marker__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `code`
#include "rosidl_runtime_c/string_functions.h"
// Member `corners`
#include "geometry_msgs/msg/detail/vector3__functions.h"
// Member `camera_pose`
// Member `pose_relative_to_camera`
#include "geometry_msgs/msg/detail/pose__functions.h"

bool
iviz_msgs__msg__ARMarker__init(iviz_msgs__msg__ARMarker * msg)
{
  if (!msg) {
    return false;
  }
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    iviz_msgs__msg__ARMarker__fini(msg);
    return false;
  }
  // type
  // code
  if (!rosidl_runtime_c__String__init(&msg->code)) {
    iviz_msgs__msg__ARMarker__fini(msg);
    return false;
  }
  // corners
  for (size_t i = 0; i < 4; ++i) {
    if (!geometry_msgs__msg__Vector3__init(&msg->corners[i])) {
      iviz_msgs__msg__ARMarker__fini(msg);
      return false;
    }
  }
  // camera_intrinsic
  // camera_pose
  if (!geometry_msgs__msg__Pose__init(&msg->camera_pose)) {
    iviz_msgs__msg__ARMarker__fini(msg);
    return false;
  }
  // has_reliable_pose
  // marker_size_in_mm
  // pose_relative_to_camera
  if (!geometry_msgs__msg__Pose__init(&msg->pose_relative_to_camera)) {
    iviz_msgs__msg__ARMarker__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__msg__ARMarker__fini(iviz_msgs__msg__ARMarker * msg)
{
  if (!msg) {
    return;
  }
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // type
  // code
  rosidl_runtime_c__String__fini(&msg->code);
  // corners
  for (size_t i = 0; i < 4; ++i) {
    geometry_msgs__msg__Vector3__fini(&msg->corners[i]);
  }
  // camera_intrinsic
  // camera_pose
  geometry_msgs__msg__Pose__fini(&msg->camera_pose);
  // has_reliable_pose
  // marker_size_in_mm
  // pose_relative_to_camera
  geometry_msgs__msg__Pose__fini(&msg->pose_relative_to_camera);
}

bool
iviz_msgs__msg__ARMarker__are_equal(const iviz_msgs__msg__ARMarker * lhs, const iviz_msgs__msg__ARMarker * rhs)
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
  // type
  if (lhs->type != rhs->type) {
    return false;
  }
  // code
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->code), &(rhs->code)))
  {
    return false;
  }
  // corners
  for (size_t i = 0; i < 4; ++i) {
    if (!geometry_msgs__msg__Vector3__are_equal(
        &(lhs->corners[i]), &(rhs->corners[i])))
    {
      return false;
    }
  }
  // camera_intrinsic
  for (size_t i = 0; i < 9; ++i) {
    if (lhs->camera_intrinsic[i] != rhs->camera_intrinsic[i]) {
      return false;
    }
  }
  // camera_pose
  if (!geometry_msgs__msg__Pose__are_equal(
      &(lhs->camera_pose), &(rhs->camera_pose)))
  {
    return false;
  }
  // has_reliable_pose
  if (lhs->has_reliable_pose != rhs->has_reliable_pose) {
    return false;
  }
  // marker_size_in_mm
  if (lhs->marker_size_in_mm != rhs->marker_size_in_mm) {
    return false;
  }
  // pose_relative_to_camera
  if (!geometry_msgs__msg__Pose__are_equal(
      &(lhs->pose_relative_to_camera), &(rhs->pose_relative_to_camera)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__ARMarker__copy(
  const iviz_msgs__msg__ARMarker * input,
  iviz_msgs__msg__ARMarker * output)
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
  // type
  output->type = input->type;
  // code
  if (!rosidl_runtime_c__String__copy(
      &(input->code), &(output->code)))
  {
    return false;
  }
  // corners
  for (size_t i = 0; i < 4; ++i) {
    if (!geometry_msgs__msg__Vector3__copy(
        &(input->corners[i]), &(output->corners[i])))
    {
      return false;
    }
  }
  // camera_intrinsic
  for (size_t i = 0; i < 9; ++i) {
    output->camera_intrinsic[i] = input->camera_intrinsic[i];
  }
  // camera_pose
  if (!geometry_msgs__msg__Pose__copy(
      &(input->camera_pose), &(output->camera_pose)))
  {
    return false;
  }
  // has_reliable_pose
  output->has_reliable_pose = input->has_reliable_pose;
  // marker_size_in_mm
  output->marker_size_in_mm = input->marker_size_in_mm;
  // pose_relative_to_camera
  if (!geometry_msgs__msg__Pose__copy(
      &(input->pose_relative_to_camera), &(output->pose_relative_to_camera)))
  {
    return false;
  }
  return true;
}

iviz_msgs__msg__ARMarker *
iviz_msgs__msg__ARMarker__create()
{
  iviz_msgs__msg__ARMarker * msg = (iviz_msgs__msg__ARMarker *)malloc(sizeof(iviz_msgs__msg__ARMarker));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__ARMarker));
  bool success = iviz_msgs__msg__ARMarker__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__ARMarker__destroy(iviz_msgs__msg__ARMarker * msg)
{
  if (msg) {
    iviz_msgs__msg__ARMarker__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__ARMarker__Sequence__init(iviz_msgs__msg__ARMarker__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__ARMarker * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__ARMarker *)calloc(size, sizeof(iviz_msgs__msg__ARMarker));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__ARMarker__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__ARMarker__fini(&data[i - 1]);
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
iviz_msgs__msg__ARMarker__Sequence__fini(iviz_msgs__msg__ARMarker__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__ARMarker__fini(&array->data[i]);
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

iviz_msgs__msg__ARMarker__Sequence *
iviz_msgs__msg__ARMarker__Sequence__create(size_t size)
{
  iviz_msgs__msg__ARMarker__Sequence * array = (iviz_msgs__msg__ARMarker__Sequence *)malloc(sizeof(iviz_msgs__msg__ARMarker__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__ARMarker__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__ARMarker__Sequence__destroy(iviz_msgs__msg__ARMarker__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__ARMarker__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__ARMarker__Sequence__are_equal(const iviz_msgs__msg__ARMarker__Sequence * lhs, const iviz_msgs__msg__ARMarker__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__ARMarker__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__ARMarker__Sequence__copy(
  const iviz_msgs__msg__ARMarker__Sequence * input,
  iviz_msgs__msg__ARMarker__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__ARMarker);
    iviz_msgs__msg__ARMarker * data =
      (iviz_msgs__msg__ARMarker *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__ARMarker__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__ARMarker__fini(&data[i]);
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
    if (!iviz_msgs__msg__ARMarker__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
