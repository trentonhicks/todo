<template lang="pug">

b-container
  h1.mb-3 My Lists

  ul
    li(v-for="item in todoLists" :key="item.id")
      router-link(:to="'/lists/' + item.id") {{ item.listTitle }}
      ul
        li(v-for="subitem in item.todoItemPreview") {{ subitem.toDoName }}

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
      url: 'http://localhost:5000/accounts/4/lists',
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
