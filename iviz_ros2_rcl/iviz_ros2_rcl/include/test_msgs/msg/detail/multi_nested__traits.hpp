// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:msg/MultiNested.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__MULTI_NESTED__TRAITS_HPP_
#define TEST_MSGS__MSG__DETAIL__MULTI_NESTED__TRAITS_HPP_

#include "test_msgs/msg/detail/multi_nested__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'array_of_arrays'
// Member 'bounded_sequence_of_arrays'
#include "test_msgs/msg/detail/arrays__traits.hpp"
// Member 'array_of_bounded_sequences'
// Member 'bounded_sequence_of_bounded_sequences'
#include "test_msgs/msg/detail/bounded_sequences__traits.hpp"
// Member 'array_of_unbounded_sequences'
// Member 'bounded_sequence_of_unbounded_sequences'
#include "test_msgs/msg/detail/unbounded_sequences__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::msg::MultiNested>()
{
  return "test_msgs::msg::MultiNested";
}

template<>
inline const char * name<test_msgs::msg::MultiNested>()
{
  return "test_msgs/msg/MultiNested";
}

template<>
struct has_fixed_size<test_msgs::msg::MultiNested>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<test_msgs::msg::MultiNested>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<test_msgs::msg::MultiNested>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__MSG__DETAIL__MULTI_NESTED__TRAITS_HPP_
