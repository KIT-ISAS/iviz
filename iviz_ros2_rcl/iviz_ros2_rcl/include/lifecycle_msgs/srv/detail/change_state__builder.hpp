// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from lifecycle_msgs:srv/ChangeState.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__BUILDER_HPP_
#define LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__BUILDER_HPP_

#include "lifecycle_msgs/srv/detail/change_state__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace lifecycle_msgs
{

namespace srv
{

namespace builder
{

class Init_ChangeState_Request_transition
{
public:
  Init_ChangeState_Request_transition()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::lifecycle_msgs::srv::ChangeState_Request transition(::lifecycle_msgs::srv::ChangeState_Request::_transition_type arg)
  {
    msg_.transition = std::move(arg);
    return std::move(msg_);
  }

private:
  ::lifecycle_msgs::srv::ChangeState_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::lifecycle_msgs::srv::ChangeState_Request>()
{
  return lifecycle_msgs::srv::builder::Init_ChangeState_Request_transition();
}

}  // namespace lifecycle_msgs


namespace lifecycle_msgs
{

namespace srv
{

namespace builder
{

class Init_ChangeState_Response_success
{
public:
  Init_ChangeState_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::lifecycle_msgs::srv::ChangeState_Response success(::lifecycle_msgs::srv::ChangeState_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return std::move(msg_);
  }

private:
  ::lifecycle_msgs::srv::ChangeState_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::lifecycle_msgs::srv::ChangeState_Response>()
{
  return lifecycle_msgs::srv::builder::Init_ChangeState_Response_success();
}

}  // namespace lifecycle_msgs

#endif  // LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__BUILDER_HPP_
