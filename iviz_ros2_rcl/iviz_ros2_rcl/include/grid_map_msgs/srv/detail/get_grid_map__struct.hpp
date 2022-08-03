// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from grid_map_msgs:srv/GetGridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__STRUCT_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__srv__GetGridMap_Request __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__srv__GetGridMap_Request __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetGridMap_Request_
{
  using Type = GetGridMap_Request_<ContainerAllocator>;

  explicit GetGridMap_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->frame_id = "";
      this->position_x = 0.0;
      this->position_y = 0.0;
      this->length_x = 0.0;
      this->length_y = 0.0;
    }
  }

  explicit GetGridMap_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : frame_id(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->frame_id = "";
      this->position_x = 0.0;
      this->position_y = 0.0;
      this->length_x = 0.0;
      this->length_y = 0.0;
    }
  }

  // field types and members
  using _frame_id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _frame_id_type frame_id;
  using _position_x_type =
    double;
  _position_x_type position_x;
  using _position_y_type =
    double;
  _position_y_type position_y;
  using _length_x_type =
    double;
  _length_x_type length_x;
  using _length_y_type =
    double;
  _length_y_type length_y;
  using _layers_type =
    std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other>;
  _layers_type layers;

  // setters for named parameter idiom
  Type & set__frame_id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->frame_id = _arg;
    return *this;
  }
  Type & set__position_x(
    const double & _arg)
  {
    this->position_x = _arg;
    return *this;
  }
  Type & set__position_y(
    const double & _arg)
  {
    this->position_y = _arg;
    return *this;
  }
  Type & set__length_x(
    const double & _arg)
  {
    this->length_x = _arg;
    return *this;
  }
  Type & set__length_y(
    const double & _arg)
  {
    this->length_y = _arg;
    return *this;
  }
  Type & set__layers(
    const std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other> & _arg)
  {
    this->layers = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__srv__GetGridMap_Request
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__srv__GetGridMap_Request
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetGridMap_Request_ & other) const
  {
    if (this->frame_id != other.frame_id) {
      return false;
    }
    if (this->position_x != other.position_x) {
      return false;
    }
    if (this->position_y != other.position_y) {
      return false;
    }
    if (this->length_x != other.length_x) {
      return false;
    }
    if (this->length_y != other.length_y) {
      return false;
    }
    if (this->layers != other.layers) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetGridMap_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetGridMap_Request_

// alias to use template instance with default allocator
using GetGridMap_Request =
  grid_map_msgs::srv::GetGridMap_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace grid_map_msgs


// Include directives for member types
// Member 'map'
#include "grid_map_msgs/msg/detail/grid_map__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__srv__GetGridMap_Response __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__srv__GetGridMap_Response __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetGridMap_Response_
{
  using Type = GetGridMap_Response_<ContainerAllocator>;

  explicit GetGridMap_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : map(_init)
  {
    (void)_init;
  }

  explicit GetGridMap_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
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
    grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__srv__GetGridMap_Response
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__srv__GetGridMap_Response
    std::shared_ptr<grid_map_msgs::srv::GetGridMap_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetGridMap_Response_ & other) const
  {
    if (this->map != other.map) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetGridMap_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetGridMap_Response_

// alias to use template instance with default allocator
using GetGridMap_Response =
  grid_map_msgs::srv::GetGridMap_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace grid_map_msgs

namespace grid_map_msgs
{

namespace srv
{

struct GetGridMap
{
  using Request = grid_map_msgs::srv::GetGridMap_Request;
  using Response = grid_map_msgs::srv::GetGridMap_Response;
};

}  // namespace srv

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__SRV__DETAIL__GET_GRID_MAP__STRUCT_HPP_
