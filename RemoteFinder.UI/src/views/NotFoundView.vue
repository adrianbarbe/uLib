<template>
    <v-container fluid class="fill-height">
               <div class="d-flex fill-height align-center justify-center flex-column">
                   <div>
                       <logo/>
                   </div>
                   <div>
                       <p class="text-h4 text-center">Cannot found this page!</p>
                       <p class="text-h5 mt-5 text-center">
                           <router-link to="/">Navigate to Home</router-link>
                       </p>
                   </div>
               </div>
    </v-container>
</template>
<script>
import Logo from "@/components/common/Logo";
export default {
    components: {
        Logo,
    }
}
</script>