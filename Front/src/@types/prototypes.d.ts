import {AxiosStatic} from 'axios';

declare module 'vue/types/vue' {
  export interface Vue   {
    $http: AxiosStatic;
  }
}