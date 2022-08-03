// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Feedback.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__FEEDBACK__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__FEEDBACK__BUILDER_HPP_

#include "iviz_msgs/msg/detail/feedback__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Feedback_trajectory
{
public:
  explicit Init_Feedback_trajectory(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Feedback trajectory(::iviz_msgs::msg::Feedback::_trajectory_type arg)
  {
    msg_.trajectory = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_scale
{
public:
  explicit Init_Feedback_scale(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_trajectory scale(::iviz_msgs::msg::Feedback::_scale_type arg)
  {
    msg_.scale = std::move(arg);
    return Init_Feedback_trajectory(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_orientation
{
public:
  explicit Init_Feedback_orientation(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_scale orientation(::iviz_msgs::msg::Feedback::_orientation_type arg)
  {
    msg_.orientation = std::move(arg);
    return Init_Feedback_scale(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_position
{
public:
  explicit Init_Feedback_position(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_orientation position(::iviz_msgs::msg::Feedback::_position_type arg)
  {
    msg_.position = std::move(arg);
    return Init_Feedback_orientation(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_angle
{
public:
  explicit Init_Feedback_angle(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_position angle(::iviz_msgs::msg::Feedback::_angle_type arg)
  {
    msg_.angle = std::move(arg);
    return Init_Feedback_position(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_entry_id
{
public:
  explicit Init_Feedback_entry_id(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_angle entry_id(::iviz_msgs::msg::Feedback::_entry_id_type arg)
  {
    msg_.entry_id = std::move(arg);
    return Init_Feedback_angle(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_type
{
public:
  explicit Init_Feedback_type(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_entry_id type(::iviz_msgs::msg::Feedback::_type_type arg)
  {
    msg_.type = std::move(arg);
    return Init_Feedback_entry_id(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_id
{
public:
  explicit Init_Feedback_id(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_type id(::iviz_msgs::msg::Feedback::_id_type arg)
  {
    msg_.id = std::move(arg);
    return Init_Feedback_type(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_viz_id
{
public:
  explicit Init_Feedback_viz_id(::iviz_msgs::msg::Feedback & msg)
  : msg_(msg)
  {}
  Init_Feedback_id viz_id(::iviz_msgs::msg::Feedback::_viz_id_type arg)
  {
    msg_.viz_id = std::move(arg);
    return Init_Feedback_id(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

class Init_Feedback_header
{
public:
  Init_Feedback_header()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Feedback_viz_id header(::iviz_msgs::msg::Feedback::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_Feedback_viz_id(msg_);
  }

private:
  ::iviz_msgs::msg::Feedback msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Feedback>()
{
  return iviz_msgs::msg::builder::Init_Feedback_header();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__FEEDBACK__BUILDER_HPP_
