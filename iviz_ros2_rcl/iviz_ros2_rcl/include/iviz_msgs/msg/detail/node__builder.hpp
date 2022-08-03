// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Node.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__NODE__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__NODE__BUILDER_HPP_

#include "iviz_msgs/msg/detail/node__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Node_meshes
{
public:
  explicit Init_Node_meshes(::iviz_msgs::msg::Node & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Node meshes(::iviz_msgs::msg::Node::_meshes_type arg)
  {
    msg_.meshes = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Node msg_;
};

class Init_Node_transform
{
public:
  explicit Init_Node_transform(::iviz_msgs::msg::Node & msg)
  : msg_(msg)
  {}
  Init_Node_meshes transform(::iviz_msgs::msg::Node::_transform_type arg)
  {
    msg_.transform = std::move(arg);
    return Init_Node_meshes(msg_);
  }

private:
  ::iviz_msgs::msg::Node msg_;
};

class Init_Node_parent
{
public:
  explicit Init_Node_parent(::iviz_msgs::msg::Node & msg)
  : msg_(msg)
  {}
  Init_Node_transform parent(::iviz_msgs::msg::Node::_parent_type arg)
  {
    msg_.parent = std::move(arg);
    return Init_Node_transform(msg_);
  }

private:
  ::iviz_msgs::msg::Node msg_;
};

class Init_Node_name
{
public:
  Init_Node_name()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Node_parent name(::iviz_msgs::msg::Node::_name_type arg)
  {
    msg_.name = std::move(arg);
    return Init_Node_parent(msg_);
  }

private:
  ::iviz_msgs::msg::Node msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Node>()
{
  return iviz_msgs::msg::builder::Init_Node_name();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__NODE__BUILDER_HPP_
