//
//  main.cpp
//  Ros2Test
//
//  Created by Antonio Zea on 10.07.22.
//

#include <unistd.h>

#include "iviz_ros2_rcl.h"

#include <string>
#include <map>
#include <vector>

#include <fastrtps/Domain.h>

#include <iostream>
#include <rcl/error_handling.h>
#include <rcl/node.h>
#include <rcl/graph.h>
#include <rcl/rcl.h>

#include <rcutils/logging.h>

#include <rosidl_runtime_c/string_functions.h>
#include <rmw/rmw.h>

#include <std_msgs/msg/string.h>
#include <std_msgs/msg/color_rgba.h>
#include <tf2_msgs/msg/tf_message.h>
#include <rcl_interfaces/msg/log.h>

#include <geometry_msgs/msg/transform.h>
#include <geometry_msgs/msg/transform_stamped.h>
#include <geometry_msgs/msg/pose.h>
#include <geometry_msgs/msg/pose_stamped.h>
#include <geometry_msgs/msg/point.h>
#include <geometry_msgs/msg/point_stamped.h>
#include <geometry_msgs/msg/wrench.h>
#include <geometry_msgs/msg/wrench_stamped.h>
#include <geometry_msgs/msg/twist.h>
#include <geometry_msgs/msg/twist_stamped.h>

#include <nav_msgs/msg/odometry.h>
#include <nav_msgs/msg/occupancy_grid.h>

#include <sensor_msgs/msg/point_cloud2.h>
#include <sensor_msgs/msg/image.h>
#include <sensor_msgs/msg/compressed_image.h>
#include <sensor_msgs/msg/laser_scan.h>
#include <sensor_msgs/msg/camera_info.h>
#include <sensor_msgs/msg/joy.h>

#include <visualization_msgs/msg/marker.h>
#include <visualization_msgs/msg/interactive_marker.h>
#include <visualization_msgs/msg/interactive_marker_feedback.h>
#include <visualization_msgs/msg/interactive_marker_update.h>
#include <visualization_msgs/msg/interactive_marker_init.h>


static char log_buffer[2048];

static void empty_handler(int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message) { }

static void (*rcl_external_logger) (int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message) = empty_handler;

struct interop_context
{
    rcl_context_t context;
    std::string message;
    std::vector<std::string> strings1 {32};
    std::vector<std::string> strings2 {32};
    std::vector<std::string> strings3 {32};
    
    std::vector<const char*> arrays1 {32};
    std::vector<const char*> arrays2 {32};
    std::vector<const char*> arrays3 {32};
    
    std::vector<std::array<char, RMW_GID_STORAGE_SIZE>> gids {32};
    std::vector<rmw_qos_profile_t> profiles {32};
};


#define MSGTYPE(a, b) (rosidl_message_type_support_t*) ROSIDL_GET_MSG_TYPE_SUPPORT(a, msg, b)
#define ENTRY(a, b) {#a "/" #b, MSGTYPE(a, b)}

static std::map<std::string, rosidl_message_type_support_t*> default_types
{
    ENTRY(std_msgs, String),
    ENTRY(tf2_msgs, TFMessage),
    ENTRY(rcl_interfaces, Log),
    ENTRY(geometry_msgs, Transform),
    ENTRY(geometry_msgs, TransformStamped),
    ENTRY(geometry_msgs, Pose),
    ENTRY(geometry_msgs, PoseStamped),
    ENTRY(geometry_msgs, Point),
    ENTRY(geometry_msgs, PointStamped),
    ENTRY(geometry_msgs, Wrench),
    ENTRY(geometry_msgs, WrenchStamped),
    ENTRY(geometry_msgs, Twist),
    ENTRY(geometry_msgs, TwistStamped),
    ENTRY(nav_msgs, Odometry),
    ENTRY(nav_msgs, OccupancyGrid),
    ENTRY(sensor_msgs, PointCloud2),
    ENTRY(sensor_msgs, Image),
    ENTRY(sensor_msgs, CompressedImage),
    ENTRY(sensor_msgs, LaserScan),
    ENTRY(sensor_msgs, CameraInfo),
    ENTRY(sensor_msgs, Joy),
    ENTRY(visualization_msgs, Marker),
    ENTRY(visualization_msgs, InteractiveMarker),
    ENTRY(visualization_msgs, InteractiveMarkerFeedback),
    ENTRY(visualization_msgs, InteractiveMarkerInit),
    ENTRY(visualization_msgs, InteractiveMarkerUpdate),
};

