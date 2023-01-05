import config from "../app.config";
import {AppConfig} from "../config/configFactory";
import jwtDecode from "jwt-decode";
import intersectionWith from "lodash/intersectionWith";

const configuration = AppConfig(config);

const auth = (app) => {
    const login = (idToken, refreshToken) => {
        localStorage.setItem('id_token', idToken);
        localStorage.setItem('refresh_token', refreshToken);
    };
    
    const logout = () => {
        localStorage.removeItem('id_token');
        localStorage.removeItem('refresh_token');
    };
    
    const isAuthenticated = () => {
        const idToken = localStorage.getItem('id_token');
        if (idToken === null || idToken === "undefined") {
            return false;
        }
        
        const decoded = jwtDecode(idToken);
        
        if (decoded === null) {
            return false;
        }
        
        return (new Date().getTime() / 1000) < decoded.exp;
    };
    
    const getUsername = () => {
        const idToken = localStorage.getItem('id_token');
        if (idToken === null || idToken === "undefined") {
            return false;
        }
        
        const decoded = jwtDecode(idToken);

        return (decoded !== null && decoded.unique_name);
    };
    
    const getRoles = () => {
        const idToken = localStorage.getItem('id_token');
        if (idToken === null || idToken === "undefined") {
            return false;
        }
        
        const decoded = jwtDecode(idToken);

        if (decoded !== null) {
            return decoded.role;
        }

        return [];
    };
    
    const hasRoles = (rolesStr) => {
        let role = typeof rolesStr === "string" ? [rolesStr] : rolesStr;
        
        const idToken = localStorage.getItem('id_token');
        return intersectionWith(role, idToken.roles, (a, b) => {
            return a === b;
        }).length > 0;
    };
    
    const getIdToken = () => {
        return localStorage.getItem('id_token');
    };
    
    const getRefreshToken = () => {
        return localStorage.getItem('refresh_token');
    }
    
    return {
        login,
        logout,
        isAuthenticated,
        getUsername,
    };
};

export default {
    install: function (app) {
        app.config.globalProperties.$auth = auth(app);
    }
}