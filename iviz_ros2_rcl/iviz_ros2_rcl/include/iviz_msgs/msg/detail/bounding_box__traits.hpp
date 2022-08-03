// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/BoundingBox.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__TRAITS_HPP_

#include "iviz_msgs/msg/detail/bounding_box__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'center'
#include "geometry_msgs/msg/detail/pose__traits.hpp"
// Member 'size'
#include "geometry_msgs/msg/detail/vector3__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::BoundingBox>()
{
  return "iviz_msgs::msg::BoundingBox";
}

template<>
inline const char * name<iviz_msgs::msg::BoundingBox>()
{
  return "iviz_msgs/msg/BoundingBox";
}

template<>
struct has_fixed_size<iviz_msgs::msg::BoundingBox>
  : std::integral_constant<bool, has_fixed_size<geometry_msgs::msg::Pose>::value && has_fixed_size<geometry_msgs::msg::Vector3>::value> {};

template<>
struct has_bounded_size<iviz_msgs::msg::BoundingBox>
  : std::integral_constant<bool, has_bounded_size<geometry_msgs::msg::Pose>::value && has_bounded_size<geometry_msgs::msg::Vector3>::value> {};

template<>
struct is_message<iviz_msgs::msg::BoundingBox>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__TRAITS_HPP_
