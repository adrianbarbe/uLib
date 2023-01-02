<template>
    <div>
        <!--        <v-menu v-if="$auth.isAuthenticated()" offset-y left>-->
        <v-menu offset-y left>
            <template v-slot:activator="{ on }">
                <v-btn v-on="on" color="info">
                    <!--                    {{$auth.getUsername()}}-->
                    Username
                </v-btn>
            </template>
            <v-list>
                <v-list-item :to="{name: 'settingsRoot'}">
                    <v-list-item-title>Settings</v-list-item-title>
                </v-list-item>
                <!--                <v-list-item @click="$auth.logout()">-->
                <v-list-item>
                    <v-list-item-title>Logout</v-list-item-title>
                </v-list-item>
            </v-list>
        </v-menu>
        <!--        <v-btn v-if="!$auth.isAuthenticated()" @click="$auth.login()" color="warning">-->
        <v-btn color="warning">
            Login
        </v-btn>
    </div>
</template>