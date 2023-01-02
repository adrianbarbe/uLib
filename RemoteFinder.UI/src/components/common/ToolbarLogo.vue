<template>
    <div :class="{'avatar-image': true, 'avatar-image-env': $config.configMode !== 'production'}">
        <img
            :src="require('../../assets/ulib_logo.png')"
            alt="uLIb"
        >
        <div v-if="$config.configMode === 'development'" class="env-text-helper">Develop</div>
        <div v-if="$config.configMode === 'staging'" class="env-text-helper">Staging</div>
    </div>
</template>