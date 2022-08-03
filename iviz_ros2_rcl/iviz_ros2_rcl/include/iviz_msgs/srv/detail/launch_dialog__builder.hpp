// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/LaunchDialog.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__BUILDER_HPP_

#include "iviz_msgs/srv/detail/launch_dialog__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_LaunchDialog_Request_dialog
{
public:
  Init_LaunchDialog_Request_dialog()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::srv::LaunchDialog_Request dialog(::iviz_msgs::srv::LaunchDialog_Request::_dialog_type arg)
  {
    msg_.dialog = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::LaunchDialog_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::LaunchDialog_Request>()
{
  return iviz_msgs::srv::builder::Init_LaunchDialog_Request_dialog();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_LaunchDialog_Response_feedback
{
public:
  explicit Init_LaunchDialog_Response_feedback(::iviz_msgs::srv::LaunchDialog_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::LaunchDialog_Response feedback(::iviz_msgs::srv::LaunchDialog_Response::_feedback_type arg)
  {
    msg_.feedback = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::LaunchDialog_Response msg_;
};

class Init_LaunchDialog_Response_message
{
public:
  explicit Init_LaunchDialog_Response_message(::iviz_msgs::srv::LaunchDialog_Response & msg)
  : msg_(msg)
  {}
  Init_LaunchDialog_Response_feedback message(::iviz_msgs::srv::LaunchDialog_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return Init_LaunchDialog_Response_feedback(msg_);
  }

private:
  ::iviz_msgs::srv::LaunchDialog_Response msg_;
};

class Init_LaunchDialog_Response_success
{
public:
  Init_LaunchDialog_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_LaunchDialog_Response_message success(::iviz_msgs::srv::LaunchDialog_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_LaunchDialog_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::LaunchDialog_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::LaunchDialog_Response>()
{
  return iviz_msgs::srv::builder::Init_LaunchDialog_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__BUILDER_HPP_
