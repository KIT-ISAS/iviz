// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Model.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MODEL__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MODEL__BUILDER_HPP_

#include "iviz_msgs/msg/detail/model__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Model_nodes
{
public:
  explicit Init_Model_nodes(::iviz_msgs::msg::Model & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Model nodes(::iviz_msgs::msg::Model::_nodes_type arg)
  {
    msg_.nodes = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Model msg_;
};

class Init_Model_materials
{
public:
  explicit Init_Model_materials(::iviz_msgs::msg::Model & msg)
  : msg_(msg)
  {}
  Init_Model_nodes materials(::iviz_msgs::msg::Model::_materials_type arg)
  {
    msg_.materials = std::move(arg);
    return Init_Model_nodes(msg_);
  }

private:
  ::iviz_msgs::msg::Model msg_;
};

class Init_Model_meshes
{
public:
  explicit Init_Model_meshes(::iviz_msgs::msg::Model & msg)
  : msg_(msg)
  {}
  Init_Model_materials meshes(::iviz_msgs::msg::Model::_meshes_type arg)
  {
    msg_.meshes = std::move(arg);
    return Init_Model_materials(msg_);
  }

private:
  ::iviz_msgs::msg::Model msg_;
};

class Init_Model_orientation_hint
{
public:
  explicit Init_Model_orientation_hint(::iviz_msgs::msg::Model & msg)
  : msg_(msg)
  {}
  Init_Model_meshes orientation_hint(::iviz_msgs::msg::Model::_orientation_hint_type arg)
  {
    msg_.orientation_hint = std::move(arg);
    return Init_Model_meshes(msg_);
  }

private:
  ::iviz_msgs::msg::Model msg_;
};

class Init_Model_filename
{
public:
  explicit Init_Model_filename(::iviz_msgs::msg::Model & msg)
  : msg_(msg)
  {}
  Init_Model_orientation_hint filename(::iviz_msgs::msg::Model::_filename_type arg)
  {
    msg_.filename = std::move(arg);
    return Init_Model_orientation_hint(msg_);
  }

private:
  ::iviz_msgs::msg::Model msg_;
};

class Init_Model_name
{
public:
  Init_Model_name()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Model_filename name(::iviz_msgs::msg::Model::_name_type arg)
  {
    msg_.name = std::move(arg);
    return Init_Model_filename(msg_);
  }

private:
  ::iviz_msgs::msg::Model msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Model>()
{
  return iviz_msgs::msg::builder::Init_Model_name();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MODEL__BUILDER_HPP_
