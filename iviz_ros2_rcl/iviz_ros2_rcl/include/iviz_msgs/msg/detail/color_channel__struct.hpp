// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/ColorChannel.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'colors'
#include "iviz_msgs/msg/detail/color32__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__ColorChannel __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__ColorChannel __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct ColorChannel_
{
  using Type = ColorChannel_<ContainerAllocator>;

  explicit ColorChannel_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
  }

  explicit ColorChannel_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
    (void)_alloc;
  }

  // field types and members
  using _colors_type =
    std::vector<iviz_msgs::msg::Color32_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Color32_<ContainerAllocator>>::other>;
  _colors_type colors;

  // setters for named parameter idiom
  Type & set__colors(
    const std::vector<iviz_msgs::msg::Color32_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Color32_<ContainerAllocator>>::other> & _arg)
  {
    this->colors = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::ColorChannel_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::ColorChannel_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::ColorChannel_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::ColorChannel_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__ColorChannel
    std::shared_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__ColorChannel
    std::shared_ptr<iviz_msgs::msg::ColorChannel_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ColorChannel_ & other) const
  {
    if (this->colors != other.colors) {
      return false;
    }
    return true;
  }
  bool operator!=(const ColorChannel_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ColorChannel_

// alias to use template instance with default allocator
using ColorChannel =
  iviz_msgs::msg::ColorChannel_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__COLOR_CHANNEL__STRUCT_HPP_
