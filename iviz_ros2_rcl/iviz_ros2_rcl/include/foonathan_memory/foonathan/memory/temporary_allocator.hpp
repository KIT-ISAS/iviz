// Copyright (C) 2015-2021 Müller <jonathanmueller.dev@gmail.com>
// This file is subject to the license terms in the LICENSE file
// found in the top-level directory of this distribution.

#ifndef FOONATHAN_MEMORY_TEMPORARY_ALLOCATOR_HPP_INCLUDED
#define FOONATHAN_MEMORY_TEMPORARY_ALLOCATOR_HPP_INCLUDED

/// \file
/// Class \ref foonathan::memory::temporary_allocator and related functions.

#include "config.hpp"
#include "memory_stack.hpp"

#if FOONATHAN_MEMORY_TEMPORARY_STACK_MODE >= 2
#include <atomic>
#endif

namespace foonathan
{
    namespace memory
    {
        class temporary_allocator;
        class temporary_stack;

        namespace detail
        {
            class temporary_block_allocator
            {
            public:
                explicit temporary_block_allocator(std::size_t block_size) noexcept;

                memory_block allocate_block();

                void deallocate_block(memory_block block);

                std::size_t next_block_size() const noexcept
                {
                    return block_size_;
                }

                using growth_tracker = void (*)(std::size_t size);

                growth_tracker set_growth_tracker(growth_tracker t) noexcept;

                growth_tracker get_growth_tracker() noexcept;

            private:
                growth_tracker tracker_;
                std::size_t    block_size_;
            };

            using temporary_stack_impl = memory_stack<temporary_block_allocator>;

            class temporary_stack_list;

#if FOONATHAN_MEMORY_TEMPORARY_STACK_MODE >= 2
            class temporary_stack_list_node
            {
            public:
                // doesn't add into list
                temporary_stack_list_node() noexcept : in_use_(true) {}

                temporary_stack_list_node(int) noexcept;

                ~temporary_stack_list_node() noexcept {}

            private:
                temporary_stack_list_node* next_ = nullptr;
                std::atomic<bool>          in_use_;

                friend temporary_stack_list;
            };

            static class temporary_allocator_dtor_t
            {
            public:
                temporary_allocator_dtor_t() noexcept;
                ~temporary_allocator_dtor_t() noexcept;
            } temporary_allocator_dtor;
#else
            class temporary_stack_list_node
            {
            protected:
                temporary_stack_list_node() noexcept {}

                temporary_stack_list_node(int) noexcept {}

                ~temporary_stack_list_node() noexcept {}
            };
#endif
        } // namespace detail

        /// A wrapper around the \ref memory_stack that is used by the \ref temporary_allocator.
        /// There should be at least one per-thread.
        /// \ingroup allocator
        class temporary_stack : FOONATHAN_EBO(detail::temporary_stack_list_node)
        {
        public:
            /// The type of the handler called when the internal \ref memory_stack grows.
            /// It gets the size of the new block that will be allocated.
            /// \requiredbe The handler shall log the growth, throw an exception or aborts the program.
            /// If this function does not return, the growth is prevented but the allocator unusable until memory is freed.
            /// \defaultbe The default handler does nothing.
            using growth_tracker = detail::temporary_block_allocator::growth_tracker;

            /// \effects Sets \c h as the new \ref growth_tracker.
            /// A \c nullptr sets the default \ref growth_tracker.
            /// Each thread has its own, separate tracker.
            /// \returns The previous \ref growth_tracker. This is never \c nullptr.
            growth_tracker set_growth_tracker(growth_tracker t) noexcept
            {
                return stack_.get_allocator().set_growth_tracker(t);
            }

            /// \returns The current \ref growth_tracker. This is never \c nullptr.
            growth_tracker get_growth_tracker() noexcept
            {
                return stack_.get_allocator().get_growth_tracker();
            }

            /// \effects Creates it with a given initial size of the stack.
            /// It can grow if needed, although that is expensive.
            /// \requires `initial_size` must be greater than `0`.
            explicit temporary_stack(std::size_t initial_size) : stack_(initial_size), top_(nullptr)
            {
            }

