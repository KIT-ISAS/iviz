// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:srv/UpdateRobot.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__STRUCT_HPP_
#define IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'configuration'
#include "iviz_msgs/msg/detail/robot_configuration__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__UpdateRobot_Request __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__UpdateRobot_Request __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct UpdateRobot_Request_
{
  using Type = UpdateRobot_Request_<ContainerAllocator>;

  explicit UpdateRobot_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : configuration(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->operation = 0l;
      this->id = "";
    }
  }

  explicit UpdateRobot_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : id(_alloc),
    configuration(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->operation = 0l;
      this->id = "";
    }
  }

  // field types and members
  using _operation_type =
    int32_t;
  _operation_type operation;
  using _id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _id_type id;
  using _configuration_type =
    iviz_msgs::msg::RobotConfiguration_<ContainerAllocator>;
  _configuration_type configuration;
  using _valid_fields_type =
    std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other>;
  _valid_fields_type valid_fields;

  // setters for named parameter idiom
  Type & set__operation(
    const int32_t & _arg)
  {
    this->operation = _arg;
    return *this;
  }
  Type & set__id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->id = _arg;
    return *this;
  }
  Type & set__configuration(
    const iviz_msgs::msg::RobotConfiguration_<ContainerAllocator> & _arg)
  {
    this->configuration = _arg;
    return *this;
  }
  Type & set__valid_fields(
    const std::vector<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, typename ContainerAllocator::template rebind<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>::other> & _arg)
  {
    this->valid_fields = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__UpdateRobot_Request
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__UpdateRobot_Request
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const UpdateRobot_Request_ & other) const
  {
    if (this->operation != other.operation) {
      return false;
    }
    if (this->id != other.id) {
      return false;
    }
    if (this->configuration != other.configuration) {
      return false;
    }
    if (this->valid_fields != other.valid_fields) {
      return false;
    }
    return true;
  }
  bool operator!=(const UpdateRobot_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct UpdateRobot_Request_

// alias to use template instance with default allocator
using UpdateRobot_Request =
  iviz_msgs::srv::UpdateRobot_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__UpdateRobot_Response __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__UpdateRobot_Response __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct UpdateRobot_Response_
{
  using Type = UpdateRobot_Response_<ContainerAllocator>;

  explicit UpdateRobot_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
    }
  }

  explicit UpdateRobot_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : message(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;
  using _message_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _message_type message;

  // setters for named parameter idiom
  Type & set__success(
    const bool & _arg)
  {
    this->success = _arg;
    return *this;
  }
  Type & set__message(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->message = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__UpdateRobot_Response
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__UpdateRobot_Response
    std::shared_ptr<iviz_msgs::srv::UpdateRobot_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const UpdateRobot_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->message != other.message) {
      return false;
    }
    return true;
  }
  bool operator!=(const UpdateRobot_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct UpdateRobot_Response_

// alias to use template instance with default allocator
using UpdateRobot_Response =
  iviz_msgs::srv::UpdateRobot_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs

namespace iviz_msgs
{

namespace srv
{

struct UpdateRobot
{
  using Request = iviz_msgs::srv::UpdateRobot_Request;
  using Response = iviz_msgs::srv::UpdateRobot_Response;
};

}  // namespace srv

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__UPDATE_ROBOT__STRUCT_HPP_
