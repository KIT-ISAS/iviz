// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:msg/Builtins.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__BUILTINS__BUILDER_HPP_
#define TEST_MSGS__MSG__DETAIL__BUILTINS__BUILDER_HPP_

#include "test_msgs/msg/detail/builtins__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace msg
{

namespace builder
{

class Init_Builtins_time_value
{
public:
  explicit Init_Builtins_time_value(::test_msgs::msg::Builtins & msg)
  : msg_(msg)
  {}
  ::test_msgs::msg::Builtins time_value(::test_msgs::msg::Builtins::_time_value_type arg)
  {
    msg_.time_value = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::msg::Builtins msg_;
};

class Init_Builtins_duration_value
{
public:
  Init_Builtins_duration_value()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Builtins_time_value duration_value(::test_msgs::msg::Builtins::_duration_value_type arg)
  {
    msg_.duration_value = std::move(arg);
    return Init_Builtins_time_value(msg_);
  }

private:
  ::test_msgs::msg::Builtins msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::msg::Builtins>()
{
  return test_msgs::msg::builder::Init_Builtins_duration_value();
}

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__BUILTINS__BUILDER_HPP_
