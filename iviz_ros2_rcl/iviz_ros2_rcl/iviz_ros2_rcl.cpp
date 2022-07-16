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

#include <iostream>
#include <rcl/error_handling.h>
#include <rcl/node.h>
#include <rcl/rcl.h>

#include <rcutils/logging.h>

#include <rosidl_runtime_c/string_functions.h>
#include <rmw/rmw.h>

#include <std_msgs/msg/string.h>
#include <std_msgs/msg/color_rgba.h>
#include <tf2_msgs/msg/tf_message.h>
#include <rcl_interfaces/msg/log.h>

#include <geometry_msgs/msg/quaternion.h>
#include <geometry_msgs/msg/vector3.h>
#include <geometry_msgs/msg/point.h>
#include <geometry_msgs/msg/pose.h>
#include <geometry_msgs/msg/transform.h>
#include <geometry_msgs/msg/point32.h>
#include <geometry_msgs/msg/transform_stamped.h>
#include <geometry_msgs/msg/polygon_stamped.h>

static rcl_context_t context;
static std::string error_message;
static char log_buffer[1024];

static void (*rcl_external_logger) (int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message);


#define MSGTYPE(a, b) (rosidl_message_type_support_t*) ROSIDL_GET_MSG_TYPE_SUPPORT(a, msg, b)
#define ENTRY(a, b) {#a "/" #b, MSGTYPE(a, b)}

static std::map<std::string, rosidl_message_type_support_t*> default_types
{
    ENTRY(std_msgs, String),
    ENTRY(std_msgs, ColorRGBA),
    ENTRY(tf2_msgs, TFMessage),
    ENTRY(rcl_interfaces, Log),
    ENTRY(geometry_msgs, Vector3),
    ENTRY(geometry_msgs, Point),
    ENTRY(geometry_msgs, Pose),
    ENTRY(geometry_msgs, Transform),
    ENTRY(geometry_msgs, Quaternion),
    ENTRY(geometry_msgs, Point32),
    ENTRY(geometry_msgs, TransformStamped),
    ENTRY(geometry_msgs, PolygonStamped),
};

extern "C"
{

static void IgnoreRet(rcl_ret_t) {}

int32_t native_rcl_init()
{
    int num_args = 0;
    context = rcl_get_zero_initialized_context();
    rcl_init_options_t init_options = rcl_get_zero_initialized_init_options();
    rcl_ret_t ret = rcl_init_options_init(&init_options, rcl_get_default_allocator());
    if (RCL_RET_OK != ret)
    {
        return ret;
    }
    
    const char ** arg_values = NULL;
    rcl_ret_t r_ret = rcl_init(num_args, arg_values, &init_options, &context);
    
    IgnoreRet(rcl_init_options_fini(&init_options));
    
    return r_ret;
}


static void rcl_logging_output_handler (const rcutils_log_location_t *location, int severity,
                                          const char *name, rcutils_time_point_value_t timestamp,
                                          const char *format, va_list *args)
{
    if (rcl_external_logger == nullptr)
    {
        return;
    }
    
    vsnprintf(log_buffer, sizeof(log_buffer), format, *args);
    rcl_external_logger(severity, name, timestamp, log_buffer);
    //std::cout << name << ": " << log_buffer << std::endl;
}

int32_t native_rcl_init_logging()
{
    rcl_ret_t ret = rcutils_logging_initialize();
    if (RCL_RET_OK != ret)
    {
        return ret;
    }
    
    return RCL_RET_OK;
}

void native_rcl_set_logging_level(int level)
{
    rcutils_logging_set_default_logger_level(level);
}

void native_rcl_set_logging_handler(void (*logger) (int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message))
{
    rcl_external_logger = logger;
}

int32_t native_rcl_create_node_handle(void **node_handle, const char *name, const char *name_space)
{
    rcl_node_t *node = (rcl_node_t *)malloc(sizeof(rcl_node_t));
    *node = rcl_get_zero_initialized_node();
    
    rcl_node_options_t default_options = rcl_node_get_default_options();
    rcl_ret_t ret = rcl_node_init(node, name, name_space, &context, &default_options);
    *node_handle = node;
    return ret;
}

int32_t native_rcl_destroy_node_handle(void *node_handle)
{
    rcl_ret_t ret = rcl_node_fini((rcl_node_t*)node_handle);
    free(node_handle);
    return ret;
}

bool native_rcl_ok()
{
    return rcl_context_is_valid(&context);
    
}

void *native_rcl_create_wait_set()
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)malloc(sizeof(rcl_wait_set_t));
    *wait_set = rcl_get_zero_initialized_wait_set();
    return wait_set;
}

int32_t native_rcl_wait_set_init(void *wait_set_handle,
                                 int32_t number_of_subscriptions,
                                 int32_t number_of_guard_conditions,
                                 int32_t number_of_timers,
                                 int32_t number_of_clients,
                                 int32_t number_of_services,
                                 int32_t number_of_events)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    
    rcl_ret_t ret = rcl_wait_set_init(wait_set, number_of_subscriptions, number_of_guard_conditions,
                                      number_of_timers, number_of_clients, number_of_services,
                                      number_of_events, &context, rcl_get_default_allocator());
    
    return ret;
}

