// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from lifecycle_msgs:msg/Transition.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__MSG__DETAIL__TRANSITION__TRAITS_HPP_
#define LIFECYCLE_MSGS__MSG__DETAIL__TRANSITION__TRAITS_HPP_

#include "lifecycle_msgs/msg/detail/transition__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<lifecycle_msgs::msg::Transition>()
{
  return "lifecycle_msgs::msg::Transition";
}

template<>
inline const char * name<lifecycle_msgs::msg::Transition>()
{
  return "lifecycle_msgs/msg/Transition";
}

template<>
struct has_fixed_size<lifecycle_msgs::msg::Transition>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<lifecycle_msgs::msg::Transition>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<lifecycle_msgs::msg::Transition>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // LIFECYCLE_MSGS__MSG__DETAIL__TRANSITION__TRAITS_HPP_
