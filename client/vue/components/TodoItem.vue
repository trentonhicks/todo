<template lang="pug">

.todo-item-wrapper(:data-id="item.id")

  //- Todo List Item
  b-list-group-item.todo-item.bg-light

    .todo-item-content
      b-icon-list.todo-item-handle(font-scale="1.2")

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
    b-form(v-on:submit.prevent="editTodoItem" id="edit-item-form")

        //- Name
        b-form-group(label="Name")
          b-form-input(
            class="form-input-focus"
            type="text"
            placeholder="Name"
            v-model="form.name"
            maxlength="50"
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
          draggable(v-model="subItems" handle=".sub-item-handle")

            //- Sub-item Component
            sub-item(
              v-for="item in subItems"
              :key="item.id"
              :id="item.id"
              :name="item.name"
              :completed="item.completed"
              @sub-item-edited="refreshSubItems"
              @sub-item-toggled="refreshSubItems"
              @sub-item-deleted="removeSubItemFromList")

          //- Add sub-item
          b-button(variant="secondary" class="btn-block mt-2" @click="addingSubItem = true" v-if="!addingSubItem") Add sub-item

          .adding-sub-item(v-if="addingSubItem").mt-2
            b-form-group
              b-form-input(
                :class="{'is-invalid': subitemFormLengthExceeded}"
                id="add-sub-item"
                v-model="subItemForm.name" @keydown.enter.prevent="addSubItem()" placeholder="Add sub-item" v-focus)
              .invalid-feedback(v-if="subitemFormLengthExceeded") Name must be less than 50 characters.
              b-button(variant="success" @click="addSubItem()").mt-2 Add
              b-button(variant="secondary" @click="addingSubItem = false; subItemForm.name = ''").mt-2.ml-2 Cancel

        b-form-group.mb-0.text-right
          b-button(type="submit" variant="primary" class="mr-2") Save Changes
          b-button(variant="secondary" @click="$bvModal.hide('modal-edit-' + item.id)") Cancel

</template>

<script lang="ts">

import axios from 'axios';
import moment from 'moment';
import Draggable from 'vuedraggable';
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
    refreshItemCompletedState(item) {
      if(item.id == this.item.id)
        this.item.completed = item.completed;
    }
    toggleCompleted() {
      if(this.subItems < 1) {
        axios({
          method: 'PUT',
          url: `/api/todos/${this.item.id}/completed`,
          data: JSON.stringify({ completed: this.item.completed }),
          headers: {
            'content-type': 'application/json'
          }
        })
      }
      this.$emit('toggled-list-item', this.item);
    },
    editTodoItem() {
      this.$bvModal.hide('modal-edit-' + this.item.id);
      this.form.completed = this.item.completed;
      let data = JSON.stringify(this.form);
      this.item = JSON.parse(data);

      axios({
        method: 'PUT',
        url: `/api/todos/${this.item.id}`,
        data,
        headers: {
          'content-type': 'application/json'
        }
      });
    },
    getSubItems() {
      axios({
        method: 'GET',
        url: `/api/lists/${this.listId}/todos/${this.item.id}/subitems`
      }).then(response => {
        this.subItems = response.data;
      });
    },
    addSubItem() {
      if(this.subitemFormValid) {
        let data = JSON.stringify({ name: this.subItemForm.name });

        this.subItemForm.name = '';
        let addSubItemInput = document.getElementById("add-sub-item");
        addSubItemInput.focus();

        axios({
          method: 'POST',
          url: `/api/lists/${this.listId}/todos/${this.item.id}/subitems`,
          data,
          headers: {
            'content-type': 'application/json'
          }
        }).then(response => {
          this.subItems.unshift(response.data);
        });

      }
    },
    refreshSubItems(item) {
      let index = this.subItems.findIndex(({id}) => id === item.id);
      this.$set(this.subItems, index, item);
    },
    removeSubItemFromList(item) {
      let index = this.subItems.findIndex(({id}) => id === item.id);
      this.subItems.splice(index, 1);
    }
  },
  created: function() {
    this.getSubItems();
  },
  mounted() {
    this.$root.$on('bv::modal::shown', (bvEvent, modalId) => {
      let formInput = document.querySelector(`#${modalId} .form-input-focus`);
      formInput.focus();
    });
    this.$store.state.connection.on("ItemCompleted", (item) => this.refreshItemCompletedState(item));
  },
  watch: {
    checkboxToggle: function() {
      this.toggleCompleted();
    },
    allSubItemsCompleted: function() {
      if(this.allSubItemsCompleted) {
        this.$set(this.item, 'completed', true);
      }
      else {
        this.$set(this.item, 'completed', false);
      }
    }
  },
  computed: {
    subitemFormIsEmpty(){
        return this.subItemForm.name.length == 0
    },
    subitemFormLengthExceeded(){
      return this.subItemForm.name.length > 50 
    },
    subitemFormValid(){
      return !this.subitemFormIsEmpty && !this.subitemFormLengthExceeded
    },
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
    },
    allSubItemsCompleted() {
      return this.subItems.every(item => item.completed) && this.subItems.length > 0
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
    SubItem,
    Draggable
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
  align-items: flex-start;
  padding: 14px 12px 14px 12px;
  font-family: 'Nunito', sans-serif;
  flex-direction: column;

  &:hover {
    .todo-item-handle {
      opacity: 1;
    }
  }

  @media screen and (min-width: 768px) {
    flex-direction: row;
    align-items: center;
  }

  .todo-item-content {
    display: flex;
    align-items: center;
  }

  .todo-item-handle {
    margin: 3px 12px 0 0;
    opacity: 0.3;
    transition: opacity 0.3s ease;
    align-self: flex-start;

    &:hover {
      cursor: move;
    }
  }

  .todo-item-name {
    margin-top: 2px;
    font-weight: bold;
    line-height: 1.2;
    max-width: 400px;
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
    max-width: 400px;
    overflow-wrap: break-word;

    @media screen and (min-width: 768px) {
      max-width: 400px;
    }
  }

  .todo-item-options {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    margin-left: 24px;
    margin-top: 12px;

    @media screen and (min-width: 768px) {
      margin-top: 0px;
    }

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