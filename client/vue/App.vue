<template lang="pug">
  
#content.mt-4
  b-navbar(toggleable="sm" type="light" variant="light").fixed-top
    b-navbar-brand Todo
    b-avatar(text="A").ml-auto

  router-view

</template>

<script lang="ts">

export default {
  name: 'App',
  data() {
    return {};
  },
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

</style>