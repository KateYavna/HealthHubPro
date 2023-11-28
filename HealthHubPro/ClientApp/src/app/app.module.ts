import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard} from './guards/auth-guard';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProfileFormComponent } from './profileForm/profileForm.component';
import { PatientsComponent } from './patients/patients.component';
import { HealthcareProvidersComponent } from './healthcare-providers/healthcare-providers.component';
import { PatientUpdateComponent } from './patient-update/patient-update.component';
import { HealthcareProvidersUpdateComponent } from './healthcare-providers/healthcare-providers-update/healthcare-providers-update.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

export function tokenGetter(){
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProfileFormComponent,
    PatientsComponent,
    HealthcareProvidersComponent,
    PatientUpdateComponent,
    HealthcareProvidersUpdateComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'registration', component: ProfileFormComponent, canActivate: [AuthGuard]},
      { path: 'patients', component: PatientsComponent},
      { path: 'healthcareProviders', component: HealthcareProvidersComponent},
      { path: 'patientUpdate/:id', component: PatientUpdateComponent, canActivate: [AuthGuard]},
      { path: 'healthcareProviderUpdate/:id', component: HealthcareProvidersUpdateComponent, canActivate: [AuthGuard]},
      { path: 'login', component: FetchDataComponent },
    ]),
    JwtModule.forRoot({
      config:{
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:44495"],
        disallowedRoutes:[]
      }
    })
  ],
  
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
