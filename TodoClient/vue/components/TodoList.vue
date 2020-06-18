<template>

    <div class="todo-list-wrapper">
        <canvas id="confetti" :class="{ 'hidden': !allItemsCompleted }"></canvas>

        <div class="todo-list">

            <h1 class="todo-list-title mb-4" v-if="list.listTitle">{{ list.listTitle }}</h1>

            <b-row>

                <b-col md="8" class="mb-3">

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
    </div>
    
</template>

<script>

    import ConfettiGenerator from "confetti-js";

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
        beforeUpdate() {
            var confettiSettings = { target: 'confetti' };
            var confetti = new ConfettiGenerator(confettiSettings);
            confetti.render();
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

<style lang="scss" scoped>

    #confetti {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        pointer-events: none;
        z-index: 1040;

        &.hidden {
            opacity: 0;
        }
    }

</style>