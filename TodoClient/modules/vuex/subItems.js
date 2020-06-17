import Vue from 'vue';
import axios from 'axios';

const subItems = {
    state: () => ({
        subItems: {}
    }),
    mutations: {
        setSubItems(state, { todoItemId, subItems }) {
            state.subItems[todoItemId] = subItems;
        },
        addSubItem(state, { subItem }) {
            state.subItems[subItem.listItemId].unshift(subItem);
        },
        updateSubItem() {

        },
        removeSubItem() {

        },
        updateSubItemCompletedState(state, { subItem }) {
            const index = state.subItems[subItem.listItemId].findIndex(i => i.id == subItem.id);
            state.subItems[subItem.listItemId][index].completed = subItem.completed;
        }
    },
    actions: {
        async loadSubItems(context, { listId, todoItemId }) {
            try {
                const response = await axios({
                    method: 'GET',
                    url: `api/lists/${listId}/todos/${todoItemId}/subitems`
                });

                context.commit('setSubItems', { todoItemId: todoItemId, subItems: response.data });
            }
            catch(error) {
                console.log(error);
            }
        },
        async addSubItem(context, { listId, todoItemId, name }) {
            try {
                await axios({
                    method: 'POST',
                    url: `api/lists/${listId}/todos/${todoItemId}/subitems`,
                    headers: { 'content-type': 'application/json' },
                    data: JSON.stringify({ name })
                });
            }
            catch(error) {
                console.log(error);
            }
        },
        updateSubItem() {

        },
        trashSubItem() {

        },
        async toggleSubItemCompletedState(context, { listId, todoItemId, subItemId, completed }) {
            try {
                await axios({
                    method: 'PUT',
                    url: `api/lists/${listId}/todos/${todoItemId}/subitems/${subItemId}/completed`,
                    headers: {
                        'content-type': 'application/json'
                    },
                    data: completed
                });
            }
            catch(error) {
                console.log(error);
            }
        }
    },
    getters: {
        getSubItemsByItemId: (state) => (itemId) => {
            return state.subItems[itemId];
        },
        getSubItemCompletedState: (state) => (itemId, subItemId) => {
            return state.subItems[itemId].find(i => i.id === subItemId).completed;
        },
        todoItemHasSubItems: (state) => (itemId) => {
            return state.subItems[itemId].length > 0;
        }
    }
}

export default subItems;