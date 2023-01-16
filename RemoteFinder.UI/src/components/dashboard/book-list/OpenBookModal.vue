<template>
    <v-card>
        <v-toolbar
            dark
            color="white"
        >
            <v-btn
                icon
                dark
                @click="$emit('closed')"
            >
                <v-icon>mdi-close</v-icon>
            </v-btn>
            <v-toolbar-title>Read the book</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items>
                <v-btn
                    variant="text"
                    @click="$emit('closed')"
                >
                    Close
                </v-btn>
            </v-toolbar-items>
        </v-toolbar>
        <iframe v-if="loadedBook" v-show="!loading" :src="getPdfLink()" @load="iframeLoaded()" style="width: 100%; height: 100%; border: none;"></iframe>
        <v-layout class="d-flex justify-center" v-if="loading">
            <v-progress-circular
                :size="150"
                color="primary"
                indeterminate
            ></v-progress-circular>
        </v-layout>
    </v-card>
</template>

<script>
import AxiosService from "@/AxiosService";
export default {
    name: "OpenBookModal",
    props: {
        itemId: Number,
    },
    data: () => ({
        bookItem: null,
        loading: false,
        loadedBook: false,
    }),
    mounted() {
        this.getBook();
    },
    methods: {
        getBook() {
            this.loading = true;
            
            return AxiosService.getInstance()
                .get(`book/${this.itemId}`)
                .then(bookItem => {
                    this.bookItem = bookItem;
                    this.loadedBook = true;
                    this.loading = false;
                })
                .catch(_ => {
                    this.loadedBook = false;
                    this.loading = false;
                });
        },
         getPdfLink() {
            const url = `${this.$config.apiEndpoint}/file/get/book/${this.bookItem.fileName}`;
            
            return url;
        },
        iframeLoaded() {
            this.loading = false;
        }
    },
}
</script>

<style scoped>

</style>