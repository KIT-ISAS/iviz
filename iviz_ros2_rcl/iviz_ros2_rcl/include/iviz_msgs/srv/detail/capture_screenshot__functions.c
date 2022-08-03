// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:srv/CaptureScreenshot.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/srv/detail/capture_screenshot__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

bool
iviz_msgs__srv__CaptureScreenshot_Request__init(iviz_msgs__srv__CaptureScreenshot_Request * msg)
{
  if (!msg) {
    return false;
  }
  // compress
  return true;
}

void
iviz_msgs__srv__CaptureScreenshot_Request__fini(iviz_msgs__srv__CaptureScreenshot_Request * msg)
{
  if (!msg) {
    return;
  }
  // compress
}

bool
iviz_msgs__srv__CaptureScreenshot_Request__are_equal(const iviz_msgs__srv__CaptureScreenshot_Request * lhs, const iviz_msgs__srv__CaptureScreenshot_Request * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // compress
  if (lhs->compress != rhs->compress) {
    return false;
  }
  return true;
}

bool
iviz_msgs__srv__CaptureScreenshot_Request__copy(
  const iviz_msgs__srv__CaptureScreenshot_Request * input,
  iviz_msgs__srv__CaptureScreenshot_Request * output)
{
  if (!input || !output) {
    return false;
  }
  // compress
  output->compress = input->compress;
  return true;
}

