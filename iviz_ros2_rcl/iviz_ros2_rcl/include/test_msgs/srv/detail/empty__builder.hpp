// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:srv/Empty.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__SRV__DETAIL__EMPTY__BUILDER_HPP_
#define TEST_MSGS__SRV__DETAIL__EMPTY__BUILDER_HPP_

#include "test_msgs/srv/detail/empty__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace srv
{


}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::srv::Empty_Request>()
{
  return ::test_msgs::srv::Empty_Request(rosidl_runtime_cpp::MessageInitialization::ZERO);
}

}  // namespace test_msgs


namespace test_msgs
{

namespace srv
{


}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::srv::Empty_Response>()
{
  return ::test_msgs::srv::Empty_Response(rosidl_runtime_cpp::MessageInitialization::ZERO);
}

}  // namespace test_msgs

#endif  // TEST_MSGS__SRV__DETAIL__EMPTY__BUILDER_HPP_
