// generated from rosidl_generator_cpp/resource/idl__builder.hpp.em
// with input from iviz_msgs:msg/Mesh.idl
// generated code does not contain a copyright notice

#ifndef IVIZ_MSGS__MSG__DETAIL__MESH__BUILDER_HPP_
#define IVIZ_MSGS__MSG__DETAIL__MESH__BUILDER_HPP_

#include "iviz_msgs/msg/detail/mesh__struct.hpp"
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <utility>


namespace iviz_msgs
{

namespace msg
{

namespace builder
{

class Init_Mesh_material_index
{
public:
  explicit Init_Mesh_material_index(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  ::iviz_msgs::msg::Mesh material_index(::iviz_msgs::msg::Mesh::_material_index_type arg)
  {
    msg_.material_index = std::move(arg);
    return std::move(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_faces
{
public:
  explicit Init_Mesh_faces(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  Init_Mesh_material_index faces(::iviz_msgs::msg::Mesh::_faces_type arg)
  {
    msg_.faces = std::move(arg);
    return Init_Mesh_material_index(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_color_channels
{
public:
  explicit Init_Mesh_color_channels(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  Init_Mesh_faces color_channels(::iviz_msgs::msg::Mesh::_color_channels_type arg)
  {
    msg_.color_channels = std::move(arg);
    return Init_Mesh_faces(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_tex_coords
{
public:
  explicit Init_Mesh_tex_coords(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  Init_Mesh_color_channels tex_coords(::iviz_msgs::msg::Mesh::_tex_coords_type arg)
  {
    msg_.tex_coords = std::move(arg);
    return Init_Mesh_color_channels(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_bi_tangents
{
public:
  explicit Init_Mesh_bi_tangents(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  Init_Mesh_tex_coords bi_tangents(::iviz_msgs::msg::Mesh::_bi_tangents_type arg)
  {
    msg_.bi_tangents = std::move(arg);
    return Init_Mesh_tex_coords(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_tangents
{
public:
  explicit Init_Mesh_tangents(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  Init_Mesh_bi_tangents tangents(::iviz_msgs::msg::Mesh::_tangents_type arg)
  {
    msg_.tangents = std::move(arg);
    return Init_Mesh_bi_tangents(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_normals
{
public:
  explicit Init_Mesh_normals(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  Init_Mesh_tangents normals(::iviz_msgs::msg::Mesh::_normals_type arg)
  {
    msg_.normals = std::move(arg);
    return Init_Mesh_tangents(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_vertices
{
public:
  explicit Init_Mesh_vertices(::iviz_msgs::msg::Mesh & msg)
  : msg_(msg)
  {}
  Init_Mesh_normals vertices(::iviz_msgs::msg::Mesh::_vertices_type arg)
  {
    msg_.vertices = std::move(arg);
    return Init_Mesh_normals(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

class Init_Mesh_name
{
public:
  Init_Mesh_name()
  : msg_(::rosidl_runtime_cpp::MessageInitialization::SKIP)
  {}
  Init_Mesh_vertices name(::iviz_msgs::msg::Mesh::_name_type arg)
  {
    msg_.name = std::move(arg);
    return Init_Mesh_vertices(msg_);
  }

private:
  ::iviz_msgs::msg::Mesh msg_;
};

}  // namespace builder

}  // namespace msg

template<typename MessageType>
auto build();

template<>
inline
auto build<::iviz_msgs::msg::Mesh>()
{
  return iviz_msgs::msg::builder::Init_Mesh_name();
}

}  // namespace iviz_msgs

#endif  // IVIZ_MSGS__MSG__DETAIL__MESH__BUILDER_HPP_
