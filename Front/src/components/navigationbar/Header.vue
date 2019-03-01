<template>
  <div>
      <b-navbar toggleable="md" type="dark" variant="info">
        <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
        <b-navbar-brand>Prode!!!! </b-navbar-brand>
        <b-navbar-brand v-if="LogedIn">Hola  {{ComputedUserName}}</b-navbar-brand>
        <b-collapse is-nav id="nav_collapse">
          <b-navbar-nav class="ml-auto">
            <b-nav-item to="/Posiciones" v-if="LogedIn">Posiciones</b-nav-item>
            <b-nav-item to="/" exact>Calendario</b-nav-item>
            <b-nav-item to="/Forecast" v-if="LogedIn">Pronosticos</b-nav-item>
            <b-nav-item to="/Rules">Reglas</b-nav-item>
            <b-nav-item to="/Profile" v-if="LogedIn">Perfil</b-nav-item>
            <b-nav-item to="/Login" v-if="!LogedIn">Entrar</b-nav-item>
            <b-nav-item to="/Logout" v-if="LogedIn">Salir</b-nav-item>
            <b-nav-item to="/Admin" v-if="ComputedIsAdmin">Administrar</b-nav-item>
          </b-navbar-nav>
          <!-- Right aligned nav items -->
        </b-collapse>
      </b-navbar>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class Header extends Vue {
  get IsAdmin(): boolean {
    return this.$store.getters['UserData/IsAdmin'];
  }
  get LogedIn(): boolean {
    return this.$store.getters['UserData/LogedIn'];
  }
}
</script>



