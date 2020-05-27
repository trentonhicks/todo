<template>
    
    <b-list-group-item class="todo-item bg-light">
        <b-form-checkbox class="todo-item-checkbox" v-model="todoListItem.completed" @change="toggleItemCompletedState">
            {{ todoListItem.name }}
        </b-form-checkbox>

        <div class="todo-list-item-preview-options">
            <b-button-group>
                <b-button variant="info" @click="$bvModal.show(`modal-${todoListItem.id}`)">View</b-button>
                <b-button variant="danger">Delete</b-button>
            </b-button-group>
            <EditTodoItemForm :todoListItem="todoListItem"></EditTodoItemForm>
        </div>

    </b-list-group-item>

</template>

<script>

    import EditTodoItemForm from './EditTodoItemForm';

    export default {
        name: 'TodoListItem',
        props: ['todoListItem'],
        components: {
            EditTodoItemForm
        },
        methods: {
            toggleItemCompletedState() {
                this.$store.dispatch('toggleItemCompletedState', { id: this.todoListItem.id, completed: this.todoListItem.completed });
            }
        },
    }
    
</script>

<style lang="scss" scoped>

    .todo-item {
        display: flex !important;
        align-items: center;
        justify-content: space-between;
    }

</style>