            /// \returns `next_capacity()` of the internal `memory_stack`.
            std::size_t next_capacity() const noexcept
            {
                return stack_.next_capacity();
            }

        private:
            temporary_stack(int i, std::size_t initial_size)
            : detail::temporary_stack_list_node(i), stack_(initial_size), top_(nullptr)
            {
            }

            using marker = detail::temporary_stack_impl::marker;

            marker top() const noexcept
            {
                return stack_.top();
            }

            void unwind(marker m) noexcept
            {
                stack_.unwind(m);
            }

            detail::temporary_stack_impl stack_;
            temporary_allocator*         top_;

#if !defined(DOXYGEN)
            friend temporary_allocator;
            friend memory_stack_raii_unwind<temporary_stack>;
            friend detail::temporary_stack_list;
#endif
        };

        /// Manually takes care of the lifetime of the per-thread \ref temporary_stack.
        /// The constructor will create it, if not already done, and the destructor will destroy it, if not already done.
        /// \note If there are multiple objects in a thread,
        /// this will lead to unnecessary construction and destruction of the stack.
        /// It is thus adviced to create one object on the top-level function of the thread, e.g. in `main()`.
        /// \note If `FOONATHAN_MEMORY_TEMPORARY_STACK_MODE == 2`, it is not necessary to use this class,
        /// the nifty counter will clean everything upon program termination.
        /// But it can still be used as an optimization if you have a thread that is terminated long before program exit.
        /// The automatic clean up will only occur much later.
        /// \note If `FOONATHAN_MEMORY_TEMPORARY_STACK_MODE == 0`, the use of this class has no effect,
        /// because the per-thread stack is disabled.
        /// \relatesalso temporary_stack
        class temporary_stack_initializer
        {
        public:
            static constexpr std::size_t default_stack_size = 4096u;

            static const struct defer_create_t
            {
                defer_create_t() noexcept {}
            } defer_create;

            /// \effects Does not create the per-thread stack.
            /// It will be created by the first call to \ref get_temporary_stack() in the current thread.
            /// \note If `FOONATHAN_MEMORY_TEMPORARY_STACK_MODE == 0`, this function has no effect.
            temporary_stack_initializer(defer_create_t) noexcept {}

            /// \effects Creates the per-thread stack with the given default size if it wasn't already created.
            /// \requires `initial_size` must not be `0` if `FOONATHAN_MEMORY_TEMPORARY_STACK_MODE != 0`.
            /// \note If `FOONATHAN_MEMORY_TEMPORARY_STACK_MODE == 0`, this function will issue a warning in debug mode.
            /// This can be disabled by passing `0` as the initial size.
            temporary_stack_initializer(std::size_t initial_size = default_stack_size);

            /// \effects Destroys the per-thread stack if it isn't already destroyed.
            ~temporary_stack_initializer() noexcept;

            temporary_stack_initializer(temporary_stack_initializer&&) = delete;
            temporary_stack_initializer& operator=(temporary_stack_initializer&&) = delete;
        };

        /// \effects Creates the per-thread \ref temporary_stack with the given initial size,
        /// if it wasn't already created.
        /// \returns The per-thread \ref temporary_stack.
        /// \requires There must be a per-thread temporary stack (\ref FOONATHAN_MEMORY_TEMPORARY_STACK_MODE must not be equal to `0`).
        /// \note If \ref FOONATHAN_MEMORY_TEMPORARY_STACK_MODE is equal to `1`,
        /// this function can create the temporary stack.
        /// But if there is no \ref temporary_stack_initializer, it won't be destroyed.
        /// \relatesalso temporary_stack
        temporary_stack& get_temporary_stack(
            std::size_t initial_size = temporary_stack_initializer::default_stack_size);

        /// A stateful \concept{concept_rawallocator,RawAllocator} that handles temporary allocations.
        /// It works similar to \c alloca() but uses a seperate \ref memory_stack for the allocations,
        /// instead of the actual program stack.
        /// This avoids the stack overflow error and is portable,
        /// with a similar speed.
        /// All allocations done in the scope of the allocator object are automatically freed when the object is destroyed.
        /// \ingroup allocator
        class temporary_allocator
        {
        public:
            /// \effects Creates it by using the \ref get_temporary_stack() to get the temporary stack.
            /// \requires There must be a per-thread temporary stack (\ref FOONATHAN_MEMORY_TEMPORARY_STACK_MODE must not be equal to `0`).
            temporary_allocator();

