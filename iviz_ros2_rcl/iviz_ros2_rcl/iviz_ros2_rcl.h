
#pragma once

#include <unistd.h>

extern "C"
{
int32_t native_rcl_init();

int32_t native_rcl_create_node_handle(void **node_handle, const char *name, const char *name_space);

int32_t native_rcl_destroy_node_handle(void *node_handle);

bool native_rcl_ok();

void *native_rcl_create_wait_set();

int32_t native_rcl_wait_set_init(void *wait_set_handle,
                                 int32_t number_of_subscriptions,
                                 int32_t number_of_guard_conditions,
                                 int32_t number_of_timers,
                                 int32_t number_of_clients,
                                 int32_t number_of_services,
                                 int32_t number_of_events);

int32_t native_rcl_wait_set_clear(void *wait_set_handle);

int32_t native_rcl_wait(void *wait_set_handle, int32_t timeout_in_ms);

int32_t native_rcl_wait_set_add_subscription(void *wait_set_handle, void *subscription_handle);

int32_t native_rcl_destroy_wait_set(void *wait_set_handle);

const char* native_rcl_get_error_string();

int32_t native_rcl_create_subscription_handle(void **subscription_handle,
                                              void *node_handle,
                                              const char *topic,
                                              const char *type);

int32_t native_rcl_destroy_subscription_handle(void *subscription_handle, void *node_handle);

bool native_rcl_is_type_supported(const char *type);

int32_t native_rcl_destroy_subscription_handle(void *subscription_handle, void *node_handle);

int32_t native_rcl_take_serialized_message(void *subscription_handle, void *serialized_message, void **ptr, int32_t *length);

int32_t native_rcl_create_serialized_message(void **message_handle);

int32_t native_rcl_ensure_serialized_message_size(void *message_handle, int32_t size);

int32_t native_rcl_destroy_serialized_message(void *message_handle);

int32_t native_rcl_create_publisher_handle(void **publisher_handle,
                                           void *node_handle,
                                           const char *topic,
                                           const char *type);

int32_t native_rcl_destroy_publisher_handle(void *publisher_handle, void *node_handle);

int32_t native_rcl_publish(void *publisher_handle, void *raw_ros_message);

int32_t native_rcl_publish_serialized_message(void *publisher_handle, void *serialized_message_handle);

}
