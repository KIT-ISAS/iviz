// generated from rosidl_generator_c/resource/idl__functions.h.em
// with input from iviz_msgs:msg/XRHandState.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__FUNCTIONS_H_
#define IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__FUNCTIONS_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stdlib.h>

#include "rosidl_runtime_c/visibility_control.h"
#include "iviz_msgs/msg/rosidl_generator_c__visibility_control.h"

#include "iviz_msgs/msg/detail/xr_hand_state__struct.h"

/// Initialize msg/XRHandState message.
/**
 * If the init function is called twice for the same message without
 * calling fini inbetween previously allocated memory will be leaked.
 * \param[in,out] msg The previously allocated message pointer.
 * Fields without a default value will not be initialized by this function.
 * You might want to call memset(msg, 0, sizeof(
 * iviz_msgs__msg__XRHandState
 * )) before or use
 * iviz_msgs__msg__XRHandState__create()
 * to allocate and initialize the message.
 * \return true if initialization was successful, otherwise false
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__XRHandState__init(iviz_msgs__msg__XRHandState * msg);

/// Finalize msg/XRHandState message.
/**
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__XRHandState__fini(iviz_msgs__msg__XRHandState * msg);

/// Create msg/XRHandState message.
/**
 * It allocates the memory for the message, sets the memory to zero, and
 * calls
 * iviz_msgs__msg__XRHandState__init().
 * \return The pointer to the initialized message if successful,
 * otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__msg__XRHandState *
iviz_msgs__msg__XRHandState__create();

/// Destroy msg/XRHandState message.
/**
 * It calls
 * iviz_msgs__msg__XRHandState__fini()
 * and frees the memory of the message.
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__XRHandState__destroy(iviz_msgs__msg__XRHandState * msg);

/// Check for msg/XRHandState message equality.
/**
 * \param[in] lhs The message on the left hand size of the equality operator.
 * \param[in] rhs The message on the right hand size of the equality operator.
 * \return true if messages are equal, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__XRHandState__are_equal(const iviz_msgs__msg__XRHandState * lhs, const iviz_msgs__msg__XRHandState * rhs);

/// Copy a msg/XRHandState message.
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
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__XRHandState__copy(
  const iviz_msgs__msg__XRHandState * input,
  iviz_msgs__msg__XRHandState * output);

/// Initialize array of msg/XRHandState messages.
/**
 * It allocates the memory for the number of elements and calls
 * iviz_msgs__msg__XRHandState__init()
 * for each element of the array.
 * \param[in,out] array The allocated array pointer.
 * \param[in] size The size / capacity of the array.
 * \return true if initialization was successful, otherwise false
 * If the array pointer is valid and the size is zero it is guaranteed
 # to return true.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__XRHandState__Sequence__init(iviz_msgs__msg__XRHandState__Sequence * array, size_t size);

/// Finalize array of msg/XRHandState messages.
/**
 * It calls
 * iviz_msgs__msg__XRHandState__fini()
 * for each element of the array and frees the memory for the number of
 * elements.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__XRHandState__Sequence__fini(iviz_msgs__msg__XRHandState__Sequence * array);

/// Create array of msg/XRHandState messages.
/**
 * It allocates the memory for the array and calls
 * iviz_msgs__msg__XRHandState__Sequence__init().
 * \param[in] size The size / capacity of the array.
 * \return The pointer to the initialized array if successful, otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__msg__XRHandState__Sequence *
iviz_msgs__msg__XRHandState__Sequence__create(size_t size);

/// Destroy array of msg/XRHandState messages.
/**
 * It calls
 * iviz_msgs__msg__XRHandState__Sequence__fini()
 * on the array,
 * and frees the memory of the array.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__XRHandState__Sequence__destroy(iviz_msgs__msg__XRHandState__Sequence * array);

/// Check for msg/XRHandState message array equality.
/**
 * \param[in] lhs The message array on the left hand size of the equality operator.
 * \param[in] rhs The message array on the right hand size of the equality operator.
 * \return true if message arrays are equal in size and content, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__XRHandState__Sequence__are_equal(const iviz_msgs__msg__XRHandState__Sequence * lhs, const iviz_msgs__msg__XRHandState__Sequence * rhs);

/// Copy an array of msg/XRHandState messages.
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
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__XRHandState__Sequence__copy(
  const iviz_msgs__msg__XRHandState__Sequence * input,
  iviz_msgs__msg__XRHandState__Sequence * output);

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__FUNCTIONS_H_
