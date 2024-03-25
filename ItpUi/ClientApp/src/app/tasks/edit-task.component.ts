import {Component, OnInit} from '@angular/core';
import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators
} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { AddCommentComponent } from './add-comment.component';
import { EditTaskService } from './edit-task.service';
import { DomSanitizer } from '@angular/platform-browser';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    <h2>Edit task</h2>
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
          <mat-option (click)="onProjectUnselected()">None</mat-option>
          <mat-option [option]="project.projectId" 
          [value]="project.id" *ngFor="let project of projects" 
          (click)="onProjectSelected()">{{project.projectName}}</mat-option>
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
    <button mat-raised-button [disabled]="!isStartTimeValid() || !taskNameFormControl.value || !projectIdFormControl.value || !startTimeFormControl.value" color="primary" (click)="onSaveButtonClicked()">save</button>
    <button mat-raised-button (click)="onAddCommentButtonClicked()">add</button>
    <mat-list>
      <mat-list-item *ngFor="let comment of taskComments">
        <mat-icon (click)="onDeleteCommentClicked(comment.id)">delete</mat-icon>
        <span *ngIf="comment.commentType == 1">          
          {{comment.content}}
        </span>
        <span *ngIf="comment.commentType == 2">                                      
          file item
        </span>
      </mat-list-item>
    </mat-list>   
  </div>`})
export class EditTaskComponent implements OnInit
{
  taskNameFormControl = new FormControl('', [Validators.required]);
  projectIdFormControl = new FormControl('', [Validators.required]);
  startTimeFormControl = new FormControl('', []);
  endTimeFormControl = new FormControl('', []);
  taskDescriptionFormControl = new FormControl('', []);

  matcher = new MyErrorStateMatcher();  

  taskId: object = null;
  projects = [];
  taskComments = [];

  fileUrl = null;

  constructor(private snackBar: MatSnackBar, private dialog: MatDialog, private route: ActivatedRoute, private editTaskService: EditTaskService)
  {
  }
  
  ngOnInit(): void 
  {    
    this.route.params.subscribe(params => {     
      this.taskId = params['id'];          
      this.editTaskService.getTask(params['id'])
        .subscribe(result => {
          
          this.projects = result.projects;          
          this.taskNameFormControl.setValue(result.taskName);
          this.projectIdFormControl.setValue(result.projectId);
          this.startTimeFormControl.setValue(result.startTime);
          this.endTimeFormControl.setValue(result.endTime);
          this.taskDescriptionFormControl.setValue(result.taskDescription);
                  
          this.taskComments = result.taskComments;  

      }, err => console.error(err));    
    });
  }
   
  onSaveButtonClicked()
  {
    this.editTaskService.updateTask(
      this.taskId,
      this.taskNameFormControl.value,
      this.projectIdFormControl.value,
      this.startTimeFormControl.value,
      this.endTimeFormControl.value,
      this.taskDescriptionFormControl.value
    ).subscribe(result =>{
      console.log(result);
      this.snackBar.open('Data updated success', 'Undo', {
        duration: 1000
      });      
    },err => console.error(err));
  }

  onAddCommentButtonClicked()
  {
    let dialogref = this.dialog.open(AddCommentComponent,{    
      data: {},
      width: '60%'
    });
    dialogref.afterClosed().subscribe(dialogResult =>{      
      if(dialogResult.completed)
      {      
        switch(dialogResult.data.commentType)
        {
          case 1: 
          {
            this.editTaskService.postTextComment(this.taskId, dialogResult.data.commentType, dialogResult.data.content)
            .subscribe(result => {
              console.log(result);
              this.taskComments.push({
                id: result.id,
                commentType: dialogResult.data.commentType,
                content: dialogResult.data.content,
                taskId: this.taskId
              });
              this.snackBar.open('Text comment added success', 'Undo', {
                duration: 1000
              });  
            },err => console.error(err));          
            break;    
          }
          default: 
          {
            this.editTaskService.postFileComment(this.taskId, dialogResult.data.commentType, dialogResult.data.content)
            .subscribe(result => {              
              this.taskComments.push({
                id: result.id,
                commentType: dialogResult.data.commentType,
                content: dialogResult.data.content,
                taskId: this.taskId
              });
              this.snackBar.open('File added success', 'Undo', {
                duration: 1000
              });
            },err => console.error(err));   
            break;
          }
        }    
      }
    });
  }  

  onDeleteCommentClicked( id )
  {    
    this.editTaskService.deleteComment(id)
      .subscribe(result => {
        
        if(result==1)
        {
          this.taskComments = this.taskComments.filter( item => item.id != id);
          this.snackBar.open('Comment removed success', 'Undo', {
            duration: 1000
          });
        }

      },err => console.error(err));    
  }

  onProjectUnselected()
  {
  }

  onProjectSelected()
  {
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
