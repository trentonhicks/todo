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

    draggable(v-model="todoListItems" @end="updateItemPosition").todo-list-items.mb-3

      todo-item(
        v-for="(todo, index) in todoListItems"
        v-on:deleted-list-item="deleteTodoListItem"
        v-on:toggled-list-item="updateListCompletedState"
        :key="todo.id"
        :listId="id"
        :id="todo.id"
        :name="todo.name"
        :notes="todo.notes"
        :dueDate="todo.dueDate"
        :completed="todo.completed"
        :class="`item-${index}`")

      b-list-group-item.no-items.bg-light(v-if="listIsEmpty") Your list is empty. Add a new item to get started.
      
    b-button(id="add-list-item-btn" @click="$bvModal.show('modal-add')") Add list item

    b-modal(id="modal-add" title="Add new list item")
      b-form(v-on:submit.prevent="addTodoListItem(form.name, form.notes, form.dueDate)" id="add-list-item-form")

        b-form-group(label="Name")
          b-form-input(
            type="text"
            placeholder="Name"
            v-model="form.name"
            required)
        b-form-group(label="Notes")
          b-form-textarea(
            placeholder="Notes"
            rows="4"
            v-model="form.notes"
            maxlength="200")
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
  mounted() {
    var confettiSettings = { target: 'confetti' };
    var confetti = new ConfettiGenerator(confettiSettings);
    confetti.render();
  },
  methods: {
    getTodoList(id : number) : void {
      // Get list
      axios({
        method: 'get',
        url: '/api/lists/' + id
      }).then((response) => {
        this.todoList = response.data;
      }).catch((e) => {
        console.log(e);
      });

      // Get list layout
      axios({
        method: 'get',
        url: `/api/lists/${id}/layout`
      }).then((response) => {
        this.todoListLayout = response.data;

        // Get todo list items
        axios({
          method: 'get',
          url: '/api/lists/' + id + '/todos'
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
    updateListTitle(listTitle : string) : void {
      let data = JSON.stringify({ listTitle });

      axios({
        url: `/api/lists/${this.id}`,
        method: 'PUT',
        data,
        headers: {
          'content-type': 'application/json'
        }
      });
    },
    addTodoListItem(name : string, notes : string, dueDate : Date) : void {
      this.$bvModal.hide('modal-add');

      let data = JSON.stringify({
        name,
        notes,
        dueDate
      });

      axios({
        url: `/api/lists/${this.id}/todos`,
        method: 'POST',
        data,
        headers: {
          'content-type': 'application/json'
        }
      }).then((response) => {
          this.todoListItems.unshift(response.data);
          this.form.name = '';
          this.form.notes = '';
          this.listIsEmpty = false;
      });
    },
    deleteTodoListItem(item) : void {
      this.$bvModal.msgBoxConfirm(`Are you sure you want to delete ${item.name}?`, {
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
                url: `/api/todos/${item.id}`,
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
    updateListCompletedState(item) : void {
      let index = this.todoListItems.findIndex(({id}) => id === item.id);
      this.$set(this.todoListItems, index, item)
    },
    throwConfetti() : void {
      this.confetti = true;
    },
    hideConfetti() : void {
      this.confetti = false;
    },
    updateItemPosition(e) : void {
      let position = e.newIndex;
      let itemId = parseInt(e.item.dataset.id);
      let data = JSON.stringify({
        position,
        itemId
      });
      
      axios({
        method: 'PUT',
        url: `/api/lists/${this.id}/layout`,
        data,
        headers: {
          'content-type': 'application/json'
        }
      });
    }
  },
  components: {
    TodoItem,
    draggable
  },
  computed: {
    allItemsCompleted() {
      return this.todoListItems.every(item => item.completed)
    }
  },
  watch: {
    allItemsCompleted: {
      handler: function() {
        if(this.allItemsCompleted) {
          this.throwConfetti();
        }
        else {
          this.hideConfetti();
        }
      },
      immediate: true
    }
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