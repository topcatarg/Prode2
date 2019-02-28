<template>
    <div class="container">
        <b-container class="mt-2">
            <b-row>
                <b-col>
                    <b-form-select 
                        v-model="selected" 
                        :options="options" 
                        class="mb-3" 
                        v-if="!ComboSearching"/>
                    <b-spinner 
                        variant="primary" 
                        label="Spinning" 
                        v-if="ComboSearching"/>
                </b-col>
            </b-row>
        </b-container>
        <b-table striped hover stacked="md" :items="items" :fields="fields" class="mt-2">
            <template slot="userdata" slot-scope="data">
                <b-button @click="ShowModal(data.item.userId)" variant="primary">Ver puntajes</b-button>
            </template>
        </b-table>
        <b-modal v-model="showmodal" size="lg" centered ok-only hide-header>
            <UserForecast :id="this.userid"/>
        </b-modal>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import UserForecast from '../components/positions/UserForecast.vue';
import IFixtureData from '../helpers/IFixtureData';
import ISelectInput from '../helpers/ISelectInput';
import IResultTableFields from '../helpers/ITableFields';
import GameGroups from '../interfaces/IGameGroups';
import IResults from '../interfaces/IResults';

@Component({
  components: {
    UserForecast
  }
})
export default class Positions extends Vue {
    public items: IResults[] = [];
    public fields: IResultTableFields[] = [];
    public selected: number = 0;
    public options: ISelectInput[] = [];
    public userid: number = 0;
    private showmodal: boolean = false;
    private otheritems: IFixtureData[] = [];
    private ComboSearching: boolean = false;
    private GridSearching: boolean = false;

    constructor() {
        super();
        this.fields.push( {
            key: 'position',
            label: 'Posición'
        });
        this.fields.push( {
            key: 'teamName',
            label: 'Nombre del equipo'
        });
        this.fields.push( {
            key: 'score',
            label: 'Puntajes'
        });
        this.fields.push( {
            key: 'userdata',
            label: ''
        });
    }

    private mounted() {
        this.ComboSearching = true;
        const self = this;
        this.$http.get('GetUserGroups',
        {withCredentials: true})
        .then(response => {
            response.data.forEach((element: GameGroups)  => {
                this.options.push({
                    value: element.id,
                    text: element.gameGroup
                });
            });
        })
        .finally(() => self.ComboSearching = false);
    }

    @Watch ('selected')
    private ChangeGroup() {
        this.$http.get('results?GroupNumber='
        + this.selected,
        {withCredentials: true})
        .then(data => {
            this.items = data.data;
            let i = 1;
            this.items.forEach(
                (e: IResults) => {
                    e.position = i;
                    i++;
                }
            );
        });
    }

    get UserGroupId(): number {
        return this.$store.getters.UserGroup;
    }

    private ShowModal(id: number) {
        this.userid = id;
        this.showmodal = true;
    }
}
</script>
