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
#include <fastdds/dds/domain/DomainParticipant.hpp>

#include <iostream>
#include <rcl/error_handling.h>
#include <rcl/node.h>
#include <rcl/graph.h>
#include <rcl/rcl.h>

#include <rosidl_typesupport_fastrtps_cpp/message_type_support.h>
#include <rosidl_typesupport_fastrtps_cpp/service_type_support.h>

#include <rcutils/logging.h>
#include <rosidl_runtime_c/string_functions.h>

#include "fastcdr/Cdr.h"
#include "fastcdr/FastBuffer.h"
#include "fastdds/rtps/common/WriteParams.h"
#include "fastdds/dds/subscriber/SampleInfo.hpp"

#include <cassert>
#include "rmw/rmw.h"
#include "rmw/error_handling.h"
#include "rmw/impl/cpp/macros.hpp"
#include "rmw/types.h"
#include "rmw_fastrtps_cpp/identifier.hpp"
#include "rmw_fastrtps_shared_cpp/custom_client_info.hpp"
#include "rmw_fastrtps_shared_cpp/custom_service_info.hpp"
#include "rmw_fastrtps_shared_cpp/custom_subscriber_info.hpp"
#include "rmw_fastrtps_shared_cpp/guid_utils.hpp"
#include "rmw_fastrtps_shared_cpp/rmw_common.hpp"
#include "rmw_fastrtps_shared_cpp/TypeSupport.hpp"

#include <rcl_interfaces/msg/log.h>
#include <rcl_interfaces/srv/describe_parameters.h>


static char log_buffer[2048];

/// Corresponds in C# to Iviz.Roslib2.RclInterop.Rcl.ConsoleLoggingHandler().
static LoggerCallback rcl_external_logger;

/// Corresponds in C# to Iviz.Roslib2.RclInterop.Rcl.CdrDeserializeCallback().
static CdrDeserializeCallback cdr_deserialize_callback;

/// Corresponds in C# to Iviz.Roslib2.RclInterop.Rcl.CdrSerializeCallback().
static CdrSerializeCallback cdr_serialize_callback;

/// Corresponds in C# to Iviz.Roslib2.RclInterop.Rcl.CdrGetSerializedSizeCallback()
static CdrGetSerializedSizeCallback cdr_get_serialized_size_callback;


static bool cdr_deserialize(eprosima::fastcdr::Cdr &cdr, void *message_context)
{
    if (cdr_deserialize_callback != nullptr)
    {
        size_t buffer_size = cdr.__getBuffer().getBufferSize();
        cdr_deserialize_callback(message_context, cdr.__getBuffer().getBuffer(), (int32_t) buffer_size);
        // deserialization happens in C#, here we just consume the entire buffer
        cdr.reset();
        cdr.jump(buffer_size);
    }
    
    return true;
}

static bool cdr_serialize(const void *message_context, eprosima::fastcdr::Cdr &cdr)
{
    if (cdr_serialize_callback != nullptr)
    {
        size_t buffer_size = cdr.__getBuffer().getBufferSize();
        cdr_serialize_callback(message_context, cdr.__getBuffer().getBuffer(), (int32_t) buffer_size);
        // serialization happens in C#, here we just consume the entire buffer
        cdr.reset();
        cdr.jump(buffer_size);
    }
    
    return true;
}

static uint32_t cdr_get_serialized_size(const void *message_context)
{
    if (cdr_get_serialized_size_callback != nullptr)
    {
        // size calculation happens in C#
        return cdr_get_serialized_size_callback(message_context);
    }
    
    return 0;
}

static size_t cdr_max_serialized_size(bool &full_bounded)
{
    // we don't use this
    full_bounded = false;
    return 1; // just some random value
}


/// Storage for pointers so that they don't get reclaimed when we return to C#
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

struct iviz_message_type_support
{
    std::string message_namespace;
    std::string message_name;
    rosidl_message_type_support_t message_support;
    message_type_support_callbacks_t message_callbacks;

    iviz_message_type_support(const std::string &message_namespace, const std::string &message_name)
    {
        this->message_namespace = message_namespace;
        this->message_name = message_name;
        
        const rosidl_message_type_support_t *template_typesupport =
        ROSIDL_GET_MSG_TYPE_SUPPORT(rcl_interfaces, msg, Log);
        
        message_support.typesupport_identifier = template_typesupport->typesupport_identifier;
        message_support.data = &message_callbacks;
        message_support.func = template_typesupport->func;

        message_callbacks.message_namespace_ = this->message_namespace.data();
        message_callbacks.message_name_ = this->message_name.data();
        message_callbacks.cdr_serialize = cdr_serialize;
        message_callbacks.cdr_deserialize = cdr_deserialize;
        message_callbacks.get_serialized_size = cdr_get_serialized_size;
        message_callbacks.max_serialized_size = cdr_max_serialized_size;
    }
};

