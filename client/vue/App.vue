<template lang="pug">
  
#content.mt-4
  b-navbar(toggleable="sm" type="light" variant="light").fixed-top
    b-navbar-brand Todo
    b-button(v-if="isAuthenticated" @click="logout").ml-auto.mr-2 Logout
    b-avatar(v-if="isAuthenticated" :src="$store.state.user.pictureUrl" @click="")

  router-view

</template>

<script lang="ts">

import axios from 'axios';

export default {
  name: 'App',
  data() {
    return {
      isAuthenticated: false
    }
  },
  methods: {
    checkAuthState() {
      axios({
        method: 'GET',
        url: 'api/accounts/login'
      }).then((response) => {

        axios({
          method: 'GET',
          url: 'api/accounts'
        }).then((user) => {
          this.$store.commit('setUserData', user.data);
        });

        this.isAuthenticated = true;

      }).catch(() => {
        if(this.$router.name !== 'Login') {

          this.$router.push('/login');
        }
      });
    },
    logout() {
      axios({
        method: 'GET',
        url: 'api/accounts/logout'
      }).then(() => {
        this.$router.push('/login');
        this.isAuthenticated = false;
      });
    }
  },
  created: function() {
    this.checkAuthState();
  }
};

</script>

<style lang="scss">

* {
  box-sizing: border-box;
}

#content {
  padding: 75px 20px;
  height: 100vh;

  h1,
  h2,
  h3,
  h4 {
    font-family: 'Nunito', sans-serif;
    font-weight: bold;
  }
}

h1 {
  font-size: 40px;
}

.list-title {
  font-size: 40px;
  font-family: 'Nunito', sans-serif;
  font-weight: bold;
  color: #212529;
  display: block;
  border: none;
  width: 100%;

  .form-control {
    @extend .list-title;
    padding: 0;
    height: auto;
    padding: 0 10px;
    background: #f8f9fa !important;
  }
}

.btn {
  font-family: 'Nunito', sans-serif;
  font-weight: bold;
}

.modal-hide-footer .modal-footer {
  display: none;
}

.item-0 {
    .todo-item.list-group-item {
      border-bottom-left-radius: 0;
      border-bottom-right-radius: 0;
    }
}

.todo-item-wrapper:not(.item-0) {
  .todo-item.list-group-item {
      border-radius: 0;
      border-top: none;
    }
}

.todo-item-wrapper:not(.item-0):last-child .todo-item {
  border-bottom-left-radius: .25rem;
  border-bottom-right-radius: .25rem;
}

.navbar.fixed-top {
  z-index: 400;
}

</style>