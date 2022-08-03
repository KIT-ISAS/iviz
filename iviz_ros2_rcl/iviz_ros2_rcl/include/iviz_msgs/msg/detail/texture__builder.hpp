// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Texture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TEXTURE__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__TEXTURE__BUILDER_HPP_

#include "iviz_msgs/msg/detail/texture__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Texture_wrap_mode_v
{
public:
  explicit Init_Texture_wrap_mode_v(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Texture wrap_mode_v(::iviz_msgs::msg::Texture::_wrap_mode_v_type arg)
  {
    msg_.wrap_mode_v = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_wrap_mode_u
{
public:
  explicit Init_Texture_wrap_mode_u(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  Init_Texture_wrap_mode_v wrap_mode_u(::iviz_msgs::msg::Texture::_wrap_mode_u_type arg)
  {
    msg_.wrap_mode_u = std::move(arg);
    return Init_Texture_wrap_mode_v(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_operation
{
public:
  explicit Init_Texture_operation(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  Init_Texture_wrap_mode_u operation(::iviz_msgs::msg::Texture::_operation_type arg)
  {
    msg_.operation = std::move(arg);
    return Init_Texture_wrap_mode_u(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_blend_factor
{
public:
  explicit Init_Texture_blend_factor(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  Init_Texture_operation blend_factor(::iviz_msgs::msg::Texture::_blend_factor_type arg)
  {
    msg_.blend_factor = std::move(arg);
    return Init_Texture_operation(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_uv_index
{
public:
  explicit Init_Texture_uv_index(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  Init_Texture_blend_factor uv_index(::iviz_msgs::msg::Texture::_uv_index_type arg)
  {
    msg_.uv_index = std::move(arg);
    return Init_Texture_blend_factor(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_mapping
{
public:
  explicit Init_Texture_mapping(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  Init_Texture_uv_index mapping(::iviz_msgs::msg::Texture::_mapping_type arg)
  {
    msg_.mapping = std::move(arg);
    return Init_Texture_uv_index(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_type
{
public:
  explicit Init_Texture_type(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  Init_Texture_mapping type(::iviz_msgs::msg::Texture::_type_type arg)
  {
    msg_.type = std::move(arg);
    return Init_Texture_mapping(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_index
{
public:
  explicit Init_Texture_index(::iviz_msgs::msg::Texture & msg)
  : msg_(msg)
  {}
  Init_Texture_type index(::iviz_msgs::msg::Texture::_index_type arg)
  {
    msg_.index = std::move(arg);
    return Init_Texture_type(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

class Init_Texture_path
{
public:
  Init_Texture_path()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Texture_index path(::iviz_msgs::msg::Texture::_path_type arg)
  {
    msg_.path = std::move(arg);
    return Init_Texture_index(msg_);
  }

private:
  ::iviz_msgs::msg::Texture msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Texture>()
{
  return iviz_msgs::msg::builder::Init_Texture_path();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__TEXTURE__BUILDER_HPP_
