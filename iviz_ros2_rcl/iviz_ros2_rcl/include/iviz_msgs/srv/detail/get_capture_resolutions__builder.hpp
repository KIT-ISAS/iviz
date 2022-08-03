// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/GetCaptureResolutions.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__BUILDER_HPP_

#include "iviz_msgs/srv/detail/get_capture_resolutions__struct.hpp"
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
auto build<::iviz_msgs::srv::GetCaptureResolutions_Request>()
{
  return ::iviz_msgs::srv::GetCaptureResolutions_Request(rosidl_runtime_cpp::MessageInitialization::ZERO);
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_GetCaptureResolutions_Response_resolutions
{
public:
  explicit Init_GetCaptureResolutions_Response_resolutions(::iviz_msgs::srv::GetCaptureResolutions_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::GetCaptureResolutions_Response resolutions(::iviz_msgs::srv::GetCaptureResolutions_Response::_resolutions_type arg)
  {
    msg_.resolutions = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::GetCaptureResolutions_Response msg_;
};

class Init_GetCaptureResolutions_Response_message
{
public:
  explicit Init_GetCaptureResolutions_Response_message(::iviz_msgs::srv::GetCaptureResolutions_Response & msg)
  : msg_(msg)
  {}
  Init_GetCaptureResolutions_Response_resolutions message(::iviz_msgs::srv::GetCaptureResolutions_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return Init_GetCaptureResolutions_Response_resolutions(msg_);
  }

private:
  ::iviz_msgs::srv::GetCaptureResolutions_Response msg_;
};

class Init_GetCaptureResolutions_Response_success
{
public:
  Init_GetCaptureResolutions_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_GetCaptureResolutions_Response_message success(::iviz_msgs::srv::GetCaptureResolutions_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_GetCaptureResolutions_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::GetCaptureResolutions_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::GetCaptureResolutions_Response>()
{
  return iviz_msgs::srv::builder::Init_GetCaptureResolutions_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__BUILDER_HPP_
