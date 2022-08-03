// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/ARMarker.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__AR_MARKER__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__AR_MARKER__BUILDER_HPP_

#include "iviz_msgs/msg/detail/ar_marker__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_ARMarker_pose_relative_to_camera
{
public:
  explicit Init_ARMarker_pose_relative_to_camera(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::ARMarker pose_relative_to_camera(::iviz_msgs::msg::ARMarker::_pose_relative_to_camera_type arg)
  {
    msg_.pose_relative_to_camera = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_marker_size_in_mm
{
public:
  explicit Init_ARMarker_marker_size_in_mm(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  Init_ARMarker_pose_relative_to_camera marker_size_in_mm(::iviz_msgs::msg::ARMarker::_marker_size_in_mm_type arg)
  {
    msg_.marker_size_in_mm = std::move(arg);
    return Init_ARMarker_pose_relative_to_camera(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_has_reliable_pose
{
public:
  explicit Init_ARMarker_has_reliable_pose(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  Init_ARMarker_marker_size_in_mm has_reliable_pose(::iviz_msgs::msg::ARMarker::_has_reliable_pose_type arg)
  {
    msg_.has_reliable_pose = std::move(arg);
    return Init_ARMarker_marker_size_in_mm(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_camera_pose
{
public:
  explicit Init_ARMarker_camera_pose(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  Init_ARMarker_has_reliable_pose camera_pose(::iviz_msgs::msg::ARMarker::_camera_pose_type arg)
  {
    msg_.camera_pose = std::move(arg);
    return Init_ARMarker_has_reliable_pose(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_camera_intrinsic
{
public:
  explicit Init_ARMarker_camera_intrinsic(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  Init_ARMarker_camera_pose camera_intrinsic(::iviz_msgs::msg::ARMarker::_camera_intrinsic_type arg)
  {
    msg_.camera_intrinsic = std::move(arg);
    return Init_ARMarker_camera_pose(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_corners
{
public:
  explicit Init_ARMarker_corners(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  Init_ARMarker_camera_intrinsic corners(::iviz_msgs::msg::ARMarker::_corners_type arg)
  {
    msg_.corners = std::move(arg);
    return Init_ARMarker_camera_intrinsic(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_code
{
public:
  explicit Init_ARMarker_code(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  Init_ARMarker_corners code(::iviz_msgs::msg::ARMarker::_code_type arg)
  {
    msg_.code = std::move(arg);
    return Init_ARMarker_corners(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_type
{
public:
  explicit Init_ARMarker_type(::iviz_msgs::msg::ARMarker & msg)
  : msg_(msg)
  {}
  Init_ARMarker_code type(::iviz_msgs::msg::ARMarker::_type_type arg)
  {
    msg_.type = std::move(arg);
    return Init_ARMarker_code(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

class Init_ARMarker_header
{
public:
  Init_ARMarker_header()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_ARMarker_type header(::iviz_msgs::msg::ARMarker::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_ARMarker_type(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarker msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::ARMarker>()
{
  return iviz_msgs::msg::builder::Init_ARMarker_header();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__AR_MARKER__BUILDER_HPP_
