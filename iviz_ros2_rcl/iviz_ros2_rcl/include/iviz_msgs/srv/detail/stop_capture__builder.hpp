// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/StopCapture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__STOP_CAPTURE__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__STOP_CAPTURE__BUILDER_HPP_

#include "iviz_msgs/srv/detail/stop_capture__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{


}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::StopCapture_Request>()
{
  return ::iviz_msgs::srv::StopCapture_Request(rosidl_runtime_cpp::MessageInitialization::ZERO);
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_StopCapture_Response_message
{
public:
  explicit Init_StopCapture_Response_message(::iviz_msgs::srv::StopCapture_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::StopCapture_Response message(::iviz_msgs::srv::StopCapture_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::StopCapture_Response msg_;
};

class Init_StopCapture_Response_success
{
public:
  Init_StopCapture_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_StopCapture_Response_message success(::iviz_msgs::srv::StopCapture_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_StopCapture_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::StopCapture_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::StopCapture_Response>()
{
  return iviz_msgs::srv::builder::Init_StopCapture_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__STOP_CAPTURE__BUILDER_HPP_
