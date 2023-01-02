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
        
        AxiosService.getInstance()
            .post(`oauth2/authorize-sign-up`, {code: code})
            .then(res => {
                console.log(res);
            });
    }
}
</script>

<style scoped>

</style>