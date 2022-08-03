// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from grid_map_msgs:srv/SetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__STRUCT_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'map'
#include "grid_map_msgs/msg/detail/grid_map__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__srv__SetGridMap_Request __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__srv__SetGridMap_Request __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct SetGridMap_Request_
{
  using Type = SetGridMap_Request_<ContainerAllocator>;

  explicit SetGridMap_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : map(_init)
  {
    (void)_init;
  }

  explicit SetGridMap_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : map(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _map_type =
    grid_map_msgs::msg::GridMap_<ContainerAllocator>;
  _map_type map;

  // setters for named parameter idiom
  Type & set__map(
    const grid_map_msgs::msg::GridMap_<ContainerAllocator> & _arg)
  {
    this->map = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__srv__SetGridMap_Request
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__srv__SetGridMap_Request
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const SetGridMap_Request_ & other) const
  {
    if (this->map != other.map) {
      return false;
    }
    return true;
  }
  bool operator!=(const SetGridMap_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct SetGridMap_Request_

// alias to use template instance with default allocator
using SetGridMap_Request =
  grid_map_msgs::srv::SetGridMap_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace grid_map_msgs


#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__srv__SetGridMap_Response __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__srv__SetGridMap_Response __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct SetGridMap_Response_
{
  using Type = SetGridMap_Response_<ContainerAllocator>;

  explicit SetGridMap_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->structure_needs_at_least_one_member = 0;
    }
  }

  explicit SetGridMap_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
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
    grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__srv__SetGridMap_Response
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__srv__SetGridMap_Response
    std::shared_ptr<grid_map_msgs::srv::SetGridMap_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const SetGridMap_Response_ & other) const
  {
    if (this->structure_needs_at_least_one_member != other.structure_needs_at_least_one_member) {
      return false;
    }
    return true;
  }
  bool operator!=(const SetGridMap_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct SetGridMap_Response_

// alias to use template instance with default allocator
using SetGridMap_Response =
  grid_map_msgs::srv::SetGridMap_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace grid_map_msgs

namespace grid_map_msgs
{

namespace srv
{

struct SetGridMap
{
  using Request = grid_map_msgs::srv::SetGridMap_Request;
  using Response = grid_map_msgs::srv::SetGridMap_Response;
};

}  // namespace srv

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__SRV__DETAIL__SET_GRID_MAP__STRUCT_HPP_
