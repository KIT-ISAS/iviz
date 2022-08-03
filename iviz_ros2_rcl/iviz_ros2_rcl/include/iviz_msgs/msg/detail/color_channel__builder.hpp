// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/ColorChannel.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__BUILDER_HPP_

#include "iviz_msgs/msg/detail/color_channel__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_ColorChannel_colors
{
public:
  Init_ColorChannel_colors()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::msg::ColorChannel colors(::iviz_msgs::msg::ColorChannel::_colors_type arg)
  {
    msg_.colors = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::ColorChannel msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::ColorChannel>()
{
  return iviz_msgs::msg::builder::Init_ColorChannel_colors();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__BUILDER_HPP_
