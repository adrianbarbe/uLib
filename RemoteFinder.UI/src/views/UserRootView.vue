<template>
    <v-app>
        <vue-headful title="uLib"/>
        <toolbar
            @toggleDrawer="drawerApp = !drawerApp"
        />
        <v-main>
            <router-view/>
        </v-main>
        <snack-displayer/>
    </v-app>
</template>

<script>
import Toolbar from "@/components/user/common/Toolbar";
import SnackDisplayer from "@/components/common/SnackDisplayer";

export default {
    components: {
        toolbar: Toolbar,
        snackDisplayer: SnackDisplayer,
    },
    data: () => ({
        drawerApp: true,
    }),
}
</script>
