import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { MainNavComponent } from './main-nav/main-nav.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
    {
        path: '', component: MainNavComponent,
        canActivate: [AuthGuard],
        children: [
            {
                path: '', 
                redirectTo: '/home', 
                pathMatch: 'full'
            },
            {
                path: 'home', 
                loadChildren: () => import('./modules/home/home.module')
                .then(m => m.HomeModule),
            }
        ]
    },
    {
        path: 'login', 
        component: LoginComponent
    },
    {
        path: 'register', 
        component: RegisterComponent
    },
    {
        path: '**', 
        pathMatch: 'full',
        component: NotFoundComponent
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
