// generated from rosidl_generator_cpp/resource/idl__struct.hpp.em
// with input from test_msgs:msg/Arrays.idl
// generated code does not contain a copyright notice

#ifndef TEST_MSGS__MSG__DETAIL__ARRAYS__STRUCT_HPP_
#define TEST_MSGS__MSG__DETAIL__ARRAYS__STRUCT_HPP_

#include <rosidl_runtime_cpp/bounded_vector.hpp>
#include <rosidl_runtime_cpp/message_initialization.hpp>
#include <algorithm>
#include <array>
#include <memory>
#include <string>
#include <vector>


// Include directives for member types
// Member 'basic_types_values'
#include "test_msgs/msg/detail/basic_types__struct.hpp"
// Member 'constants_values'
#include "test_msgs/msg/detail/constants__struct.hpp"
// Member 'defaults_values'
#include "test_msgs/msg/detail/defaults__struct.hpp"

#ifndef _WIN32
# define DEPRECATED__test_msgs__msg__Arrays __attribute__((deprecated))
#else
# define DEPRECATED__test_msgs__msg__Arrays __declspec(deprecated)
#endif

namespace test_msgs
{

namespace msg
{

// message struct
template<class ContainerAllocator>
struct Arrays_
{
  using Type = Arrays_<ContainerAllocator>;

