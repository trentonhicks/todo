import Vue from 'vue';
import VueRouter from 'vue-router';
import BootstrapVue from "bootstrap-vue";
import App from './vue/App.vue';
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

// This imports all the layout components such as <b-container>, <b-row>, <b-col>:
import { LayoutPlugin } from 'bootstrap-vue';
Vue.use(LayoutPlugin);

// This imports <b-modal> as well as the v-b-modal directive as a plugin:
import { ModalPlugin } from 'bootstrap-vue';
Vue.use(ModalPlugin);

// This imports <b-card> along with all the <b-card-*> sub-components as a plugin:
import { CardPlugin } from 'bootstrap-vue';
Vue.use(CardPlugin);

Vue.use(BootstrapVue);
Vue.use(VueRouter);

const router = new VueRouter({
  routes: [
    // dynamic segments start with a colon
    { path: '/', component: App }
  ]
});

new Vue({
  el: '#app',
  router,
  render: h => h(App),
});