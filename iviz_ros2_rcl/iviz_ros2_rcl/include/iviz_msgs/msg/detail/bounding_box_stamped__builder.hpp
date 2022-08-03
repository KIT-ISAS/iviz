// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/BoundingBoxStamped.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__BUILDER_HPP_

#include "iviz_msgs/msg/detail/bounding_box_stamped__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_BoundingBoxStamped_boundary
{
public:
  explicit Init_BoundingBoxStamped_boundary(::iviz_msgs::msg::BoundingBoxStamped & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::BoundingBoxStamped boundary(::iviz_msgs::msg::BoundingBoxStamped::_boundary_type arg)
  {
    msg_.boundary = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::BoundingBoxStamped msg_;
};

class Init_BoundingBoxStamped_header
{
public:
  Init_BoundingBoxStamped_header()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_BoundingBoxStamped_boundary header(::iviz_msgs::msg::BoundingBoxStamped::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_BoundingBoxStamped_boundary(msg_);
  }

private:
  ::iviz_msgs::msg::BoundingBoxStamped msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::BoundingBoxStamped>()
{
  return iviz_msgs::msg::builder::Init_BoundingBoxStamped_header();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__BUILDER_HPP_
