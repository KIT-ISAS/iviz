// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Material.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MATERIAL__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MATERIAL__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'ambient'
// Member 'diffuse'
// Member 'emissive'
#include "iviz_msgs/msg/detail/color32__struct.hpp"
// Member 'textures'
#include "iviz_msgs/msg/detail/texture__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Material __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Material __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Material_
{
  using Type = Material_<ContainerAllocator>;

  explicit Material_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : ambient(_init),
    diffuse(_init),
    emissive(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->opacity = 0.0f;
      this->bump_scaling = 0.0f;
      this->shininess = 0.0f;
      this->shininess_strength = 0.0f;
      this->reflectivity = 0.0f;
      this->blend_mode = 0;
    }
  }

  explicit Material_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : name(_alloc),
    ambient(_alloc, _init),
    diffuse(_alloc, _init),
    emissive(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->opacity = 0.0f;
      this->bump_scaling = 0.0f;
      this->shininess = 0.0f;
      this->shininess_strength = 0.0f;
      this->reflectivity = 0.0f;
      this->blend_mode = 0;
    }
  }

  // field types and members
  using _name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _name_type name;
  using _ambient_type =
    iviz_msgs::msg::Color32_<ContainerAllocator>;
  _ambient_type ambient;
  using _diffuse_type =
    iviz_msgs::msg::Color32_<ContainerAllocator>;
  _diffuse_type diffuse;
  using _emissive_type =
    iviz_msgs::msg::Color32_<ContainerAllocator>;
  _emissive_type emissive;
  using _opacity_type =
    float;
  _opacity_type opacity;
  using _bump_scaling_type =
    float;
  _bump_scaling_type bump_scaling;
  using _shininess_type =
    float;
  _shininess_type shininess;
  using _shininess_strength_type =
    float;
  _shininess_strength_type shininess_strength;
  using _reflectivity_type =
    float;
  _reflectivity_type reflectivity;
  using _blend_mode_type =
    uint8_t;
  _blend_mode_type blend_mode;
  using _textures_type =
    std::vector<iviz_msgs::msg::Texture_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Texture_<ContainerAllocator>>::other>;
  _textures_type textures;

  // setters for named parameter idiom
  Type & set__name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->name = _arg;
    return *this;
  }
  Type & set__ambient(
    const iviz_msgs::msg::Color32_<ContainerAllocator> & _arg)
  {
    this->ambient = _arg;
    return *this;
  }
  Type & set__diffuse(
    const iviz_msgs::msg::Color32_<ContainerAllocator> & _arg)
  {
    this->diffuse = _arg;
    return *this;
  }
  Type & set__emissive(
    const iviz_msgs::msg::Color32_<ContainerAllocator> & _arg)
  {
    this->emissive = _arg;
    return *this;
  }
  Type & set__opacity(
    const float & _arg)
  {
    this->opacity = _arg;
    return *this;
  }
  Type & set__bump_scaling(
    const float & _arg)
  {
    this->bump_scaling = _arg;
    return *this;
  }
  Type & set__shininess(
    const float & _arg)
  {
    this->shininess = _arg;
    return *this;
  }
  Type & set__shininess_strength(
    const float & _arg)
  {
    this->shininess_strength = _arg;
    return *this;
  }
  Type & set__reflectivity(
    const float & _arg)
  {
    this->reflectivity = _arg;
    return *this;
  }
  Type & set__blend_mode(
    const uint8_t & _arg)
  {
    this->blend_mode = _arg;
    return *this;
  }
  Type & set__textures(
    const std::vector<iviz_msgs::msg::Texture_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Texture_<ContainerAllocator>>::other> & _arg)
  {
    this->textures = _arg;
    return *this;
  }

  // constant declarations
  static constexpr uint8_t BLEND_DEFAULT =
    0u;
  static constexpr uint8_t BLEND_ADDITIVE =
    1u;

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Material_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Material_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Material_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Material_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Material_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Material_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Material_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Material_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Material_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Material_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Material
    std::shared_ptr<iviz_msgs::msg::Material_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Material
    std::shared_ptr<iviz_msgs::msg::Material_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Material_ & other) const
  {
    if (this->name != other.name) {
      return false;
    }
    if (this->ambient != other.ambient) {
      return false;
    }
    if (this->diffuse != other.diffuse) {
      return false;
    }
    if (this->emissive != other.emissive) {
      return false;
    }
    if (this->opacity != other.opacity) {
      return false;
    }
    if (this->bump_scaling != other.bump_scaling) {
      return false;
    }
    if (this->shininess != other.shininess) {
      return false;
    }
    if (this->shininess_strength != other.shininess_strength) {
      return false;
    }
    if (this->reflectivity != other.reflectivity) {
      return false;
    }
    if (this->blend_mode != other.blend_mode) {
      return false;
    }
    if (this->textures != other.textures) {
      return false;
    }
    return true;
  }
  bool operator!=(const Material_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Material_

// alias to use template instance with default allocator
using Material =
  iviz_msgs::msg::Material_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
constexpr uint8_t Material_<ContainerAllocator>::BLEND_DEFAULT;
template<typename ContainerAllocator>
constexpr uint8_t Material_<ContainerAllocator>::BLEND_ADDITIVE;

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MATERIAL__STRUCT_HPP_
