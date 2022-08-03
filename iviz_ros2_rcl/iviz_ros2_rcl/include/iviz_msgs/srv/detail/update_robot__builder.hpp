// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/UpdateRobot.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__BUILDER_HPP_

#include "iviz_msgs/srv/detail/update_robot__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_UpdateRobot_Request_valid_fields
{
public:
  explicit Init_UpdateRobot_Request_valid_fields(::iviz_msgs::srv::UpdateRobot_Request & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::UpdateRobot_Request valid_fields(::iviz_msgs::srv::UpdateRobot_Request::_valid_fields_type arg)
  {
    msg_.valid_fields = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateRobot_Request msg_;
};

class Init_UpdateRobot_Request_configuration
{
public:
  explicit Init_UpdateRobot_Request_configuration(::iviz_msgs::srv::UpdateRobot_Request & msg)
  : msg_(msg)
  {}
  Init_UpdateRobot_Request_valid_fields configuration(::iviz_msgs::srv::UpdateRobot_Request::_configuration_type arg)
  {
    msg_.configuration = std::move(arg);
    return Init_UpdateRobot_Request_valid_fields(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateRobot_Request msg_;
};

class Init_UpdateRobot_Request_id
{
public:
  explicit Init_UpdateRobot_Request_id(::iviz_msgs::srv::UpdateRobot_Request & msg)
  : msg_(msg)
  {}
  Init_UpdateRobot_Request_configuration id(::iviz_msgs::srv::UpdateRobot_Request::_id_type arg)
  {
    msg_.id = std::move(arg);
    return Init_UpdateRobot_Request_configuration(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateRobot_Request msg_;
};

class Init_UpdateRobot_Request_operation
{
public:
  Init_UpdateRobot_Request_operation()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_UpdateRobot_Request_id operation(::iviz_msgs::srv::UpdateRobot_Request::_operation_type arg)
  {
    msg_.operation = std::move(arg);
    return Init_UpdateRobot_Request_id(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateRobot_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::UpdateRobot_Request>()
{
  return iviz_msgs::srv::builder::Init_UpdateRobot_Request_operation();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_UpdateRobot_Response_message
{
public:
  explicit Init_UpdateRobot_Response_message(::iviz_msgs::srv::UpdateRobot_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::UpdateRobot_Response message(::iviz_msgs::srv::UpdateRobot_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateRobot_Response msg_;
};

class Init_UpdateRobot_Response_success
{
public:
  Init_UpdateRobot_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_UpdateRobot_Response_message success(::iviz_msgs::srv::UpdateRobot_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_UpdateRobot_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::UpdateRobot_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::UpdateRobot_Response>()
{
  return iviz_msgs::srv::builder::Init_UpdateRobot_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__BUILDER_HPP_
