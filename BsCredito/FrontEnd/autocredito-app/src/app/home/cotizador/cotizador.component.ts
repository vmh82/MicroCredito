import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AnonymousSubject } from 'rxjs/internal/Subject';
import { Credito } from 'src/app/modelos/Credito';
import { Parametros } from 'src/app/modelos/Parametros';
import { TablaAmortizacion } from 'src/app/modelos/TablaAmortizacion';
import { CreditoService } from 'src/app/services/credito.service';
import { SolicitudDialogComponent } from './solicitud-dialog/solicitud-dialog.component';

@Component({
  selector: 'app-cotizador',
  templateUrl: './cotizador.component.html',
  styleUrls: ['./cotizador.component.scss']
})
export class CotizadorComponent implements OnInit {

  form: FormGroup;
  displayedColumns = ['numeroCuota', 'fechaPago', 'capital', 'interes', 'cuotaTotal', 'saldo' ];
  dataSource: MatTableDataSource<TablaAmortizacion>;
  constructor(public dialog:MatDialog,private creditoService:CreditoService) {
    this.inicializarFormulario();
   }
   plazo : [];
   interes:any;
   desgravamen:any;
   idPlazoSel:number;
   esInactivo:boolean;

  ngOnInit(): void {
    this.consultarParametrosCredito();
    this.esInactivo = true;
  }



  inicializarFormulario(){
    this.form = new FormGroup({
      'nombres': new FormControl('', [Validators.required]),
      'apellidos': new FormControl('', [Validators.required]),
      'monto': new FormControl('', [Validators.min(300), Validators.max(2000), Validators.required]),
      'interes': new FormControl(),
      'plazo': new FormControl(null, [Validators.required]),
    });
  }


  consultarParametrosCredito(){
    this.creditoService.consultarParametrosCredito().subscribe(data =>{
      this.plazo = data.plazo;
      this.interes = data.interes;
    });
  }

  cotizarCredito(){
    let credito = new Credito();
    credito.montoSolicitado = this.form.value['monto'];
    credito.plazo = this.idPlazoSel;
    this.creditoService.consultarTablaAmortizacion(credito).subscribe((data)=>{
      if(data.length > 0){
        this.dataSource = new MatTableDataSource(data);
        this.esInactivo = false;
      }else{
        this.dataSource = new MatTableDataSource([]);
        this.esInactivo = true;
      }
    });
  }

  abrirDialogSolicitud(){
    let creditoDialog = new Credito();
    console.log(this.form.value['monto']);
    creditoDialog.montoSolicitado = this.form.value['monto'];
    creditoDialog.plazo = this.idPlazoSel;
    const dialogRef = this.dialog.open(SolicitudDialogComponent,{
      width: '750px',
      height: '600px',
      data:creditoDialog,
      disableClose: true
    }) .afterClosed().pipe().subscribe(()=>{
        this.limpiarCotizacion();
    });
  }

  limpiarCotizacion(){
    this.dataSource = new MatTableDataSource([]);
    this.form.controls["monto"].reset('');
    this.form.controls["plazo"].reset('');
    this.esInactivo = true;
  }

}
