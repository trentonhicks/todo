<template lang="pug">

b-container
  b-button(href="http://localhost:5000/api/accounts/login") Login
      
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
        url: 'http://localhost:5000/api/accounts/logout'
      })
    }
  },
};

</script>