extern "C"
{

static void IgnoreRet(rcl_ret_t) {}

bool native_rcl_set_dds_profile_path(const char *path)
{
    return eprosima::fastrtps::Domain::loadXMLProfilesFile(path);
}

void *native_rcl_create_context()
{
    return new interop_context();
}

void native_rcl_destroy_context(void *context)
{
    delete (interop_context*) context;
}

int32_t native_rcl_init(void *context_handle)
{
    int num_args = 0;
    
    interop_context *context = (interop_context*) context_handle;
    
    context->context = rcl_get_zero_initialized_context();
    rcl_init_options_t init_options = rcl_get_zero_initialized_init_options();
    
    rcl_ret_t ret = rcl_init_options_init(&init_options, rcl_get_default_allocator());
    if (RCL_RET_OK != ret)
    {
        return ret;
    }
    
    rmw_init_options_t *rmw_init_options = rcl_init_options_get_rmw_init_options(&init_options);
    rmw_init_options->localhost_only = RMW_LOCALHOST_ONLY_DISABLED;
    
    const char ** arg_values = NULL;
    rcl_ret_t r_ret = rcl_init(num_args, arg_values, &init_options, &context->context);
    
    IgnoreRet(rcl_init_options_fini(&init_options));
    
    return r_ret;
}

int32_t native_rcl_shutdown(void *context_handle)
{
    interop_context *context = (interop_context*) context_handle;
    return rcl_shutdown(&context->context);
}

static void rcl_logging_output_handler (const rcutils_log_location_t *location, int severity,
                                        const char *name, rcutils_time_point_value_t timestamp,
                                        const char *format, va_list *args)
{
    if (rcl_external_logger == empty_handler)
    {
        return;
    }
    
    vsnprintf(log_buffer, sizeof(log_buffer), format, *args);
    rcl_external_logger(severity, name, timestamp, log_buffer);
}

int32_t native_rcl_init_logging()
{
    rcl_ret_t ret = rcutils_logging_initialize();
    if (RCL_RET_OK != ret)
    {
        return ret;
    }
    
    rcutils_logging_set_output_handler(rcl_logging_output_handler);
    
    return RCL_RET_OK;
}

void native_rcl_set_logging_level(int level)
{
    rcutils_logging_set_default_logger_level(level);
}

void native_rcl_set_logging_handler(void (*logger) (int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message))
{
    rcl_external_logger = logger == nullptr ? empty_handler : logger;
}

int32_t native_rcl_create_node_handle(void *context_handle, void **node_handle, const char *name, const char *name_space)
{
    interop_context *context = (interop_context*) context_handle;
    
    rcl_node_t *node = (rcl_node_t *)malloc(sizeof(rcl_node_t));
    *node = rcl_get_zero_initialized_node();
    
    rcl_node_options_t default_options = rcl_node_get_default_options();
    
    rcl_ret_t ret = rcl_node_init(node, name, name_space, &context->context, &default_options);
    *node_handle = node;
    return ret;
}

int32_t native_rcl_destroy_node_handle(void *node_handle)
{
    rcl_ret_t ret = rcl_node_fini((rcl_node_t*)node_handle);
    free(node_handle);
    return ret;
}

const char* native_rcl_get_fully_qualified_node_name(void *node_handle)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    return rcl_node_get_fully_qualified_name(node);
}

bool native_rcl_ok(void *context_handle)
{
    interop_context *context = (interop_context*) context_handle;
    return rcl_context_is_valid(&context->context);
}


void *native_rcl_create_guard(void *context_handle)
{
    interop_context *context = (interop_context*) context_handle;
    rcl_guard_condition_t *guard = (rcl_guard_condition_t *)malloc(sizeof(rcl_guard_condition_t));
    *guard = rcl_get_zero_initialized_guard_condition();
    
    rcl_guard_condition_options_t options = rcl_guard_condition_get_default_options();
    IgnoreRet(rcl_guard_condition_init(guard, &context->context, options));
    return guard;
}

int32_t native_rcl_destroy_guard(void *guard_handle)
{
    rcl_guard_condition_t *guard = (rcl_guard_condition_t *) guard_handle;
    rcl_ret_t ret = rcl_guard_condition_fini(guard);
    free(guard);
    return ret;
}

int32_t native_rcl_trigger_guard(void *guard_handle)
{
    rcl_guard_condition_t *guard = (rcl_guard_condition_t *) guard_handle;
    return rcl_trigger_guard_condition(guard);
}


void *native_rcl_create_wait_set()
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)malloc(sizeof(rcl_wait_set_t));
    *wait_set = rcl_get_zero_initialized_wait_set();
    return wait_set;
}