struct iviz_service_type_support
{
    std::string service_namespace;
    std::string service_name;
    rosidl_service_type_support_t service_support;
    service_type_support_callbacks_t service_callbacks;
    
    std::string request_namespace;
    std::string request_name;
    rosidl_message_type_support_t request_support;
    message_type_support_callbacks_t request_callbacks;

    std::string response_namespace;
    std::string response_name;
    rosidl_message_type_support_t response_support;
    message_type_support_callbacks_t response_callbacks;

    
    iviz_service_type_support(const std::string &service_namespace, const std::string &service_name)
    {
        this->service_namespace = service_namespace;
        this->service_name = service_name;
        
        const rosidl_service_type_support_t *template_service_typesupport =
        ROSIDL_GET_SRV_TYPE_SUPPORT(rcl_interfaces, srv, DescribeParameters);

        service_support.typesupport_identifier = template_service_typesupport->typesupport_identifier;
        service_support.data = &service_callbacks;
        service_support.func = template_service_typesupport->func;

        const service_type_support_callbacks_t *ts_callbacks = (service_type_support_callbacks_t*) template_service_typesupport->data;
        const rosidl_message_type_support_t *ts_request = ts_callbacks->request_members_;
        const rosidl_message_type_support_t *ts_response = ts_callbacks->response_members_;

        service_callbacks.service_namespace_ = this->service_namespace.data();
        service_callbacks.service_name_ = this->service_name.data();
        service_callbacks.request_members_ = &request_support;
        service_callbacks.response_members_ = &response_support;
            
        request_support.typesupport_identifier = ts_request->typesupport_identifier;
        request_support.data = &request_callbacks;
        request_support.func = ts_request->func;
        
        this->request_name = service_name + "_Request";
        this->request_namespace = service_namespace;
        request_callbacks.message_name_ = this->request_name.data();
        request_callbacks.message_namespace_ = this->request_namespace.data();
        request_callbacks.cdr_deserialize = cdr_deserialize;
        request_callbacks.cdr_serialize = cdr_serialize;
        request_callbacks.get_serialized_size = cdr_get_serialized_size;
        request_callbacks.max_serialized_size = cdr_max_serialized_size;

        response_support.typesupport_identifier = ts_response->typesupport_identifier;
        response_support.data = &response_callbacks;
        response_support.func = ts_response->func;

        this->response_name = service_name + "_Response";
        this->response_namespace = service_namespace;
        response_callbacks.message_name_ = this->response_name.data();
        response_callbacks.message_namespace_ = this->response_namespace.data();
        response_callbacks.cdr_deserialize = cdr_deserialize;
        response_callbacks.cdr_serialize = cdr_serialize;
        response_callbacks.get_serialized_size = cdr_get_serialized_size;
        response_callbacks.max_serialized_size = cdr_max_serialized_size;
    }
};


/// Cache for message type supports already generated
static std::map<std::string, iviz_message_type_support*> custom_message_types;

/// Cache for service type supports already generated
static std::map<std::string, iviz_service_type_support*> custom_service_types;


