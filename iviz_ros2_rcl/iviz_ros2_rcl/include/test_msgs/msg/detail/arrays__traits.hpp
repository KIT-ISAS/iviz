// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:msg/Arrays.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__ARRAYS__TRAITS_HPP_
#define TEST_MSGS__MSG__DETAIL__ARRAYS__TRAITS_HPP_

#include "test_msgs/msg/detail/arrays__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'basic_types_values'
#include "test_msgs/msg/detail/basic_types__traits.hpp"
// Member 'constants_values'
#include "test_msgs/msg/detail/constants__traits.hpp"
// Member 'defaults_values'
#include "test_msgs/msg/detail/defaults__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::msg::Arrays>()
{
  return "test_msgs::msg::Arrays";
}

template<>
inline const char * name<test_msgs::msg::Arrays>()
{
  return "test_msgs/msg/Arrays";
}

template<>
struct has_fixed_size<test_msgs::msg::Arrays>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<test_msgs::msg::Arrays>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<test_msgs::msg::Arrays>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__MSG__DETAIL__ARRAYS__TRAITS_HPP_
