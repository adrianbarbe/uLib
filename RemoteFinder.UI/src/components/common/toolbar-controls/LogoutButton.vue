<template>
    <div>
        <v-menu v-if="$auth.isAuthenticated()" offset-y left>
            <template v-slot:activator="{ props }">
                <v-btn v-bind="props" color="info">
                    {{ $auth.getUsername() }}
                </v-btn>
            </template>
            <v-list>
                <v-list-item @click="logout()">
                    <v-list-item-title>Logout</v-list-item-title>
                </v-list-item>
            </v-list>
        </v-menu>
        <v-btn v-if="!$auth.isAuthenticated()" @click="login()" color="warning">
            Login
        </v-btn>
    </div>
</template>
<script>
export default {
    methods: {
        logout() {
            this.$router.push({name: 'sign-up'});
            this.$auth.logout();
        }
    }
};
</script>