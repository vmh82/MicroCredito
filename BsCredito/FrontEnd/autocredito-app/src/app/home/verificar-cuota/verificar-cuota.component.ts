import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { TablaAmortizacion } from 'src/app/modelos/TablaAmortizacion';
import { CreditoService } from 'src/app/services/credito.service';
import { PagarcuotaDialogComponent } from './pagarcuota-dialog/pagarcuota-dialog.component';

@Component({
  selector: 'app-verificar-cuota',
  templateUrl: './verificar-cuota.component.html',
  styleUrls: ['./verificar-cuota.component.scss']
})
export class VerificarCuotaComponent implements OnInit {

  inputIdentificacion  = new FormControl('');
  identificacion :string;
  displayedColumns = ['numeroCuota', 'fechaPago', 'capital', 'interes', 'cuotaTotal', 'saldo', 'diasVencidos', 'mora', 'visualizar'];
  dataSource: MatTableDataSource<TablaAmortizacion>;
  constructor(public dialog:MatDialog, private creditoService:CreditoService) { }

  ngOnInit(): void {

  }

  consultarCuotaPendiente(){
    this.identificacion = this.inputIdentificacion.value;
    this.creditoService.consultarCuotaPendiente(this.identificacion).subscribe(data=>{
      this.dataSource = new MatTableDataSource(data);
    });
  }

  abrirDialogCuota(row:TablaAmortizacion){
    console.log(row);
    const dialogRef = this.dialog.open(PagarcuotaDialogComponent,{
      width: '750px',
      height: '600px',
      data:row,
      disableClose: true
    }) .afterClosed().pipe().subscribe(()=>{
      this.consultarCuotaPendiente();
    });

  }

  limpiarConsulta(){
    this.inputIdentificacion.patchValue('');
    this.dataSource  = new MatTableDataSource([]);
  }

}
