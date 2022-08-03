// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Triangle.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TRIANGLE__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__TRIANGLE__BUILDER_HPP_

#include "iviz_msgs/msg/detail/triangle__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Triangle_c
{
public:
  explicit Init_Triangle_c(::iviz_msgs::msg::Triangle & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Triangle c(::iviz_msgs::msg::Triangle::_c_type arg)
  {
    msg_.c = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Triangle msg_;
};

class Init_Triangle_b
{
public:
  explicit Init_Triangle_b(::iviz_msgs::msg::Triangle & msg)
  : msg_(msg)
  {}
  Init_Triangle_c b(::iviz_msgs::msg::Triangle::_b_type arg)
  {
    msg_.b = std::move(arg);
    return Init_Triangle_c(msg_);
  }

private:
  ::iviz_msgs::msg::Triangle msg_;
};

class Init_Triangle_a
{
public:
  Init_Triangle_a()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Triangle_b a(::iviz_msgs::msg::Triangle::_a_type arg)
  {
    msg_.a = std::move(arg);
    return Init_Triangle_b(msg_);
  }

private:
  ::iviz_msgs::msg::Triangle msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Triangle>()
{
  return iviz_msgs::msg::builder::Init_Triangle_a();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__TRIANGLE__BUILDER_HPP_
