<template>
    <div>
        <div v-if="!loading && !bookUploaded" class="drag-file-uploader" v-bind="getRootProps()">
            <div class="mx-3 my-10">
                <input v-bind="getInputProps()"/>
                <p>Drag & drop some PDF file here, or click to select your PDF file</p>
            </div>
        </div>
        <div v-if="loading" class="drag-file-uploader loading-parent">
            <div class="loading-file"></div>
            <div class="mx-3 my-10 text-center">
                <p>Uploading file...</p>
            </div>
        </div>
        <div class="book-name-wrapper" v-if="bookUploaded">
            <v-icon size="32">mdi-book-open-blank-variant</v-icon>
            <span class="ml-2 book-name">{{ bookUploaded }}</span>
        </div>
    </div>
</template>

<script setup>
import {useDropzone} from "vue3-dropzone";
import AxiosService from "@/AxiosService";
import {ref, defineEmits} from "vue";

const bookUploaded = ref(null);
const loading = ref(false);
const emitUploaded = defineEmits(['uploaded'])

const onDrop = (acceptFiles, rejectReasons) => {
    saveFiles(acceptFiles);
    console.log(rejectReasons);
};

const {getRootProps, getInputProps, ...rest} = useDropzone(
    {
        onDrop,
        multiple: false,
        accept: ['.pdf'],
        maxSize: 31457280,
    }
);

const saveFiles = (files) => {
    const uploadUrl = "file/upload/book";

    const formData = new FormData();
    formData.append('file', files[0]);

    loading.value = true;
    let idToken = localStorage.getItem('id_token');

    return AxiosService.getInstance(
        {
            "Content-Type": "multipart/form-data",
            "Authorization": `Bearer ${idToken}`
        }
    )
        .post(uploadUrl, formData)
        .then((response) => {
            bookUploaded.value = response.fileName;
            emitUploaded('uploaded', response);
            loading.value = false;
        })
        .catch((err) => {
            console.error(err);
        });
};
</script>

<style lang="scss" scoped>
.drag-file-uploader {
    position: relative;
    overflow: hidden;
    border-radius: 50px;
    border: 3px dashed #ccc;

    &:not(.loading-parent):hover {
        background: #e7e7e7;
        cursor: pointer;
    }
}

.book-name-wrapper {
    display: flex;
    align-items: center;
}

.book-name {
    font-size: 1rem;
}

.loading-parent {
    background-color: rgb(211,211,211);
    z-index: 44;
}

.loading-file {
    position: absolute;
    left: -45%;
    height: 100%;
    width: 45%;
    background-image: linear-gradient(to left, rgba(251,251,251, .05), rgba(251,251,251, .3), rgba(251,251,251, .6), rgba(251,251,251, .3), rgba(251,251,251, .05));
    background-image: -moz-linear-gradient(to left, rgba(251,251,251, .05), rgba(251,251,251, .3), rgba(251,251,251, .6), rgba(251,251,251, .3), rgba(251,251,251, .05));
    background-image: -webkit-linear-gradient(to left, rgba(251,251,251, .05), rgba(251,251,251, .3), rgba(251,251,251, .6), rgba(251,251,251, .3), rgba(251,251,251, .05));
    animation: loading 1s infinite;
    z-index: 45;
}
@keyframes loading {
    0%{
        left: -45%;
    }
    100%{
        left: 100%;
    }
}
</style>