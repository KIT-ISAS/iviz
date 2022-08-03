// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from lifecycle_msgs:srv/ChangeState.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__STRUCT_HPP_
#define LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'transition'
#include "lifecycle_msgs/msg/detail/transition__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__lifecycle_msgs__srv__ChangeState_Request __attribute__((deprecated))
#else
# define DEPRECATED__lifecycle_msgs__srv__ChangeState_Request __declspec(deprecated)
#endif

namespace lifecycle_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct ChangeState_Request_
{
  using Type = ChangeState_Request_<ContainerAllocator>;

  explicit ChangeState_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : transition(_init)
  {
    (void)_init;
  }

  explicit ChangeState_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : transition(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _transition_type =
    lifecycle_msgs::msg::Transition_<ContainerAllocator>;
  _transition_type transition;

  // setters for named parameter idiom
  Type & set__transition(
    const lifecycle_msgs::msg::Transition_<ContainerAllocator> & _arg)
  {
    this->transition = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__lifecycle_msgs__srv__ChangeState_Request
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__lifecycle_msgs__srv__ChangeState_Request
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ChangeState_Request_ & other) const
  {
    if (this->transition != other.transition) {
      return false;
    }
    return true;
  }
  bool operator!=(const ChangeState_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ChangeState_Request_

// alias to use template instance with default allocator
using ChangeState_Request =
  lifecycle_msgs::srv::ChangeState_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace lifecycle_msgs


#ifndef _WIN32
# define DEPRECATED__lifecycle_msgs__srv__ChangeState_Response __attribute__((deprecated))
#else
# define DEPRECATED__lifecycle_msgs__srv__ChangeState_Response __declspec(deprecated)
#endif

namespace lifecycle_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct ChangeState_Response_
{
  using Type = ChangeState_Response_<ContainerAllocator>;

  explicit ChangeState_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
    }
  }

  explicit ChangeState_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;

  // setters for named parameter idiom
  Type & set__success(
    const bool & _arg)
  {
    this->success = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__lifecycle_msgs__srv__ChangeState_Response
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__lifecycle_msgs__srv__ChangeState_Response
    std::shared_ptr<lifecycle_msgs::srv::ChangeState_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ChangeState_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    return true;
  }
  bool operator!=(const ChangeState_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ChangeState_Response_

// alias to use template instance with default allocator
using ChangeState_Response =
  lifecycle_msgs::srv::ChangeState_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace lifecycle_msgs

namespace lifecycle_msgs
{

namespace srv
{

struct ChangeState
{
  using Request = lifecycle_msgs::srv::ChangeState_Request;
  using Response = lifecycle_msgs::srv::ChangeState_Response;
};

}  // namespace srv

}  // namespace lifecycle_msgs

#endif  // LIFECYCLE_MSGS__SRV__DETAIL__CHANGE_STATE__STRUCT_HPP_
