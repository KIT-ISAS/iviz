// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:srv/LaunchDialog.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__STRUCT_HPP_
#define IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'dialog'
#include "iviz_msgs/msg/detail/dialog__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__LaunchDialog_Request __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__LaunchDialog_Request __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct LaunchDialog_Request_
{
  using Type = LaunchDialog_Request_<ContainerAllocator>;

  explicit LaunchDialog_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : dialog(_init)
  {
    (void)_init;
  }

  explicit LaunchDialog_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : dialog(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _dialog_type =
    iviz_msgs::msg::Dialog_<ContainerAllocator>;
  _dialog_type dialog;

  // setters for named parameter idiom
  Type & set__dialog(
    const iviz_msgs::msg::Dialog_<ContainerAllocator> & _arg)
  {
    this->dialog = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__LaunchDialog_Request
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__LaunchDialog_Request
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const LaunchDialog_Request_ & other) const
  {
    if (this->dialog != other.dialog) {
      return false;
    }
    return true;
  }
  bool operator!=(const LaunchDialog_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct LaunchDialog_Request_

// alias to use template instance with default allocator
using LaunchDialog_Request =
  iviz_msgs::srv::LaunchDialog_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs


// Include directives for member types
// Member 'feedback'
#include "iviz_msgs/msg/detail/feedback__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__LaunchDialog_Response __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__LaunchDialog_Response __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct LaunchDialog_Response_
{
  using Type = LaunchDialog_Response_<ContainerAllocator>;

  explicit LaunchDialog_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : feedback(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
    }
  }

  explicit LaunchDialog_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : message(_alloc),
    feedback(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;
  using _message_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _message_type message;
  using _feedback_type =
    iviz_msgs::msg::Feedback_<ContainerAllocator>;
  _feedback_type feedback;

  // setters for named parameter idiom
  Type & set__success(
    const bool & _arg)
  {
    this->success = _arg;
    return *this;
  }
  Type & set__message(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->message = _arg;
    return *this;
  }
  Type & set__feedback(
    const iviz_msgs::msg::Feedback_<ContainerAllocator> & _arg)
  {
    this->feedback = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__LaunchDialog_Response
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__LaunchDialog_Response
    std::shared_ptr<iviz_msgs::srv::LaunchDialog_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const LaunchDialog_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->message != other.message) {
      return false;
    }
    if (this->feedback != other.feedback) {
      return false;
    }
    return true;
  }
  bool operator!=(const LaunchDialog_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct LaunchDialog_Response_

// alias to use template instance with default allocator
using LaunchDialog_Response =
  iviz_msgs::srv::LaunchDialog_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs

namespace iviz_msgs
{

namespace srv
{

struct LaunchDialog
{
  using Request = iviz_msgs::srv::LaunchDialog_Request;
  using Response = iviz_msgs::srv::LaunchDialog_Response;
};

}  // namespace srv

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__LAUNCH_DIALOG__STRUCT_HPP_
