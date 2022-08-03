// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Scene.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__SCENE__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__SCENE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'includes'
#include "iviz_msgs/msg/detail/include__struct.hpp"
// Member 'lights'
#include "iviz_msgs/msg/detail/light__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Scene __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Scene __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Scene_
{
  using Type = Scene_<ContainerAllocator>;

  explicit Scene_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->filename = "";
    }
  }

  explicit Scene_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : name(_alloc),
    filename(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->filename = "";
    }
  }

  // field types and members
  using _name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _name_type name;
  using _filename_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _filename_type filename;
  using _includes_type =
    std::vector<iviz_msgs::msg::Include_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Include_<ContainerAllocator>>::other>;
  _includes_type includes;
  using _lights_type =
    std::vector<iviz_msgs::msg::Light_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Light_<ContainerAllocator>>::other>;
  _lights_type lights;

  // setters for named parameter idiom
  Type & set__name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->name = _arg;
    return *this;
  }
  Type & set__filename(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->filename = _arg;
    return *this;
  }
  Type & set__includes(
    const std::vector<iviz_msgs::msg::Include_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Include_<ContainerAllocator>>::other> & _arg)
  {
    this->includes = _arg;
    return *this;
  }
  Type & set__lights(
    const std::vector<iviz_msgs::msg::Light_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Light_<ContainerAllocator>>::other> & _arg)
  {
    this->lights = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Scene_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Scene_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Scene_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Scene_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Scene_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Scene_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Scene_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Scene_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Scene_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Scene_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Scene
    std::shared_ptr<iviz_msgs::msg::Scene_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Scene
    std::shared_ptr<iviz_msgs::msg::Scene_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Scene_ & other) const
  {
    if (this->name != other.name) {
      return false;
    }
    if (this->filename != other.filename) {
      return false;
    }
    if (this->includes != other.includes) {
      return false;
    }
    if (this->lights != other.lights) {
      return false;
    }
    return true;
  }
  bool operator!=(const Scene_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Scene_

// alias to use template instance with default allocator
using Scene =
  iviz_msgs::msg::Scene_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__SCENE__STRUCT_HPP_