int32_t native_rcl_wait_set_init(void *context_handle,
                                 void *wait_set_handle,
                                 int32_t number_of_subscriptions,
                                 int32_t number_of_guard_conditions,
                                 int32_t number_of_timers,
                                 int32_t number_of_clients,
                                 int32_t number_of_services,
                                 int32_t number_of_events)
{
    interop_context *context = (interop_context*) context_handle;
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    
    rcl_ret_t ret = rcl_wait_set_init(wait_set, number_of_subscriptions, number_of_guard_conditions,
                                      number_of_timers, number_of_clients, number_of_services,
                                      number_of_events, &context->context, rcl_get_default_allocator());
    
    return ret;
}

static int32_t native_rcl_wait_set_clear(void *wait_set_handle)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    return rcl_wait_set_clear(wait_set);
}

static int32_t native_rcl_wait_set_add_subscription(void *wait_set_handle, void *subscription_handle)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    rcl_subscription_t *subscription = (rcl_subscription_t *)subscription_handle;
    return rcl_wait_set_add_subscription(wait_set, subscription, nullptr);
}

int32_t native_rcl_wait_clear_and_add(void *wait_set_handle,
                                      void **subscription_handles, int num_subscription_handles,
                                      void **guard_handles, int num_guard_handles)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    IgnoreRet(rcl_wait_set_clear(wait_set));
    
    for (int i = 0; i < num_subscription_handles; i++)
    {
        rcl_subscription_t *subscription = (rcl_subscription_t *)subscription_handles[i];
        IgnoreRet(rcl_wait_set_add_subscription(wait_set, subscription, nullptr));
    }
    
    for (int i = 0; i < num_guard_handles; i++)
    {
        rcl_guard_condition_t *guard = (rcl_guard_condition_t *)guard_handles[i];
        IgnoreRet(rcl_wait_set_add_guard_condition(wait_set, guard, nullptr));
    }
    
    return RCL_RET_OK;
}

int32_t native_rcl_wait(void *wait_set_handle, int32_t timeout_in_ms,
                        void ***subscription_handles, void ***guard_handles)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    rcl_ret_t ret = rcl_wait(wait_set, RCL_MS_TO_NS(timeout_in_ms));
    
    if (ret != RCL_RET_OK)
    {
        *subscription_handles = nullptr;
        *guard_handles = nullptr;
        return ret;
    }
    
    *subscription_handles = (void**) wait_set->subscriptions;
    *guard_handles = (void**) wait_set->guard_conditions;
    return RCL_RET_OK;
}


int32_t native_rcl_destroy_wait_set(void *wait_set_handle)
{
    rcl_ret_t ret = rcl_wait_set_fini((rcl_wait_set_t*) wait_set_handle);
    free(wait_set_handle);
    return ret;
}

const char* native_rcl_get_error_string(void *context_handle)
{
    interop_context *context = (interop_context*) context_handle;
    context->message = rcl_get_error_string().str;
    return context->message.c_str();
}

bool native_rcl_is_type_supported(const char *type)
{
    return default_types.count(type) != 0;
}

int32_t native_rcl_create_subscription_handle(void **subscription_handle,
                                              void *node_handle,
                                              const char *topic,
                                              const char *type,
                                              void *profile_handle)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    
    auto it{ default_types.find( type ) };
    
    if (it == default_types.end())
    {
        return -1;
    }
    
    const rosidl_message_type_support_t *ts = it->second;
    
    rcl_subscription_t *subscription = (rcl_subscription_t *)malloc(sizeof(rcl_subscription_t));
    *subscription = rcl_get_zero_initialized_subscription();
    
    rcl_subscription_options_t subscription_ops = rcl_subscription_get_default_options();
    rmw_qos_profile_t *profile = (rmw_qos_profile_t*) profile_handle;
    subscription_ops.qos = *profile;
    
    rcl_ret_t ret = rcl_subscription_init(subscription, node, ts, topic, &subscription_ops);
    
    *subscription_handle = (void *)subscription;
    
    return ret;
}

int32_t native_rcl_destroy_subscription_handle(void *subscription_handle, void *node_handle)
{
    rcl_subscription_t *subscription = (rcl_subscription_t *)subscription_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcl_ret_t ret = rcl_subscription_fini(subscription, node);
    free(subscription_handle);
    return ret;
}

int32_t native_rcl_subscription_get_publisher_count(void *subscription_handle, int32_t *count)
{
    rcl_subscription_t *subscription = (rcl_subscription_t *)subscription_handle;
    
    size_t tmp_count;
    rcl_ret_t ret = rcl_subscription_get_publisher_count(subscription, &tmp_count);
    *count = (int32_t) tmp_count;
    return ret;
}

