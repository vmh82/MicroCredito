import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TablaAmortizacion } from 'src/app/modelos/TablaAmortizacion';
import { CreditoService } from 'src/app/services/credito.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-pagarcuota-dialog',
  templateUrl: './pagarcuota-dialog.component.html',
  styleUrls: ['./pagarcuota-dialog.component.scss']
})
export class PagarcuotaDialogComponent implements OnInit {

  form: FormGroup;
  constructor(public dialogRef: MatDialogRef<PagarcuotaDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private cuotaDialog: TablaAmortizacion,
    private creditoService:CreditoService) { }
  fechaVencimiento:any;
  valorCuota:any;
  ngOnInit(): void {
    this.inicializarFormulario();
  }



  inicializarFormulario(){
    this.form = new FormGroup({
      'valorCuota': new FormControl(this.cuotaDialog.cuotaTotal, [Validators.required]),
      'fechaVencimiento': new FormControl(this.cuotaDialog.fechaPago, [Validators.required]),
      'montoPago': new FormControl('', [Validators.required]),
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  guardarCuota(){
    this.cuotaDialog.montoPago = this.form.value['montoPago'];
    this.creditoService.guardarCuota(this.cuotaDialog).subscribe(response =>{
      Swal.fire("",response.toString(), 'success');
      this.onNoClick();
    },((error:HttpErrorResponse) =>{
      Swal.fire("",error.error,'error');
    }));
  }

}
