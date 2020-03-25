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

<style lang="scss" scoped>

* {
  box-sizing: border-box;
}

#content {
  padding: 50px 20px;
  height: 100vh;
}

</style>