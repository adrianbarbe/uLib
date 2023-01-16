<template>
    <div v-if="loading">
        <v-progress-circular color="primary" indeterminate size="200"></v-progress-circular>
    </div>
    <div v-if="!categories.length && !loading">
        <div>
            <img
                class="ml-7"
                :src="require('../../../assets/bookmark_rainbow.png')"
                width="150"
                alt="empty_bookshelf"
            >
        </div>
        <p class="text-center">Your book categories are not yet set-up...</p>
        <v-btn @click="addDialog = true" class="mt-8" size="large">Add your first category</v-btn>
    </div>

    <div v-if="categories.length && !loading">
        <v-table class="categories-table">
            <thead>
            <tr>
                <th class="text-left category-name-header">
                    Category Name
                </th>
                <th class="text-left category-name-header">
                    Books Count
                </th>
                <th class="text-left">
                    Actions
                </th>
            </tr>
            </thead>
            <tbody>
            <tr
                v-for="category in categories"
                :key="category.id"
            >
                <td><span>{{ category.name }}</span></td>
                <td>{{category.books ? category.books.length : "-"}}</td>
                <td>
                    <v-btn
                        icon="mdi-pencil"
                        color="primary"
                        class="mr-2"
                        variant="plain"
                        size="small"
                        @click="onEditDialog(category.id)"
                    ></v-btn>
                    <v-btn
                        icon="mdi-delete"
                        color="error"
                        variant="plain"
                        size="small"
                        @click="onDeleteDialog(category.id)"
                    ></v-btn>
                </td>
            </tr>
            </tbody>
        </v-table>
        <v-btn @click="addDialog = true" class="mt-8" size="large">Add category</v-btn>
    </div>

    <v-dialog v-model="addDialog" max-width="500">
        <add-category-modal v-if="addDialog" @saved="onSaved" @closed="onClosed"/>
    </v-dialog>

    <v-dialog v-model="editDialog" max-width="500">
        <edit-category-modal @saved="onEdited" v-if="editDialog" :item-id="itemId" @closed="onClosed"/>
    </v-dialog>

    <v-dialog v-model="deleteDialog" max-width="500">
        <delete-category-modal @removed="onRemoved" v-if="deleteDialog" :item-id="itemId" @closed="onClosed"/>
    </v-dialog>
</template>

<script>
import AxiosService from "@/AxiosService";
import AddCategoryModal from "@/components/dashboard/categories-list/AddCategoryModal";
import EditCategoryModal from "@/components/dashboard/categories-list/EditCategoryModal";
import DeleteCategoryModal from "@/components/dashboard/categories-list/DeleteCategoryModal";

export default {
    name: "DashboardList",
    components: {
        AddCategoryModal,
        EditCategoryModal,
        DeleteCategoryModal,
    },
    data: () => ({
        categories: [],

        itemId: null,
        
        addDialog: false,
        editDialog: false,
        deleteDialog: false,
        
        loading: true,
    }),

    mounted() {
        this.getCategoryList();
    },
    methods: {
        getCategoryList() {
            AxiosService.getInstance()
                .get("category")
                .then(categories => {
                    this.categories = categories;
                    this.loading = false;
                })
                .catch(err => {
                    this.loading = false;
                });
        },
        onSaved() {
            this.addDialog = false;
            this.getCategoryList();
        },
        onDeleteDialog(itemId) {
            this.deleteDialog = true;
            this.itemId = itemId;
        },
        onRemoved() {
            this.deleteDialog = false;
            this.getCategoryList();
        },
        onEditDialog(itemId) {
            this.editDialog = true;
            this.itemId = itemId;
        },
        onEdited() {
            this.editDialog = false;
            this.getCategoryList();
        },
        onClosed() {
            this.openDialog = false;
            this.deleteDialog = false;
            this.editDialog = false;
            this.addDialog = false;
        }
    }
}
</script>

<style lang="scss" scoped>
.categories-table {
    & table thead tr th {
        font-size: 1em;
        
        &.category-name-header {
            min-width: 30em;
        }
    }
}
</style>