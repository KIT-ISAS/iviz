// generated from rosidl_generator_c/resource/idl__functions.h.em
// with input from grid_map_msgs:msg/GridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__FUNCTIONS_H_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__FUNCTIONS_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stdlib.h>

#include "rosidl_runtime_c/visibility_control.h"
#include "grid_map_msgs/msg/rosidl_generator_c__visibility_control.h"

#include "grid_map_msgs/msg/detail/grid_map__struct.h"

/// Initialize msg/GridMap message.
/**
 * If the init function is called twice for the same message without
 * calling fini inbetween previously allocated memory will be leaked.
 * \param[in,out] msg The previously allocated message pointer.
 * Fields without a default value will not be initialized by this function.
 * You might want to call memset(msg, 0, sizeof(
 * grid_map_msgs__msg__GridMap
 * )) before or use
 * grid_map_msgs__msg__GridMap__create()
 * to allocate and initialize the message.
 * \return true if initialization was successful, otherwise false
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__msg__GridMap__init(grid_map_msgs__msg__GridMap * msg);

/// Finalize msg/GridMap message.
/**
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__msg__GridMap__fini(grid_map_msgs__msg__GridMap * msg);

/// Create msg/GridMap message.
/**
 * It allocates the memory for the message, sets the memory to zero, and
 * calls
 * grid_map_msgs__msg__GridMap__init().
 * \return The pointer to the initialized message if successful,
 * otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
grid_map_msgs__msg__GridMap *
grid_map_msgs__msg__GridMap__create();

/// Destroy msg/GridMap message.
/**
 * It calls
 * grid_map_msgs__msg__GridMap__fini()
 * and frees the memory of the message.
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__msg__GridMap__destroy(grid_map_msgs__msg__GridMap * msg);

/// Check for msg/GridMap message equality.
/**
 * \param[in] lhs The message on the left hand size of the equality operator.
 * \param[in] rhs The message on the right hand size of the equality operator.
 * \return true if messages are equal, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__msg__GridMap__are_equal(const grid_map_msgs__msg__GridMap * lhs, const grid_map_msgs__msg__GridMap * rhs);

/// Copy a msg/GridMap message.
/**
 * This functions performs a deep copy, as opposed to the shallow copy that
 * plain assignment yields.
 *
 * \param[in] input The source message pointer.
 * \param[out] output The target message pointer, which must
 *   have been initialized before calling this function.
 * \return true if successful, or false if either pointer is null
 *   or memory allocation fails.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__msg__GridMap__copy(
  const grid_map_msgs__msg__GridMap * input,
  grid_map_msgs__msg__GridMap * output);

/// Initialize array of msg/GridMap messages.
/**
 * It allocates the memory for the number of elements and calls
 * grid_map_msgs__msg__GridMap__init()
 * for each element of the array.
 * \param[in,out] array The allocated array pointer.
 * \param[in] size The size / capacity of the array.
 * \return true if initialization was successful, otherwise false
 * If the array pointer is valid and the size is zero it is guaranteed
 # to return true.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__msg__GridMap__Sequence__init(grid_map_msgs__msg__GridMap__Sequence * array, size_t size);

/// Finalize array of msg/GridMap messages.
/**
 * It calls
 * grid_map_msgs__msg__GridMap__fini()
 * for each element of the array and frees the memory for the number of
 * elements.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__msg__GridMap__Sequence__fini(grid_map_msgs__msg__GridMap__Sequence * array);

/// Create array of msg/GridMap messages.
/**
 * It allocates the memory for the array and calls
 * grid_map_msgs__msg__GridMap__Sequence__init().
 * \param[in] size The size / capacity of the array.
 * \return The pointer to the initialized array if successful, otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
grid_map_msgs__msg__GridMap__Sequence *
grid_map_msgs__msg__GridMap__Sequence__create(size_t size);

/// Destroy array of msg/GridMap messages.
/**
 * It calls
 * grid_map_msgs__msg__GridMap__Sequence__fini()
 * on the array,
 * and frees the memory of the array.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__msg__GridMap__Sequence__destroy(grid_map_msgs__msg__GridMap__Sequence * array);

/// Check for msg/GridMap message array equality.
/**
 * \param[in] lhs The message array on the left hand size of the equality operator.
 * \param[in] rhs The message array on the right hand size of the equality operator.
 * \return true if message arrays are equal in size and content, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__msg__GridMap__Sequence__are_equal(const grid_map_msgs__msg__GridMap__Sequence * lhs, const grid_map_msgs__msg__GridMap__Sequence * rhs);

/// Copy an array of msg/GridMap messages.
/**
 * This functions performs a deep copy, as opposed to the shallow copy that
 * plain assignment yields.
 *
 * \param[in] input The source array pointer.
 * \param[out] output The target array pointer, which must
 *   have been initialized before calling this function.
 * \return true if successful, or false if either pointer
 *   is null or memory allocation fails.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__msg__GridMap__Sequence__copy(
  const grid_map_msgs__msg__GridMap__Sequence * input,
  grid_map_msgs__msg__GridMap__Sequence * output);

#ifdef __cplusplus
}
#endif

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__FUNCTIONS_H_
