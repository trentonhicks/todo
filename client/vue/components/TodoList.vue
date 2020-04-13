<template lang="pug">

  .todo-list

    canvas(v-show="confetti")#confetti

    .list-title.mb-3
      h1(v-if="!editingTitle" @click="editingTitle = true") {{ todoList.listTitle }}
      input(
        v-if="editingTitle"
        type="text"
        v-model="todoList.listTitle" 
        class="form-control" 
        @keydown.enter="editingTitle = false"
        @blur="editingTitle = false; updateListTitle(todoList.listTitle)"
        v-focus="")

    draggable(v-model="todoListItems").todo-list-items.mb-3

      todo-item(
        v-for="(todo, index) in todoListItems"
        v-on:deleted-list-item="deleteTodoListItem"
        v-on:toggled-list-item="checkIfListCompleted"
        :key="todo.id"
        :id="todo.id"
        :toDoName="todo.toDoName"
        :notes="todo.notes"
        :dueDate="todo.dueDate"
        :completed="todo.completed"
        :class="`item-${index}`")

      b-list-group-item.no-items.bg-light(v-if="listIsEmpty") Your list is empty. Add a new item to get started.
      
    b-button(id="add-list-item-btn" @click="$bvModal.show('modal-add')") Add list item

    b-modal(id="modal-add" title="Add new list item")
      b-form(v-on:submit.prevent="addTodoListItem(form.toDoName, form.notes, form.dueDate)" id="add-list-item-form")

        b-form-group(label="Name")
          b-form-input(
            type="text"
            placeholder="Name"
            v-model="form.toDoName"
            required)
        b-form-group(label="Notes")
          b-form-textarea(
            placeholder="Notes"
            rows="3"
            v-model="form.notes")
        b-form-group(label="Due Date")
          b-form-datepicker(v-model="form.dueDate")

        b-button(type="submit" variant="primary" class="mr-2") Add
        b-button(variant="secondary" @click="$bvModal.hide('modal-add')") Cancel

</template>

<script lang="ts">

import axios from 'axios';
import TodoItem from './TodoItem.vue';
import draggable from 'vuedraggable';
import ConfettiGenerator from "confetti-js";

export default {
  name: 'TodoList',
  props: ['id'],
  data() {
    return {
      todoList: {},
      todoListLayout: [],
      todoListItems: [],
      form: {},
      listIsEmpty: false,
      editingTitle: false,
      confetti: false
    }
  },
  created: function() {
    this.getTodoList(this.id);
  },
  methods: {
    getTodoList(id : number) : void {
      // Get list
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists/' + id
      }).then((response) => {
        this.todoList = response.data;
      }).catch((e) => {
        console.log(e);
      });

      // Get list layout
      axios({
        method: 'get',
        url: `http://localhost:5000/accounts/1/lists/${id}/layout`
      }).then((response) => {
        this.todoListLayout = response.data;

        // Get todo list items
        axios({
          method: 'get',
          url: 'http://localhost:5000/accounts/1/lists/' + id + '/todos'
        }).then((response) => {

          this.todoListLayout.forEach(position => {
            let index = response.data.findIndex(item => item.id === position);

            if(index !== -1) {
              this.todoListItems.push(response.data[index]);
            }
          });

          if(this.todoListItems.length < 1) {
            this.listIsEmpty = true;
          }
          else {
            this.listIsEmpty = false;
          }
        });
      });
    },
    updateListTitle(listTitle : string) {
      let data = JSON.stringify({ listTitle });

      axios({
        url: `http://localhost:5000/accounts/1/lists/${this.id}`,
        method: 'PUT',
        data,
        headers: {
          'content-type': 'application/json'
        }
      });
    },
    addTodoListItem(toDoName : string, notes : string, dueDate : Date) : void {
      this.$bvModal.hide('modal-add');

      let data = JSON.stringify({
        toDoName,
        notes,
        dueDate
      });

      axios({
        url: `http://localhost:5000/accounts/1/lists/${this.id}/todos`,
        method: 'POST',
        data,
        headers: {
          'content-type': 'application/json'
        }
      }).then((response) => {
          this.todoListItems.unshift(response.data);
          this.form.toDoName = '';
          this.form.notes = '';
          this.listIsEmpty = false;
      });
    },
    deleteTodoListItem(item) : void {
      this.$bvModal.msgBoxConfirm(`Are you sure you want to delete ${item.toDoName}?`, {
          size: 'sm',
          okVariant: 'danger',
          okTitle: 'Delete',
          cancelTitle: 'Cancel',
          footerClass: 'p-2',
          hideHeaderClose: false,
      })
      .then(choseToDelete => {
          if(choseToDelete) {
              axios({
                url: `http://localhost:5000/accounts/1/todos/${item.id}`,
                method: 'DELETE'
              }).then((response) => {
                  let index = this.todoListItems.findIndex(({id}) => id === item.id);
                  if(index !== -1) {
                      this.todoListItems.splice(index, 1);
                  }
              });
          }
      });
    },
    checkIfListCompleted() {
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists/' + this.id
      }).then((response) => {
        if(response.data.completed === true) {
          this.confetti = true;
          var confettiSettings = { target: 'confetti' };
          var confetti = new ConfettiGenerator(confettiSettings);
          confetti.render();
        }
        else {
          this.confetti = false;
        }
      })
    }
  },
  components: {
    TodoItem,
    draggable
  },
  directives: {
    focus: {
      inserted (el) {
        el.focus()
      }
    }
  }
};

</script>

<style lang="scss" scoped>

#confetti {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  pointer-events: none;
  z-index: 500;
}

</style>