extern "C"
{

static void IgnoreRet(rcl_ret_t) {}

bool native_rcl_set_dds_profile_path(const char *path)
{
    return eprosima::fastrtps::Domain::loadXMLProfilesFile(path);
}


void native_rcl_set_message_callbacks(CdrDeserializeCallback new_cdr_deserialize_callback,
                                      CdrSerializeCallback new_cdr_serialize_callback,
                                      CdrGetSerializedSizeCallback new_cdr_get_serialized_size_callback)
{
    cdr_deserialize_callback = new_cdr_deserialize_callback;
    cdr_serialize_callback = new_cdr_serialize_callback;
    cdr_get_serialized_size_callback = new_cdr_get_serialized_size_callback;
}

void *native_rcl_create_context()
{
    return new interop_context();
}

void native_rcl_destroy_context(void *context)
{
    delete (interop_context*) context;
}

int32_t native_rcl_init(void *context_handle, int32_t domain_id)
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
    rmw_init_options->domain_id = domain_id;
    rmw_init_options->localhost_only = RMW_LOCALHOST_ONLY_DISABLED;
    
    const char ** arg_values = NULL;
    ret = rcl_init(num_args, arg_values, &init_options, &context->context);
    
    IgnoreRet(rcl_init_options_fini(&init_options));
    
    return ret;
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
    if (rcl_external_logger == nullptr)
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
    rcl_external_logger = logger;
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
                                      void **guard_handles, int num_guard_handles,
                                      void **client_handles, int num_client_handles,
                                      void **service_handles, int num_service_handles)
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
    
    for (int i = 0; i < num_client_handles; i++)
    {
        rcl_client_t *client = (rcl_client_t *)client_handles[i];
        IgnoreRet(rcl_wait_set_add_client(wait_set, client, nullptr));
    }
    
    for (int i = 0; i < num_service_handles; i++)
    {
        rcl_service_t *service = (rcl_service_t *)service_handles[i];
        IgnoreRet(rcl_wait_set_add_service(wait_set, service, nullptr));
    }
    
    return RCL_RET_OK;
}

