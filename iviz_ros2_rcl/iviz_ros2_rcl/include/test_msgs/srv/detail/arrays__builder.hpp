// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:srv/Arrays.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__SRV__DETAIL__ARRAYS__BUILDER_HPP_
#define TEST_MSGS__SRV__DETAIL__ARRAYS__BUILDER_HPP_

#include "test_msgs/srv/detail/arrays__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace srv
{

namespace builder
{

class Init_Arrays_Request_string_values_default
{
public:
  explicit Init_Arrays_Request_string_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  ::test_msgs::srv::Arrays_Request string_values_default(::test_msgs::srv::Arrays_Request::_string_values_default_type arg)
  {
    msg_.string_values_default = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint64_values_default
{
public:
  explicit Init_Arrays_Request_uint64_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_string_values_default uint64_values_default(::test_msgs::srv::Arrays_Request::_uint64_values_default_type arg)
  {
    msg_.uint64_values_default = std::move(arg);
    return Init_Arrays_Request_string_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int64_values_default
{
public:
  explicit Init_Arrays_Request_int64_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint64_values_default int64_values_default(::test_msgs::srv::Arrays_Request::_int64_values_default_type arg)
  {
    msg_.int64_values_default = std::move(arg);
    return Init_Arrays_Request_uint64_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint32_values_default
{
public:
  explicit Init_Arrays_Request_uint32_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int64_values_default uint32_values_default(::test_msgs::srv::Arrays_Request::_uint32_values_default_type arg)
  {
    msg_.uint32_values_default = std::move(arg);
    return Init_Arrays_Request_int64_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int32_values_default
{
public:
  explicit Init_Arrays_Request_int32_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint32_values_default int32_values_default(::test_msgs::srv::Arrays_Request::_int32_values_default_type arg)
  {
    msg_.int32_values_default = std::move(arg);
    return Init_Arrays_Request_uint32_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint16_values_default
{
public:
  explicit Init_Arrays_Request_uint16_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int32_values_default uint16_values_default(::test_msgs::srv::Arrays_Request::_uint16_values_default_type arg)
  {
    msg_.uint16_values_default = std::move(arg);
    return Init_Arrays_Request_int32_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int16_values_default
{
public:
  explicit Init_Arrays_Request_int16_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint16_values_default int16_values_default(::test_msgs::srv::Arrays_Request::_int16_values_default_type arg)
  {
    msg_.int16_values_default = std::move(arg);
    return Init_Arrays_Request_uint16_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint8_values_default
{
public:
  explicit Init_Arrays_Request_uint8_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int16_values_default uint8_values_default(::test_msgs::srv::Arrays_Request::_uint8_values_default_type arg)
  {
    msg_.uint8_values_default = std::move(arg);
    return Init_Arrays_Request_int16_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int8_values_default
{
public:
  explicit Init_Arrays_Request_int8_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint8_values_default int8_values_default(::test_msgs::srv::Arrays_Request::_int8_values_default_type arg)
  {
    msg_.int8_values_default = std::move(arg);
    return Init_Arrays_Request_uint8_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_float64_values_default
{
public:
  explicit Init_Arrays_Request_float64_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int8_values_default float64_values_default(::test_msgs::srv::Arrays_Request::_float64_values_default_type arg)
  {
    msg_.float64_values_default = std::move(arg);
    return Init_Arrays_Request_int8_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_float32_values_default
{
public:
  explicit Init_Arrays_Request_float32_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_float64_values_default float32_values_default(::test_msgs::srv::Arrays_Request::_float32_values_default_type arg)
  {
    msg_.float32_values_default = std::move(arg);
    return Init_Arrays_Request_float64_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_char_values_default
{
public:
  explicit Init_Arrays_Request_char_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_float32_values_default char_values_default(::test_msgs::srv::Arrays_Request::_char_values_default_type arg)
  {
    msg_.char_values_default = std::move(arg);
    return Init_Arrays_Request_float32_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_byte_values_default
{
public:
  explicit Init_Arrays_Request_byte_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_char_values_default byte_values_default(::test_msgs::srv::Arrays_Request::_byte_values_default_type arg)
  {
    msg_.byte_values_default = std::move(arg);
    return Init_Arrays_Request_char_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_bool_values_default
{
public:
  explicit Init_Arrays_Request_bool_values_default(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_byte_values_default bool_values_default(::test_msgs::srv::Arrays_Request::_bool_values_default_type arg)
  {
    msg_.bool_values_default = std::move(arg);
    return Init_Arrays_Request_byte_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_defaults_values
{
public:
  explicit Init_Arrays_Request_defaults_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_bool_values_default defaults_values(::test_msgs::srv::Arrays_Request::_defaults_values_type arg)
  {
    msg_.defaults_values = std::move(arg);
    return Init_Arrays_Request_bool_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_constants_values
{
public:
  explicit Init_Arrays_Request_constants_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_defaults_values constants_values(::test_msgs::srv::Arrays_Request::_constants_values_type arg)
  {
    msg_.constants_values = std::move(arg);
    return Init_Arrays_Request_defaults_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_basic_types_values
{
public:
  explicit Init_Arrays_Request_basic_types_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_constants_values basic_types_values(::test_msgs::srv::Arrays_Request::_basic_types_values_type arg)
  {
    msg_.basic_types_values = std::move(arg);
    return Init_Arrays_Request_constants_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_string_values
{
public:
  explicit Init_Arrays_Request_string_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_basic_types_values string_values(::test_msgs::srv::Arrays_Request::_string_values_type arg)
  {
    msg_.string_values = std::move(arg);
    return Init_Arrays_Request_basic_types_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint64_values
{
public:
  explicit Init_Arrays_Request_uint64_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_string_values uint64_values(::test_msgs::srv::Arrays_Request::_uint64_values_type arg)
  {
    msg_.uint64_values = std::move(arg);
    return Init_Arrays_Request_string_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int64_values
{
public:
  explicit Init_Arrays_Request_int64_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint64_values int64_values(::test_msgs::srv::Arrays_Request::_int64_values_type arg)
  {
    msg_.int64_values = std::move(arg);
    return Init_Arrays_Request_uint64_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint32_values
{
public:
  explicit Init_Arrays_Request_uint32_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int64_values uint32_values(::test_msgs::srv::Arrays_Request::_uint32_values_type arg)
  {
    msg_.uint32_values = std::move(arg);
    return Init_Arrays_Request_int64_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int32_values
{
public:
  explicit Init_Arrays_Request_int32_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint32_values int32_values(::test_msgs::srv::Arrays_Request::_int32_values_type arg)
  {
    msg_.int32_values = std::move(arg);
    return Init_Arrays_Request_uint32_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint16_values
{
public:
  explicit Init_Arrays_Request_uint16_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int32_values uint16_values(::test_msgs::srv::Arrays_Request::_uint16_values_type arg)
  {
    msg_.uint16_values = std::move(arg);
    return Init_Arrays_Request_int32_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int16_values
{
public:
  explicit Init_Arrays_Request_int16_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint16_values int16_values(::test_msgs::srv::Arrays_Request::_int16_values_type arg)
  {
    msg_.int16_values = std::move(arg);
    return Init_Arrays_Request_uint16_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_uint8_values
{
public:
  explicit Init_Arrays_Request_uint8_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int16_values uint8_values(::test_msgs::srv::Arrays_Request::_uint8_values_type arg)
  {
    msg_.uint8_values = std::move(arg);
    return Init_Arrays_Request_int16_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_int8_values
{
public:
  explicit Init_Arrays_Request_int8_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_uint8_values int8_values(::test_msgs::srv::Arrays_Request::_int8_values_type arg)
  {
    msg_.int8_values = std::move(arg);
    return Init_Arrays_Request_uint8_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_float64_values
{
public:
  explicit Init_Arrays_Request_float64_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_int8_values float64_values(::test_msgs::srv::Arrays_Request::_float64_values_type arg)
  {
    msg_.float64_values = std::move(arg);
    return Init_Arrays_Request_int8_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_float32_values
{
public:
  explicit Init_Arrays_Request_float32_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_float64_values float32_values(::test_msgs::srv::Arrays_Request::_float32_values_type arg)
  {
    msg_.float32_values = std::move(arg);
    return Init_Arrays_Request_float64_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_char_values
{
public:
  explicit Init_Arrays_Request_char_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_float32_values char_values(::test_msgs::srv::Arrays_Request::_char_values_type arg)
  {
    msg_.char_values = std::move(arg);
    return Init_Arrays_Request_float32_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_byte_values
{
public:
  explicit Init_Arrays_Request_byte_values(::test_msgs::srv::Arrays_Request & msg)
  : msg_(msg)
  {}
  Init_Arrays_Request_char_values byte_values(::test_msgs::srv::Arrays_Request::_byte_values_type arg)
  {
    msg_.byte_values = std::move(arg);
    return Init_Arrays_Request_char_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

class Init_Arrays_Request_bool_values
{
public:
  Init_Arrays_Request_bool_values()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Arrays_Request_byte_values bool_values(::test_msgs::srv::Arrays_Request::_bool_values_type arg)
  {
    msg_.bool_values = std::move(arg);
    return Init_Arrays_Request_byte_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::srv::Arrays_Request>()
{
  return test_msgs::srv::builder::Init_Arrays_Request_bool_values();
}

}  // namespace test_msgs


namespace test_msgs
{

namespace srv
{

namespace builder
{

class Init_Arrays_Response_string_values_default
{
public:
  explicit Init_Arrays_Response_string_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  ::test_msgs::srv::Arrays_Response string_values_default(::test_msgs::srv::Arrays_Response::_string_values_default_type arg)
  {
    msg_.string_values_default = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint64_values_default
{
public:
  explicit Init_Arrays_Response_uint64_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_string_values_default uint64_values_default(::test_msgs::srv::Arrays_Response::_uint64_values_default_type arg)
  {
    msg_.uint64_values_default = std::move(arg);
    return Init_Arrays_Response_string_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int64_values_default
{
public:
  explicit Init_Arrays_Response_int64_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint64_values_default int64_values_default(::test_msgs::srv::Arrays_Response::_int64_values_default_type arg)
  {
    msg_.int64_values_default = std::move(arg);
    return Init_Arrays_Response_uint64_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint32_values_default
{
public:
  explicit Init_Arrays_Response_uint32_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int64_values_default uint32_values_default(::test_msgs::srv::Arrays_Response::_uint32_values_default_type arg)
  {
    msg_.uint32_values_default = std::move(arg);
    return Init_Arrays_Response_int64_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int32_values_default
{
public:
  explicit Init_Arrays_Response_int32_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint32_values_default int32_values_default(::test_msgs::srv::Arrays_Response::_int32_values_default_type arg)
  {
    msg_.int32_values_default = std::move(arg);
    return Init_Arrays_Response_uint32_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint16_values_default
{
public:
  explicit Init_Arrays_Response_uint16_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int32_values_default uint16_values_default(::test_msgs::srv::Arrays_Response::_uint16_values_default_type arg)
  {
    msg_.uint16_values_default = std::move(arg);
    return Init_Arrays_Response_int32_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int16_values_default
{
public:
  explicit Init_Arrays_Response_int16_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint16_values_default int16_values_default(::test_msgs::srv::Arrays_Response::_int16_values_default_type arg)
  {
    msg_.int16_values_default = std::move(arg);
    return Init_Arrays_Response_uint16_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint8_values_default
{
public:
  explicit Init_Arrays_Response_uint8_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int16_values_default uint8_values_default(::test_msgs::srv::Arrays_Response::_uint8_values_default_type arg)
  {
    msg_.uint8_values_default = std::move(arg);
    return Init_Arrays_Response_int16_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int8_values_default
{
public:
  explicit Init_Arrays_Response_int8_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint8_values_default int8_values_default(::test_msgs::srv::Arrays_Response::_int8_values_default_type arg)
  {
    msg_.int8_values_default = std::move(arg);
    return Init_Arrays_Response_uint8_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_float64_values_default
{
public:
  explicit Init_Arrays_Response_float64_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int8_values_default float64_values_default(::test_msgs::srv::Arrays_Response::_float64_values_default_type arg)
  {
    msg_.float64_values_default = std::move(arg);
    return Init_Arrays_Response_int8_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_float32_values_default
{
public:
  explicit Init_Arrays_Response_float32_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_float64_values_default float32_values_default(::test_msgs::srv::Arrays_Response::_float32_values_default_type arg)
  {
    msg_.float32_values_default = std::move(arg);
    return Init_Arrays_Response_float64_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_char_values_default
{
public:
  explicit Init_Arrays_Response_char_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_float32_values_default char_values_default(::test_msgs::srv::Arrays_Response::_char_values_default_type arg)
  {
    msg_.char_values_default = std::move(arg);
    return Init_Arrays_Response_float32_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_byte_values_default
{
public:
  explicit Init_Arrays_Response_byte_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_char_values_default byte_values_default(::test_msgs::srv::Arrays_Response::_byte_values_default_type arg)
  {
    msg_.byte_values_default = std::move(arg);
    return Init_Arrays_Response_char_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_bool_values_default
{
public:
  explicit Init_Arrays_Response_bool_values_default(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_byte_values_default bool_values_default(::test_msgs::srv::Arrays_Response::_bool_values_default_type arg)
  {
    msg_.bool_values_default = std::move(arg);
    return Init_Arrays_Response_byte_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_defaults_values
{
public:
  explicit Init_Arrays_Response_defaults_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_bool_values_default defaults_values(::test_msgs::srv::Arrays_Response::_defaults_values_type arg)
  {
    msg_.defaults_values = std::move(arg);
    return Init_Arrays_Response_bool_values_default(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_constants_values
{
public:
  explicit Init_Arrays_Response_constants_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_defaults_values constants_values(::test_msgs::srv::Arrays_Response::_constants_values_type arg)
  {
    msg_.constants_values = std::move(arg);
    return Init_Arrays_Response_defaults_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_basic_types_values
{
public:
  explicit Init_Arrays_Response_basic_types_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_constants_values basic_types_values(::test_msgs::srv::Arrays_Response::_basic_types_values_type arg)
  {
    msg_.basic_types_values = std::move(arg);
    return Init_Arrays_Response_constants_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_string_values
{
public:
  explicit Init_Arrays_Response_string_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_basic_types_values string_values(::test_msgs::srv::Arrays_Response::_string_values_type arg)
  {
    msg_.string_values = std::move(arg);
    return Init_Arrays_Response_basic_types_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint64_values
{
public:
  explicit Init_Arrays_Response_uint64_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_string_values uint64_values(::test_msgs::srv::Arrays_Response::_uint64_values_type arg)
  {
    msg_.uint64_values = std::move(arg);
    return Init_Arrays_Response_string_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int64_values
{
public:
  explicit Init_Arrays_Response_int64_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint64_values int64_values(::test_msgs::srv::Arrays_Response::_int64_values_type arg)
  {
    msg_.int64_values = std::move(arg);
    return Init_Arrays_Response_uint64_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint32_values
{
public:
  explicit Init_Arrays_Response_uint32_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int64_values uint32_values(::test_msgs::srv::Arrays_Response::_uint32_values_type arg)
  {
    msg_.uint32_values = std::move(arg);
    return Init_Arrays_Response_int64_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int32_values
{
public:
  explicit Init_Arrays_Response_int32_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint32_values int32_values(::test_msgs::srv::Arrays_Response::_int32_values_type arg)
  {
    msg_.int32_values = std::move(arg);
    return Init_Arrays_Response_uint32_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint16_values
{
public:
  explicit Init_Arrays_Response_uint16_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int32_values uint16_values(::test_msgs::srv::Arrays_Response::_uint16_values_type arg)
  {
    msg_.uint16_values = std::move(arg);
    return Init_Arrays_Response_int32_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int16_values
{
public:
  explicit Init_Arrays_Response_int16_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint16_values int16_values(::test_msgs::srv::Arrays_Response::_int16_values_type arg)
  {
    msg_.int16_values = std::move(arg);
    return Init_Arrays_Response_uint16_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_uint8_values
{
public:
  explicit Init_Arrays_Response_uint8_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int16_values uint8_values(::test_msgs::srv::Arrays_Response::_uint8_values_type arg)
  {
    msg_.uint8_values = std::move(arg);
    return Init_Arrays_Response_int16_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_int8_values
{
public:
  explicit Init_Arrays_Response_int8_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_uint8_values int8_values(::test_msgs::srv::Arrays_Response::_int8_values_type arg)
  {
    msg_.int8_values = std::move(arg);
    return Init_Arrays_Response_uint8_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_float64_values
{
public:
  explicit Init_Arrays_Response_float64_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_int8_values float64_values(::test_msgs::srv::Arrays_Response::_float64_values_type arg)
  {
    msg_.float64_values = std::move(arg);
    return Init_Arrays_Response_int8_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_float32_values
{
public:
  explicit Init_Arrays_Response_float32_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_float64_values float32_values(::test_msgs::srv::Arrays_Response::_float32_values_type arg)
  {
    msg_.float32_values = std::move(arg);
    return Init_Arrays_Response_float64_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_char_values
{
public:
  explicit Init_Arrays_Response_char_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_float32_values char_values(::test_msgs::srv::Arrays_Response::_char_values_type arg)
  {
    msg_.char_values = std::move(arg);
    return Init_Arrays_Response_float32_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_byte_values
{
public:
  explicit Init_Arrays_Response_byte_values(::test_msgs::srv::Arrays_Response & msg)
  : msg_(msg)
  {}
  Init_Arrays_Response_char_values byte_values(::test_msgs::srv::Arrays_Response::_byte_values_type arg)
  {
    msg_.byte_values = std::move(arg);
    return Init_Arrays_Response_char_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

class Init_Arrays_Response_bool_values
{
public:
  Init_Arrays_Response_bool_values()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Arrays_Response_byte_values bool_values(::test_msgs::srv::Arrays_Response::_bool_values_type arg)
  {
    msg_.bool_values = std::move(arg);
    return Init_Arrays_Response_byte_values(msg_);
  }

private:
  ::test_msgs::srv::Arrays_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::srv::Arrays_Response>()
{
  return test_msgs::srv::builder::Init_Arrays_Response_bool_values();
}

}  // namespace test_msgs

#endif  // TEST_MSGS__SRV__DETAIL__ARRAYS__BUILDER_HPP_
