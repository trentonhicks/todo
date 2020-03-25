<template lang="pug">

b-container
  h1.mb-3 My Lists

  ul
    li(v-for="todoList in todoLists" :key="todoList.id") {{ todoList.listTitle }}

</template>

<script lang="ts">

import axios from 'axios';

export default {
  name: 'Home',
  data() {
    return {
      todoLists: []
    };
  },
  created: function() {
    axios.get('http://localhost:5000/accounts/1/lists')
      .then((response) => {
        this.todoLists = response;
        console.log(response);
      })
      .catch((e) => {
        console.log(e);
      });
  }
};

</script>
