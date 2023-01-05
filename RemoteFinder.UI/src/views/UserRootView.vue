<template>
    <v-app>
        <toolbar
            @toggleDrawer="drawerApp = !drawerApp"
        />
        <v-main>
            <dashboard-list/>
        </v-main>
        <snack-displayer/>
    </v-app>
</template>

<script>
import Toolbar from "@/components/user/common/Toolbar";
import SnackDisplayer from "@/components/common/SnackDisplayer";
import DashboardList from "@/components/dashboard/DashboardList";

export default {
    components: {
        toolbar: Toolbar,
        snackDisplayer: SnackDisplayer,
        DashboardList,
    },
    data: () => ({
        drawerApp: true,
    }),
}
</script>
