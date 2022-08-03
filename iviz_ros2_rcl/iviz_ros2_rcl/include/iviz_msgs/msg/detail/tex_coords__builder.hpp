// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/TexCoords.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TEX_COORDS__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__TEX_COORDS__BUILDER_HPP_

#include "iviz_msgs/msg/detail/tex_coords__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_TexCoords_coords
{
public:
  Init_TexCoords_coords()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::msg::TexCoords coords(::iviz_msgs::msg::TexCoords::_coords_type arg)
  {
    msg_.coords = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::TexCoords msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::TexCoords>()
{
  return iviz_msgs::msg::builder::Init_TexCoords_coords();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__TEX_COORDS__BUILDER_HPP_
