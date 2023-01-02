<template>
    <v-snackbar
        v-model="show"
        :color="color"
        :multi-line="true"
        :timeout=3000
    >
        {{ message }}
        <v-btn dark text @click="$store.commit('snackMessages/update', { show: false })">
            <v-icon>clear</v-icon>
        </v-btn>
    </v-snackbar>
</template>
<script>
export default {
    computed: {
        message() {
            return this.$store.getters['snackMessages/message'];
        },
        color() {
            return this.$store.getters['snackMessages/color'];
        },
        show: {
            get() {
                return this.$store.getters['snackMessages/show'];
            },
            set(value) {
                this.$store.commit('snackMessages/update', {show: value});
            }
        }
    }
}
</script>