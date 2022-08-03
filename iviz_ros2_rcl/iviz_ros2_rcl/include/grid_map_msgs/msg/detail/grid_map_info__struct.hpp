// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from grid_map_msgs:msg/GridMapInfo.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__STRUCT_HPP_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.hpp"
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__msg__GridMapInfo __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__msg__GridMapInfo __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct GridMapInfo_
{
  using Type = GridMapInfo_<ContainerAllocator>;

  explicit GridMapInfo_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_init),
    pose(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->resolution = 0.0;
      this->length_x = 0.0;
      this->length_y = 0.0;
    }
  }

  explicit GridMapInfo_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_alloc, _init),
    pose(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->resolution = 0.0;
      this->length_x = 0.0;
      this->length_y = 0.0;
    }
  }

  // field types and members
  using _header_type =
    std_msgs::msg::Header_<ContainerAllocator>;
  _header_type header;
  using _resolution_type =
    double;
  _resolution_type resolution;
  using _length_x_type =
    double;
  _length_x_type length_x;
  using _length_y_type =
    double;
  _length_y_type length_y;
  using _pose_type =
    geometry_msgs::msg::Pose_<ContainerAllocator>;
  _pose_type pose;

  // setters for named parameter idiom
  Type & set__header(
    const std_msgs::msg::Header_<ContainerAllocator> & _arg)
  {
    this->header = _arg;
    return *this;
  }
  Type & set__resolution(
    const double & _arg)
  {
    this->resolution = _arg;
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
  Type & set__pose(
    const geometry_msgs::msg::Pose_<ContainerAllocator> & _arg)
  {
    this->pose = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    grid_map_msgs::msg::GridMapInfo_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::msg::GridMapInfo_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::msg::GridMapInfo_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::msg::GridMapInfo_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__msg__GridMapInfo
    std::shared_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__msg__GridMapInfo
    std::shared_ptr<grid_map_msgs::msg::GridMapInfo_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GridMapInfo_ & other) const
  {
    if (this->header != other.header) {
      return false;
    }
    if (this->resolution != other.resolution) {
      return false;
    }
    if (this->length_x != other.length_x) {
      return false;
    }
    if (this->length_y != other.length_y) {
      return false;
    }
    if (this->pose != other.pose) {
      return false;
    }
    return true;
  }
  bool operator!=(const GridMapInfo_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GridMapInfo_

// alias to use template instance with default allocator
using GridMapInfo =
  grid_map_msgs::msg::GridMapInfo_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP_INFO__STRUCT_HPP_
