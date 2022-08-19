
#pragma once

#include <stdint.h>

extern "C"
{

typedef void (*CdrDeserializeCallback)(void *message_context, const void *ptr, int32_t length);

typedef void (*CdrSerializeCallback)(const void *message_context, void *ptr, int32_t length);

typedef int32_t (*CdrGetSerializedSizeCallback)(const void *message_context);

typedef void (*LoggerCallback) (int severity, const char *name, int64_t timestamp, const char *message);


bool native_rcl_set_dds_profile_path(const char *path);

void native_rcl_set_message_callbacks(CdrDeserializeCallback new_cdr_deserialize_callback,
                                      CdrSerializeCallback new_cdr_serialize_callback,
                                      CdrGetSerializedSizeCallback new_cdr_get_serialized_size_callback);

void *native_rcl_create_context();

void native_rcl_destroy_context(void *context);


int32_t native_rcl_init(void *context, int32_t domain_id);

int32_t native_rcl_shutdown(void *context_handle);


int32_t native_rcl_init_logging();

void native_rcl_set_logging_level(int level);

void native_rcl_set_logging_handler(LoggerCallback logger);


int32_t native_rcl_create_node_handle(void *context_handle, void **node_handle, const char *name, const char *name_space);

int32_t native_rcl_destroy_node_handle(void *node_handle);


const char* native_rcl_get_fully_qualified_node_name(void *node_handle);


bool native_rcl_ok(void *context_handle);


void *native_rcl_create_guard(void *context_handle);

int32_t native_rcl_destroy_guard(void *guard_handle);

int32_t native_rcl_trigger_guard(void *guard_handle);


void *native_rcl_create_wait_set();

int32_t native_rcl_wait_set_init(void *context_handle,
                                 void *wait_set_handle,
                                 int32_t number_of_subscriptions,
                                 int32_t number_of_guard_conditions,
                                 int32_t number_of_timers,
                                 int32_t number_of_clients,
                                 int32_t number_of_services,
                                 int32_t number_of_events);

int32_t native_rcl_wait(void *wait_set_handle, int32_t timeout_in_ms,
                        void ***subscription_handles,
                        void ***guard_handles,
                        void ***client_handles,
                        void ***service_handles);

int32_t native_rcl_wait_clear_and_add(void *wait_set_handle,
                                      void **subscription_handles, int num_subscription_handles,
                                      void **guard_handles, int num_guard_handles,
                                      void **client_handles, int num_client_handles,
                                      void **service_handles, int num_service_handles);

int32_t native_rcl_destroy_wait_set(void *wait_set_handle);


const char* native_rcl_get_error_string(void *context_handle);


int32_t native_rcl_create_subscription_handle(void **subscription_handle,
                                              void *node_handle,
                                              const char *topic,
                                              const char *type,
                                              void *profile);

int32_t native_rcl_destroy_subscription_handle(void *subscription_handle, void *node_handle);

int32_t native_rcl_subscription_get_publisher_count(void *subscription_handle, int32_t *count);

int32_t native_rcl_destroy_subscription_handle(void *subscription_handle, void *node_handle);



bool native_rcl_is_message_type_supported(const char *type);

bool native_rcl_is_service_type_supported(const char *type);



int32_t native_rcl_take_serialized_message(const void *subscription_handle,
                                           void *serialized_message,
                                           void **ptr, int32_t *length,
                                           void *gid, uint8_t *more_remaining);

int32_t native_rcl_take(const void *subscription_handle, void *message_context, void *gid, uint8_t *more_remaining);


int32_t native_rcl_create_serialized_message(void **message_handle);

int32_t native_rcl_ensure_serialized_message_size(void *message_handle, int32_t size, void **ptr);

int32_t native_rcl_destroy_serialized_message(void *message_handle);


int32_t native_rcl_create_publisher_handle(void **publisher_handle,
                                           void *node_handle,
                                           const char *topic,
                                           const char *type,
                                           void *profile_handle);

int32_t native_rcl_destroy_publisher_handle(void *publisher_handle, void *node_handle);

int32_t native_rcl_publisher_get_subscription_count(void *subscription_handle, int32_t *count);

int32_t native_rcl_publish(void *publisher_handle, void *message_context);

int32_t native_rcl_publish_serialized_message(void *publisher_handle, void *serialized_message_handle);


int32_t native_rcl_get_node_names(void *context_handle, void *node_handle,
                                  const char*** node_names_handle, int32_t *num_node_names,
                                  const char*** node_namespaces_handle, int32_t *num_node_namespaces);

int32_t native_rcl_get_topic_names_and_types(void *context_handle, void *node_handle,
                                             const char*** topic_names_handle,
                                             const char*** topic_types_handle, int32_t *num_topic_types);

int32_t native_rcl_get_service_names_and_types(void *context_handle, void *node_handle,
                                               const char*** service_names_handle,
                                               const char*** node_namespaces_handle, int32_t *num_node_namespaces);

int32_t native_rcl_get_service_names_and_types_by_node(void *context_handle, void *node_handle,
                                                       char *node_name, char *node_namespace,
                                                       const char*** service_names_handle,
                                                       const char*** service_types_handle, int32_t *num_service_types);

int32_t native_rcl_get_publishers_info_by_topic(void *context_handle, void *node_handle, char *topic_name,
                                                const char ***node_names_handle,
                                                const char ***node_namespaces_handle,
                                                const char ***topic_types_handle,
                                                char **gid_handle,
                                                void **profiles_handle, int32_t *num_nodes);

int32_t native_rcl_get_subscribers_info_by_topic(void *context_handle, void *node_handle, char *topic_name,
                                                 const char ***node_names_handle,
                                                 const char ***node_namespaces_handle,
                                                 const char ***topic_types_handle,
                                                 char **gid_handle,
                                                 void **profiles_handle, int32_t *num_nodes);

int32_t native_rcl_count_publishers(void *node_handle, char *topic_name, int32_t *count);

int32_t native_rcl_count_subscribers(void *node_handle, char *topic_name, int32_t *count);

const void *native_rcl_get_graph_guard_condition(void *node_handle);



int32_t native_rcl_create_client_handle(void **client_handle,
                                        void *node_handle,
                                        const char *service,
                                        const char *type,
                                        void *profile_handle);

int32_t native_rcl_destroy_client_handle(void *client_handle, void *node_handle);

int32_t native_rcl_is_service_server_available(void *client_handle, void *node_handle, uint8_t *is_available);

int32_t native_rcl_send_request(void *client_handle, void* serialized_message_handle, int64_t *sequence_id);

int32_t native_rcl_take_response(void *client_handle, void* serialized_message_handle,
                                 void *request_header_handle, void **ptr, int32_t *length);

int32_t native_rcl_create_service_handle(void **service_handle,
                                         void *node_handle,
                                         const char *service,
                                         const char *type,
                                         void *profile_handle);

int32_t native_rcl_destroy_service_handle(void *service_handle, void *node_handle);

int32_t native_rcl_send_response(void *service_handle, void* serialized_message_handle, const void *request_header_handle);

int32_t native_rcl_take_request(void *service_handle, void* serialized_message_handle,
                                void *request_header_handle, void **ptr, int32_t *length);
}
