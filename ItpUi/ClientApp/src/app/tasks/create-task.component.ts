import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Component, Inject } from '@angular/core';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatDialogRef } from '@angular/material/dialog';

import { ErrorStateMatcher } from '@angular/material/core';
import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
  ValidationErrors,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms'; 

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-create-task',
  template: `
   
<div style="padding: 20px;">
    <h2>Create task</h2>
    <div style="padding: 20px;" *ngIf="!taskNameFormControl.value || !projectIdFormControl.value || !startTimeFormControl.value || !isStartTimeValid()">
      <div *ngIf="!taskNameFormControl.value">Please enter task name</div>
      <div *ngIf="!projectIdFormControl.value">Please select project</div>
      <div *ngIf="!startTimeFormControl.value">Please enter start time</div>
      <div *ngIf="!isStartTimeValid()">Start time must be less than end time </div>      
    </div>
    <form class="example-form">
      <mat-form-field style="width: 50%;">
        <mat-label>Task name</mat-label>
        <input matInput [formControl]="taskNameFormControl" [errorStateMatcher]="matcher">            
        <mat-error *ngIf="taskNameFormControl.hasError('required')">Please enter a task name</mat-error>                      
      </mat-form-field>
      <mat-form-field style="width: 50%;">
        <mat-label>Select project</mat-label>       
        <mat-select [formControl]="projectIdFormControl">
          <mat-option>None</mat-option>
          <mat-option [option]="project.projectId" 
          [value]="project.id" *ngFor="let project of projects" 
          >{{project.projectName}}</mat-option>
        </mat-select>
        <mat-error>Please choose a project</mat-error>
      </mat-form-field> 
      <mat-form-field style="width: 50%;">
        <mat-label>Start time</mat-label>
        <input matInput type="time" [formControl]="startTimeFormControl">
        <mat-error>Please enter a start time</mat-error>
      </mat-form-field>

      <mat-form-field style="width: 50%;">
        <mat-label>End time</mat-label>
        <input matInput type="time" [formControl]="endTimeFormControl">
        <mat-error>Please enter a end time</mat-error>
      </mat-form-field>

      <mat-form-field style="width: 100%;">
        <mat-label>Task description</mat-label>
        <textarea matInput [formControl]="taskDescriptionFormControl"></textarea>
        <mat-error>Please enter a task description</mat-error>
      </mat-form-field>
    </form>
        
    <button mat-raised-button [disabled]="!taskNameFormControl.value || !projectIdFormControl.value || !startTimeFormControl.value || !isStartTimeValid()" color="primary" (click)="onCreateButtonClicked()">create</button>
    <button mat-raised-button (click)="onCancelButtonClicked()">cancel</button>
</div>
  `
})
export class CreateTaskComponent {

  taskNameFormControl = new FormControl('', [Validators.required]);
  projectIdFormControl = new FormControl('', [Validators.required]);
  startTimeFormControl = new FormControl('', [ ]);
  endTimeFormControl = new FormControl('', [ ]);
  taskDescriptionFormControl = new FormControl('', []);
  matcher = new MyErrorStateMatcher();

  projects = [];
      
  constructor(private dialogRef: MatDialogRef<CreateTaskComponent>, @Inject(MAT_DIALOG_DATA) public data: {projects: any[]} )
  {
      this.projects = data.projects;      
  }

  onCancelButtonClicked(){
    this.dialogRef.close({
      completed: false
    });
  }  

  onCreateButtonClicked(){    
    this.dialogRef.close({
      completed: true,
      data: {
        taskName: this.taskNameFormControl.value,
        projectId: this.projectIdFormControl.value,
        startTime: this.startTimeFormControl.value,
        endTime: this.endTimeFormControl.value,
        taskDescription: this.taskDescriptionFormControl.value
      }
    });
  }

  isStartTimeValid()
  {
    let start = this.startTimeFormControl.value;
    if(!start || start.length!=5)
      return false;
    let startValue = parseInt(start.substring(3))+(parseInt(start.substring(0,2))*60);
    
    let end = this.endTimeFormControl.value;
    if(!end || end.length!=5)
      return true;
    let endValue = parseInt(end.substring(3))+(parseInt(end.substring(0,2))*60);
    
    return endValue >= startValue;
  }
 
}