<template lang="pug">

  b-list-group-item.sub-item
    b-icon-list.sub-item-handle
    .item-checkbox-wrapper
      b-form-checkbox(v-model="item.completed")
    .item-name-wrapper(@click="editing = true" v-if="!editing")
      div.item-name {{ item.name }}
    .item-editing(v-else)
      b-form-group
        b-form-input.item-name(v-focus="" @keydown.enter="editing = false; editSubItem();" placeholder="Name" maxlength="50" v-model="form.name")
      b-button(variant="info" size="sm" @click="editing = false; editSubItem();").mr-1 Update
      b-button(variant="danger" size="sm" @click="editing = false; deleteSubItem();").mr-1 Trash
      b-button(variant="secondary" size="sm" @click="editing = false") Cancel
      
</template>

<script lang="ts">

import axios from 'axios';
import moment from 'moment';

export default {
  name: 'SubItem',
  props: ['id', 'name', 'completed'],
  data() {
    return {
      editing: false,
      item: {
        id: this.id,
        name: this.name,
        completed: this.completed
      },
      form: {
        name: this.name
      }
    };
  },
  methods: {
    toggleCompleted() {
      this.$emit('sub-item-toggled', this.item);

      axios({
        method: 'PUT',
        url: `/api/subitems/${this.item.id}/completed`,
        headers: {
          'content-type': 'application/json'
        },
        data: this.item.completed
      });
    },
    editSubItem() {
      let data = JSON.stringify(this.form);

      axios({
        method: 'PUT',
        url: `/api/subitems/${this.item.id}`,
        data,
        headers: {
          'content-type': 'application/json'
        }
      });

    },
    refreshSubItem(subItem) {
      if(subItem.id === this.item.id) {
        this.item = subItem;
        this.form.name = subItem.name;
        this.$emit('sub-item-edited', subItem);
      }
    },
    deleteSubItem() {
      axios({
        method: 'DELETE',
        url: `/api/subitems/${this.item.id}`,
      });

      this.$emit('sub-item-deleted', this.item);
    }
  },
  mounted() {
    this.$store.state.connection.on("SubItemUpdated", (subItem) => this.refreshSubItem(subItem));
    this.$store.state.connection.on("SubItemCompletedStateChanged", (subItem) => this.refreshSubItem(subItem));
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
  directives: {
    focus: {
      inserted (el) {
        el.focus();
      }
    }
  }
};

</script>

<style lang="scss" scoped>

  .sub-item {
    display: flex;
    padding: 0 0 0 10px;
    align-items: center;

    &:hover {
      background-color: #f8f9fa;

      .sub-item-handle {
        opacity: 1;
      }
    }

    &:active {
      background-color: darken(#f8f9fa, 1.5);
    }

    .item-name-wrapper {
      display: flex;
      align-items: center;
      flex: 1 0 auto;
      align-self: stretch;

      &:hover {
        cursor: pointer;
      }
    }

    .item-checkbox-wrapper {
      padding: 12px 0 12px 10px;
    }

    .item-editing {
      width: 100%;
      padding: 12px;
    }

    .sub-item-handle {
      opacity: 0.3;
      transition: opacity 0.3s ease;

      &:hover {
        cursor: move;
      }
    }
  }

</style>