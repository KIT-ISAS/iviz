// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from test_msgs:action/NestedMessage.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__ACTION__DETAIL__NESTED_MESSAGE__STRUCT_HPP_
#define TEST_MSGS__ACTION__DETAIL__NESTED_MESSAGE__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'nested_field_no_pkg'
#include "test_msgs/msg/detail/builtins__struct.hpp"
// Member 'nested_field'
#include "test_msgs/msg/detail/basic_types__struct.hpp"
// Member 'nested_different_pkg'
#include "builtin_interfaces/msg/detail/time__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_Goal __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_Goal __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_Goal_
{
  using Type = NestedMessage_Goal_<ContainerAllocator>;

  explicit NestedMessage_Goal_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : nested_field_no_pkg(_init),
    nested_field(_init),
    nested_different_pkg(_init)
  {
    (void)_init;
  }

  explicit NestedMessage_Goal_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : nested_field_no_pkg(_alloc, _init),
    nested_field(_alloc, _init),
    nested_different_pkg(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _nested_field_no_pkg_type =
    test_msgs::msg::Builtins_<ContainerAllocator>;
  _nested_field_no_pkg_type nested_field_no_pkg;
  using _nested_field_type =
    test_msgs::msg::BasicTypes_<ContainerAllocator>;
  _nested_field_type nested_field;
  using _nested_different_pkg_type =
    builtin_interfaces::msg::Time_<ContainerAllocator>;
  _nested_different_pkg_type nested_different_pkg;

  // setters for named parameter idiom
  Type & set__nested_field_no_pkg(
    const test_msgs::msg::Builtins_<ContainerAllocator> & _arg)
  {
    this->nested_field_no_pkg = _arg;
    return *this;
  }
  Type & set__nested_field(
    const test_msgs::msg::BasicTypes_<ContainerAllocator> & _arg)
  {
    this->nested_field = _arg;
    return *this;
  }
  Type & set__nested_different_pkg(
    const builtin_interfaces::msg::Time_<ContainerAllocator> & _arg)
  {
    this->nested_different_pkg = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_Goal_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_Goal_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_Goal_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_Goal_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_Goal
    std::shared_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_Goal
    std::shared_ptr<test_msgs::action::NestedMessage_Goal_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_Goal_ & other) const
  {
    if (this->nested_field_no_pkg != other.nested_field_no_pkg) {
      return false;
    }
    if (this->nested_field != other.nested_field) {
      return false;
    }
    if (this->nested_different_pkg != other.nested_different_pkg) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_Goal_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_Goal_

// alias to use template instance with default allocator
using NestedMessage_Goal =
  test_msgs::action::NestedMessage_Goal_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs


// Include directives for member types
// Member 'nested_field_no_pkg'
// already included above
// #include "test_msgs/msg/detail/builtins__struct.hpp"
// Member 'nested_field'
// already included above
// #include "test_msgs/msg/detail/basic_types__struct.hpp"
// Member 'nested_different_pkg'
// already included above
// #include "builtin_interfaces/msg/detail/time__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_Result __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_Result __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_Result_
{
  using Type = NestedMessage_Result_<ContainerAllocator>;

  explicit NestedMessage_Result_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : nested_field_no_pkg(_init),
    nested_field(_init),
    nested_different_pkg(_init)
  {
    (void)_init;
  }

  explicit NestedMessage_Result_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : nested_field_no_pkg(_alloc, _init),
    nested_field(_alloc, _init),
    nested_different_pkg(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _nested_field_no_pkg_type =
    test_msgs::msg::Builtins_<ContainerAllocator>;
  _nested_field_no_pkg_type nested_field_no_pkg;
  using _nested_field_type =
    test_msgs::msg::BasicTypes_<ContainerAllocator>;
  _nested_field_type nested_field;
  using _nested_different_pkg_type =
    builtin_interfaces::msg::Time_<ContainerAllocator>;
  _nested_different_pkg_type nested_different_pkg;

  // setters for named parameter idiom
  Type & set__nested_field_no_pkg(
    const test_msgs::msg::Builtins_<ContainerAllocator> & _arg)
  {
    this->nested_field_no_pkg = _arg;
    return *this;
  }
  Type & set__nested_field(
    const test_msgs::msg::BasicTypes_<ContainerAllocator> & _arg)
  {
    this->nested_field = _arg;
    return *this;
  }
  Type & set__nested_different_pkg(
    const builtin_interfaces::msg::Time_<ContainerAllocator> & _arg)
  {
    this->nested_different_pkg = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_Result_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_Result_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_Result_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_Result_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_Result
    std::shared_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_Result
    std::shared_ptr<test_msgs::action::NestedMessage_Result_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_Result_ & other) const
  {
    if (this->nested_field_no_pkg != other.nested_field_no_pkg) {
      return false;
    }
    if (this->nested_field != other.nested_field) {
      return false;
    }
    if (this->nested_different_pkg != other.nested_different_pkg) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_Result_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_Result_

// alias to use template instance with default allocator
using NestedMessage_Result =
  test_msgs::action::NestedMessage_Result_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs


// Include directives for member types
// Member 'nested_field_no_pkg'
// already included above
// #include "test_msgs/msg/detail/builtins__struct.hpp"
// Member 'nested_field'
// already included above
// #include "test_msgs/msg/detail/basic_types__struct.hpp"
// Member 'nested_different_pkg'
// already included above
// #include "builtin_interfaces/msg/detail/time__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_Feedback __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_Feedback __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_Feedback_
{
  using Type = NestedMessage_Feedback_<ContainerAllocator>;

  explicit NestedMessage_Feedback_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : nested_field_no_pkg(_init),
    nested_field(_init),
    nested_different_pkg(_init)
  {
    (void)_init;
  }

  explicit NestedMessage_Feedback_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : nested_field_no_pkg(_alloc, _init),
    nested_field(_alloc, _init),
    nested_different_pkg(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _nested_field_no_pkg_type =
    test_msgs::msg::Builtins_<ContainerAllocator>;
  _nested_field_no_pkg_type nested_field_no_pkg;
  using _nested_field_type =
    test_msgs::msg::BasicTypes_<ContainerAllocator>;
  _nested_field_type nested_field;
  using _nested_different_pkg_type =
    builtin_interfaces::msg::Time_<ContainerAllocator>;
  _nested_different_pkg_type nested_different_pkg;

  // setters for named parameter idiom
  Type & set__nested_field_no_pkg(
    const test_msgs::msg::Builtins_<ContainerAllocator> & _arg)
  {
    this->nested_field_no_pkg = _arg;
    return *this;
  }
  Type & set__nested_field(
    const test_msgs::msg::BasicTypes_<ContainerAllocator> & _arg)
  {
    this->nested_field = _arg;
    return *this;
  }
  Type & set__nested_different_pkg(
    const builtin_interfaces::msg::Time_<ContainerAllocator> & _arg)
  {
    this->nested_different_pkg = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_Feedback_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_Feedback_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_Feedback_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_Feedback_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_Feedback
    std::shared_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_Feedback
    std::shared_ptr<test_msgs::action::NestedMessage_Feedback_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_Feedback_ & other) const
  {
    if (this->nested_field_no_pkg != other.nested_field_no_pkg) {
      return false;
    }
    if (this->nested_field != other.nested_field) {
      return false;
    }
    if (this->nested_different_pkg != other.nested_different_pkg) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_Feedback_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_Feedback_

// alias to use template instance with default allocator
using NestedMessage_Feedback =
  test_msgs::action::NestedMessage_Feedback_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs


// Include directives for member types
// Member 'goal_id'
#include "unique_identifier_msgs/msg/detail/uuid__struct.hpp"
// Member 'goal'
#include "test_msgs/action/detail/nested_message__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Request __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Request __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_SendGoal_Request_
{
  using Type = NestedMessage_SendGoal_Request_<ContainerAllocator>;

  explicit NestedMessage_SendGoal_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : goal_id(_init),
    goal(_init)
  {
    (void)_init;
  }

  explicit NestedMessage_SendGoal_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : goal_id(_alloc, _init),
    goal(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _goal_id_type =
    unique_identifier_msgs::msg::UUID_<ContainerAllocator>;
  _goal_id_type goal_id;
  using _goal_type =
    test_msgs::action::NestedMessage_Goal_<ContainerAllocator>;
  _goal_type goal;

  // setters for named parameter idiom
  Type & set__goal_id(
    const unique_identifier_msgs::msg::UUID_<ContainerAllocator> & _arg)
  {
    this->goal_id = _arg;
    return *this;
  }
  Type & set__goal(
    const test_msgs::action::NestedMessage_Goal_<ContainerAllocator> & _arg)
  {
    this->goal = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Request
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Request
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_SendGoal_Request_ & other) const
  {
    if (this->goal_id != other.goal_id) {
      return false;
    }
    if (this->goal != other.goal) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_SendGoal_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_SendGoal_Request_

// alias to use template instance with default allocator
using NestedMessage_SendGoal_Request =
  test_msgs::action::NestedMessage_SendGoal_Request_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs


// Include directives for member types
// Member 'stamp'
// already included above
// #include "builtin_interfaces/msg/detail/time__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Response __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Response __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_SendGoal_Response_
{
  using Type = NestedMessage_SendGoal_Response_<ContainerAllocator>;

  explicit NestedMessage_SendGoal_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : stamp(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->accepted = false;
    }
  }

  explicit NestedMessage_SendGoal_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : stamp(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->accepted = false;
    }
  }

  // field types and members
  using _accepted_type =
    bool;
  _accepted_type accepted;
  using _stamp_type =
    builtin_interfaces::msg::Time_<ContainerAllocator>;
  _stamp_type stamp;

  // setters for named parameter idiom
  Type & set__accepted(
    const bool & _arg)
  {
    this->accepted = _arg;
    return *this;
  }
  Type & set__stamp(
    const builtin_interfaces::msg::Time_<ContainerAllocator> & _arg)
  {
    this->stamp = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Response
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_SendGoal_Response
    std::shared_ptr<test_msgs::action::NestedMessage_SendGoal_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_SendGoal_Response_ & other) const
  {
    if (this->accepted != other.accepted) {
      return false;
    }
    if (this->stamp != other.stamp) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_SendGoal_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_SendGoal_Response_

// alias to use template instance with default allocator
using NestedMessage_SendGoal_Response =
  test_msgs::action::NestedMessage_SendGoal_Response_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs

namespace test_msgs
{

namespace action
{

struct NestedMessage_SendGoal
{
  using Request = test_msgs::action::NestedMessage_SendGoal_Request;
  using Response = test_msgs::action::NestedMessage_SendGoal_Response;
};

}  // namespace action

}  // namespace test_msgs


// Include directives for member types
// Member 'goal_id'
// already included above
// #include "unique_identifier_msgs/msg/detail/uuid__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_GetResult_Request __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_GetResult_Request __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_GetResult_Request_
{
  using Type = NestedMessage_GetResult_Request_<ContainerAllocator>;

  explicit NestedMessage_GetResult_Request_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : goal_id(_init)
  {
    (void)_init;
  }

  explicit NestedMessage_GetResult_Request_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : goal_id(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _goal_id_type =
    unique_identifier_msgs::msg::UUID_<ContainerAllocator>;
  _goal_id_type goal_id;

  // setters for named parameter idiom
  Type & set__goal_id(
    const unique_identifier_msgs::msg::UUID_<ContainerAllocator> & _arg)
  {
    this->goal_id = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_GetResult_Request
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_GetResult_Request
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Request_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_GetResult_Request_ & other) const
  {
    if (this->goal_id != other.goal_id) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_GetResult_Request_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_GetResult_Request_

// alias to use template instance with default allocator
using NestedMessage_GetResult_Request =
  test_msgs::action::NestedMessage_GetResult_Request_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs


// Include directives for member types
// Member 'result'
// already included above
// #include "test_msgs/action/detail/nested_message__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_GetResult_Response __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_GetResult_Response __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_GetResult_Response_
{
  using Type = NestedMessage_GetResult_Response_<ContainerAllocator>;

  explicit NestedMessage_GetResult_Response_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : result(_init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->status = 0;
    }
  }

  explicit NestedMessage_GetResult_Response_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : result(_alloc, _init)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      this->status = 0;
    }
  }

  // field types and members
  using _status_type =
    int8_t;
  _status_type status;
  using _result_type =
    test_msgs::action::NestedMessage_Result_<ContainerAllocator>;
  _result_type result;

  // setters for named parameter idiom
  Type & set__status(
    const int8_t & _arg)
  {
    this->status = _arg;
    return *this;
  }
  Type & set__result(
    const test_msgs::action::NestedMessage_Result_<ContainerAllocator> & _arg)
  {
    this->result = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_GetResult_Response
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_GetResult_Response
    std::shared_ptr<test_msgs::action::NestedMessage_GetResult_Response_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_GetResult_Response_ & other) const
  {
    if (this->status != other.status) {
      return false;
    }
    if (this->result != other.result) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_GetResult_Response_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_GetResult_Response_

// alias to use template instance with default allocator
using NestedMessage_GetResult_Response =
  test_msgs::action::NestedMessage_GetResult_Response_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs

namespace test_msgs
{

namespace action
{

struct NestedMessage_GetResult
{
  using Request = test_msgs::action::NestedMessage_GetResult_Request;
  using Response = test_msgs::action::NestedMessage_GetResult_Response;
};

}  // namespace action

}  // namespace test_msgs


// Include directives for member types
// Member 'goal_id'
// already included above
// #include "unique_identifier_msgs/msg/detail/uuid__struct.hpp"
// Member 'feedback'
// already included above
// #include "test_msgs/action/detail/nested_message__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__action__NestedMessage_FeedbackMessage __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__action__NestedMessage_FeedbackMessage __declspec(deprecated)
#endif

namespace test_msgs
{

namespace action
{

// message struct
template<class ContainerAllocator>
struct NestedMessage_FeedbackMessage_
{
  using Type = NestedMessage_FeedbackMessage_<ContainerAllocator>;

  explicit NestedMessage_FeedbackMessage_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : goal_id(_init),
    feedback(_init)
  {
    (void)_init;
  }

  explicit NestedMessage_FeedbackMessage_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : goal_id(_alloc, _init),
    feedback(_alloc, _init)
  {
    (void)_init;
  }

  // field types and members
  using _goal_id_type =
    unique_identifier_msgs::msg::UUID_<ContainerAllocator>;
  _goal_id_type goal_id;
  using _feedback_type =
    test_msgs::action::NestedMessage_Feedback_<ContainerAllocator>;
  _feedback_type feedback;

  // setters for named parameter idiom
  Type & set__goal_id(
    const unique_identifier_msgs::msg::UUID_<ContainerAllocator> & _arg)
  {
    this->goal_id = _arg;
    return *this;
  }
  Type & set__feedback(
    const test_msgs::action::NestedMessage_Feedback_<ContainerAllocator> & _arg)
  {
    this->feedback = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__action__NestedMessage_FeedbackMessage
    std::shared_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__action__NestedMessage_FeedbackMessage
    std::shared_ptr<test_msgs::action::NestedMessage_FeedbackMessage_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const NestedMessage_FeedbackMessage_ & other) const
  {
    if (this->goal_id != other.goal_id) {
      return false;
    }
    if (this->feedback != other.feedback) {
      return false;
    }
    return true;
  }
  bool operator!=(const NestedMessage_FeedbackMessage_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct NestedMessage_FeedbackMessage_

// alias to use template instance with default allocator
using NestedMessage_FeedbackMessage =
  test_msgs::action::NestedMessage_FeedbackMessage_<std::allocator<void>>;

// constant definitions

}  // namespace action

}  // namespace test_msgs

#include "action_msgs/srv/cancel_goal.hpp"
#include "action_msgs/msg/goal_info.hpp"
#include "action_msgs/msg/goal_status_array.hpp"

namespace test_msgs
{

namespace action
{

struct NestedMessage
{
  /// The goal message defined in the action definition.
  using Goal = test_msgs::action::NestedMessage_Goal;
  /// The result message defined in the action definition.
  using Result = test_msgs::action::NestedMessage_Result;
  /// The feedback message defined in the action definition.
  using Feedback = test_msgs::action::NestedMessage_Feedback;

  struct Impl
  {
    /// The send_goal service using a wrapped version of the goal message as a request.
    using SendGoalService = test_msgs::action::NestedMessage_SendGoal;
    /// The get_result service using a wrapped version of the result message as a response.
    using GetResultService = test_msgs::action::NestedMessage_GetResult;
    /// The feedback message with generic fields which wraps the feedback message.
    using FeedbackMessage = test_msgs::action::NestedMessage_FeedbackMessage;

    /// The generic service to cancel a goal.
    using CancelGoalService = action_msgs::srv::CancelGoal;
    /// The generic message for the status of a goal.
    using GoalStatusMessage = action_msgs::msg::GoalStatusArray;
  };
};

typedef struct NestedMessage NestedMessage;

}  // namespace action

}  // namespace test_msgs

#endif  // TEST_MSGS__ACTION__DETAIL__NESTED_MESSAGE__STRUCT_HPP_
