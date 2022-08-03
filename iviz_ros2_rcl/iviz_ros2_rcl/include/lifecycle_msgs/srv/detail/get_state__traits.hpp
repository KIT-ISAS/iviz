// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from lifecycle_msgs:srv/GetState.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__SRV__DETAIL__GET_STATE__TRAITS_HPP_
#define LIFECYCLE_MSGS__SRV__DETAIL__GET_STATE__TRAITS_HPP_

#include "lifecycle_msgs/srv/detail/get_state__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::GetState_Request>()
{
  return "lifecycle_msgs::srv::GetState_Request";
}

template<>
inline const char * name<lifecycle_msgs::srv::GetState_Request>()
{
  return "lifecycle_msgs/srv/GetState_Request";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::GetState_Request>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<lifecycle_msgs::srv::GetState_Request>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<lifecycle_msgs::srv::GetState_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'current_state'
#include "lifecycle_msgs/msg/detail/state__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::GetState_Response>()
{
  return "lifecycle_msgs::srv::GetState_Response";
}

template<>
inline const char * name<lifecycle_msgs::srv::GetState_Response>()
{
  return "lifecycle_msgs/srv/GetState_Response";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::GetState_Response>
  : std::integral_constant<bool, has_fixed_size<lifecycle_msgs::msg::State>::value> {};

template<>
struct has_bounded_size<lifecycle_msgs::srv::GetState_Response>
  : std::integral_constant<bool, has_bounded_size<lifecycle_msgs::msg::State>::value> {};

template<>
struct is_message<lifecycle_msgs::srv::GetState_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::GetState>()
{
  return "lifecycle_msgs::srv::GetState";
}

template<>
inline const char * name<lifecycle_msgs::srv::GetState>()
{
  return "lifecycle_msgs/srv/GetState";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::GetState>
  : std::integral_constant<
    bool,
    has_fixed_size<lifecycle_msgs::srv::GetState_Request>::value &&
    has_fixed_size<lifecycle_msgs::srv::GetState_Response>::value
  >
{
};

template<>
struct has_bounded_size<lifecycle_msgs::srv::GetState>
  : std::integral_constant<
    bool,
    has_bounded_size<lifecycle_msgs::srv::GetState_Request>::value &&
    has_bounded_size<lifecycle_msgs::srv::GetState_Response>::value
  >
{
};

template<>
struct is_service<lifecycle_msgs::srv::GetState>
  : std::true_type
{
};

template<>
struct is_service_request<lifecycle_msgs::srv::GetState_Request>
  : std::true_type
{
};

template<>
struct is_service_response<lifecycle_msgs::srv::GetState_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // LIFECYCLE_MSGS__SRV__DETAIL__GET_STATE__TRAITS_HPP_
