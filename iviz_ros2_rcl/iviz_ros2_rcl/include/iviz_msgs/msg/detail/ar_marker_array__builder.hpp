// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/ARMarkerArray.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__BUILDER_HPP_

#include "iviz_msgs/msg/detail/ar_marker_array__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_ARMarkerArray_markers
{
public:
  Init_ARMarkerArray_markers()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::msg::ARMarkerArray markers(::iviz_msgs::msg::ARMarkerArray::_markers_type arg)
  {
    msg_.markers = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::ARMarkerArray msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::ARMarkerArray>()
{
  return iviz_msgs::msg::builder::Init_ARMarkerArray_markers();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__AR_MARKER_ARRAY__BUILDER_HPP_
