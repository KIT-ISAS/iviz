// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from test_msgs:srv/BasicTypes.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__SRV__DETAIL__BASIC_TYPES__BUILDER_HPP_
#define TEST_MSGS__SRV__DETAIL__BASIC_TYPES__BUILDER_HPP_

#include "test_msgs/srv/detail/basic_types__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace test_msgs
{

namespace srv
{

namespace builder
{

class Init_BasicTypes_Request_string_value
{
public:
  explicit Init_BasicTypes_Request_string_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  ::test_msgs::srv::BasicTypes_Request string_value(::test_msgs::srv::BasicTypes_Request::_string_value_type arg)
  {
    msg_.string_value = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_uint64_value
{
public:
  explicit Init_BasicTypes_Request_uint64_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_string_value uint64_value(::test_msgs::srv::BasicTypes_Request::_uint64_value_type arg)
  {
    msg_.uint64_value = std::move(arg);
    return Init_BasicTypes_Request_string_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_int64_value
{
public:
  explicit Init_BasicTypes_Request_int64_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_uint64_value int64_value(::test_msgs::srv::BasicTypes_Request::_int64_value_type arg)
  {
    msg_.int64_value = std::move(arg);
    return Init_BasicTypes_Request_uint64_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_uint32_value
{
public:
  explicit Init_BasicTypes_Request_uint32_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_int64_value uint32_value(::test_msgs::srv::BasicTypes_Request::_uint32_value_type arg)
  {
    msg_.uint32_value = std::move(arg);
    return Init_BasicTypes_Request_int64_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_int32_value
{
public:
  explicit Init_BasicTypes_Request_int32_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_uint32_value int32_value(::test_msgs::srv::BasicTypes_Request::_int32_value_type arg)
  {
    msg_.int32_value = std::move(arg);
    return Init_BasicTypes_Request_uint32_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_uint16_value
{
public:
  explicit Init_BasicTypes_Request_uint16_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_int32_value uint16_value(::test_msgs::srv::BasicTypes_Request::_uint16_value_type arg)
  {
    msg_.uint16_value = std::move(arg);
    return Init_BasicTypes_Request_int32_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_int16_value
{
public:
  explicit Init_BasicTypes_Request_int16_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_uint16_value int16_value(::test_msgs::srv::BasicTypes_Request::_int16_value_type arg)
  {
    msg_.int16_value = std::move(arg);
    return Init_BasicTypes_Request_uint16_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_uint8_value
{
public:
  explicit Init_BasicTypes_Request_uint8_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_int16_value uint8_value(::test_msgs::srv::BasicTypes_Request::_uint8_value_type arg)
  {
    msg_.uint8_value = std::move(arg);
    return Init_BasicTypes_Request_int16_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_int8_value
{
public:
  explicit Init_BasicTypes_Request_int8_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_uint8_value int8_value(::test_msgs::srv::BasicTypes_Request::_int8_value_type arg)
  {
    msg_.int8_value = std::move(arg);
    return Init_BasicTypes_Request_uint8_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_float64_value
{
public:
  explicit Init_BasicTypes_Request_float64_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_int8_value float64_value(::test_msgs::srv::BasicTypes_Request::_float64_value_type arg)
  {
    msg_.float64_value = std::move(arg);
    return Init_BasicTypes_Request_int8_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_float32_value
{
public:
  explicit Init_BasicTypes_Request_float32_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_float64_value float32_value(::test_msgs::srv::BasicTypes_Request::_float32_value_type arg)
  {
    msg_.float32_value = std::move(arg);
    return Init_BasicTypes_Request_float64_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_char_value
{
public:
  explicit Init_BasicTypes_Request_char_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_float32_value char_value(::test_msgs::srv::BasicTypes_Request::_char_value_type arg)
  {
    msg_.char_value = std::move(arg);
    return Init_BasicTypes_Request_float32_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_byte_value
{
public:
  explicit Init_BasicTypes_Request_byte_value(::test_msgs::srv::BasicTypes_Request & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Request_char_value byte_value(::test_msgs::srv::BasicTypes_Request::_byte_value_type arg)
  {
    msg_.byte_value = std::move(arg);
    return Init_BasicTypes_Request_char_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

class Init_BasicTypes_Request_bool_value
{
public:
  Init_BasicTypes_Request_bool_value()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_BasicTypes_Request_byte_value bool_value(::test_msgs::srv::BasicTypes_Request::_bool_value_type arg)
  {
    msg_.bool_value = std::move(arg);
    return Init_BasicTypes_Request_byte_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::srv::BasicTypes_Request>()
{
  return test_msgs::srv::builder::Init_BasicTypes_Request_bool_value();
}

}  // namespace test_msgs


namespace test_msgs
{

namespace srv
{

namespace builder
{

class Init_BasicTypes_Response_string_value
{
public:
  explicit Init_BasicTypes_Response_string_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  ::test_msgs::srv::BasicTypes_Response string_value(::test_msgs::srv::BasicTypes_Response::_string_value_type arg)
  {
    msg_.string_value = std::move(arg);
    return std::move(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_uint64_value
{
public:
  explicit Init_BasicTypes_Response_uint64_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_string_value uint64_value(::test_msgs::srv::BasicTypes_Response::_uint64_value_type arg)
  {
    msg_.uint64_value = std::move(arg);
    return Init_BasicTypes_Response_string_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_int64_value
{
public:
  explicit Init_BasicTypes_Response_int64_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_uint64_value int64_value(::test_msgs::srv::BasicTypes_Response::_int64_value_type arg)
  {
    msg_.int64_value = std::move(arg);
    return Init_BasicTypes_Response_uint64_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_uint32_value
{
public:
  explicit Init_BasicTypes_Response_uint32_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_int64_value uint32_value(::test_msgs::srv::BasicTypes_Response::_uint32_value_type arg)
  {
    msg_.uint32_value = std::move(arg);
    return Init_BasicTypes_Response_int64_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_int32_value
{
public:
  explicit Init_BasicTypes_Response_int32_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_uint32_value int32_value(::test_msgs::srv::BasicTypes_Response::_int32_value_type arg)
  {
    msg_.int32_value = std::move(arg);
    return Init_BasicTypes_Response_uint32_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_uint16_value
{
public:
  explicit Init_BasicTypes_Response_uint16_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_int32_value uint16_value(::test_msgs::srv::BasicTypes_Response::_uint16_value_type arg)
  {
    msg_.uint16_value = std::move(arg);
    return Init_BasicTypes_Response_int32_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_int16_value
{
public:
  explicit Init_BasicTypes_Response_int16_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_uint16_value int16_value(::test_msgs::srv::BasicTypes_Response::_int16_value_type arg)
  {
    msg_.int16_value = std::move(arg);
    return Init_BasicTypes_Response_uint16_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_uint8_value
{
public:
  explicit Init_BasicTypes_Response_uint8_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_int16_value uint8_value(::test_msgs::srv::BasicTypes_Response::_uint8_value_type arg)
  {
    msg_.uint8_value = std::move(arg);
    return Init_BasicTypes_Response_int16_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_int8_value
{
public:
  explicit Init_BasicTypes_Response_int8_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_uint8_value int8_value(::test_msgs::srv::BasicTypes_Response::_int8_value_type arg)
  {
    msg_.int8_value = std::move(arg);
    return Init_BasicTypes_Response_uint8_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_float64_value
{
public:
  explicit Init_BasicTypes_Response_float64_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_int8_value float64_value(::test_msgs::srv::BasicTypes_Response::_float64_value_type arg)
  {
    msg_.float64_value = std::move(arg);
    return Init_BasicTypes_Response_int8_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_float32_value
{
public:
  explicit Init_BasicTypes_Response_float32_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_float64_value float32_value(::test_msgs::srv::BasicTypes_Response::_float32_value_type arg)
  {
    msg_.float32_value = std::move(arg);
    return Init_BasicTypes_Response_float64_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_char_value
{
public:
  explicit Init_BasicTypes_Response_char_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_float32_value char_value(::test_msgs::srv::BasicTypes_Response::_char_value_type arg)
  {
    msg_.char_value = std::move(arg);
    return Init_BasicTypes_Response_float32_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_byte_value
{
public:
  explicit Init_BasicTypes_Response_byte_value(::test_msgs::srv::BasicTypes_Response & msg)
  : msg_(msg)
  {}
  Init_BasicTypes_Response_char_value byte_value(::test_msgs::srv::BasicTypes_Response::_byte_value_type arg)
  {
    msg_.byte_value = std::move(arg);
    return Init_BasicTypes_Response_char_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

class Init_BasicTypes_Response_bool_value
{
public:
  Init_BasicTypes_Response_bool_value()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_BasicTypes_Response_byte_value bool_value(::test_msgs::srv::BasicTypes_Response::_bool_value_type arg)
  {
    msg_.bool_value = std::move(arg);
    return Init_BasicTypes_Response_byte_value(msg_);
  }

private:
  ::test_msgs::srv::BasicTypes_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::test_msgs::srv::BasicTypes_Response>()
{
  return test_msgs::srv::builder::Init_BasicTypes_Response_bool_value();
}

}  // namespace test_msgs

#endif  // TEST_MSGS__SRV__DETAIL__BASIC_TYPES__BUILDER_HPP_
