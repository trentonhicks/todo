<template>

    <div class="todo-list">

        <h1 class="todo-list-title" v-if="list.listTitle">{{ list.listTitle }}</h1>

        <b-row>

            <b-col md="8">

                <TodoListItems
                    :listId="todoListId"
                    :todoListItems="items"></TodoListItems>

                <AddTodoListItemForm
                    class="mt-3"
                    :todoListId="todoListId">
                </AddTodoListItemForm>

            </b-col>

            <b-col md="4">

                <Contributors
                    class="mb-3"
                    :todoListContributors="list.contributors"
                    :accountContributors="contributors">
                </Contributors>

                <InviteContributorsForm
                    :listId="this.todoListId">
                </InviteContributorsForm>

            </b-col>

        </b-row>

    </div>
    
</template>

<script>

    import AddTodoListItemForm from './AddTodoListItemForm';
    import TodoListItems from './TodoListItems';
    import Contributors from './Contributors';
    import InviteContributorsForm from './InviteContributorsForm';

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
            contributors() {
                return this.$store.getters.contributors;
            },
            allItemsCompleted() {
                return this.items.every(item => item.completed === true) && this.items.length > 0;
            }
        },
        components: {
            AddTodoListItemForm,
            TodoListItems,
            Contributors,
            InviteContributorsForm
        },
        methods: {
            getItems() {
                return this.$store.getters.getItemsByListId(this.todoListId);
            }
        },
    }

</script>