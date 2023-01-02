const state = {
    loading: 0,
};

const getters = {
    loadingState(state) {
        return state.loading;
    },
};

const mutations = {
    setLoading(state) {
        state.loading = state.loading + 1;
    },
    setLoaded(state) {
        state.loading = state.loading - 1;
    }
};


export default {
    namespaced: true,
    state,
    getters,
    mutations
}