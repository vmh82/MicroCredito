import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Credito } from '../modelos/Credito';
import { Parametros } from '../modelos/Parametros';
import { TablaAmortizacion } from '../modelos/TablaAmortizacion';

@Injectable({
  providedIn: 'root'
})
export class CreditoService {

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  }

  url: string = `${environment.HOST}`;

  constructor(private http:HttpClient) { }

  consultarParametrosCredito(){
    const consultarParametros = `${this.url}/credito/ConsultarParametros`
    return this.http.get<Parametros>(consultarParametros, this.httpOptions)
  }

  consultarTablaAmortizacion(credito:Credito){
    const consultarTablaAmortizacion = `${this.url}/credito/ConsultarTablaAmortizacion`
    return this.http.post<any>(consultarTablaAmortizacion, credito, this.httpOptions)
  }

  guardarCredito(credito:Credito){
    const guardarCredito = `${this.url}/credito/GuardarCredito`
    return this.http.post(guardarCredito, credito, {responseType: 'text'})
  }

  consultarCuotaPendiente(identificacion:string){
    const consultarTablaAmortizacion = `${this.url}/credito/consultarCuotaPendiente`
    return this.http.post<any>(consultarTablaAmortizacion, JSON.stringify(identificacion), this.httpOptions)
  }

  guardarCuota(couta:TablaAmortizacion){
    const guardarCuota = `${this.url}/credito/PagarCuotaCredito`
    return this.http.post(guardarCuota, couta, {responseType: 'text'})
  }

}
