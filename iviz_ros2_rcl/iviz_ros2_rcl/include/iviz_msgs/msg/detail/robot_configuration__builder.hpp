// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/RobotConfiguration.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__BUILDER_HPP_

#include "iviz_msgs/msg/detail/robot_configuration__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_RobotConfiguration_visible
{
public:
  explicit Init_RobotConfiguration_visible(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::RobotConfiguration visible(::iviz_msgs::msg::RobotConfiguration::_visible_type arg)
  {
    msg_.visible = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_id
{
public:
  explicit Init_RobotConfiguration_id(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_visible id(::iviz_msgs::msg::RobotConfiguration::_id_type arg)
  {
    msg_.id = std::move(arg);
    return Init_RobotConfiguration_visible(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_smoothness
{
public:
  explicit Init_RobotConfiguration_smoothness(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_id smoothness(::iviz_msgs::msg::RobotConfiguration::_smoothness_type arg)
  {
    msg_.smoothness = std::move(arg);
    return Init_RobotConfiguration_id(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_metallic
{
public:
  explicit Init_RobotConfiguration_metallic(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_smoothness metallic(::iviz_msgs::msg::RobotConfiguration::_metallic_type arg)
  {
    msg_.metallic = std::move(arg);
    return Init_RobotConfiguration_smoothness(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_tint
{
public:
  explicit Init_RobotConfiguration_tint(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_metallic tint(::iviz_msgs::msg::RobotConfiguration::_tint_type arg)
  {
    msg_.tint = std::move(arg);
    return Init_RobotConfiguration_metallic(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_render_as_occlusion_only
{
public:
  explicit Init_RobotConfiguration_render_as_occlusion_only(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_tint render_as_occlusion_only(::iviz_msgs::msg::RobotConfiguration::_render_as_occlusion_only_type arg)
  {
    msg_.render_as_occlusion_only = std::move(arg);
    return Init_RobotConfiguration_tint(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_attached_to_tf
{
public:
  explicit Init_RobotConfiguration_attached_to_tf(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_render_as_occlusion_only attached_to_tf(::iviz_msgs::msg::RobotConfiguration::_attached_to_tf_type arg)
  {
    msg_.attached_to_tf = std::move(arg);
    return Init_RobotConfiguration_render_as_occlusion_only(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_frame_suffix
{
public:
  explicit Init_RobotConfiguration_frame_suffix(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_attached_to_tf frame_suffix(::iviz_msgs::msg::RobotConfiguration::_frame_suffix_type arg)
  {
    msg_.frame_suffix = std::move(arg);
    return Init_RobotConfiguration_attached_to_tf(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_frame_prefix
{
public:
  explicit Init_RobotConfiguration_frame_prefix(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_frame_suffix frame_prefix(::iviz_msgs::msg::RobotConfiguration::_frame_prefix_type arg)
  {
    msg_.frame_prefix = std::move(arg);
    return Init_RobotConfiguration_frame_suffix(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_saved_robot_name
{
public:
  explicit Init_RobotConfiguration_saved_robot_name(::iviz_msgs::msg::RobotConfiguration & msg)
  : msg_(msg)
  {}
  Init_RobotConfiguration_frame_prefix saved_robot_name(::iviz_msgs::msg::RobotConfiguration::_saved_robot_name_type arg)
  {
    msg_.saved_robot_name = std::move(arg);
    return Init_RobotConfiguration_frame_prefix(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

class Init_RobotConfiguration_source_parameter
{
public:
  Init_RobotConfiguration_source_parameter()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_RobotConfiguration_saved_robot_name source_parameter(::iviz_msgs::msg::RobotConfiguration::_source_parameter_type arg)
  {
    msg_.source_parameter = std::move(arg);
    return Init_RobotConfiguration_saved_robot_name(msg_);
  }

private:
  ::iviz_msgs::msg::RobotConfiguration msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::RobotConfiguration>()
{
  return iviz_msgs::msg::builder::Init_RobotConfiguration_source_parameter();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__BUILDER_HPP_
