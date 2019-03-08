<template>
    <div class="container">
        <b-form @submit="onSubmit" class="mt-4">
            <b-form-group
                label-cols="4"
                id="fieldset1"
                label="Nombre de usuario"
                :invalid-feedback="invalidUserName"
                :valid-feedback="validUserName"
                :state="stateUserName">
                <b-form-input v-model.trim="UserName" required/>
            </b-form-group>
            <b-form-group
                label-cols="4"
                id="pass2"
                label="Contraseña">
                <b-form-input v-model.trim="UserPassword" required type="password"/>
            </b-form-group>
            <b-button type="submit" variant="primary" class="mr-3">
                <b-spinner small type="grow" v-if="Ingresando">
                    Ingresando
                </b-spinner>
                <div v-if="!Ingresando">
                    Ingresar
                </div>
            </b-button>
            <ErrorAlert :message=this.GeneralError class="mt-2"/>
        </b-form>
    </div>
</template>

<script lang="ts">
import ErrorAlert from '@/components/general/ErrorAlert.vue';
import Axios from 'axios';
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';

@Component({
  components: {
    ErrorAlert
  }
})
export default class LoginUser extends Vue {

    private Ingresando: boolean = false;
    private GeneralError: string = '';
    private UserName: string = '';
    private UserPassword: string = '';

    get invalidUserName(): string {
        if (this.UserName.includes(' ')) {
            return 'No puede contener espacios';
        }
        return '';
    }

    get validUserName(): string {
        return '';
    }

    get stateUserName(): boolean {
        return !this.UserName.includes(' ');
    }

    private onSubmit() {
        this.GeneralError = '';
        this.Ingresando = true;
        const self = this;
        this.$http.post('login', {
            name: this.UserName,
            password: this.UserPassword
        })
        .then(data => {
                self.$store.dispatch('GetUserData');
                self.$router.push('/');
                }
            )
        .catch(error => this.GetError(error.response.data))
        .finally(() => self.Ingresando = false);
    }

    private GetError(data: any) {
        this.GeneralError = 'Usuario o password incorrectos';
    }
}
</script>
