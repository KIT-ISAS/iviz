// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Mesh.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MESH__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__MESH__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

// Include directives for member types
// Member 'name'
#include "rosidl_runtime_c/string.h"
// Member 'vertices'
// Member 'normals'
// Member 'tangents'
// Member 'bi_tangents'
#include "iviz_msgs/msg/detail/vector3f__struct.h"
// Member 'tex_coords'
#include "iviz_msgs/msg/detail/tex_coords__struct.h"
// Member 'color_channels'
#include "iviz_msgs/msg/detail/color_channel__struct.h"
// Member 'faces'
#include "iviz_msgs/msg/detail/triangle__struct.h"

// Struct defined in msg/Mesh in the package iviz_msgs.
typedef struct iviz_msgs__msg__Mesh
{
  rosidl_runtime_c__String name;
  iviz_msgs__msg__Vector3f__Sequence vertices;
  iviz_msgs__msg__Vector3f__Sequence normals;
  iviz_msgs__msg__Vector3f__Sequence tangents;
  iviz_msgs__msg__Vector3f__Sequence bi_tangents;
  iviz_msgs__msg__TexCoords__Sequence tex_coords;
  iviz_msgs__msg__ColorChannel__Sequence color_channels;
  iviz_msgs__msg__Triangle__Sequence faces;
  uint32_t material_index;
} iviz_msgs__msg__Mesh;

// Struct for a sequence of iviz_msgs__msg__Mesh.
typedef struct iviz_msgs__msg__Mesh__Sequence
{
  iviz_msgs__msg__Mesh * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Mesh__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__MESH__STRUCT_H_
