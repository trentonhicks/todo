<template>
    
    <b-list-group class="todo-list-items">
        <b-list-group-item
            v-if="!todoListItems">
            Add an item to get started.
        </b-list-group-item>

        <TodoListItem
            v-for="item in todoListItems"
            :key="item.id"
            :todoListItem="item"
            class="todo-list-item">
        </TodoListItem>
    </b-list-group>

</template>

<script>

    import TodoListItem from './TodoListItem';

    export default {
        name: 'TodoListItems',
        props: ['todoListItems'],
        components: {
            TodoListItem
        }
    }

</script>