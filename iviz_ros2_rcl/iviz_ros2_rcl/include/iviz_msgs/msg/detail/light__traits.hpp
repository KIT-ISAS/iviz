// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/Light.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__LIGHT__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__LIGHT__TRAITS_HPP_

#include "iviz_msgs/msg/detail/light__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'diffuse'
#include "iviz_msgs/msg/detail/color32__traits.hpp"
// Member 'position'
// Member 'direction'
#include "iviz_msgs/msg/detail/vector3f__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::Light>()
{
  return "iviz_msgs::msg::Light";
}

template<>
inline const char * name<iviz_msgs::msg::Light>()
{
  return "iviz_msgs/msg/Light";
}

template<>
struct has_fixed_size<iviz_msgs::msg::Light>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::msg::Light>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::msg::Light>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__LIGHT__TRAITS_HPP_
