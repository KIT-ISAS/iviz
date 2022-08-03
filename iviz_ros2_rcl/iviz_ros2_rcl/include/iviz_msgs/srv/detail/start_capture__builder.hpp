// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/StartCapture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__BUILDER_HPP_

#include "iviz_msgs/srv/detail/start_capture__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_StartCapture_Request_with_holograms
{
public:
  explicit Init_StartCapture_Request_with_holograms(::iviz_msgs::srv::StartCapture_Request & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::StartCapture_Request with_holograms(::iviz_msgs::srv::StartCapture_Request::_with_holograms_type arg)
  {
    msg_.with_holograms = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::StartCapture_Request msg_;
};

class Init_StartCapture_Request_resolution_y
{
public:
  explicit Init_StartCapture_Request_resolution_y(::iviz_msgs::srv::StartCapture_Request & msg)
  : msg_(msg)
  {}
  Init_StartCapture_Request_with_holograms resolution_y(::iviz_msgs::srv::StartCapture_Request::_resolution_y_type arg)
  {
    msg_.resolution_y = std::move(arg);
    return Init_StartCapture_Request_with_holograms(msg_);
  }

private:
  ::iviz_msgs::srv::StartCapture_Request msg_;
};

class Init_StartCapture_Request_resolution_x
{
public:
  Init_StartCapture_Request_resolution_x()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_StartCapture_Request_resolution_y resolution_x(::iviz_msgs::srv::StartCapture_Request::_resolution_x_type arg)
  {
    msg_.resolution_x = std::move(arg);
    return Init_StartCapture_Request_resolution_y(msg_);
  }

private:
  ::iviz_msgs::srv::StartCapture_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::StartCapture_Request>()
{
  return iviz_msgs::srv::builder::Init_StartCapture_Request_resolution_x();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_StartCapture_Response_message
{
public:
  explicit Init_StartCapture_Response_message(::iviz_msgs::srv::StartCapture_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::StartCapture_Response message(::iviz_msgs::srv::StartCapture_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::StartCapture_Response msg_;
};

class Init_StartCapture_Response_success
{
public:
  Init_StartCapture_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_StartCapture_Response_message success(::iviz_msgs::srv::StartCapture_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_StartCapture_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::StartCapture_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::StartCapture_Response>()
{
  return iviz_msgs::srv::builder::Init_StartCapture_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__BUILDER_HPP_
