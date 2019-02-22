import axios from 'axios';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'bootstrap/dist/css/bootstrap.css';
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
Vue.use(BootstrapVue);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
