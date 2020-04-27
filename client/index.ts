import Vue from 'vue';
import Router from './modules/router';
import App from './vue/App.vue';
import './modules/bootstrap';

new Vue({
  el: '#app',
  router: Router,
  render: h => h(App)
});
