// generated from rosidl_generator_c/resource/idl__functions.h.em
// with input from iviz_msgs:srv/UpdateModule.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__FUNCTIONS_H_
#define IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__FUNCTIONS_H_

#ifdef __cplusplus
extern "C"
{
#endif

#include <stdbool.h>
#include <stdlib.h>

#include "rosidl_runtime_c/visibility_control.h"
#include "iviz_msgs/msg/rosidl_generator_c__visibility_control.h"

#include "iviz_msgs/srv/detail/update_module__struct.h"

/// Initialize srv/UpdateModule message.
/**
 * If the init function is called twice for the same message without
 * calling fini inbetween previously allocated memory will be leaked.
 * \param[in,out] msg The previously allocated message pointer.
 * Fields without a default value will not be initialized by this function.
 * You might want to call memset(msg, 0, sizeof(
 * iviz_msgs__srv__UpdateModule_Request
 * )) before or use
 * iviz_msgs__srv__UpdateModule_Request__create()
 * to allocate and initialize the message.
 * \return true if initialization was successful, otherwise false
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Request__init(iviz_msgs__srv__UpdateModule_Request * msg);

/// Finalize srv/UpdateModule message.
/**
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Request__fini(iviz_msgs__srv__UpdateModule_Request * msg);

/// Create srv/UpdateModule message.
/**
 * It allocates the memory for the message, sets the memory to zero, and
 * calls
 * iviz_msgs__srv__UpdateModule_Request__init().
 * \return The pointer to the initialized message if successful,
 * otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__srv__UpdateModule_Request *
iviz_msgs__srv__UpdateModule_Request__create();

/// Destroy srv/UpdateModule message.
/**
 * It calls
 * iviz_msgs__srv__UpdateModule_Request__fini()
 * and frees the memory of the message.
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Request__destroy(iviz_msgs__srv__UpdateModule_Request * msg);

/// Check for srv/UpdateModule message equality.
/**
 * \param[in] lhs The message on the left hand size of the equality operator.
 * \param[in] rhs The message on the right hand size of the equality operator.
 * \return true if messages are equal, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Request__are_equal(const iviz_msgs__srv__UpdateModule_Request * lhs, const iviz_msgs__srv__UpdateModule_Request * rhs);

/// Copy a srv/UpdateModule message.
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
iviz_msgs__srv__UpdateModule_Request__copy(
  const iviz_msgs__srv__UpdateModule_Request * input,
  iviz_msgs__srv__UpdateModule_Request * output);

/// Initialize array of srv/UpdateModule messages.
/**
 * It allocates the memory for the number of elements and calls
 * iviz_msgs__srv__UpdateModule_Request__init()
 * for each element of the array.
 * \param[in,out] array The allocated array pointer.
 * \param[in] size The size / capacity of the array.
 * \return true if initialization was successful, otherwise false
 * If the array pointer is valid and the size is zero it is guaranteed
 # to return true.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Request__Sequence__init(iviz_msgs__srv__UpdateModule_Request__Sequence * array, size_t size);

/// Finalize array of srv/UpdateModule messages.
/**
 * It calls
 * iviz_msgs__srv__UpdateModule_Request__fini()
 * for each element of the array and frees the memory for the number of
 * elements.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Request__Sequence__fini(iviz_msgs__srv__UpdateModule_Request__Sequence * array);

/// Create array of srv/UpdateModule messages.
/**
 * It allocates the memory for the array and calls
 * iviz_msgs__srv__UpdateModule_Request__Sequence__init().
 * \param[in] size The size / capacity of the array.
 * \return The pointer to the initialized array if successful, otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__srv__UpdateModule_Request__Sequence *
iviz_msgs__srv__UpdateModule_Request__Sequence__create(size_t size);

/// Destroy array of srv/UpdateModule messages.
/**
 * It calls
 * iviz_msgs__srv__UpdateModule_Request__Sequence__fini()
 * on the array,
 * and frees the memory of the array.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Request__Sequence__destroy(iviz_msgs__srv__UpdateModule_Request__Sequence * array);

/// Check for srv/UpdateModule message array equality.
/**
 * \param[in] lhs The message array on the left hand size of the equality operator.
 * \param[in] rhs The message array on the right hand size of the equality operator.
 * \return true if message arrays are equal in size and content, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Request__Sequence__are_equal(const iviz_msgs__srv__UpdateModule_Request__Sequence * lhs, const iviz_msgs__srv__UpdateModule_Request__Sequence * rhs);

/// Copy an array of srv/UpdateModule messages.
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
iviz_msgs__srv__UpdateModule_Request__Sequence__copy(
  const iviz_msgs__srv__UpdateModule_Request__Sequence * input,
  iviz_msgs__srv__UpdateModule_Request__Sequence * output);

/// Initialize srv/UpdateModule message.
/**
 * If the init function is called twice for the same message without
 * calling fini inbetween previously allocated memory will be leaked.
 * \param[in,out] msg The previously allocated message pointer.
 * Fields without a default value will not be initialized by this function.
 * You might want to call memset(msg, 0, sizeof(
 * iviz_msgs__srv__UpdateModule_Response
 * )) before or use
 * iviz_msgs__srv__UpdateModule_Response__create()
 * to allocate and initialize the message.
 * \return true if initialization was successful, otherwise false
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Response__init(iviz_msgs__srv__UpdateModule_Response * msg);

/// Finalize srv/UpdateModule message.
/**
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Response__fini(iviz_msgs__srv__UpdateModule_Response * msg);

/// Create srv/UpdateModule message.
/**
 * It allocates the memory for the message, sets the memory to zero, and
 * calls
 * iviz_msgs__srv__UpdateModule_Response__init().
 * \return The pointer to the initialized message if successful,
 * otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__srv__UpdateModule_Response *
iviz_msgs__srv__UpdateModule_Response__create();

/// Destroy srv/UpdateModule message.
/**
 * It calls
 * iviz_msgs__srv__UpdateModule_Response__fini()
 * and frees the memory of the message.
 * \param[in,out] msg The allocated message pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Response__destroy(iviz_msgs__srv__UpdateModule_Response * msg);

/// Check for srv/UpdateModule message equality.
/**
 * \param[in] lhs The message on the left hand size of the equality operator.
 * \param[in] rhs The message on the right hand size of the equality operator.
 * \return true if messages are equal, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Response__are_equal(const iviz_msgs__srv__UpdateModule_Response * lhs, const iviz_msgs__srv__UpdateModule_Response * rhs);

/// Copy a srv/UpdateModule message.
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
iviz_msgs__srv__UpdateModule_Response__copy(
  const iviz_msgs__srv__UpdateModule_Response * input,
  iviz_msgs__srv__UpdateModule_Response * output);

/// Initialize array of srv/UpdateModule messages.
/**
 * It allocates the memory for the number of elements and calls
 * iviz_msgs__srv__UpdateModule_Response__init()
 * for each element of the array.
 * \param[in,out] array The allocated array pointer.
 * \param[in] size The size / capacity of the array.
 * \return true if initialization was successful, otherwise false
 * If the array pointer is valid and the size is zero it is guaranteed
 # to return true.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Response__Sequence__init(iviz_msgs__srv__UpdateModule_Response__Sequence * array, size_t size);

/// Finalize array of srv/UpdateModule messages.
/**
 * It calls
 * iviz_msgs__srv__UpdateModule_Response__fini()
 * for each element of the array and frees the memory for the number of
 * elements.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Response__Sequence__fini(iviz_msgs__srv__UpdateModule_Response__Sequence * array);

/// Create array of srv/UpdateModule messages.
/**
 * It allocates the memory for the array and calls
 * iviz_msgs__srv__UpdateModule_Response__Sequence__init().
 * \param[in] size The size / capacity of the array.
 * \return The pointer to the initialized array if successful, otherwise NULL
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
iviz_msgs__srv__UpdateModule_Response__Sequence *
iviz_msgs__srv__UpdateModule_Response__Sequence__create(size_t size);

/// Destroy array of srv/UpdateModule messages.
/**
 * It calls
 * iviz_msgs__srv__UpdateModule_Response__Sequence__fini()
 * on the array,
 * and frees the memory of the array.
 * \param[in,out] array The initialized array pointer.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
void
iviz_msgs__srv__UpdateModule_Response__Sequence__destroy(iviz_msgs__srv__UpdateModule_Response__Sequence * array);

/// Check for srv/UpdateModule message array equality.
/**
 * \param[in] lhs The message array on the left hand size of the equality operator.
 * \param[in] rhs The message array on the right hand size of the equality operator.
 * \return true if message arrays are equal in size and content, otherwise false.
 */
ROSIDL_GENERATOR_C_PUBLIC_iviz_msgs
bool
iviz_msgs__srv__UpdateModule_Response__Sequence__are_equal(const iviz_msgs__srv__UpdateModule_Response__Sequence * lhs, const iviz_msgs__srv__UpdateModule_Response__Sequence * rhs);

/// Copy an array of srv/UpdateModule messages.
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
iviz_msgs__srv__UpdateModule_Response__Sequence__copy(
  const iviz_msgs__srv__UpdateModule_Response__Sequence * input,
  iviz_msgs__srv__UpdateModule_Response__Sequence * output);

#ifdef __cplusplus
}
#endif

#endif  // IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__FUNCTIONS_H_
