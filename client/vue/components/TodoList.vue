<template lang="pug">

b-container
  b-button(class="back-button" variant="link" to="/")
    b-icon(icon="chevron-left")
    | Lists
  h1.mb-3 {{ todolist.listTitle }}

  .todos-wrapper
    todo-item(v-for="todo in todos" :key="todo.id" :name="todo.toDoName")

</template>

<script lang="ts">
import axios from 'axios';
import TodoItem from './TodoItem.vue';

export default {
  name: 'TodoList',
  props: ['id'],
  data() {
    return {
      todolist: {},
      todos: []
    };
  },
  created: function() {
    this.getTodoList(this.id);
    this.getTodoListItems(this.id);
  },
  methods: {
    getTodoList(id : number) : void {
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists/' + id
      }).then((response) => {
        this.todolist = response.data[0]
      }).catch((e) => {
        console.log(e);
      });
    },
    getTodoListItems(id : number) {
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists/' + id + '/todos'
      }).then((response) => {
        this.todos = response.data;
        console.log(response.data);
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