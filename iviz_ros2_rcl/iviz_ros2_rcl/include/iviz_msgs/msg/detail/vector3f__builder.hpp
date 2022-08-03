// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Vector3f.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__VECTOR3F__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__VECTOR3F__BUILDER_HPP_

#include "iviz_msgs/msg/detail/vector3f__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Vector3f_z
{
public:
  explicit Init_Vector3f_z(::iviz_msgs::msg::Vector3f & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Vector3f z(::iviz_msgs::msg::Vector3f::_z_type arg)
  {
    msg_.z = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Vector3f msg_;
};

class Init_Vector3f_y
{
public:
  explicit Init_Vector3f_y(::iviz_msgs::msg::Vector3f & msg)
  : msg_(msg)
  {}
  Init_Vector3f_z y(::iviz_msgs::msg::Vector3f::_y_type arg)
  {
    msg_.y = std::move(arg);
    return Init_Vector3f_z(msg_);
  }

private:
  ::iviz_msgs::msg::Vector3f msg_;
};

class Init_Vector3f_x
{
public:
  Init_Vector3f_x()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Vector3f_y x(::iviz_msgs::msg::Vector3f::_x_type arg)
  {
    msg_.x = std::move(arg);
    return Init_Vector3f_y(msg_);
  }

private:
  ::iviz_msgs::msg::Vector3f msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Vector3f>()
{
  return iviz_msgs::msg::builder::Init_Vector3f_x();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__VECTOR3F__BUILDER_HPP_
