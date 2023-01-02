import store from "./store";
import router from "./router";

export default class ErrorHandlingService {
    static prodcessReponse(response, withoutSnackMessage) {        
        const status = response.status;
       
        switch (status) {
            case 200:
            case 204:
                if (response.config.method === "post" && !withoutSnackMessage) {
                    store.commit("snackMessages/set", {message: "Successfully saved", color: 'success'});
                }
                
                return response.data;
            case 400:
                if (response.data.errorMessage !== undefined) {
                    store.commit("snackMessages/set", {message: response.data.errorMessage, color: 'error'});

                    throw response.data;
                }
                else {
                    throw response.data;
                }
            case 401:
                router.push("/NotAuthorized");
                return;
            case 403:
                store.commit("snackMessages/set", {message: response.data.errorMessage, color: 'warning'});

                return;
            case 404:
                router.push("/NotFound");
                return;
            case 500:
                store.commit("snackMessages/set", {message: response.data.errorMessage, color: 'warning'});

                return;
            default:
                store.commit("snackMessages/set", {message: response.data.errorMessage, color: 'warning'});

                return;
        }
    }
}
