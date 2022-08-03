// generated from rosidl_generator_c/resource/idl__functions.h.em
// with input from grid_map_msgs:srv/GetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__FUNCTIONS_H_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__FUNCTIONS_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stdlib.h>

#include "rosidl_runtime_c/visibility_control.h"
#include "grid_map_msgs/msg/rosidl_generator_c__visibility_control.h"

#include "grid_map_msgs/srv/detail/get_grid_map__struct.h"

/// Initialize srv/GetGridMap message.
/**
 * If the init function is called twice for the same message without
 * calling fini inbetween previously allocated memory will be leaked.
 * \param[in,out] msg The previously allocated message pointer.
 * Fields without a default value will not be initialized by this function.
 * You might want to call memset(msg, 0, sizeof(
 * grid_map_msgs__srv__GetGridMap_Request
 * )) before or use
 * grid_map_msgs__srv__GetGridMap_Request__create()
 * to allocate and initialize the message.
 * \return true if initialization was successful, otherwise false
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Request__init(grid_map_msgs__srv__GetGridMap_Request * msg);

/// Finalize srv/GetGridMap message.
/**
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Request__fini(grid_map_msgs__srv__GetGridMap_Request * msg);

/// Create srv/GetGridMap message.
/**
 * It allocates the memory for the message, sets the memory to zero, and
 * calls
 * grid_map_msgs__srv__GetGridMap_Request__init().
 * \return The pointer to the initialized message if successful,
 * otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
grid_map_msgs__srv__GetGridMap_Request *
grid_map_msgs__srv__GetGridMap_Request__create();

/// Destroy srv/GetGridMap message.
/**
 * It calls
 * grid_map_msgs__srv__GetGridMap_Request__fini()
 * and frees the memory of the message.
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Request__destroy(grid_map_msgs__srv__GetGridMap_Request * msg);

/// Check for srv/GetGridMap message equality.
/**
 * \param[in] lhs The message on the left hand size of the equality operator.
 * \param[in] rhs The message on the right hand size of the equality operator.
 * \return true if messages are equal, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Request__are_equal(const grid_map_msgs__srv__GetGridMap_Request * lhs, const grid_map_msgs__srv__GetGridMap_Request * rhs);

/// Copy a srv/GetGridMap message.
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
grid_map_msgs__srv__GetGridMap_Request__copy(
  const grid_map_msgs__srv__GetGridMap_Request * input,
  grid_map_msgs__srv__GetGridMap_Request * output);

/// Initialize array of srv/GetGridMap messages.
/**
 * It allocates the memory for the number of elements and calls
 * grid_map_msgs__srv__GetGridMap_Request__init()
 * for each element of the array.
 * \param[in,out] array The allocated array pointer.
 * \param[in] size The size / capacity of the array.
 * \return true if initialization was successful, otherwise false
 * If the array pointer is valid and the size is zero it is guaranteed
 # to return true.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Request__Sequence__init(grid_map_msgs__srv__GetGridMap_Request__Sequence * array, size_t size);

/// Finalize array of srv/GetGridMap messages.
/**
 * It calls
 * grid_map_msgs__srv__GetGridMap_Request__fini()
 * for each element of the array and frees the memory for the number of
 * elements.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Request__Sequence__fini(grid_map_msgs__srv__GetGridMap_Request__Sequence * array);

/// Create array of srv/GetGridMap messages.
/**
 * It allocates the memory for the array and calls
 * grid_map_msgs__srv__GetGridMap_Request__Sequence__init().
 * \param[in] size The size / capacity of the array.
 * \return The pointer to the initialized array if successful, otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
grid_map_msgs__srv__GetGridMap_Request__Sequence *
grid_map_msgs__srv__GetGridMap_Request__Sequence__create(size_t size);

/// Destroy array of srv/GetGridMap messages.
/**
 * It calls
 * grid_map_msgs__srv__GetGridMap_Request__Sequence__fini()
 * on the array,
 * and frees the memory of the array.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Request__Sequence__destroy(grid_map_msgs__srv__GetGridMap_Request__Sequence * array);

/// Check for srv/GetGridMap message array equality.
/**
 * \param[in] lhs The message array on the left hand size of the equality operator.
 * \param[in] rhs The message array on the right hand size of the equality operator.
 * \return true if message arrays are equal in size and content, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Request__Sequence__are_equal(const grid_map_msgs__srv__GetGridMap_Request__Sequence * lhs, const grid_map_msgs__srv__GetGridMap_Request__Sequence * rhs);

/// Copy an array of srv/GetGridMap messages.
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
grid_map_msgs__srv__GetGridMap_Request__Sequence__copy(
  const grid_map_msgs__srv__GetGridMap_Request__Sequence * input,
  grid_map_msgs__srv__GetGridMap_Request__Sequence * output);

/// Initialize srv/GetGridMap message.
/**
 * If the init function is called twice for the same message without
 * calling fini inbetween previously allocated memory will be leaked.
 * \param[in,out] msg The previously allocated message pointer.
 * Fields without a default value will not be initialized by this function.
 * You might want to call memset(msg, 0, sizeof(
 * grid_map_msgs__srv__GetGridMap_Response
 * )) before or use
 * grid_map_msgs__srv__GetGridMap_Response__create()
 * to allocate and initialize the message.
 * \return true if initialization was successful, otherwise false
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Response__init(grid_map_msgs__srv__GetGridMap_Response * msg);

/// Finalize srv/GetGridMap message.
/**
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Response__fini(grid_map_msgs__srv__GetGridMap_Response * msg);

/// Create srv/GetGridMap message.
/**
 * It allocates the memory for the message, sets the memory to zero, and
 * calls
 * grid_map_msgs__srv__GetGridMap_Response__init().
 * \return The pointer to the initialized message if successful,
 * otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
grid_map_msgs__srv__GetGridMap_Response *
grid_map_msgs__srv__GetGridMap_Response__create();

/// Destroy srv/GetGridMap message.
/**
 * It calls
 * grid_map_msgs__srv__GetGridMap_Response__fini()
 * and frees the memory of the message.
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Response__destroy(grid_map_msgs__srv__GetGridMap_Response * msg);

/// Check for srv/GetGridMap message equality.
/**
 * \param[in] lhs The message on the left hand size of the equality operator.
 * \param[in] rhs The message on the right hand size of the equality operator.
 * \return true if messages are equal, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Response__are_equal(const grid_map_msgs__srv__GetGridMap_Response * lhs, const grid_map_msgs__srv__GetGridMap_Response * rhs);

/// Copy a srv/GetGridMap message.
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
grid_map_msgs__srv__GetGridMap_Response__copy(
  const grid_map_msgs__srv__GetGridMap_Response * input,
  grid_map_msgs__srv__GetGridMap_Response * output);

/// Initialize array of srv/GetGridMap messages.
/**
 * It allocates the memory for the number of elements and calls
 * grid_map_msgs__srv__GetGridMap_Response__init()
 * for each element of the array.
 * \param[in,out] array The allocated array pointer.
 * \param[in] size The size / capacity of the array.
 * \return true if initialization was successful, otherwise false
 * If the array pointer is valid and the size is zero it is guaranteed
 # to return true.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Response__Sequence__init(grid_map_msgs__srv__GetGridMap_Response__Sequence * array, size_t size);

/// Finalize array of srv/GetGridMap messages.
/**
 * It calls
 * grid_map_msgs__srv__GetGridMap_Response__fini()
 * for each element of the array and frees the memory for the number of
 * elements.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Response__Sequence__fini(grid_map_msgs__srv__GetGridMap_Response__Sequence * array);

/// Create array of srv/GetGridMap messages.
/**
 * It allocates the memory for the array and calls
 * grid_map_msgs__srv__GetGridMap_Response__Sequence__init().
 * \param[in] size The size / capacity of the array.
 * \return The pointer to the initialized array if successful, otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
grid_map_msgs__srv__GetGridMap_Response__Sequence *
grid_map_msgs__srv__GetGridMap_Response__Sequence__create(size_t size);

/// Destroy array of srv/GetGridMap messages.
/**
 * It calls
 * grid_map_msgs__srv__GetGridMap_Response__Sequence__fini()
 * on the array,
 * and frees the memory of the array.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
void
grid_map_msgs__srv__GetGridMap_Response__Sequence__destroy(grid_map_msgs__srv__GetGridMap_Response__Sequence * array);

/// Check for srv/GetGridMap message array equality.
/**
 * \param[in] lhs The message array on the left hand size of the equality operator.
 * \param[in] rhs The message array on the right hand size of the equality operator.
 * \return true if message arrays are equal in size and content, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_grid_map_msgs
bool
grid_map_msgs__srv__GetGridMap_Response__Sequence__are_equal(const grid_map_msgs__srv__GetGridMap_Response__Sequence * lhs, const grid_map_msgs__srv__GetGridMap_Response__Sequence * rhs);

/// Copy an array of srv/GetGridMap messages.
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
grid_map_msgs__srv__GetGridMap_Response__Sequence__copy(
  const grid_map_msgs__srv__GetGridMap_Response__Sequence * input,
  grid_map_msgs__srv__GetGridMap_Response__Sequence * output);

#ifdef __cplusplus
}
#endif

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__FUNCTIONS_H_
