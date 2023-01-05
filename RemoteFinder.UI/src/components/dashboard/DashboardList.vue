<template>
    <div class="fill-height">
        <div class="d-flex fill-height align-center justify-center flex-column">
            <img
                :src="require('../../assets/empty_bookshelf.png')"
                width="150"
                alt="empty_bookshelf"
            >
            <p>Your bookshelf is empty...</p>
            <p>You could start using this app by adding your first book!</p>
            <v-btn @click="addDialog = true" class="mt-8" size="large">Add your first book</v-btn>
        </div>

        <v-dialog v-model="addDialog" max-width="500">
                <form @submit="submitForm" novalidate="true">
                    <v-card>
                        <v-card-title>
                            <span class="headline">Add new book to your bookshelf</span>
                        </v-card-title>
                        <v-card-text>
                            <file-uploader @uploaded="form.fileName = $event" class="mb-8"/>
                            <v-text-field :error-messages="errors.name" 
                                          v-model="form.name"
                                          label="Your Book Name"
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
                            >Save book into library
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </form>
        </v-dialog>
    </div>
</template>

<script>
import FileUploader from "@/components/dashboard/FileUploader";

export default {
    name: "DashboardList",
    components: {
        FileUploader,
    },
    data: () => ({
        addDialog: false,

        loadingSave: false,

        form: {
            fileName: '',
            name: '',
        },
        errors: {
            name: '',
        }
    }),

    methods: {
        submitForm(e) {
            e.preventDefault();

            this.loadingSave = true;
            console.log("this.form", this.form);
            
            // HttpService.getInstance()
            //     .post("books", this.form)
            //     .then(resp => {
            //         this.loadingSave = false;
            //     })
            //     .catch(err => {
            //         this.loadingSave = false;
            //         if (err.errors) {
            //             this.errors = remapErrors(err.errors);
            //         }
            //     });
        },
    }
}
</script>

<style scoped>

</style>