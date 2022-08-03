// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:msg/Nested.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__NESTED__TRAITS_HPP_
#define TEST_MSGS__MSG__DETAIL__NESTED__TRAITS_HPP_

#include "test_msgs/msg/detail/nested__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'basic_types_value'
#include "test_msgs/msg/detail/basic_types__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::msg::Nested>()
{
  return "test_msgs::msg::Nested";
}

template<>
inline const char * name<test_msgs::msg::Nested>()
{
  return "test_msgs/msg/Nested";
}

template<>
struct has_fixed_size<test_msgs::msg::Nested>
  : std::integral_constant<bool, has_fixed_size<test_msgs::msg::BasicTypes>::value> {};

template<>
struct has_bounded_size<test_msgs::msg::Nested>
  : std::integral_constant<bool, has_bounded_size<test_msgs::msg::BasicTypes>::value> {};

template<>
struct is_message<test_msgs::msg::Nested>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__MSG__DETAIL__NESTED__TRAITS_HPP_
