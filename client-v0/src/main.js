import { createApp } from 'vue';
import { createPinia } from 'pinia';
import VueTippy from 'vue-tippy';

import App from './App.vue';
import router from './router';

import 'tippy.js/dist/tippy.css'; // optional for styling
import './styles/global.scss';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(VueTippy);

app.mount('#app');
