// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Matrix4.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MATRIX4__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MATRIX4__BUILDER_HPP_

#include "iviz_msgs/msg/detail/matrix4__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Matrix4_m
{
public:
  Init_Matrix4_m()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::msg::Matrix4 m(::iviz_msgs::msg::Matrix4::_m_type arg)
  {
    msg_.m = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Matrix4 msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Matrix4>()
{
  return iviz_msgs::msg::builder::Init_Matrix4_m();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MATRIX4__BUILDER_HPP_
