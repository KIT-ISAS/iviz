// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from grid_map_msgs:msg/GridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__TRAITS_HPP_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__TRAITS_HPP_

#include "grid_map_msgs/msg/detail/grid_map__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'info'
#include "grid_map_msgs/msg/detail/grid_map_info__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::msg::GridMap>()
{
  return "grid_map_msgs::msg::GridMap";
}

template<>
inline const char * name<grid_map_msgs::msg::GridMap>()
{
  return "grid_map_msgs/msg/GridMap";
}

template<>
struct has_fixed_size<grid_map_msgs::msg::GridMap>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<grid_map_msgs::msg::GridMap>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<grid_map_msgs::msg::GridMap>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__TRAITS_HPP_
