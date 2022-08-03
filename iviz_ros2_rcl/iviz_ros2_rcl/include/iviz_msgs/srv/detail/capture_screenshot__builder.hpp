// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/CaptureScreenshot.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__BUILDER_HPP_

#include "iviz_msgs/srv/detail/capture_screenshot__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_CaptureScreenshot_Request_compress
{
public:
  Init_CaptureScreenshot_Request_compress()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::srv::CaptureScreenshot_Request compress(::iviz_msgs::srv::CaptureScreenshot_Request::_compress_type arg)
  {
    msg_.compress = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Request msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::CaptureScreenshot_Request>()
{
  return iviz_msgs::srv::builder::Init_CaptureScreenshot_Request_compress();
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_CaptureScreenshot_Response_data
{
public:
  explicit Init_CaptureScreenshot_Response_data(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::srv::CaptureScreenshot_Response data(::iviz_msgs::srv::CaptureScreenshot_Response::_data_type arg)
  {
    msg_.data = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_pose
{
public:
  explicit Init_CaptureScreenshot_Response_pose(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  Init_CaptureScreenshot_Response_data pose(::iviz_msgs::srv::CaptureScreenshot_Response::_pose_type arg)
  {
    msg_.pose = std::move(arg);
    return Init_CaptureScreenshot_Response_data(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_intrinsics
{
public:
  explicit Init_CaptureScreenshot_Response_intrinsics(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  Init_CaptureScreenshot_Response_pose intrinsics(::iviz_msgs::srv::CaptureScreenshot_Response::_intrinsics_type arg)
  {
    msg_.intrinsics = std::move(arg);
    return Init_CaptureScreenshot_Response_pose(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_bpp
{
public:
  explicit Init_CaptureScreenshot_Response_bpp(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  Init_CaptureScreenshot_Response_intrinsics bpp(::iviz_msgs::srv::CaptureScreenshot_Response::_bpp_type arg)
  {
    msg_.bpp = std::move(arg);
    return Init_CaptureScreenshot_Response_intrinsics(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_height
{
public:
  explicit Init_CaptureScreenshot_Response_height(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  Init_CaptureScreenshot_Response_bpp height(::iviz_msgs::srv::CaptureScreenshot_Response::_height_type arg)
  {
    msg_.height = std::move(arg);
    return Init_CaptureScreenshot_Response_bpp(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_width
{
public:
  explicit Init_CaptureScreenshot_Response_width(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  Init_CaptureScreenshot_Response_height width(::iviz_msgs::srv::CaptureScreenshot_Response::_width_type arg)
  {
    msg_.width = std::move(arg);
    return Init_CaptureScreenshot_Response_height(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_header
{
public:
  explicit Init_CaptureScreenshot_Response_header(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  Init_CaptureScreenshot_Response_width header(::iviz_msgs::srv::CaptureScreenshot_Response::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_CaptureScreenshot_Response_width(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_message
{
public:
  explicit Init_CaptureScreenshot_Response_message(::iviz_msgs::srv::CaptureScreenshot_Response & msg)
  : msg_(msg)
  {}
  Init_CaptureScreenshot_Response_header message(::iviz_msgs::srv::CaptureScreenshot_Response::_message_type arg)
  {
    msg_.message = std::move(arg);
    return Init_CaptureScreenshot_Response_header(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

class Init_CaptureScreenshot_Response_success
{
public:
  Init_CaptureScreenshot_Response_success()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_CaptureScreenshot_Response_message success(::iviz_msgs::srv::CaptureScreenshot_Response::_success_type arg)
  {
    msg_.success = std::move(arg);
    return Init_CaptureScreenshot_Response_message(msg_);
  }

private:
  ::iviz_msgs::srv::CaptureScreenshot_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::CaptureScreenshot_Response>()
{
  return iviz_msgs::srv::builder::Init_CaptureScreenshot_Response_success();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__BUILDER_HPP_
