// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Texture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__TEXTURE__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__TEXTURE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Texture __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Texture __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Texture_
{
  using Type = Texture_<ContainerAllocator>;

  explicit Texture_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->path = "";
      this->index = 0l;
      this->type = 0;
      this->mapping = 0;
      this->uv_index = 0l;
      this->blend_factor = 0.0f;
      this->operation = 0;
      this->wrap_mode_u = 0;
      this->wrap_mode_v = 0;
    }
  }

  explicit Texture_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : path(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->path = "";
      this->index = 0l;
      this->type = 0;
      this->mapping = 0;
      this->uv_index = 0l;
      this->blend_factor = 0.0f;
      this->operation = 0;
      this->wrap_mode_u = 0;
      this->wrap_mode_v = 0;
    }
  }

  // field types and members
  using _path_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _path_type path;
  using _index_type =
    int32_t;
  _index_type index;
  using _type_type =
    uint8_t;
  _type_type type;
  using _mapping_type =
    uint8_t;
  _mapping_type mapping;
  using _uv_index_type =
    int32_t;
  _uv_index_type uv_index;
  using _blend_factor_type =
    float;
  _blend_factor_type blend_factor;
  using _operation_type =
    uint8_t;
  _operation_type operation;
  using _wrap_mode_u_type =
    uint8_t;
  _wrap_mode_u_type wrap_mode_u;
  using _wrap_mode_v_type =
    uint8_t;
  _wrap_mode_v_type wrap_mode_v;

  // setters for named parameter idiom
  Type & set__path(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->path = _arg;
    return *this;
  }
  Type & set__index(
    const int32_t & _arg)
  {
    this->index = _arg;
    return *this;
  }
  Type & set__type(
    const uint8_t & _arg)
  {
    this->type = _arg;
    return *this;
  }
  Type & set__mapping(
    const uint8_t & _arg)
  {
    this->mapping = _arg;
    return *this;
  }
  Type & set__uv_index(
    const int32_t & _arg)
  {
    this->uv_index = _arg;
    return *this;
  }
  Type & set__blend_factor(
    const float & _arg)
  {
    this->blend_factor = _arg;
    return *this;
  }
  Type & set__operation(
    const uint8_t & _arg)
  {
    this->operation = _arg;
    return *this;
  }
  Type & set__wrap_mode_u(
    const uint8_t & _arg)
  {
    this->wrap_mode_u = _arg;
    return *this;
  }
  Type & set__wrap_mode_v(
    const uint8_t & _arg)
  {
    this->wrap_mode_v = _arg;
    return *this;
  }

  // constant declarations
  static constexpr uint8_t TYPE_NONE =
    0u;
  static constexpr uint8_t TYPE_DIFFUSE =
    1u;
  static constexpr uint8_t TYPE_SPECULAR =
    2u;
  static constexpr uint8_t TYPE_AMBIENT =
    3u;
  static constexpr uint8_t TYPE_EMISSIVE =
    4u;
  static constexpr uint8_t TYPE_HEIGHT =
    5u;
  static constexpr uint8_t TYPE_NORMALS =
    6u;
  static constexpr uint8_t TYPE_SHININESS =
    7u;
  static constexpr uint8_t TYPE_OPACITY =
    8u;
  static constexpr uint8_t TYPE_DISPLACEMENT =
    9u;
  static constexpr uint8_t TYPE_LIGHTMAP =
    10u;
  static constexpr uint8_t TYPE_REFLECTION =
    11u;
  static constexpr uint8_t TYPE_UNKNOWN =
    12u;
  static constexpr uint8_t MAPPING_FROM_UV =
    0u;
  static constexpr uint8_t MAPPING_SPHERE =
    1u;
  static constexpr uint8_t MAPPING_CYLINDER =
    2u;
  static constexpr uint8_t MAPPING_BOX =
    3u;
  static constexpr uint8_t MAPPING_PLANE =
    4u;
  static constexpr uint8_t MAPPING_UNKNOWN =
    5u;
  static constexpr uint8_t OP_MULTIPLY =
    0u;
  static constexpr uint8_t OP_ADD =
    1u;
  static constexpr uint8_t OP_SUBTRACT =
    2u;
  static constexpr uint8_t OP_DIVIDE =
    3u;
  static constexpr uint8_t OP_SMOOTH_ADD =
    4u;
  static constexpr uint8_t OP_SIGNED_ADD =
    5u;
  static constexpr uint8_t WRAP_WRAP =
    0u;
  static constexpr uint8_t WRAP_CLAMP =
    1u;
  static constexpr uint8_t WRAP_MIRROR =
    2u;
  static constexpr uint8_t WRAP_DECAL =
    3u;

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Texture_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Texture_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Texture_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Texture_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Texture_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Texture_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Texture_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Texture_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Texture_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Texture_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Texture
    std::shared_ptr<iviz_msgs::msg::Texture_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Texture
    std::shared_ptr<iviz_msgs::msg::Texture_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Texture_ & other) const
  {
    if (this->path != other.path) {
      return false;
    }
    if (this->index != other.index) {
      return false;
    }
    if (this->type != other.type) {
      return false;
    }
    if (this->mapping != other.mapping) {
      return false;
    }
    if (this->uv_index != other.uv_index) {
      return false;
    }
    if (this->blend_factor != other.blend_factor) {
      return false;
    }
    if (this->operation != other.operation) {
      return false;
    }
    if (this->wrap_mode_u != other.wrap_mode_u) {
      return false;
    }
    if (this->wrap_mode_v != other.wrap_mode_v) {
      return false;
    }
    return true;
  }
  bool operator!=(const Texture_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Texture_

// alias to use template instance with default allocator
using Texture =
  iviz_msgs::msg::Texture_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_NONE;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_DIFFUSE;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_SPECULAR;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_AMBIENT;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_EMISSIVE;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_HEIGHT;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_NORMALS;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_SHININESS;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_OPACITY;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_DISPLACEMENT;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_LIGHTMAP;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_REFLECTION;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::TYPE_UNKNOWN;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::MAPPING_FROM_UV;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::MAPPING_SPHERE;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::MAPPING_CYLINDER;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::MAPPING_BOX;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::MAPPING_PLANE;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::MAPPING_UNKNOWN;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::OP_MULTIPLY;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::OP_ADD;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::OP_SUBTRACT;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::OP_DIVIDE;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::OP_SMOOTH_ADD;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::OP_SIGNED_ADD;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::WRAP_WRAP;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::WRAP_CLAMP;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::WRAP_MIRROR;
template<typename ContainerAllocator>
constexpr uint8_t Texture_<ContainerAllocator>::WRAP_DECAL;

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__TEXTURE__STRUCT_HPP_
