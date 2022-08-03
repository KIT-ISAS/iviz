// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Widget.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__WIDGET__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__WIDGET__STRUCT_HPP_

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
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__struct.hpp"
// Member 'color'
// Member 'secondary_color'
#include "std_msgs/msg/detail/color_rgba__struct.hpp"
// Member 'boundary'
#include "iviz_msgs/msg/detail/bounding_box__struct.hpp"
// Member 'secondary_boundaries'
#include "iviz_msgs/msg/detail/bounding_box_stamped__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Widget __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Widget __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Widget_
{
  using Type = Widget_<ContainerAllocator>;

  explicit Widget_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_init),
    pose(_init),
    color(_init),
    secondary_color(_init),
    boundary(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->action = 0;
      this->id = "";
      this->type = 0;
      this->scale = 0.0;
      this->secondary_scale = 0.0;
      this->caption = "";
    }
  }

  explicit Widget_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_alloc, _init),
    id(_alloc),
    pose(_alloc, _init),
    color(_alloc, _init),
    secondary_color(_alloc, _init),
    caption(_alloc),
    boundary(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->action = 0;
      this->id = "";
      this->type = 0;
      this->scale = 0.0;
      this->secondary_scale = 0.0;
      this->caption = "";
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
  using _type_type =
    unsigned char;
  _type_type type;
  using _pose_type =
    geometry_msgs::msg::Pose_<ContainerAllocator>;
  _pose_type pose;
  using _color_type =
    std_msgs::msg::ColorRGBA_<ContainerAllocator>;
  _color_type color;
  using _secondary_color_type =
    std_msgs::msg::ColorRGBA_<ContainerAllocator>;
  _secondary_color_type secondary_color;
  using _scale_type =
    double;
  _scale_type scale;
  using _secondary_scale_type =
    double;
  _secondary_scale_type secondary_scale;
  using _caption_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _caption_type caption;
  using _boundary_type =
    iviz_msgs::msg::BoundingBox_<ContainerAllocator>;
  _boundary_type boundary;
  using _secondary_boundaries_type =
    std::vector<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>>::other>;
  _secondary_boundaries_type secondary_boundaries;

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
  Type & set__type(
    const unsigned char & _arg)
  {
    this->type = _arg;
    return *this;
  }
  Type & set__pose(
    const geometry_msgs::msg::Pose_<ContainerAllocator> & _arg)
  {
    this->pose = _arg;
    return *this;
  }
  Type & set__color(
    const std_msgs::msg::ColorRGBA_<ContainerAllocator> & _arg)
  {
    this->color = _arg;
    return *this;
  }
  Type & set__secondary_color(
    const std_msgs::msg::ColorRGBA_<ContainerAllocator> & _arg)
  {
    this->secondary_color = _arg;
    return *this;
  }
  Type & set__scale(
    const double & _arg)
  {
    this->scale = _arg;
    return *this;
  }
  Type & set__secondary_scale(
    const double & _arg)
  {
    this->secondary_scale = _arg;
    return *this;
  }
  Type & set__caption(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->caption = _arg;
    return *this;
  }
  Type & set__boundary(
    const iviz_msgs::msg::BoundingBox_<ContainerAllocator> & _arg)
  {
    this->boundary = _arg;
    return *this;
  }
  Type & set__secondary_boundaries(
    const std::vector<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>>::other> & _arg)
  {
    this->secondary_boundaries = _arg;
    return *this;
  }

  // constant declarations
  static constexpr uint8_t ACTION_ADD =
    0u;
  static constexpr uint8_t ACTION_REMOVE =
    1u;
  static constexpr uint8_t ACTION_REMOVEALL =
    2u;
  static constexpr uint8_t TYPE_ROTATIONDISC =
    0u;
  static constexpr uint8_t TYPE_SPRINGDISC =
    1u;
  static constexpr uint8_t TYPE_SPRINGDISC3D =
    2u;
  static constexpr uint8_t TYPE_TRAJECTORYDISC =
    3u;
  static constexpr uint8_t TYPE_TOOLTIP =
    4u;
  static constexpr uint8_t TYPE_TARGETAREA =
    5u;
  static constexpr uint8_t TYPE_POSITIONDISC =
    6u;
  static constexpr uint8_t TYPE_POSITIONDISC3D =
    7u;
  static constexpr uint8_t TYPE_BOUNDARY =
    8u;
  static constexpr uint8_t TYPE_BOUNDARYCHECK =
    9u;

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Widget_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Widget_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Widget_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Widget_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Widget_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Widget_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Widget_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Widget_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Widget_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Widget_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Widget
    std::shared_ptr<iviz_msgs::msg::Widget_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Widget
    std::shared_ptr<iviz_msgs::msg::Widget_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Widget_ & other) const
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
    if (this->type != other.type) {
      return false;
    }
    if (this->pose != other.pose) {
      return false;
    }
    if (this->color != other.color) {
      return false;
    }
    if (this->secondary_color != other.secondary_color) {
      return false;
    }
    if (this->scale != other.scale) {
      return false;
    }
    if (this->secondary_scale != other.secondary_scale) {
      return false;
    }
    if (this->caption != other.caption) {
      return false;
    }
    if (this->boundary != other.boundary) {
      return false;
    }
    if (this->secondary_boundaries != other.secondary_boundaries) {
      return false;
    }
    return true;
  }
  bool operator!=(const Widget_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Widget_

// alias to use template instance with default allocator
using Widget =
  iviz_msgs::msg::Widget_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::ACTION_ADD;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::ACTION_REMOVE;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::ACTION_REMOVEALL;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_ROTATIONDISC;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_SPRINGDISC;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_SPRINGDISC3D;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_TRAJECTORYDISC;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_TOOLTIP;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_TARGETAREA;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_POSITIONDISC;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_POSITIONDISC3D;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_BOUNDARY;
template<typename ContainerAllocator>
constexpr uint8_t Widget_<ContainerAllocator>::TYPE_BOUNDARYCHECK;

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__WIDGET__STRUCT_HPP_
