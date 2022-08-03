// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/RobotConfiguration.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'tint'
#include "std_msgs/msg/detail/color_rgba__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__RobotConfiguration __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__RobotConfiguration __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct RobotConfiguration_
{
  using Type = RobotConfiguration_<ContainerAllocator>;

  explicit RobotConfiguration_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : tint(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->source_parameter = "";
      this->saved_robot_name = "";
      this->frame_prefix = "";
      this->frame_suffix = "";
      this->attached_to_tf = false;
      this->render_as_occlusion_only = false;
      this->metallic = 0.0f;
      this->smoothness = 0.0f;
      this->id = "";
      this->visible = false;
    }
  }

  explicit RobotConfiguration_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : source_parameter(_alloc),
    saved_robot_name(_alloc),
    frame_prefix(_alloc),
    frame_suffix(_alloc),
    tint(_alloc, _init),
    id(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->source_parameter = "";
      this->saved_robot_name = "";
      this->frame_prefix = "";
      this->frame_suffix = "";
      this->attached_to_tf = false;
      this->render_as_occlusion_only = false;
      this->metallic = 0.0f;
      this->smoothness = 0.0f;
      this->id = "";
      this->visible = false;
    }
  }

  // field types and members
  using _source_parameter_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _source_parameter_type source_parameter;
  using _saved_robot_name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _saved_robot_name_type saved_robot_name;
  using _frame_prefix_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _frame_prefix_type frame_prefix;
  using _frame_suffix_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _frame_suffix_type frame_suffix;
  using _attached_to_tf_type =
    bool;
  _attached_to_tf_type attached_to_tf;
  using _render_as_occlusion_only_type =
    bool;
  _render_as_occlusion_only_type render_as_occlusion_only;
  using _tint_type =
    std_msgs::msg::ColorRGBA_<ContainerAllocator>;
  _tint_type tint;
  using _metallic_type =
    float;
  _metallic_type metallic;
  using _smoothness_type =
    float;
  _smoothness_type smoothness;
  using _id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _id_type id;
  using _visible_type =
    bool;
  _visible_type visible;

  // setters for named parameter idiom
  Type & set__source_parameter(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->source_parameter = _arg;
    return *this;
  }
  Type & set__saved_robot_name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->saved_robot_name = _arg;
    return *this;
  }
  Type & set__frame_prefix(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->frame_prefix = _arg;
    return *this;
  }
  Type & set__frame_suffix(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->frame_suffix = _arg;
    return *this;
  }
  Type & set__attached_to_tf(
    const bool & _arg)
  {
    this->attached_to_tf = _arg;
    return *this;
  }
  Type & set__render_as_occlusion_only(
    const bool & _arg)
  {
    this->render_as_occlusion_only = _arg;
    return *this;
  }
  Type & set__tint(
    const std_msgs::msg::ColorRGBA_<ContainerAllocator> & _arg)
  {
    this->tint = _arg;
    return *this;
  }
  Type & set__metallic(
    const float & _arg)
  {
    this->metallic = _arg;
    return *this;
  }
  Type & set__smoothness(
    const float & _arg)
  {
    this->smoothness = _arg;
    return *this;
  }
  Type & set__id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->id = _arg;
    return *this;
  }
  Type & set__visible(
    const bool & _arg)
  {
    this->visible = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::RobotConfiguration_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::RobotConfiguration_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::RobotConfiguration_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::RobotConfiguration_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__RobotConfiguration
    std::shared_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__RobotConfiguration
    std::shared_ptr<iviz_msgs::msg::RobotConfiguration_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const RobotConfiguration_ & other) const
  {
    if (this->source_parameter != other.source_parameter) {
      return false;
    }
    if (this->saved_robot_name != other.saved_robot_name) {
      return false;
    }
    if (this->frame_prefix != other.frame_prefix) {
      return false;
    }
    if (this->frame_suffix != other.frame_suffix) {
      return false;
    }
    if (this->attached_to_tf != other.attached_to_tf) {
      return false;
    }
    if (this->render_as_occlusion_only != other.render_as_occlusion_only) {
      return false;
    }
    if (this->tint != other.tint) {
      return false;
    }
    if (this->metallic != other.metallic) {
      return false;
    }
    if (this->smoothness != other.smoothness) {
      return false;
    }
    if (this->id != other.id) {
      return false;
    }
    if (this->visible != other.visible) {
      return false;
    }
    return true;
  }
  bool operator!=(const RobotConfiguration_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct RobotConfiguration_

// alias to use template instance with default allocator
using RobotConfiguration =
  iviz_msgs::msg::RobotConfiguration_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__ROBOT_CONFIGURATION__STRUCT_HPP_
