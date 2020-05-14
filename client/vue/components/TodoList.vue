<template lang="pug">

  .todo-list

    canvas(v-show="confetti")#confetti

    .list-title.mb-3
      h1(v-if="!editingTitle" @click="toggleTitleEditor") {{ todoList.listTitle }}
      input(
        v-if="editingTitle"
        type="text"
        v-model="todoList.listTitle" 
        class="form-control" 
        @blur="toggleTitleEditor"
        @keydown.enter="$event.target.blur()"
        v-focus="")

    .invalid-feedback(:class="{ 'd-block mb-3': invalidTitle }") Title must be between 1 and 50 characters long.

    b-row

      b-col(md=9)

        draggable(v-model="todoListItems" @end="updateItemPosition" handle=".todo-item-handle").todo-list-items.mb-3

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

        b-button(id="add-list-item-btn" @click="showAddItemModal").mb-3 Add list item

      b-col(md=3)
        b-form(@submit.prevent="sendInvitation")
          b-form-group
            b-form-input(v-model="invitationToList.email" placeholder="email" type="email" required)
          b-button(type="submit" class="btn-block") Invite user

    b-modal(id="modal-add" title="Add new list item")
      b-form(v-on:submit.prevent="saveTodoListItem(form.name, form.notes, form.dueDate)" id="add-list-item-form")

        b-form-group(label="Name")
          b-form-input(
            class="form-input-focus"
            type="text"
            placeholder="Name"
            v-model="form.name"
            maxlength="50"
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
      todoList: {
        listTitle: ''
      },
      todoListLayout: [],
      todoListItems: [],
      form: {},
      invitationToList: {
        email: ''
      },
      editingTitle: false,
      confetti: false,
    }
  },
  created: function() {
    this.getTodoList(this.id);
  },
  methods: {
    showAddItemModal() {
      this.$bvModal.show('modal-add');
    },
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
        });
      });
    },
    toggleTitleEditor() {

      // User wants to save changes
      if(this.editingTitle && !this.invalidTitle) {
        this.editingTitle = false;
        this.updateListTitle(this.todoList.listTitle);
      }

      // User wants to edit title
      else {
        this.editingTitle = true;
      }
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
    saveTodoListItem(name : string, notes : string, dueDate : Date) : void {
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
          this.form.name = '';
          this.form.notes = '';
      });
    },
    addTodoListItemToList(listId, item) {
      if(this.id == listId) {
        this.todoListItems.unshift(item);
      }
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
              });
          }
      });
    },
    removeTodoListItem(listId, item) : void {
      let index = this.todoListItems.findIndex(({id}) => id === item.id);
      if(index !== -1) {
          this.todoListItems.splice(index, 1);
      }
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
      let itemId = e.item.dataset.id;
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
    },
    sendInvitation() : void {
      let data = JSON.stringify({ listId: this.id, email: this.invitationToList.email });

      axios({
        method: 'POST',
        url: `/api/lists/${this.id}/email`,
        data, 
        headers: {
          'content-type': 'application/json'
        }
      }).then(function() {
        this.invitationToList.email = '';
      });
    }
  },
  components: {
    TodoItem,
    draggable
  },
  computed: {
    allItemsCompleted() {
      return this.todoListItems.every(item => item.completed) && this.todoListItems.length > 0
    },
    invalidTitle() {
      if(this.todoList.listTitle.length === 0 || this.todoList.listTitle.length > 50) {
        return true;
      }
      return false;
    },
    listIsEmpty(){
      return this.todoListItems.length < 1;
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
  mounted: function() {
    var confettiSettings = { target: 'confetti' };
    var confetti = new ConfettiGenerator(confettiSettings);
    confetti.render();

    this.$root.$on('bv::modal::shown', (bvEvent, modalId) => {
      let formInput = document.querySelector(`#${modalId} .form-input-focus`);
      formInput.focus();
    });

    this.$store.state.connection.on("ItemCreated", (listId, item) => this.addTodoListItemToList(listId, item));
    this.$store.state.connection.on("ItemTrashed", (listId, item) => this.removeTodoListItem(listId, item));

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