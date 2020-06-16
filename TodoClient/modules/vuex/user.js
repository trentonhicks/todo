import axios from 'axios';

const user = {
    state: () => ({
        user: {}
    }),
    mutations: {
        setUserData(state, data) {
            state.user = data;
        },
    },
    actions: {

    },
    getters: {
        user(state) {
            return state.user;
        }
    }
}

export default user;