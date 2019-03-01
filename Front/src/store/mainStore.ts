import { UserDataStore } from '@/store/UserData/Store';
import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';

Vue.use(Vuex);

const store: StoreOptions<undefined> = {
    modules: {
        UserData: UserDataStore
    }
};

export default new Vuex.Store<undefined>(store);
