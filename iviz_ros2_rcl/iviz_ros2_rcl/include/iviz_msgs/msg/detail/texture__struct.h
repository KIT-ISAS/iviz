// generated from rosidl_generator_c/resource/idl__struct.h.em
// with input from iviz_msgs:msg/Texture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TEXTURE__STRUCT_H_
#define IVIZ_MSGS__MSG__DETAIL__TEXTURE__STRUCT_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>


// Constants defined in the message

/// Constant 'TYPE_NONE'.
enum
{
  iviz_msgs__msg__Texture__TYPE_NONE = 0
};

/// Constant 'TYPE_DIFFUSE'.
enum
{
  iviz_msgs__msg__Texture__TYPE_DIFFUSE = 1
};

/// Constant 'TYPE_SPECULAR'.
enum
{
  iviz_msgs__msg__Texture__TYPE_SPECULAR = 2
};

/// Constant 'TYPE_AMBIENT'.
enum
{
  iviz_msgs__msg__Texture__TYPE_AMBIENT = 3
};

/// Constant 'TYPE_EMISSIVE'.
enum
{
  iviz_msgs__msg__Texture__TYPE_EMISSIVE = 4
};

/// Constant 'TYPE_HEIGHT'.
enum
{
  iviz_msgs__msg__Texture__TYPE_HEIGHT = 5
};

/// Constant 'TYPE_NORMALS'.
enum
{
  iviz_msgs__msg__Texture__TYPE_NORMALS = 6
};

/// Constant 'TYPE_SHININESS'.
enum
{
  iviz_msgs__msg__Texture__TYPE_SHININESS = 7
};

/// Constant 'TYPE_OPACITY'.
enum
{
  iviz_msgs__msg__Texture__TYPE_OPACITY = 8
};

/// Constant 'TYPE_DISPLACEMENT'.
enum
{
  iviz_msgs__msg__Texture__TYPE_DISPLACEMENT = 9
};

/// Constant 'TYPE_LIGHTMAP'.
enum
{
  iviz_msgs__msg__Texture__TYPE_LIGHTMAP = 10
};

/// Constant 'TYPE_REFLECTION'.
enum
{
  iviz_msgs__msg__Texture__TYPE_REFLECTION = 11
};

/// Constant 'TYPE_UNKNOWN'.
enum
{
  iviz_msgs__msg__Texture__TYPE_UNKNOWN = 12
};

/// Constant 'MAPPING_FROM_UV'.
enum
{
  iviz_msgs__msg__Texture__MAPPING_FROM_UV = 0
};

/// Constant 'MAPPING_SPHERE'.
enum
{
  iviz_msgs__msg__Texture__MAPPING_SPHERE = 1
};

/// Constant 'MAPPING_CYLINDER'.
enum
{
  iviz_msgs__msg__Texture__MAPPING_CYLINDER = 2
};

/// Constant 'MAPPING_BOX'.
enum
{
  iviz_msgs__msg__Texture__MAPPING_BOX = 3
};

/// Constant 'MAPPING_PLANE'.
enum
{
  iviz_msgs__msg__Texture__MAPPING_PLANE = 4
};

/// Constant 'MAPPING_UNKNOWN'.
enum
{
  iviz_msgs__msg__Texture__MAPPING_UNKNOWN = 5
};

/// Constant 'OP_MULTIPLY'.
enum
{
  iviz_msgs__msg__Texture__OP_MULTIPLY = 0
};

/// Constant 'OP_ADD'.
enum
{
  iviz_msgs__msg__Texture__OP_ADD = 1
};

/// Constant 'OP_SUBTRACT'.
enum
{
  iviz_msgs__msg__Texture__OP_SUBTRACT = 2
};

/// Constant 'OP_DIVIDE'.
enum
{
  iviz_msgs__msg__Texture__OP_DIVIDE = 3
};

/// Constant 'OP_SMOOTH_ADD'.
enum
{
  iviz_msgs__msg__Texture__OP_SMOOTH_ADD = 4
};

/// Constant 'OP_SIGNED_ADD'.
enum
{
  iviz_msgs__msg__Texture__OP_SIGNED_ADD = 5
};

/// Constant 'WRAP_WRAP'.
enum
{
  iviz_msgs__msg__Texture__WRAP_WRAP = 0
};

/// Constant 'WRAP_CLAMP'.
enum
{
  iviz_msgs__msg__Texture__WRAP_CLAMP = 1
};

/// Constant 'WRAP_MIRROR'.
enum
{
  iviz_msgs__msg__Texture__WRAP_MIRROR = 2
};

/// Constant 'WRAP_DECAL'.
enum
{
  iviz_msgs__msg__Texture__WRAP_DECAL = 3
};

// Include directives for member types
// Member 'path'
#include "rosidl_runtime_c/string.h"

// Struct defined in msg/Texture in the package iviz_msgs.
typedef struct iviz_msgs__msg__Texture
{
  rosidl_runtime_c__String path;
  int32_t index;
  uint8_t type;
  uint8_t mapping;
  int32_t uv_index;
  float blend_factor;
  uint8_t operation;
  uint8_t wrap_mode_u;
  uint8_t wrap_mode_v;
} iviz_msgs__msg__Texture;

// Struct for a sequence of iviz_msgs__msg__Texture.
typedef struct iviz_msgs__msg__Texture__Sequence
{
  iviz_msgs__msg__Texture * data;
  /// The number of valid items in data
  size_t size;
  /// The number of allocated items in data
  size_t capacity;
} iviz_msgs__msg__Texture__Sequence;

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__TEXTURE__STRUCT_H_
