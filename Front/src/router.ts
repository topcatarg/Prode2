import Vue from 'vue';
import Router from 'vue-router';
import Schedule from './views/Schedule.vue';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    },
    {
      path : '/',
      name: 'Schedule',
      component: Schedule
    },
    {
      path : '/Posiciones',
      name: 'Posiciones',
      component: () => import(/* webpackChunkName: "Positions" */ './views/Positions.vue')
    },
    {
      path: '/Forecast',
      name: 'Pronosticos',
      component: () => import(/* webpackChunkName: "Forecast" */ './views/Forecast.vue')
    },
    {
      path: '/Rules',
      name: 'Reglas',
      component: () => import(/* webpackChunkName: "Forecast" */ './views/Rules.vue')
    },
    {
      path: '/Profile',
      name: 'UserData',
      component: () => import(/* webpackChunkName: "UserData" */ './views/UserData.vue')
    },
    {
      path: '/Login',
      name: 'Logueo',
      component: () => import(/* webpackChunkName: "Logn" */ './views/Login.vue')
    },
    {
      path: '/Logout',
      name: 'Logout',
      component: () => import(/* webpackChunkName: "Logn" */ './views/Logout.vue')
    },
    {
      path: '/Admin',
      name: 'Administrar',
      component: () => import(/* webpackChunkName: "Logn" */ './views/Admin.vue')
    },
    {
      path: '/recoverpass',
      name: 'RecoverMail',
      component: () => import(/* webpackChunkName: "Logn" */ './views/RecoverMail.vue')
    },
    {
      path: '/Privacy',
      name: 'privacy',
      component: () => import(/* webpackChunkName: "Logn" */ './views/Privacy.vue')
    }
  ]
});
