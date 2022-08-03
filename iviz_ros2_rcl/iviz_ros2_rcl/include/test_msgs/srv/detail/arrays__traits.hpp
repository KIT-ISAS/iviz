// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:srv/Arrays.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__SRV__DETAIL__ARRAYS__TRAITS_HPP_
#define TEST_MSGS__SRV__DETAIL__ARRAYS__TRAITS_HPP_

#include "test_msgs/srv/detail/arrays__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'basic_types_values'
#include "test_msgs/msg/detail/basic_types__traits.hpp"
// Member 'constants_values'
#include "test_msgs/msg/detail/constants__traits.hpp"
// Member 'defaults_values'
#include "test_msgs/msg/detail/defaults__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::Arrays_Request>()
{
  return "test_msgs::srv::Arrays_Request";
}

template<>
inline const char * name<test_msgs::srv::Arrays_Request>()
{
  return "test_msgs/srv/Arrays_Request";
}

template<>
struct has_fixed_size<test_msgs::srv::Arrays_Request>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<test_msgs::srv::Arrays_Request>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<test_msgs::srv::Arrays_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'basic_types_values'
// already included above
// #include "test_msgs/msg/detail/basic_types__traits.hpp"
// Member 'constants_values'
// already included above
// #include "test_msgs/msg/detail/constants__traits.hpp"
// Member 'defaults_values'
// already included above
// #include "test_msgs/msg/detail/defaults__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::Arrays_Response>()
{
  return "test_msgs::srv::Arrays_Response";
}

template<>
inline const char * name<test_msgs::srv::Arrays_Response>()
{
  return "test_msgs/srv/Arrays_Response";
}

template<>
struct has_fixed_size<test_msgs::srv::Arrays_Response>
  : std::integral_constant<bool, false> {};

template<>
struct has_bounded_size<test_msgs::srv::Arrays_Response>
  : std::integral_constant<bool, false> {};

template<>
struct is_message<test_msgs::srv::Arrays_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::srv::Arrays>()
{
  return "test_msgs::srv::Arrays";
}

template<>
inline const char * name<test_msgs::srv::Arrays>()
{
  return "test_msgs/srv/Arrays";
}

template<>
struct has_fixed_size<test_msgs::srv::Arrays>
  : std::integral_constant<
    bool,
    has_fixed_size<test_msgs::srv::Arrays_Request>::value &&
    has_fixed_size<test_msgs::srv::Arrays_Response>::value
  >
{
};

template<>
struct has_bounded_size<test_msgs::srv::Arrays>
  : std::integral_constant<
    bool,
    has_bounded_size<test_msgs::srv::Arrays_Request>::value &&
    has_bounded_size<test_msgs::srv::Arrays_Response>::value
  >
{
};

template<>
struct is_service<test_msgs::srv::Arrays>
  : std::true_type
{
};

template<>
struct is_service_request<test_msgs::srv::Arrays_Request>
  : std::true_type
{
};

template<>
struct is_service_response<test_msgs::srv::Arrays_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

#endif  // TEST_MSGS__SRV__DETAIL__ARRAYS__TRAITS_HPP_
