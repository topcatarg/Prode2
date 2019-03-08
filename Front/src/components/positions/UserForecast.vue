<template>
    <div class="container">
        <b-table striped hover small stacked="md" :items="items" :fields="fields">
              <template slot="Result" slot-scope="data">
                {{data.item.team1Forecast}} - {{data.item.team2Forecast}}
            </template>
            <template slot="score" slot-scope="data">
              {{data.item.points}}
            </template>
        </b-table>
    </div>
</template>

<script lang="ts">
import ITableFields from '@/helpers/ITableFields';
import IFixtureData from '@/interfaces/IFixtureData';
import Axios from 'axios';
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';

const AppProps = Vue.extend({
  props: {
    id: Number
  }
});

@Component
export default class UserForecast extends AppProps {

    private otherid: number = 0;
    private fields: ITableFields[] = [];
    private items: IFixtureData[] = [];

    constructor() {
        super();
        this.fields.push({key: 'team1Name', label: 'Equipo'});
        this.fields.push({key: 'Result', label:  'Resultado'});
        this.fields.push({key: 'team2Name', label:  'Equipo'});
        this.fields.push({key: 'score', label:  'Puntaje'});
    }

    @Watch('id')
    private MostrarError(newvalue: number, oldvalue: number) {
        this.otherid = newvalue;
        this.$http.get('forecast/others?UserId=' + this.otherid, {withCredentials: true})
        .then(Response => this.items = Response.data);
    }
}
</script>