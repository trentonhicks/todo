<template lang="pug">

  .todo-list
    h1.mb-3 {{ todoList.listTitle }}

    draggable(v-model="todoListItems").todos-wrapper.mb-3

      todo-item(
        v-for="todo in todoListItems"
        :key="todo.id"
        :id="todo.id"
        :toDoName="todo.toDoName"
        :notes="todo.notes"
        :completed="todo.completed")

      b-list-group-item.no-items.bg-light(v-if="listIsEmpty") Your list is empty. Add a new item to get started.
      
    b-button(id="add-list-item-btn" @click="$bvModal.show('modal-add')") Add list item

    b-modal(id="modal-add" title="Add new list item")
      b-form(@submit="addTodoListItem(form.toDoName, form.notes)" id="add-list-item-form")
        b-form-group
          b-form-input(
            type="text"
            placeholder="List item name"
            v-model="form.toDoName"
            required
          )
        b-form-group
          b-form-input(
            type="text"
            placeholder="Notes"
            v-model="form.notes"
          )
        b-button(type="submit" variant="primary" class="mr-2") Add
        b-button(variant="secondary" @click="$bvModal.hide('modal-add')") Cancel

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
      todoListItems: [],
      form: {
        toDoName: '',
        notes: ''
      },
      listIsEmpty: false
    }
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
    getTodoListItems(id : number) : void {
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists/' + id + '/todos'
      }).then((response) => {
        this.todoListItems = response.data;

        if(this.todoListItems.length < 1) {
          this.listIsEmpty = true;
        }
        else {
          this.listIsEmpty = false;
        }
      }).catch((e) => {
        console.log(e);
      });
    },
    addTodoListItem(toDoName : string, notes : string) : void {
      this.$bvModal.hide('modal-add');

      let data = JSON.stringify({
        toDoName,
        notes
      });

      axios({
        url: `http://localhost:5000/accounts/1/lists/${this.id}/todos`,
        method: 'POST',
        data,
        headers: {
          'content-type': 'application/json'
        }
      }).then((response) => {
        if(response.status == 200) {
          this.todoListItems.push(response.data);
          this.form.toDoName = '';
          this.form.notes = '';
          this.listIsEmpty = false;
        }
      });
    }
  },
  components: {
    TodoItem,
    draggable
  }
};

</script>