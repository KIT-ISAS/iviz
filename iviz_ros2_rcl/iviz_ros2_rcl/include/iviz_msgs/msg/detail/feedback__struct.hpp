// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Feedback.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__FEEDBACK__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__FEEDBACK__STRUCT_HPP_

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
// Member 'position'
#include "geometry_msgs/msg/detail/point__struct.hpp"
// Member 'orientation'
#include "geometry_msgs/msg/detail/quaternion__struct.hpp"
// Member 'scale'
#include "geometry_msgs/msg/detail/vector3__struct.hpp"
// Member 'trajectory'
#include "iviz_msgs/msg/detail/trajectory__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Feedback __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Feedback __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Feedback_
{
  using Type = Feedback_<ContainerAllocator>;

  explicit Feedback_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_init),
    position(_init),
    orientation(_init),
    scale(_init),
    trajectory(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->viz_id = "";
      this->id = "";
      this->type = 0;
      this->entry_id = 0l;
      this->angle = 0.0;
    }
  }

  explicit Feedback_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_alloc, _init),
    viz_id(_alloc),
    id(_alloc),
    position(_alloc, _init),
    orientation(_alloc, _init),
    scale(_alloc, _init),
    trajectory(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->viz_id = "";
      this->id = "";
      this->type = 0;
      this->entry_id = 0l;
      this->angle = 0.0;
    }
  }

  // field types and members
  using _header_type =
    std_msgs::msg::Header_<ContainerAllocator>;
  _header_type header;
  using _viz_id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _viz_id_type viz_id;
  using _id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _id_type id;
  using _type_type =
    uint8_t;
  _type_type type;
  using _entry_id_type =
    int32_t;
  _entry_id_type entry_id;
  using _angle_type =
    double;
  _angle_type angle;
  using _position_type =
    geometry_msgs::msg::Point_<ContainerAllocator>;
  _position_type position;
  using _orientation_type =
    geometry_msgs::msg::Quaternion_<ContainerAllocator>;
  _orientation_type orientation;
  using _scale_type =
    geometry_msgs::msg::Vector3_<ContainerAllocator>;
  _scale_type scale;
  using _trajectory_type =
    iviz_msgs::msg::Trajectory_<ContainerAllocator>;
  _trajectory_type trajectory;

  // setters for named parameter idiom
  Type & set__header(
    const std_msgs::msg::Header_<ContainerAllocator> & _arg)
  {
    this->header = _arg;
    return *this;
  }
  Type & set__viz_id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->viz_id = _arg;
    return *this;
  }
  Type & set__id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->id = _arg;
    return *this;
  }
  Type & set__type(
    const uint8_t & _arg)
  {
    this->type = _arg;
    return *this;
  }
  Type & set__entry_id(
    const int32_t & _arg)
  {
    this->entry_id = _arg;
    return *this;
  }
  Type & set__angle(
    const double & _arg)
  {
    this->angle = _arg;
    return *this;
  }
  Type & set__position(
    const geometry_msgs::msg::Point_<ContainerAllocator> & _arg)
  {
    this->position = _arg;
    return *this;
  }
  Type & set__orientation(
    const geometry_msgs::msg::Quaternion_<ContainerAllocator> & _arg)
  {
    this->orientation = _arg;
    return *this;
  }
  Type & set__scale(
    const geometry_msgs::msg::Vector3_<ContainerAllocator> & _arg)
  {
    this->scale = _arg;
    return *this;
  }
  Type & set__trajectory(
    const iviz_msgs::msg::Trajectory_<ContainerAllocator> & _arg)
  {
    this->trajectory = _arg;
    return *this;
  }

  // constant declarations
  static constexpr uint8_t TYPE_EXPIRED =
    0u;
  static constexpr uint8_t TYPE_BUTTON_CLICK =
    1u;
  static constexpr uint8_t TYPE_MENUENTRY_CLICK =
    2u;
  static constexpr uint8_t TYPE_POSITION_CHANGED =
    3u;
  static constexpr uint8_t TYPE_ORIENTATION_CHANGED =
    4u;
  static constexpr uint8_t TYPE_SCALE_CHANGED =
    5u;
  static constexpr uint8_t TYPE_TRAJECTORY_CHANGED =
    6u;

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Feedback_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Feedback_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Feedback_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Feedback_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Feedback
    std::shared_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Feedback
    std::shared_ptr<iviz_msgs::msg::Feedback_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Feedback_ & other) const
  {
    if (this->header != other.header) {
      return false;
    }
    if (this->viz_id != other.viz_id) {
      return false;
    }
    if (this->id != other.id) {
      return false;
    }
    if (this->type != other.type) {
      return false;
    }
    if (this->entry_id != other.entry_id) {
      return false;
    }
    if (this->angle != other.angle) {
      return false;
    }
    if (this->position != other.position) {
      return false;
    }
    if (this->orientation != other.orientation) {
      return false;
    }
    if (this->scale != other.scale) {
      return false;
    }
    if (this->trajectory != other.trajectory) {
      return false;
    }
    return true;
  }
  bool operator!=(const Feedback_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Feedback_

// alias to use template instance with default allocator
using Feedback =
  iviz_msgs::msg::Feedback_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
constexpr uint8_t Feedback_<ContainerAllocator>::TYPE_EXPIRED;
template<typename ContainerAllocator>
constexpr uint8_t Feedback_<ContainerAllocator>::TYPE_BUTTON_CLICK;
template<typename ContainerAllocator>
constexpr uint8_t Feedback_<ContainerAllocator>::TYPE_MENUENTRY_CLICK;
template<typename ContainerAllocator>
constexpr uint8_t Feedback_<ContainerAllocator>::TYPE_POSITION_CHANGED;
template<typename ContainerAllocator>
constexpr uint8_t Feedback_<ContainerAllocator>::TYPE_ORIENTATION_CHANGED;
template<typename ContainerAllocator>
constexpr uint8_t Feedback_<ContainerAllocator>::TYPE_SCALE_CHANGED;
template<typename ContainerAllocator>
constexpr uint8_t Feedback_<ContainerAllocator>::TYPE_TRAJECTORY_CHANGED;

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__FEEDBACK__STRUCT_HPP_
