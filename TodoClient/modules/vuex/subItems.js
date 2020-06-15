import Vue from 'vue';
import axios from 'axios';

const subItems = {
    state: () => ({
        subItems: {}
    }),
    mutations: {
        setSubItems() {

        },
        addSubItem() {

        },
        updateSubItem() {

        },
        removeSubItem() {

        }
    },
    actions: {
        loadSubItems() {

        },
        async addSubItem(context, { listId, todoItemId, name }) {
            try {
                const response = await axios({
                    method: 'POST',
                    url: `api/lists/${listId}/todos/${todoItemId}/subitems`,
                    headers: { 'content-type': 'application/json' },
                    data: JSON.stringify({ name })
                });
    
                return response.data;
            }
            catch(error) {
                console.log(error);
            }
        },
        updateSubItem() {

        },
        trashSubItem() {

        }
    },
    getters: {
        getSubItemsByItemId: (state) => (itemId) => {
            return state.subItems[itemId];
        },
        getSubItemCompletedState: (state) => (itemId, subItemId) => {
            return state.subItems[itemId].find(i => i.id === subItemId).completed;
        }
    }
}

export default subItems;