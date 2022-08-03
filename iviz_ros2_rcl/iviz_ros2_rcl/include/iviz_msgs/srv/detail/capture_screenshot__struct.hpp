// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:srv/CaptureScreenshot.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__STRUCT_HPP_
#define IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Request __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Request __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct CaptureScreenshot_Request_
{
  using Type = CaptureScreenshot_Request_<ContainerAllocator>;

  explicit CaptureScreenshot_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->compress = false;
    }
  }

  explicit CaptureScreenshot_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->compress = false;
    }
  }

  // field types and members
  using _compress_type =
    bool;
  _compress_type compress;

  // setters for named parameter idiom
  Type & set__compress(
    const bool & _arg)
  {
    this->compress = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Request
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Request
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const CaptureScreenshot_Request_ & other) const
  {
    if (this->compress != other.compress) {
      return false;
    }
    return true;
  }
  bool operator!=(const CaptureScreenshot_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct CaptureScreenshot_Request_

// alias to use template instance with default allocator
using CaptureScreenshot_Request =
  iviz_msgs::srv::CaptureScreenshot_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs


// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.hpp"
// Member 'pose'
#include "geometry_msgs/msg/detail/pose__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Response __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Response __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct CaptureScreenshot_Response_
{
  using Type = CaptureScreenshot_Response_<ContainerAllocator>;

  explicit CaptureScreenshot_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_init),
    pose(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
      this->width = 0l;
      this->height = 0l;
      this->bpp = 0l;
      std::fill<typename std::array<double, 9>::iterator, double>(this->intrinsics.begin(), this->intrinsics.end(), 0.0);
    }
  }

  explicit CaptureScreenshot_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : message(_alloc),
    header(_alloc, _init),
    intrinsics(_alloc),
    pose(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
      this->message = "";
      this->width = 0l;
      this->height = 0l;
      this->bpp = 0l;
      std::fill<typename std::array<double, 9>::iterator, double>(this->intrinsics.begin(), this->intrinsics.end(), 0.0);
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;
  using _message_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _message_type message;
  using _header_type =
    std_msgs::msg::Header_<ContainerAllocator>;
  _header_type header;
  using _width_type =
    int32_t;
  _width_type width;
  using _height_type =
    int32_t;
  _height_type height;
  using _bpp_type =
    int32_t;
  _bpp_type bpp;
  using _intrinsics_type =
    std::array<double, 9>;
  _intrinsics_type intrinsics;
  using _pose_type =
    geometry_msgs::msg::Pose_<ContainerAllocator>;
  _pose_type pose;
  using _data_type =
    std::vector<unsigned char, typename ContainerAllocator::template rebind<unsigned char>::other>;
  _data_type data;

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
  Type & set__header(
    const std_msgs::msg::Header_<ContainerAllocator> & _arg)
  {
    this->header = _arg;
    return *this;
  }
  Type & set__width(
    const int32_t & _arg)
  {
    this->width = _arg;
    return *this;
  }
  Type & set__height(
    const int32_t & _arg)
  {
    this->height = _arg;
    return *this;
  }
  Type & set__bpp(
    const int32_t & _arg)
  {
    this->bpp = _arg;
    return *this;
  }
  Type & set__intrinsics(
    const std::array<double, 9> & _arg)
  {
    this->intrinsics = _arg;
    return *this;
  }
  Type & set__pose(
    const geometry_msgs::msg::Pose_<ContainerAllocator> & _arg)
  {
    this->pose = _arg;
    return *this;
  }
  Type & set__data(
    const std::vector<unsigned char, typename ContainerAllocator::template rebind<unsigned char>::other> & _arg)
  {
    this->data = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Response
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__srv__CaptureScreenshot_Response
    std::shared_ptr<iviz_msgs::srv::CaptureScreenshot_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const CaptureScreenshot_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    if (this->message != other.message) {
      return false;
    }
    if (this->header != other.header) {
      return false;
    }
    if (this->width != other.width) {
      return false;
    }
    if (this->height != other.height) {
      return false;
    }
    if (this->bpp != other.bpp) {
      return false;
    }
    if (this->intrinsics != other.intrinsics) {
      return false;
    }
    if (this->pose != other.pose) {
      return false;
    }
    if (this->data != other.data) {
      return false;
    }
    return true;
  }
  bool operator!=(const CaptureScreenshot_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct CaptureScreenshot_Response_

// alias to use template instance with default allocator
using CaptureScreenshot_Response =
  iviz_msgs::srv::CaptureScreenshot_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace iviz_msgs

namespace iviz_msgs
{

namespace srv
{

struct CaptureScreenshot
{
  using Request = iviz_msgs::srv::CaptureScreenshot_Request;
  using Response = iviz_msgs::srv::CaptureScreenshot_Response;
};

}  // namespace srv

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__CAPTURE_SCREENSHOT__STRUCT_HPP_
