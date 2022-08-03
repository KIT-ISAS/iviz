// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/Color32.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__COLOR32__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__COLOR32__TRAITS_HPP_

#include "iviz_msgs/msg/detail/color32__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::Color32>()
{
  return "iviz_msgs::msg::Color32";
}

template<>
inline const char * name<iviz_msgs::msg::Color32>()
{
  return "iviz_msgs/msg/Color32";
}

template<>
struct has_fixed_size<iviz_msgs::msg::Color32>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<iviz_msgs::msg::Color32>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<iviz_msgs::msg::Color32>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__COLOR32__TRAITS_HPP_
