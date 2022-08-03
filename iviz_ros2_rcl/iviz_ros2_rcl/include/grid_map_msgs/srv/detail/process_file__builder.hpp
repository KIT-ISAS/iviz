// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from grid_map_msgs:srv/ProcessFile.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__BUILDER_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__BUILDER_HPP_

#include "grid_map_msgs/srv/detail/process_file__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace grid_map_msgs
{

namespace srv
{

namespace builder
{

class Init_ProcessFile_Request_topic_name
{
public:
  explicit Init_ProcessFile_Request_topic_name(::grid_map_msgs::srv::ProcessFile_Request & msg)
  : msg_(msg)
  {}
  ::grid_map_msgs::srv::ProcessFile_Request topic_name(::grid_map_msgs::srv::ProcessFile_Request::_topic_name_type arg)
  {
    msg_.topic_name = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::srv::ProcessFile_Request msg_;
};

class Init_ProcessFile_Request_file_path
{
public:
  Init_ProcessFile_Request_file_path()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_ProcessFile_Request_topic_name file_path(::grid_map_msgs::srv::ProcessFile_Request::_file_path_type arg)
  {
    msg_.file_path = std::move(arg);
    return Init_ProcessFile_Request_topic_name(msg_);
  }

private:
  ::grid_map_msgs::srv::ProcessFile_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::ProcessFile_Request>()
{
  return grid_map_msgs::srv::builder::Init_ProcessFile_Request_file_path();
}

}  // namespace grid_map_msgs


namespace grid_map_msgs
{

namespace srv
{

namespace builder
{

class Init_ProcessFile_Response_success
{
public:
  Init_ProcessFile_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::grid_map_msgs::srv::ProcessFile_Response success(::grid_map_msgs::srv::ProcessFile_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::srv::ProcessFile_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::ProcessFile_Response>()
{
  return grid_map_msgs::srv::builder::Init_ProcessFile_Response_success();
}

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__BUILDER_HPP_
