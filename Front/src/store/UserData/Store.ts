import IUserInfo from '@/interfaces/IUserInfo';
import { state } from '@/store/UserData/State';
import { Module } from 'vuex';
import { getters } from './Getters';

const namespaced: boolean = true;

export const UserDataStore: Module<IUserInfo, undefined> = {
    namespaced,
    state,
    getters
};
