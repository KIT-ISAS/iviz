// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/BoundingBox.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__BUILDER_HPP_

#include "iviz_msgs/msg/detail/bounding_box__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_BoundingBox_size
{
public:
  explicit Init_BoundingBox_size(::iviz_msgs::msg::BoundingBox & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::BoundingBox size(::iviz_msgs::msg::BoundingBox::_size_type arg)
  {
    msg_.size = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::BoundingBox msg_;
};

class Init_BoundingBox_center
{
public:
  Init_BoundingBox_center()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_BoundingBox_size center(::iviz_msgs::msg::BoundingBox::_center_type arg)
  {
    msg_.center = std::move(arg);
    return Init_BoundingBox_size(msg_);
  }

private:
  ::iviz_msgs::msg::BoundingBox msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::BoundingBox>()
{
  return iviz_msgs::msg::builder::Init_BoundingBox_center();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX__BUILDER_HPP_
