// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from lifecycle_msgs:srv/GetState.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__SRV__DETAIL__GET_STATE__STRUCT_HPP_
#define LIFECYCLE_MSGS__SRV__DETAIL__GET_STATE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__lifecycle_msgs__srv__GetState_Request __attribute__((deprecated))
#else
# define DEPRECATED__lifecycle_msgs__srv__GetState_Request __declspec(deprecated)
#endif

namespace lifecycle_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetState_Request_
{
  using Type = GetState_Request_<ContainerAllocator>;

  explicit GetState_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->structure_needs_at_least_one_member = 0;
    }
  }

  explicit GetState_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->structure_needs_at_least_one_member = 0;
    }
  }

  // field types and members
  using _structure_needs_at_least_one_member_type =
    uint8_t;
  _structure_needs_at_least_one_member_type structure_needs_at_least_one_member;


  // constant declarations

  // pointer types
  using RawPtr =
    lifecycle_msgs::srv::GetState_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const lifecycle_msgs::srv::GetState_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetState_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetState_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__lifecycle_msgs__srv__GetState_Request
    std::shared_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__lifecycle_msgs__srv__GetState_Request
    std::shared_ptr<lifecycle_msgs::srv::GetState_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetState_Request_ & other) const
  {
    if (this->structure_needs_at_least_one_member != other.structure_needs_at_least_one_member) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetState_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetState_Request_

// alias to use template instance with default allocator
using GetState_Request =
  lifecycle_msgs::srv::GetState_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace lifecycle_msgs


// Include directives for member types
// Member 'current_state'
#include "lifecycle_msgs/msg/detail/state__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__lifecycle_msgs__srv__GetState_Response __attribute__((deprecated))
#else
# define DEPRECATED__lifecycle_msgs__srv__GetState_Response __declspec(deprecated)
#endif

namespace lifecycle_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetState_Response_
{
  using Type = GetState_Response_<ContainerAllocator>;

  explicit GetState_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : current_state(_init)
  {
    (void)_init;
  }

  explicit GetState_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : current_state(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _current_state_type =
    lifecycle_msgs::msg::State_<ContainerAllocator>;
  _current_state_type current_state;

  // setters for named parameter idiom
  Type & set__current_state(
    const lifecycle_msgs::msg::State_<ContainerAllocator> & _arg)
  {
    this->current_state = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    lifecycle_msgs::srv::GetState_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const lifecycle_msgs::srv::GetState_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetState_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetState_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__lifecycle_msgs__srv__GetState_Response
    std::shared_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__lifecycle_msgs__srv__GetState_Response
    std::shared_ptr<lifecycle_msgs::srv::GetState_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetState_Response_ & other) const
  {
    if (this->current_state != other.current_state) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetState_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetState_Response_

// alias to use template instance with default allocator
using GetState_Response =
  lifecycle_msgs::srv::GetState_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace lifecycle_msgs

namespace lifecycle_msgs
{

namespace srv
{

struct GetState
{
  using Request = lifecycle_msgs::srv::GetState_Request;
  using Response = lifecycle_msgs::srv::GetState_Response;
};

}  // namespace srv

}  // namespace lifecycle_msgs

#endif  // LIFECYCLE_MSGS__SRV__DETAIL__GET_STATE__STRUCT_HPP_
