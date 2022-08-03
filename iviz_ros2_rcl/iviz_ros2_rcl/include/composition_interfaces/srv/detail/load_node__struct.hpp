// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from composition_interfaces:srv/LoadNode.idl
// generated code does not contain a copyright notice

#ifndef COMPOSITION_INTERFACES__SRV__DETAIL__LOAD_NODE__STRUCT_HPP_
#define COMPOSITION_INTERFACES__SRV__DETAIL__LOAD_NODE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'parameters'
// Member 'extra_arguments'
#include "rcl_interfaces/msg/detail/parameter__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__composition_interfaces__srv__LoadNode_Request __attribute__((deprecated))
#else
# define DEPRECATED__composition_interfaces__srv__LoadNode_Request __declspec(deprecated)
#endif

namespace composition_interfaces
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct LoadNode_Request_
{
  using Type = LoadNode_Request_<ContainerAllocator>;

  explicit LoadNode_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->package_name = "";
      this->plugin_name = "";
      this->node_name = "";
      this->node_namespace = "";
      this->log_level = 0;
    }
  }

  explicit LoadNode_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : package_name(_alloc),
    plugin_name(_alloc),
    node_name(_alloc),
    node_namespace(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->package_name = "";
      this->plugin_name = "";
      this->node_name = "";
      this->node_namespace = "";
      this->log_level = 0;
    }
  }

  // field types and members
  using _package_name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _package_name_type package_name;
  using _plugin_name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _plugin_name_type plugin_name;
  using _node_name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _node_name_type node_name;
  using _node_namespace_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _node_namespace_type node_namespace;
  using _log_level_type =
    uint8_t;
  _log_level_type log_level;
  using _remap_rules_type =
    std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other>;
  _remap_rules_type remap_rules;
  using _parameters_type =
    std::vector<rcl_interfaces::msg::Parameter_<ContainerAllocator>, typename ContainerAllocator::template rebind<rcl_interfaces::msg::Parameter_<ContainerAllocator>>::other>;
  _parameters_type parameters;
  using _extra_arguments_type =
    std::vector<rcl_interfaces::msg::Parameter_<ContainerAllocator>, typename ContainerAllocator::template rebind<rcl_interfaces::msg::Parameter_<ContainerAllocator>>::other>;
  _extra_arguments_type extra_arguments;

  // setters for named parameter idiom
  Type & set__package_name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->package_name = _arg;
    return *this;
  }
  Type & set__plugin_name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->plugin_name = _arg;
    return *this;
  }
  Type & set__node_name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->node_name = _arg;
    return *this;
  }
  Type & set__node_namespace(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->node_namespace = _arg;
    return *this;
  }
  Type & set__log_level(
    const uint8_t & _arg)
  {
    this->log_level = _arg;
    return *this;
  }
  Type & set__remap_rules(
    const std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other> & _arg)
  {
    this->remap_rules = _arg;
    return *this;
  }
  Type & set__parameters(
    const std::vector<rcl_interfaces::msg::Parameter_<ContainerAllocator>, typename ContainerAllocator::template rebind<rcl_interfaces::msg::Parameter_<ContainerAllocator>>::other> & _arg)
  {
    this->parameters = _arg;
    return *this;
  }
  Type & set__extra_arguments(
    const std::vector<rcl_interfaces::msg::Parameter_<ContainerAllocator>, typename ContainerAllocator::template rebind<rcl_interfaces::msg::Parameter_<ContainerAllocator>>::other> & _arg)
  {
    this->extra_arguments = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    composition_interfaces::srv::LoadNode_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const composition_interfaces::srv::LoadNode_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::LoadNode_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::LoadNode_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__composition_interfaces__srv__LoadNode_Request
    std::shared_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__composition_interfaces__srv__LoadNode_Request
    std::shared_ptr<composition_interfaces::srv::LoadNode_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const LoadNode_Request_ & other) const
  {
    if (this->package_name != other.package_name) {
      return false;
    }
    if (this->plugin_name != other.plugin_name) {
      return false;
    }
    if (this->node_name != other.node_name) {
      return false;
    }
    if (this->node_namespace != other.node_namespace) {
      return false;
    }
    if (this->log_level != other.log_level) {
      return false;
    }
    if (this->remap_rules != other.remap_rules) {
      return false;
    }
    if (this->parameters != other.parameters) {
      return false;
    }
    if (this->extra_arguments != other.extra_arguments) {
      return false;
    }
    return true;
  }
  bool operator!=(const LoadNode_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct LoadNode_Request_

// alias to use template instance with default allocator
using LoadNode_Request =
  composition_interfaces::srv::LoadNode_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace composition_interfaces


#ifndef _WIN32
# define DEPRECATED__composition_interfaces__srv__LoadNode_Response __attribute__((deprecated))
#else
# define DEPRECATED__composition_interfaces__srv__LoadNode_Response __declspec(deprecated)
#endif

namespace composition_interfaces
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct LoadNode_Response_
{
  using Type = LoadNode_Response_<ContainerAllocator>;

  explicit LoadNode_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->error_message = "";
      this->full_node_name = "";
      this->unique_id = 0ull;
    }
  }

  explicit LoadNode_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : error_message(_alloc),
    full_node_name(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->error_message = "";
      this->full_node_name = "";
      this->unique_id = 0ull;
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;
  using _error_message_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _error_message_type error_message;
  using _full_node_name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _full_node_name_type full_node_name;
  using _unique_id_type =
    uint64_t;
  _unique_id_type unique_id;

  // setters for named parameter idiom
  Type & set__success(
    const bool & _arg)
  {
    this->success = _arg;
    return *this;
  }
  Type & set__error_message(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->error_message = _arg;
    return *this;
  }
  Type & set__full_node_name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->full_node_name = _arg;
    return *this;
  }
  Type & set__unique_id(
    const uint64_t & _arg)
  {
    this->unique_id = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    composition_interfaces::srv::LoadNode_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const composition_interfaces::srv::LoadNode_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::LoadNode_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      composition_interfaces::srv::LoadNode_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__composition_interfaces__srv__LoadNode_Response
    std::shared_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__composition_interfaces__srv__LoadNode_Response
    std::shared_ptr<composition_interfaces::srv::LoadNode_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const LoadNode_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->error_message != other.error_message) {
      return false;
    }
    if (this->full_node_name != other.full_node_name) {
      return false;
    }
    if (this->unique_id != other.unique_id) {
      return false;
    }
    return true;
  }
  bool operator!=(const LoadNode_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct LoadNode_Response_

// alias to use template instance with default allocator
using LoadNode_Response =
  composition_interfaces::srv::LoadNode_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace composition_interfaces

namespace composition_interfaces
{

namespace srv
{

struct LoadNode
{
  using Request = composition_interfaces::srv::LoadNode_Request;
  using Response = composition_interfaces::srv::LoadNode_Response;
};

}  // namespace srv

}  // namespace composition_interfaces

#endif  // COMPOSITION_INTERFACES__SRV__DETAIL__LOAD_NODE__STRUCT_HPP_
