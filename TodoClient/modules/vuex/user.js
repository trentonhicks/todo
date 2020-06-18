import axios from 'axios';

const user = {
    state: () => ({
        user: {},
        plan: {}
    }),
    mutations: {
        setUserData(state, data) {
            state.user = data;
        },
        setPlanData(state, data) {
            state.plan = data;
        }
    },
    actions: {

    },
    getters: {
        user(state) {
            return state.user;
        },
        plan(state) {
            return state.plan;
        }
    }
}

export default user;