int32_t native_rcl_take_serialized_message(void *subscription_handle, void *serialized_message, void **ptr, int32_t *length, void *gid)
{
    rcl_subscription_t * subscription = (rcl_subscription_t *)subscription_handle;
    rcl_serialized_message_t *message = (rcl_serialized_message_t *) serialized_message;
    
    rmw_message_info_t info;
    rcl_ret_t ret = rcl_take_serialized_message(subscription, message, &info, NULL);
    if (ret == RCL_RET_OK)
    {
        *ptr = message->buffer;
        *length = (int) message->buffer_length;
        memcpy(gid, info.publisher_gid.data, RMW_GID_STORAGE_SIZE);
    }
    else
    {
        *ptr = nullptr;
        *length = 0;
    }
    
    return ret;
}

int32_t native_rcl_create_serialized_message(void** message_handle)
{
    rcl_serialized_message_t *message = (rcl_serialized_message_t *)malloc(sizeof(rcl_serialized_message_t));
    *message = rmw_get_zero_initialized_serialized_message();
    
    *message_handle = message;
    
    rcutils_allocator_t allocator = rcl_get_default_allocator();
    return rmw_serialized_message_init(message, 0, &allocator);
}

int32_t native_rcl_ensure_serialized_message_size(void *message_handle, int32_t size, void **ptr)
{
    rcl_serialized_message_t* message = (rcl_serialized_message_t*) message_handle;
    if (message->buffer_capacity >= size)
    {
        message->buffer_length = size;
        *ptr = message->buffer;
        return RCL_RET_OK;
    }
    
    rcl_ret_t ret = rcutils_uint8_array_resize(message, size);
    message->buffer_length = size;
    *ptr = message->buffer;
    return ret;
}

int32_t native_rcl_destroy_serialized_message(void *message_handle)
{
    rcl_ret_t ret = rcutils_uint8_array_fini((rcl_serialized_message_t*) message_handle);
    free(message_handle);
    return ret;
}

int32_t native_rcl_create_publisher_handle(void **publisher_handle,
                                           void *node_handle,
                                           const char *topic,
                                           const char *type)
{
    auto it{ default_types.find( type ) };
    
    if (it == default_types.end())
    {
        return -1;
    }
    
    rcl_node_t *node = (rcl_node_t *)node_handle;
    
    const rosidl_message_type_support_t *ts = it->second;
    
    rcl_publisher_t *publisher = (rcl_publisher_t *)malloc(sizeof(rcl_publisher_t));
    *publisher = rcl_get_zero_initialized_publisher();
    
    rcl_publisher_options_t publisher_ops = rcl_publisher_get_default_options();
    
    rcl_ret_t ret = rcl_publisher_init(publisher, node, ts, topic, &publisher_ops);
    
    *publisher_handle = (void *)publisher;
    
    return ret;
}

int32_t native_rcl_destroy_publisher_handle(void *publisher_handle, void *node_handle)
{
    rcl_publisher_t * publisher = (rcl_publisher_t *)publisher_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcl_ret_t ret = rcl_publisher_fini(publisher, node);
    free(publisher_handle);
    return ret;
}

int32_t native_rcl_publisher_get_subscription_count(void *publisher_handle, int32_t *count)
{
    rcl_publisher_t *publisher = (rcl_publisher_t *)publisher_handle;
    
    size_t tmp_count;
    rcl_ret_t ret = rcl_publisher_get_subscription_count(publisher, &tmp_count);
    *count = (int32_t) tmp_count;
    return ret;
}

int32_t native_rcl_publish(void *publisher_handle, void *raw_ros_message)
{
    rcl_publisher_t * publisher = (rcl_publisher_t *)publisher_handle;
    return rcl_publish(publisher, raw_ros_message, nullptr);
}

int32_t native_rcl_publish_serialized_message(void *publisher_handle, void *serialized_message_handle)
{
    rcl_publisher_t *publisher = (rcl_publisher_t *)publisher_handle;
    rcl_serialized_message_t *serialized_message = (rcl_serialized_message_t *)serialized_message_handle;
    return rcl_publish_serialized_message(publisher, serialized_message, nullptr);
}


