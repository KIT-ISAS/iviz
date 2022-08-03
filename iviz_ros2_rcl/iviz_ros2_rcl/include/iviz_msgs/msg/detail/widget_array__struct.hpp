// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/WidgetArray.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'dialogs'
#include "iviz_msgs/msg/detail/dialog__struct.hpp"
// Member 'widgets'
#include "iviz_msgs/msg/detail/widget__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__WidgetArray __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__WidgetArray __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct WidgetArray_
{
  using Type = WidgetArray_<ContainerAllocator>;

  explicit WidgetArray_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
  }

  explicit WidgetArray_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
    (void)_alloc;
  }

  // field types and members
  using _dialogs_type =
    std::vector<iviz_msgs::msg::Dialog_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Dialog_<ContainerAllocator>>::other>;
  _dialogs_type dialogs;
  using _widgets_type =
    std::vector<iviz_msgs::msg::Widget_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Widget_<ContainerAllocator>>::other>;
  _widgets_type widgets;

  // setters for named parameter idiom
  Type & set__dialogs(
    const std::vector<iviz_msgs::msg::Dialog_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Dialog_<ContainerAllocator>>::other> & _arg)
  {
    this->dialogs = _arg;
    return *this;
  }
  Type & set__widgets(
    const std::vector<iviz_msgs::msg::Widget_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Widget_<ContainerAllocator>>::other> & _arg)
  {
    this->widgets = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::WidgetArray_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::WidgetArray_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::WidgetArray_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::WidgetArray_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__WidgetArray
    std::shared_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__WidgetArray
    std::shared_ptr<iviz_msgs::msg::WidgetArray_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const WidgetArray_ & other) const
  {
    if (this->dialogs != other.dialogs) {
      return false;
    }
    if (this->widgets != other.widgets) {
      return false;
    }
    return true;
  }
  bool operator!=(const WidgetArray_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct WidgetArray_

// alias to use template instance with default allocator
using WidgetArray =
  iviz_msgs::msg::WidgetArray_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__WIDGET_ARRAY__STRUCT_HPP_
