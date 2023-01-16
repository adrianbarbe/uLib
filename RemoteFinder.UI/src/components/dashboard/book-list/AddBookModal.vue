<template>
    <form @submit="submitForm" novalidate="true">
        <v-card>
            <v-card-title>
                <span class="headline">Add new book to your bookshelf</span>
            </v-card-title>
            <v-card-text>
                <div class="d-flex align-center justify-center" v-if="loading">
                    <v-progress-circular color="primary" indeterminate size="200"></v-progress-circular>
                </div>
                <div v-if="categories.length && !loading">
                    <file-uploader @uploaded="form.file = $event" class="mb-8"/>
                    <v-select
                        :items="categories"
                        :error-messages="errors.category"
                        v-model="form.category"
                        item-title="name"
                        item-value="id"
                        return-object
                        label="Book category"
                    ></v-select>
                    <v-text-field :error-messages="errors.name"
                                  v-model="form.name"
                                  label="Your Book Name"
                    ></v-text-field>
                </div>
                <div v-if="!categories.length && !loading" class="text-subtitle-1 mt-5 mb-5">
                    Before starting, please
                    <router-link to="/about">add a book category</router-link>
                </div>
            </v-card-text>
            <v-card-actions v-if="categories.length > 0">
                <v-spacer></v-spacer>
                <v-btn
                    color="primary"
                    type="submit"
                    variant="tonal"
                    :loading="loadingSave"
                    :disabled="loadingSave"
                >Save book into library
                </v-btn>
            </v-card-actions>
        </v-card>
    </form>
</template>

<script>
import AxiosService from "@/AxiosService";
import {remapErrors} from "@/helpers/errorsHelper";
import FileUploader from "@/components/dashboard/FileUploader";

export default {
    name: "AddBookModal",
    components: {
        FileUploader,
    },
    data: () => ({
        categories: [],
       
        loading: false,
        loadingSave: false,

        form: {
            id: 0,
            file: '',
            name: '',
            category: null,
        },
        errors: {
            name: '',
            category: '',
        }
    }),
    mounted() {
        this.getBooksCategories();
    },
    methods: {
        getBooksCategories() {
            this.loading = true;
            
            AxiosService.getInstance()
                .get("category")
                .then(categories => {
                    this.categories = categories;
                    this.loading = false;
                })
                .catch(_ => {
                    this.loading = false;
                });
        },

        submitForm(e) {
            e.preventDefault();

            this.loadingSave = true;

            AxiosService.getInstance()
                .post("book", this.form)
                .then(_ => {
                    this.$emit("saved");
                    this.loadingSave = false;
                })
                .catch(err => {
                    this.loadingSave = false;
                    if (err.errors) {
                        this.errors = remapErrors(err.errors);
                    }
                });
        },
    },
}
</script>

<style scoped>

</style>