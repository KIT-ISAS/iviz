// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:msg/Strings.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__STRINGS__TRAITS_HPP_
#define TEST_MSGS__MSG__DETAIL__STRINGS__TRAITS_HPP_

#include "test_msgs/msg/detail/strings__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::msg::Strings>()
{
  return "test_msgs::msg::Strings";
}

template<>
inline const char * name<test_msgs::msg::Strings>()
{
  return "test_msgs/msg/Strings";
}

template<>
struct has_fixed_size<test_msgs::msg::Strings>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<test_msgs::msg::Strings>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<test_msgs::msg::Strings>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__MSG__DETAIL__STRINGS__TRAITS_HPP_
