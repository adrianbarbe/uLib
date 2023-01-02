const state = {
    show: false,
    message: '',
    color: 'info',
};

const getters = {
    show(state) {
        return state.show;
    },
    message(state) {
        return state.message;
    },
    color(state) {
        return state.color;
    }
};

const mutations = {
    set(state, data) {
        state.show = true;
        state.message = data.message;
        if (data.color !== undefined) {
            state.color = data.color;
        }
    },
    update(state, data) {
        state.show = !!data.show;
        state.color = 'info';
    }
};


export default {
    namespaced: true,
    state,
    getters,
    mutations
}