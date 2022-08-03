// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from iviz_msgs:msg/ARMarker.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__AR_MARKER__STRUCT_HPP_
#define IVIZ_MSGS__MSG__DETAIL__AR_MARKER__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'header'
#include "std_msgs/msg/detail/header__struct.hpp"
// Member 'corners'
#include "geometry_msgs/msg/detail/vector3__struct.hpp"
// Member 'camera_pose'
// Member 'pose_relative_to_camera'
#include "geometry_msgs/msg/detail/pose__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__iviz_msgs__msg__ARMarker __attribute__((deprecated))
#else
# define DEPRECATED__iviz_msgs__msg__ARMarker __declspec(deprecated)
#endif

namespace iviz_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct ARMarker_
{
  using Type = ARMarker_<ContainerAllocator>;

  explicit ARMarker_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_init),
    camera_pose(_init),
    pose_relative_to_camera(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->type = 0;
      this->code = "";
      this->corners.fill(geometry_msgs::msg::Vector3_<ContainerAllocator>{_init});
      std::fill<typename std::array<double, 9>::iterator, double>(this->camera_intrinsic.begin(), this->camera_intrinsic.end(), 0.0);
      this->has_reliable_pose = false;
      this->marker_size_in_mm = 0.0;
    }
  }

  explicit ARMarker_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : header(_alloc, _init),
    code(_alloc),
    corners(_alloc),
    camera_intrinsic(_alloc),
    camera_pose(_alloc, _init),
    pose_relative_to_camera(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->type = 0;
      this->code = "";
      this->corners.fill(geometry_msgs::msg::Vector3_<ContainerAllocator>{_alloc, _init});
      std::fill<typename std::array<double, 9>::iterator, double>(this->camera_intrinsic.begin(), this->camera_intrinsic.end(), 0.0);
      this->has_reliable_pose = false;
      this->marker_size_in_mm = 0.0;
    }
  }

  // field types and members
  using _header_type =
    std_msgs::msg::Header_<ContainerAllocator>;
  _header_type header;
  using _type_type =
    unsigned char;
  _type_type type;
  using _code_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _code_type code;
  using _corners_type =
    std::array<geometry_msgs::msg::Vector3_<ContainerAllocator>, 4>;
  _corners_type corners;
  using _camera_intrinsic_type =
    std::array<double, 9>;
  _camera_intrinsic_type camera_intrinsic;
  using _camera_pose_type =
    geometry_msgs::msg::Pose_<ContainerAllocator>;
  _camera_pose_type camera_pose;
  using _has_reliable_pose_type =
    bool;
  _has_reliable_pose_type has_reliable_pose;
  using _marker_size_in_mm_type =
    double;
  _marker_size_in_mm_type marker_size_in_mm;
  using _pose_relative_to_camera_type =
    geometry_msgs::msg::Pose_<ContainerAllocator>;
  _pose_relative_to_camera_type pose_relative_to_camera;

  // setters for named parameter idiom
  Type & set__header(
    const std_msgs::msg::Header_<ContainerAllocator> & _arg)
  {
    this->header = _arg;
    return *this;
  }
  Type & set__type(
    const unsigned char & _arg)
  {
    this->type = _arg;
    return *this;
  }
  Type & set__code(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->code = _arg;
    return *this;
  }
  Type & set__corners(
    const std::array<geometry_msgs::msg::Vector3_<ContainerAllocator>, 4> & _arg)
  {
    this->corners = _arg;
    return *this;
  }
  Type & set__camera_intrinsic(
    const std::array<double, 9> & _arg)
  {
    this->camera_intrinsic = _arg;
    return *this;
  }
  Type & set__camera_pose(
    const geometry_msgs::msg::Pose_<ContainerAllocator> & _arg)
  {
    this->camera_pose = _arg;
    return *this;
  }
  Type & set__has_reliable_pose(
    const bool & _arg)
  {
    this->has_reliable_pose = _arg;
    return *this;
  }
  Type & set__marker_size_in_mm(
    const double & _arg)
  {
    this->marker_size_in_mm = _arg;
    return *this;
  }
  Type & set__pose_relative_to_camera(
    const geometry_msgs::msg::Pose_<ContainerAllocator> & _arg)
  {
    this->pose_relative_to_camera = _arg;
    return *this;
  }

  // constant declarations
  static constexpr unsigned char TYPE_ARUCO =
    0;
  static constexpr unsigned char TYPE_QRCODE =
    1;

  // pointer types
  using RawPtr =
    iviz_msgs::msg::ARMarker_<ContainerAllocator> *;
  using ConstRawPtr =
    const iviz_msgs::msg::ARMarker_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::ARMarker_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      iviz_msgs::msg::ARMarker_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__iviz_msgs__msg__ARMarker
    std::shared_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__iviz_msgs__msg__ARMarker
    std::shared_ptr<iviz_msgs::msg::ARMarker_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ARMarker_ & other) const
  {
    if (this->header != other.header) {
      return false;
    }
    if (this->type != other.type) {
      return false;
    }
    if (this->code != other.code) {
      return false;
    }
    if (this->corners != other.corners) {
      return false;
    }
    if (this->camera_intrinsic != other.camera_intrinsic) {
      return false;
    }
    if (this->camera_pose != other.camera_pose) {
      return false;
    }
    if (this->has_reliable_pose != other.has_reliable_pose) {
      return false;
    }
    if (this->marker_size_in_mm != other.marker_size_in_mm) {
      return false;
    }
    if (this->pose_relative_to_camera != other.pose_relative_to_camera) {
      return false;
    }
    return true;
  }
  bool operator!=(const ARMarker_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ARMarker_

// alias to use template instance with default allocator
using ARMarker =
  iviz_msgs::msg::ARMarker_<std::allocator<void>>;

// constant definitions
template<typename ContainerAllocator>
constexpr unsigned char ARMarker_<ContainerAllocator>::TYPE_ARUCO;
template<typename ContainerAllocator>
constexpr unsigned char ARMarker_<ContainerAllocator>::TYPE_QRCODE;

}  // namespace msg

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__AR_MARKER__STRUCT_HPP_
