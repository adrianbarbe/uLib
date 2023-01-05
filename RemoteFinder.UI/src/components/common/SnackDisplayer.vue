<template>
    <v-snackbar
        v-model="show"
        :color="color"
        :multi-line="true"
        :timeout=3000
    >
        {{ message }}
        <template v-slot:actions>
            <v-btn variant="text" @click="$store.commit('snackMessages/update', { show: false })">Close</v-btn>
        </template>
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