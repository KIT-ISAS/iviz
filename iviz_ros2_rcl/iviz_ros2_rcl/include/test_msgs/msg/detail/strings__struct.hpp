// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from test_msgs:msg/Strings.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__STRINGS__STRUCT_HPP_
#define TEST_MSGS__MSG__DETAIL__STRINGS__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__test_msgs__msg__Strings __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__msg__Strings __declspec(deprecated)
#endif

namespace test_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Strings_
{
  using Type = Strings_<ContainerAllocator>;

  explicit Strings_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::DEFAULTS_ONLY == _init)
    {
      this->string_value_default1 = "Hello world!";
      this->string_value_default2 = "Hello'world!";
      this->string_value_default3 = "Hello\"world!";
      this->string_value_default4 = "Hello'world!";
      this->string_value_default5 = "Hello\"world!";
      this->bounded_string_value_default1 = "Hello world!";
      this->bounded_string_value_default2 = "Hello'world!";
      this->bounded_string_value_default3 = "Hello\"world!";
      this->bounded_string_value_default4 = "Hello'world!";
      this->bounded_string_value_default5 = "Hello\"world!";
    } else if (rosidl_runtime_cpp::MessageInitialization::ZERO == _init) {
      this->string_value = "";
      this->string_value_default1 = "";
      this->string_value_default2 = "";
      this->string_value_default3 = "";
      this->string_value_default4 = "";
      this->string_value_default5 = "";
      this->bounded_string_value = "";
      this->bounded_string_value_default1 = "";
      this->bounded_string_value_default2 = "";
      this->bounded_string_value_default3 = "";
      this->bounded_string_value_default4 = "";
      this->bounded_string_value_default5 = "";
    }
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->string_value = "";
      this->bounded_string_value = "";
    }
  }

  explicit Strings_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : string_value(_alloc),
    string_value_default1(_alloc),
    string_value_default2(_alloc),
    string_value_default3(_alloc),
    string_value_default4(_alloc),
    string_value_default5(_alloc),
    bounded_string_value(_alloc),
    bounded_string_value_default1(_alloc),
    bounded_string_value_default2(_alloc),
    bounded_string_value_default3(_alloc),
    bounded_string_value_default4(_alloc),
    bounded_string_value_default5(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::DEFAULTS_ONLY == _init)
    {
      this->string_value_default1 = "Hello world!";
      this->string_value_default2 = "Hello'world!";
      this->string_value_default3 = "Hello\"world!";
      this->string_value_default4 = "Hello'world!";
      this->string_value_default5 = "Hello\"world!";
      this->bounded_string_value_default1 = "Hello world!";
      this->bounded_string_value_default2 = "Hello'world!";
      this->bounded_string_value_default3 = "Hello\"world!";
      this->bounded_string_value_default4 = "Hello'world!";
      this->bounded_string_value_default5 = "Hello\"world!";
    } else if (rosidl_runtime_cpp::MessageInitialization::ZERO == _init) {
      this->string_value = "";
      this->string_value_default1 = "";
      this->string_value_default2 = "";
      this->string_value_default3 = "";
      this->string_value_default4 = "";
      this->string_value_default5 = "";
      this->bounded_string_value = "";
      this->bounded_string_value_default1 = "";
      this->bounded_string_value_default2 = "";
      this->bounded_string_value_default3 = "";
      this->bounded_string_value_default4 = "";
      this->bounded_string_value_default5 = "";
    }
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->string_value = "";
      this->bounded_string_value = "";
    }
  }

  // field types and members
  using _string_value_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _string_value_type string_value;
  using _string_value_default1_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _string_value_default1_type string_value_default1;
  using _string_value_default2_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _string_value_default2_type string_value_default2;
  using _string_value_default3_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _string_value_default3_type string_value_default3;
  using _string_value_default4_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _string_value_default4_type string_value_default4;
  using _string_value_default5_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _string_value_default5_type string_value_default5;
  using _bounded_string_value_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _bounded_string_value_type bounded_string_value;
  using _bounded_string_value_default1_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _bounded_string_value_default1_type bounded_string_value_default1;
  using _bounded_string_value_default2_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _bounded_string_value_default2_type bounded_string_value_default2;
  using _bounded_string_value_default3_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _bounded_string_value_default3_type bounded_string_value_default3;
  using _bounded_string_value_default4_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _bounded_string_value_default4_type bounded_string_value_default4;
  using _bounded_string_value_default5_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _bounded_string_value_default5_type bounded_string_value_default5;

  // setters for named parameter idiom
  Type & set__string_value(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->string_value = _arg;
    return *this;
  }
  Type & set__string_value_default1(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->string_value_default1 = _arg;
    return *this;
  }
  Type & set__string_value_default2(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->string_value_default2 = _arg;
    return *this;
  }
  Type & set__string_value_default3(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->string_value_default3 = _arg;
    return *this;
  }
  Type & set__string_value_default4(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->string_value_default4 = _arg;
    return *this;
  }
  Type & set__string_value_default5(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->string_value_default5 = _arg;
    return *this;
  }
  Type & set__bounded_string_value(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->bounded_string_value = _arg;
    return *this;
  }
  Type & set__bounded_string_value_default1(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->bounded_string_value_default1 = _arg;
    return *this;
  }
  Type & set__bounded_string_value_default2(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->bounded_string_value_default2 = _arg;
    return *this;
  }
  Type & set__bounded_string_value_default3(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->bounded_string_value_default3 = _arg;
    return *this;
  }
  Type & set__bounded_string_value_default4(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->bounded_string_value_default4 = _arg;
    return *this;
  }
  Type & set__bounded_string_value_default5(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->bounded_string_value_default5 = _arg;
    return *this;
  }

  // constant declarations
  static const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> STRING_CONST;

  // pointer types
  using RawPtr =
    test_msgs::msg::Strings_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::msg::Strings_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::msg::Strings_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::msg::Strings_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::Strings_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::Strings_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::Strings_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::Strings_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::msg::Strings_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::msg::Strings_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__msg__Strings
    std::shared_ptr<test_msgs::msg::Strings_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__msg__Strings
    std::shared_ptr<test_msgs::msg::Strings_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Strings_ & other) const
  {
    if (this->string_value != other.string_value) {
      return false;
    }
    if (this->string_value_default1 != other.string_value_default1) {
      return false;
    }
    if (this->string_value_default2 != other.string_value_default2) {
      return false;
    }
    if (this->string_value_default3 != other.string_value_default3) {
      return false;
    }
    if (this->string_value_default4 != other.string_value_default4) {
      return false;
    }
    if (this->string_value_default5 != other.string_value_default5) {
      return false;
    }
    if (this->bounded_string_value != other.bounded_string_value) {
      return false;
    }
    if (this->bounded_string_value_default1 != other.bounded_string_value_default1) {
      return false;
    }
    if (this->bounded_string_value_default2 != other.bounded_string_value_default2) {
      return false;
    }
    if (this->bounded_string_value_default3 != other.bounded_string_value_default3) {
      return false;
    }
    if (this->bounded_string_value_default4 != other.bounded_string_value_default4) {
      return false;
    }
    if (this->bounded_string_value_default5 != other.bounded_string_value_default5) {
      return false;
    }
    return true;
  }
  bool operator!=(const Strings_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Strings_

// alias to use template instance with default allocator
using Strings =
  test_msgs::msg::Strings_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>
Strings_<ContainerAllocator>::STRING_CONST = "Hello world!";

}  // namespace msg

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__STRINGS__STRUCT_HPP_