int32_t native_rcl_get_node_names(void *context_handle, void *node_handle,
                                  const char*** node_names_handle, int32_t *num_node_names,
                                  const char*** node_namespaces_handle, int32_t *num_node_namespaces)
{
    interop_context *context = (interop_context*) context_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcutils_allocator_t allocator = rcl_get_default_allocator();
    
    rcutils_string_array_t node_names = rcutils_get_zero_initialized_string_array();
    rcutils_string_array_t node_namespaces = rcutils_get_zero_initialized_string_array();
    
    rcl_ret_t ret = rcl_get_node_names(node, allocator, &node_names, &node_namespaces);
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    if (node_names.size == 0)
    {
        IgnoreRet(rcutils_string_array_fini(&node_names));
        IgnoreRet(rcutils_string_array_fini(&node_namespaces));

        *node_names_handle = nullptr;
        *node_namespaces_handle = nullptr;
        *num_node_names = 0;
        *num_node_namespaces = 0;
        return RCL_RET_OK;
    }
    
    context->strings1.clear();
    context->arrays1.clear();
    context->strings2.clear();
    context->arrays2.clear();
    
    context->strings1.reserve(node_names.size);
    context->arrays1.reserve(node_names.size);
    
    context->strings2.reserve(node_namespaces.size);
    context->arrays2.reserve(node_namespaces.size);
    
    for (int i = 0; i < node_names.size; i++)
    {
        context->strings1.push_back(node_names.data[i]);
        context->arrays1.push_back(context->strings1.back().data());
    }
    
    for (int i = 0; i < node_namespaces.size; i++)
    {
        context->strings2.push_back(node_namespaces.data[i]);
        context->arrays2.push_back(context->strings2.back().data());
    }
    
    IgnoreRet(rcutils_string_array_fini(&node_names));
    IgnoreRet(rcutils_string_array_fini(&node_namespaces));
    
    *node_names_handle = context->arrays1.data();
    *node_namespaces_handle = context->arrays2.data();
    
    *num_node_names = (int32_t) context->arrays1.size();
    *num_node_namespaces = (int32_t) context->arrays2.size();
    
    return RCL_RET_OK;
}

int32_t native_rcl_get_topic_names_and_types(void *context_handle, void *node_handle,
                                             const char*** topic_names_handle,
                                             const char*** topic_types_handle, int32_t *num_topic_types)
{
    
    interop_context *context = (interop_context*) context_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcutils_allocator_t allocator = rcl_get_default_allocator();
    
    rcl_names_and_types_t topic_names_and_types = rmw_get_zero_initialized_names_and_types();
    
    rcl_ret_t ret = rcl_get_topic_names_and_types (node, &allocator, false, &topic_names_and_types);
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    if (topic_names_and_types.names.size == 0)
    {
        IgnoreRet(rmw_names_and_types_fini(&topic_names_and_types));
        
        *topic_names_handle = nullptr;
        *topic_types_handle = nullptr;
        *num_topic_types = 0;
        return RCL_RET_OK;
    }
    
    context->strings1.clear();
    context->arrays1.clear();
    context->strings2.clear();
    context->arrays2.clear();
    
    context->strings1.reserve(topic_names_and_types.names.size);
    context->arrays1.reserve(topic_names_and_types.names.size);
    context->strings2.reserve(topic_names_and_types.names.size);
    context->arrays2.reserve(topic_names_and_types.names.size);
    
    for (int i = 0; i < topic_names_and_types.names.size; i++)
    {
        context->strings1.push_back(topic_names_and_types.names.data[i]);
        context->arrays1.push_back(context->strings1.back().data());
        
        rcutils_string_array_t *nat = &topic_names_and_types.types[i];
        const char *type = nat->size != 0 ? nat->data[0] : "";
        
        context->strings2.push_back(type);
        context->arrays2.push_back(context->strings2.back().data());
    }
    
    IgnoreRet(rmw_names_and_types_fini(&topic_names_and_types));
    
    *topic_names_handle = context->arrays1.data();
    *topic_types_handle = context->arrays2.data();
    
    *num_topic_types = (int32_t) context->arrays2.size();
    
    return RCL_RET_OK;
}

int32_t native_rcl_get_service_names_and_types(void *context_handle, void *node_handle,
                                               const char*** service_names_handle,
                                               const char*** service_types_handle, int32_t *num_service_types)
{
    
    interop_context *context = (interop_context*) context_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcutils_allocator_t allocator = rcl_get_default_allocator();
    
    rcl_names_and_types_t topic_names_and_types = rmw_get_zero_initialized_names_and_types();
    
    rcl_ret_t ret = rcl_get_service_names_and_types(node, &allocator, &topic_names_and_types);
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    if (topic_names_and_types.names.size == 0)
    {
        IgnoreRet(rmw_names_and_types_fini(&topic_names_and_types));
        
        *service_names_handle = nullptr;
        *service_types_handle = nullptr;
        *num_service_types = 0;
        return RCL_RET_OK;
    }
    
    
    context->strings1.clear();
    context->arrays1.clear();
    context->strings2.clear();
    context->arrays2.clear();
    
    context->strings1.reserve(topic_names_and_types.names.size);
    context->arrays1.reserve(topic_names_and_types.names.size);
    context->strings2.reserve(topic_names_and_types.types->size);
    context->arrays2.reserve(topic_names_and_types.types->size);
    
    for (int i = 0; i < topic_names_and_types.names.size; i++)
    {
        context->strings1.push_back(topic_names_and_types.names.data[i]);
        context->arrays1.push_back(context->strings1.back().data());

        rcutils_string_array_t *nat = &topic_names_and_types.types[i];
        const char *type = nat->size != 0 ? nat->data[0] : "";
        
        context->strings2.push_back(type);
        context->arrays2.push_back(context->strings2.back().data());
    }
    
    IgnoreRet(rmw_names_and_types_fini(&topic_names_and_types));
    
    *service_names_handle = context->arrays1.data();
    *service_types_handle = context->arrays2.data();
    
    *num_service_types = (int32_t) context->arrays1.size();
    
    return RCL_RET_OK;
}

