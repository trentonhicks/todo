import Vue from 'vue';
import Vuex from 'vuex';
import * as signalR from "@microsoft/signalr";

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    user: {
        id: '',
        pictureUrl: '',
        fullName: '',
        email: '',
    },
    connection: new signalR.HubConnectionBuilder().withUrl("/notifications").build()
  },
  mutations: {
    setUserData(state, data) {
      state.user = data;
    },
  }
});

export default store;
