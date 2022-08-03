// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:msg/Strings.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__STRINGS__BUILDER_HPP_
#define TEST_MSGS__MSG__DETAIL__STRINGS__BUILDER_HPP_

#include "test_msgs/msg/detail/strings__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace msg
{

namespace builder
{

class Init_Strings_bounded_string_value_default5
{
public:
  explicit Init_Strings_bounded_string_value_default5(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  ::test_msgs::msg::Strings bounded_string_value_default5(::test_msgs::msg::Strings::_bounded_string_value_default5_type arg)
  {
    msg_.bounded_string_value_default5 = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_bounded_string_value_default4
{
public:
  explicit Init_Strings_bounded_string_value_default4(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_bounded_string_value_default5 bounded_string_value_default4(::test_msgs::msg::Strings::_bounded_string_value_default4_type arg)
  {
    msg_.bounded_string_value_default4 = std::move(arg);
    return Init_Strings_bounded_string_value_default5(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_bounded_string_value_default3
{
public:
  explicit Init_Strings_bounded_string_value_default3(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_bounded_string_value_default4 bounded_string_value_default3(::test_msgs::msg::Strings::_bounded_string_value_default3_type arg)
  {
    msg_.bounded_string_value_default3 = std::move(arg);
    return Init_Strings_bounded_string_value_default4(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_bounded_string_value_default2
{
public:
  explicit Init_Strings_bounded_string_value_default2(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_bounded_string_value_default3 bounded_string_value_default2(::test_msgs::msg::Strings::_bounded_string_value_default2_type arg)
  {
    msg_.bounded_string_value_default2 = std::move(arg);
    return Init_Strings_bounded_string_value_default3(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_bounded_string_value_default1
{
public:
  explicit Init_Strings_bounded_string_value_default1(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_bounded_string_value_default2 bounded_string_value_default1(::test_msgs::msg::Strings::_bounded_string_value_default1_type arg)
  {
    msg_.bounded_string_value_default1 = std::move(arg);
    return Init_Strings_bounded_string_value_default2(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_bounded_string_value
{
public:
  explicit Init_Strings_bounded_string_value(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_bounded_string_value_default1 bounded_string_value(::test_msgs::msg::Strings::_bounded_string_value_type arg)
  {
    msg_.bounded_string_value = std::move(arg);
    return Init_Strings_bounded_string_value_default1(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_string_value_default5
{
public:
  explicit Init_Strings_string_value_default5(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_bounded_string_value string_value_default5(::test_msgs::msg::Strings::_string_value_default5_type arg)
  {
    msg_.string_value_default5 = std::move(arg);
    return Init_Strings_bounded_string_value(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_string_value_default4
{
public:
  explicit Init_Strings_string_value_default4(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_string_value_default5 string_value_default4(::test_msgs::msg::Strings::_string_value_default4_type arg)
  {
    msg_.string_value_default4 = std::move(arg);
    return Init_Strings_string_value_default5(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_string_value_default3
{
public:
  explicit Init_Strings_string_value_default3(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_string_value_default4 string_value_default3(::test_msgs::msg::Strings::_string_value_default3_type arg)
  {
    msg_.string_value_default3 = std::move(arg);
    return Init_Strings_string_value_default4(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_string_value_default2
{
public:
  explicit Init_Strings_string_value_default2(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_string_value_default3 string_value_default2(::test_msgs::msg::Strings::_string_value_default2_type arg)
  {
    msg_.string_value_default2 = std::move(arg);
    return Init_Strings_string_value_default3(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_string_value_default1
{
public:
  explicit Init_Strings_string_value_default1(::test_msgs::msg::Strings & msg)
  : msg_(msg)
  {}
  Init_Strings_string_value_default2 string_value_default1(::test_msgs::msg::Strings::_string_value_default1_type arg)
  {
    msg_.string_value_default1 = std::move(arg);
    return Init_Strings_string_value_default2(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

class Init_Strings_string_value
{
public:
  Init_Strings_string_value()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Strings_string_value_default1 string_value(::test_msgs::msg::Strings::_string_value_type arg)
  {
    msg_.string_value = std::move(arg);
    return Init_Strings_string_value_default1(msg_);
  }

private:
  ::test_msgs::msg::Strings msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::msg::Strings>()
{
  return test_msgs::msg::builder::Init_Strings_string_value();
}

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__STRINGS__BUILDER_HPP_