iviz_msgs__srv__CaptureScreenshot_Request *
iviz_msgs__srv__CaptureScreenshot_Request__create()
{
  iviz_msgs__srv__CaptureScreenshot_Request * msg = (iviz_msgs__srv__CaptureScreenshot_Request *)malloc(sizeof(iviz_msgs__srv__CaptureScreenshot_Request));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__srv__CaptureScreenshot_Request));
  bool success = iviz_msgs__srv__CaptureScreenshot_Request__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__srv__CaptureScreenshot_Request__destroy(iviz_msgs__srv__CaptureScreenshot_Request * msg)
{
  if (msg) {
    iviz_msgs__srv__CaptureScreenshot_Request__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__srv__CaptureScreenshot_Request__Sequence__init(iviz_msgs__srv__CaptureScreenshot_Request__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__srv__CaptureScreenshot_Request * data = NULL;
  if (size) {
    data = (iviz_msgs__srv__CaptureScreenshot_Request *)calloc(size, sizeof(iviz_msgs__srv__CaptureScreenshot_Request));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__srv__CaptureScreenshot_Request__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__srv__CaptureScreenshot_Request__fini(&data[i - 1]);
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
iviz_msgs__srv__CaptureScreenshot_Request__Sequence__fini(iviz_msgs__srv__CaptureScreenshot_Request__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__srv__CaptureScreenshot_Request__fini(&array->data[i]);
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

iviz_msgs__srv__CaptureScreenshot_Request__Sequence *
iviz_msgs__srv__CaptureScreenshot_Request__Sequence__create(size_t size)
{
  iviz_msgs__srv__CaptureScreenshot_Request__Sequence * array = (iviz_msgs__srv__CaptureScreenshot_Request__Sequence *)malloc(sizeof(iviz_msgs__srv__CaptureScreenshot_Request__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__srv__CaptureScreenshot_Request__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__srv__CaptureScreenshot_Request__Sequence__destroy(iviz_msgs__srv__CaptureScreenshot_Request__Sequence * array)
{
  if (array) {
    iviz_msgs__srv__CaptureScreenshot_Request__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__srv__CaptureScreenshot_Request__Sequence__are_equal(const iviz_msgs__srv__CaptureScreenshot_Request__Sequence * lhs, const iviz_msgs__srv__CaptureScreenshot_Request__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__srv__CaptureScreenshot_Request__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__srv__CaptureScreenshot_Request__Sequence__copy(
  const iviz_msgs__srv__CaptureScreenshot_Request__Sequence * input,
  iviz_msgs__srv__CaptureScreenshot_Request__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__srv__CaptureScreenshot_Request);
    iviz_msgs__srv__CaptureScreenshot_Request * data =
      (iviz_msgs__srv__CaptureScreenshot_Request *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__srv__CaptureScreenshot_Request__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__srv__CaptureScreenshot_Request__fini(&data[i]);
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
    if (!iviz_msgs__srv__CaptureScreenshot_Request__copy(
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
// Member `header`
#include "std_msgs/msg/detail/header__functions.h"
// Member `pose`
#include "geometry_msgs/msg/detail/pose__functions.h"
// Member `data`
#include "rosidl_runtime_c/primitives_sequence_functions.h"

bool
iviz_msgs__srv__CaptureScreenshot_Response__init(iviz_msgs__srv__CaptureScreenshot_Response * msg)
{
  if (!msg) {
    return false;
  }
  // success
  // message
  if (!rosidl_runtime_c__String__init(&msg->message)) {
    iviz_msgs__srv__CaptureScreenshot_Response__fini(msg);
    return false;
  }
  // header
  if (!std_msgs__msg__Header__init(&msg->header)) {
    iviz_msgs__srv__CaptureScreenshot_Response__fini(msg);
    return false;
  }
  // width
  // height
  // bpp
  // intrinsics
  // pose
  if (!geometry_msgs__msg__Pose__init(&msg->pose)) {
    iviz_msgs__srv__CaptureScreenshot_Response__fini(msg);
    return false;
  }
  // data
  if (!rosidl_runtime_c__octet__Sequence__init(&msg->data, 0)) {
    iviz_msgs__srv__CaptureScreenshot_Response__fini(msg);
    return false;
  }
  return true;
}

void
iviz_msgs__srv__CaptureScreenshot_Response__fini(iviz_msgs__srv__CaptureScreenshot_Response * msg)
{
  if (!msg) {
    return;
  }
  // success
  // message
  rosidl_runtime_c__String__fini(&msg->message);
  // header
  std_msgs__msg__Header__fini(&msg->header);
  // width
  // height
  // bpp
  // intrinsics
  // pose
  geometry_msgs__msg__Pose__fini(&msg->pose);
  // data
  rosidl_runtime_c__octet__Sequence__fini(&msg->data);
}

bool
iviz_msgs__srv__CaptureScreenshot_Response__are_equal(const iviz_msgs__srv__CaptureScreenshot_Response * lhs, const iviz_msgs__srv__CaptureScreenshot_Response * rhs)
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
  // header
  if (!std_msgs__msg__Header__are_equal(
      &(lhs->header), &(rhs->header)))
  {
    return false;
  }
  // width
  if (lhs->width != rhs->width) {
    return false;
  }
  // height
  if (lhs->height != rhs->height) {
    return false;
  }
  // bpp
  if (lhs->bpp != rhs->bpp) {
    return false;
  }
  // intrinsics
  for (size_t i = 0; i < 9; ++i) {
    if (lhs->intrinsics[i] != rhs->intrinsics[i]) {
      return false;
    }
  }
  // pose
  if (!geometry_msgs__msg__Pose__are_equal(
      &(lhs->pose), &(rhs->pose)))
  {
    return false;
  }
  // data
  if (!rosidl_runtime_c__octet__Sequence__are_equal(
      &(lhs->data), &(rhs->data)))
  {
    return false;
  }
  return true;
}

bool
iviz_msgs__srv__CaptureScreenshot_Response__copy(
  const iviz_msgs__srv__CaptureScreenshot_Response * input,
  iviz_msgs__srv__CaptureScreenshot_Response * output)
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
  // header
  if (!std_msgs__msg__Header__copy(
      &(input->header), &(output->header)))
  {
    return false;
  }
  // width
  output->width = input->width;
  // height
  output->height = input->height;
  // bpp
  output->bpp = input->bpp;
  // intrinsics
  for (size_t i = 0; i < 9; ++i) {
    output->intrinsics[i] = input->intrinsics[i];
  }
  // pose
  if (!geometry_msgs__msg__Pose__copy(
      &(input->pose), &(output->pose)))
  {
    return false;
  }
  // data
  if (!rosidl_runtime_c__octet__Sequence__copy(
      &(input->data), &(output->data)))
  {
    return false;
  }
  return true;
}

iviz_msgs__srv__CaptureScreenshot_Response *
iviz_msgs__srv__CaptureScreenshot_Response__create()
{
  iviz_msgs__srv__CaptureScreenshot_Response * msg = (iviz_msgs__srv__CaptureScreenshot_Response *)malloc(sizeof(iviz_msgs__srv__CaptureScreenshot_Response));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__srv__CaptureScreenshot_Response));
  bool success = iviz_msgs__srv__CaptureScreenshot_Response__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__srv__CaptureScreenshot_Response__destroy(iviz_msgs__srv__CaptureScreenshot_Response * msg)
{
  if (msg) {
    iviz_msgs__srv__CaptureScreenshot_Response__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__srv__CaptureScreenshot_Response__Sequence__init(iviz_msgs__srv__CaptureScreenshot_Response__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__srv__CaptureScreenshot_Response * data = NULL;
  if (size) {
    data = (iviz_msgs__srv__CaptureScreenshot_Response *)calloc(size, sizeof(iviz_msgs__srv__CaptureScreenshot_Response));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__srv__CaptureScreenshot_Response__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__srv__CaptureScreenshot_Response__fini(&data[i - 1]);
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
iviz_msgs__srv__CaptureScreenshot_Response__Sequence__fini(iviz_msgs__srv__CaptureScreenshot_Response__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__srv__CaptureScreenshot_Response__fini(&array->data[i]);
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

iviz_msgs__srv__CaptureScreenshot_Response__Sequence *
iviz_msgs__srv__CaptureScreenshot_Response__Sequence__create(size_t size)
{
  iviz_msgs__srv__CaptureScreenshot_Response__Sequence * array = (iviz_msgs__srv__CaptureScreenshot_Response__Sequence *)malloc(sizeof(iviz_msgs__srv__CaptureScreenshot_Response__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__srv__CaptureScreenshot_Response__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__srv__CaptureScreenshot_Response__Sequence__destroy(iviz_msgs__srv__CaptureScreenshot_Response__Sequence * array)
{
  if (array) {
    iviz_msgs__srv__CaptureScreenshot_Response__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__srv__CaptureScreenshot_Response__Sequence__are_equal(const iviz_msgs__srv__CaptureScreenshot_Response__Sequence * lhs, const iviz_msgs__srv__CaptureScreenshot_Response__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__srv__CaptureScreenshot_Response__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__srv__CaptureScreenshot_Response__Sequence__copy(
  const iviz_msgs__srv__CaptureScreenshot_Response__Sequence * input,
  iviz_msgs__srv__CaptureScreenshot_Response__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__srv__CaptureScreenshot_Response);
    iviz_msgs__srv__CaptureScreenshot_Response * data =
      (iviz_msgs__srv__CaptureScreenshot_Response *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__srv__CaptureScreenshot_Response__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__srv__CaptureScreenshot_Response__fini(&data[i]);
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
    if (!iviz_msgs__srv__CaptureScreenshot_Response__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
