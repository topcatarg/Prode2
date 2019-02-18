import Vuex, { StoreOptions } from 'vuex';

import Vue from 'vue';

Vue.use(Vuex);

const store: StoreOptions<undefined> = {};

export default new Vuex.Store<undefined>(store);
