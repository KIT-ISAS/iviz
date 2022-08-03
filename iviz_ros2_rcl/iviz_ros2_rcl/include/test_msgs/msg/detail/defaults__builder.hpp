// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:msg/Defaults.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__DEFAULTS__BUILDER_HPP_
#define TEST_MSGS__MSG__DETAIL__DEFAULTS__BUILDER_HPP_

#include "test_msgs/msg/detail/defaults__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace msg
{

namespace builder
{

class Init_Defaults_uint64_value
{
public:
  explicit Init_Defaults_uint64_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  ::test_msgs::msg::Defaults uint64_value(::test_msgs::msg::Defaults::_uint64_value_type arg)
  {
    msg_.uint64_value = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_int64_value
{
public:
  explicit Init_Defaults_int64_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_uint64_value int64_value(::test_msgs::msg::Defaults::_int64_value_type arg)
  {
    msg_.int64_value = std::move(arg);
    return Init_Defaults_uint64_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_uint32_value
{
public:
  explicit Init_Defaults_uint32_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_int64_value uint32_value(::test_msgs::msg::Defaults::_uint32_value_type arg)
  {
    msg_.uint32_value = std::move(arg);
    return Init_Defaults_int64_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_int32_value
{
public:
  explicit Init_Defaults_int32_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_uint32_value int32_value(::test_msgs::msg::Defaults::_int32_value_type arg)
  {
    msg_.int32_value = std::move(arg);
    return Init_Defaults_uint32_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_uint16_value
{
public:
  explicit Init_Defaults_uint16_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_int32_value uint16_value(::test_msgs::msg::Defaults::_uint16_value_type arg)
  {
    msg_.uint16_value = std::move(arg);
    return Init_Defaults_int32_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_int16_value
{
public:
  explicit Init_Defaults_int16_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_uint16_value int16_value(::test_msgs::msg::Defaults::_int16_value_type arg)
  {
    msg_.int16_value = std::move(arg);
    return Init_Defaults_uint16_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_uint8_value
{
public:
  explicit Init_Defaults_uint8_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_int16_value uint8_value(::test_msgs::msg::Defaults::_uint8_value_type arg)
  {
    msg_.uint8_value = std::move(arg);
    return Init_Defaults_int16_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_int8_value
{
public:
  explicit Init_Defaults_int8_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_uint8_value int8_value(::test_msgs::msg::Defaults::_int8_value_type arg)
  {
    msg_.int8_value = std::move(arg);
    return Init_Defaults_uint8_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_float64_value
{
public:
  explicit Init_Defaults_float64_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_int8_value float64_value(::test_msgs::msg::Defaults::_float64_value_type arg)
  {
    msg_.float64_value = std::move(arg);
    return Init_Defaults_int8_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_float32_value
{
public:
  explicit Init_Defaults_float32_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_float64_value float32_value(::test_msgs::msg::Defaults::_float32_value_type arg)
  {
    msg_.float32_value = std::move(arg);
    return Init_Defaults_float64_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_char_value
{
public:
  explicit Init_Defaults_char_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_float32_value char_value(::test_msgs::msg::Defaults::_char_value_type arg)
  {
    msg_.char_value = std::move(arg);
    return Init_Defaults_float32_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_byte_value
{
public:
  explicit Init_Defaults_byte_value(::test_msgs::msg::Defaults & msg)
  : msg_(msg)
  {}
  Init_Defaults_char_value byte_value(::test_msgs::msg::Defaults::_byte_value_type arg)
  {
    msg_.byte_value = std::move(arg);
    return Init_Defaults_char_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

class Init_Defaults_bool_value
{
public:
  Init_Defaults_bool_value()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Defaults_byte_value bool_value(::test_msgs::msg::Defaults::_bool_value_type arg)
  {
    msg_.bool_value = std::move(arg);
    return Init_Defaults_byte_value(msg_);
  }

private:
  ::test_msgs::msg::Defaults msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::msg::Defaults>()
{
  return test_msgs::msg::builder::Init_Defaults_bool_value();
}

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__DEFAULTS__BUILDER_HPP_
