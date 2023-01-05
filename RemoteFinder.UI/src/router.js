import {createRouter, createWebHistory} from 'vue-router';
import UserRootView from "@/views/UserRootView";
import NotFoundView from "@/views/NotFoundView";
import SignUp from "@/components/auth/SignUp";
import AuthRootView from "@/views/AuthRootView";
import AuthRedirect from "@/components/auth/AuthRedirect";
import routeGuard from "@/routeGuards/routeGuard";

const createRoutes = (app) => [
    {
        path: '/',
        name: "dashboard",
        component: UserRootView,
        beforeEnter: (to, from, next) => routeGuard(to, from, next)(app),
    },
    {
        path: '/auth',
        component: AuthRootView,
        children: [
            {
                path: "sign-up",
                name: "sign-up",
                component: SignUp,
            },
            {
                path: "redirect",
                name: "redirect",
                component: AuthRedirect,
            }
        ],
    },
    {path: '/404', component: NotFoundView},
    {path: '/:pathMatch(.*)*', redirect: '/404'},
]

const router = (app) => {
    return createRouter({
        history: createWebHistory(),
        routes: createRoutes(app)
    });
};

export default router;