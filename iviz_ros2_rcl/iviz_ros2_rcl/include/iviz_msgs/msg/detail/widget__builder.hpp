// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Widget.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__WIDGET__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__WIDGET__BUILDER_HPP_

#include "iviz_msgs/msg/detail/widget__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Widget_secondary_boundaries
{
public:
  explicit Init_Widget_secondary_boundaries(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Widget secondary_boundaries(::iviz_msgs::msg::Widget::_secondary_boundaries_type arg)
  {
    msg_.secondary_boundaries = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_boundary
{
public:
  explicit Init_Widget_boundary(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_secondary_boundaries boundary(::iviz_msgs::msg::Widget::_boundary_type arg)
  {
    msg_.boundary = std::move(arg);
    return Init_Widget_secondary_boundaries(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_caption
{
public:
  explicit Init_Widget_caption(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_boundary caption(::iviz_msgs::msg::Widget::_caption_type arg)
  {
    msg_.caption = std::move(arg);
    return Init_Widget_boundary(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_secondary_scale
{
public:
  explicit Init_Widget_secondary_scale(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_caption secondary_scale(::iviz_msgs::msg::Widget::_secondary_scale_type arg)
  {
    msg_.secondary_scale = std::move(arg);
    return Init_Widget_caption(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_scale
{
public:
  explicit Init_Widget_scale(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_secondary_scale scale(::iviz_msgs::msg::Widget::_scale_type arg)
  {
    msg_.scale = std::move(arg);
    return Init_Widget_secondary_scale(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_secondary_color
{
public:
  explicit Init_Widget_secondary_color(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_scale secondary_color(::iviz_msgs::msg::Widget::_secondary_color_type arg)
  {
    msg_.secondary_color = std::move(arg);
    return Init_Widget_scale(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_color
{
public:
  explicit Init_Widget_color(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_secondary_color color(::iviz_msgs::msg::Widget::_color_type arg)
  {
    msg_.color = std::move(arg);
    return Init_Widget_secondary_color(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_pose
{
public:
  explicit Init_Widget_pose(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_color pose(::iviz_msgs::msg::Widget::_pose_type arg)
  {
    msg_.pose = std::move(arg);
    return Init_Widget_color(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_type
{
public:
  explicit Init_Widget_type(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_pose type(::iviz_msgs::msg::Widget::_type_type arg)
  {
    msg_.type = std::move(arg);
    return Init_Widget_pose(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_id
{
public:
  explicit Init_Widget_id(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_type id(::iviz_msgs::msg::Widget::_id_type arg)
  {
    msg_.id = std::move(arg);
    return Init_Widget_type(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_action
{
public:
  explicit Init_Widget_action(::iviz_msgs::msg::Widget & msg)
  : msg_(msg)
  {}
  Init_Widget_id action(::iviz_msgs::msg::Widget::_action_type arg)
  {
    msg_.action = std::move(arg);
    return Init_Widget_id(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

class Init_Widget_header
{
public:
  Init_Widget_header()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Widget_action header(::iviz_msgs::msg::Widget::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_Widget_action(msg_);
  }

private:
  ::iviz_msgs::msg::Widget msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Widget>()
{
  return iviz_msgs::msg::builder::Init_Widget_header();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__WIDGET__BUILDER_HPP_
