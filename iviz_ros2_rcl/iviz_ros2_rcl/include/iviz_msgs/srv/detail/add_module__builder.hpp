// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/AddModule.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__ADD_MODULE__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__ADD_MODULE__BUILDER_HPP_

#include "iviz_msgs/srv/detail/add_module__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_AddModule_Request_id
{
public:
  explicit Init_AddModule_Request_id(::iviz_msgs::srv::AddModule_Request & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::AddModule_Request id(::iviz_msgs::srv::AddModule_Request::_id_type arg)
  {
    msg_.id = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::AddModule_Request msg_;
};

class Init_AddModule_Request_module_type
{
public:
  Init_AddModule_Request_module_type()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_AddModule_Request_id module_type(::iviz_msgs::srv::AddModule_Request::_module_type_type arg)
  {
    msg_.module_type = std::move(arg);
    return Init_AddModule_Request_id(msg_);
  }

private:
  ::iviz_msgs::srv::AddModule_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::AddModule_Request>()
{
  return iviz_msgs::srv::builder::Init_AddModule_Request_module_type();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_AddModule_Response_id
{
public:
  explicit Init_AddModule_Response_id(::iviz_msgs::srv::AddModule_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::AddModule_Response id(::iviz_msgs::srv::AddModule_Response::_id_type arg)
  {
    msg_.id = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::AddModule_Response msg_;
};

class Init_AddModule_Response_message
{
public:
  explicit Init_AddModule_Response_message(::iviz_msgs::srv::AddModule_Response & msg)
  : msg_(msg)
  {}
  Init_AddModule_Response_id message(::iviz_msgs::srv::AddModule_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return Init_AddModule_Response_id(msg_);
  }

private:
  ::iviz_msgs::srv::AddModule_Response msg_;
};

class Init_AddModule_Response_success
{
public:
  Init_AddModule_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_AddModule_Response_message success(::iviz_msgs::srv::AddModule_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_AddModule_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::AddModule_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::AddModule_Response>()
{
  return iviz_msgs::srv::builder::Init_AddModule_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__ADD_MODULE__BUILDER_HPP_