int32_t native_rcl_get_service_names_and_types_by_node(void *context_handle, void *node_handle,
                                                       char *node_name, char *node_namespace,
                                                       const char*** service_names_handle,
                                                       const char*** service_types_handle, int32_t *num_service_types)
{
    
    interop_context *context = (interop_context*) context_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcutils_allocator_t allocator = rcl_get_default_allocator();
    
    rcl_names_and_types_t topic_names_and_types = rmw_get_zero_initialized_names_and_types();
    
    rcl_ret_t ret = rcl_get_service_names_and_types_by_node(node, &allocator, node_name,
                                                            node_namespace, &topic_names_and_types);
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    if (topic_names_and_types.names.size == 0)
    {
        IgnoreRet(rmw_names_and_types_fini(&topic_names_and_types));
        
        *service_names_handle = nullptr;
        *service_types_handle = nullptr;
        *num_service_types = 0;
        return RCL_RET_OK;
    }
    
    context->strings1.clear();
    context->arrays1.clear();
    context->strings2.clear();
    context->arrays2.clear();
    
    context->strings1.reserve(topic_names_and_types.names.size);
    context->arrays1.reserve(topic_names_and_types.names.size);
    context->strings2.reserve(topic_names_and_types.types->size);
    context->arrays2.reserve(topic_names_and_types.types->size);
    
    for (int i = 0; i < topic_names_and_types.names.size; i++)
    {
        context->strings1.push_back(topic_names_and_types.names.data[i]);
        context->arrays1.push_back(context->strings1.back().data());

        rcutils_string_array_t *nat = &topic_names_and_types.types[i];
        const char *type = nat->size != 0 ? nat->data[0] : "";
        
        context->strings2.push_back(type);
        context->arrays2.push_back(context->strings2.back().data());
    }
    
    IgnoreRet(rmw_names_and_types_fini(&topic_names_and_types));
    
    *service_names_handle = context->arrays1.data();
    *service_types_handle = context->arrays2.data();
    
    *num_service_types = (int32_t) context->arrays2.size();
    
    return RCL_RET_OK;
}

int32_t native_rcl_get_publishers_info_by_topic(void *context_handle, void *node_handle, char *topic_name,
                                                const char ***node_names_handle,
                                                const char ***node_namespaces_handle,
                                                const char ***topic_types_handle,
                                                char **gid_handle,
                                                void **profiles_handle, int32_t *num_nodes)
{
    
    interop_context *context = (interop_context*) context_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcutils_allocator_t allocator = rcl_get_default_allocator();
    
    rcl_topic_endpoint_info_array_t topic_infos = rmw_get_zero_initialized_topic_endpoint_info_array();
    
    rcl_ret_t ret = rcl_get_publishers_info_by_topic(node, &allocator, topic_name, false, &topic_infos);
    
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    if (topic_infos.size == 0)
    {
        IgnoreRet(rmw_topic_endpoint_info_array_fini(&topic_infos, &allocator));
        
        *node_names_handle = nullptr;
        *node_namespaces_handle = nullptr;
        *topic_types_handle = nullptr;
        *gid_handle = nullptr;
        *profiles_handle = nullptr;
        return RCL_RET_OK;
    }
    
    context->strings1.clear();
    context->arrays1.clear();
    context->strings2.clear();
    context->arrays2.clear();
    context->strings3.clear();
    context->arrays3.clear();
    context->gids.clear();
    context->profiles.clear();

    context->strings1.reserve(topic_infos.size);
    context->arrays1.reserve(topic_infos.size);
    context->strings2.reserve(topic_infos.size);
    context->arrays2.reserve(topic_infos.size);
    context->strings3.reserve(topic_infos.size);
    context->arrays3.reserve(topic_infos.size);
    context->gids.reserve(topic_infos.size);
    context->profiles.reserve(topic_infos.size);

    
    for (int i = 0; i < topic_infos.size; i++)
    {
        context->strings1.push_back(topic_infos.info_array[i].node_name);
        context->arrays1.push_back(context->strings1.back().data());
        
        context->strings2.push_back(topic_infos.info_array[i].node_namespace);
        context->arrays2.push_back(context->strings2.back().data());
        
        context->strings3.push_back(topic_infos.info_array[i].topic_type);
        context->arrays3.push_back(context->strings3.back().data());
        
        std::array<char, RMW_GID_STORAGE_SIZE> array;
        memcpy(array.data(), topic_infos.info_array[i].endpoint_gid, RMW_GID_STORAGE_SIZE);
        context->gids.push_back(array);
        
        context->profiles.push_back(topic_infos.info_array[i].qos_profile);
    }
    
    IgnoreRet(rmw_topic_endpoint_info_array_fini(&topic_infos, &allocator));
    
    *node_names_handle = context->arrays1.data();
    *node_namespaces_handle = context->arrays2.data();
    *topic_types_handle = context->arrays3.data();
    *gid_handle = context->gids[0].data();
    *profiles_handle = (void**) context->profiles.data();
    
    *num_nodes = (int32_t) context->gids.size();
    
    return RCL_RET_OK;
}

