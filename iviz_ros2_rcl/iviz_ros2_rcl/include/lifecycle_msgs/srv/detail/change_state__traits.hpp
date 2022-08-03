// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from lifecycle_msgs:srv/ChangeState.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__TRAITS_HPP_
#define LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__TRAITS_HPP_

#include "lifecycle_msgs/srv/detail/change_state__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'transition'
#include "lifecycle_msgs/msg/detail/transition__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::ChangeState_Request>()
{
  return "lifecycle_msgs::srv::ChangeState_Request";
}

template<>
inline const char * name<lifecycle_msgs::srv::ChangeState_Request>()
{
  return "lifecycle_msgs/srv/ChangeState_Request";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::ChangeState_Request>
  : std::integral_constant<bool, has_fixed_size<lifecycle_msgs::msg::Transition>::value> {};

template<>
struct has_bounded_size<lifecycle_msgs::srv::ChangeState_Request>
  : std::integral_constant<bool, has_bounded_size<lifecycle_msgs::msg::Transition>::value> {};

template<>
struct is_message<lifecycle_msgs::srv::ChangeState_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::ChangeState_Response>()
{
  return "lifecycle_msgs::srv::ChangeState_Response";
}

template<>
inline const char * name<lifecycle_msgs::srv::ChangeState_Response>()
{
  return "lifecycle_msgs/srv/ChangeState_Response";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::ChangeState_Response>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<lifecycle_msgs::srv::ChangeState_Response>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<lifecycle_msgs::srv::ChangeState_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::srv::ChangeState>()
{
  return "lifecycle_msgs::srv::ChangeState";
}

template<>
inline const char * name<lifecycle_msgs::srv::ChangeState>()
{
  return "lifecycle_msgs/srv/ChangeState";
}

template<>
struct has_fixed_size<lifecycle_msgs::srv::ChangeState>
  : std::integral_constant<
    bool,
    has_fixed_size<lifecycle_msgs::srv::ChangeState_Request>::value &&
    has_fixed_size<lifecycle_msgs::srv::ChangeState_Response>::value
  >
{
};

template<>
struct has_bounded_size<lifecycle_msgs::srv::ChangeState>
  : std::integral_constant<
    bool,
    has_bounded_size<lifecycle_msgs::srv::ChangeState_Request>::value &&
    has_bounded_size<lifecycle_msgs::srv::ChangeState_Response>::value
  >
{
};

template<>
struct is_service<lifecycle_msgs::srv::ChangeState>
  : std::true_type
{
};

template<>
struct is_service_request<lifecycle_msgs::srv::ChangeState_Request>
  : std::true_type
{
};

template<>
struct is_service_response<lifecycle_msgs::srv::ChangeState_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__TRAITS_HPP_
