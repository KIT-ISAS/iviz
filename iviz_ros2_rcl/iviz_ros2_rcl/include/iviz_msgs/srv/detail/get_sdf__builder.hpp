// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/GetSdf.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_SDF__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__GET_SDF__BUILDER_HPP_

#include "iviz_msgs/srv/detail/get_sdf__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_GetSdf_Request_uri
{
public:
  Init_GetSdf_Request_uri()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::srv::GetSdf_Request uri(::iviz_msgs::srv::GetSdf_Request::_uri_type arg)
  {
    msg_.uri = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::GetSdf_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::GetSdf_Request>()
{
  return iviz_msgs::srv::builder::Init_GetSdf_Request_uri();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_GetSdf_Response_message
{
public:
  explicit Init_GetSdf_Response_message(::iviz_msgs::srv::GetSdf_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::GetSdf_Response message(::iviz_msgs::srv::GetSdf_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::GetSdf_Response msg_;
};

class Init_GetSdf_Response_scene
{
public:
  explicit Init_GetSdf_Response_scene(::iviz_msgs::srv::GetSdf_Response & msg)
  : msg_(msg)
  {}
  Init_GetSdf_Response_message scene(::iviz_msgs::srv::GetSdf_Response::_scene_type arg)
  {
    msg_.scene = std::move(arg);
    return Init_GetSdf_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::GetSdf_Response msg_;
};

class Init_GetSdf_Response_success
{
public:
  Init_GetSdf_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_GetSdf_Response_scene success(::iviz_msgs::srv::GetSdf_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_GetSdf_Response_scene(msg_);
  }

private:
  ::iviz_msgs::srv::GetSdf_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::GetSdf_Response>()
{
  return iviz_msgs::srv::builder::Init_GetSdf_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_SDF__BUILDER_HPP_
