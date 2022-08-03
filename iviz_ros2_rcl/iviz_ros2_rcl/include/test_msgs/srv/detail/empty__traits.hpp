// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:srv/Empty.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__SRV__DETAIL__EMPTY__TRAITS_HPP_
#define TEST_MSGS__SRV__DETAIL__EMPTY__TRAITS_HPP_

#include "test_msgs/srv/detail/empty__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::Empty_Request>()
{
  return "test_msgs::srv::Empty_Request";
}

template<>
inline const char * name<test_msgs::srv::Empty_Request>()
{
  return "test_msgs/srv/Empty_Request";
}

template<>
struct has_fixed_size<test_msgs::srv::Empty_Request>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<test_msgs::srv::Empty_Request>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<test_msgs::srv::Empty_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::Empty_Response>()
{
  return "test_msgs::srv::Empty_Response";
}

template<>
inline const char * name<test_msgs::srv::Empty_Response>()
{
  return "test_msgs/srv/Empty_Response";
}

template<>
struct has_fixed_size<test_msgs::srv::Empty_Response>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<test_msgs::srv::Empty_Response>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<test_msgs::srv::Empty_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::Empty>()
{
  return "test_msgs::srv::Empty";
}

template<>
inline const char * name<test_msgs::srv::Empty>()
{
  return "test_msgs/srv/Empty";
}

template<>
struct has_fixed_size<test_msgs::srv::Empty>
  : std::integral_constant<
    bool,
    has_fixed_size<test_msgs::srv::Empty_Request>::value &&
    has_fixed_size<test_msgs::srv::Empty_Response>::value
  >
{
};

template<>
struct has_bounded_size<test_msgs::srv::Empty>
  : std::integral_constant<
    bool,
    has_bounded_size<test_msgs::srv::Empty_Request>::value &&
    has_bounded_size<test_msgs::srv::Empty_Response>::value
  >
{
};

template<>
struct is_service<test_msgs::srv::Empty>
  : std::true_type
{
};

template<>
struct is_service_request<test_msgs::srv::Empty_Request>
  : std::true_type
{
};

template<>
struct is_service_response<test_msgs::srv::Empty_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__SRV__DETAIL__EMPTY__TRAITS_HPP_
