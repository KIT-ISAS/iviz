// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Include.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__INCLUDE__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__INCLUDE__BUILDER_HPP_

#include "iviz_msgs/msg/detail/include__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Include_package
{
public:
  explicit Init_Include_package(::iviz_msgs::msg::Include & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Include package(::iviz_msgs::msg::Include::_package_type arg)
  {
    msg_.package = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Include msg_;
};

class Init_Include_material
{
public:
  explicit Init_Include_material(::iviz_msgs::msg::Include & msg)
  : msg_(msg)
  {}
  Init_Include_package material(::iviz_msgs::msg::Include::_material_type arg)
  {
    msg_.material = std::move(arg);
    return Init_Include_package(msg_);
  }

private:
  ::iviz_msgs::msg::Include msg_;
};

class Init_Include_pose
{
public:
  explicit Init_Include_pose(::iviz_msgs::msg::Include & msg)
  : msg_(msg)
  {}
  Init_Include_material pose(::iviz_msgs::msg::Include::_pose_type arg)
  {
    msg_.pose = std::move(arg);
    return Init_Include_material(msg_);
  }

private:
  ::iviz_msgs::msg::Include msg_;
};

class Init_Include_uri
{
public:
  Init_Include_uri()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Include_pose uri(::iviz_msgs::msg::Include::_uri_type arg)
  {
    msg_.uri = std::move(arg);
    return Init_Include_pose(msg_);
  }

private:
  ::iviz_msgs::msg::Include msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Include>()
{
  return iviz_msgs::msg::builder::Init_Include_uri();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__INCLUDE__BUILDER_HPP_
