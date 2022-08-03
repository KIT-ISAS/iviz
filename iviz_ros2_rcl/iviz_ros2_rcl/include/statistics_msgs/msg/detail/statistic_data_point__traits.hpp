// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from statistics_msgs:msg/StatisticDataPoint.idl
// generated code does not contain a copyright notice

#ifndef STATISTICS_MSGS__MSG__DETAIL__STATISTIC_DATA_POINT__TRAITS_HPP_
#define STATISTICS_MSGS__MSG__DETAIL__STATISTIC_DATA_POINT__TRAITS_HPP_

#include "statistics_msgs/msg/detail/statistic_data_point__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<statistics_msgs::msg::StatisticDataPoint>()
{
  return "statistics_msgs::msg::StatisticDataPoint";
}

template<>
inline const char * name<statistics_msgs::msg::StatisticDataPoint>()
{
  return "statistics_msgs/msg/StatisticDataPoint";
}

template<>
struct has_fixed_size<statistics_msgs::msg::StatisticDataPoint>
  : std::integral_constant<bool, true> {};

template<>
struct has_bounded_size<statistics_msgs::msg::StatisticDataPoint>
  : std::integral_constant<bool, true> {};

template<>
struct is_message<statistics_msgs::msg::StatisticDataPoint>
  : std::true_type {};

}  // namespace rosidl_generator_traits

#endif  // STATISTICS_MSGS__MSG__DETAIL__STATISTIC_DATA_POINT__TRAITS_HPP_
