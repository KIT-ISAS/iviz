// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from grid_map_msgs:msg/GridMap.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__STRUCT_HPP_
#define GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'info'
#include "grid_map_msgs/msg/detail/grid_map_info__struct.hpp"
// Member 'data'
#include "std_msgs/msg/detail/float32_multi_array__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__msg__GridMap __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__msg__GridMap __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct GridMap_
{
  using Type = GridMap_<ContainerAllocator>;

  explicit GridMap_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : info(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->outer_start_index = 0;
      this->inner_start_index = 0;
    }
  }

  explicit GridMap_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : info(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->outer_start_index = 0;
      this->inner_start_index = 0;
    }
  }

  // field types and members
  using _info_type =
    grid_map_msgs::msg::GridMapInfo_<ContainerAllocator>;
  _info_type info;
  using _layers_type =
    std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other>;
  _layers_type layers;
  using _basic_layers_type =
    std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other>;
  _basic_layers_type basic_layers;
  using _data_type =
    std::vector<std_msgs::msg::Float32MultiArray_<ContainerAllocator>, typename ContainerAllocator::template rebind<std_msgs::msg::Float32MultiArray_<ContainerAllocator>>::other>;
  _data_type data;
  using _outer_start_index_type =
    uint16_t;
  _outer_start_index_type outer_start_index;
  using _inner_start_index_type =
    uint16_t;
  _inner_start_index_type inner_start_index;

  // setters for named parameter idiom
  Type & set__info(
    const grid_map_msgs::msg::GridMapInfo_<ContainerAllocator> & _arg)
  {
    this->info = _arg;
    return *this;
  }
  Type & set__layers(
    const std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other> & _arg)
  {
    this->layers = _arg;
    return *this;
  }
  Type & set__basic_layers(
    const std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other> & _arg)
  {
    this->basic_layers = _arg;
    return *this;
  }
  Type & set__data(
    const std::vector<std_msgs::msg::Float32MultiArray_<ContainerAllocator>, typename ContainerAllocator::template rebind<std_msgs::msg::Float32MultiArray_<ContainerAllocator>>::other> & _arg)
  {
    this->data = _arg;
    return *this;
  }
  Type & set__outer_start_index(
    const uint16_t & _arg)
  {
    this->outer_start_index = _arg;
    return *this;
  }
  Type & set__inner_start_index(
    const uint16_t & _arg)
  {
    this->inner_start_index = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    grid_map_msgs::msg::GridMap_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::msg::GridMap_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::msg::GridMap_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::msg::GridMap_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__msg__GridMap
    std::shared_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__msg__GridMap
    std::shared_ptr<grid_map_msgs::msg::GridMap_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GridMap_ & other) const
  {
    if (this->info != other.info) {
      return false;
    }
    if (this->layers != other.layers) {
      return false;
    }
    if (this->basic_layers != other.basic_layers) {
      return false;
    }
    if (this->data != other.data) {
      return false;
    }
    if (this->outer_start_index != other.outer_start_index) {
      return false;
    }
    if (this->inner_start_index != other.inner_start_index) {
      return false;
    }
    return true;
  }
  bool operator!=(const GridMap_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GridMap_

// alias to use template instance with default allocator
using GridMap =
  grid_map_msgs::msg::GridMap_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__MSG__DETAIL__GRID_MAP__STRUCT_HPP_
