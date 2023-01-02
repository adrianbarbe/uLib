import {createRouter, createWebHistory} from 'vue-router';
import UserRootView from "@/views/UserRootView";
import NotFoundView from "@/views/NotFoundView";
import SignUp from "@/components/auth/SignUp";
import AuthRootView from "@/views/AuthRootView";
import AuthRedirectSignUp from "@/components/auth/AuthRedirectSignUp";

const routes = [
    { 
        path: '/', 
        component: UserRootView
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
                path: "redirect-sign-up",
                name: "redirect-sign-up",
                component: AuthRedirectSignUp,
            }
        ],
    },
    { path: '/404', component: NotFoundView },
    { path: '/:pathMatch(.*)*', redirect: '/404' },
]

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;