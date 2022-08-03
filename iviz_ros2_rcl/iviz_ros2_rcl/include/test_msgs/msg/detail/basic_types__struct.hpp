// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from test_msgs:msg/BasicTypes.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__BASIC_TYPES__STRUCT_HPP_
#define TEST_MSGS__MSG__DETAIL__BASIC_TYPES__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__test_msgs__msg__BasicTypes __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__msg__BasicTypes __declspec(deprecated)
#endif

namespace test_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct BasicTypes_
{
  using Type = BasicTypes_<ContainerAllocator>;

  explicit BasicTypes_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->bool_value = false;
      this->byte_value = 0;
      this->char_value = 0;
      this->float32_value = 0.0f;
      this->float64_value = 0.0;
      this->int8_value = 0;
      this->uint8_value = 0;
      this->int16_value = 0;
      this->uint16_value = 0;
      this->int32_value = 0l;
      this->uint32_value = 0ul;
      this->int64_value = 0ll;
      this->uint64_value = 0ull;
    }
  }

  explicit BasicTypes_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->bool_value = false;
      this->byte_value = 0;
      this->char_value = 0;
      this->float32_value = 0.0f;
      this->float64_value = 0.0;
      this->int8_value = 0;
      this->uint8_value = 0;
      this->int16_value = 0;
      this->uint16_value = 0;
      this->int32_value = 0l;
      this->uint32_value = 0ul;
      this->int64_value = 0ll;
      this->uint64_value = 0ull;
    }
  }

  // field types and members
  using _bool_value_type =
    bool;
  _bool_value_type bool_value;
  using _byte_value_type =
    unsigned char;
  _byte_value_type byte_value;
  using _char_value_type =
    uint8_t;
  _char_value_type char_value;
  using _float32_value_type =
    float;
  _float32_value_type float32_value;
  using _float64_value_type =
    double;
  _float64_value_type float64_value;
  using _int8_value_type =
    int8_t;
  _int8_value_type int8_value;
  using _uint8_value_type =
    uint8_t;
  _uint8_value_type uint8_value;
  using _int16_value_type =
    int16_t;
  _int16_value_type int16_value;
  using _uint16_value_type =
    uint16_t;
  _uint16_value_type uint16_value;
  using _int32_value_type =
    int32_t;
  _int32_value_type int32_value;
  using _uint32_value_type =
    uint32_t;
  _uint32_value_type uint32_value;
  using _int64_value_type =
    int64_t;
  _int64_value_type int64_value;
  using _uint64_value_type =
    uint64_t;
  _uint64_value_type uint64_value;

  // setters for named parameter idiom
  Type & set__bool_value(
    const bool & _arg)
  {
    this->bool_value = _arg;
    return *this;
  }
  Type & set__byte_value(
    const unsigned char & _arg)
  {
    this->byte_value = _arg;
    return *this;
  }
  Type & set__char_value(
    const uint8_t & _arg)
  {
    this->char_value = _arg;
    return *this;
  }
  Type & set__float32_value(
    const float & _arg)
  {
    this->float32_value = _arg;
    return *this;
  }
  Type & set__float64_value(
    const double & _arg)
  {
    this->float64_value = _arg;
    return *this;
  }
  Type & set__int8_value(
    const int8_t & _arg)
  {
    this->int8_value = _arg;
    return *this;
  }
  Type & set__uint8_value(
    const uint8_t & _arg)
  {
    this->uint8_value = _arg;
    return *this;
  }
  Type & set__int16_value(
    const int16_t & _arg)
  {
    this->int16_value = _arg;
    return *this;
  }
  Type & set__uint16_value(
    const uint16_t & _arg)
  {
    this->uint16_value = _arg;
    return *this;
  }
  Type & set__int32_value(
    const int32_t & _arg)
  {
    this->int32_value = _arg;
    return *this;
  }
  Type & set__uint32_value(
    const uint32_t & _arg)
  {
    this->uint32_value = _arg;
    return *this;
  }
  Type & set__int64_value(
    const int64_t & _arg)
  {
    this->int64_value = _arg;
    return *this;
  }
  Type & set__uint64_value(
    const uint64_t & _arg)
  {
    this->uint64_value = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::msg::BasicTypes_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::msg::BasicTypes_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::BasicTypes_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::BasicTypes_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__msg__BasicTypes
    std::shared_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__msg__BasicTypes
    std::shared_ptr<test_msgs::msg::BasicTypes_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const BasicTypes_ & other) const
  {
    if (this->bool_value != other.bool_value) {
      return false;
    }
    if (this->byte_value != other.byte_value) {
      return false;
    }
    if (this->char_value != other.char_value) {
      return false;
    }
    if (this->float32_value != other.float32_value) {
      return false;
    }
    if (this->float64_value != other.float64_value) {
      return false;
    }
    if (this->int8_value != other.int8_value) {
      return false;
    }
    if (this->uint8_value != other.uint8_value) {
      return false;
    }
    if (this->int16_value != other.int16_value) {
      return false;
    }
    if (this->uint16_value != other.uint16_value) {
      return false;
    }
    if (this->int32_value != other.int32_value) {
      return false;
    }
    if (this->uint32_value != other.uint32_value) {
      return false;
    }
    if (this->int64_value != other.int64_value) {
      return false;
    }
    if (this->uint64_value != other.uint64_value) {
      return false;
    }
    return true;
  }
  bool operator!=(const BasicTypes_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct BasicTypes_

// alias to use template instance with default allocator
using BasicTypes =
  test_msgs::msg::BasicTypes_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__BASIC_TYPES__STRUCT_HPP_
