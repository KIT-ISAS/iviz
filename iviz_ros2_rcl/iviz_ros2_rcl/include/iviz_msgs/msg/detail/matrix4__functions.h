// generated from rosidl_generator_c/resource/idl__functions.h.em
// with input from iviz_msgs:msg/Matrix4.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MATRIX4__FUNCTIONS_H_
#define IVIZ_MSGS__MSG__DETAIL__MATRIX4__FUNCTIONS_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stdlib.h>

#include "rosidl_runtime_c/visibility_control.h"
#include "iviz_msgs/msg/rosidl_generator_c__visibility_control.h"

#include "iviz_msgs/msg/detail/matrix4__struct.h"

/// Initialize msg/Matrix4 message.
/**
 * If the init function is called twice for the same message without
 * calling fini inbetween previously allocated memory will be leaked.
 * \param[in,out] msg The previously allocated message pointer.
 * Fields without a default value will not be initialized by this function.
 * You might want to call memset(msg, 0, sizeof(
 * iviz_msgs__msg__Matrix4
 * )) before or use
 * iviz_msgs__msg__Matrix4__create()
 * to allocate and initialize the message.
 * \return true if initialization was successful, otherwise false
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__Matrix4__init(iviz_msgs__msg__Matrix4 * msg);

/// Finalize msg/Matrix4 message.
/**
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__Matrix4__fini(iviz_msgs__msg__Matrix4 * msg);

/// Create msg/Matrix4 message.
/**
 * It allocates the memory for the message, sets the memory to zero, and
 * calls
 * iviz_msgs__msg__Matrix4__init().
 * \return The pointer to the initialized message if successful,
 * otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__msg__Matrix4 *
iviz_msgs__msg__Matrix4__create();

/// Destroy msg/Matrix4 message.
/**
 * It calls
 * iviz_msgs__msg__Matrix4__fini()
 * and frees the memory of the message.
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__Matrix4__destroy(iviz_msgs__msg__Matrix4 * msg);

/// Check for msg/Matrix4 message equality.
/**
 * \param[in] lhs The message on the left hand size of the equality operator.
 * \param[in] rhs The message on the right hand size of the equality operator.
 * \return true if messages are equal, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__Matrix4__are_equal(const iviz_msgs__msg__Matrix4 * lhs, const iviz_msgs__msg__Matrix4 * rhs);

/// Copy a msg/Matrix4 message.
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
iviz_msgs__msg__Matrix4__copy(
  const iviz_msgs__msg__Matrix4 * input,
  iviz_msgs__msg__Matrix4 * output);

/// Initialize array of msg/Matrix4 messages.
/**
 * It allocates the memory for the number of elements and calls
 * iviz_msgs__msg__Matrix4__init()
 * for each element of the array.
 * \param[in,out] array The allocated array pointer.
 * \param[in] size The size / capacity of the array.
 * \return true if initialization was successful, otherwise false
 * If the array pointer is valid and the size is zero it is guaranteed
 # to return true.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__Matrix4__Sequence__init(iviz_msgs__msg__Matrix4__Sequence * array, size_t size);

/// Finalize array of msg/Matrix4 messages.
/**
 * It calls
 * iviz_msgs__msg__Matrix4__fini()
 * for each element of the array and frees the memory for the number of
 * elements.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__Matrix4__Sequence__fini(iviz_msgs__msg__Matrix4__Sequence * array);

/// Create array of msg/Matrix4 messages.
/**
 * It allocates the memory for the array and calls
 * iviz_msgs__msg__Matrix4__Sequence__init().
 * \param[in] size The size / capacity of the array.
 * \return The pointer to the initialized array if successful, otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__msg__Matrix4__Sequence *
iviz_msgs__msg__Matrix4__Sequence__create(size_t size);

/// Destroy array of msg/Matrix4 messages.
/**
 * It calls
 * iviz_msgs__msg__Matrix4__Sequence__fini()
 * on the array,
 * and frees the memory of the array.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__msg__Matrix4__Sequence__destroy(iviz_msgs__msg__Matrix4__Sequence * array);

/// Check for msg/Matrix4 message array equality.
/**
 * \param[in] lhs The message array on the left hand size of the equality operator.
 * \param[in] rhs The message array on the right hand size of the equality operator.
 * \return true if message arrays are equal in size and content, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__msg__Matrix4__Sequence__are_equal(const iviz_msgs__msg__Matrix4__Sequence * lhs, const iviz_msgs__msg__Matrix4__Sequence * rhs);

/// Copy an array of msg/Matrix4 messages.
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
iviz_msgs__msg__Matrix4__Sequence__copy(
  const iviz_msgs__msg__Matrix4__Sequence * input,
  iviz_msgs__msg__Matrix4__Sequence * output);

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__MSG__DETAIL__MATRIX4__FUNCTIONS_H_
