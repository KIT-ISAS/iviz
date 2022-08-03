// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/WidgetArray.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__BUILDER_HPP_

#include "iviz_msgs/msg/detail/widget_array__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_WidgetArray_widgets
{
public:
  explicit Init_WidgetArray_widgets(::iviz_msgs::msg::WidgetArray & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::WidgetArray widgets(::iviz_msgs::msg::WidgetArray::_widgets_type arg)
  {
    msg_.widgets = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::WidgetArray msg_;
};

class Init_WidgetArray_dialogs
{
public:
  Init_WidgetArray_dialogs()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_WidgetArray_widgets dialogs(::iviz_msgs::msg::WidgetArray::_dialogs_type arg)
  {
    msg_.dialogs = std::move(arg);
    return Init_WidgetArray_widgets(msg_);
  }

private:
  ::iviz_msgs::msg::WidgetArray msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::WidgetArray>()
{
  return iviz_msgs::msg::builder::Init_WidgetArray_dialogs();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__BUILDER_HPP_
