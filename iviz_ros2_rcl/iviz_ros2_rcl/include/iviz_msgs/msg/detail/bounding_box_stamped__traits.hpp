// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/BoundingBoxStamped.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__TRAITS_HPP_

#include "iviz_msgs/msg/detail/bounding_box_stamped__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__traits.hpp"
// Member 'boundary'
#include "iviz_msgs/msg/detail/bounding_box__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::BoundingBoxStamped>()
{
  return "iviz_msgs::msg::BoundingBoxStamped";
}

template<>
inline const char * name<iviz_msgs::msg::BoundingBoxStamped>()
{
  return "iviz_msgs/msg/BoundingBoxStamped";
}

template<>
struct has_fixed_size<iviz_msgs::msg::BoundingBoxStamped>
  : std::integral_constant<bool, has_fixed_size<iviz_msgs::msg::BoundingBox>::value && has_fixed_size<std_msgs::msg::Header>::value> {};

template<>
struct has_bounded_size<iviz_msgs::msg::BoundingBoxStamped>
  : std::integral_constant<bool, has_bounded_size<iviz_msgs::msg::BoundingBox>::value && has_bounded_size<std_msgs::msg::Header>::value> {};

template<>
struct is_message<iviz_msgs::msg::BoundingBoxStamped>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__TRAITS_HPP_
