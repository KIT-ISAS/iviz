// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:srv/AddModuleFromTopic.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__STRUCT_HPP_
#define IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Request __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Request __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct AddModuleFromTopic_Request_
{
  using Type = AddModuleFromTopic_Request_<ContainerAllocator>;

  explicit AddModuleFromTopic_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->topic = "";
      this->id = "";
    }
  }

  explicit AddModuleFromTopic_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : topic(_alloc),
    id(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->topic = "";
      this->id = "";
    }
  }

  // field types and members
  using _topic_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _topic_type topic;
  using _id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _id_type id;

  // setters for named parameter idiom
  Type & set__topic(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->topic = _arg;
    return *this;
  }
  Type & set__id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->id = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Request
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Request
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const AddModuleFromTopic_Request_ & other) const
  {
    if (this->topic != other.topic) {
      return false;
    }
    if (this->id != other.id) {
      return false;
    }
    return true;
  }
  bool operator!=(const AddModuleFromTopic_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct AddModuleFromTopic_Request_

// alias to use template instance with default allocator
using AddModuleFromTopic_Request =
  iviz_msgs::srv::AddModuleFromTopic_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Response __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Response __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct AddModuleFromTopic_Response_
{
  using Type = AddModuleFromTopic_Response_<ContainerAllocator>;

  explicit AddModuleFromTopic_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
      this->id = "";
    }
  }

  explicit AddModuleFromTopic_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : message(_alloc),
    id(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
      this->id = "";
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;
  using _message_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _message_type message;
  using _id_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _id_type id;

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
  Type & set__id(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->id = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Response
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__AddModuleFromTopic_Response
    std::shared_ptr<iviz_msgs::srv::AddModuleFromTopic_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const AddModuleFromTopic_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->message != other.message) {
      return false;
    }
    if (this->id != other.id) {
      return false;
    }
    return true;
  }
  bool operator!=(const AddModuleFromTopic_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct AddModuleFromTopic_Response_

// alias to use template instance with default allocator
using AddModuleFromTopic_Response =
  iviz_msgs::srv::AddModuleFromTopic_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs

namespace iviz_msgs
{

namespace srv
{

struct AddModuleFromTopic
{
  using Request = iviz_msgs::srv::AddModuleFromTopic_Request;
  using Response = iviz_msgs::srv::AddModuleFromTopic_Response;
};

}  // namespace srv

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__ADD_MODULE_FROM_TOPIC__STRUCT_HPP_
