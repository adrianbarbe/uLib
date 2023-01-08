<template>
    <v-progress-circular color="primary" indeterminate size="200"></v-progress-circular>
</template>

<script>

import AxiosService from "@/AxiosService";

export default {
    name: "AuthRedirect",
    mounted() {
        const queryParams = new URLSearchParams(window.location.search);

        const code = queryParams.get('code');
        
        AxiosService.getInstance({}, {}, true)
            .post(`oauth2/authorize`, {code: code})
            .then(res => {
                this.$auth.login(res.id_token, res.refresh_token);

                this.$router.push({name: 'dashboard'});
                console.log(res);
            });
    }
}
</script>

<style scoped>

</style>