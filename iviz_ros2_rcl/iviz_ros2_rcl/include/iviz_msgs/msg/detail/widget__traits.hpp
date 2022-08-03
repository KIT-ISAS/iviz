// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/Widget.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__WIDGET__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__WIDGET__TRAITS_HPP_

#include "iviz_msgs/msg/detail/widget__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__traits.hpp"
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__traits.hpp"
// Member 'color'
// Member 'secondary_color'
#include "std_msgs/msg/detail/color_rgba__traits.hpp"
// Member 'boundary'
#include "iviz_msgs/msg/detail/bounding_box__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::Widget>()
{
  return "iviz_msgs::msg::Widget";
}

template<>
inline const char * name<iviz_msgs::msg::Widget>()
{
  return "iviz_msgs/msg/Widget";
}

template<>
struct has_fixed_size<iviz_msgs::msg::Widget>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::msg::Widget>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::msg::Widget>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__WIDGET__TRAITS_HPP_
