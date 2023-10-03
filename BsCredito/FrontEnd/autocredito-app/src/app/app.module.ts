import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CotizadorComponent } from './home/cotizador/cotizador.component';
import { VerificarCuotaComponent } from './home/verificar-cuota/verificar-cuota.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterComponent } from './shared/footer/footer.component';
import { HeaderComponent } from './shared/header/header.component';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { MaterialModule } from './material.module';
import { HomeComponent } from './home/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SolicitudDialogComponent } from './home/cotizador/solicitud-dialog/solicitud-dialog.component';
import { PagarcuotaDialogComponent } from './home/verificar-cuota/pagarcuota-dialog/pagarcuota-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    CotizadorComponent,
    VerificarCuotaComponent,
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    HomeComponent,
    SolicitudDialogComponent,
    PagarcuotaDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
