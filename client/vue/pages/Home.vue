<template lang="pug">

b-container
  h1.mb-3 My Lists

  .todo-lists
    b-card(v-for="item in todoLists" :key="item.id" class="mb-3 bg-light")
      b-button(:to="'/lists/'+ item.id").card-title {{item.listTitle}}
      .todo-list-options
        b-button(variant="info" size="sm" :to="'/lists/'+ item.id").mr-2: b-icon-list-ul
        b-button(variant="danger" size="sm"): b-icon-trash

    b-button(@click="") Add list
      
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
  }
};

</script>

<style lang="scss" scoped>

.card {
  
  .card-body {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 0px;
  }

  .card-title {
    font-size: 20px;
    margin-bottom: 0px;
    width: 100%;
    text-align: left;
    background: transparent;
    color: #212529;
    border: none;
    font-weight: bold;
    padding: 20px;

    &:focus, &:hover, &:not(:disabled):not(.disabled):active {
      background-color: transparent;
      border: none;
      color: #212529;
    }
  }

  .todo-list-options {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    padding: 20px;

    .btn {
      height: 36px;
      width: 36px;
      border-radius: 100px;
      display: flex; 
      align-items: center;
      justify-content: center;
    }

  }

}

</style>