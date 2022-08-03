// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Dialog.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__DIALOG__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__DIALOG__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.hpp"
// Member 'lifetime'
#include "builtin_interfaces/msg/detail/duration__struct.hpp"
// Member 'background_color'
#include "std_msgs/msg/detail/color_rgba__struct.hpp"
// Member 'tf_offset'
// Member 'dialog_displacement'
// Member 'tf_displacement'
#include "geometry_msgs/msg/detail/vector3__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Dialog __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Dialog __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Dialog_
{
  using Type = Dialog_<ContainerAllocator>;

  explicit Dialog_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_init),
    lifetime(_init),
    background_color(_init),
    tf_offset(_init),
    dialog_displacement(_init),
    tf_displacement(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->action = 0;
      this->id = "";
      this->scale = 0.0;
      this->type = 0;
      this->buttons = 0;
      this->icon = 0;
      this->title = "";
      this->caption = "";
      this->caption_alignment = 0;
      this->binding_type = 0;
    }
  }

  explicit Dialog_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_alloc, _init),
    id(_alloc),
    lifetime(_alloc, _init),
    background_color(_alloc, _init),
    title(_alloc),
    caption(_alloc),
    tf_offset(_alloc, _init),
    dialog_displacement(_alloc, _init),
    tf_displacement(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->action = 0;
      this->id = "";
      this->scale = 0.0;
      this->type = 0;
      this->buttons = 0;
      this->icon = 0;
      this->title = "";
      this->caption = "";
      this->caption_alignment = 0;
      this->binding_type = 0;
    }
  }

  // field types and members
  using _header_type =
    std_msgs::msg::Header_<ContainerAllocator>;
  _header_type header;
  using _action_type =
    unsigned char;
  _action_type action;
  using _id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _id_type id;
  using _lifetime_type =
    builtin_interfaces::msg::Duration_<ContainerAllocator>;
  _lifetime_type lifetime;
  using _scale_type =
    double;
  _scale_type scale;
  using _type_type =
    unsigned char;
  _type_type type;
  using _buttons_type =
    unsigned char;
  _buttons_type buttons;
  using _icon_type =
    unsigned char;
  _icon_type icon;
  using _background_color_type =
    std_msgs::msg::ColorRGBA_<ContainerAllocator>;
  _background_color_type background_color;
  using _title_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _title_type title;
  using _caption_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _caption_type caption;
  using _caption_alignment_type =
    uint16_t;
  _caption_alignment_type caption_alignment;
  using _menu_entries_type =
    std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other>;
  _menu_entries_type menu_entries;
  using _binding_type_type =
    unsigned char;
  _binding_type_type binding_type;
  using _tf_offset_type =
    geometry_msgs::msg::Vector3_<ContainerAllocator>;
  _tf_offset_type tf_offset;
  using _dialog_displacement_type =
    geometry_msgs::msg::Vector3_<ContainerAllocator>;
  _dialog_displacement_type dialog_displacement;
  using _tf_displacement_type =
    geometry_msgs::msg::Vector3_<ContainerAllocator>;
  _tf_displacement_type tf_displacement;

  // setters for named parameter idiom
  Type & set__header(
    const std_msgs::msg::Header_<ContainerAllocator> & _arg)
  {
    this->header = _arg;
    return *this;
  }
  Type & set__action(
    const unsigned char & _arg)
  {
    this->action = _arg;
    return *this;
  }
  Type & set__id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->id = _arg;
    return *this;
  }
  Type & set__lifetime(
    const builtin_interfaces::msg::Duration_<ContainerAllocator> & _arg)
  {
    this->lifetime = _arg;
    return *this;
  }
  Type & set__scale(
    const double & _arg)
  {
    this->scale = _arg;
    return *this;
  }
  Type & set__type(
    const unsigned char & _arg)
  {
    this->type = _arg;
    return *this;
  }
  Type & set__buttons(
    const unsigned char & _arg)
  {
    this->buttons = _arg;
    return *this;
  }
  Type & set__icon(
    const unsigned char & _arg)
  {
    this->icon = _arg;
    return *this;
  }
  Type & set__background_color(
    const std_msgs::msg::ColorRGBA_<ContainerAllocator> & _arg)
  {
    this->background_color = _arg;
    return *this;
  }
  Type & set__title(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->title = _arg;
    return *this;
  }
  Type & set__caption(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->caption = _arg;
    return *this;
  }
  Type & set__caption_alignment(
    const uint16_t & _arg)
  {
    this->caption_alignment = _arg;
    return *this;
  }
  Type & set__menu_entries(
    const std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other> & _arg)
  {
    this->menu_entries = _arg;
    return *this;
  }
  Type & set__binding_type(
    const unsigned char & _arg)
  {
    this->binding_type = _arg;
    return *this;
  }
  Type & set__tf_offset(
    const geometry_msgs::msg::Vector3_<ContainerAllocator> & _arg)
  {
    this->tf_offset = _arg;
    return *this;
  }
  Type & set__dialog_displacement(
    const geometry_msgs::msg::Vector3_<ContainerAllocator> & _arg)
  {
    this->dialog_displacement = _arg;
    return *this;
  }
  Type & set__tf_displacement(
    const geometry_msgs::msg::Vector3_<ContainerAllocator> & _arg)
  {
    this->tf_displacement = _arg;
    return *this;
  }

  // constant declarations
  static constexpr uint8_t ACTION_ADD =
    0u;
  static constexpr uint8_t ACTION_REMOVE =
    1u;
  static constexpr uint8_t ACTION_REMOVEALL =
    2u;
  static constexpr uint8_t TYPE_PLAIN =
    0u;
  static constexpr uint8_t TYPE_SHORT =
    1u;
  static constexpr uint8_t TYPE_NOTICE =
    2u;
  static constexpr uint8_t TYPE_MENU =
    3u;
  static constexpr uint8_t TYPE_BUTTON =
    4u;
  static constexpr uint8_t TYPE_ICON =
    5u;
  static constexpr uint8_t BUTTONS_OK =
    0u;
  static constexpr uint8_t BUTTONS_YESNO =
    1u;
  static constexpr uint8_t BUTTONS_OKCANCEL =
    2u;
  static constexpr uint8_t BUTTONS_FORWARD =
    3u;
  static constexpr uint8_t BUTTONS_FORWARDBACKWARD =
    4u;
  static constexpr uint8_t BUTTONS_BACKWARD =
    5u;
  static constexpr uint8_t ICON_NONE =
    0u;
  static constexpr uint8_t ICON_CROSS =
    1u;
  static constexpr uint8_t ICON_OK =
    2u;
  static constexpr uint8_t ICON_FORWARD =
    3u;
  static constexpr uint8_t ICON_BACKWARD =
    4u;
  static constexpr uint8_t ICON_DIALOG =
    5u;
  static constexpr uint8_t ICON_UP =
    6u;
  static constexpr uint8_t ICON_DOWN =
    7u;
  static constexpr uint8_t ICON_INFO =
    8u;
  static constexpr uint8_t ICON_WARN =
    9u;
  static constexpr uint8_t ICON_ERROR =
    10u;
  static constexpr uint8_t ICON_DIALOGS =
    11u;
  static constexpr uint8_t ICON_QUESTION =
    12u;
  static constexpr uint16_t ALIGNMENT_DEFAULT =
    0u;
  static constexpr uint16_t ALIGNMENT_LEFT =
    1u;
  static constexpr uint16_t ALIGNMENT_CENTER =
    2u;
  static constexpr uint16_t ALIGNMENT_RIGHT =
    4u;
  static constexpr uint16_t ALIGNMENT_JUSTIFIED =
    8u;
  static constexpr uint16_t ALIGNMENT_FLUSH =
    16u;
  static constexpr uint16_t ALIGNMENT_GEOMETRYCENTER =
    32u;
  static constexpr uint16_t ALIGNMENT_TOP =
    256u;
  static constexpr uint16_t ALIGNMENT_MID =
    512u;
  static constexpr uint16_t ALIGNMENT_BOTTOM =
    1024u;

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Dialog_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Dialog_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Dialog_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Dialog_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Dialog
    std::shared_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Dialog
    std::shared_ptr<iviz_msgs::msg::Dialog_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Dialog_ & other) const
  {
    if (this->header != other.header) {
      return false;
    }
    if (this->action != other.action) {
      return false;
    }
    if (this->id != other.id) {
      return false;
    }
    if (this->lifetime != other.lifetime) {
      return false;
    }
    if (this->scale != other.scale) {
      return false;
    }
    if (this->type != other.type) {
      return false;
    }
    if (this->buttons != other.buttons) {
      return false;
    }
    if (this->icon != other.icon) {
      return false;
    }
    if (this->background_color != other.background_color) {
      return false;
    }
    if (this->title != other.title) {
      return false;
    }
    if (this->caption != other.caption) {
      return false;
    }
    if (this->caption_alignment != other.caption_alignment) {
      return false;
    }
    if (this->menu_entries != other.menu_entries) {
      return false;
    }
    if (this->binding_type != other.binding_type) {
      return false;
    }
    if (this->tf_offset != other.tf_offset) {
      return false;
    }
    if (this->dialog_displacement != other.dialog_displacement) {
      return false;
    }
    if (this->tf_displacement != other.tf_displacement) {
      return false;
    }
    return true;
  }
  bool operator!=(const Dialog_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Dialog_

// alias to use template instance with default allocator
using Dialog =
  iviz_msgs::msg::Dialog_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ACTION_ADD;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ACTION_REMOVE;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ACTION_REMOVEALL;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::TYPE_PLAIN;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::TYPE_SHORT;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::TYPE_NOTICE;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::TYPE_MENU;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::TYPE_BUTTON;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::TYPE_ICON;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::BUTTONS_OK;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::BUTTONS_YESNO;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::BUTTONS_OKCANCEL;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::BUTTONS_FORWARD;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::BUTTONS_FORWARDBACKWARD;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::BUTTONS_BACKWARD;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_NONE;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_CROSS;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_OK;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_FORWARD;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_BACKWARD;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_DIALOG;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_UP;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_DOWN;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_INFO;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_WARN;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_ERROR;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_DIALOGS;
template<typename ContainerAllocator>
constexpr uint8_t Dialog_<ContainerAllocator>::ICON_QUESTION;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_DEFAULT;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_LEFT;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_CENTER;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_RIGHT;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_JUSTIFIED;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_FLUSH;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_GEOMETRYCENTER;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_TOP;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_MID;
template<typename ContainerAllocator>
constexpr uint16_t Dialog_<ContainerAllocator>::ALIGNMENT_BOTTOM;

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__DIALOG__STRUCT_HPP_
