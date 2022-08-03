// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/Feedback.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__FEEDBACK__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__FEEDBACK__TRAITS_HPP_

#include "iviz_msgs/msg/detail/feedback__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__traits.hpp"
// Member 'position'
#include "geometry_msgs/msg/detail/point__traits.hpp"
// Member 'orientation'
#include "geometry_msgs/msg/detail/quaternion__traits.hpp"
// Member 'scale'
#include "geometry_msgs/msg/detail/vector3__traits.hpp"
// Member 'trajectory'
#include "iviz_msgs/msg/detail/trajectory__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::Feedback>()
{
  return "iviz_msgs::msg::Feedback";
}

template<>
inline const char * name<iviz_msgs::msg::Feedback>()
{
  return "iviz_msgs/msg/Feedback";
}

template<>
struct has_fixed_size<iviz_msgs::msg::Feedback>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::msg::Feedback>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::msg::Feedback>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__FEEDBACK__TRAITS_HPP_
