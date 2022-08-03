// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:srv/StartCapture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__STRUCT_HPP_
#define IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__StartCapture_Request __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__StartCapture_Request __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct StartCapture_Request_
{
  using Type = StartCapture_Request_<ContainerAllocator>;

  explicit StartCapture_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->resolution_x = 0l;
      this->resolution_y = 0l;
      this->with_holograms = false;
    }
  }

  explicit StartCapture_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->resolution_x = 0l;
      this->resolution_y = 0l;
      this->with_holograms = false;
    }
  }

  // field types and members
  using _resolution_x_type =
    int32_t;
  _resolution_x_type resolution_x;
  using _resolution_y_type =
    int32_t;
  _resolution_y_type resolution_y;
  using _with_holograms_type =
    bool;
  _with_holograms_type with_holograms;

  // setters for named parameter idiom
  Type & set__resolution_x(
    const int32_t & _arg)
  {
    this->resolution_x = _arg;
    return *this;
  }
  Type & set__resolution_y(
    const int32_t & _arg)
  {
    this->resolution_y = _arg;
    return *this;
  }
  Type & set__with_holograms(
    const bool & _arg)
  {
    this->with_holograms = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::StartCapture_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::StartCapture_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::StartCapture_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::StartCapture_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__StartCapture_Request
    std::shared_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__StartCapture_Request
    std::shared_ptr<iviz_msgs::srv::StartCapture_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const StartCapture_Request_ & other) const
  {
    if (this->resolution_x != other.resolution_x) {
      return false;
    }
    if (this->resolution_y != other.resolution_y) {
      return false;
    }
    if (this->with_holograms != other.with_holograms) {
      return false;
    }
    return true;
  }
  bool operator!=(const StartCapture_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct StartCapture_Request_

// alias to use template instance with default allocator
using StartCapture_Request =
  iviz_msgs::srv::StartCapture_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__StartCapture_Response __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__StartCapture_Response __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct StartCapture_Response_
{
  using Type = StartCapture_Response_<ContainerAllocator>;

  explicit StartCapture_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
    }
  }

  explicit StartCapture_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
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
    iviz_msgs::srv::StartCapture_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::StartCapture_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::StartCapture_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::StartCapture_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__StartCapture_Response
    std::shared_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__StartCapture_Response
    std::shared_ptr<iviz_msgs::srv::StartCapture_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const StartCapture_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->message != other.message) {
      return false;
    }
    return true;
  }
  bool operator!=(const StartCapture_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct StartCapture_Response_

// alias to use template instance with default allocator
using StartCapture_Response =
  iviz_msgs::srv::StartCapture_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs

namespace iviz_msgs
{

namespace srv
{

struct StartCapture
{
  using Request = iviz_msgs::srv::StartCapture_Request;
  using Response = iviz_msgs::srv::StartCapture_Response;
};

}  // namespace srv

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__START_CAPTURE__STRUCT_HPP_
