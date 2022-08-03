// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from composition_interfaces:srv/LoadNode.idl
// generated code does not contain a copyright notice

#ifndef COMPOSITION_INTERFACES__SRV__DETAIL__LOAD_NODE__BUILDER_HPP_
#define COMPOSITION_INTERFACES__SRV__DETAIL__LOAD_NODE__BUILDER_HPP_

#include "composition_interfaces/srv/detail/load_node__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace composition_interfaces
{

namespace srv
{

namespace builder
{

class Init_LoadNode_Request_extra_arguments
{
public:
  explicit Init_LoadNode_Request_extra_arguments(::composition_interfaces::srv::LoadNode_Request & msg)
  : msg_(msg)
  {}
  ::composition_interfaces::srv::LoadNode_Request extra_arguments(::composition_interfaces::srv::LoadNode_Request::_extra_arguments_type arg)
  {
    msg_.extra_arguments = std::move(arg);
    return std::move(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

class Init_LoadNode_Request_parameters
{
public:
  explicit Init_LoadNode_Request_parameters(::composition_interfaces::srv::LoadNode_Request & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Request_extra_arguments parameters(::composition_interfaces::srv::LoadNode_Request::_parameters_type arg)
  {
    msg_.parameters = std::move(arg);
    return Init_LoadNode_Request_extra_arguments(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

class Init_LoadNode_Request_remap_rules
{
public:
  explicit Init_LoadNode_Request_remap_rules(::composition_interfaces::srv::LoadNode_Request & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Request_parameters remap_rules(::composition_interfaces::srv::LoadNode_Request::_remap_rules_type arg)
  {
    msg_.remap_rules = std::move(arg);
    return Init_LoadNode_Request_parameters(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

class Init_LoadNode_Request_log_level
{
public:
  explicit Init_LoadNode_Request_log_level(::composition_interfaces::srv::LoadNode_Request & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Request_remap_rules log_level(::composition_interfaces::srv::LoadNode_Request::_log_level_type arg)
  {
    msg_.log_level = std::move(arg);
    return Init_LoadNode_Request_remap_rules(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

class Init_LoadNode_Request_node_namespace
{
public:
  explicit Init_LoadNode_Request_node_namespace(::composition_interfaces::srv::LoadNode_Request & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Request_log_level node_namespace(::composition_interfaces::srv::LoadNode_Request::_node_namespace_type arg)
  {
    msg_.node_namespace = std::move(arg);
    return Init_LoadNode_Request_log_level(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

class Init_LoadNode_Request_node_name
{
public:
  explicit Init_LoadNode_Request_node_name(::composition_interfaces::srv::LoadNode_Request & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Request_node_namespace node_name(::composition_interfaces::srv::LoadNode_Request::_node_name_type arg)
  {
    msg_.node_name = std::move(arg);
    return Init_LoadNode_Request_node_namespace(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

class Init_LoadNode_Request_plugin_name
{
public:
  explicit Init_LoadNode_Request_plugin_name(::composition_interfaces::srv::LoadNode_Request & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Request_node_name plugin_name(::composition_interfaces::srv::LoadNode_Request::_plugin_name_type arg)
  {
    msg_.plugin_name = std::move(arg);
    return Init_LoadNode_Request_node_name(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

class Init_LoadNode_Request_package_name
{
public:
  Init_LoadNode_Request_package_name()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_LoadNode_Request_plugin_name package_name(::composition_interfaces::srv::LoadNode_Request::_package_name_type arg)
  {
    msg_.package_name = std::move(arg);
    return Init_LoadNode_Request_plugin_name(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::composition_interfaces::srv::LoadNode_Request>()
{
  return composition_interfaces::srv::builder::Init_LoadNode_Request_package_name();
}

}  // namespace composition_interfaces


namespace composition_interfaces
{

namespace srv
{

namespace builder
{

class Init_LoadNode_Response_unique_id
{
public:
  explicit Init_LoadNode_Response_unique_id(::composition_interfaces::srv::LoadNode_Response & msg)
  : msg_(msg)
  {}
  ::composition_interfaces::srv::LoadNode_Response unique_id(::composition_interfaces::srv::LoadNode_Response::_unique_id_type arg)
  {
    msg_.unique_id = std::move(arg);
    return std::move(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Response msg_;
};

class Init_LoadNode_Response_full_node_name
{
public:
  explicit Init_LoadNode_Response_full_node_name(::composition_interfaces::srv::LoadNode_Response & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Response_unique_id full_node_name(::composition_interfaces::srv::LoadNode_Response::_full_node_name_type arg)
  {
    msg_.full_node_name = std::move(arg);
    return Init_LoadNode_Response_unique_id(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Response msg_;
};

class Init_LoadNode_Response_error_message
{
public:
  explicit Init_LoadNode_Response_error_message(::composition_interfaces::srv::LoadNode_Response & msg)
  : msg_(msg)
  {}
  Init_LoadNode_Response_full_node_name error_message(::composition_interfaces::srv::LoadNode_Response::_error_message_type arg)
  {
    msg_.error_message = std::move(arg);
    return Init_LoadNode_Response_full_node_name(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Response msg_;
};

class Init_LoadNode_Response_success
{
public:
  Init_LoadNode_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_LoadNode_Response_error_message success(::composition_interfaces::srv::LoadNode_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_LoadNode_Response_error_message(msg_);
  }

private:
  ::composition_interfaces::srv::LoadNode_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::composition_interfaces::srv::LoadNode_Response>()
{
  return composition_interfaces::srv::builder::Init_LoadNode_Response_success();
}

}  // namespace composition_interfaces

#endif  // COMPOSITION_INTERFACES__SRV__DETAIL__LOAD_NODE__BUILDER_HPP_
