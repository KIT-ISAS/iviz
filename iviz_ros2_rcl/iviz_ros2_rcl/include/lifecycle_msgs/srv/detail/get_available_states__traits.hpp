// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from lifecycle_msgs:srv/GetAvailableStates.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__SRV__DETAIL__GET_AVAILABLE_STATES__TRAITS_HPP_
#define LIFECYCLE_MSGS__SRV__DETAIL__GET_AVAILABLE_STATES__TRAITS_HPP_

#include "lifecycle_msgs/srv/detail/get_available_states__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::GetAvailableStates_Request>()
{
  return "lifecycle_msgs::srv::GetAvailableStates_Request";
}

template<>
inline const char * name<lifecycle_msgs::srv::GetAvailableStates_Request>()
{
  return "lifecycle_msgs/srv/GetAvailableStates_Request";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::GetAvailableStates_Request>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<lifecycle_msgs::srv::GetAvailableStates_Request>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<lifecycle_msgs::srv::GetAvailableStates_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::GetAvailableStates_Response>()
{
  return "lifecycle_msgs::srv::GetAvailableStates_Response";
}

template<>
inline const char * name<lifecycle_msgs::srv::GetAvailableStates_Response>()
{
  return "lifecycle_msgs/srv/GetAvailableStates_Response";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::GetAvailableStates_Response>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<lifecycle_msgs::srv::GetAvailableStates_Response>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<lifecycle_msgs::srv::GetAvailableStates_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::GetAvailableStates>()
{
  return "lifecycle_msgs::srv::GetAvailableStates";
}

template<>
inline const char * name<lifecycle_msgs::srv::GetAvailableStates>()
{
  return "lifecycle_msgs/srv/GetAvailableStates";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::GetAvailableStates>
  : std::integral_constant<
    bool,
    has_fixed_size<lifecycle_msgs::srv::GetAvailableStates_Request>::value &&
    has_fixed_size<lifecycle_msgs::srv::GetAvailableStates_Response>::value
  >
{
};

template<>
struct has_bounded_size<lifecycle_msgs::srv::GetAvailableStates>
  : std::integral_constant<
    bool,
    has_bounded_size<lifecycle_msgs::srv::GetAvailableStates_Request>::value &&
    has_bounded_size<lifecycle_msgs::srv::GetAvailableStates_Response>::value
  >
{
};

template<>
struct is_service<lifecycle_msgs::srv::GetAvailableStates>
  : std::true_type
{
};

template<>
struct is_service_request<lifecycle_msgs::srv::GetAvailableStates_Request>
  : std::true_type
{
};

template<>
struct is_service_response<lifecycle_msgs::srv::GetAvailableStates_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // LIFECYCLE_MSGS__SRV__DETAIL__GET_AVAILABLE_STATES__TRAITS_HPP_
