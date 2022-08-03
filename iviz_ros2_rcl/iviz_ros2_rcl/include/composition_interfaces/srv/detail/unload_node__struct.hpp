// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from composition_interfaces:srv/UnloadNode.idl
// generated code does not contain a copyright notice

#ifndef COMPOSITION_INTERFACES__SRV__DETAIL__UNLOAD_NODE__STRUCT_HPP_
#define COMPOSITION_INTERFACES__SRV__DETAIL__UNLOAD_NODE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__composition_interfaces__srv__UnloadNode_Request __attribute__((deprecated))
#else
# define DEPRECATED__composition_interfaces__srv__UnloadNode_Request __declspec(deprecated)
#endif

namespace composition_interfaces
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct UnloadNode_Request_
{
  using Type = UnloadNode_Request_<ContainerAllocator>;

  explicit UnloadNode_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->unique_id = 0ull;
    }
  }

  explicit UnloadNode_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->unique_id = 0ull;
    }
  }

  // field types and members
  using _unique_id_type =
    uint64_t;
  _unique_id_type unique_id;

  // setters for named parameter idiom
  Type & set__unique_id(
    const uint64_t & _arg)
  {
    this->unique_id = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__composition_interfaces__srv__UnloadNode_Request
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__composition_interfaces__srv__UnloadNode_Request
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const UnloadNode_Request_ & other) const
  {
    if (this->unique_id != other.unique_id) {
      return false;
    }
    return true;
  }
  bool operator!=(const UnloadNode_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct UnloadNode_Request_

// alias to use template instance with default allocator
using UnloadNode_Request =
  composition_interfaces::srv::UnloadNode_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace composition_interfaces


#ifndef _WIN32
# define DEPRECATED__composition_interfaces__srv__UnloadNode_Response __attribute__((deprecated))
#else
# define DEPRECATED__composition_interfaces__srv__UnloadNode_Response __declspec(deprecated)
#endif

namespace composition_interfaces
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct UnloadNode_Response_
{
  using Type = UnloadNode_Response_<ContainerAllocator>;

  explicit UnloadNode_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->error_message = "";
    }
  }

  explicit UnloadNode_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : error_message(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->error_message = "";
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;
  using _error_message_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _error_message_type error_message;

  // setters for named parameter idiom
  Type & set__success(
    const bool & _arg)
  {
    this->success = _arg;
    return *this;
  }
  Type & set__error_message(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->error_message = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__composition_interfaces__srv__UnloadNode_Response
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__composition_interfaces__srv__UnloadNode_Response
    std::shared_ptr<composition_interfaces::srv::UnloadNode_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const UnloadNode_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->error_message != other.error_message) {
      return false;
    }
    return true;
  }
  bool operator!=(const UnloadNode_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct UnloadNode_Response_

// alias to use template instance with default allocator
using UnloadNode_Response =
  composition_interfaces::srv::UnloadNode_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace composition_interfaces

namespace composition_interfaces
{

namespace srv
{

struct UnloadNode
{
  using Request = composition_interfaces::srv::UnloadNode_Request;
  using Response = composition_interfaces::srv::UnloadNode_Response;
};

}  // namespace srv

}  // namespace composition_interfaces

#endif  // COMPOSITION_INTERFACES__SRV__DETAIL__UNLOAD_NODE__STRUCT_HPP_
