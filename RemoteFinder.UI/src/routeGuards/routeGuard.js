export default (to, from, next) => (app) => {
    if (!app.config.globalProperties.$auth.isAuthenticated()) {
        next({name: 'sign-up'});
    }
    next();
}