// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from composition_interfaces:srv/ListNodes.idl
// generated code does not contain a copyright notice

#ifndef COMPOSITION_INTERFACES__SRV__DETAIL__LIST_NODES__STRUCT_HPP_
#define COMPOSITION_INTERFACES__SRV__DETAIL__LIST_NODES__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__composition_interfaces__srv__ListNodes_Request __attribute__((deprecated))
#else
# define DEPRECATED__composition_interfaces__srv__ListNodes_Request __declspec(deprecated)
#endif

namespace composition_interfaces
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct ListNodes_Request_
{
  using Type = ListNodes_Request_<ContainerAllocator>;

  explicit ListNodes_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->structure_needs_at_least_one_member = 0;
    }
  }

  explicit ListNodes_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
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
    composition_interfaces::srv::ListNodes_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const composition_interfaces::srv::ListNodes_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::ListNodes_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::ListNodes_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__composition_interfaces__srv__ListNodes_Request
    std::shared_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__composition_interfaces__srv__ListNodes_Request
    std::shared_ptr<composition_interfaces::srv::ListNodes_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ListNodes_Request_ & other) const
  {
    if (this->structure_needs_at_least_one_member != other.structure_needs_at_least_one_member) {
      return false;
    }
    return true;
  }
  bool operator!=(const ListNodes_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ListNodes_Request_

// alias to use template instance with default allocator
using ListNodes_Request =
  composition_interfaces::srv::ListNodes_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace composition_interfaces


#ifndef _WIN32
# define DEPRECATED__composition_interfaces__srv__ListNodes_Response __attribute__((deprecated))
#else
# define DEPRECATED__composition_interfaces__srv__ListNodes_Response __declspec(deprecated)
#endif

namespace composition_interfaces
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct ListNodes_Response_
{
  using Type = ListNodes_Response_<ContainerAllocator>;

  explicit ListNodes_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
  }

  explicit ListNodes_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_init;
    (void)_alloc;
  }

  // field types and members
  using _full_node_names_type =
    std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other>;
  _full_node_names_type full_node_names;
  using _unique_ids_type =
    std::vector<uint64_t, typename ContainerAllocator::template rebind<uint64_t>::other>;
  _unique_ids_type unique_ids;

  // setters for named parameter idiom
  Type & set__full_node_names(
    const std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other> & _arg)
  {
    this->full_node_names = _arg;
    return *this;
  }
  Type & set__unique_ids(
    const std::vector<uint64_t, typename ContainerAllocator::template rebind<uint64_t>::other> & _arg)
  {
    this->unique_ids = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    composition_interfaces::srv::ListNodes_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const composition_interfaces::srv::ListNodes_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::ListNodes_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::ListNodes_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__composition_interfaces__srv__ListNodes_Response
    std::shared_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__composition_interfaces__srv__ListNodes_Response
    std::shared_ptr<composition_interfaces::srv::ListNodes_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ListNodes_Response_ & other) const
  {
    if (this->full_node_names != other.full_node_names) {
      return false;
    }
    if (this->unique_ids != other.unique_ids) {
      return false;
    }
    return true;
  }
  bool operator!=(const ListNodes_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ListNodes_Response_

// alias to use template instance with default allocator
using ListNodes_Response =
  composition_interfaces::srv::ListNodes_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace composition_interfaces

namespace composition_interfaces
{

namespace srv
{

struct ListNodes
{
  using Request = composition_interfaces::srv::ListNodes_Request;
  using Response = composition_interfaces::srv::ListNodes_Response;
};

}  // namespace srv

}  // namespace composition_interfaces

#endif  // COMPOSITION_INTERFACES__SRV__DETAIL__LIST_NODES__STRUCT_HPP_
