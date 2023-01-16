<template>
    <div class="d-flex align-center justify-center flex-column" v-if="!books.length && !loading">
        <img
            :src="require('../../../assets/empty_bookshelf.png')"
            width="150"
            alt="empty_bookshelf"
        >
        <p>Your bookshelf is empty...</p>
        <p>You could start using this app by adding your first book!</p>
        <v-btn @click="addDialog = true" class="mt-8" size="large">Add your first book</v-btn>
    </div>

    <div v-if="loading">
        <v-progress-circular color="primary" indeterminate size="200"></v-progress-circular>
    </div>


    <Transition name="slide">
        <div v-if="books.length && !loading" class="bookshelf-wrapper">
            <div class="book-items-grid">
                <div class="book-item" v-for="book in books" :key="book.id">
                    <div @click="onOpenDialog(book.id)" class="book-item__title">
                        {{ book.name }}
                    </div>
                    <div class="book-item__actions">
                        <v-btn
                            icon="mdi-pencil"
                            color="primary"
                            class="mr-2"
                            variant="plain"
                            size="x-small"
                            @click="onEditDialog(book.id)"
                        ></v-btn>
                        <v-btn
                            icon="mdi-delete"
                            color="error"
                            variant="plain"
                            size="x-small"
                            @click="onDeleteDialog(book.id)"
                        ></v-btn>
                    </div>
                </div>
            </div>
        </div>
    </Transition>
    <div v-if="shouldShowPagination()">
        <v-btn v-if="shouldShowPreviousPage()" @click="setPreviousPage()" size="small">
            <v-icon>mdi-chevron-left</v-icon>
            Previous bookshelf
        </v-btn>
        <v-btn v-if="shouldShowNextPage()" @click="setNextPage()" class="ml-2" size="small">Next bookshelf
            <v-icon>mdi-chevron-right</v-icon>
        </v-btn>
    </div>

    <v-btn v-if="books.length && !loading" @click="addDialog = true" class="mt-8" size="large">Add another book</v-btn>

    <v-dialog v-model="addDialog" max-width="500">
        <add-book-modal v-if="addDialog" @saved="onSaved" @closed="onClosed"/>
    </v-dialog>

    <v-dialog v-model="editDialog" max-width="500">
        <edit-book-modal @saved="onEdited" v-if="editDialog" :item-id="itemId" @closed="onClosed"/>
    </v-dialog>

    <v-dialog v-model="deleteDialog" max-width="500">
        <delete-book-modal @removed="onRemoved" v-if="deleteDialog" :item-id="itemId" @closed="onClosed"/>
    </v-dialog>

    <v-dialog fullscreen
              :scrim="false"
              transition="dialog-bottom-transition"
              v-model="openDialog"
    >
        <open-book-modal v-if="openDialog" :item-id="itemId" @closed="onClosed"></open-book-modal>
    </v-dialog>

</template>

<script>
import AxiosService from "@/AxiosService";
import AddBookModal from "@/components/dashboard/book-list/AddBookModal";
import EditBookModal from "@/components/dashboard/book-list/EditBookModal";
import OpenBookModal from "@/components/dashboard/book-list/OpenBookModal";
import DeleteBookModal from "@/components/dashboard/book-list/DeleteBookModal";

export default {
    name: "DashboardList",
    components: {
        AddBookModal,
        EditBookModal,
        OpenBookModal,
        DeleteBookModal,
    },
    data: () => ({
        books: [],
        totalItems: 0,
        itemsPerPage: 16,
        pageNumber: 1,

        itemId: null,

        addDialog: false,
        openDialog: false,
        editDialog: false,
        deleteDialog: false,

        loading: false,
    }),

    mounted() {
        this.pageNumber = 1;
        this.getBooksList();
    },
    methods: {
        getBooksList(shouldShowLoader = true) {
            if (shouldShowLoader) {
                this.loading = true;
            }

            AxiosService.getInstance()
                .get(`book?pageNumber=${this.pageNumber}&itemsPerPage=${this.itemsPerPage}`)
                .then(books => {
                    this.books = books.items;
                    this.totalItems = books.total;

                    this.loading = false;
                })
                .catch(_ => {
                    this.loading = false;
                });
        },
        getPagesNumber() {
            return Math.ceil(this.totalItems / this.itemsPerPage);
        },
        shouldShowNextPage() {
            return this.pageNumber < this.getPagesNumber();
        },
        shouldShowPreviousPage() {
            return this.pageNumber > 1;
        },
        shouldShowPagination() {
            return this.getPagesNumber() > 1;
        },
        setNextPage() {
            if (this.pageNumber < this.getPagesNumber()) {
                this.pageNumber++;
                this.getBooksList(false);
            }
        },
        setPreviousPage() {
            if (this.pageNumber - 1 > 0) {
                this.pageNumber--;
                this.getBooksList(false);
            }
        },

        onEditDialog(itemId) {
            this.editDialog = true;
            this.itemId = itemId;
        },
        onDeleteDialog(itemId) {
            this.deleteDialog = true;
            this.itemId = itemId;
        },
        onSaved() {
            this.addDialog = false;
            this.getBooksList();
        },
        onEdited() {
            this.editDialog = false;
            this.getBooksList();
        },
        onRemoved() {
            this.deleteDialog = false;
            this.getBooksList();
        },
        onOpenDialog(itemId) {
            this.openDialog = true;
            this.itemId = itemId;
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

<style scoped lang="scss">
.bookshelf-wrapper {
    display: block;
    width: 674px;
    height: 713px;
    padding: 40px 70px;
    background: url('../../../assets/empty_bookshelf.png') center no-repeat;
}

.book-items-grid {
    height: 100%;
    width: 100%;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    grid-template-rows: repeat(4, 1fr);
    grid-column-gap: 15px;
    grid-row-gap: 23px;
    align-items: end;
    justify-items: center;
}

.book-item {
    display: flex;
    flex-direction: column;
    position: relative;
    width: 120px;
    height: 140px;
    background: red;
    background: url('../../../assets/book_cover.png') center no-repeat;
    background-size: contain;
    align-items: center;
    justify-content: center;
    padding: 0 8px 0 28px;
    font-size: 0.8em;
    color: white;
    cursor: pointer;
    text-align: center;
    transition: all .2s ease-in-out;

    &:hover {
        transform: scale(1.1);

        .book-item__actions {
            display: block;
        }
    }

    &__title {
        padding: 20px 0px;
    }

    &__actions {
        position: absolute;
        bottom: 10px;
        display: none;
    }
}


.slide-leave-active,
.slide-enter-active {
    transition: 1s;
}
.slide-enter {
    transform: translate(100%, 0);
}
.slide-leave-to {
    transform: translate(-100%, 0);
}
</style>