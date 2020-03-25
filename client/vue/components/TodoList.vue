<template lang="pug">

b-container
  b-button(class="back-button" variant="link" to="/")
    b-icon(icon="chevron-left")
    | Lists
  h1.mb-3 List {{ id }}

  b-list-group
    todo-item(v-for="todo in todos" :key="todo.id" :name="todo.name")

</template>

<script lang="ts">
import axios from 'axios';
import TodoItem from './TodoItem.vue';

export default {
  name: 'TodoList',
  props: ['id'],
  data() {
    return {
      todos: [
        { id: 1, name: "Do something" },
        { id: 2, name: "Do another thing" },
        { id: 3, name: "Do something else" },
      ]
    };
  },
  methods: {
    created: function() {
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists',
      })
      .then((response) => {
        console.log(response);
        this.todoLists = response.data
      }).catch((e) => {
        console.log(e);
      });
    }
  },
  components: {
    TodoItem
  }
};

</script>

<style lang="scss" scoped>

.back-button {
  display: flex;
  align-items: center;
  padding: 0px;
  position: relative;
  left: -7px;
  margin-bottom: 10px;
}

.todo-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

</style>