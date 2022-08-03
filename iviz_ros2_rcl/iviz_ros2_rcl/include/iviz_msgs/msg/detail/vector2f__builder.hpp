// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Vector2f.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__VECTOR2F__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__VECTOR2F__BUILDER_HPP_

#include "iviz_msgs/msg/detail/vector2f__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Vector2f_y
{
public:
  explicit Init_Vector2f_y(::iviz_msgs::msg::Vector2f & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Vector2f y(::iviz_msgs::msg::Vector2f::_y_type arg)
  {
    msg_.y = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Vector2f msg_;
};

class Init_Vector2f_x
{
public:
  Init_Vector2f_x()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Vector2f_y x(::iviz_msgs::msg::Vector2f::_x_type arg)
  {
    msg_.x = std::move(arg);
    return Init_Vector2f_y(msg_);
  }

private:
  ::iviz_msgs::msg::Vector2f msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Vector2f>()
{
  return iviz_msgs::msg::builder::Init_Vector2f_x();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__VECTOR2F__BUILDER_HPP_