int32_t native_rcl_get_subscribers_info_by_topic(void *context_handle, void *node_handle, char *topic_name,
                                                 const char ***node_names_handle,
                                                 const char ***node_namespaces_handle,
                                                 const char ***topic_types_handle,
                                                 char **gid_handle,
                                                 void **profiles_handle, int32_t *num_nodes)
{
    interop_context *context = (interop_context*) context_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcutils_allocator_t allocator = rcl_get_default_allocator();
    
    rcl_topic_endpoint_info_array_t topic_infos = rmw_get_zero_initialized_topic_endpoint_info_array();
    
    rcl_ret_t ret = rcl_get_subscriptions_info_by_topic(node, &allocator, topic_name, false, &topic_infos);
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    if (topic_infos.size == 0)
    {
        IgnoreRet(rmw_topic_endpoint_info_array_fini(&topic_infos, &allocator));
        
        *node_names_handle = nullptr;
        *node_namespaces_handle = nullptr;
        *topic_types_handle = nullptr;
        *gid_handle = 0;
        return RCL_RET_OK;
    }
    
    context->strings1.clear();
    context->arrays1.clear();
    context->strings2.clear();
    context->arrays2.clear();
    context->strings3.clear();
    context->arrays3.clear();
    context->gids.clear();
    context->profiles.clear();
    
    context->strings1.reserve(topic_infos.size);
    context->arrays1.reserve(topic_infos.size);
    context->strings2.reserve(topic_infos.size);
    context->arrays2.reserve(topic_infos.size);
    context->strings3.reserve(topic_infos.size);
    context->arrays3.reserve(topic_infos.size);
    context->gids.reserve(topic_infos.size);
    context->profiles.reserve(topic_infos.size);
    
    
    for (int i = 0; i < topic_infos.size; i++)
    {
        context->strings1.push_back(topic_infos.info_array[i].node_name);
        context->arrays1.push_back(context->strings1.back().data());
        
        context->strings2.push_back(topic_infos.info_array[i].node_namespace);
        context->arrays2.push_back(context->strings2.back().data());
        
        context->strings3.push_back(topic_infos.info_array[i].topic_type);
        context->arrays3.push_back(context->strings3.back().data());
        
        std::array<char, RMW_GID_STORAGE_SIZE> array;
        memcpy(array.data(), topic_infos.info_array[i].endpoint_gid, RMW_GID_STORAGE_SIZE);
        context->gids.push_back(array);
        
        context->profiles.push_back(topic_infos.info_array[i].qos_profile);
    }
    
    IgnoreRet(rmw_topic_endpoint_info_array_fini(&topic_infos, &allocator));
    
    *node_names_handle = context->arrays1.data();
    *node_namespaces_handle = context->arrays2.data();
    *topic_types_handle = context->arrays3.data();
    *gid_handle = context->gids[0].data();
    
    *profiles_handle = (void**) context->profiles.data();
    
    *num_nodes = (int32_t) context->gids.size();
    
    return RCL_RET_OK;
}

int32_t native_rcl_count_publishers(void *node_handle, char *topic_name, int32_t *count)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    size_t count_large;
    rcl_ret_t ret = rcl_count_publishers(node, topic_name, &count_large);
    *count = (int32_t) count_large;
    return ret;
}

int32_t native_rcl_count_subscribers(void *node_handle, char *topic_name, int32_t *count)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    size_t count_large;
    rcl_ret_t ret = rcl_count_subscribers(node, topic_name, &count_large);
    *count = (int32_t) count_large;
    return ret;
}




void default_handler(int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message)
{
    std::cout << "[" << name << "] " << message << std::endl;
}


