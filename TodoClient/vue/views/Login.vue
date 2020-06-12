<template lang="pug">

b-container
  b-button(href="/api/accounts/login") Login
      
</template>

<script>

import axios from 'axios';

export default {
  name: 'Login',
  data() {
    return {};
  },
  methods: {
    logout() {
      axios({
        method: 'GET',
        url: '/api/accounts/logout'
      })
    }
  },
};

</script>