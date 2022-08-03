// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Light.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__LIGHT__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__LIGHT__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'diffuse'
#include "iviz_msgs/msg/detail/color32__struct.hpp"
// Member 'position'
// Member 'direction'
#include "iviz_msgs/msg/detail/vector3f__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Light __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Light __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Light_
{
  using Type = Light_<ContainerAllocator>;

  explicit Light_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : diffuse(_init),
    position(_init),
    direction(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->type = 0;
      this->cast_shadows = false;
      this->range = 0.0f;
      this->inner_angle = 0.0f;
      this->outer_angle = 0.0f;
    }
  }

  explicit Light_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : name(_alloc),
    diffuse(_alloc, _init),
    position(_alloc, _init),
    direction(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->type = 0;
      this->cast_shadows = false;
      this->range = 0.0f;
      this->inner_angle = 0.0f;
      this->outer_angle = 0.0f;
    }
  }

  // field types and members
  using _name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _name_type name;
  using _type_type =
    uint8_t;
  _type_type type;
  using _cast_shadows_type =
    bool;
  _cast_shadows_type cast_shadows;
  using _diffuse_type =
    iviz_msgs::msg::Color32_<ContainerAllocator>;
  _diffuse_type diffuse;
  using _range_type =
    float;
  _range_type range;
  using _position_type =
    iviz_msgs::msg::Vector3f_<ContainerAllocator>;
  _position_type position;
  using _direction_type =
    iviz_msgs::msg::Vector3f_<ContainerAllocator>;
  _direction_type direction;
  using _inner_angle_type =
    float;
  _inner_angle_type inner_angle;
  using _outer_angle_type =
    float;
  _outer_angle_type outer_angle;

  // setters for named parameter idiom
  Type & set__name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->name = _arg;
    return *this;
  }
  Type & set__type(
    const uint8_t & _arg)
  {
    this->type = _arg;
    return *this;
  }
  Type & set__cast_shadows(
    const bool & _arg)
  {
    this->cast_shadows = _arg;
    return *this;
  }
  Type & set__diffuse(
    const iviz_msgs::msg::Color32_<ContainerAllocator> & _arg)
  {
    this->diffuse = _arg;
    return *this;
  }
  Type & set__range(
    const float & _arg)
  {
    this->range = _arg;
    return *this;
  }
  Type & set__position(
    const iviz_msgs::msg::Vector3f_<ContainerAllocator> & _arg)
  {
    this->position = _arg;
    return *this;
  }
  Type & set__direction(
    const iviz_msgs::msg::Vector3f_<ContainerAllocator> & _arg)
  {
    this->direction = _arg;
    return *this;
  }
  Type & set__inner_angle(
    const float & _arg)
  {
    this->inner_angle = _arg;
    return *this;
  }
  Type & set__outer_angle(
    const float & _arg)
  {
    this->outer_angle = _arg;
    return *this;
  }

  // constant declarations
  static constexpr uint8_t POINT =
    0u;
  static constexpr uint8_t DIRECTIONAL =
    1u;
  static constexpr uint8_t SPOT =
    2u;

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Light_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Light_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Light_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Light_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Light_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Light_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Light_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Light_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Light_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Light_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Light
    std::shared_ptr<iviz_msgs::msg::Light_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Light
    std::shared_ptr<iviz_msgs::msg::Light_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Light_ & other) const
  {
    if (this->name != other.name) {
      return false;
    }
    if (this->type != other.type) {
      return false;
    }
    if (this->cast_shadows != other.cast_shadows) {
      return false;
    }
    if (this->diffuse != other.diffuse) {
      return false;
    }
    if (this->range != other.range) {
      return false;
    }
    if (this->position != other.position) {
      return false;
    }
    if (this->direction != other.direction) {
      return false;
    }
    if (this->inner_angle != other.inner_angle) {
      return false;
    }
    if (this->outer_angle != other.outer_angle) {
      return false;
    }
    return true;
  }
  bool operator!=(const Light_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Light_

// alias to use template instance with default allocator
using Light =
  iviz_msgs::msg::Light_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
constexpr uint8_t Light_<ContainerAllocator>::POINT;
template<typename ContainerAllocator>
constexpr uint8_t Light_<ContainerAllocator>::DIRECTIONAL;
template<typename ContainerAllocator>
constexpr uint8_t Light_<ContainerAllocator>::SPOT;

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__LIGHT__STRUCT_HPP_
