import Vue from 'vue';
import Vuex from 'vuex';
import * as signalR from "@microsoft/signalr";
import todoLists from './vuex/todoLists';
import user from './vuex/user';
import todoListItems from './vuex/todoListItems';

Vue.use(Vuex);

const store = new Vuex.Store({
  modules: {
    user,
    todoLists,
    todoListItems
  },
  state: {
    connection: new signalR.HubConnectionBuilder().withUrl("/notifications").build(),
  },
});

export default store;
