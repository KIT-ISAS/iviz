// NOLINT: This file starts with a BOM since it contain non-ASCII characters
// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from test_msgs:msg/WStrings.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__W_STRINGS__STRUCT_HPP_
#define TEST_MSGS__MSG__DETAIL__W_STRINGS__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__test_msgs__msg__WStrings __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__msg__WStrings __declspec(deprecated)
#endif

namespace test_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct WStrings_
{
  using Type = WStrings_<ContainerAllocator>;

  explicit WStrings_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::DEFAULTS_ONLY == _init)
    {
      this->wstring_value_default1 = u"Hello world!";
      this->wstring_value_default2 = u"Hellö wörld!";
      this->wstring_value_default3 = u"ハローワールド";
    } else if (rosidl_runtime_cpp::MessageInitialization::ZERO == _init) {
      this->wstring_value = u"";
      this->wstring_value_default1 = u"";
      this->wstring_value_default2 = u"";
      this->wstring_value_default3 = u"";
      std::fill<typename std::array<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3>::iterator, std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>(this->array_of_wstrings.begin(), this->array_of_wstrings.end(), u"");
    }
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->wstring_value = u"";
      std::fill<typename std::array<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3>::iterator, std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>(this->array_of_wstrings.begin(), this->array_of_wstrings.end(), u"");
    }
  }

  explicit WStrings_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : wstring_value(_alloc),
    wstring_value_default1(_alloc),
    wstring_value_default2(_alloc),
    wstring_value_default3(_alloc),
    array_of_wstrings(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::DEFAULTS_ONLY == _init)
    {
      this->wstring_value_default1 = u"Hello world!";
      this->wstring_value_default2 = u"Hellö wörld!";
      this->wstring_value_default3 = u"ハローワールド";
    } else if (rosidl_runtime_cpp::MessageInitialization::ZERO == _init) {
      this->wstring_value = u"";
      this->wstring_value_default1 = u"";
      this->wstring_value_default2 = u"";
      this->wstring_value_default3 = u"";
      std::fill<typename std::array<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3>::iterator, std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>(this->array_of_wstrings.begin(), this->array_of_wstrings.end(), u"");
    }
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->wstring_value = u"";
      std::fill<typename std::array<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3>::iterator, std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>(this->array_of_wstrings.begin(), this->array_of_wstrings.end(), u"");
    }
  }

  // field types and members
  using _wstring_value_type =
    std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>;
  _wstring_value_type wstring_value;
  using _wstring_value_default1_type =
    std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>;
  _wstring_value_default1_type wstring_value_default1;
  using _wstring_value_default2_type =
    std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>;
  _wstring_value_default2_type wstring_value_default2;
  using _wstring_value_default3_type =
    std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>;
  _wstring_value_default3_type wstring_value_default3;
  using _array_of_wstrings_type =
    std::array<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3>;
  _array_of_wstrings_type array_of_wstrings;
  using _bounded_sequence_of_wstrings_type =
    rosidl_runtime_cpp::BoundedVector<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3, typename ContainerAllocator::template rebind<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>::other>;
  _bounded_sequence_of_wstrings_type bounded_sequence_of_wstrings;
  using _unbounded_sequence_of_wstrings_type =
    std::vector<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, typename ContainerAllocator::template rebind<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>::other>;
  _unbounded_sequence_of_wstrings_type unbounded_sequence_of_wstrings;

  // setters for named parameter idiom
  Type & set__wstring_value(
    const std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other> & _arg)
  {
    this->wstring_value = _arg;
    return *this;
  }
  Type & set__wstring_value_default1(
    const std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other> & _arg)
  {
    this->wstring_value_default1 = _arg;
    return *this;
  }
  Type & set__wstring_value_default2(
    const std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other> & _arg)
  {
    this->wstring_value_default2 = _arg;
    return *this;
  }
  Type & set__wstring_value_default3(
    const std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other> & _arg)
  {
    this->wstring_value_default3 = _arg;
    return *this;
  }
  Type & set__array_of_wstrings(
    const std::array<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3> & _arg)
  {
    this->array_of_wstrings = _arg;
    return *this;
  }
  Type & set__bounded_sequence_of_wstrings(
    const rosidl_runtime_cpp::BoundedVector<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, 3, typename ContainerAllocator::template rebind<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>::other> & _arg)
  {
    this->bounded_sequence_of_wstrings = _arg;
    return *this;
  }
  Type & set__unbounded_sequence_of_wstrings(
    const std::vector<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>, typename ContainerAllocator::template rebind<std::basic_string<char16_t, std::char_traits<char16_t>, typename ContainerAllocator::template rebind<char16_t>::other>>::other> & _arg)
  {
    this->unbounded_sequence_of_wstrings = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::msg::WStrings_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::msg::WStrings_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::msg::WStrings_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::msg::WStrings_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::WStrings_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::WStrings_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::WStrings_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::WStrings_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::msg::WStrings_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::msg::WStrings_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__msg__WStrings
    std::shared_ptr<test_msgs::msg::WStrings_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__msg__WStrings
    std::shared_ptr<test_msgs::msg::WStrings_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const WStrings_ & other) const
  {
    if (this->wstring_value != other.wstring_value) {
      return false;
    }
    if (this->wstring_value_default1 != other.wstring_value_default1) {
      return false;
    }
    if (this->wstring_value_default2 != other.wstring_value_default2) {
      return false;
    }
    if (this->wstring_value_default3 != other.wstring_value_default3) {
      return false;
    }
    if (this->array_of_wstrings != other.array_of_wstrings) {
      return false;
    }
    if (this->bounded_sequence_of_wstrings != other.bounded_sequence_of_wstrings) {
      return false;
    }
    if (this->unbounded_sequence_of_wstrings != other.unbounded_sequence_of_wstrings) {
      return false;
    }
    return true;
  }
  bool operator!=(const WStrings_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct WStrings_

// alias to use template instance with default allocator
using WStrings =
  test_msgs::msg::WStrings_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__W_STRINGS__STRUCT_HPP_
