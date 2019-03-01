import IUserInfo from '@/interfaces/IUserInfo';
import { GetterTree } from 'vuex';
import { state } from './State';

export const getters: GetterTree<IUserInfo, undefined> = {
    GetState(): IUserInfo {
        return state;
    },
    LogedIn(): boolean {
        return state.loged;
    },
    IsAdmin(): boolean {
        return state.isAdmin;
    }
};
