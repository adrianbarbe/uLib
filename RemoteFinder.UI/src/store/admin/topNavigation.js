const state = {
    topNavigationItem: {
        name: '',
    },
};

const getters = {
    getTopNavigationTitle: state => {
        return state.topNavigationItem.name;
    },
};

const actions = {
};

const mutations = {
    setTopNavigationItem(state, navItem) {
        state.topNavigationItem = navItem;
    }
};


export default {
    state,
    getters,
    actions,
    mutations
}