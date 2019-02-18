import axios from 'axios';
import Vue from 'vue';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';
import store from './store/mainStore';

const instance = axios.create({
  baseURL: process.env.VUE_APP_BASE_URI,
  withCredentials: true
});

Vue.config.productionTip = false;
Vue.prototype.$http = instance;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
