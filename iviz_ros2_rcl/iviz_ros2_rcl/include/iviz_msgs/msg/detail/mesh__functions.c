// generated from rosidl_generator_c/resource/idl__functions.c.em
// with input from iviz_msgs:msg/Mesh.idl
// generated code does not contain a copyright notice
#include "iviz_msgs/msg/detail/mesh__functions.h"

#include <assert.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>


// Include directives for member types
// Member `name`
#include "rosidl_runtime_c/string_functions.h"
// Member `vertices`
// Member `normals`
// Member `tangents`
// Member `bi_tangents`
#include "iviz_msgs/msg/detail/vector3f__functions.h"
// Member `tex_coords`
#include "iviz_msgs/msg/detail/tex_coords__functions.h"
// Member `color_channels`
#include "iviz_msgs/msg/detail/color_channel__functions.h"
// Member `faces`
#include "iviz_msgs/msg/detail/triangle__functions.h"

bool
iviz_msgs__msg__Mesh__init(iviz_msgs__msg__Mesh * msg)
{
  if (!msg) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__init(&msg->name)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // vertices
  if (!iviz_msgs__msg__Vector3f__Sequence__init(&msg->vertices, 0)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // normals
  if (!iviz_msgs__msg__Vector3f__Sequence__init(&msg->normals, 0)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // tangents
  if (!iviz_msgs__msg__Vector3f__Sequence__init(&msg->tangents, 0)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // bi_tangents
  if (!iviz_msgs__msg__Vector3f__Sequence__init(&msg->bi_tangents, 0)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // tex_coords
  if (!iviz_msgs__msg__TexCoords__Sequence__init(&msg->tex_coords, 0)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // color_channels
  if (!iviz_msgs__msg__ColorChannel__Sequence__init(&msg->color_channels, 0)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // faces
  if (!iviz_msgs__msg__Triangle__Sequence__init(&msg->faces, 0)) {
    iviz_msgs__msg__Mesh__fini(msg);
    return false;
  }
  // material_index
  return true;
}

void
iviz_msgs__msg__Mesh__fini(iviz_msgs__msg__Mesh * msg)
{
  if (!msg) {
    return;
  }
  // name
  rosidl_runtime_c__String__fini(&msg->name);
  // vertices
  iviz_msgs__msg__Vector3f__Sequence__fini(&msg->vertices);
  // normals
  iviz_msgs__msg__Vector3f__Sequence__fini(&msg->normals);
  // tangents
  iviz_msgs__msg__Vector3f__Sequence__fini(&msg->tangents);
  // bi_tangents
  iviz_msgs__msg__Vector3f__Sequence__fini(&msg->bi_tangents);
  // tex_coords
  iviz_msgs__msg__TexCoords__Sequence__fini(&msg->tex_coords);
  // color_channels
  iviz_msgs__msg__ColorChannel__Sequence__fini(&msg->color_channels);
  // faces
  iviz_msgs__msg__Triangle__Sequence__fini(&msg->faces);
  // material_index
}

bool
iviz_msgs__msg__Mesh__are_equal(const iviz_msgs__msg__Mesh * lhs, const iviz_msgs__msg__Mesh * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__are_equal(
      &(lhs->name), &(rhs->name)))
  {
    return false;
  }
  // vertices
  if (!iviz_msgs__msg__Vector3f__Sequence__are_equal(
      &(lhs->vertices), &(rhs->vertices)))
  {
    return false;
  }
  // normals
  if (!iviz_msgs__msg__Vector3f__Sequence__are_equal(
      &(lhs->normals), &(rhs->normals)))
  {
    return false;
  }
  // tangents
  if (!iviz_msgs__msg__Vector3f__Sequence__are_equal(
      &(lhs->tangents), &(rhs->tangents)))
  {
    return false;
  }
  // bi_tangents
  if (!iviz_msgs__msg__Vector3f__Sequence__are_equal(
      &(lhs->bi_tangents), &(rhs->bi_tangents)))
  {
    return false;
  }
  // tex_coords
  if (!iviz_msgs__msg__TexCoords__Sequence__are_equal(
      &(lhs->tex_coords), &(rhs->tex_coords)))
  {
    return false;
  }
  // color_channels
  if (!iviz_msgs__msg__ColorChannel__Sequence__are_equal(
      &(lhs->color_channels), &(rhs->color_channels)))
  {
    return false;
  }
  // faces
  if (!iviz_msgs__msg__Triangle__Sequence__are_equal(
      &(lhs->faces), &(rhs->faces)))
  {
    return false;
  }
  // material_index
  if (lhs->material_index != rhs->material_index) {
    return false;
  }
  return true;
}

bool
iviz_msgs__msg__Mesh__copy(
  const iviz_msgs__msg__Mesh * input,
  iviz_msgs__msg__Mesh * output)
{
  if (!input || !output) {
    return false;
  }
  // name
  if (!rosidl_runtime_c__String__copy(
      &(input->name), &(output->name)))
  {
    return false;
  }
  // vertices
  if (!iviz_msgs__msg__Vector3f__Sequence__copy(
      &(input->vertices), &(output->vertices)))
  {
    return false;
  }
  // normals
  if (!iviz_msgs__msg__Vector3f__Sequence__copy(
      &(input->normals), &(output->normals)))
  {
    return false;
  }
  // tangents
  if (!iviz_msgs__msg__Vector3f__Sequence__copy(
      &(input->tangents), &(output->tangents)))
  {
    return false;
  }
  // bi_tangents
  if (!iviz_msgs__msg__Vector3f__Sequence__copy(
      &(input->bi_tangents), &(output->bi_tangents)))
  {
    return false;
  }
  // tex_coords
  if (!iviz_msgs__msg__TexCoords__Sequence__copy(
      &(input->tex_coords), &(output->tex_coords)))
  {
    return false;
  }
  // color_channels
  if (!iviz_msgs__msg__ColorChannel__Sequence__copy(
      &(input->color_channels), &(output->color_channels)))
  {
    return false;
  }
  // faces
  if (!iviz_msgs__msg__Triangle__Sequence__copy(
      &(input->faces), &(output->faces)))
  {
    return false;
  }
  // material_index
  output->material_index = input->material_index;
  return true;
}

iviz_msgs__msg__Mesh *
iviz_msgs__msg__Mesh__create()
{
  iviz_msgs__msg__Mesh * msg = (iviz_msgs__msg__Mesh *)malloc(sizeof(iviz_msgs__msg__Mesh));
  if (!msg) {
    return NULL;
  }
  memset(msg, 0, sizeof(iviz_msgs__msg__Mesh));
  bool success = iviz_msgs__msg__Mesh__init(msg);
  if (!success) {
    free(msg);
    return NULL;
  }
  return msg;
}

void
iviz_msgs__msg__Mesh__destroy(iviz_msgs__msg__Mesh * msg)
{
  if (msg) {
    iviz_msgs__msg__Mesh__fini(msg);
  }
  free(msg);
}


bool
iviz_msgs__msg__Mesh__Sequence__init(iviz_msgs__msg__Mesh__Sequence * array, size_t size)
{
  if (!array) {
    return false;
  }
  iviz_msgs__msg__Mesh * data = NULL;
  if (size) {
    data = (iviz_msgs__msg__Mesh *)calloc(size, sizeof(iviz_msgs__msg__Mesh));
    if (!data) {
      return false;
    }
    // initialize all array elements
    size_t i;
    for (i = 0; i < size; ++i) {
      bool success = iviz_msgs__msg__Mesh__init(&data[i]);
      if (!success) {
        break;
      }
    }
    if (i < size) {
      // if initialization failed finalize the already initialized array elements
      for (; i > 0; --i) {
        iviz_msgs__msg__Mesh__fini(&data[i - 1]);
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
iviz_msgs__msg__Mesh__Sequence__fini(iviz_msgs__msg__Mesh__Sequence * array)
{
  if (!array) {
    return;
  }
  if (array->data) {
    // ensure that data and capacity values are consistent
    assert(array->capacity > 0);
    // finalize all array elements
    for (size_t i = 0; i < array->capacity; ++i) {
      iviz_msgs__msg__Mesh__fini(&array->data[i]);
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

iviz_msgs__msg__Mesh__Sequence *
iviz_msgs__msg__Mesh__Sequence__create(size_t size)
{
  iviz_msgs__msg__Mesh__Sequence * array = (iviz_msgs__msg__Mesh__Sequence *)malloc(sizeof(iviz_msgs__msg__Mesh__Sequence));
  if (!array) {
    return NULL;
  }
  bool success = iviz_msgs__msg__Mesh__Sequence__init(array, size);
  if (!success) {
    free(array);
    return NULL;
  }
  return array;
}

void
iviz_msgs__msg__Mesh__Sequence__destroy(iviz_msgs__msg__Mesh__Sequence * array)
{
  if (array) {
    iviz_msgs__msg__Mesh__Sequence__fini(array);
  }
  free(array);
}

bool
iviz_msgs__msg__Mesh__Sequence__are_equal(const iviz_msgs__msg__Mesh__Sequence * lhs, const iviz_msgs__msg__Mesh__Sequence * rhs)
{
  if (!lhs || !rhs) {
    return false;
  }
  if (lhs->size != rhs->size) {
    return false;
  }
  for (size_t i = 0; i < lhs->size; ++i) {
    if (!iviz_msgs__msg__Mesh__are_equal(&(lhs->data[i]), &(rhs->data[i]))) {
      return false;
    }
  }
  return true;
}

bool
iviz_msgs__msg__Mesh__Sequence__copy(
  const iviz_msgs__msg__Mesh__Sequence * input,
  iviz_msgs__msg__Mesh__Sequence * output)
{
  if (!input || !output) {
    return false;
  }
  if (output->capacity < input->size) {
    const size_t allocation_size =
      input->size * sizeof(iviz_msgs__msg__Mesh);
    iviz_msgs__msg__Mesh * data =
      (iviz_msgs__msg__Mesh *)realloc(output->data, allocation_size);
    if (!data) {
      return false;
    }
    for (size_t i = output->capacity; i < input->size; ++i) {
      if (!iviz_msgs__msg__Mesh__init(&data[i])) {
        /* free currently allocated and return false */
        for (; i-- > output->capacity; ) {
          iviz_msgs__msg__Mesh__fini(&data[i]);
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
    if (!iviz_msgs__msg__Mesh__copy(
        &(input->data[i]), &(output->data[i])))
    {
      return false;
    }
  }
  return true;
}