  explicit Arrays_(rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::DEFAULTS_ONLY == _init)
    {
      this->bool_values_default[0] = false;
      this->bool_values_default[1] = true;
      this->bool_values_default[2] = false;
      this->byte_values_default[0] = 0;
      this->byte_values_default[1] = 1;
      this->byte_values_default[2] = 255;
      this->char_values_default[0] = 0;
      this->char_values_default[1] = 1;
      this->char_values_default[2] = 127;
      this->float32_values_default[0] = 1.125f;
      this->float32_values_default[1] = 0.0f;
      this->float32_values_default[2] = -1.125f;
      this->float64_values_default[0] = 3.1415;
      this->float64_values_default[1] = 0.0;
      this->float64_values_default[2] = -3.1415;
      this->int8_values_default[0] = 0;
      this->int8_values_default[1] = 127;
      this->int8_values_default[2] = -128;
      this->uint8_values_default[0] = 0;
      this->uint8_values_default[1] = 1;
      this->uint8_values_default[2] = 255;
      this->int16_values_default[0] = 0;
      this->int16_values_default[1] = 32767;
      this->int16_values_default[2] = -32768;
      this->uint16_values_default[0] = 0;
      this->uint16_values_default[1] = 1;
      this->uint16_values_default[2] = 65535;
      this->int32_values_default[0] = 0l;
      this->int32_values_default[1] = 2147483647l;
      this->int32_values_default[2] = (-2147483647l - 1);
      this->uint32_values_default[0] = 0ul;
      this->uint32_values_default[1] = 1ul;
      this->uint32_values_default[2] = 4294967295ul;
      this->int64_values_default[0] = 0ll;
      this->int64_values_default[1] = 9223372036854775807ll;
      this->int64_values_default[2] = (-9223372036854775807ll - 1);
      this->uint64_values_default[0] = 0ull;
      this->uint64_values_default[1] = 1ull;
      this->uint64_values_default[2] = 18446744073709551615ull;
      this->string_values_default[0] = "";
      this->string_values_default[1] = "max value";
      this->string_values_default[2] = "min value";
    } else if (rosidl_runtime_cpp::MessageInitialization::ZERO == _init) {
      std::fill<typename std::array<bool, 3>::iterator, bool>(this->bool_values.begin(), this->bool_values.end(), false);
      std::fill<typename std::array<unsigned char, 3>::iterator, unsigned char>(this->byte_values.begin(), this->byte_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->char_values.begin(), this->char_values.end(), 0);
      std::fill<typename std::array<float, 3>::iterator, float>(this->float32_values.begin(), this->float32_values.end(), 0.0f);
      std::fill<typename std::array<double, 3>::iterator, double>(this->float64_values.begin(), this->float64_values.end(), 0.0);
      std::fill<typename std::array<int8_t, 3>::iterator, int8_t>(this->int8_values.begin(), this->int8_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->uint8_values.begin(), this->uint8_values.end(), 0);
      std::fill<typename std::array<int16_t, 3>::iterator, int16_t>(this->int16_values.begin(), this->int16_values.end(), 0);
      std::fill<typename std::array<uint16_t, 3>::iterator, uint16_t>(this->uint16_values.begin(), this->uint16_values.end(), 0);
      std::fill<typename std::array<int32_t, 3>::iterator, int32_t>(this->int32_values.begin(), this->int32_values.end(), 0l);
      std::fill<typename std::array<uint32_t, 3>::iterator, uint32_t>(this->uint32_values.begin(), this->uint32_values.end(), 0ul);
      std::fill<typename std::array<int64_t, 3>::iterator, int64_t>(this->int64_values.begin(), this->int64_values.end(), 0ll);
      std::fill<typename std::array<uint64_t, 3>::iterator, uint64_t>(this->uint64_values.begin(), this->uint64_values.end(), 0ull);
      std::fill<typename std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>::iterator, std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>(this->string_values.begin(), this->string_values.end(), "");
      std::fill<typename std::array<bool, 3>::iterator, bool>(this->bool_values_default.begin(), this->bool_values_default.end(), false);
      std::fill<typename std::array<unsigned char, 3>::iterator, unsigned char>(this->byte_values_default.begin(), this->byte_values_default.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->char_values_default.begin(), this->char_values_default.end(), 0);
      std::fill<typename std::array<float, 3>::iterator, float>(this->float32_values_default.begin(), this->float32_values_default.end(), 0.0f);
      std::fill<typename std::array<double, 3>::iterator, double>(this->float64_values_default.begin(), this->float64_values_default.end(), 0.0);
      std::fill<typename std::array<int8_t, 3>::iterator, int8_t>(this->int8_values_default.begin(), this->int8_values_default.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->uint8_values_default.begin(), this->uint8_values_default.end(), 0);
      std::fill<typename std::array<int16_t, 3>::iterator, int16_t>(this->int16_values_default.begin(), this->int16_values_default.end(), 0);
      std::fill<typename std::array<uint16_t, 3>::iterator, uint16_t>(this->uint16_values_default.begin(), this->uint16_values_default.end(), 0);
      std::fill<typename std::array<int32_t, 3>::iterator, int32_t>(this->int32_values_default.begin(), this->int32_values_default.end(), 0l);
      std::fill<typename std::array<uint32_t, 3>::iterator, uint32_t>(this->uint32_values_default.begin(), this->uint32_values_default.end(), 0ul);
      std::fill<typename std::array<int64_t, 3>::iterator, int64_t>(this->int64_values_default.begin(), this->int64_values_default.end(), 0ll);
      std::fill<typename std::array<uint64_t, 3>::iterator, uint64_t>(this->uint64_values_default.begin(), this->uint64_values_default.end(), 0ull);
      std::fill<typename std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>::iterator, std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>(this->string_values_default.begin(), this->string_values_default.end(), "");
      this->alignment_check = 0l;
    }
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      std::fill<typename std::array<bool, 3>::iterator, bool>(this->bool_values.begin(), this->bool_values.end(), false);
      std::fill<typename std::array<unsigned char, 3>::iterator, unsigned char>(this->byte_values.begin(), this->byte_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->char_values.begin(), this->char_values.end(), 0);
      std::fill<typename std::array<float, 3>::iterator, float>(this->float32_values.begin(), this->float32_values.end(), 0.0f);
      std::fill<typename std::array<double, 3>::iterator, double>(this->float64_values.begin(), this->float64_values.end(), 0.0);
      std::fill<typename std::array<int8_t, 3>::iterator, int8_t>(this->int8_values.begin(), this->int8_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->uint8_values.begin(), this->uint8_values.end(), 0);
      std::fill<typename std::array<int16_t, 3>::iterator, int16_t>(this->int16_values.begin(), this->int16_values.end(), 0);
      std::fill<typename std::array<uint16_t, 3>::iterator, uint16_t>(this->uint16_values.begin(), this->uint16_values.end(), 0);
      std::fill<typename std::array<int32_t, 3>::iterator, int32_t>(this->int32_values.begin(), this->int32_values.end(), 0l);
      std::fill<typename std::array<uint32_t, 3>::iterator, uint32_t>(this->uint32_values.begin(), this->uint32_values.end(), 0ul);
      std::fill<typename std::array<int64_t, 3>::iterator, int64_t>(this->int64_values.begin(), this->int64_values.end(), 0ll);
      std::fill<typename std::array<uint64_t, 3>::iterator, uint64_t>(this->uint64_values.begin(), this->uint64_values.end(), 0ull);
      std::fill<typename std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>::iterator, std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>(this->string_values.begin(), this->string_values.end(), "");
      this->basic_types_values.fill(test_msgs::msg::BasicTypes_<ContainerAllocator>{_init});
      this->constants_values.fill(test_msgs::msg::Constants_<ContainerAllocator>{_init});
      this->defaults_values.fill(test_msgs::msg::Defaults_<ContainerAllocator>{_init});
      this->alignment_check = 0l;
    }
  }

