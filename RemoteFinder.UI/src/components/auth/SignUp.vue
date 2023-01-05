<template>
    <v-card class="sign-up-wrapper pa-10">
        <div class="d-flex align-center justify-center flex-column">
            <logo/>
            <v-btn :href="getAuthUrl()" size="x-large" color="primary" class="mt-5">
                <img width="35" :src="require('../../assets/google.png')" />
                <span class="ml-2">Sign Up by Google</span>
            </v-btn>

            <div class="mt-10 mb-2">By signing up or signing in you're agree with our <a href="#">terms and conditions</a></div>
        </div>
    </v-card>
</template>

<script>
import Logo from "@/components/common/Logo";

export default {
    name: "SignUp",
    components: {
        Logo,
    },
    methods: {
        getAuthUrl() {
            const rootUrl = `https://accounts.google.com/o/oauth2/v2/auth`;

            const clientId = '455359139560-h6e2hpgeheekl0po8ll0pv8blsf7fju9.apps.googleusercontent.com';
            const queryData = {
                scope: 'email openid profile',
                access_type: 'offline',
                response_type: 'code',
                redirect_uri: `${this.$config.appEndpoint}/auth/redirect`,
                client_id: clientId,
            };

            const qs = new URLSearchParams(queryData);

            return `${rootUrl}?${qs.toString()}`;
        }
    }
}
</script>

<style scoped>
</style>