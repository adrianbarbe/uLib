<template>
    <v-app>
        <vue-headful title="uLib"/>
        <v-container fluid class="fill-height">
            <div class="d-flex fill-height align-center justify-center flex-column">
                <router-view/>
            </div>
        </v-container>
        <snack-displayer/>
    </v-app>
</template>

<script>
import SnackDisplayer from "@/components/common/SnackDisplayer";

export default {
    components: {
        snackDisplayer: SnackDisplayer,
    },
    data: () => ({
        drawerApp: true,
    }),
}
</script>
