// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:msg/WStrings.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__W_STRINGS__BUILDER_HPP_
#define TEST_MSGS__MSG__DETAIL__W_STRINGS__BUILDER_HPP_

#include "test_msgs/msg/detail/w_strings__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace msg
{

namespace builder
{

class Init_WStrings_unbounded_sequence_of_wstrings
{
public:
  explicit Init_WStrings_unbounded_sequence_of_wstrings(::test_msgs::msg::WStrings & msg)
  : msg_(msg)
  {}
  ::test_msgs::msg::WStrings unbounded_sequence_of_wstrings(::test_msgs::msg::WStrings::_unbounded_sequence_of_wstrings_type arg)
  {
    msg_.unbounded_sequence_of_wstrings = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::msg::WStrings msg_;
};

class Init_WStrings_bounded_sequence_of_wstrings
{
public:
  explicit Init_WStrings_bounded_sequence_of_wstrings(::test_msgs::msg::WStrings & msg)
  : msg_(msg)
  {}
  Init_WStrings_unbounded_sequence_of_wstrings bounded_sequence_of_wstrings(::test_msgs::msg::WStrings::_bounded_sequence_of_wstrings_type arg)
  {
    msg_.bounded_sequence_of_wstrings = std::move(arg);
    return Init_WStrings_unbounded_sequence_of_wstrings(msg_);
  }

private:
  ::test_msgs::msg::WStrings msg_;
};

class Init_WStrings_array_of_wstrings
{
public:
  explicit Init_WStrings_array_of_wstrings(::test_msgs::msg::WStrings & msg)
  : msg_(msg)
  {}
  Init_WStrings_bounded_sequence_of_wstrings array_of_wstrings(::test_msgs::msg::WStrings::_array_of_wstrings_type arg)
  {
    msg_.array_of_wstrings = std::move(arg);
    return Init_WStrings_bounded_sequence_of_wstrings(msg_);
  }

private:
  ::test_msgs::msg::WStrings msg_;
};

class Init_WStrings_wstring_value_default3
{
public:
  explicit Init_WStrings_wstring_value_default3(::test_msgs::msg::WStrings & msg)
  : msg_(msg)
  {}
  Init_WStrings_array_of_wstrings wstring_value_default3(::test_msgs::msg::WStrings::_wstring_value_default3_type arg)
  {
    msg_.wstring_value_default3 = std::move(arg);
    return Init_WStrings_array_of_wstrings(msg_);
  }

private:
  ::test_msgs::msg::WStrings msg_;
};

class Init_WStrings_wstring_value_default2
{
public:
  explicit Init_WStrings_wstring_value_default2(::test_msgs::msg::WStrings & msg)
  : msg_(msg)
  {}
  Init_WStrings_wstring_value_default3 wstring_value_default2(::test_msgs::msg::WStrings::_wstring_value_default2_type arg)
  {
    msg_.wstring_value_default2 = std::move(arg);
    return Init_WStrings_wstring_value_default3(msg_);
  }

private:
  ::test_msgs::msg::WStrings msg_;
};

class Init_WStrings_wstring_value_default1
{
public:
  explicit Init_WStrings_wstring_value_default1(::test_msgs::msg::WStrings & msg)
  : msg_(msg)
  {}
  Init_WStrings_wstring_value_default2 wstring_value_default1(::test_msgs::msg::WStrings::_wstring_value_default1_type arg)
  {
    msg_.wstring_value_default1 = std::move(arg);
    return Init_WStrings_wstring_value_default2(msg_);
  }

private:
  ::test_msgs::msg::WStrings msg_;
};

class Init_WStrings_wstring_value
{
public:
  Init_WStrings_wstring_value()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_WStrings_wstring_value_default1 wstring_value(::test_msgs::msg::WStrings::_wstring_value_type arg)
  {
    msg_.wstring_value = std::move(arg);
    return Init_WStrings_wstring_value_default1(msg_);
  }

private:
  ::test_msgs::msg::WStrings msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::msg::WStrings>()
{
  return test_msgs::msg::builder::Init_WStrings_wstring_value();
}

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__W_STRINGS__BUILDER_HPP_
