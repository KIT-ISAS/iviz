// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/XRGazeState.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__XR_GAZE_STATE__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__XR_GAZE_STATE__BUILDER_HPP_

#include "iviz_msgs/msg/detail/xr_gaze_state__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_XRGazeState_transform
{
public:
  explicit Init_XRGazeState_transform(::iviz_msgs::msg::XRGazeState & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::XRGazeState transform(::iviz_msgs::msg::XRGazeState::_transform_type arg)
  {
    msg_.transform = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::XRGazeState msg_;
};

class Init_XRGazeState_header
{
public:
  explicit Init_XRGazeState_header(::iviz_msgs::msg::XRGazeState & msg)
  : msg_(msg)
  {}
  Init_XRGazeState_transform header(::iviz_msgs::msg::XRGazeState::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_XRGazeState_transform(msg_);
  }

private:
  ::iviz_msgs::msg::XRGazeState msg_;
};

class Init_XRGazeState_is_valid
{
public:
  Init_XRGazeState_is_valid()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_XRGazeState_header is_valid(::iviz_msgs::msg::XRGazeState::_is_valid_type arg)
  {
    msg_.is_valid = std::move(arg);
    return Init_XRGazeState_header(msg_);
  }

private:
  ::iviz_msgs::msg::XRGazeState msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::XRGazeState>()
{
  return iviz_msgs::msg::builder::Init_XRGazeState_is_valid();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__XR_GAZE_STATE__BUILDER_HPP_
