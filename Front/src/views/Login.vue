<template>
    <div class="container">
        <b-tabs class="mt-2">
            <b-tab title="Ingresar" active>
                <LoginUser/>
            </b-tab>
            <b-tab title="Crear Usuario">
                <CreateUser/>
            </b-tab>
            <b-tab title="Recuperar Password">
               <RecoverMail />
            </b-tab>
        </b-tabs>
    </div>
</template>

<script lang="ts">
import CreateUser from '@/components/login/CreateUser.vue';
import LoginUser from '@/components/login/LoginUser.vue';
import RecoverMail from '@/components/login/RecoverMail.vue';
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';

@Component({
  components: {
    CreateUser,
    LoginUser,
    RecoverMail
  }
})
export default class Login extends Vue {
}
</script>