// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from grid_map_msgs:srv/GetGridMapInfo.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__BUILDER_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__BUILDER_HPP_

#include "grid_map_msgs/srv/detail/get_grid_map_info__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace grid_map_msgs
{

namespace srv
{


}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::GetGridMapInfo_Request>()
{
  return ::grid_map_msgs::srv::GetGridMapInfo_Request(rosidl_runtime_cpp::MessageInitialization::ZERO);
}

}  // namespace grid_map_msgs


namespace grid_map_msgs
{

namespace srv
{

namespace builder
{

class Init_GetGridMapInfo_Response_info
{
public:
  Init_GetGridMapInfo_Response_info()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::grid_map_msgs::srv::GetGridMapInfo_Response info(::grid_map_msgs::srv::GetGridMapInfo_Response::_info_type arg)
  {
    msg_.info = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMapInfo_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::GetGridMapInfo_Response>()
{
  return grid_map_msgs::srv::builder::Init_GetGridMapInfo_Response_info();
}

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP_INFO__BUILDER_HPP_
