// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from statistics_msgs:msg/MetricsMessage.idl
// generated code does not contain a copyright notice

#ifndef STATISTICS_MSGS__MSG__DETAIL__METRICS_MESSAGE__TRAITS_HPP_
#define STATISTICS_MSGS__MSG__DETAIL__METRICS_MESSAGE__TRAITS_HPP_

#include "statistics_msgs/msg/detail/metrics_message__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'window_start'
// Member 'window_stop'
#include "builtin_interfaces/msg/detail/time__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<statistics_msgs::msg::MetricsMessage>()
{
  return "statistics_msgs::msg::MetricsMessage";
}

template<>
inline const char * name<statistics_msgs::msg::MetricsMessage>()
{
  return "statistics_msgs/msg/MetricsMessage";
}

template<>
struct has_fixed_size<statistics_msgs::msg::MetricsMessage>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<statistics_msgs::msg::MetricsMessage>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<statistics_msgs::msg::MetricsMessage>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // STATISTICS_MSGS__MSG__DETAIL__METRICS_MESSAGE__TRAITS_HPP_
