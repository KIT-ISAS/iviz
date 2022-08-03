// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Color32.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__COLOR32__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__COLOR32__BUILDER_HPP_

#include "iviz_msgs/msg/detail/color32__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Color32_a
{
public:
  explicit Init_Color32_a(::iviz_msgs::msg::Color32 & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Color32 a(::iviz_msgs::msg::Color32::_a_type arg)
  {
    msg_.a = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Color32 msg_;
};

class Init_Color32_b
{
public:
  explicit Init_Color32_b(::iviz_msgs::msg::Color32 & msg)
  : msg_(msg)
  {}
  Init_Color32_a b(::iviz_msgs::msg::Color32::_b_type arg)
  {
    msg_.b = std::move(arg);
    return Init_Color32_a(msg_);
  }

private:
  ::iviz_msgs::msg::Color32 msg_;
};

class Init_Color32_g
{
public:
  explicit Init_Color32_g(::iviz_msgs::msg::Color32 & msg)
  : msg_(msg)
  {}
  Init_Color32_b g(::iviz_msgs::msg::Color32::_g_type arg)
  {
    msg_.g = std::move(arg);
    return Init_Color32_b(msg_);
  }

private:
  ::iviz_msgs::msg::Color32 msg_;
};

class Init_Color32_r
{
public:
  Init_Color32_r()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Color32_g r(::iviz_msgs::msg::Color32::_r_type arg)
  {
    msg_.r = std::move(arg);
    return Init_Color32_g(msg_);
  }

private:
  ::iviz_msgs::msg::Color32 msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Color32>()
{
  return iviz_msgs::msg::builder::Init_Color32_r();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__COLOR32__BUILDER_HPP_
