<template lang="pug">

b-container
  b-button(class="back-button" variant="link" to="/")
    b-icon(icon="chevron-left")
    | My Lists

  todo-list(:id="$route.params.id")

</template>

<script lang="ts">

import TodoList from '../components/TodoList.vue'

export default {
  name: 'TodoListDetails',
  data() {
    return {

    };
  },
  components: {
    TodoList
  }
};

</script>

<style lang="scss" scoped>

.back-button {
  display: flex;
  align-items: center;
  padding: 0px;
  position: relative;
  left: -7px;
  margin-bottom: 10px;
}

</style>