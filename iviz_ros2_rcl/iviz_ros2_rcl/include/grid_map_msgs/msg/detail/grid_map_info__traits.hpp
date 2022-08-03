// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from grid_map_msgs:msg/GridMapInfo.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__TRAITS_HPP_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__TRAITS_HPP_

#include "grid_map_msgs/msg/detail/grid_map_info__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__traits.hpp"
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::msg::GridMapInfo>()
{
  return "grid_map_msgs::msg::GridMapInfo";
}

template<>
inline const char * name<grid_map_msgs::msg::GridMapInfo>()
{
  return "grid_map_msgs/msg/GridMapInfo";
}

template<>
struct has_fixed_size<grid_map_msgs::msg::GridMapInfo>
  : std::integral_constant<bool, has_fixed_size<geometry_msgs::msg::Pose>::value && has_fixed_size<std_msgs::msg::Header>::value> {};

template<>
struct has_bounded_size<grid_map_msgs::msg::GridMapInfo>
  : std::integral_constant<bool, has_bounded_size<geometry_msgs::msg::Pose>::value && has_bounded_size<std_msgs::msg::Header>::value> {};

template<>
struct is_message<grid_map_msgs::msg::GridMapInfo>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__TRAITS_HPP_
