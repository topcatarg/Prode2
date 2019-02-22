<template>
  <div>
      <b-navbar toggleable="md" type="dark" variant="info">
        <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
        <b-navbar-brand>Prode!!!! </b-navbar-brand>
        <b-navbar-brand v-if="ComputedUserName !== ''">Hola  {{ComputedUserName}}</b-navbar-brand>
        <b-collapse is-nav id="nav_collapse">
          <b-navbar-nav class="ml-auto">
            <b-nav-item to="/Posiciones" v-if="ComputedUserName !== ''">Posiciones</b-nav-item>
            <b-nav-item to="/" exact>Calendario</b-nav-item>
            <b-nav-item to="/Forecast" v-if="ComputedUserName !== ''">Pronosticos</b-nav-item>
            <b-nav-item to="/Rules">Reglas</b-nav-item>
            <b-nav-item to="/Profile" v-if="ComputedUserName !== ''">Perfil</b-nav-item>
            <b-nav-item to="/Login" v-if="ComputedUserName === ''">Entrar</b-nav-item>
            <b-nav-item to="/Logout" v-if="ComputedUserName !== ''">Salir</b-nav-item>
            <b-nav-item to="/Admin" v-if="ComputedIsAdmin">Administrar</b-nav-item>
            <b-nav-item @click="GoogleLogin()"> Google login </b-nav-item>
          </b-navbar-nav>
          <!-- Right aligned nav items -->
        </b-collapse>
      </b-navbar>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
// import { IUserInfo, ProfileState  } from '../models/UserInfo';
// import store from '../store';


@Component
export default class Header extends Vue {
  get ComputedIsAdmin(): boolean {
    return this.$store.getters.IsAdmin;
  }
  get ComputedUserName(): string {
    return this.$store.getters.UserName;
  }
  private GoogleLogin(): void {
    console.log('apreto google');
    this.$gAuth.signIn()
    .then(GoogleUser => {
      // On success do something, refer to
      // https://developers.google.com/api-client-library/javascript/reference/referencedocs#googleusergetid
      console.log('user', GoogleUser)
      // this.isSignIn = this.$gAuth.isAuthorized
    })
.catch(error  => {
  // on fail do something
})
  }
}
</script>



