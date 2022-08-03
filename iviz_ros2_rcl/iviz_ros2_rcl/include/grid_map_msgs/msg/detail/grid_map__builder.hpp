// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from grid_map_msgs:msg/GridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__BUILDER_HPP_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__BUILDER_HPP_

#include "grid_map_msgs/msg/detail/grid_map__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace grid_map_msgs
{

namespace msg
{

namespace builder
{

class Init_GridMap_inner_start_index
{
public:
  explicit Init_GridMap_inner_start_index(::grid_map_msgs::msg::GridMap & msg)
  : msg_(msg)
  {}
  ::grid_map_msgs::msg::GridMap inner_start_index(::grid_map_msgs::msg::GridMap::_inner_start_index_type arg)
  {
    msg_.inner_start_index = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMap msg_;
};

class Init_GridMap_outer_start_index
{
public:
  explicit Init_GridMap_outer_start_index(::grid_map_msgs::msg::GridMap & msg)
  : msg_(msg)
  {}
  Init_GridMap_inner_start_index outer_start_index(::grid_map_msgs::msg::GridMap::_outer_start_index_type arg)
  {
    msg_.outer_start_index = std::move(arg);
    return Init_GridMap_inner_start_index(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMap msg_;
};

class Init_GridMap_data
{
public:
  explicit Init_GridMap_data(::grid_map_msgs::msg::GridMap & msg)
  : msg_(msg)
  {}
  Init_GridMap_outer_start_index data(::grid_map_msgs::msg::GridMap::_data_type arg)
  {
    msg_.data = std::move(arg);
    return Init_GridMap_outer_start_index(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMap msg_;
};

class Init_GridMap_basic_layers
{
public:
  explicit Init_GridMap_basic_layers(::grid_map_msgs::msg::GridMap & msg)
  : msg_(msg)
  {}
  Init_GridMap_data basic_layers(::grid_map_msgs::msg::GridMap::_basic_layers_type arg)
  {
    msg_.basic_layers = std::move(arg);
    return Init_GridMap_data(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMap msg_;
};

class Init_GridMap_layers
{
public:
  explicit Init_GridMap_layers(::grid_map_msgs::msg::GridMap & msg)
  : msg_(msg)
  {}
  Init_GridMap_basic_layers layers(::grid_map_msgs::msg::GridMap::_layers_type arg)
  {
    msg_.layers = std::move(arg);
    return Init_GridMap_basic_layers(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMap msg_;
};

class Init_GridMap_info
{
public:
  Init_GridMap_info()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_GridMap_layers info(::grid_map_msgs::msg::GridMap::_info_type arg)
  {
    msg_.info = std::move(arg);
    return Init_GridMap_layers(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMap msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::msg::GridMap>()
{
  return grid_map_msgs::msg::builder::Init_GridMap_info();
}

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__BUILDER_HPP_
