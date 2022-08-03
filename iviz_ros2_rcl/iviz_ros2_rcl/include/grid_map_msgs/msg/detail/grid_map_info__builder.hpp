// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from grid_map_msgs:msg/GridMapInfo.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__BUILDER_HPP_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__BUILDER_HPP_

#include "grid_map_msgs/msg/detail/grid_map_info__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace grid_map_msgs
{

namespace msg
{

namespace builder
{

class Init_GridMapInfo_pose
{
public:
  explicit Init_GridMapInfo_pose(::grid_map_msgs::msg::GridMapInfo & msg)
  : msg_(msg)
  {}
  ::grid_map_msgs::msg::GridMapInfo pose(::grid_map_msgs::msg::GridMapInfo::_pose_type arg)
  {
    msg_.pose = std::move(arg);
    return std::move(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMapInfo msg_;
};

class Init_GridMapInfo_length_y
{
public:
  explicit Init_GridMapInfo_length_y(::grid_map_msgs::msg::GridMapInfo & msg)
  : msg_(msg)
  {}
  Init_GridMapInfo_pose length_y(::grid_map_msgs::msg::GridMapInfo::_length_y_type arg)
  {
    msg_.length_y = std::move(arg);
    return Init_GridMapInfo_pose(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMapInfo msg_;
};

class Init_GridMapInfo_length_x
{
public:
  explicit Init_GridMapInfo_length_x(::grid_map_msgs::msg::GridMapInfo & msg)
  : msg_(msg)
  {}
  Init_GridMapInfo_length_y length_x(::grid_map_msgs::msg::GridMapInfo::_length_x_type arg)
  {
    msg_.length_x = std::move(arg);
    return Init_GridMapInfo_length_y(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMapInfo msg_;
};

class Init_GridMapInfo_resolution
{
public:
  explicit Init_GridMapInfo_resolution(::grid_map_msgs::msg::GridMapInfo & msg)
  : msg_(msg)
  {}
  Init_GridMapInfo_length_x resolution(::grid_map_msgs::msg::GridMapInfo::_resolution_type arg)
  {
    msg_.resolution = std::move(arg);
    return Init_GridMapInfo_length_x(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMapInfo msg_;
};

class Init_GridMapInfo_header
{
public:
  Init_GridMapInfo_header()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_GridMapInfo_resolution header(::grid_map_msgs::msg::GridMapInfo::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_GridMapInfo_resolution(msg_);
  }

private:
  ::grid_map_msgs::msg::GridMapInfo msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::grid_map_msgs::msg::GridMapInfo>()
{
  return grid_map_msgs::msg::builder::Init_GridMapInfo_header();
}

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__BUILDER_HPP_
