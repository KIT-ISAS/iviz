// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from grid_map_msgs:srv/GetGridMapInfo.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__TRAITS_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__TRAITS_HPP_

#include "grid_map_msgs/srv/detail/get_grid_map_info__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::srv::GetGridMapInfo_Request>()
{
  return "grid_map_msgs::srv::GetGridMapInfo_Request";
}

template<>
inline const char * name<grid_map_msgs::srv::GetGridMapInfo_Request>()
{
  return "grid_map_msgs/srv/GetGridMapInfo_Request";
}

template<>
struct has_fixed_size<grid_map_msgs::srv::GetGridMapInfo_Request>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<grid_map_msgs::srv::GetGridMapInfo_Request>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<grid_map_msgs::srv::GetGridMapInfo_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'info'
#include "grid_map_msgs/msg/detail/grid_map_info__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::srv::GetGridMapInfo_Response>()
{
  return "grid_map_msgs::srv::GetGridMapInfo_Response";
}

template<>
inline const char * name<grid_map_msgs::srv::GetGridMapInfo_Response>()
{
  return "grid_map_msgs/srv/GetGridMapInfo_Response";
}

template<>
struct has_fixed_size<grid_map_msgs::srv::GetGridMapInfo_Response>
  : std::integral_constant<bool, has_fixed_size<grid_map_msgs::msg::GridMapInfo>::value> {};

template<>
struct has_bounded_size<grid_map_msgs::srv::GetGridMapInfo_Response>
  : std::integral_constant<bool, has_bounded_size<grid_map_msgs::msg::GridMapInfo>::value> {};

template<>
struct is_message<grid_map_msgs::srv::GetGridMapInfo_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<grid_map_msgs::srv::GetGridMapInfo>()
{
  return "grid_map_msgs::srv::GetGridMapInfo";
}

template<>
inline const char * name<grid_map_msgs::srv::GetGridMapInfo>()
{
  return "grid_map_msgs/srv/GetGridMapInfo";
}

template<>
struct has_fixed_size<grid_map_msgs::srv::GetGridMapInfo>
  : std::integral_constant<
    bool,
    has_fixed_size<grid_map_msgs::srv::GetGridMapInfo_Request>::value &&
    has_fixed_size<grid_map_msgs::srv::GetGridMapInfo_Response>::value
  >
{
};

template<>
struct has_bounded_size<grid_map_msgs::srv::GetGridMapInfo>
  : std::integral_constant<
    bool,
    has_bounded_size<grid_map_msgs::srv::GetGridMapInfo_Request>::value &&
    has_bounded_size<grid_map_msgs::srv::GetGridMapInfo_Response>::value
  >
{
};

template<>
struct is_service<grid_map_msgs::srv::GetGridMapInfo>
  : std::true_type
{
};

template<>
struct is_service_request<grid_map_msgs::srv::GetGridMapInfo_Request>
  : std::true_type
{
};

template<>
struct is_service_response<grid_map_msgs::srv::GetGridMapInfo_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__TRAITS_HPP_
