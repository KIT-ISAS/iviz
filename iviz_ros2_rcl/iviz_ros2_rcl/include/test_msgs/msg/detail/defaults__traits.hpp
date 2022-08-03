// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:msg/Defaults.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__DEFAULTS__TRAITS_HPP_
#define TEST_MSGS__MSG__DETAIL__DEFAULTS__TRAITS_HPP_

#include "test_msgs/msg/detail/defaults__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::msg::Defaults>()
{
  return "test_msgs::msg::Defaults";
}

template<>
inline const char * name<test_msgs::msg::Defaults>()
{
  return "test_msgs/msg/Defaults";
}

template<>
struct has_fixed_size<test_msgs::msg::Defaults>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<test_msgs::msg::Defaults>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<test_msgs::msg::Defaults>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__MSG__DETAIL__DEFAULTS__TRAITS_HPP_
