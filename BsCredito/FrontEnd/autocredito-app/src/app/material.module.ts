import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatRadioModule } from '@angular/material/radio';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatSortModule} from '@angular/material/sort';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatDialogModule} from '@angular/material/dialog';
import {MatMenuModule} from '@angular/material/menu';
import {MatSelectModule} from '@angular/material/select';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
    imports: [MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatDatepickerModule,
        MatNativeDateModule, MatExpansionModule, MatRadioModule, MatAutocompleteModule, MatToolbarModule,
        MatTooltipModule, MatSidenavModule, MatListModule,MatCardModule,
        MatTableModule, MatPaginatorModule, MatSortModule, MatSnackBarModule,MatDialogModule,
        MatMenuModule, FlexLayoutModule, MatSelectModule,MatProgressSpinnerModule, MatCheckboxModule  ],
    exports: [MatButtonModule, MatIconModule, MatIconModule, MatInputModule, MatFormFieldModule, MatDatepickerModule,
        MatNativeDateModule, MatExpansionModule, MatRadioModule, MatAutocompleteModule, MatToolbarModule,
        MatTooltipModule, MatSidenavModule, MatListModule,MatCardModule,
        MatTableModule, MatPaginatorModule, MatSortModule, MatSnackBarModule,MatDialogModule,
        MatMenuModule, FlexLayoutModule, MatSelectModule,MatProgressSpinnerModule,MatCheckboxModule ]

})

export class MaterialModule { }
