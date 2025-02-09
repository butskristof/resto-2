import { createApp } from 'vue';
import { createPinia } from 'pinia';
import { VueQueryPlugin } from '@tanstack/vue-query';
import VueTippy from 'vue-tippy';
import Toast from 'vue-toastification';

import App from './App.vue';
import router from './router';

import 'vue-multiselect/dist/vue-multiselect.css';
import 'tippy.js/dist/tippy.css';
import 'vue-toastification/dist/index.css';
import 'lucide-static/font/lucide.css';
import 'normalize.css/normalize.css';
import './styles/global.scss';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(VueQueryPlugin);
app.use(VueTippy);
app.use(Toast);

app.mount('#app');
