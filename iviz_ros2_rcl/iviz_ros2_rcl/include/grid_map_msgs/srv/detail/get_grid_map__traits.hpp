// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from grid_map_msgs:srv/GetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__TRAITS_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__TRAITS_HPP_

#include "grid_map_msgs/srv/detail/get_grid_map__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::srv::GetGridMap_Request>()
{
  return "grid_map_msgs::srv::GetGridMap_Request";
}

template<>
inline const char * name<grid_map_msgs::srv::GetGridMap_Request>()
{
  return "grid_map_msgs/srv/GetGridMap_Request";
}

template<>
struct has_fixed_size<grid_map_msgs::srv::GetGridMap_Request>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<grid_map_msgs::srv::GetGridMap_Request>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<grid_map_msgs::srv::GetGridMap_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'map'
#include "grid_map_msgs/msg/detail/grid_map__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::srv::GetGridMap_Response>()
{
  return "grid_map_msgs::srv::GetGridMap_Response";
}

template<>
inline const char * name<grid_map_msgs::srv::GetGridMap_Response>()
{
  return "grid_map_msgs/srv/GetGridMap_Response";
}

template<>
struct has_fixed_size<grid_map_msgs::srv::GetGridMap_Response>
  : std::integral_constant<bool, has_fixed_size<grid_map_msgs::msg::GridMap>::value> {};

template<>
struct has_bounded_size<grid_map_msgs::srv::GetGridMap_Response>
  : std::integral_constant<bool, has_bounded_size<grid_map_msgs::msg::GridMap>::value> {};

template<>
struct is_message<grid_map_msgs::srv::GetGridMap_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::srv::GetGridMap>()
{
  return "grid_map_msgs::srv::GetGridMap";
}

template<>
inline const char * name<grid_map_msgs::srv::GetGridMap>()
{
  return "grid_map_msgs/srv/GetGridMap";
}

template<>
struct has_fixed_size<grid_map_msgs::srv::GetGridMap>
  : std::integral_constant<
    bool,
    has_fixed_size<grid_map_msgs::srv::GetGridMap_Request>::value &&
    has_fixed_size<grid_map_msgs::srv::GetGridMap_Response>::value
  >
{
};

template<>
struct has_bounded_size<grid_map_msgs::srv::GetGridMap>
  : std::integral_constant<
    bool,
    has_bounded_size<grid_map_msgs::srv::GetGridMap_Request>::value &&
    has_bounded_size<grid_map_msgs::srv::GetGridMap_Response>::value
  >
{
};

template<>
struct is_service<grid_map_msgs::srv::GetGridMap>
  : std::true_type
{
};

template<>
struct is_service_request<grid_map_msgs::srv::GetGridMap_Request>
  : std::true_type
{
};

template<>
struct is_service_response<grid_map_msgs::srv::GetGridMap_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__TRAITS_HPP_
