// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/AddModuleFromTopic.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__BUILDER_HPP_

#include "iviz_msgs/srv/detail/add_module_from_topic__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_AddModuleFromTopic_Request_id
{
public:
  explicit Init_AddModuleFromTopic_Request_id(::iviz_msgs::srv::AddModuleFromTopic_Request & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::AddModuleFromTopic_Request id(::iviz_msgs::srv::AddModuleFromTopic_Request::_id_type arg)
  {
    msg_.id = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::AddModuleFromTopic_Request msg_;
};

class Init_AddModuleFromTopic_Request_topic
{
public:
  Init_AddModuleFromTopic_Request_topic()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_AddModuleFromTopic_Request_id topic(::iviz_msgs::srv::AddModuleFromTopic_Request::_topic_type arg)
  {
    msg_.topic = std::move(arg);
    return Init_AddModuleFromTopic_Request_id(msg_);
  }

private:
  ::iviz_msgs::srv::AddModuleFromTopic_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::AddModuleFromTopic_Request>()
{
  return iviz_msgs::srv::builder::Init_AddModuleFromTopic_Request_topic();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_AddModuleFromTopic_Response_id
{
public:
  explicit Init_AddModuleFromTopic_Response_id(::iviz_msgs::srv::AddModuleFromTopic_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::AddModuleFromTopic_Response id(::iviz_msgs::srv::AddModuleFromTopic_Response::_id_type arg)
  {
    msg_.id = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::AddModuleFromTopic_Response msg_;
};

class Init_AddModuleFromTopic_Response_message
{
public:
  explicit Init_AddModuleFromTopic_Response_message(::iviz_msgs::srv::AddModuleFromTopic_Response & msg)
  : msg_(msg)
  {}
  Init_AddModuleFromTopic_Response_id message(::iviz_msgs::srv::AddModuleFromTopic_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return Init_AddModuleFromTopic_Response_id(msg_);
  }

private:
  ::iviz_msgs::srv::AddModuleFromTopic_Response msg_;
};

class Init_AddModuleFromTopic_Response_success
{
public:
  Init_AddModuleFromTopic_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_AddModuleFromTopic_Response_message success(::iviz_msgs::srv::AddModuleFromTopic_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_AddModuleFromTopic_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::AddModuleFromTopic_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::AddModuleFromTopic_Response>()
{
  return iviz_msgs::srv::builder::Init_AddModuleFromTopic_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__BUILDER_HPP_
