// Copyright (C) 2015-2020 Jonathan Müller <jonathanmueller.dev@gmail.com>
// This file is subject to the license terms in the LICENSE file
// found in the top-level directory of this distribution.

#ifndef FOONATHAN_MEMORY_IMPL_IN_CONFIG_HPP
#error "do not include this file directly, use config.hpp"
#endif

#include <cstddef>

//=== options ===//
// clang-format off
#define FOONATHAN_MEMORY_CHECK_ALLOCATION_SIZE 1
#define FOONATHAN_MEMORY_IMPL_DEFAULT_ALLOCATOR heap_allocator
#define FOONATHAN_MEMORY_DEBUG_ASSERT 0
#define FOONATHAN_MEMORY_DEBUG_FILL 0
#define FOONATHAN_MEMORY_DEBUG_FENCE 0
#define FOONATHAN_MEMORY_DEBUG_LEAK_CHECK 0
#define FOONATHAN_MEMORY_DEBUG_POINTER_CHECK 0
#define FOONATHAN_MEMORY_DEBUG_DOUBLE_DEALLOC_CHECK 0
#define FOONATHAN_MEMORY_EXTERN_TEMPLATE 1
#define FOONATHAN_MEMORY_TEMPORARY_STACK_MODE 2
// clang-format on
