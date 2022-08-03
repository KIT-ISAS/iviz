// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from lifecycle_msgs:msg/State.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__MSG__DETAIL__STATE__TRAITS_HPP_
#define LIFECYCLE_MSGS__MSG__DETAIL__STATE__TRAITS_HPP_

#include "lifecycle_msgs/msg/detail/state__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::msg::State>()
{
  return "lifecycle_msgs::msg::State";
}

template<>
inline const char * name<lifecycle_msgs::msg::State>()
{
  return "lifecycle_msgs/msg/State";
}

template<>
struct has_fixed_size<lifecycle_msgs::msg::State>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<lifecycle_msgs::msg::State>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<lifecycle_msgs::msg::State>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // LIFECYCLE_MSGS__MSG__DETAIL__STATE__TRAITS_HPP_
