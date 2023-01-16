<template>
    <div>
        <v-progress-linear
            v-if="loadingState > 0"
            height="3"
            style="margin: 0; z-index: 999; position: absolute"
            :indeterminate="true"
            color="info"
        ></v-progress-linear>

        <v-app-bar
            color="white"
            dark
            app
            height="50px"
            :clipped-left="true"
            fixed
        >
            <v-toolbar-title style="width: 250px" class="ml-1 pa-1 cursor-pointer" @click="navigateToHome()">
                <!--        <v-app-bar-nav-icon @click.stop="$emit('toggleDrawer')"></v-app-bar-nav-icon>-->
                <toolbar-logo></toolbar-logo>
            </v-toolbar-title>

            <v-spacer></v-spacer>

            <v-toolbar-title>{{ getTopNavigationTitle }}</v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn :to="{name: 'categories'}" class="mr-5" prepend-icon="mdi-bookmark-multiple">
                Categories
            </v-btn>
            
            <logout-button/>

        </v-app-bar>
    </div>
</template>

<script>
import {mapGetters} from "vuex";
import LogoutButton from "@/components/common/toolbar-controls/LogoutButton";
import ToolbarLogo from "@/components/common/ToolbarLogo";

export default {
    name: "ToolbarComponent",
    components: {
        LogoutButton,
        ToolbarLogo
    },
    computed: {
        ...mapGetters(['selectedNavigationItem', 'getTopNavigationTitle']),
        loadingState() {
            return this.$store.getters['loadingState/loadingState'];
        },
    },
    methods: {
        isRouteWithNav() {
            return this.$route.params.group !== undefined;
        },
        navigateToHome() {
            this.$router.push('/');
        }
    },
}
</script>
<style lang="scss">
.invisible {
    visibility: hidden;
}
</style>