import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
//import { provideForms } from '@angular/forms';

import { AppComponent } from './app/app.component';
import { appRoutes } from './app/app.routes';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),  // si usas routing
    provideHttpClient()       // PROVEER HttpClient para inyección
    //provideForms()             // FormsModule y ReactiveFormsModule
  ]
});//.catch(err => console.error(err));