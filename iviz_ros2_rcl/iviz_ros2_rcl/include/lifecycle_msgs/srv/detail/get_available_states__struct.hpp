// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from lifecycle_msgs:srv/GetAvailableStates.idl
// generated code does not contain a copyright notice

#ifndef LIFECYCLE_MSGS__SRV__DETAIL__GET_AVAILABLE_STATES__STRUCT_HPP_
#define LIFECYCLE_MSGS__SRV__DETAIL__GET_AVAILABLE_STATES__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Request __attribute__((deprecated))
#else
# define DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Request __declspec(deprecated)
#endif

namespace lifecycle_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetAvailableStates_Request_
{
  using Type = GetAvailableStates_Request_<ContainerAllocator>;

  explicit GetAvailableStates_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->structure_needs_at_least_one_member = 0;
    }
  }

  explicit GetAvailableStates_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
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
    lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Request
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Request
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetAvailableStates_Request_ & other) const
  {
    if (this->structure_needs_at_least_one_member != other.structure_needs_at_least_one_member) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetAvailableStates_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetAvailableStates_Request_

// alias to use template instance with default allocator
using GetAvailableStates_Request =
  lifecycle_msgs::srv::GetAvailableStates_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace lifecycle_msgs


// Include directives for member types
// Member 'available_states'
#include "lifecycle_msgs/msg/detail/state__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Response __attribute__((deprecated))
#else
# define DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Response __declspec(deprecated)
#endif

namespace lifecycle_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetAvailableStates_Response_
{
  using Type = GetAvailableStates_Response_<ContainerAllocator>;

  explicit GetAvailableStates_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
  }

  explicit GetAvailableStates_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
    (void)_alloc;
  }

  // field types and members
  using _available_states_type =
    std::vector<lifecycle_msgs::msg::State_<ContainerAllocator>, typename ContainerAllocator::template rebind<lifecycle_msgs::msg::State_<ContainerAllocator>>::other>;
  _available_states_type available_states;

  // setters for named parameter idiom
  Type & set__available_states(
    const std::vector<lifecycle_msgs::msg::State_<ContainerAllocator>, typename ContainerAllocator::template rebind<lifecycle_msgs::msg::State_<ContainerAllocator>>::other> & _arg)
  {
    this->available_states = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Response
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__lifecycle_msgs__srv__GetAvailableStates_Response
    std::shared_ptr<lifecycle_msgs::srv::GetAvailableStates_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetAvailableStates_Response_ & other) const
  {
    if (this->available_states != other.available_states) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetAvailableStates_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetAvailableStates_Response_

// alias to use template instance with default allocator
using GetAvailableStates_Response =
  lifecycle_msgs::srv::GetAvailableStates_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace lifecycle_msgs

namespace lifecycle_msgs
{

namespace srv
{

struct GetAvailableStates
{
  using Request = lifecycle_msgs::srv::GetAvailableStates_Request;
  using Response = lifecycle_msgs::srv::GetAvailableStates_Response;
};

}  // namespace srv

}  // namespace lifecycle_msgs

#endif  // LIFECYCLE_MSGS__SRV__DETAIL__GET_AVAILABLE_STATES__STRUCT_HPP_
