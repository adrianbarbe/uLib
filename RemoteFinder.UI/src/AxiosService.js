import axios from "axios";
import ErrorHandlingService from "./ErrorHandlingService";
import config from "./app.config";
import {AppConfig} from "./config/configFactory";
import store from "./store";
import deepmerge from "deepmerge";

let configuration = AppConfig(config);

export default class AxiosService {
    static instance = null;

    static getInstance = (postAdditionalHeaders = {}, getAdditionalHeaders = {}, withoutSnackMessage = false) => {
        let headers = {};
        let acessToken = localStorage.getItem('access_token');
        if (acessToken !== undefined) {
            headers.Authorization = `Bearer ${acessToken}`
        }

        AxiosService.instance = axios.create({
            baseURL: configuration.apiEndpoint,
            validateStatus: status => status < 500,
            headers: headers
        });
        AxiosService.instance.interceptors.request.use(config => {
            const getHeaders = {
                "Cache-Control": "no-cache",
                "Pragma": "no-cache",
                "Content-Type": "application/json"
            };
            
            const postHeaders = {
                "Accept": "application/json",
                "Content-Type": "application/json",
            };
            
            
            config.headers = deepmerge(getHeaders, getAdditionalHeaders);
            config.headers = deepmerge(postHeaders, postAdditionalHeaders);

            store.commit("loadingState/setLoading");
            
            return config;
        });
        AxiosService.instance.interceptors.response.use(response => {
                store.commit("loadingState/setLoaded");

                return ErrorHandlingService.prodcessReponse(response, withoutSnackMessage);
            },
            error => {
                store.commit("loadingState/setLoaded");
                store.commit("snackMessages/set", {message: error.toString(), color: 'error'});
                return error;
            });
        
        return AxiosService.instance;
    }
}
