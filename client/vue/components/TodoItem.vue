<template lang="pug">

b-list-group-item.todo-item.bg-light
  b-form-checkbox(v-model="item.completed")
    .todo-item-name {{ item.toDoName }}
    .todo-item-notes: small.text-muted {{ item.notes }}
  .todo-item-options
    b-button(variant="danger" size="sm"  @click="$emit('delete-list-item', item)"): b-icon-trash

</template>

<script lang="ts">

import axios from 'axios';

export default {
  name: 'TodoItem',
  props: ['id', 'toDoName', 'notes', 'completed'],
  data() {
    return {
      item: {
        id: this.id,
        toDoName: this.toDoName,
        notes: this.notes,
        completed: this.completed
      },
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
};

</script>

<style lang="scss" scoped>

.todo-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 12px 14px 12px;
  font-family: 'Nunito', sans-serif;

  .todo-item-name {
    font-weight: bold;
  }

  .todo-item-notes {
    line-height: 1;
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