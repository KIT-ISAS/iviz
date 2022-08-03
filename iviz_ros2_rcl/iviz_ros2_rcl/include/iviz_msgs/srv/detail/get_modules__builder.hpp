// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:srv/GetModules.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__SRV__DETAIL__GET_MODULES__BUILDER_HPP_
#define IVIZ_MSGS__SRV__DETAIL__GET_MODULES__BUILDER_HPP_

#include "iviz_msgs/srv/detail/get_modules__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace srv
{


}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::GetModules_Request>()
{
  return ::iviz_msgs::srv::GetModules_Request(rosidl_runtime_cpp::MessageInitialization::ZERO);
}

}  // namespace iviz_msgs


namespace iviz_msgs
{

namespace srv
{

namespace builder
{

class Init_GetModules_Response_configs
{
public:
  Init_GetModules_Response_configs()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  ::iviz_msgs::srv::GetModules_Response configs(::iviz_msgs::srv::GetModules_Response::_configs_type arg)
  {
    msg_.configs = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::srv::GetModules_Response msg_;
};

}  // namespace builder

}  // namespace srv

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::srv::GetModules_Response>()
{
  return iviz_msgs::srv::builder::Init_GetModules_Response_configs();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__SRV__DETAIL__GET_MODULES__BUILDER_HPP_
