<template lang="pug">

.todo-item-wrapper
  b-list-group-item.todo-item.bg-light
    b-form-checkbox(v-model="item.completed")
      .todo-item-name {{ item.toDoName }}
      .todo-item-notes(v-if="item.notes"): small.text-muted {{ item.notes }}
    .todo-item-options
      b-button(variant="info" size="sm"  @click="$bvModal.show('modal-edit-' + item.id)"): b-icon-three-dots
      b-button(variant="danger" size="sm"  @click="$emit('delete-list-item', item)"): b-icon-trash

  b-modal(:id="'modal-edit-' + item.id" :title="`Edit todo item`" modal-class="modal-hide-footer")
    b-form(v-on:submit.prevent="editTodoItem" id="edit-item-form")

        b-form-group(label="Name")
          b-form-input(
            type="text"
            placeholder="Name"
            v-model="item.toDoName"
            required)
        b-form-group(label="Notes")
          b-form-textarea(
            placeholder="Notes"
            rows="3"
            v-model="item.notes")
        b-form-group(label="Due Date")
          b-form-datepicker(v-model="item.dueDate").mb-2
          label(v-if="item.dueDate") Current due date: {{ item.dueDate | formatDate }}

        b-button(type="submit" variant="primary" class="mr-2") Save Changes
        b-button(variant="secondary" @click="$bvModal.hide('modal-edit-' + item.id)") Cancel

</template>

<script lang="ts">

import axios from 'axios';
import moment from 'moment';

export default {
  name: 'TodoItem',
  props: ['id', 'toDoName', 'notes', 'dueDate', 'completed'],
  data() {
    return {
      item: {
        id: this.id,
        toDoName: this.toDoName,
        notes: this.notes,
        completed: this.completed,
        dueDate: this.dueDate
      }
    };
  },
  methods: {
    toggleCompleted() {
      axios({
        method: 'PUT',
        url: `http://localhost:5000/accounts/1/todos/${this.item.id}/completed`,
        data: this.item.completed,
        headers: {
          'content-type': 'application/json'
        }
      })
    },
    editTodoItem() {
      this.$bvModal.hide('modal-edit-' + this.item.id);

      let data = JSON.stringify(this.item);

      axios({
        method: 'PUT',
        url: `http://localhost:5000/accounts/1/todos/${this.item.id}`,
        data,
        headers: {
          'content-type': 'application/json'
        }
      });
    }
  },
  watch: {
    checkboxToggle: function() {
      this.toggleCompleted();
    }
  },
  computed: {
    checkboxToggle() {
      return this.item.completed;
    }
  },
  filters: {
    formatDate: function(value) : string {
      return moment(value).format('dddd, MMMM Do YYYY');
    }
  }
};

</script>

<style lang="scss" scoped>

.todo-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 14px 12px 14px 12px;
  font-family: 'Nunito', sans-serif;

  .todo-item-name {
    margin-top: 2px;
    font-weight: bold;
    line-height: 1.1;
    max-width: 160px;
  }

  .todo-item-notes {
    line-height: 1;
    margin-top: 3px;
    max-width: 160px;
  }

  .todo-item-options {
    display: flex;
    justify-content: flex-end;
    align-items: center;

    .btn {
        height: 36px;
        width: 36px;
        border-radius: 100px;
        display: flex; 
        align-items: center;
        justify-content: center;

        &:not(:last-child) {
          margin-right: 8px;
        }
    }
  }

}

</style>