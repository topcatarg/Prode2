<template>
    <div class="container">
        <b-form-select v-model="FilterValue" class="m-md-2">
            <option value="">Todos los grupos</option>
            <option value="A">A</option>
            <option value="B">B</option>
            <option value="C">C</option>
            <option value="CUARTOS">CUARTOS</option>
            <option value="SEMIFINALES">SEMIFINALES</option>
            <option value="FINALES">FINALES</option>
        </b-form-select>
        <b-table striped hover stacked="md" :items="filteredItems" :fields="fields" :busy="Loading">
            <div slot="table-busy" class="text-center text-danger my-2">
                <b-spinner variant="primary" label="Spinning" />
            </div>
            <template slot="date" slot-scope="data">
                {{data.item.standardDate}}
            </template>
            <template slot="team1Name" slot-scope="data">
                <b-img v-if="data.item.team2Flag!=null" :src="'/img/assets/'+ data.item.team1Flag +'.png'" />
                {{data.item.team1Name}}
            </template>
            <template slot="Result" slot-scope="data">
                {{data.item.team1Goals}} - {{data.item.team2Goals}}
            </template>
            <template slot="team2Name" slot-scope="data">
                <b-img v-if="data.item.team2Flag!=null" :src="'/img/assets/'+ data.item.team2Flag +'.png'" />
                {{data.item.team2Name}}
            </template>
        </b-table>
    </div>
</template>
<script lang="ts">
import { Component, Vue, Watch} from 'vue-property-decorator';
import IFixtureTableData from '../helpers/IFixtureTableData';
import ITableFields from '../helpers/ITableFields';


@Component
export default class Schedule extends Vue {
    private fields: ITableFields[] = [];
    private items: IFixtureTableData[] = [];
    private filteredItems: IFixtureTableData[] = this.items;
    private FilterValue: string = '';
    private Loading: boolean = false;

    constructor() {
        super();
        this.fields.push({key: 'group', label: 'Grupo'});
        this.fields.push({key: 'date', label: 'Fecha'});
        this.fields.push({key: 'team1Name', label: 'Equipo'});
        this.fields.push({key: 'Result', label: 'Resultado'});
        this.fields.push({key: 'team2Name', label: 'Equipo'});
    }

    private mounted() {
        this.Loading = true;
        const self = this;
        this.$http.get('fixture/allmatchs')
        .then(Response => {
            self.items = self.filteredItems = Response.data;
        })
        .finally(() => self.Loading = false);
    }

    @Watch('FilterValue')
    private filtrar() {
        if (this.FilterValue === '') {
            this.filteredItems = this.items;
            return;
        }
        this.filteredItems = [];
        this.items.forEach(element => {
            if (element.group === this.FilterValue) {
                this.filteredItems.push(element);
            }
        });
    }
}
</script>