int32_t native_rcl_wait(void *wait_set_handle, int32_t timeout_in_ms,
                        void ***subscription_handles,
                        void ***guard_handles,
                        void ***client_handles,
                        void ***service_handles)
{
    rcl_wait_set_t *wait_set = (rcl_wait_set_t *)wait_set_handle;
    rcl_ret_t ret = rcl_wait(wait_set, RCL_MS_TO_NS(timeout_in_ms));
    
    if (ret != RCL_RET_OK)
    {
        *subscription_handles = nullptr;
        *guard_handles = nullptr;
        *client_handles = nullptr;
        *service_handles = nullptr;
        return ret;
    }
    
    *subscription_handles = (void**) wait_set->subscriptions;
    *guard_handles = (void**) wait_set->guard_conditions;
    *client_handles = (void**) wait_set->clients;
    *service_handles = (void**) wait_set->services;
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

bool native_rcl_is_message_type_supported(const char *type)
{
    //return default_types.count(type) != 0;
    return true;
}

bool native_rcl_is_service_type_supported(const char *type)
{
    //return default_service_types.count(type) != 0;
    return true;
}

int32_t native_rcl_create_subscription_handle(void **subscription_handle,
                                              void *node_handle,
                                              const char *topic,
                                              const char *type,
                                              void *profile_handle)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    
    //auto it{ default_types.find( type ) };
    auto it { custom_message_types.find(type) };
    
    const rosidl_message_type_support_t *ts;
    if (it == custom_message_types.end())
    {
        std::string short_type_name = type;
        size_t index = short_type_name.find("/");
        if (index == std::string::npos)
        {
            std::cout << "Invalid message name '" << short_type_name << "'!" << std::endl; // can't really recover from this
            return -1;
        }
        else
        {
            std::string message_namespace = short_type_name.substr(0, index) + "::msg";
            std::string message_name = short_type_name.substr(index + 1);

            auto its = new iviz_message_type_support(message_namespace, message_name);
            ts = &its->message_support;
            custom_message_types[type] = its;
        }
    }
    else
    {
        //const rosidl_message_type_support_t *ts = it->second;
        ts = &it->second->message_support;
    }
    
    //const rosidl_message_type_support_t *ts = it->second;
    
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

int32_t native_rcl_take_serialized_message(const void *subscription_handle, void *serialized_message,
                                           void **ptr, int32_t *length, void *gid,
                                           uint8_t *more_remaining)
{
    const rcl_subscription_t * subscription = (const rcl_subscription_t *)subscription_handle;
    rcl_serialized_message_t *message = (rcl_serialized_message_t *) serialized_message;
    
    rmw_message_info_t info;
    rcl_ret_t ret = rcl_take_serialized_message(subscription, message, &info, NULL);
    if (ret == RCL_RET_OK)
    {
        *ptr = message->buffer;
        *length = (int) message->buffer_length;
        memcpy(gid, info.publisher_gid.data, RMW_GID_STORAGE_SIZE);
        
        
        rmw_subscription_t *rmw_subscription = rcl_subscription_get_rmw_handle(subscription);
        auto info = static_cast<CustomSubscriberInfo *>(rmw_subscription->data);
        *more_remaining = info->subscriber_->get_unread_count() != 0;
    }
    else
    {
        *ptr = nullptr;
        *length = 0;
        *more_remaining = false;
    }
    
    return ret;
}

int32_t native_rcl_take(const void *subscription_handle, void *message_context, void *gid, uint8_t *more_remaining)
{
    const rcl_subscription_t * subscription = (const rcl_subscription_t *)subscription_handle;
    
    rmw_message_info_t message_info;
    rcl_ret_t ret = rcl_take(subscription, message_context, &message_info, nullptr);

    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    memcpy(gid, message_info.publisher_gid.data, RMW_GID_STORAGE_SIZE);
    return RCL_RET_OK;
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
                                           const char *type,
                                           void *profile_handle)
{
    auto it { custom_message_types.find(type) };
    
    const rosidl_message_type_support_t *ts;
    if (it == custom_message_types.end())
    {
        std::string short_type_name = type;
        size_t index = short_type_name.find("/");
        if (index == std::string::npos)
        {
            std::cout << "Invalid message name '" << short_type_name << "'!" << std::endl; // can't really recover from this
            return -1;
        }
        else
        {
            std::string message_namespace = short_type_name.substr(0, index) + "::msg";
            std::string message_name = short_type_name.substr(index + 1);

            auto its = new iviz_message_type_support(message_namespace, message_name);
            ts = &its->message_support;
            custom_message_types[type] = its;
        }
    }
    else
    {
        ts = &it->second->message_support;
    }
    
    rcl_node_t *node = (rcl_node_t *)node_handle;
    
    rcl_publisher_t *publisher = (rcl_publisher_t *)malloc(sizeof(rcl_publisher_t));
    *publisher = rcl_get_zero_initialized_publisher();
    
    rcl_publisher_options_t publisher_ops = rcl_publisher_get_default_options();
    rmw_qos_profile_t *profile = (rmw_qos_profile_t*) profile_handle;
    publisher_ops.qos = *profile;
    
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

int32_t native_rcl_publish(void *publisher_handle, void *message_context)
{
    rcl_publisher_t * publisher = (rcl_publisher_t *)publisher_handle;
    return rcl_publish(publisher, message_context, nullptr);
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

const void *native_rcl_get_graph_guard_condition(void *node_handle)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    return rcl_node_get_graph_guard_condition(node);
}

int32_t native_rcl_create_client_handle(void **client_handle,
                                        void *node_handle,
                                        const char *service,
                                        const char *type,
                                        void *profile_handle)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    
    auto it { custom_service_types.find(type) };
    
    const rosidl_service_type_support_t *ts;
    if (it == custom_service_types.end())
    {
        std::string short_type_name = type;
        size_t index = short_type_name.find("/");
        if (index == std::string::npos)
        {
            std::cout << "Invalid message name '" << short_type_name << "'!" << std::endl;
            return -1;
        }
        else
        {
            std::string service_namespace = short_type_name.substr(0, index) + "::srv";
            std::string service_name = short_type_name.substr(index + 1);

            auto its = new iviz_service_type_support(service_namespace, service_name);
            ts = &its->service_support;
            custom_service_types[type] = its;
        }
    }
    else
    {
        ts = &it->second->service_support;
    }
    
    
    rcl_client_t *client = (rcl_client_t *)malloc(sizeof(rcl_client_t));
    *client = rcl_get_zero_initialized_client();
    
    rcl_client_options_t options = rcl_client_get_default_options();
    rmw_qos_profile_t *profile = (rmw_qos_profile_t*) profile_handle;
    options.qos = *profile;
    
    rcl_ret_t ret = rcl_client_init(client, node, ts, service, &options);
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    *client_handle = client;
    return RCL_RET_OK;
}

int32_t native_rcl_destroy_client_handle(void *client_handle, void *node_handle)
{
    rcl_client_t *client = (rcl_client_t *)client_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcl_ret_t ret = rcl_client_fini(client, node);
    free(client_handle);
    return ret;
}

int32_t native_rcl_is_service_server_available(void *client_handle, void *node_handle, uint8_t *is_available)
{
    rcl_client_t *client = (rcl_client_t *)client_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    bool is_available_bool;
    rcl_ret_t ret = rcl_service_server_is_available(node, client, &is_available_bool);
    *is_available = is_available_bool;
    return ret;
}


int32_t native_rcl_send_request(void *client_handle, void* serialized_message_handle, int64_t *sequence_id)
{
    rcl_client_t *rcl_client = (rcl_client_t *)client_handle;
    rcl_serialized_message_t *serialized_message = (rcl_serialized_message_t *)serialized_message_handle;
    
    const char *identifier = eprosima_fastrtps_identifier;
    
    rmw_client_t *client = rcl_client_get_rmw_handle(rcl_client);
    
    RMW_CHECK_ARGUMENT_FOR_NULL(client, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_TYPE_IDENTIFIERS_MATCH(client,
                                     client->implementation_identifier, identifier,
                                     return RMW_RET_INCORRECT_RMW_IMPLEMENTATION);
    RMW_CHECK_ARGUMENT_FOR_NULL(sequence_id, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_ARGUMENT_FOR_NULL(serialized_message_handle, RMW_RET_INVALID_ARGUMENT);
    
    rmw_ret_t returnedValue = RMW_RET_ERROR;
    
    CustomClientInfo* info = static_cast<CustomClientInfo *>(client->data);
    assert(info);
    
    eprosima::fastcdr::FastBuffer buffer(reinterpret_cast<char *>(serialized_message->buffer),
                                         serialized_message->buffer_length);
    eprosima::fastcdr::Cdr ser(buffer, eprosima::fastcdr::Cdr::DEFAULT_ENDIAN,
                               eprosima::fastcdr::Cdr::DDS_CDR);
    if (!ser.jump(serialized_message->buffer_length))
    {
        RMW_SET_ERROR_MSG("cannot correctly set serialized buffer");
        return RMW_RET_ERROR;
    }
    
    eprosima::fastrtps::rtps::WriteParams wparams;
    rmw_fastrtps_shared_cpp::SerializedData data;
    data.is_cdr_buffer = true;
    data.data = &ser;
    data.impl = nullptr;
    wparams.related_sample_identity().writer_guid() = info->reader_guid_;
    if (info->request_publisher_->write(&data, wparams))
    {
        returnedValue = RMW_RET_OK;
        *sequence_id = ((int64_t)wparams.sample_identity().sequence_number().high) << 32 |
        wparams.sample_identity().sequence_number().low;
    }
    else
    {
        RMW_SET_ERROR_MSG("cannot publish data");
    }
    
    return returnedValue;
}

int32_t native_rcl_take_response(void *client_handle, void* serialized_message_handle,
                                 void *request_header_handle, void **ptr, int32_t *length)
{
    rcl_client_t *rcl_client = (rcl_client_t *)client_handle;
    rcl_serialized_message_t *serialized_message = (rcl_serialized_message_t *)serialized_message_handle;
    
    const char *identifier = eprosima_fastrtps_identifier;
    
    rmw_client_t *client = rcl_client_get_rmw_handle(rcl_client);
    
    
    RMW_CHECK_ARGUMENT_FOR_NULL(client, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_TYPE_IDENTIFIERS_MATCH(client, client->implementation_identifier, identifier,
                                     return RMW_RET_INCORRECT_RMW_IMPLEMENTATION);
    RMW_CHECK_ARGUMENT_FOR_NULL(request_header_handle, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_ARGUMENT_FOR_NULL(serialized_message_handle, RMW_RET_INVALID_ARGUMENT);
    
    CustomClientInfo* info = static_cast<CustomClientInfo *>(client->data);
    assert(info);
    
    CustomClientResponse response;
    
    if (info->listener_->getResponse(response))
    {
        auto buffer_size = static_cast<size_t>(response.buffer_->getBufferSize());
        if (serialized_message->buffer_capacity < buffer_size)
        {
            auto ret = rmw_serialized_message_resize(serialized_message, buffer_size);
            if (ret != RMW_RET_OK)
            {
                return ret;  // Error message already set
            }
        }
        
        serialized_message->buffer_length = buffer_size;
        memcpy(serialized_message->buffer, response.buffer_->getBuffer(), serialized_message->buffer_length);
        
        
        rmw_service_info_t *request_header = (rmw_service_info_t*) request_header_handle;
        request_header->source_timestamp = response.sample_info_.sourceTimestamp.to_ns();
        request_header->received_timestamp = response.sample_info_.receptionTimestamp.to_ns();
        request_header->request_id.sequence_number =
        ((int64_t)response.sample_identity_.sequence_number().high) <<
        32 | response.sample_identity_.sequence_number().low;
        
        *ptr = serialized_message->buffer;
        *length = (int32_t) serialized_message->buffer_length;
        
        return RMW_RET_OK;
    }
    
    *ptr = nullptr;
    *length = 0;
    
    return RCL_RET_CLIENT_TAKE_FAILED;
}

int32_t native_rcl_create_service_handle(void **service_handle,
                                         void *node_handle,
                                         const char *service,
                                         const char *type,
                                         void *profile_handle)
{
    rcl_node_t *node = (rcl_node_t *)node_handle;
    
    auto it { custom_service_types.find(type) };
    
    const rosidl_service_type_support_t *ts;
    if (it == custom_service_types.end())
    {
        std::string short_type_name = type;
        size_t index = short_type_name.find("/");
        if (index == std::string::npos)
        {
            std::cout << "Invalid message name '" << short_type_name << "'!" << std::endl;
            return -1;
        }
        else
        {
            std::string service_namespace = short_type_name.substr(0, index) + "::srv";
            std::string service_name = short_type_name.substr(index + 1);

            auto its = new iviz_service_type_support(service_namespace, service_name);
            ts = &its->service_support;
            custom_service_types[type] = its;
        }
    }
    else
    {
        ts = &it->second->service_support;
    }
    
    rcl_service_t *server = (rcl_service_t *)malloc(sizeof(rcl_service_t));
    *server = rcl_get_zero_initialized_service();
    
    rcl_service_options_t options = rcl_service_get_default_options();
    rmw_qos_profile_t *profile = (rmw_qos_profile_t*) profile_handle;
    options.qos = *profile;
    
    rcl_ret_t ret = rcl_service_init(server, node, ts, service, &options);
    if (ret != RCL_RET_OK)
    {
        return ret;
    }
    
    *service_handle = server;
    return RCL_RET_OK;
}

int32_t native_rcl_destroy_service_handle(void *service_handle, void *node_handle)
{
    rcl_service_t *service = (rcl_service_t *)service_handle;
    rcl_node_t *node = (rcl_node_t *)node_handle;
    rcl_ret_t ret = rcl_service_fini(service, node);
    free(service_handle);
    return ret;
}


int32_t native_rcl_send_response(void *service_handle, void* serialized_message_handle, const void *request_header_handle)
{
    rcl_service_t *rcl_service = (rcl_service_t *)service_handle;
    rcl_serialized_message_t *serialized_message = (rcl_serialized_message_t *)serialized_message_handle;
    
    const char *identifier = eprosima_fastrtps_identifier;
    
    rmw_service_t *service = rcl_service_get_rmw_handle(rcl_service);
    
    
    RMW_CHECK_ARGUMENT_FOR_NULL(service, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_TYPE_IDENTIFIERS_MATCH(service,
                                     service->implementation_identifier, identifier,
                                     return RMW_RET_INCORRECT_RMW_IMPLEMENTATION);
    RMW_CHECK_ARGUMENT_FOR_NULL(request_header_handle, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_ARGUMENT_FOR_NULL(serialized_message, RMW_RET_INVALID_ARGUMENT);
    
    rmw_ret_t returnedValue = RMW_RET_ERROR;
    
    auto info = static_cast<CustomServiceInfo *>(service->data);
    assert(info);
    
    rmw_request_id_t *request_header = (rmw_request_id_t*) request_header_handle;
    eprosima::fastrtps::rtps::WriteParams wparams;
    rmw_fastrtps_shared_cpp::copy_from_byte_array_to_fastrtps_guid(request_header->writer_guid,
                                                                   &wparams.related_sample_identity().writer_guid());
    wparams.related_sample_identity().sequence_number().high =
    (int32_t)((request_header->sequence_number & 0xFFFFFFFF00000000) >> 32);
    wparams.related_sample_identity().sequence_number().low =
    (int32_t)(request_header->sequence_number & 0xFFFFFFFF);
    
    constexpr uint8_t entity_id_is_reader_bit = 0x04;
    const eprosima::fastrtps::rtps::GUID_t & related_guid =
    wparams.related_sample_identity().writer_guid();
    if ((related_guid.entityId.value[3] & entity_id_is_reader_bit) != 0)
    {
        auto listener = static_cast<PatchedServicePubListener *>(info->pub_listener_);
        client_present_t ret = listener->check_for_subscription(related_guid);
        if (ret == client_present_t::GONE)
        {
            return RMW_RET_OK;
        }
        else if (ret == client_present_t::MAYBE)
        {
            RMW_SET_ERROR_MSG("client will not receive response");
            return RMW_RET_TIMEOUT;
        }
    }
    
    eprosima::fastcdr::FastBuffer buffer(reinterpret_cast<char *>(serialized_message->buffer),
                                         serialized_message->buffer_length);
    eprosima::fastcdr::Cdr ser(buffer, eprosima::fastcdr::Cdr::DEFAULT_ENDIAN,
                               eprosima::fastcdr::Cdr::DDS_CDR);
    if (!ser.jump(serialized_message->buffer_length))
    {
        RMW_SET_ERROR_MSG("cannot correctly set serialized buffer");
        return RMW_RET_ERROR;
    }
    
    
    rmw_fastrtps_shared_cpp::SerializedData data;
    data.is_cdr_buffer = true;
    data.data = &ser;
    data.impl = nullptr;
    if (info->response_publisher_->write(&data, wparams))
    {
        returnedValue = RMW_RET_OK;
    }
    else
    {
        RMW_SET_ERROR_MSG("cannot publish data");
    }
    
    return returnedValue;
}

int32_t native_rcl_take_request(void *service_handle, void* serialized_message_handle,
                                void *request_header_handle, void **ptr, int32_t *length)
{
    rcl_service_t *rcl_service = (rcl_service_t *)service_handle;
    rcl_serialized_message_t *serialized_message = (rcl_serialized_message_t *)serialized_message_handle;
    
    const char *identifier = eprosima_fastrtps_identifier;
    
    rmw_service_t *service = rcl_service_get_rmw_handle(rcl_service);
    
    RMW_CHECK_ARGUMENT_FOR_NULL(service, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_TYPE_IDENTIFIERS_MATCH(service,
                                     service->implementation_identifier, identifier,
                                     return RMW_RET_INCORRECT_RMW_IMPLEMENTATION);
    RMW_CHECK_ARGUMENT_FOR_NULL(request_header_handle, RMW_RET_INVALID_ARGUMENT);
    RMW_CHECK_ARGUMENT_FOR_NULL(serialized_message_handle, RMW_RET_INVALID_ARGUMENT);
    
    auto info = static_cast<CustomServiceInfo *>(service->data);
    assert(info);
    
    CustomServiceRequest request = info->listener_->getRequest();
    
    if (request.buffer_ != nullptr)
    {
        auto buffer_size = static_cast<size_t>(request.buffer_->getBufferSize());
        if (serialized_message->buffer_capacity < buffer_size)
        {
            auto ret = rmw_serialized_message_resize(serialized_message, buffer_size);
            if (ret != RMW_RET_OK)
            {
                return ret;  // Error message already set
            }
        }
        
        serialized_message->buffer_length = buffer_size;
        memcpy(serialized_message->buffer, request.buffer_->getBuffer(), serialized_message->buffer_length);
        
        rmw_service_info_t *request_header = (rmw_service_info_t*) request_header_handle;
        rmw_fastrtps_shared_cpp::copy_from_fastrtps_guid_to_byte_array(request.sample_identity_.writer_guid(),
                                                                       request_header->request_id.writer_guid);
        request_header->request_id.sequence_number =
        ((int64_t)request.sample_identity_.sequence_number().high) <<
        32 | request.sample_identity_.sequence_number().low;
        request_header->source_timestamp = request.sample_info_.sourceTimestamp.to_ns();
        request_header->received_timestamp = request.sample_info_.receptionTimestamp.to_ns();
        
        delete request.buffer_;
        
        *ptr = serialized_message->buffer;
        *length = (int32_t)serialized_message->buffer_length;
        
        return RMW_RET_OK;
    }
    
    *ptr = nullptr;
    *length = 0;
    
    return RCL_RET_SERVICE_TAKE_FAILED;
}




void default_handler(int severity, const char *name, rcutils_time_point_value_t timestamp, const char *message)
{
    std::cout << "[" << name << "] " << message << std::endl;
}


int main_publish()
{
    std::cout << "running..." << std::endl;
    
    interop_context context;
    
    native_rcl_init(&context, 0);
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
    
    ret = native_rcl_create_publisher_handle((void**)&publisher_handle, node_handle,
                                             "my_chatter", "tf2_msgs/TFMessage",
                                             (void*)&rmw_qos_profile_default);
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to initialize publisher" << std::endl;
    }
    
    /*
    tf2_msgs__msg__TFMessage v;
    tf2_msgs__msg__TFMessage__init(&v);
    
    geometry_msgs__msg__TransformStamped__Sequence__init(&v.transforms, 3);
    
    rosidl_runtime_c__String a;
    rosidl_runtime_c__String__init(&a);
    rosidl_runtime_c__String__assign(&a, "abcd");
    
    v.transforms.data[0].child_frame_id = a;
    
    
    
    
    while (native_rcl_ok(&context))
    {
        std::cout << "publishing!" << std::endl;
        native_rcl_publish(publisher_handle, &v);
        usleep(1000 * 1000);
    }
     */
    
    
    return 0;
}


int main_subscribe()
{
    std::cout << "running..." << std::endl;
    
    interop_context context;
    
    native_rcl_init(&context, 0);
    
    rcl_node_t *node_handle;
    rcl_ret_t ret = native_rcl_create_node_handle(&context, (void**)&node_handle, "test", "");
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to create node" << std::endl;
        return 1;
    }
    
    
    rcl_subscription_t* subscription_handle;
    rmw_qos_profile_t profile = rmw_qos_profile_system_default;
    
    ret = native_rcl_create_subscription_handle((void**)&subscription_handle, node_handle, "chatter", "std_msgs/String",
                                                &profile);
    
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to initialize subscription" << std::endl;
    }
    
    std::cout << "started!" << std::endl;
    
    rcl_serialized_message_t* message;
    native_rcl_create_serialized_message((void**)&message);
    
    //ROSIDL_GET_MSG_TYPE_SUPPORT(std_msgs, msg, String);
    
    while (native_rcl_ok(&context))
    {
        rcl_wait_set_t* wait_set_handle = (rcl_wait_set_t*) native_rcl_create_wait_set();
        
        native_rcl_wait_set_init(&context, wait_set_handle, 1, 0, 0, 0, 0, 0);
        native_rcl_wait_set_clear(wait_set_handle);
        
        native_rcl_wait_set_add_subscription((void*)wait_set_handle, (void*)subscription_handle);
        
        void **dummy;
        ret = native_rcl_wait(wait_set_handle, 5000, &dummy, &dummy, &dummy, &dummy);
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
            uint8_t remaining;
            char gid[RMW_GID_STORAGE_SIZE];
            
            ret = native_rcl_take_serialized_message(subscription_handle, message,
                                                     &ptr, &dummy, gid, &remaining);
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


int main_subscribe_2()
{
    std::cout << "running..." << std::endl;
    
    interop_context context;
    
    native_rcl_init(&context, 0);
    
    rcl_node_t *node_handle;
    rcl_ret_t ret = native_rcl_create_node_handle(&context, (void**)&node_handle, "test", "");
    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to create node" << std::endl;
        return 1;
    }
    
    auto service = new iviz_service_type_support("iviz_msgs", "GetModelResource");
    
    
    rcl_subscription_t* subscription_handle;
    rmw_qos_profile_t profile = rmw_qos_profile_system_default;
    ret = native_rcl_create_subscription_handle((void**)&subscription_handle, node_handle, "chatter_int", "std_msgs/Int32",
                                                &profile);

    if (ret != RCL_RET_OK)
    {
        std::cout << "failed to initialize subscription" << std::endl;
    }
    
    std::cout << "started!" << std::endl;
    
    rcl_serialized_message_t* message;
    native_rcl_create_serialized_message((void**)&message);
    
    //ROSIDL_GET_MSG_TYPE_SUPPORT(std_msgs, msg, String);
    
    //tf2_msgs__msg__TFMessage;
    //std_msgs__msg__String string;
    
    
    while (native_rcl_ok(&context))
    {
        rcl_wait_set_t* wait_set_handle = (rcl_wait_set_t*) native_rcl_create_wait_set();
        
        native_rcl_wait_set_init(&context, wait_set_handle, 1, 0, 0, 0, 0, 0);
        native_rcl_wait_set_clear(wait_set_handle);
        
        native_rcl_wait_set_add_subscription((void*)wait_set_handle, (void*)subscription_handle);
        
        void **dummy;
        ret = native_rcl_wait(wait_set_handle, 5000, &dummy, &dummy, &dummy, &dummy);
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
            uint8_t remaining;
            char gid[RMW_GID_STORAGE_SIZE];
            
            //ret = native_rcl_take_serialized_message(subscription_handle, message,
            //                                         &ptr, &dummy, gid, &remaining);
            

            
            ret = rcl_take(subscription_handle, (void*)1, nullptr, nullptr);
            
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
    main_subscribe_2();
}



}