  explicit Arrays_(const ContainerAllocator & _alloc, rosidl_runtime_cpp::MessageInitialization _init = rosidl_runtime_cpp::MessageInitialization::ALL)
  : bool_values(_alloc),
    byte_values(_alloc),
    char_values(_alloc),
    float32_values(_alloc),
    float64_values(_alloc),
    int8_values(_alloc),
    uint8_values(_alloc),
    int16_values(_alloc),
    uint16_values(_alloc),
    int32_values(_alloc),
    uint32_values(_alloc),
    int64_values(_alloc),
    uint64_values(_alloc),
    string_values(_alloc),
    basic_types_values(_alloc),
    constants_values(_alloc),
    defaults_values(_alloc),
    bool_values_default(_alloc),
    byte_values_default(_alloc),
    char_values_default(_alloc),
    float32_values_default(_alloc),
    float64_values_default(_alloc),
    int8_values_default(_alloc),
    uint8_values_default(_alloc),
    int16_values_default(_alloc),
    uint16_values_default(_alloc),
    int32_values_default(_alloc),
    uint32_values_default(_alloc),
    int64_values_default(_alloc),
    uint64_values_default(_alloc),
    string_values_default(_alloc)
  {
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::DEFAULTS_ONLY == _init)
    {
      this->bool_values_default[0] = false;
      this->bool_values_default[1] = true;
      this->bool_values_default[2] = false;
      this->byte_values_default[0] = 0;
      this->byte_values_default[1] = 1;
      this->byte_values_default[2] = 255;
      this->char_values_default[0] = 0;
      this->char_values_default[1] = 1;
      this->char_values_default[2] = 127;
      this->float32_values_default[0] = 1.125f;
      this->float32_values_default[1] = 0.0f;
      this->float32_values_default[2] = -1.125f;
      this->float64_values_default[0] = 3.1415;
      this->float64_values_default[1] = 0.0;
      this->float64_values_default[2] = -3.1415;
      this->int8_values_default[0] = 0;
      this->int8_values_default[1] = 127;
      this->int8_values_default[2] = -128;
      this->uint8_values_default[0] = 0;
      this->uint8_values_default[1] = 1;
      this->uint8_values_default[2] = 255;
      this->int16_values_default[0] = 0;
      this->int16_values_default[1] = 32767;
      this->int16_values_default[2] = -32768;
      this->uint16_values_default[0] = 0;
      this->uint16_values_default[1] = 1;
      this->uint16_values_default[2] = 65535;
      this->int32_values_default[0] = 0l;
      this->int32_values_default[1] = 2147483647l;
      this->int32_values_default[2] = (-2147483647l - 1);
      this->uint32_values_default[0] = 0ul;
      this->uint32_values_default[1] = 1ul;
      this->uint32_values_default[2] = 4294967295ul;
      this->int64_values_default[0] = 0ll;
      this->int64_values_default[1] = 9223372036854775807ll;
      this->int64_values_default[2] = (-9223372036854775807ll - 1);
      this->uint64_values_default[0] = 0ull;
      this->uint64_values_default[1] = 1ull;
      this->uint64_values_default[2] = 18446744073709551615ull;
      this->string_values_default[0] = "";
      this->string_values_default[1] = "max value";
      this->string_values_default[2] = "min value";
    } else if (rosidl_runtime_cpp::MessageInitialization::ZERO == _init) {
      std::fill<typename std::array<bool, 3>::iterator, bool>(this->bool_values.begin(), this->bool_values.end(), false);
      std::fill<typename std::array<unsigned char, 3>::iterator, unsigned char>(this->byte_values.begin(), this->byte_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->char_values.begin(), this->char_values.end(), 0);
      std::fill<typename std::array<float, 3>::iterator, float>(this->float32_values.begin(), this->float32_values.end(), 0.0f);
      std::fill<typename std::array<double, 3>::iterator, double>(this->float64_values.begin(), this->float64_values.end(), 0.0);
      std::fill<typename std::array<int8_t, 3>::iterator, int8_t>(this->int8_values.begin(), this->int8_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->uint8_values.begin(), this->uint8_values.end(), 0);
      std::fill<typename std::array<int16_t, 3>::iterator, int16_t>(this->int16_values.begin(), this->int16_values.end(), 0);
      std::fill<typename std::array<uint16_t, 3>::iterator, uint16_t>(this->uint16_values.begin(), this->uint16_values.end(), 0);
      std::fill<typename std::array<int32_t, 3>::iterator, int32_t>(this->int32_values.begin(), this->int32_values.end(), 0l);
      std::fill<typename std::array<uint32_t, 3>::iterator, uint32_t>(this->uint32_values.begin(), this->uint32_values.end(), 0ul);
      std::fill<typename std::array<int64_t, 3>::iterator, int64_t>(this->int64_values.begin(), this->int64_values.end(), 0ll);
      std::fill<typename std::array<uint64_t, 3>::iterator, uint64_t>(this->uint64_values.begin(), this->uint64_values.end(), 0ull);
      std::fill<typename std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>::iterator, std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>(this->string_values.begin(), this->string_values.end(), "");
      std::fill<typename std::array<bool, 3>::iterator, bool>(this->bool_values_default.begin(), this->bool_values_default.end(), false);
      std::fill<typename std::array<unsigned char, 3>::iterator, unsigned char>(this->byte_values_default.begin(), this->byte_values_default.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->char_values_default.begin(), this->char_values_default.end(), 0);
      std::fill<typename std::array<float, 3>::iterator, float>(this->float32_values_default.begin(), this->float32_values_default.end(), 0.0f);
      std::fill<typename std::array<double, 3>::iterator, double>(this->float64_values_default.begin(), this->float64_values_default.end(), 0.0);
      std::fill<typename std::array<int8_t, 3>::iterator, int8_t>(this->int8_values_default.begin(), this->int8_values_default.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->uint8_values_default.begin(), this->uint8_values_default.end(), 0);
      std::fill<typename std::array<int16_t, 3>::iterator, int16_t>(this->int16_values_default.begin(), this->int16_values_default.end(), 0);
      std::fill<typename std::array<uint16_t, 3>::iterator, uint16_t>(this->uint16_values_default.begin(), this->uint16_values_default.end(), 0);
      std::fill<typename std::array<int32_t, 3>::iterator, int32_t>(this->int32_values_default.begin(), this->int32_values_default.end(), 0l);
      std::fill<typename std::array<uint32_t, 3>::iterator, uint32_t>(this->uint32_values_default.begin(), this->uint32_values_default.end(), 0ul);
      std::fill<typename std::array<int64_t, 3>::iterator, int64_t>(this->int64_values_default.begin(), this->int64_values_default.end(), 0ll);
      std::fill<typename std::array<uint64_t, 3>::iterator, uint64_t>(this->uint64_values_default.begin(), this->uint64_values_default.end(), 0ull);
      std::fill<typename std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>::iterator, std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>(this->string_values_default.begin(), this->string_values_default.end(), "");
      this->alignment_check = 0l;
    }
    if (rosidl_runtime_cpp::MessageInitialization::ALL == _init ||
      rosidl_runtime_cpp::MessageInitialization::ZERO == _init)
    {
      std::fill<typename std::array<bool, 3>::iterator, bool>(this->bool_values.begin(), this->bool_values.end(), false);
      std::fill<typename std::array<unsigned char, 3>::iterator, unsigned char>(this->byte_values.begin(), this->byte_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->char_values.begin(), this->char_values.end(), 0);
      std::fill<typename std::array<float, 3>::iterator, float>(this->float32_values.begin(), this->float32_values.end(), 0.0f);
      std::fill<typename std::array<double, 3>::iterator, double>(this->float64_values.begin(), this->float64_values.end(), 0.0);
      std::fill<typename std::array<int8_t, 3>::iterator, int8_t>(this->int8_values.begin(), this->int8_values.end(), 0);
      std::fill<typename std::array<uint8_t, 3>::iterator, uint8_t>(this->uint8_values.begin(), this->uint8_values.end(), 0);
      std::fill<typename std::array<int16_t, 3>::iterator, int16_t>(this->int16_values.begin(), this->int16_values.end(), 0);
      std::fill<typename std::array<uint16_t, 3>::iterator, uint16_t>(this->uint16_values.begin(), this->uint16_values.end(), 0);
      std::fill<typename std::array<int32_t, 3>::iterator, int32_t>(this->int32_values.begin(), this->int32_values.end(), 0l);
      std::fill<typename std::array<uint32_t, 3>::iterator, uint32_t>(this->uint32_values.begin(), this->uint32_values.end(), 0ul);
      std::fill<typename std::array<int64_t, 3>::iterator, int64_t>(this->int64_values.begin(), this->int64_values.end(), 0ll);
      std::fill<typename std::array<uint64_t, 3>::iterator, uint64_t>(this->uint64_values.begin(), this->uint64_values.end(), 0ull);
      std::fill<typename std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>::iterator, std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>>(this->string_values.begin(), this->string_values.end(), "");
      this->basic_types_values.fill(test_msgs::msg::BasicTypes_<ContainerAllocator>{_alloc, _init});
      this->constants_values.fill(test_msgs::msg::Constants_<ContainerAllocator>{_alloc, _init});
      this->defaults_values.fill(test_msgs::msg::Defaults_<ContainerAllocator>{_alloc, _init});
      this->alignment_check = 0l;
    }
  }

  // field types and members
  using _bool_values_type =
    std::array<bool, 3>;
  _bool_values_type bool_values;
  using _byte_values_type =
    std::array<unsigned char, 3>;
  _byte_values_type byte_values;
  using _char_values_type =
    std::array<uint8_t, 3>;
  _char_values_type char_values;
  using _float32_values_type =
    std::array<float, 3>;
  _float32_values_type float32_values;
  using _float64_values_type =
    std::array<double, 3>;
  _float64_values_type float64_values;
  using _int8_values_type =
    std::array<int8_t, 3>;
  _int8_values_type int8_values;
  using _uint8_values_type =
    std::array<uint8_t, 3>;
  _uint8_values_type uint8_values;
  using _int16_values_type =
    std::array<int16_t, 3>;
  _int16_values_type int16_values;
  using _uint16_values_type =
    std::array<uint16_t, 3>;
  _uint16_values_type uint16_values;
  using _int32_values_type =
    std::array<int32_t, 3>;
  _int32_values_type int32_values;
  using _uint32_values_type =
    std::array<uint32_t, 3>;
  _uint32_values_type uint32_values;
  using _int64_values_type =
    std::array<int64_t, 3>;
  _int64_values_type int64_values;
  using _uint64_values_type =
    std::array<uint64_t, 3>;
  _uint64_values_type uint64_values;
  using _string_values_type =
    std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>;
  _string_values_type string_values;
  using _basic_types_values_type =
    std::array<test_msgs::msg::BasicTypes_<ContainerAllocator>, 3>;
  _basic_types_values_type basic_types_values;
  using _constants_values_type =
    std::array<test_msgs::msg::Constants_<ContainerAllocator>, 3>;
  _constants_values_type constants_values;
  using _defaults_values_type =
    std::array<test_msgs::msg::Defaults_<ContainerAllocator>, 3>;
  _defaults_values_type defaults_values;
  using _bool_values_default_type =
    std::array<bool, 3>;
  _bool_values_default_type bool_values_default;
  using _byte_values_default_type =
    std::array<unsigned char, 3>;
  _byte_values_default_type byte_values_default;
  using _char_values_default_type =
    std::array<uint8_t, 3>;
  _char_values_default_type char_values_default;
  using _float32_values_default_type =
    std::array<float, 3>;
  _float32_values_default_type float32_values_default;
  using _float64_values_default_type =
    std::array<double, 3>;
  _float64_values_default_type float64_values_default;
  using _int8_values_default_type =
    std::array<int8_t, 3>;
  _int8_values_default_type int8_values_default;
  using _uint8_values_default_type =
    std::array<uint8_t, 3>;
  _uint8_values_default_type uint8_values_default;
  using _int16_values_default_type =
    std::array<int16_t, 3>;
  _int16_values_default_type int16_values_default;
  using _uint16_values_default_type =
    std::array<uint16_t, 3>;
  _uint16_values_default_type uint16_values_default;
  using _int32_values_default_type =
    std::array<int32_t, 3>;
  _int32_values_default_type int32_values_default;
  using _uint32_values_default_type =
    std::array<uint32_t, 3>;
  _uint32_values_default_type uint32_values_default;
  using _int64_values_default_type =
    std::array<int64_t, 3>;
  _int64_values_default_type int64_values_default;
  using _uint64_values_default_type =
    std::array<uint64_t, 3>;
  _uint64_values_default_type uint64_values_default;
  using _string_values_default_type =
    std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3>;
  _string_values_default_type string_values_default;
  using _alignment_check_type =
    int32_t;
  _alignment_check_type alignment_check;

  // setters for named parameter idiom
  Type & set__bool_values(
    const std::array<bool, 3> & _arg)
  {
    this->bool_values = _arg;
    return *this;
  }
  Type & set__byte_values(
    const std::array<unsigned char, 3> & _arg)
  {
    this->byte_values = _arg;
    return *this;
  }
  Type & set__char_values(
    const std::array<uint8_t, 3> & _arg)
  {
    this->char_values = _arg;
    return *this;
  }
  Type & set__float32_values(
    const std::array<float, 3> & _arg)
  {
    this->float32_values = _arg;
    return *this;
  }
  Type & set__float64_values(
    const std::array<double, 3> & _arg)
  {
    this->float64_values = _arg;
    return *this;
  }
  Type & set__int8_values(
    const std::array<int8_t, 3> & _arg)
  {
    this->int8_values = _arg;
    return *this;
  }
  Type & set__uint8_values(
    const std::array<uint8_t, 3> & _arg)
  {
    this->uint8_values = _arg;
    return *this;
  }
  Type & set__int16_values(
    const std::array<int16_t, 3> & _arg)
  {
    this->int16_values = _arg;
    return *this;
  }
  Type & set__uint16_values(
    const std::array<uint16_t, 3> & _arg)
  {
    this->uint16_values = _arg;
    return *this;
  }
  Type & set__int32_values(
    const std::array<int32_t, 3> & _arg)
  {
    this->int32_values = _arg;
    return *this;
  }
  Type & set__uint32_values(
    const std::array<uint32_t, 3> & _arg)
  {
    this->uint32_values = _arg;
    return *this;
  }
  Type & set__int64_values(
    const std::array<int64_t, 3> & _arg)
  {
    this->int64_values = _arg;
    return *this;
  }
  Type & set__uint64_values(
    const std::array<uint64_t, 3> & _arg)
  {
    this->uint64_values = _arg;
    return *this;
  }
  Type & set__string_values(
    const std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3> & _arg)
  {
    this->string_values = _arg;
    return *this;
  }
  Type & set__basic_types_values(
    const std::array<test_msgs::msg::BasicTypes_<ContainerAllocator>, 3> & _arg)
  {
    this->basic_types_values = _arg;
    return *this;
  }
  Type & set__constants_values(
    const std::array<test_msgs::msg::Constants_<ContainerAllocator>, 3> & _arg)
  {
    this->constants_values = _arg;
    return *this;
  }
  Type & set__defaults_values(
    const std::array<test_msgs::msg::Defaults_<ContainerAllocator>, 3> & _arg)
  {
    this->defaults_values = _arg;
    return *this;
  }
  Type & set__bool_values_default(
    const std::array<bool, 3> & _arg)
  {
    this->bool_values_default = _arg;
    return *this;
  }
  Type & set__byte_values_default(
    const std::array<unsigned char, 3> & _arg)
  {
    this->byte_values_default = _arg;
    return *this;
  }
  Type & set__char_values_default(
    const std::array<uint8_t, 3> & _arg)
  {
    this->char_values_default = _arg;
    return *this;
  }
  Type & set__float32_values_default(
    const std::array<float, 3> & _arg)
  {
    this->float32_values_default = _arg;
    return *this;
  }
  Type & set__float64_values_default(
    const std::array<double, 3> & _arg)
  {
    this->float64_values_default = _arg;
    return *this;
  }
  Type & set__int8_values_default(
    const std::array<int8_t, 3> & _arg)
  {
    this->int8_values_default = _arg;
    return *this;
  }
  Type & set__uint8_values_default(
    const std::array<uint8_t, 3> & _arg)
  {
    this->uint8_values_default = _arg;
    return *this;
  }
  Type & set__int16_values_default(
    const std::array<int16_t, 3> & _arg)
  {
    this->int16_values_default = _arg;
    return *this;
  }
  Type & set__uint16_values_default(
    const std::array<uint16_t, 3> & _arg)
  {
    this->uint16_values_default = _arg;
    return *this;
  }
  Type & set__int32_values_default(
    const std::array<int32_t, 3> & _arg)
  {
    this->int32_values_default = _arg;
    return *this;
  }
  Type & set__uint32_values_default(
    const std::array<uint32_t, 3> & _arg)
  {
    this->uint32_values_default = _arg;
    return *this;
  }
  Type & set__int64_values_default(
    const std::array<int64_t, 3> & _arg)
  {
    this->int64_values_default = _arg;
    return *this;
  }
  Type & set__uint64_values_default(
    const std::array<uint64_t, 3> & _arg)
  {
    this->uint64_values_default = _arg;
    return *this;
  }
  Type & set__string_values_default(
    const std::array<std::basic_string<char, std::char_traits<char>, typename ContainerAllocator::template rebind<char>::other>, 3> & _arg)
  {
    this->string_values_default = _arg;
    return *this;
  }
  Type & set__alignment_check(
    const int32_t & _arg)
  {
    this->alignment_check = _arg;
    return *this;
  }

  // constant declarations

  // pointer types
  using RawPtr =
    test_msgs::msg::Arrays_<ContainerAllocator> *;
  using ConstRawPtr =
    const test_msgs::msg::Arrays_<ContainerAllocator> *;
  using SharedPtr =
    std::shared_ptr<test_msgs::msg::Arrays_<ContainerAllocator>>;
  using ConstSharedPtr =
    std::shared_ptr<test_msgs::msg::Arrays_<ContainerAllocator> const>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::Arrays_<ContainerAllocator>>>
  using UniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::Arrays_<ContainerAllocator>, Deleter>;

  using UniquePtr = UniquePtrWithDeleter<>;

  template<typename Deleter = std::default_delete<
      test_msgs::msg::Arrays_<ContainerAllocator>>>
  using ConstUniquePtrWithDeleter =
    std::unique_ptr<test_msgs::msg::Arrays_<ContainerAllocator> const, Deleter>;
  using ConstUniquePtr = ConstUniquePtrWithDeleter<>;

  using WeakPtr =
    std::weak_ptr<test_msgs::msg::Arrays_<ContainerAllocator>>;
  using ConstWeakPtr =
    std::weak_ptr<test_msgs::msg::Arrays_<ContainerAllocator> const>;

  // pointer types similar to ROS 1, use SharedPtr / ConstSharedPtr instead
  // NOTE: Can't use 'using' here because GNU C++ can't parse attributes properly
  typedef DEPRECATED__test_msgs__msg__Arrays
    std::shared_ptr<test_msgs::msg::Arrays_<ContainerAllocator>>
    Ptr;
  typedef DEPRECATED__test_msgs__msg__Arrays
    std::shared_ptr<test_msgs::msg::Arrays_<ContainerAllocator> const>
    ConstPtr;

  // comparison operators
  bool operator==(const Arrays_ & other) const
  {
    if (this->bool_values != other.bool_values) {
      return false;
    }
    if (this->byte_values != other.byte_values) {
      return false;
    }
    if (this->char_values != other.char_values) {
      return false;
    }
    if (this->float32_values != other.float32_values) {
      return false;
    }
    if (this->float64_values != other.float64_values) {
      return false;
    }
    if (this->int8_values != other.int8_values) {
      return false;
    }
    if (this->uint8_values != other.uint8_values) {
      return false;
    }
    if (this->int16_values != other.int16_values) {
      return false;
    }
    if (this->uint16_values != other.uint16_values) {
      return false;
    }
    if (this->int32_values != other.int32_values) {
      return false;
    }
    if (this->uint32_values != other.uint32_values) {
      return false;
    }
    if (this->int64_values != other.int64_values) {
      return false;
    }
    if (this->uint64_values != other.uint64_values) {
      return false;
    }
    if (this->string_values != other.string_values) {
      return false;
    }
    if (this->basic_types_values != other.basic_types_values) {
      return false;
    }
    if (this->constants_values != other.constants_values) {
      return false;
    }
    if (this->defaults_values != other.defaults_values) {
      return false;
    }
    if (this->bool_values_default != other.bool_values_default) {
      return false;
    }
    if (this->byte_values_default != other.byte_values_default) {
      return false;
    }
    if (this->char_values_default != other.char_values_default) {
      return false;
    }
    if (this->float32_values_default != other.float32_values_default) {
      return false;
    }
    if (this->float64_values_default != other.float64_values_default) {
      return false;
    }
    if (this->int8_values_default != other.int8_values_default) {
      return false;
    }
    if (this->uint8_values_default != other.uint8_values_default) {
      return false;
    }
    if (this->int16_values_default != other.int16_values_default) {
      return false;
    }
    if (this->uint16_values_default != other.uint16_values_default) {
      return false;
    }
    if (this->int32_values_default != other.int32_values_default) {
      return false;
    }
    if (this->uint32_values_default != other.uint32_values_default) {
      return false;
    }
    if (this->int64_values_default != other.int64_values_default) {
      return false;
    }
    if (this->uint64_values_default != other.uint64_values_default) {
      return false;
    }
    if (this->string_values_default != other.string_values_default) {
      return false;
    }
    if (this->alignment_check != other.alignment_check) {
      return false;
    }
    return true;
  }
  bool operator!=(const Arrays_ & other) const
  {
    return !this->operator==(other);
  }
};  // struct Arrays_

// alias to use template instance with default allocator
using Arrays =
  test_msgs::msg::Arrays_<std::allocator<void>>;

// constant definitions

}  // namespace msg

}  // namespace test_msgs

#endif  // TEST_MSGS__MSG__DETAIL__ARRAYS__STRUCT_HPP_
