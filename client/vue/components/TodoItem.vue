<template lang="pug">

b-list-group-item.todo-item(button :click="showItemDetails()")
    | {{ name }}

</template>

<script lang="ts">

export default {
  name: 'TodoItem',
  props: ['name'],
  data() {
    return {};
  },
  methods: {
    showItemDetails() {

    }
  },
};

</script>

<style lang="scss" scoped>

.todo-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  outline: none !important;
}

</style>