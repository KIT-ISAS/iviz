// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:msg/ARMarkerArray.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__TRAITS_HPP_
#define IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__TRAITS_HPP_

#include "iviz_msgs/msg/detail/ar_marker_array__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::msg::ARMarkerArray>()
{
  return "iviz_msgs::msg::ARMarkerArray";
}

template<>
inline const char * name<iviz_msgs::msg::ARMarkerArray>()
{
  return "iviz_msgs/msg/ARMarkerArray";
}

template<>
struct has_fixed_size<iviz_msgs::msg::ARMarkerArray>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::msg::ARMarkerArray>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::msg::ARMarkerArray>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__TRAITS_HPP_
