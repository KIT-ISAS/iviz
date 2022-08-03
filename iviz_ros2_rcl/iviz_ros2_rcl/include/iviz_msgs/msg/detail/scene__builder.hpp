// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Scene.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__SCENE__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__SCENE__BUILDER_HPP_

#include "iviz_msgs/msg/detail/scene__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Scene_lights
{
public:
  explicit Init_Scene_lights(::iviz_msgs::msg::Scene & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Scene lights(::iviz_msgs::msg::Scene::_lights_type arg)
  {
    msg_.lights = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Scene msg_;
};

class Init_Scene_includes
{
public:
  explicit Init_Scene_includes(::iviz_msgs::msg::Scene & msg)
  : msg_(msg)
  {}
  Init_Scene_lights includes(::iviz_msgs::msg::Scene::_includes_type arg)
  {
    msg_.includes = std::move(arg);
    return Init_Scene_lights(msg_);
  }

private:
  ::iviz_msgs::msg::Scene msg_;
};

class Init_Scene_filename
{
public:
  explicit Init_Scene_filename(::iviz_msgs::msg::Scene & msg)
  : msg_(msg)
  {}
  Init_Scene_includes filename(::iviz_msgs::msg::Scene::_filename_type arg)
  {
    msg_.filename = std::move(arg);
    return Init_Scene_includes(msg_);
  }

private:
  ::iviz_msgs::msg::Scene msg_;
};

class Init_Scene_name
{
public:
  Init_Scene_name()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Scene_filename name(::iviz_msgs::msg::Scene::_name_type arg)
  {
    msg_.name = std::move(arg);
    return Init_Scene_filename(msg_);
  }

private:
  ::iviz_msgs::msg::Scene msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Scene>()
{
  return iviz_msgs::msg::builder::Init_Scene_name();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__SCENE__BUILDER_HPP_
