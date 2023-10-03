import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Credito } from 'src/app/modelos/Credito';
import { CreditoService } from 'src/app/services/credito.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-solicitud-dialog',
  templateUrl: './solicitud-dialog.component.html',
  styleUrls: ['./solicitud-dialog.component.scss']
})
export class SolicitudDialogComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<SolicitudDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private creditoDialog: Credito,
    private creditoService:CreditoService) { }

  form: FormGroup;

  ngOnInit(): void {
    this.inicializarFormulario();
  }

  guardarCredito(){
    this.creditoDialog.nombresCliente =  this.form.value['nombres'];
    this.creditoDialog.apellidosCliente =  this.form.value['apellidos'];
    this.creditoDialog.identificacion =  this.form.value['identificacion'];
    this.creditoService.guardarCredito(this.creditoDialog).subscribe(response =>{
      Swal.fire("",response.toString(), 'success');
      this.onNoClick();
    },((error:HttpErrorResponse) =>{
      Swal.fire("",error.error,'error');
    }));
  }

  inicializarFormulario(){
    this.form = new FormGroup({
      'nombres': new FormControl('', [Validators.required]),
      'apellidos': new FormControl('', [Validators.required]),
      'identificacion': new FormControl('', [Validators.required]),
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
