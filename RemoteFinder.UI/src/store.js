import { createStore } from 'vuex';

import snackMessages from "./store/admin/snackMessages";
import topNavigation from "./store/admin/topNavigation";
import loadingState from "./store/loadingState";

export default createStore({
    modules: {
        snackMessages,
        loadingState,
        topNavigation,
    },
    state: {},
    mutations: {},
    actions: {},
});
