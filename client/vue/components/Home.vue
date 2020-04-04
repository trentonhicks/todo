<template lang="pug">

b-container
  h1.mb-4 My Lists

  .todo-lists
    b-card(v-for="item in todoLists" :key="item.id" :title="item.listTitle" class="mb-3")
      b-button(variant="primary" size="sm" :to="'/lists/' + item.id").mr-2 View
      b-button(variant="danger" size="sm") Delete
      
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
    axios({
      method: 'get',
      url: 'http://localhost:5000/accounts/1/lists',
    })
    .then((response) => {
      console.log(response);
      this.todoLists = response.data
    }).catch((e) => {
      console.log(e);
    });
  }
};

</script>