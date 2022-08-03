// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from grid_map_msgs:srv/ProcessFile.idl
// generated code does not contain a copyright notice

#ifndef GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__STRUCT_HPP_
#define GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__srv__ProcessFile_Request __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__srv__ProcessFile_Request __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct ProcessFile_Request_
{
  using Type = ProcessFile_Request_<ContainerAllocator>;

  explicit ProcessFile_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->file_path = "";
      this->topic_name = "";
    }
  }

  explicit ProcessFile_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : file_path(_alloc),
    topic_name(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->file_path = "";
      this->topic_name = "";
    }
  }

  // field types and members
  using _file_path_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _file_path_type file_path;
  using _topic_name_type =
    std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>;
  _topic_name_type topic_name;

  // setters for named parameter idiom
  Type & set__file_path(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->file_path = _arg;
    return *this;
  }
  Type & set__topic_name(
    const std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other> & _arg)
  {
    this->topic_name = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__srv__ProcessFile_Request
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__srv__ProcessFile_Request
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ProcessFile_Request_ & other) const
  {
    if (this->file_path != other.file_path) {
      return false;
    }
    if (this->topic_name != other.topic_name) {
      return false;
    }
    return true;
  }
  bool operator!=(const ProcessFile_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ProcessFile_Request_

// alias to use template instance with default allocator
using ProcessFile_Request =
  grid_map_msgs::srv::ProcessFile_Request_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace grid_map_msgs


#ifndef _WIN32
# define DEPRECATED__grid_map_msgs__srv__ProcessFile_Response __attribute__((deprecated))
#else
# define DEPRECATED__grid_map_msgs__srv__ProcessFile_Response __declspec(deprecated)
#endif

namespace grid_map_msgs
{

namespace srv
{

// message struct
template<class ContainerAllocator>
struct ProcessFile_Response_
{
  using Type = ProcessFile_Response_<ContainerAllocator>;

  explicit ProcessFile_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
    }
  }

  explicit ProcessFile_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    (void)_alloc;
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->success = false;
    }
  }

  // field types and members
  using _success_type =
    bool;
  _success_type success;

  // setters for named parameter idiom
  Type & set__success(
    const bool & _arg)
  {
    this->success = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__grid_map_msgs__srv__ProcessFile_Response
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__grid_map_msgs__srv__ProcessFile_Response
    std::shared_ptr<grid_map_msgs::srv::ProcessFile_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const ProcessFile_Response_ & other) const
  {
    if (this->success != other.success) {
      return false;
    }
    return true;
  }
  bool operator!=(const ProcessFile_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct ProcessFile_Response_

// alias to use template instance with default allocator
using ProcessFile_Response =
  grid_map_msgs::srv::ProcessFile_Response_<std::allocator<void>>;

// constant definitions

}  // namespace srv

}  // namespace grid_map_msgs

namespace grid_map_msgs
{

namespace srv
{

struct ProcessFile
{
  using Request = grid_map_msgs::srv::ProcessFile_Request;
  using Response = grid_map_msgs::srv::ProcessFile_Response;
};

}  // namespace srv

}  // namespace grid_map_msgs

#endif  // GRID_MAP_MSGS__SRV__DETAIL__PROCESS_FILE__STRUCT_HPP_
