// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from composition_interfaces:srv/UnloadNode.idl
// generated code does not contain a copyright notice

#ifndef COMPOSITION_INTERFACES__SRV__DETAIL__UNLOAD_NODE__BUILDER_HPP_
#define COMPOSITION_INTERFACES__SRV__DETAIL__UNLOAD_NODE__BUILDER_HPP_

#include "composition_interfaces/srv/detail/unload_node__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace composition_interfaces
{

namespace srv
{

namespace builder
{

class Init_UnloadNode_Request_unique_id
{
public:
  Init_UnloadNode_Request_unique_id()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::composition_interfaces::srv::UnloadNode_Request unique_id(::composition_interfaces::srv::UnloadNode_Request::_unique_id_type arg)
  {
    msg_.unique_id = std::move(arg);
    return std::move(msg_);
  }

private:
  ::composition_interfaces::srv::UnloadNode_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::composition_interfaces::srv::UnloadNode_Request>()
{
  return composition_interfaces::srv::builder::Init_UnloadNode_Request_unique_id();
}

}  // namespace composition_interfaces


namespace composition_interfaces
{

namespace srv
{

namespace builder
{

class Init_UnloadNode_Response_error_message
{
public:
  explicit Init_UnloadNode_Response_error_message(::composition_interfaces::srv::UnloadNode_Response & msg)
  : msg_(msg)
  {}
  ::composition_interfaces::srv::UnloadNode_Response error_message(::composition_interfaces::srv::UnloadNode_Response::_error_message_type arg)
  {
    msg_.error_message = std::move(arg);
    return std::move(msg_);
  }

private:
  ::composition_interfaces::srv::UnloadNode_Response msg_;
};

class Init_UnloadNode_Response_success
{
public:
  Init_UnloadNode_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_UnloadNode_Response_error_message success(::composition_interfaces::srv::UnloadNode_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_UnloadNode_Response_error_message(msg_);
  }

private:
  ::composition_interfaces::srv::UnloadNode_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::composition_interfaces::srv::UnloadNode_Response>()
{
  return composition_interfaces::srv::builder::Init_UnloadNode_Response_success();
}

}  // namespace composition_interfaces

#endif  // COMPOSITION_INTERFACES__SRV__DETAIL__UNLOAD_NODE__BUILDER_HPP_
