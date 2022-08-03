// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/GetFramePose.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_FRAME_POSE__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__GET_FRAME_POSE__BUILDER_HPP_

#include "iviz_msgs/srv/detail/get_frame_pose__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_GetFramePose_Request_frames
{
public:
  Init_GetFramePose_Request_frames()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::srv::GetFramePose_Request frames(::iviz_msgs::srv::GetFramePose_Request::_frames_type arg)
  {
    msg_.frames = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::GetFramePose_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::GetFramePose_Request>()
{
  return iviz_msgs::srv::builder::Init_GetFramePose_Request_frames();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_GetFramePose_Response_poses
{
public:
  explicit Init_GetFramePose_Response_poses(::iviz_msgs::srv::GetFramePose_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::GetFramePose_Response poses(::iviz_msgs::srv::GetFramePose_Response::_poses_type arg)
  {
    msg_.poses = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::GetFramePose_Response msg_;
};

class Init_GetFramePose_Response_is_valid
{
public:
  Init_GetFramePose_Response_is_valid()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_GetFramePose_Response_poses is_valid(::iviz_msgs::srv::GetFramePose_Response::_is_valid_type arg)
  {
    msg_.is_valid = std::move(arg);
    return Init_GetFramePose_Response_poses(msg_);
  }

private:
  ::iviz_msgs::srv::GetFramePose_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::GetFramePose_Response>()
{
  return iviz_msgs::srv::builder::Init_GetFramePose_Response_is_valid();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_FRAME_POSE__BUILDER_HPP_