int32_t native_rcl_wait_set_clear(void *wait_set_handle)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    return rcl_wait_set_clear(wait_set);
}

int32_t native_rcl_wait(void *wait_set_handle, int32_t timeout_in_ms)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    return rcl_wait(wait_set, RCL_MS_TO_NS(timeout_in_ms));
}

int32_t native_rcl_wait_set_add_subscription(void *wait_set_handle, void *subscription_handle)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    rcl_subscription_t *subscription = (rcl_subscription_t *)subscription_handle;
    return rcl_wait_set_add_subscription(wait_set, subscription, NULL);
}

int32_t native_rcl_destroy_wait_set(void *wait_set_handle)
{
    rcl_ret_t ret = rcl_wait_set_fini((rcl_wait_set_t*) wait_set_handle);
    free(wait_set_handle);
    return ret;
}

const char* native_rcl_get_error_string()
{
    error_message = rcl_get_error_string().str;
    return error_message.c_str();
}

bool native_rcl_is_type_supported(const char *type)
{
    return default_types.count(type) != 0;
}

int32_t native_rcl_create_subscription_handle(void **subscription_handle,
                                              void *node_handle,
                                              const char *topic,
                                              const char *type)
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
    
    rcl_ret_t ret = rcl_subscription_init(subscription, node, ts, topic, &subscription_ops);
    
    *subscription_handle = (void *)subscription;
    
    return ret;
}

int32_t native_rcl_destroy_subscription_handle(void *subscription_handle, void *node_handle)
{
    rcl_ret_t ret = rcl_subscription_fini((rcl_subscription_t*) subscription_handle, (rcl_node_t*) node_handle);
    free(subscription_handle);
    return ret;
}

int32_t native_rcl_take_serialized_message(void *subscription_handle, void *serialized_message, void **ptr, int32_t *length)
{
    rcl_subscription_t * subscription = (rcl_subscription_t *)subscription_handle;
    rcl_serialized_message_t *message = (rcl_serialized_message_t *) serialized_message;
    rcl_ret_t ret = rcl_take_serialized_message(subscription, message, NULL, NULL);
    if (ret == RCL_RET_OK)
    {
        *ptr = message->buffer;
        *length = (int) message->buffer_length;
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

int32_t native_rcl_ensure_serialized_message_size(void *message_handle, int32_t size)
{
    rcl_serialized_message_t* message = (rcl_serialized_message_t*) message_handle;
    if (message->buffer_capacity >= size)
    {
        return RCL_RET_OK;
    }
    
    return rcutils_uint8_array_resize(message, size);
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
    rcl_ret_t ret = rcl_publisher_fini((rcl_publisher_t*) publisher_handle, (rcl_node_t*) node_handle);
    free(publisher_handle);
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



void default_handler(int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message)
{
    std::cout << "[" << name << "] " << message << std::endl;
}


int main_publish()
{
    std::cout << "running..." << std::endl;

    native_rcl_init();
    native_rcl_init_logging();
    
    native_rcl_set_logging_level(RCUTILS_LOG_SEVERITY_DEBUG);
    native_rcl_set_logging_handler(default_handler);

    
    rcl_node_t *node_handle;
    rcl_ret_t ret = native_rcl_create_node_handle((void**)&node_handle, "test", "");
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
    
    
    
    while (native_rcl_ok())
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
    
    native_rcl_init();
    
    rcl_node_t *node_handle;
    rcl_ret_t ret = native_rcl_create_node_handle((void**)&node_handle, "test", "");
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to create node" << std::endl;
        return 1;
    }
    
    
    rcl_subscription_t* subscription_handle;
    
    ret = native_rcl_create_subscription_handle((void**)&subscription_handle, node_handle, "chatter", "std_msgs/String");
    
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to initialize subscription" << std::endl;
    }
    
    std::cout << "started!" << std::endl;
    
    rcl_serialized_message_t* message;
    native_rcl_create_serialized_message((void**)&message);
    
    while (native_rcl_ok())
    {
        rcl_wait_set_t* wait_set_handle = (rcl_wait_set_t*) native_rcl_create_wait_set();
        
        native_rcl_wait_set_init(wait_set_handle, 1, 0, 0, 0, 0, 0);
        native_rcl_wait_set_clear(wait_set_handle);
        
        native_rcl_wait_set_add_subscription((void*)wait_set_handle, (void*)subscription_handle);
        
        ret = native_rcl_wait(wait_set_handle, 5000);
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
            
            ret = native_rcl_take_serialized_message(subscription_handle, message, &ptr, &dummy);
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
    
    std::cout << "alright! " << native_rcl_get_error_string() << std::endl;
    
    return 0;
}

int main(int argc, const char * argv[])
{
    main_publish();
}



}

