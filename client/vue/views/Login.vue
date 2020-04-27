<template lang="pug">

b-container
  b-button(href="http://localhost:5000/accounts/login") Login
  b-button(@click="logout" v-if="isAuthenticated") Logout
  b-button(to="/lists") My Lists
      
</template>

<script lang="ts">

import store from '../../modules/store';
import axios from 'axios';

export default {
  name: 'Login',
  data() {
    return {
      isAuthenticated: store.isAuthenticated
    };
  },
  methods: {
    logout() {
      axios({
        method: 'GET',
        url: 'http://localhost:5000/accounts/logout'
      })
    }
  },
};

</script>