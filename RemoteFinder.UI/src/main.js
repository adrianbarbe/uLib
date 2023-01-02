import {createApp} from 'vue';
import 'material-design-icons-iconfont/dist/material-design-icons.css';
import router from './router';
import store from './store';

import './styles/main.scss';

import ConfigFactory from "./config/configFactory";
import config from "./app.config";

import App from './App.vue';
import vuetify from './plugins/vuetify';
import {loadFonts} from './plugins/webfontloader';

loadFonts()

createApp(App)
    .use(ConfigFactory, config)
    .use(vuetify)
    .use(router)
    .use(store)
    .mount('#app');
