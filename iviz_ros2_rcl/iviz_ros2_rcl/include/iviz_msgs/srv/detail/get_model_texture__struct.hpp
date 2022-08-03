// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:srv/GetModelTexture.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_MODEL_TEXTURE__STRUCT_HPP_
#define IVIZ_MSGS__SRV__DETAIL__GET_MODEL_TEXTURE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__GetModelTexture_Request __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__GetModelTexture_Request __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetModelTexture_Request_
{
  using Type = GetModelTexture_Request_<ContainerAllocator>;

  explicit GetModelTexture_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->uri = "";
    }
  }

  explicit GetModelTexture_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : uri(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->uri = "";
    }
  }

  // field types and members
  using _uri_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _uri_type uri;

  // setters for named parameter idiom
  Type & set__uri(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->uri = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__GetModelTexture_Request
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__GetModelTexture_Request
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetModelTexture_Request_ & other) const
  {
    if (this->uri != other.uri) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetModelTexture_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetModelTexture_Request_

// alias to use template instance with default allocator
using GetModelTexture_Request =
  iviz_msgs::srv::GetModelTexture_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs


// Include directives for member types
// Member 'image'
#include "sensor_msgs/msg/detail/compressed_image__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__GetModelTexture_Response __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__GetModelTexture_Response __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct GetModelTexture_Response_
{
  using Type = GetModelTexture_Response_<ContainerAllocator>;

  explicit GetModelTexture_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : image(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
    }
  }

  explicit GetModelTexture_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : image(_alloc, _init),
    message(_alloc)
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
  using _image_type =
    sensor_msgs::msg::CompressedImage_<ContainerAllocator>;
  _image_type image;
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
  Type & set__image(
    const sensor_msgs::msg::CompressedImage_<ContainerAllocator> & _arg)
  {
    this->image = _arg;
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
    iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__GetModelTexture_Response
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__GetModelTexture_Response
    std::shared_ptr<iviz_msgs::srv::GetModelTexture_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const GetModelTexture_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->image != other.image) {
      return false;
    }
    if (this->message != other.message) {
      return false;
    }
    return true;
  }
  bool operator!=(const GetModelTexture_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct GetModelTexture_Response_

// alias to use template instance with default allocator
using GetModelTexture_Response =
  iviz_msgs::srv::GetModelTexture_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs

namespace iviz_msgs
{

namespace srv
{

struct GetModelTexture
{
  using Request = iviz_msgs::srv::GetModelTexture_Request;
  using Response = iviz_msgs::srv::GetModelTexture_Response;
};

}  // namespace srv

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_MODEL_TEXTURE__STRUCT_HPP_
