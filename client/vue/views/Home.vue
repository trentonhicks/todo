<template lang="pug">

b-container
  h1.mb-3 My Lists
  all-todo-lists
      
</template>

<script lang="ts">

import AllTodoLists from '../components/AllTodoLists.vue';

export default {
  data() {
    return {};
  },
  components: {
    AllTodoLists
  }
};

</script>