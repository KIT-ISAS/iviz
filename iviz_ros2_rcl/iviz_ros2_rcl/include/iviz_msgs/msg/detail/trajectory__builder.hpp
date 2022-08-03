// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Trajectory.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TRAJECTORY__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__TRAJECTORY__BUILDER_HPP_

#include "iviz_msgs/msg/detail/trajectory__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Trajectory_timestamps
{
public:
  explicit Init_Trajectory_timestamps(::iviz_msgs::msg::Trajectory & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Trajectory timestamps(::iviz_msgs::msg::Trajectory::_timestamps_type arg)
  {
    msg_.timestamps = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Trajectory msg_;
};

class Init_Trajectory_poses
{
public:
  Init_Trajectory_poses()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Trajectory_timestamps poses(::iviz_msgs::msg::Trajectory::_poses_type arg)
  {
    msg_.poses = std::move(arg);
    return Init_Trajectory_timestamps(msg_);
  }

private:
  ::iviz_msgs::msg::Trajectory msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Trajectory>()
{
  return iviz_msgs::msg::builder::Init_Trajectory_poses();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__TRAJECTORY__BUILDER_HPP_
