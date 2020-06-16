<template>

    <b-navbar toggleable="sm" type="light" variant="light" class="fixed-top">
      <b-navbar-brand>Todo</b-navbar-brand>
      
      <div class="user-info ml-auto" v-if="user.id">
        <b-button class="ml-auto mr-2" @click="logout">Logout</b-button>
        <b-avatar :src="user.pictureUrl"></b-avatar>
      </div>
    </b-navbar>

</template>

<script>

    import axios from 'axios';

    export default {
        name: 'Header',
        created: function() {
            this.checkAuthState();
        },
        computed: {
            user() {
                return this.$store.getters.user;
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
                });
            }
        },
    }
</script>