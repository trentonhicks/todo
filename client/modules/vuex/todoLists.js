import Vue from 'vue';
import axios from 'axios';

const todoLists = {
    state: () => ({
        todoLists: [],
        contributors: []
    }),
    mutations: {
        updateTodoLists(state, data) {
            state.todoLists = data.todoLists;
            state.contributors = data.contributors;
        },
        setTodoListCompletedState(state, { listId, listCompletedState }) {
            let index = state.todoLists.findIndex(x => x.id === listId);
            let updatedList = state.todoLists[index];
            updatedList.completed = listCompletedState;

            Vue.set(state.todoLists, index, updatedList);
        },
    },
    actions: {
        loadTodoLists(context) {
            axios({
                method: 'GET',
                url: 'api/lists'
            })
            .then((response) => {
                context.commit('updateTodoLists', response.data);
            });
        },
        addTodoList(context, payload) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'POST',
                    url: 'api/lists',
                    data: JSON.stringify(payload),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .then(() => {
                    context.dispatch('loadTodoLists');
                })
                .finally(() => {
                    resolve();
                });
            });
        },
        deleteTodoList(context, payload) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'DELETE',
                    url: `api/lists/${payload.listId}`,
                })
                .then(() => {
                    context.dispatch('loadTodoLists');
                })
                .finally(() => {
                    resolve();
                });
            });
        },
        inviteContributorToList(context, { listId, email }) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'POST',
                    url: `api/lists/${listId}/email`,
                    data: JSON.stringify({ email }),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .finally(() => {
                    resolve();
                });
            });
        }
    },
    getters: {
        todoLists(state) {
            return state.todoLists;
        },
        contributors(state) {
            return state.contributors;
        },
        getTodoListById: (state) => (todoListId) => {
            return state.todoLists.find(list => list.id === todoListId);
        },
    }
}

export default todoLists;