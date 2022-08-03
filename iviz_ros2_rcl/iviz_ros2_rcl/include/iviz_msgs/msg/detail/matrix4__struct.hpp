// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Matrix4.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MATRIX4__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MATRIX4__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Matrix4 __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Matrix4 __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Matrix4_
{
  using Type = Matrix4_<ContainerAllocator>;

  explicit Matrix4_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      std::fill<typename std::array<float, 16>::iterator, float>(this->m.begin(), this->m.end(), 0.0f);
    }
  }

  explicit Matrix4_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : m(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      std::fill<typename std::array<float, 16>::iterator, float>(this->m.begin(), this->m.end(), 0.0f);
    }
  }

  // field types and members
  using _m_type =
    std::array<float, 16>;
  _m_type m;

  // setters for named parameter idiom
  Type & set__m(
    const std::array<float, 16> & _arg)
  {
    this->m = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Matrix4_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Matrix4_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Matrix4_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Matrix4_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Matrix4
    std::shared_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Matrix4
    std::shared_ptr<iviz_msgs::msg::Matrix4_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Matrix4_ & other) const
  {
    if (this->m != other.m) {
      return false;
    }
    return true;
  }
  bool operator!=(const Matrix4_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Matrix4_

// alias to use template instance with default allocator
using Matrix4 =
  iviz_msgs::msg::Matrix4_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MATRIX4__STRUCT_HPP_
