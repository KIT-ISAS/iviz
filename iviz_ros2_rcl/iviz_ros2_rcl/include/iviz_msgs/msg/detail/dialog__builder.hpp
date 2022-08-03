// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Dialog.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__DIALOG__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__DIALOG__BUILDER_HPP_

#include "iviz_msgs/msg/detail/dialog__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Dialog_tf_displacement
{
public:
  explicit Init_Dialog_tf_displacement(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Dialog tf_displacement(::iviz_msgs::msg::Dialog::_tf_displacement_type arg)
  {
    msg_.tf_displacement = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_dialog_displacement
{
public:
  explicit Init_Dialog_dialog_displacement(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_tf_displacement dialog_displacement(::iviz_msgs::msg::Dialog::_dialog_displacement_type arg)
  {
    msg_.dialog_displacement = std::move(arg);
    return Init_Dialog_tf_displacement(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_tf_offset
{
public:
  explicit Init_Dialog_tf_offset(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_dialog_displacement tf_offset(::iviz_msgs::msg::Dialog::_tf_offset_type arg)
  {
    msg_.tf_offset = std::move(arg);
    return Init_Dialog_dialog_displacement(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_binding_type
{
public:
  explicit Init_Dialog_binding_type(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_tf_offset binding_type(::iviz_msgs::msg::Dialog::_binding_type_type arg)
  {
    msg_.binding_type = std::move(arg);
    return Init_Dialog_tf_offset(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_menu_entries
{
public:
  explicit Init_Dialog_menu_entries(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_binding_type menu_entries(::iviz_msgs::msg::Dialog::_menu_entries_type arg)
  {
    msg_.menu_entries = std::move(arg);
    return Init_Dialog_binding_type(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_caption_alignment
{
public:
  explicit Init_Dialog_caption_alignment(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_menu_entries caption_alignment(::iviz_msgs::msg::Dialog::_caption_alignment_type arg)
  {
    msg_.caption_alignment = std::move(arg);
    return Init_Dialog_menu_entries(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_caption
{
public:
  explicit Init_Dialog_caption(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_caption_alignment caption(::iviz_msgs::msg::Dialog::_caption_type arg)
  {
    msg_.caption = std::move(arg);
    return Init_Dialog_caption_alignment(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_title
{
public:
  explicit Init_Dialog_title(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_caption title(::iviz_msgs::msg::Dialog::_title_type arg)
  {
    msg_.title = std::move(arg);
    return Init_Dialog_caption(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_background_color
{
public:
  explicit Init_Dialog_background_color(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_title background_color(::iviz_msgs::msg::Dialog::_background_color_type arg)
  {
    msg_.background_color = std::move(arg);
    return Init_Dialog_title(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_icon
{
public:
  explicit Init_Dialog_icon(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_background_color icon(::iviz_msgs::msg::Dialog::_icon_type arg)
  {
    msg_.icon = std::move(arg);
    return Init_Dialog_background_color(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_buttons
{
public:
  explicit Init_Dialog_buttons(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_icon buttons(::iviz_msgs::msg::Dialog::_buttons_type arg)
  {
    msg_.buttons = std::move(arg);
    return Init_Dialog_icon(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_type
{
public:
  explicit Init_Dialog_type(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_buttons type(::iviz_msgs::msg::Dialog::_type_type arg)
  {
    msg_.type = std::move(arg);
    return Init_Dialog_buttons(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_scale
{
public:
  explicit Init_Dialog_scale(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_type scale(::iviz_msgs::msg::Dialog::_scale_type arg)
  {
    msg_.scale = std::move(arg);
    return Init_Dialog_type(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_lifetime
{
public:
  explicit Init_Dialog_lifetime(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_scale lifetime(::iviz_msgs::msg::Dialog::_lifetime_type arg)
  {
    msg_.lifetime = std::move(arg);
    return Init_Dialog_scale(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_id
{
public:
  explicit Init_Dialog_id(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_lifetime id(::iviz_msgs::msg::Dialog::_id_type arg)
  {
    msg_.id = std::move(arg);
    return Init_Dialog_lifetime(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_action
{
public:
  explicit Init_Dialog_action(::iviz_msgs::msg::Dialog & msg)
  : msg_(msg)
  {}
  Init_Dialog_id action(::iviz_msgs::msg::Dialog::_action_type arg)
  {
    msg_.action = std::move(arg);
    return Init_Dialog_id(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

class Init_Dialog_header
{
public:
  Init_Dialog_header()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Dialog_action header(::iviz_msgs::msg::Dialog::_header_type arg)
  {
    msg_.header = std::move(arg);
    return Init_Dialog_action(msg_);
  }

private:
  ::iviz_msgs::msg::Dialog msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Dialog>()
{
  return iviz_msgs::msg::builder::Init_Dialog_header();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__DIALOG__BUILDER_HPP_
