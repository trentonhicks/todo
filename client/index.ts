import Vue from 'vue';
import Vuex from 'vuex';
import Router from './modules/router';
import App from './vue/App.vue';
import './modules/bootstrap';

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    user: {
        id: '',
        pictureUrl: '',
        fullName: '',
        email: '',
    }
  },
  mutations: {
    setUserData(state, data) {
      state.user = data;
    }
  }
});

new Vue({
  el: '#app',
  store,
  router: Router,
  render: h => h(App)
});
