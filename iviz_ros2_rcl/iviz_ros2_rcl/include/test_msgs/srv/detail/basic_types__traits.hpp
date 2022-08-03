// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:srv/BasicTypes.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__SRV__DETAIL__BASIC_TYPES__TRAITS_HPP_
#define TEST_MSGS__SRV__DETAIL__BASIC_TYPES__TRAITS_HPP_

#include "test_msgs/srv/detail/basic_types__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::BasicTypes_Request>()
{
  return "test_msgs::srv::BasicTypes_Request";
}

template<>
inline const char * name<test_msgs::srv::BasicTypes_Request>()
{
  return "test_msgs/srv/BasicTypes_Request";
}

template<>
struct has_fixed_size<test_msgs::srv::BasicTypes_Request>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<test_msgs::srv::BasicTypes_Request>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<test_msgs::srv::BasicTypes_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::BasicTypes_Response>()
{
  return "test_msgs::srv::BasicTypes_Response";
}

template<>
inline const char * name<test_msgs::srv::BasicTypes_Response>()
{
  return "test_msgs/srv/BasicTypes_Response";
}

template<>
struct has_fixed_size<test_msgs::srv::BasicTypes_Response>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<test_msgs::srv::BasicTypes_Response>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<test_msgs::srv::BasicTypes_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::BasicTypes>()
{
  return "test_msgs::srv::BasicTypes";
}

template<>
inline const char * name<test_msgs::srv::BasicTypes>()
{
  return "test_msgs/srv/BasicTypes";
}

template<>
struct has_fixed_size<test_msgs::srv::BasicTypes>
  : std::integral_constant<
    bool,
    has_fixed_size<test_msgs::srv::BasicTypes_Request>::value &&
    has_fixed_size<test_msgs::srv::BasicTypes_Response>::value
  >
{
};

template<>
struct has_bounded_size<test_msgs::srv::BasicTypes>
  : std::integral_constant<
    bool,
    has_bounded_size<test_msgs::srv::BasicTypes_Request>::value &&
    has_bounded_size<test_msgs::srv::BasicTypes_Response>::value
  >
{
};

template<>
struct is_service<test_msgs::srv::BasicTypes>
  : std::true_type
{
};

template<>
struct is_service_request<test_msgs::srv::BasicTypes_Request>
  : std::true_type
{
};

template<>
struct is_service_response<test_msgs::srv::BasicTypes_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__SRV__DETAIL__BASIC_TYPES__TRAITS_HPP_
