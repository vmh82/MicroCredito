import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CotizadorComponent } from './home/cotizador/cotizador.component';
import { HomeComponent } from './home/home/home.component';
import { VerificarCuotaComponent } from './home/verificar-cuota/verificar-cuota.component';

const routes: Routes = [
  {path:'home', component:HomeComponent,
  children: [
    {path:'cotizar', component:CotizadorComponent},
     {path:'verificarcuota', component:VerificarCuotaComponent}
    ]},
    { path: '', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
