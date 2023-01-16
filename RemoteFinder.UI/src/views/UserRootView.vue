<template>
    <v-app>
        <toolbar
            @toggleDrawer="drawerApp = !drawerApp"
        />
        <v-main>
            <div class="fill-height">
                <div class="d-flex fill-height align-center justify-center flex-column">
                    <router-view/>
                </div>
            </div>
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
