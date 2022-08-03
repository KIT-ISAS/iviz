// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Light.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__LIGHT__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__LIGHT__BUILDER_HPP_

#include "iviz_msgs/msg/detail/light__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Light_outer_angle
{
public:
  explicit Init_Light_outer_angle(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Light outer_angle(::iviz_msgs::msg::Light::_outer_angle_type arg)
  {
    msg_.outer_angle = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_inner_angle
{
public:
  explicit Init_Light_inner_angle(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  Init_Light_outer_angle inner_angle(::iviz_msgs::msg::Light::_inner_angle_type arg)
  {
    msg_.inner_angle = std::move(arg);
    return Init_Light_outer_angle(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_direction
{
public:
  explicit Init_Light_direction(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  Init_Light_inner_angle direction(::iviz_msgs::msg::Light::_direction_type arg)
  {
    msg_.direction = std::move(arg);
    return Init_Light_inner_angle(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_position
{
public:
  explicit Init_Light_position(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  Init_Light_direction position(::iviz_msgs::msg::Light::_position_type arg)
  {
    msg_.position = std::move(arg);
    return Init_Light_direction(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_range
{
public:
  explicit Init_Light_range(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  Init_Light_position range(::iviz_msgs::msg::Light::_range_type arg)
  {
    msg_.range = std::move(arg);
    return Init_Light_position(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_diffuse
{
public:
  explicit Init_Light_diffuse(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  Init_Light_range diffuse(::iviz_msgs::msg::Light::_diffuse_type arg)
  {
    msg_.diffuse = std::move(arg);
    return Init_Light_range(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_cast_shadows
{
public:
  explicit Init_Light_cast_shadows(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  Init_Light_diffuse cast_shadows(::iviz_msgs::msg::Light::_cast_shadows_type arg)
  {
    msg_.cast_shadows = std::move(arg);
    return Init_Light_diffuse(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_type
{
public:
  explicit Init_Light_type(::iviz_msgs::msg::Light & msg)
  : msg_(msg)
  {}
  Init_Light_cast_shadows type(::iviz_msgs::msg::Light::_type_type arg)
  {
    msg_.type = std::move(arg);
    return Init_Light_cast_shadows(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

class Init_Light_name
{
public:
  Init_Light_name()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Light_type name(::iviz_msgs::msg::Light::_name_type arg)
  {
    msg_.name = std::move(arg);
    return Init_Light_type(msg_);
  }

private:
  ::iviz_msgs::msg::Light msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Light>()
{
  return iviz_msgs::msg::builder::Init_Light_name();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__LIGHT__BUILDER_HPP_
