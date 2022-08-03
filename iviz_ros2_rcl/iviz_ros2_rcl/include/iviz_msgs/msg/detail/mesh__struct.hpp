// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Mesh.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MESH__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MESH__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'vertices'
// Member 'normals'
// Member 'tangents'
// Member 'bi_tangents'
#include "iviz_msgs/msg/detail/vector3f__struct.hpp"
// Member 'tex_coords'
#include "iviz_msgs/msg/detail/tex_coords__struct.hpp"
// Member 'color_channels'
#include "iviz_msgs/msg/detail/color_channel__struct.hpp"
// Member 'faces'
#include "iviz_msgs/msg/detail/triangle__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Mesh __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Mesh __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Mesh_
{
  using Type = Mesh_<ContainerAllocator>;

  explicit Mesh_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->material_index = 0ul;
    }
  }

  explicit Mesh_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : name(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->material_index = 0ul;
    }
  }

  // field types and members
  using _name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _name_type name;
  using _vertices_type =
    std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other>;
  _vertices_type vertices;
  using _normals_type =
    std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other>;
  _normals_type normals;
  using _tangents_type =
    std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other>;
  _tangents_type tangents;
  using _bi_tangents_type =
    std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other>;
  _bi_tangents_type bi_tangents;
  using _tex_coords_type =
    std::vector<iviz_msgs::msg::TexCoords_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::TexCoords_<ContainerAllocator>>::other>;
  _tex_coords_type tex_coords;
  using _color_channels_type =
    std::vector<iviz_msgs::msg::ColorChannel_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::ColorChannel_<ContainerAllocator>>::other>;
  _color_channels_type color_channels;
  using _faces_type =
    std::vector<iviz_msgs::msg::Triangle_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Triangle_<ContainerAllocator>>::other>;
  _faces_type faces;
  using _material_index_type =
    uint32_t;
  _material_index_type material_index;

  // setters for named parameter idiom
  Type & set__name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->name = _arg;
    return *this;
  }
  Type & set__vertices(
    const std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other> & _arg)
  {
    this->vertices = _arg;
    return *this;
  }
  Type & set__normals(
    const std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other> & _arg)
  {
    this->normals = _arg;
    return *this;
  }
  Type & set__tangents(
    const std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other> & _arg)
  {
    this->tangents = _arg;
    return *this;
  }
  Type & set__bi_tangents(
    const std::vector<iviz_msgs::msg::Vector3f_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector3f_<ContainerAllocator>>::other> & _arg)
  {
    this->bi_tangents = _arg;
    return *this;
  }
  Type & set__tex_coords(
    const std::vector<iviz_msgs::msg::TexCoords_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::TexCoords_<ContainerAllocator>>::other> & _arg)
  {
    this->tex_coords = _arg;
    return *this;
  }
  Type & set__color_channels(
    const std::vector<iviz_msgs::msg::ColorChannel_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::ColorChannel_<ContainerAllocator>>::other> & _arg)
  {
    this->color_channels = _arg;
    return *this;
  }
  Type & set__faces(
    const std::vector<iviz_msgs::msg::Triangle_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Triangle_<ContainerAllocator>>::other> & _arg)
  {
    this->faces = _arg;
    return *this;
  }
  Type & set__material_index(
    const uint32_t & _arg)
  {
    this->material_index = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Mesh_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Mesh_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Mesh_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Mesh_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Mesh
    std::shared_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Mesh
    std::shared_ptr<iviz_msgs::msg::Mesh_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Mesh_ & other) const
  {
    if (this->name != other.name) {
      return false;
    }
    if (this->vertices != other.vertices) {
      return false;
    }
    if (this->normals != other.normals) {
      return false;
    }
    if (this->tangents != other.tangents) {
      return false;
    }
    if (this->bi_tangents != other.bi_tangents) {
      return false;
    }
    if (this->tex_coords != other.tex_coords) {
      return false;
    }
    if (this->color_channels != other.color_channels) {
      return false;
    }
    if (this->faces != other.faces) {
      return false;
    }
    if (this->material_index != other.material_index) {
      return false;
    }
    return true;
  }
  bool operator!=(const Mesh_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Mesh_

// alias to use template instance with default allocator
using Mesh =
  iviz_msgs::msg::Mesh_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MESH__STRUCT_HPP_
