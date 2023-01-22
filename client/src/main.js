import { createApp } from 'vue';
import { createPinia } from 'pinia';
import { VueQueryPlugin } from '@tanstack/vue-query';
import { vfmPlugin } from 'vue-final-modal';

import App from './App.vue';
import router from './router';

import './styles/global.scss';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(VueQueryPlugin);
app.use(vfmPlugin);

app.mount('#app');