            /// \effects Creates it by giving it the \ref temporary_stack it uses for allocation.
            explicit temporary_allocator(temporary_stack& stack);

            ~temporary_allocator() noexcept;

            temporary_allocator(temporary_allocator&&) = delete;
            temporary_allocator& operator=(temporary_allocator&&) = delete;

            /// \effects Allocates memory from the internal \ref memory_stack by forwarding to it.
            /// \returns The result of \ref memory_stack::allocate().
            /// \requires `is_active()` must return `true`.
            void* allocate(std::size_t size, std::size_t alignment);

            /// \returns Whether or not the allocator object is active.
            /// \note The active allocator object is the last object created for one stack.
            /// Moving changes the active allocator.
            bool is_active() const noexcept;

            /// \effects Instructs it to release unnecessary memory after automatic unwinding occurs.
            /// This will effectively forward to \ref memory_stack::shrink_to_fit() of the internal stack.
            /// \note Like the use of the \ref temporary_stack_initializer this can be used as an optimization,
            /// to tell when the thread's \ref temporary_stack isn't needed anymore and can be destroyed.
            /// \note It doesn't call shrink to fit immediately, only in the destructor!
            void shrink_to_fit() noexcept;

            /// \returns The internal stack the temporary allocator is using.
            /// \requires `is_active()` must return `true`.
            temporary_stack& get_stack() const noexcept
            {
                return unwind_.get_stack();
            }

        private:
            memory_stack_raii_unwind<temporary_stack> unwind_;
            temporary_allocator*                      prev_;
            bool                                      shrink_to_fit_;
        };

        template <class Allocator>
        class allocator_traits;

        /// Specialization of the \ref allocator_traits for \ref temporary_allocator classes.
        /// \note It is not allowed to mix calls through the specialization and through the member functions,
        /// i.e. \ref temporary_allocator::allocate() and this \c allocate_node().
        /// \ingroup allocator
        template <>
        class allocator_traits<temporary_allocator>
        {
        public:
            using allocator_type = temporary_allocator;
            using is_stateful    = std::true_type;

            /// \returns The result of \ref temporary_allocator::allocate().
            static void* allocate_node(allocator_type& state, std::size_t size,
                                       std::size_t alignment)
            {
                detail::check_allocation_size<bad_node_size>(size,
                                                             [&] { return max_node_size(state); },
                                                             {FOONATHAN_MEMORY_LOG_PREFIX
                                                              "::temporary_allocator",
                                                              &state});
                return state.allocate(size, alignment);
            }

            /// \returns The result of \ref temporary_allocator::allocate().
            static void* allocate_array(allocator_type& state, std::size_t count, std::size_t size,
                                        std::size_t alignment)
            {
                return allocate_node(state, count * size, alignment);
            }

            /// @{
            /// \effects Does nothing besides bookmarking for leak checking, if that is enabled.
            /// Actual deallocation will be done automatically if the allocator object goes out of scope.
            static void deallocate_node(const allocator_type&, void*, std::size_t,
                                        std::size_t) noexcept
            {
            }

            static void deallocate_array(const allocator_type&, void*, std::size_t, std::size_t,
                                         std::size_t) noexcept
            {
            }
            /// @}

            /// @{
            /// \returns The maximum size which is \ref memory_stack::next_capacity() of the internal stack.
            static std::size_t max_node_size(const allocator_type& state) noexcept
            {
                return state.get_stack().next_capacity();
            }

            static std::size_t max_array_size(const allocator_type& state) noexcept
            {
                return max_node_size(state);
            }
            /// @}

            /// \returns The maximum possible value since there is no alignment restriction
            /// (except indirectly through \ref memory_stack::next_capacity()).
            static std::size_t max_alignment(const allocator_type&) noexcept
            {
                return std::size_t(-1);
            }
        };
    } // namespace memory
} // namespace foonathan

#endif // FOONATHAN_MEMORY_TEMPORARY_ALLOCATOR_HPP_INCLUDED
