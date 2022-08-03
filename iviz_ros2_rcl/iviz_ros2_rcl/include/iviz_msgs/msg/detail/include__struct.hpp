// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Include.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__INCLUDE__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__INCLUDE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'pose'
#include "iviz_msgs/msg/detail/matrix4__struct.hpp"
// Member 'material'
#include "iviz_msgs/msg/detail/material__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Include __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Include __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Include_
{
  using Type = Include_<ContainerAllocator>;

  explicit Include_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : pose(_init),
    material(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->uri = "";
      this->package = "";
    }
  }

  explicit Include_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : uri(_alloc),
    pose(_alloc, _init),
    material(_alloc, _init),
    package(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->uri = "";
      this->package = "";
    }
  }

  // field types and members
  using _uri_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _uri_type uri;
  using _pose_type =
    iviz_msgs::msg::Matrix4_<ContainerAllocator>;
  _pose_type pose;
  using _material_type =
    iviz_msgs::msg::Material_<ContainerAllocator>;
  _material_type material;
  using _package_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _package_type package;

  // setters for named parameter idiom
  Type & set__uri(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->uri = _arg;
    return *this;
  }
  Type & set__pose(
    const iviz_msgs::msg::Matrix4_<ContainerAllocator> & _arg)
  {
    this->pose = _arg;
    return *this;
  }
  Type & set__material(
    const iviz_msgs::msg::Material_<ContainerAllocator> & _arg)
  {
    this->material = _arg;
    return *this;
  }
  Type & set__package(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->package = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Include_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Include_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Include_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Include_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Include_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Include_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Include_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Include_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Include_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Include_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Include
    std::shared_ptr<iviz_msgs::msg::Include_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Include
    std::shared_ptr<iviz_msgs::msg::Include_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Include_ & other) const
  {
    if (this->uri != other.uri) {
      return false;
    }
    if (this->pose != other.pose) {
      return false;
    }
    if (this->material != other.material) {
      return false;
    }
    if (this->package != other.package) {
      return false;
    }
    return true;
  }
  bool operator!=(const Include_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Include_

// alias to use template instance with default allocator
using Include =
  iviz_msgs::msg::Include_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__INCLUDE__STRUCT_HPP_
