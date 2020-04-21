<template lang="pug">

  b-list-group-item.sub-item
    .item-checkbox-wrapper
      b-form-checkbox(v-model="item.completed")
    .item-name-wrapper(@click="editing = true")
      div.item-name(v-if="!editing") {{ item.name }}
      b-form-input.item-name(v-else v-focus="" @keydown.enter="editing = false" placeholder="Name" v-model="item.name")
    .item-options(v-if="editing")
      b-button-group
        b-button(variant="info" @click="editing = false") Update
        b-button(variant="danger" @click="editing = false") Delete
      
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
      }
    };
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

  .sub-item {
    display: flex;
    padding: 0;
    align-items: center;

    &:hover {
      background-color: #f8f9fa;
    }

    &:active {
      background-color: darken(#f8f9fa, 1.5);
    }

    .item-name-wrapper {
      flex: 1 0 auto;
      align-self: stretch;
      display: flex;
      align-items: center;

      &:hover {
        cursor: pointer;
      }
    }

    .item-checkbox-wrapper {
      padding: 12px 0 12px 20px;
    }
  }

</style>