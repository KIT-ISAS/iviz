// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from iviz_msgs:srv/GetSdf.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_SDF__TRAITS_HPP_
#define IVIZ_MSGS__SRV__DETAIL__GET_SDF__TRAITS_HPP_

#include "iviz_msgs/srv/detail/get_sdf__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::srv::GetSdf_Request>()
{
  return "iviz_msgs::srv::GetSdf_Request";
}

template<>
inline const char * name<iviz_msgs::srv::GetSdf_Request>()
{
  return "iviz_msgs/srv/GetSdf_Request";
}

template<>
struct has_fixed_size<iviz_msgs::srv::GetSdf_Request>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::srv::GetSdf_Request>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::srv::GetSdf_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'scene'
#include "iviz_msgs/msg/detail/scene__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::srv::GetSdf_Response>()
{
  return "iviz_msgs::srv::GetSdf_Response";
}

template<>
inline const char * name<iviz_msgs::srv::GetSdf_Response>()
{
  return "iviz_msgs/srv/GetSdf_Response";
}

template<>
struct has_fixed_size<iviz_msgs::srv::GetSdf_Response>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<iviz_msgs::srv::GetSdf_Response>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<iviz_msgs::srv::GetSdf_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<iviz_msgs::srv::GetSdf>()
{
  return "iviz_msgs::srv::GetSdf";
}

template<>
inline const char * name<iviz_msgs::srv::GetSdf>()
{
  return "iviz_msgs/srv/GetSdf";
}

template<>
struct has_fixed_size<iviz_msgs::srv::GetSdf>
  : std::integral_constant<
    bool,
    has_fixed_size<iviz_msgs::srv::GetSdf_Request>::value &&
    has_fixed_size<iviz_msgs::srv::GetSdf_Response>::value
  >
{
};

template<>
struct has_bounded_size<iviz_msgs::srv::GetSdf>
  : std::integral_constant<
    bool,
    has_bounded_size<iviz_msgs::srv::GetSdf_Request>::value &&
    has_bounded_size<iviz_msgs::srv::GetSdf_Response>::value
  >
{
};

template<>
struct is_service<iviz_msgs::srv::GetSdf>
  : std::true_type
{
};

template<>
struct is_service_request<iviz_msgs::srv::GetSdf_Request>
  : std::true_type
{
};

template<>
struct is_service_response<iviz_msgs::srv::GetSdf_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_SDF__TRAITS_HPP_