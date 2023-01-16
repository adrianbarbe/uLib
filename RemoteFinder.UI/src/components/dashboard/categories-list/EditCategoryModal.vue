<template>
    <form @submit="submitForm" novalidate="true">
        <v-card>
            <v-card-title>
                <span class="headline">Edit book category</span>
            </v-card-title>
            <v-card-text>
                    <v-text-field :error-messages="errors.name"
                                  v-model="form.name"
                                  label="Your Book Category Name"
                    ></v-text-field>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                    color="primary"
                    type="submit"
                    variant="tonal"
                    :loading="loadingSave"
                    :disabled="loadingSave"
                >Save changes to category
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
    name: "EditCategoryModal",
    props: {
        itemId: Number,
    },
    data: () => ({
        loadingSave: false,

        form: {
            id: 0,
            name: '',
        },
        errors: {
            name: '',
        }
    }),
    mounted() {
        this.getBooksCategory();
    },
    methods: {
        getBooksCategory() {
            AxiosService.getInstance()
                .get(`category/${this.itemId}`)
                .then(category => {
                    this.form = category;
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
                .put(`category/${this.itemId}`, this.form)
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