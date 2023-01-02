import deepmerge from "deepmerge";

export const AppConfig = (config) => {
    let defaultConfig = {};
    let developmentConfig = {};
    let stagingConfig = {};
    let productionConfig = {};

    if (Object.prototype.hasOwnProperty.call(config,'default') ) {
        defaultConfig = config.default;
    }
    if (Object.prototype.hasOwnProperty.call(config, 'development') ) {
        developmentConfig = config.development;
    }
    if (Object.prototype.hasOwnProperty.call(config, 'staging') ) {
        stagingConfig = config.staging;
    }
    if (Object.prototype.hasOwnProperty.call(config, 'production') ) {
        productionConfig = config.production;
    }

    let finalConfig = {
        configMode: null,
    };
    
    let configMode = process.env.VUE_APP_CONFIG_MODE_ENV;
    
    if (!configMode) {
        throw new Error("VUE_APP_CONFIG_MODE_ENV not set to any mode!");
    }

    if (configMode === "development") {
        finalConfig = deepmerge(defaultConfig, developmentConfig);
        finalConfig.configMode = "development";
    }
    if (configMode === "staging") {
        finalConfig = deepmerge(defaultConfig, stagingConfig);
        finalConfig.configMode = "staging";
    }
    if (configMode === "production") {
        finalConfig = deepmerge(defaultConfig, productionConfig);
        finalConfig.configMode = "production";
    }

    return finalConfig;
};

export default {
    install: function (app, config) {
        app.config.globalProperties.$config = AppConfig(config);
    }
}