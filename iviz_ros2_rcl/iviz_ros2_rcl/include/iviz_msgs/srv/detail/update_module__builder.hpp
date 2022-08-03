// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/UpdateModule.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__BUILDER_HPP_

#include "iviz_msgs/srv/detail/update_module__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_UpdateModule_Request_config
{
public:
  explicit Init_UpdateModule_Request_config(::iviz_msgs::srv::UpdateModule_Request & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::UpdateModule_Request config(::iviz_msgs::srv::UpdateModule_Request::_config_type arg)
  {
    msg_.config = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateModule_Request msg_;
};

class Init_UpdateModule_Request_fields
{
public:
  explicit Init_UpdateModule_Request_fields(::iviz_msgs::srv::UpdateModule_Request & msg)
  : msg_(msg)
  {}
  Init_UpdateModule_Request_config fields(::iviz_msgs::srv::UpdateModule_Request::_fields_type arg)
  {
    msg_.fields = std::move(arg);
    return Init_UpdateModule_Request_config(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateModule_Request msg_;
};

class Init_UpdateModule_Request_id
{
public:
  Init_UpdateModule_Request_id()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_UpdateModule_Request_fields id(::iviz_msgs::srv::UpdateModule_Request::_id_type arg)
  {
    msg_.id = std::move(arg);
    return Init_UpdateModule_Request_fields(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateModule_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::UpdateModule_Request>()
{
  return iviz_msgs::srv::builder::Init_UpdateModule_Request_id();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_UpdateModule_Response_message
{
public:
  explicit Init_UpdateModule_Response_message(::iviz_msgs::srv::UpdateModule_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::UpdateModule_Response message(::iviz_msgs::srv::UpdateModule_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateModule_Response msg_;
};

class Init_UpdateModule_Response_success
{
public:
  Init_UpdateModule_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_UpdateModule_Response_message success(::iviz_msgs::srv::UpdateModule_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_UpdateModule_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateModule_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::UpdateModule_Response>()
{
  return iviz_msgs::srv::builder::Init_UpdateModule_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__UPDATE_MODULE__BUILDER_HPP_
