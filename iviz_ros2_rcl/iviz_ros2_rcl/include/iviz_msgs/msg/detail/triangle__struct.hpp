// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Triangle.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TRIANGLE__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__TRIANGLE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Triangle __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Triangle __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Triangle_
{
  using Type = Triangle_<ContainerAllocator>;

  explicit Triangle_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->a = 0ul;
      this->b = 0ul;
      this->c = 0ul;
    }
  }

  explicit Triangle_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->a = 0ul;
      this->b = 0ul;
      this->c = 0ul;
    }
  }

  // field types and members
  using _a_type =
    uint32_t;
  _a_type a;
  using _b_type =
    uint32_t;
  _b_type b;
  using _c_type =
    uint32_t;
  _c_type c;

  // setters for named parameter idiom
  Type & set__a(
    const uint32_t & _arg)
  {
    this->a = _arg;
    return *this;
  }
  Type & set__b(
    const uint32_t & _arg)
  {
    this->b = _arg;
    return *this;
  }
  Type & set__c(
    const uint32_t & _arg)
  {
    this->c = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Triangle_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Triangle_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Triangle_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Triangle_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Triangle
    std::shared_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Triangle
    std::shared_ptr<iviz_msgs::msg::Triangle_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Triangle_ & other) const
  {
    if (this->a != other.a) {
      return false;
    }
    if (this->b != other.b) {
      return false;
    }
    if (this->c != other.c) {
      return false;
    }
    return true;
  }
  bool operator!=(const Triangle_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Triangle_

// alias to use template instance with default allocator
using Triangle =
  iviz_msgs::msg::Triangle_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__TRIANGLE__STRUCT_HPP_