int main_publish()
{
    std::cout << "running..." << std::endl;
    
    interop_context context;
    
    native_rcl_init(&context);
    native_rcl_init_logging();
    
    native_rcl_set_logging_level(RCUTILS_LOG_SEVERITY_DEBUG);
    native_rcl_set_logging_handler(default_handler);
    
    
    rcl_node_t *node_handle;
    rcl_ret_t ret = native_rcl_create_node_handle(&context, (void**)&node_handle, "test", "");
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to create node" << std::endl;
        return 1;
    }
    
    rcl_publisher_t* publisher_handle;
    
    ret = native_rcl_create_publisher_handle((void**)&publisher_handle, node_handle, "my_chatter", "tf2_msgs/TFMessage");
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to initialize publisher" << std::endl;
    }
    
    /*
     //geometry_msgs__msg__TransformStamped v;
     geometry_msgs__msg__PolygonStamped v;
     geometry_msgs__msg__PolygonStamped__init(&v);
     
     rosidl_runtime_c__String a;
     rosidl_runtime_c__String__init(&a);
     rosidl_runtime_c__String__assign(&a, "abcd");
     
     rosidl_runtime_c__String b;
     rosidl_runtime_c__String__init(&b);
     rosidl_runtime_c__String__assign(&b, "childr");
     
     
     geometry_msgs__msg__Point32__Sequence s;
     geometry_msgs__msg__Point32__Sequence__init(&s, 5);
     s.data[0].y = 1;
     
     v.header.frame_id = a;
     v.polygon.points = s;
     */
    
    tf2_msgs__msg__TFMessage v;
    tf2_msgs__msg__TFMessage__init(&v);
    
    geometry_msgs__msg__TransformStamped__Sequence__init(&v.transforms, 3);
    
    rosidl_runtime_c__String a;
    rosidl_runtime_c__String__init(&a);
    rosidl_runtime_c__String__assign(&a, "abcd");
    
    v.transforms.data[0].child_frame_id = a;
    
    
    /*
     v.child_frame_id = b;
     v.transform.translation.x = 1;
     v.transform.translation.y = 0;
     v.transform.translation.z = 1;
     
     v.transform.rotation.x = 0;
     v.transform.rotation.y = 0;
     v.transform.rotation.z = 0;
     v.transform.rotation.w = 1;
     */
    
    
    
    while (native_rcl_ok(&context))
    {
        std::cout << "publishing!" << std::endl;
        native_rcl_publish(publisher_handle, &v);
        usleep(1000 * 1000);
    }
    
    
    return 0;
}


int main_subscribe()
{
    std::cout << "running..." << std::endl;
    
    interop_context context;
    
    native_rcl_init(&context);
    
    rcl_node_t *node_handle;
    rcl_ret_t ret = native_rcl_create_node_handle(&context, (void**)&node_handle, "test", "");
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to create node" << std::endl;
        return 1;
    }
    
    
    rcl_subscription_t* subscription_handle;
    
    rmw_qos_profile_t profile;
    ret = native_rcl_create_subscription_handle((void**)&subscription_handle, node_handle, "chatter", "std_msgs/String",
                                                &profile);
    
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to initialize subscription" << std::endl;
    }
    
    std::cout << "started!" << std::endl;
    
    rcl_serialized_message_t* message;
    native_rcl_create_serialized_message((void**)&message);
    
    while (native_rcl_ok(&context))
    {
        rcl_wait_set_t* wait_set_handle = (rcl_wait_set_t*) native_rcl_create_wait_set();
        
        native_rcl_wait_set_init(&context, wait_set_handle, 1, 0, 0, 0, 0, 0);
        native_rcl_wait_set_clear(wait_set_handle);
        
        native_rcl_wait_set_add_subscription((void*)wait_set_handle, (void*)subscription_handle);
        
        void **dummy;
        ret = native_rcl_wait(wait_set_handle, 5000, &dummy, &dummy);
        std::cout << ret << std::endl;
        if (ret == RCL_RET_TIMEOUT)
        {
            native_rcl_destroy_wait_set(wait_set_handle);
            continue;
        }
        
        //if (wait_set_handle->subscriptions[0] != nullptr)
        {
            int dummy;
            void *ptr;
            char gid[RMW_GID_STORAGE_SIZE];
            
            ret = native_rcl_take_serialized_message(subscription_handle, message, &ptr, &dummy, gid);
            if (ret != RCL_RET_OK)
            {
                std::cout << rcl_get_error_string().str << std::endl;
            }
            
            std::cout << "got message! " << ret << " size " << message->buffer_length << std::endl;
        }
        
        native_rcl_destroy_wait_set(wait_set_handle);
    }
    
    usleep(3000 * 1000);
    
    
    native_rcl_destroy_subscription_handle(subscription_handle, node_handle);
    native_rcl_destroy_node_handle(node_handle);
    
    std::cout << "alright! " << native_rcl_get_error_string(&context) << std::endl;
    
    return 0;
}

int main(int argc, const char * argv[])
{
    main_publish();
}



}

