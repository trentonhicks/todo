<template>

    <div class="todo-list">

        <h1 class="todo-list-title" v-if="list.listTitle">{{ list.listTitle }}</h1>

        <TodoListItems
            :todoListItems="items"></TodoListItems>

        <AddTodoListItemForm
            class="mt-3"
            :todoListId="todoListId">
        </AddTodoListItemForm>

    </div>
    
</template>

<script>

    import AddTodoListItemForm from './AddTodoListItemForm';
    import TodoListItems from './TodoListItems';

    export default {
        name: "TodoList",
        props: ['todoListId'],
        data() {
            return {
                items: []
            }
        },
        created() {
            this.$store.dispatch('loadItemsByListId', { todoListId: this.todoListId }).then(() => {
                this.items = this.getItems();
            });
        },
        computed: {
            list() {
                return this.$store.getters.getTodoListById(this.todoListId);
            },
            allItemsCompleted() {
                return this.items.every(item => item.completed === true) && this.items.length > 0;
            }
        },
        components: {
            AddTodoListItemForm,
            TodoListItems
        },
        methods: {
            getItems() {
                return this.$store.getters.getItemsByListId(this.todoListId);
            }
        },
    }

</script>