// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/Model.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MODEL__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MODEL__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'meshes'
#include "iviz_msgs/msg/detail/mesh__struct.hpp"
// Member 'materials'
#include "iviz_msgs/msg/detail/material__struct.hpp"
// Member 'nodes'
#include "iviz_msgs/msg/detail/node__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__Model __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__Model __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Model_
{
  using Type = Model_<ContainerAllocator>;

  explicit Model_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->filename = "";
      this->orientation_hint = "";
    }
  }

  explicit Model_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : name(_alloc),
    filename(_alloc),
    orientation_hint(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->name = "";
      this->filename = "";
      this->orientation_hint = "";
    }
  }

  // field types and members
  using _name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _name_type name;
  using _filename_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _filename_type filename;
  using _orientation_hint_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _orientation_hint_type orientation_hint;
  using _meshes_type =
    std::vector<iviz_msgs::msg::Mesh_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Mesh_<ContainerAllocator>>::other>;
  _meshes_type meshes;
  using _materials_type =
    std::vector<iviz_msgs::msg::Material_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Material_<ContainerAllocator>>::other>;
  _materials_type materials;
  using _nodes_type =
    std::vector<iviz_msgs::msg::Node_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Node_<ContainerAllocator>>::other>;
  _nodes_type nodes;

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
  Type & set__orientation_hint(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->orientation_hint = _arg;
    return *this;
  }
  Type & set__meshes(
    const std::vector<iviz_msgs::msg::Mesh_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Mesh_<ContainerAllocator>>::other> & _arg)
  {
    this->meshes = _arg;
    return *this;
  }
  Type & set__materials(
    const std::vector<iviz_msgs::msg::Material_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Material_<ContainerAllocator>>::other> & _arg)
  {
    this->materials = _arg;
    return *this;
  }
  Type & set__nodes(
    const std::vector<iviz_msgs::msg::Node_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Node_<ContainerAllocator>>::other> & _arg)
  {
    this->nodes = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::msg::Model_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::Model_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::Model_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::Model_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Model_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Model_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::Model_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::Model_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::Model_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::Model_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__Model
    std::shared_ptr<iviz_msgs::msg::Model_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__Model
    std::shared_ptr<iviz_msgs::msg::Model_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Model_ & other) const
  {
    if (this->name != other.name) {
      return false;
    }
    if (this->filename != other.filename) {
      return false;
    }
    if (this->orientation_hint != other.orientation_hint) {
      return false;
    }
    if (this->meshes != other.meshes) {
      return false;
    }
    if (this->materials != other.materials) {
      return false;
    }
    if (this->nodes != other.nodes) {
      return false;
    }
    return true;
  }
  bool operator!=(const Model_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Model_

// alias to use template instance with default allocator
using Model =
  iviz_msgs::msg::Model_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MODEL__STRUCT_HPP_
