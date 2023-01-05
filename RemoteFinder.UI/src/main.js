import {createApp} from 'vue';
import 'material-design-icons-iconfont/dist/material-design-icons.css';
import routerInstance from './router';
import store from './store';
import AuthJwt from './plugins/auth-jwt';

import './styles/main.scss';

import ConfigFactory from "./config/configFactory";
import config from "./app.config";

import App from './App.vue';
import vuetify from './plugins/vuetify';
import {loadFonts} from './plugins/webfontloader';

loadFonts()

const app = createApp(App);
const router = routerInstance(app);

app.use(ConfigFactory, config)
    .use(AuthJwt)
    .use(vuetify)
    .use(router)
    .use(store)
    .mount('#app');
