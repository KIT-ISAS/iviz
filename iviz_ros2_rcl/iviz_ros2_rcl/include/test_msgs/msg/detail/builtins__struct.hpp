// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from test_msgs:msg/Builtins.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__BUILTINS__STRUCT_HPP_
#define TEST_MSGS__MSG__DETAIL__BUILTINS__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'duration_value'
#include "builtin_interfaces/msg/detail/duration__struct.hpp"
// Member 'time_value'
#include "builtin_interfaces/msg/detail/time__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__msg__Builtins __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__msg__Builtins __declspec(deprecated)
#endif

namespace test_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Builtins_
{
  using Type = Builtins_<ContainerAllocator>;

  explicit Builtins_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : duration_value(_init),
    time_value(_init)
  {
    (void)_init;
  }

  explicit Builtins_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : duration_value(_alloc, _init),
    time_value(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _duration_value_type =
    builtin_interfaces::msg::Duration_<ContainerAllocator>;
  _duration_value_type duration_value;
  using _time_value_type =
    builtin_interfaces::msg::Time_<ContainerAllocator>;
  _time_value_type time_value;

  // setters for named parameter idiom
  Type & set__duration_value(
    const builtin_interfaces::msg::Duration_<ContainerAllocator> & _arg)
  {
    this->duration_value = _arg;
    return *this;
  }
  Type & set__time_value(
    const builtin_interfaces::msg::Time_<ContainerAllocator> & _arg)
  {
    this->time_value = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::msg::Builtins_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::msg::Builtins_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::msg::Builtins_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::msg::Builtins_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::Builtins_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::Builtins_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::Builtins_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::Builtins_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::msg::Builtins_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::msg::Builtins_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__msg__Builtins
    std::shared_ptr<test_msgs::msg::Builtins_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__msg__Builtins
    std::shared_ptr<test_msgs::msg::Builtins_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Builtins_ & other) const
  {
    if (this->duration_value != other.duration_value) {
      return false;
    }
    if (this->time_value != other.time_value) {
      return false;
    }
    return true;
  }
  bool operator!=(const Builtins_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Builtins_

// alias to use template instance with default allocator
using Builtins =
  test_msgs::msg::Builtins_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__BUILTINS__STRUCT_HPP_
