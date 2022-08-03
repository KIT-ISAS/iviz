// generated from rosidl_generator_cpp/resource/idl__traits.hpp.em
// with input from test_msgs:action/NestedMessage.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__ACTION__DETAIL__NESTED_MESSAGE__TRAITS_HPP_
#define TEST_MSGS__ACTION__DETAIL__NESTED_MESSAGE__TRAITS_HPP_

#include "test_msgs/action/detail/nested_message__struct.hpp"
#include <rosidl_runtime_cpp/traits.hpp>
#include <stdint.h>
#include <type_traits>

// Include directives for member types
// Member 'nested_field_no_pkg'
#include "test_msgs/msg/detail/builtins__traits.hpp"
// Member 'nested_field'
#include "test_msgs/msg/detail/basic_types__traits.hpp"
// Member 'nested_different_pkg'
#include "builtin_interfaces/msg/detail/time__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_Goal>()
{
  return "test_msgs::action::NestedMessage_Goal";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_Goal>()
{
  return "test_msgs/action/NestedMessage_Goal";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_Goal>
  : std::integral_constant<bool, has_fixed_size<builtin_interfaces::msg::Time>::value && has_fixed_size<test_msgs::msg::BasicTypes>::value && has_fixed_size<test_msgs::msg::Builtins>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_Goal>
  : std::integral_constant<bool, has_bounded_size<builtin_interfaces::msg::Time>::value && has_bounded_size<test_msgs::msg::BasicTypes>::value && has_bounded_size<test_msgs::msg::Builtins>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_Goal>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'nested_field_no_pkg'
// already included above
// #include "test_msgs/msg/detail/builtins__traits.hpp"
// Member 'nested_field'
// already included above
// #include "test_msgs/msg/detail/basic_types__traits.hpp"
// Member 'nested_different_pkg'
// already included above
// #include "builtin_interfaces/msg/detail/time__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_Result>()
{
  return "test_msgs::action::NestedMessage_Result";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_Result>()
{
  return "test_msgs/action/NestedMessage_Result";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_Result>
  : std::integral_constant<bool, has_fixed_size<builtin_interfaces::msg::Time>::value && has_fixed_size<test_msgs::msg::BasicTypes>::value && has_fixed_size<test_msgs::msg::Builtins>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_Result>
  : std::integral_constant<bool, has_bounded_size<builtin_interfaces::msg::Time>::value && has_bounded_size<test_msgs::msg::BasicTypes>::value && has_bounded_size<test_msgs::msg::Builtins>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_Result>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'nested_field_no_pkg'
// already included above
// #include "test_msgs/msg/detail/builtins__traits.hpp"
// Member 'nested_field'
// already included above
// #include "test_msgs/msg/detail/basic_types__traits.hpp"
// Member 'nested_different_pkg'
// already included above
// #include "builtin_interfaces/msg/detail/time__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_Feedback>()
{
  return "test_msgs::action::NestedMessage_Feedback";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_Feedback>()
{
  return "test_msgs/action/NestedMessage_Feedback";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_Feedback>
  : std::integral_constant<bool, has_fixed_size<builtin_interfaces::msg::Time>::value && has_fixed_size<test_msgs::msg::BasicTypes>::value && has_fixed_size<test_msgs::msg::Builtins>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_Feedback>
  : std::integral_constant<bool, has_bounded_size<builtin_interfaces::msg::Time>::value && has_bounded_size<test_msgs::msg::BasicTypes>::value && has_bounded_size<test_msgs::msg::Builtins>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_Feedback>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'goal_id'
#include "unique_identifier_msgs/msg/detail/uuid__traits.hpp"
// Member 'goal'
#include "test_msgs/action/detail/nested_message__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_SendGoal_Request>()
{
  return "test_msgs::action::NestedMessage_SendGoal_Request";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_SendGoal_Request>()
{
  return "test_msgs/action/NestedMessage_SendGoal_Request";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_SendGoal_Request>
  : std::integral_constant<bool, has_fixed_size<test_msgs::action::NestedMessage_Goal>::value && has_fixed_size<unique_identifier_msgs::msg::UUID>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_SendGoal_Request>
  : std::integral_constant<bool, has_bounded_size<test_msgs::action::NestedMessage_Goal>::value && has_bounded_size<unique_identifier_msgs::msg::UUID>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_SendGoal_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'stamp'
// already included above
// #include "builtin_interfaces/msg/detail/time__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_SendGoal_Response>()
{
  return "test_msgs::action::NestedMessage_SendGoal_Response";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_SendGoal_Response>()
{
  return "test_msgs/action/NestedMessage_SendGoal_Response";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_SendGoal_Response>
  : std::integral_constant<bool, has_fixed_size<builtin_interfaces::msg::Time>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_SendGoal_Response>
  : std::integral_constant<bool, has_bounded_size<builtin_interfaces::msg::Time>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_SendGoal_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_SendGoal>()
{
  return "test_msgs::action::NestedMessage_SendGoal";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_SendGoal>()
{
  return "test_msgs/action/NestedMessage_SendGoal";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_SendGoal>
  : std::integral_constant<
    bool,
    has_fixed_size<test_msgs::action::NestedMessage_SendGoal_Request>::value &&
    has_fixed_size<test_msgs::action::NestedMessage_SendGoal_Response>::value
  >
{
};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_SendGoal>
  : std::integral_constant<
    bool,
    has_bounded_size<test_msgs::action::NestedMessage_SendGoal_Request>::value &&
    has_bounded_size<test_msgs::action::NestedMessage_SendGoal_Response>::value
  >
{
};

template<>
struct is_service<test_msgs::action::NestedMessage_SendGoal>
  : std::true_type
{
};

template<>
struct is_service_request<test_msgs::action::NestedMessage_SendGoal_Request>
  : std::true_type
{
};

template<>
struct is_service_response<test_msgs::action::NestedMessage_SendGoal_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'goal_id'
// already included above
// #include "unique_identifier_msgs/msg/detail/uuid__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_GetResult_Request>()
{
  return "test_msgs::action::NestedMessage_GetResult_Request";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_GetResult_Request>()
{
  return "test_msgs/action/NestedMessage_GetResult_Request";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_GetResult_Request>
  : std::integral_constant<bool, has_fixed_size<unique_identifier_msgs::msg::UUID>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_GetResult_Request>
  : std::integral_constant<bool, has_bounded_size<unique_identifier_msgs::msg::UUID>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_GetResult_Request>
  : std::true_type {};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'result'
// already included above
// #include "test_msgs/action/detail/nested_message__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_GetResult_Response>()
{
  return "test_msgs::action::NestedMessage_GetResult_Response";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_GetResult_Response>()
{
  return "test_msgs/action/NestedMessage_GetResult_Response";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_GetResult_Response>
  : std::integral_constant<bool, has_fixed_size<test_msgs::action::NestedMessage_Result>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_GetResult_Response>
  : std::integral_constant<bool, has_bounded_size<test_msgs::action::NestedMessage_Result>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_GetResult_Response>
  : std::true_type {};

}  // namespace rosidl_generator_traits

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_GetResult>()
{
  return "test_msgs::action::NestedMessage_GetResult";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_GetResult>()
{
  return "test_msgs/action/NestedMessage_GetResult";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_GetResult>
  : std::integral_constant<
    bool,
    has_fixed_size<test_msgs::action::NestedMessage_GetResult_Request>::value &&
    has_fixed_size<test_msgs::action::NestedMessage_GetResult_Response>::value
  >
{
};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_GetResult>
  : std::integral_constant<
    bool,
    has_bounded_size<test_msgs::action::NestedMessage_GetResult_Request>::value &&
    has_bounded_size<test_msgs::action::NestedMessage_GetResult_Response>::value
  >
{
};

template<>
struct is_service<test_msgs::action::NestedMessage_GetResult>
  : std::true_type
{
};

template<>
struct is_service_request<test_msgs::action::NestedMessage_GetResult_Request>
  : std::true_type
{
};

template<>
struct is_service_response<test_msgs::action::NestedMessage_GetResult_Response>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits

// Include directives for member types
// Member 'goal_id'
// already included above
// #include "unique_identifier_msgs/msg/detail/uuid__traits.hpp"
// Member 'feedback'
// already included above
// #include "test_msgs/action/detail/nested_message__traits.hpp"

namespace rosidl_generator_traits
{

template<>
inline const char * data_type<test_msgs::action::NestedMessage_FeedbackMessage>()
{
  return "test_msgs::action::NestedMessage_FeedbackMessage";
}

template<>
inline const char * name<test_msgs::action::NestedMessage_FeedbackMessage>()
{
  return "test_msgs/action/NestedMessage_FeedbackMessage";
}

template<>
struct has_fixed_size<test_msgs::action::NestedMessage_FeedbackMessage>
  : std::integral_constant<bool, has_fixed_size<test_msgs::action::NestedMessage_Feedback>::value && has_fixed_size<unique_identifier_msgs::msg::UUID>::value> {};

template<>
struct has_bounded_size<test_msgs::action::NestedMessage_FeedbackMessage>
  : std::integral_constant<bool, has_bounded_size<test_msgs::action::NestedMessage_Feedback>::value && has_bounded_size<unique_identifier_msgs::msg::UUID>::value> {};

template<>
struct is_message<test_msgs::action::NestedMessage_FeedbackMessage>
  : std::true_type {};

}  // namespace rosidl_generator_traits


namespace rosidl_generator_traits
{

template<>
struct is_action<test_msgs::action::NestedMessage>
  : std::true_type
{
};

template<>
struct is_action_goal<test_msgs::action::NestedMessage_Goal>
  : std::true_type
{
};

template<>
struct is_action_result<test_msgs::action::NestedMessage_Result>
  : std::true_type
{
};

template<>
struct is_action_feedback<test_msgs::action::NestedMessage_Feedback>
  : std::true_type
{
};

}  // namespace rosidl_generator_traits


#endif  // TEST_MSGS__ACTION__DETAIL__NESTED_MESSAGE__TRAITS_HPP_
