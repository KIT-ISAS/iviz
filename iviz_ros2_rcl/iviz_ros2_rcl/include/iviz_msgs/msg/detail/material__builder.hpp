// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Material.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MATERIAL__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MATERIAL__BUILDER_HPP_

#include "iviz_msgs/msg/detail/material__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Material_textures
{
public:
  explicit Init_Material_textures(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Material textures(::iviz_msgs::msg::Material::_textures_type arg)
  {
    msg_.textures = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_blend_mode
{
public:
  explicit Init_Material_blend_mode(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_textures blend_mode(::iviz_msgs::msg::Material::_blend_mode_type arg)
  {
    msg_.blend_mode = std::move(arg);
    return Init_Material_textures(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_reflectivity
{
public:
  explicit Init_Material_reflectivity(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_blend_mode reflectivity(::iviz_msgs::msg::Material::_reflectivity_type arg)
  {
    msg_.reflectivity = std::move(arg);
    return Init_Material_blend_mode(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_shininess_strength
{
public:
  explicit Init_Material_shininess_strength(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_reflectivity shininess_strength(::iviz_msgs::msg::Material::_shininess_strength_type arg)
  {
    msg_.shininess_strength = std::move(arg);
    return Init_Material_reflectivity(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_shininess
{
public:
  explicit Init_Material_shininess(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_shininess_strength shininess(::iviz_msgs::msg::Material::_shininess_type arg)
  {
    msg_.shininess = std::move(arg);
    return Init_Material_shininess_strength(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_bump_scaling
{
public:
  explicit Init_Material_bump_scaling(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_shininess bump_scaling(::iviz_msgs::msg::Material::_bump_scaling_type arg)
  {
    msg_.bump_scaling = std::move(arg);
    return Init_Material_shininess(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_opacity
{
public:
  explicit Init_Material_opacity(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_bump_scaling opacity(::iviz_msgs::msg::Material::_opacity_type arg)
  {
    msg_.opacity = std::move(arg);
    return Init_Material_bump_scaling(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_emissive
{
public:
  explicit Init_Material_emissive(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_opacity emissive(::iviz_msgs::msg::Material::_emissive_type arg)
  {
    msg_.emissive = std::move(arg);
    return Init_Material_opacity(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_diffuse
{
public:
  explicit Init_Material_diffuse(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_emissive diffuse(::iviz_msgs::msg::Material::_diffuse_type arg)
  {
    msg_.diffuse = std::move(arg);
    return Init_Material_emissive(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_ambient
{
public:
  explicit Init_Material_ambient(::iviz_msgs::msg::Material & msg)
  : msg_(msg)
  {}
  Init_Material_diffuse ambient(::iviz_msgs::msg::Material::_ambient_type arg)
  {
    msg_.ambient = std::move(arg);
    return Init_Material_diffuse(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

class Init_Material_name
{
public:
  Init_Material_name()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Material_ambient name(::iviz_msgs::msg::Material::_name_type arg)
  {
    msg_.name = std::move(arg);
    return Init_Material_ambient(msg_);
  }

private:
  ::iviz_msgs::msg::Material msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Material>()
{
  return iviz_msgs::msg::builder::Init_Material_name();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MATERIAL__BUILDER_HPP_
