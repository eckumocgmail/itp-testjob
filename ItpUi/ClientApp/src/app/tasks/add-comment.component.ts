import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-add-comment',
    template: `   
<div style="padding: 20px;">
    <h2>Add comment</h2>
    <mat-radio-group (change)="onCommentTypeChanged($event)">
        <mat-radio-button value="1" checked="true">Text</mat-radio-button>
        <mat-radio-button value="2">File</mat-radio-button>
    </mat-radio-group>
    <form class="example-form">
        <mat-form-field style="width: 100%;" *ngIf="commentType==1">
            <mat-label>Text</mat-label>
            <textarea matInput [formControl]="textareaFormControl"></textarea>
            <mat-error>Please enter a text content</mat-error>
        </mat-form-field>
        <div style="width: 100%; padding: 20px;" *ngIf="commentType==2">
            <input class="form-control" type="file" #fileInput/>
        </div>
    </form>        
    <button mat-raised-button color="primary" (click)="onCreateButtonClicked()">submit</button>
    <button mat-raised-button (click)="onCancelButtonClicked()">cancel</button>
</div>`})
export class AddCommentComponent
{
    textareaFormControl = new FormControl('', [Validators.required]);
    fileFormControl = new FormControl('', [Validators.required]);

    commentType = 1;

    @ViewChild('fileInput', {static: false}) fileInput: ElementRef<HTMLInputElement>;
    
    constructor(private dialogRef: MatDialogRef<AddCommentComponent>)
    {
    }

    onCreateButtonClicked()
    {
        let content = null;
        switch(this.commentType)
        {
            case 1: 
            {                
                content = this.textareaFormControl.value;
                break;
            }         
            default: 
            {                
                const formData = new FormData();
                const file = this.fileInput.nativeElement.files[0];
                formData.append(file.name, file);
                content = formData;
                break;
            }
        }
        this.dialogRef.close({
            completed: true,
            data: {
                commentType: this.commentType,
                content: content                
            }
        });
    }
    
    onCancelButtonClicked()
    {
        this.dialogRef.close({
            completed: false
        });    
    }

    onCommentTypeChanged(evt)
    {        
        this.commentType = evt.value;
    }
}