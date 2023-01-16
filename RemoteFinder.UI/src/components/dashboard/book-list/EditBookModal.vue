<template>
    <form @submit="submitForm" novalidate="true">
        <v-card>
            <v-card-title>
                <span class="headline">Edit the book</span>
            </v-card-title>
            <v-card-text>
                <div v-if="!loading">
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
            </v-card-text>
            <v-card-actions v-if="categories.length > 0">
                <v-spacer></v-spacer>
                <v-btn
                    color="primary"
                    type="submit"
                    variant="tonal"
                    :loading="loadingSave"
                    :disabled="loadingSave"
                >Update book data
                </v-btn>
            </v-card-actions>
        </v-card>
    </form>
</template>

<script>
import AxiosService from "@/AxiosService";
import {remapErrors} from "@/helpers/errorsHelper";

export default {
    name: "EditBookModal",
    props: {
        itemId: Number,
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
        Promise.all([this.getBooksCategories(), this.getBook()]).then(() => {});
    },
    methods: {
        getBook() {
            return AxiosService.getInstance()
                .get(`book/${this.itemId}`)
                .then(bookItem => {
                    this.form = bookItem;
                    this.loading = false;
                })
                .catch(err => {
                    this.loading = false;
                });
        },
        getBooksCategories() {
            return AxiosService.getInstance()
                .get("category")
                .then(categories => {
                    this.categories = categories;
                    this.loading = false;
                })
                .catch(err => {
                    this.loading = false;
                });
        },

        submitForm(e) {
            e.preventDefault();

            this.loadingSave = true;

            AxiosService.getInstance()
                .put(`book/${this.itemId}`, this.form)
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