<template>
    <div class="container">
        <b-container class="mt-2">
            <b-row>
                <b-col>
                    <b-form-select v-model="selected" :options="options" class="mb-3" />
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
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class Positions extends Vue {
}
</script>
