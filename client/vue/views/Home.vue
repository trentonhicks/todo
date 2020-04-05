<template lang="pug">

b-container
  h1.mb-3 My Lists

  todo-list-summary(
    v-for="item in todoLists"
    :key="item.id"
    :id="item.id"
    :accountId="item.accountId"
    :listTitle="item.listTitle"
    )

  b-button(@click="" id="add-list-btn") Add list
      
</template>

<script lang="ts">

import axios from 'axios';
import TodoListSummary from '../components/TodoListSummary.vue';

export default {
  name: 'Home',
  data() {
    return {
      todoLists: []
    };
  },
  methods: {
    getTodoLists() {
      axios({
        method: 'get',
        url: 'http://localhost:5000/accounts/1/lists',
      })
      .then((response) => {
        this.todoLists = response.data
      }).catch((e) => {
        console.log(e);
      });
    }
  },
  created: function() {
    this.getTodoLists();
  },
  components: {
    TodoListSummary
  }
};

</script>