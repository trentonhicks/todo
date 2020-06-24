import Vue from 'vue';
import axios from 'axios';

const todoLists = {
    state: () => ({
        todoLists: [],
        contributors: [],
        loading: false
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
        updateListTitle(state, { listId, listTitle }) {
            const index = state.todoLists.findIndex(x => x.id === listId);
            state.todoLists[index].listTitle = listTitle;
        },
        updateLoadingState(state, { loadingState }) {
            state.loading = loadingState;
        },
        changeUserRoleByListId(state, { listId, role }) {
            const index = state.todoLists.findIndex(x => x.id === listId);
            state.todoLists[index].role = role;
        }
    },
    actions: {
        async loadTodoLists(context) {
            context.commit('updateLoadingState', { loadingState: true });

            const response = await
                axios({
                    method: 'GET',
                    url: 'api/lists'
                });

            context.commit('updateTodoLists', response.data);
            context.commit('updateLoadingState', { loadingState: false });
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
        },
        async updateListTitle(context, { listId, listTitle }) {
            await axios({
                method: 'PUT',
                url: `api/lists/${listId}`,
                data: JSON.stringify({ listTitle }),
                headers: {
                    'content-type': 'application/json',
                }
            });
        },
        async acceptInvitation(context, { listId }) {
            try {
                await axios({
                    method: 'POST',
                    url: `api/lists/${listId}/accept`
                });

                context.commit('changeUserRoleByListId', { listId, role: 2 });
            }
            catch (error) {
                console.log(error);
            }
        },
        async declineInvitation(context, { listId }) {
            try {
                await axios({
                    method: 'POST',
                    url: `api/lists/${listId}/decline`
                });

                context.commit('changeUserRoleByListId', { listId, role: 1 });
            }
            catch (error) {
                console.log(error);
            }
        },
        async leaveTodoList(context, { listId }) {
            try {
                await axios({
                    method: 'POST',
                    url: `api/lists/${listId}/removeself`
                });

                context.commit('changeUserRoleByListId', { listId, role: 4 });
            }
            catch (error) {
                console.log(error);
            }
        },
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
        getTodoListTitle: (state) => (todoListId) => {
            let title = state.todoLists.find(list => list.id === todoListId).listTitle;
            return title;
        },
        getLoadingState(state) {
            return state.loading;
        }
    }
}

export default todoLists;