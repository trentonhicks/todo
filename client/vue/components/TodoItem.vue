<template lang="pug">

.todo-item-wrapper(:data-id="item.id")

  //- Todo List Item
  b-list-group-item.todo-item.bg-light

    //- Item Info
    b-form-checkbox(v-model="item.completed" :disabled="hasSubItems")
      .todo-item-name {{ item.name }}
      .todo-item-date(v-if="item.dueDate" :class="{ 'text-info': !itemDueToday, 'text-danger': itemDueToday }")
        b-icon-clock
        | {{ item.dueDate | monthDay }}
      .todo-item-notes(v-if="item.notes"): small.text-muted {{ item.notes }}

    //- Item Options
    .todo-item-options
      b-button(variant="info" size="sm"  @click="$bvModal.show('modal-edit-' + item.id)"): b-icon-three-dots
      b-button(variant="danger" size="sm"  @click="$emit('deleted-list-item', item)"): b-icon-trash

  //- Edit Todo List Item Modal
  b-modal(:id="'modal-edit-' + item.id" :title="`Edit todo item`" modal-class="modal-hide-footer")

    //- Edit Form
    b-form(v-on:submit.prevent="" id="edit-item-form")

        //- Name
        b-form-group(label="Name")
          b-form-input(
            type="text"
            placeholder="Name"
            v-model="form.name"
            required)

        //- Notes
        b-form-group(label="Notes")
          b-form-textarea(
            placeholder="Notes"
            rows="4"
            v-model="form.notes"
            maxlength="200")

        //- Due Dates
        b-form-group(label="Due Date")
          b-form-datepicker(v-model="form.dueDate").mb-2

        //- Sub-items List
        b-form-group(label="Sub-items")
          draggable(v-model="subItems")

            //- Sub-item Component
            sub-item(
              v-for="item in subItems"
              :key="item.id"
              :id="item.id"
              :name="item.name"
              :completed="completed")

          //- Add sub-item
          b-button(variant="secondary" class="btn-block mt-2" @click="addingSubItem = true" v-if="!addingSubItem") Add sub-item

          .adding-sub-item(v-if="addingSubItem").mt-2
            b-form-group
              b-form-input(id="add-sub-item" v-model="subItemForm.name" @keydown.enter="addSubItem()" placeholder="Add sub-item" v-focus)
              b-button(variant="success" @click="addSubItem()").mt-2 Add
              b-button(variant="secondary" @click="addingSubItem = false").mt-2.ml-2 Cancel

        b-button(@click="editTodoItem" variant="primary" class="mr-2") Save Changes
        b-button(variant="secondary" @click="$bvModal.hide('modal-edit-' + item.id)") Cancel

</template>

<script lang="ts">

import axios from 'axios';
import moment from 'moment';
import SubItem from './SubItem.vue';

export default {
  name: 'TodoItem',
  props: ['listId', 'id', 'name', 'notes', 'dueDate', 'completed'],
  data() {
    return {
      item: {
        id: this.id,
        name: this.name,
        notes: this.notes,
        completed: this.completed,
        dueDate: this.dueDate
      },
      form: {
        id: this.id,
        name: this.name,
        notes: this.notes,
        completed: this.completed,
        dueDate: this.dueDate
      },
      addingSubItem: false,
      subItemForm: {
        name: '',
      },
      subItems: []
    };
  },
  methods: {
    toggleCompleted() {
      axios({
        method: 'PUT',
        url: `http://localhost:5000/accounts/1/todos/${this.item.id}/completed`,
        data: JSON.stringify({ completed: this.item.completed }),
        headers: {
          'content-type': 'application/json'
        }
      }).then(() => {
        this.$emit('toggled-list-item');
      });
    },
    editTodoItem() {
      this.$bvModal.hide('modal-edit-' + this.item.id);
      let data = JSON.stringify(this.form);
      this.item = JSON.parse(data);

      axios({
        method: 'PUT',
        url: `http://localhost:5000/accounts/1/todos/${this.item.id}`,
        data,
        headers: {
          'content-type': 'application/json'
        }
      });
    },
    getSubItems() {
      axios({
        method: 'GET',
        url: `http://localhost:5000/accounts/1/lists/${this.listId}/todos/${this.item.id}/subitems`
      }).then(response => {
        this.subItems = response.data;
      });
    },
    addSubItem() {
      let data = JSON.stringify({ name: this.subItemForm.name });

      this.subItemForm.name = '';
      let addSubItemInput = document.getElementById("add-sub-item");
      addSubItemInput.focus();

      axios({
        method: 'POST',
        url: `http://localhost:5000/accounts/1/lists/${this.listId}/todos/${this.item.id}/subitems`,
        data,
        headers: {
          'content-type': 'application/json'
        }
      }).then(response => {
        this.subItems.unshift(response.data);
      });
    }
  },
  created: function() {
    this.getSubItems();
  },
  watch: {
    checkboxToggle: function() {
      this.toggleCompleted();
    }
  },
  computed: {
    checkboxToggle() {
      return this.item.completed;
    },
    hasSubItems() {
      return this.subItems.length > 0;
    },
    itemDueToday() {
      let today = new Date();
      let dueDate = new Date(this.item.dueDate);

      if(dueDate.getTime() >= today.getTime()) {
        return false;
      }

      return true;
    }
  },
  filters: {
    formatDate: function(value) : string {
      return moment(value).format('dddd, MMMM Do YYYY');
    },
    monthDay: function(value) : string {
      return moment(value).format('MMMM Do');
    }
  },
  components: {
    SubItem
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

  .todo-item-date {
    display: flex;
    align-items: center;
    font-size: 12px;
    margin-top: 4px;

    svg {
      margin-right: 3px;
    }
  }

  .todo-item-notes {
    line-height: 1;
    margin-top: 3px;
    max-width: 160px;
    overflow-wrap: break-word;

    @media screen and (min-width: 768px) {
      max-width: 320px;
    }
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