import Vue from 'vue';
import axios from 'axios';

const todoLists = {
    state: () => ({
        items: {}
    }),
    mutations: {
        setItems(state, { listId, items }) {
            state.items[listId] = items;
        },
        addItem(state, { listId, item }) {
            state.items[listId].unshift(item);
        },
        updateItemCompletedState(state, { item }) {
            let index = state.items[item.listId].findIndex(i => i.id === item.id);
            state.items[item.listId][index].completed = item.completed;
        },
        updateItem(state, { item }) {
            let index = state.items[item.listId].findIndex(i => i.id === item.id);
            Vue.set(state.items[item.listId], index, item);
        }
    },
    actions: {
        async loadItemsByListId(context, payload) {
            const response = await axios({
                method: 'GET',
                url: `api/lists/${payload.todoListId}/todos`
            });

            context.commit('setItems', { listId: payload.todoListId, items: response.data });
        },
        addItem(context, payload) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'POST',
                    url: `api/lists/${payload.listId}/todos`,
                    data: JSON.stringify(payload),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .then((response) => {

                })
                .finally(() => {
                    resolve();
                });
            });
        },
        toggleItemCompletedState(context, { listId, itemId, completed }) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'PUT',
                    url: `api/lists/${listId}/todos/${itemId}/completed`,
                    data: JSON.stringify({ completed }),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .finally(() => {
                    resolve();
                });
            });
        },
        updateItem(context, { item }) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'PUT',
                    url: `api/lists/${item.listId}/todos/${item.id}`,
                    data: JSON.stringify({
                        name: item.name,
                        notes: item.notes,
                        dueDate: item.dueDate
                    }),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .finally(() => {
                    resolve();
                });
            });
        },
        deleteItem(context, { item }) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'DELETE',
                    url: `api/lists/${item.listId}/todos/${item.id}`
                })
                .finally(() => {
                    resolve();
                });
            });
        }
    },
    getters: {
        getItemsByListId: (state) => (listId) => {
            return state.items[listId];
        },
        getItemName: (state) => (listId, itemId) => {
            return state.items[listId].find(i => i.id === itemId).name;
        },
        getItemCompletedState: (state) => (listId, itemId) => {
            return state.items[listId].find(i => i.id === itemId).completed;
        }
    }
}

export default todoLists;