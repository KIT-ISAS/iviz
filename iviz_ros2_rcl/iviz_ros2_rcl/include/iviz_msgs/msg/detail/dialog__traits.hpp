// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/Dialog.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__DIALOG__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__DIALOG__TRAITS_HPP_

#include "iviz_msgs/msg/detail/dialog__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__traits.hpp"
// Member 'lifetime'
#include "builtin_interfaces/msg/detail/duration__traits.hpp"
// Member 'background_color'
#include "std_msgs/msg/detail/color_rgba__traits.hpp"
// Member 'tf_offset'
// Member 'dialog_displacement'
// Member 'tf_displacement'
#include "geometry_msgs/msg/detail/vector3__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::Dialog>()
{
  return "iviz_msgs::msg::Dialog";
}

template<>
inline const char * name<iviz_msgs::msg::Dialog>()
{
  return "iviz_msgs/msg/Dialog";
}

template<>
struct has_fixed_size<iviz_msgs::msg::Dialog>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::msg::Dialog>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::msg::Dialog>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__DIALOG__TRAITS_HPP_