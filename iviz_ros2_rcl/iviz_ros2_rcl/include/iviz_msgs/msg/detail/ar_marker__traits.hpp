// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/ARMarker.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__AR_MARKER__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__AR_MARKER__TRAITS_HPP_

#include "iviz_msgs/msg/detail/ar_marker__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__traits.hpp"
// Member 'corners'
#include "geometry_msgs/msg/detail/vector3__traits.hpp"
// Member 'camera_pose'
// Member 'pose_relative_to_camera'
#include "geometry_msgs/msg/detail/pose__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::ARMarker>()
{
  return "iviz_msgs::msg::ARMarker";
}

template<>
inline const char * name<iviz_msgs::msg::ARMarker>()
{
  return "iviz_msgs/msg/ARMarker";
}

template<>
struct has_fixed_size<iviz_msgs::msg::ARMarker>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::msg::ARMarker>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::msg::ARMarker>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__AR_MARKER__TRAITS_HPP_
