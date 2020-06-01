<template>
    
    <b-list-group-item class="todo-item bg-light">
        <b-form-checkbox
            class="todo-item-checkbox"
            v-model="itemCompletedState">
        </b-form-checkbox>

        <div class="todo-item-details">
            <div class="todo-item-name">{{ todoListItem.name }}</div>
            <div class="todo-item-due-date" v-if="todoListItem.dueDate"><b-icon-calendar></b-icon-calendar> {{ todoListItem.dueDate | formatDate }}</div>
            <div class="todo-item-notes" v-if="todoListItem.notes"><b-icon-text-left></b-icon-text-left> {{ todoListItem.notes | truncate(30, '...') }}</div>
        </div>

        <div class="todo-item-options">
            <b-button-group>
                <b-button variant="info" @click="$bvModal.show(`modal-${todoListItem.id}`)">View</b-button>
                <b-button variant="danger">Delete</b-button>
            </b-button-group>
            <EditTodoItemForm :todoListItem="todoListItem"></EditTodoItemForm>
        </div>

    </b-list-group-item>

</template>

<script>

    import moment from 'moment';
    import EditTodoItemForm from './EditTodoItemForm';

    export default {
        name: 'TodoListItem',
        props: ['todoListItem'],
        components: {
            EditTodoItemForm
        },
        computed: {
            itemCompletedState: {
                get() {
                    return this.$store.getters.getItemCompletedState(this.todoListItem.listId, this.todoListItem.id);
                },
                set(value) {
                    this.$store.dispatch('toggleItemCompletedState', {
                        listId: this.todoListItem.listId,
                        itemId: this.todoListItem.id,
                        completed: value
                    });
                }
            }
        },
        filters: {
            formatDate: function(value) {
                return moment(value).format('MM/D/YYYY');
            },
            truncate: function(text, length, suffix) {
                return text.substring(0, length) + suffix;
            }
        },
    }
    
</script>

<style lang="scss" scoped>

    .todo-item {
        display: flex !important;
        align-items: flex-start;
        flex-wrap: wrap;

        .todo-item-details {
            flex: 1 1 auto;
            margin-left: 7px;
            font-family: 'Nunito', sans-serif;

            .todo-item-name {
                display: block;
                font-weight: bold;
                font-size: 18px;
                line-height: 1.3;
                margin-bottom: 7px;
            }

            .todo-item-due-date,
            .todo-item-notes {
                font-size: 14px;
            }
        }

        .todo-item-options {
            align-self: center;
            margin-top: 14px;
            margin-left: 31px;

            @media screen and (min-width: 521px) {
                margin: 0;
            }
        }
    }

</style>