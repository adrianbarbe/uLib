<template>
    <v-card>
        <v-card-title class="title">Deleting the book</v-card-title>
        <v-card-text>
            Are you sure you want to delete this book?
        </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
                color="primary"
                @click="$emit('closed')"
                :disabled="loadingDelete"
            >No
            </v-btn>
            <v-btn
                text
                @click="removeItem"
                :loading="loadingDelete"
                :disabled="loadingDelete"
            >Yes
            </v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>
import AxiosService from "@/AxiosService";
export default {
    data: () => {
        return {
            loadingDelete: false,
        };
    },
    props: {
        itemId: Number,
    },
    methods: {
        removeItem() {
            this.loadingDelete = true;

            AxiosService.getInstance()
                .delete(`book/${this.itemId}`)
                .then(_ => {
                    this.loadingDelete = false;

                    this.$emit("removed");
                });
        }
    }
}
</script>