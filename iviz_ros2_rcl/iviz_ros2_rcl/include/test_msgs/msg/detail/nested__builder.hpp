// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:msg/Nested.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__NESTED__BUILDER_HPP_
#define TEST_MSGS__MSG__DETAIL__NESTED__BUILDER_HPP_

#include "test_msgs/msg/detail/nested__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace msg
{

namespace builder
{

class Init_Nested_basic_types_value
{
public:
  Init_Nested_basic_types_value()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::test_msgs::msg::Nested basic_types_value(::test_msgs::msg::Nested::_basic_types_value_type arg)
  {
    msg_.basic_types_value = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::msg::Nested msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::msg::Nested>()
{
  return test_msgs::msg::builder::Init_Nested_basic_types_value();
}

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__NESTED__BUILDER_HPP_
