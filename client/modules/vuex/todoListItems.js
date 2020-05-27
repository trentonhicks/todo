import axios from 'axios';

const todoLists = {
    state: () => ({
        items: {}
    }),
    mutations: {
        updateItems(state, payload) {
            state.items[payload[0].listId] = payload;
        },
        addItem(state, { listId, item }) {
            state.items[listId].unshift(item);
        },
        updateItemCompletedState(state, { item }) {
            for(var i = 0; i < state.items[item.listId].length; i++) {
                if(state.items[item.listId][i].id === item.id) {
                    state.items[item.listId][i].completed = item.completed;
                    break;
                }
            }
        },
    },
    actions: {
        loadItemsByListId(context, payload) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'GET',
                    url: `api/lists/${payload.todoListId}/todos`
                })
                .then((response) => {
                    context.commit('updateItems', response.data);
                })
                .finally(() => {
                    resolve();
                });
            });
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
        toggleItemCompletedState(context, { id, completed }) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'PUT',
                    url: `api/todos/${id}/completed`,
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
    },
    getters: {
        getItemsByListId: (state) => (listId) => {
            return state.items[listId];
        },
    }
}

export default todoLists;