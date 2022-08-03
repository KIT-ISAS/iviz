// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/XRHandState.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__BUILDER_HPP_

#include "iviz_msgs/msg/detail/xr_hand_state__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_XRHandState_little
{
public:
  explicit Init_XRHandState_little(::iviz_msgs::msg::XRHandState & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::XRHandState little(::iviz_msgs::msg::XRHandState::_little_type arg)
  {
    msg_.little = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

class Init_XRHandState_ring
{
public:
  explicit Init_XRHandState_ring(::iviz_msgs::msg::XRHandState & msg)
  : msg_(msg)
  {}
  Init_XRHandState_little ring(::iviz_msgs::msg::XRHandState::_ring_type arg)
  {
    msg_.ring = std::move(arg);
    return Init_XRHandState_little(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

class Init_XRHandState_middle
{
public:
  explicit Init_XRHandState_middle(::iviz_msgs::msg::XRHandState & msg)
  : msg_(msg)
  {}
  Init_XRHandState_ring middle(::iviz_msgs::msg::XRHandState::_middle_type arg)
  {
    msg_.middle = std::move(arg);
    return Init_XRHandState_ring(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

class Init_XRHandState_index
{
public:
  explicit Init_XRHandState_index(::iviz_msgs::msg::XRHandState & msg)
  : msg_(msg)
  {}
  Init_XRHandState_middle index(::iviz_msgs::msg::XRHandState::_index_type arg)
  {
    msg_.index = std::move(arg);
    return Init_XRHandState_middle(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

class Init_XRHandState_thumb
{
public:
  explicit Init_XRHandState_thumb(::iviz_msgs::msg::XRHandState & msg)
  : msg_(msg)
  {}
  Init_XRHandState_index thumb(::iviz_msgs::msg::XRHandState::_thumb_type arg)
  {
    msg_.thumb = std::move(arg);
    return Init_XRHandState_index(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

class Init_XRHandState_palm
{
public:
  explicit Init_XRHandState_palm(::iviz_msgs::msg::XRHandState & msg)
  : msg_(msg)
  {}
  Init_XRHandState_thumb palm(::iviz_msgs::msg::XRHandState::_palm_type arg)
  {
    msg_.palm = std::move(arg);
    return Init_XRHandState_thumb(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

class Init_XRHandState_header
{
public:
  explicit Init_XRHandState_header(::iviz_msgs::msg::XRHandState & msg)
  : msg_(msg)
  {}
  Init_XRHandState_palm header(::iviz_msgs::msg::XRHandState::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_XRHandState_palm(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

class Init_XRHandState_is_valid
{
public:
  Init_XRHandState_is_valid()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_XRHandState_header is_valid(::iviz_msgs::msg::XRHandState::_is_valid_type arg)
  {
    msg_.is_valid = std::move(arg);
    return Init_XRHandState_header(msg_);
  }

private:
  ::iviz_msgs::msg::XRHandState msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::XRHandState>()
{
  return iviz_msgs::msg::builder::Init_XRHandState_is_valid();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__XR_HAND_STATE__BUILDER_HPP_
