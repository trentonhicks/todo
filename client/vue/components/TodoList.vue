<template lang="pug">

b-container
  b-button(class="back-button" variant="link" to="/")
    b-icon(icon="chevron-left")
    | My Lists
  h1.mb-3 {{ todoList.listTitle }}

  draggable(v-model="todoListItems").todos-wrapper.mb-3
  
    todo-item(
      v-for="todo in todoListItems"
      :key="todo.id"
      :id="todo.id"
      :toDoName="todo.toDoName"
      :notes="todo.notes"
      :completed="todo.completed")
    
  b-button(@click="") Add list item

</template>

<script lang="ts">

import axios from 'axios';
import TodoItem from './TodoItem.vue';
import draggable from 'vuedraggable';

export default {
  name: 'TodoList',
  props: ['id'],
  data() {
    return {
      todoList: {},
      todoListItems: []
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
        console.log(response);
        this.todoList = response.data[0];
      }).catch((e) => {
        console.log(e);
      });
    },
    getTodoListItems(id : number) {
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists/' + id + '/todos'
      }).then((response) => {
        this.todoListItems = response.data;
        console.log(response.data);
      }).catch((e) => {
        console.log(e);
      });
    }
  },
  components: {
    TodoItem,
    draggable
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

</style>