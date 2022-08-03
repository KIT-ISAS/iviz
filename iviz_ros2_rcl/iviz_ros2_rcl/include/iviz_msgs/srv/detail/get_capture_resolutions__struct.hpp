// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:srv/GetCaptureResolutions.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__STRUCT_HPP_
#define IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Request __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Request __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetCaptureResolutions_Request_
{
  using Type = GetCaptureResolutions_Request_<ContainerAllocator>;

  explicit GetCaptureResolutions_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->structure_needs_at_least_one_member = 0;
    }
  }

  explicit GetCaptureResolutions_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->structure_needs_at_least_one_member = 0;
    }
  }

  // field types and members
  using _structure_needs_at_least_one_member_type =
    uint8_t;
  _structure_needs_at_least_one_member_type structure_needs_at_least_one_member;


  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Request
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Request
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetCaptureResolutions_Request_ & other) const
  {
    if (this->structure_needs_at_least_one_member != other.structure_needs_at_least_one_member) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetCaptureResolutions_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetCaptureResolutions_Request_

// alias to use template instance with default allocator
using GetCaptureResolutions_Request =
  iviz_msgs::srv::GetCaptureResolutions_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs


// Include directives for member types
// Member 'resolutions'
#include "iviz_msgs/msg/detail/vector2i__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Response __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Response __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetCaptureResolutions_Response_
{
  using Type = GetCaptureResolutions_Response_<ContainerAllocator>;

  explicit GetCaptureResolutions_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
    }
  }

  explicit GetCaptureResolutions_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
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
  using _resolutions_type =
    std::vector<iviz_msgs::msg::Vector2i_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector2i_<ContainerAllocator>>::other>;
  _resolutions_type resolutions;

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
  Type & set__resolutions(
    const std::vector<iviz_msgs::msg::Vector2i_<ContainerAllocator>, typename ContainerAllocator::template rebind<iviz_msgs::msg::Vector2i_<ContainerAllocator>>::other> & _arg)
  {
    this->resolutions = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Response
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__GetCaptureResolutions_Response
    std::shared_ptr<iviz_msgs::srv::GetCaptureResolutions_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetCaptureResolutions_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->message != other.message) {
      return false;
    }
    if (this->resolutions != other.resolutions) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetCaptureResolutions_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetCaptureResolutions_Response_

// alias to use template instance with default allocator
using GetCaptureResolutions_Response =
  iviz_msgs::srv::GetCaptureResolutions_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs

namespace iviz_msgs
{

namespace srv
{

struct GetCaptureResolutions
{
  using Request = iviz_msgs::srv::GetCaptureResolutions_Request;
  using Response = iviz_msgs::srv::GetCaptureResolutions_Response;
};

}  // namespace srv

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_CAPTURE_RESOLUTIONS__STRUCT_HPP_
