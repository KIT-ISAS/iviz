// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/BoundingBoxStamped.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__STRUCT_HPP_

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
// Member 'boundary'
#include "iviz_msgs/msg/detail/bounding_box__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__BoundingBoxStamped __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__BoundingBoxStamped __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct BoundingBoxStamped_
{
  using Type = BoundingBoxStamped_<ContainerAllocator>;

  explicit BoundingBoxStamped_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_init),
    boundary(_init)
  {
    (void)_init;
  }

  explicit BoundingBoxStamped_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_alloc, _init),
    boundary(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _header_type =
    std_msgs::msg::Header_<ContainerAllocator>;
  _header_type header;
  using _boundary_type =
    iviz_msgs::msg::BoundingBox_<ContainerAllocator>;
  _boundary_type boundary;

  // setters for named parameter idiom
  Type & set__header(
    const std_msgs::msg::Header_<ContainerAllocator> & _arg)
  {
    this->header = _arg;
    return *this;
  }
  Type & set__boundary(
    const iviz_msgs::msg::BoundingBox_<ContainerAllocator> & _arg)
  {
    this->boundary = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__BoundingBoxStamped
    std::shared_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__BoundingBoxStamped
    std::shared_ptr<iviz_msgs::msg::BoundingBoxStamped_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const BoundingBoxStamped_ & other) const
  {
    if (this->header != other.header) {
      return false;
    }
    if (this->boundary != other.boundary) {
      return false;
    }
    return true;
  }
  bool operator!=(const BoundingBoxStamped_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct BoundingBoxStamped_

// alias to use template instance with default allocator
using BoundingBoxStamped =
  iviz_msgs::msg::BoundingBoxStamped_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__BOUNDING_BOX_STAMPED__STRUCT_HPP_
