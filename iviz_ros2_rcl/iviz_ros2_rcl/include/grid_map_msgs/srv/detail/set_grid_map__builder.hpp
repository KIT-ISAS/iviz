// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from grid_map_msgs:srv/SetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__BUILDER_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__BUILDER_HPP_

#include "grid_map_msgs/srv/detail/set_grid_map__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace grid_map_msgs
{

namespace srv
{

namespace builder
{

class Init_SetGridMap_Request_map
{
public:
  Init_SetGridMap_Request_map()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::grid_map_msgs::srv::SetGridMap_Request map(::grid_map_msgs::srv::SetGridMap_Request::_map_type arg)
  {
    msg_.map = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::srv::SetGridMap_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::SetGridMap_Request>()
{
  return grid_map_msgs::srv::builder::Init_SetGridMap_Request_map();
}

}  // namespace grid_map_msgs


namespace grid_map_msgs
{

namespace srv
{


}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::SetGridMap_Response>()
{
  return ::grid_map_msgs::srv::SetGridMap_Response(rosidl_runtime_cpp::MessageInitialization::ZERO);
}

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__BUILDER_HPP_
