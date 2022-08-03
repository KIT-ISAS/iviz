// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from grid_map_msgs:srv/GetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__BUILDER_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__BUILDER_HPP_

#include "grid_map_msgs/srv/detail/get_grid_map__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace grid_map_msgs
{

namespace srv
{

namespace builder
{

class Init_GetGridMap_Request_layers
{
public:
  explicit Init_GetGridMap_Request_layers(::grid_map_msgs::srv::GetGridMap_Request & msg)
  : msg_(msg)
  {}
  ::grid_map_msgs::srv::GetGridMap_Request layers(::grid_map_msgs::srv::GetGridMap_Request::_layers_type arg)
  {
    msg_.layers = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMap_Request msg_;
};

class Init_GetGridMap_Request_length_y
{
public:
  explicit Init_GetGridMap_Request_length_y(::grid_map_msgs::srv::GetGridMap_Request & msg)
  : msg_(msg)
  {}
  Init_GetGridMap_Request_layers length_y(::grid_map_msgs::srv::GetGridMap_Request::_length_y_type arg)
  {
    msg_.length_y = std::move(arg);
    return Init_GetGridMap_Request_layers(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMap_Request msg_;
};

class Init_GetGridMap_Request_length_x
{
public:
  explicit Init_GetGridMap_Request_length_x(::grid_map_msgs::srv::GetGridMap_Request & msg)
  : msg_(msg)
  {}
  Init_GetGridMap_Request_length_y length_x(::grid_map_msgs::srv::GetGridMap_Request::_length_x_type arg)
  {
    msg_.length_x = std::move(arg);
    return Init_GetGridMap_Request_length_y(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMap_Request msg_;
};

class Init_GetGridMap_Request_position_y
{
public:
  explicit Init_GetGridMap_Request_position_y(::grid_map_msgs::srv::GetGridMap_Request & msg)
  : msg_(msg)
  {}
  Init_GetGridMap_Request_length_x position_y(::grid_map_msgs::srv::GetGridMap_Request::_position_y_type arg)
  {
    msg_.position_y = std::move(arg);
    return Init_GetGridMap_Request_length_x(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMap_Request msg_;
};

class Init_GetGridMap_Request_position_x
{
public:
  explicit Init_GetGridMap_Request_position_x(::grid_map_msgs::srv::GetGridMap_Request & msg)
  : msg_(msg)
  {}
  Init_GetGridMap_Request_position_y position_x(::grid_map_msgs::srv::GetGridMap_Request::_position_x_type arg)
  {
    msg_.position_x = std::move(arg);
    return Init_GetGridMap_Request_position_y(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMap_Request msg_;
};

class Init_GetGridMap_Request_frame_id
{
public:
  Init_GetGridMap_Request_frame_id()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_GetGridMap_Request_position_x frame_id(::grid_map_msgs::srv::GetGridMap_Request::_frame_id_type arg)
  {
    msg_.frame_id = std::move(arg);
    return Init_GetGridMap_Request_position_x(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMap_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::GetGridMap_Request>()
{
  return grid_map_msgs::srv::builder::Init_GetGridMap_Request_frame_id();
}

}  // namespace grid_map_msgs


namespace grid_map_msgs
{

namespace srv
{

namespace builder
{

class Init_GetGridMap_Response_map
{
public:
  Init_GetGridMap_Response_map()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::grid_map_msgs::srv::GetGridMap_Response map(::grid_map_msgs::srv::GetGridMap_Response::_map_type arg)
  {
    msg_.map = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::srv::GetGridMap_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::srv::GetGridMap_Response>()
{
  return grid_map_msgs::srv::builder::Init_GetGridMap_Response_map();
}

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__BUILDER_HPP_
