import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';

import { MatDatepicker } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';

import { CreateTaskComponent } from './create-task.component';
import { TasksListService } from './tasks-list.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html' 
})
export class TasksListComponent implements OnInit{

  tasks: Task[] = null;
  selectedProject = '';
  projects = [ ];
  dateFilter = null;
  total = 0;
  loaded = false;
  
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;

  constructor(private snackBar: MatSnackBar, private tasksListService: TasksListService, private dialog: MatDialog) 
  {        
  }

  ngOnInit(): void 
  {
    this.tasksListService.getTasks(this.selectedProject, this.getDateParameter())
      .subscribe(result =>{
        
        console.log(result);
        this.projects = result.projects;
        this.tasks = result.tasks;
        this.total = 0;
        this.tasks.forEach( task => { this.total+=parseInt(task.time); });
        this.loaded = true;
  
    }, error => console.error(error));
  }

  openCreateTaskDialog()
  {
    const dialogRef = this.dialog.open(CreateTaskComponent, {
      data: {
        projects: this.projects
      },
    }); 
    dialogRef.afterClosed().subscribe(dialogResult=>{

      if(dialogResult.completed)
      {
        this.tasksListService.postTask(dialogResult.data.taskName, dialogResult.data.projectId, dialogResult.data.startTime, dialogResult.data.endTime, dialogResult.data.taskDescription)
          .subscribe(result => {
            
            result.projectName = this.projects.filter( p => p.id == dialogResult.data.projectId)[0].projectName;
            result['position'] = this.tasks.length+1;
            
            this.tasks.push(Object.assign(new Task(), result));
            this.total = 0;
            this.tasks.forEach( task => { this.total+=parseInt(task.time); });
            
            this.snackBar.open('Task created success', 'Undo', {
              duration: 1000
            });

        }, err => console.error(err));
      }

    });  
  }
 
  onProjectUnselected( )
  {
    this.selectedProject = '';
    this.tasksListService.getTasks(this.selectedProject, this.getDateParameter())
      .subscribe(result =>{
        
        this.tasks = result.tasks;
        //this.total = result.total;
        this.total = 0;
        this.tasks.forEach( task => { this.total+=parseInt(task.time); });        
  
    }, error => console.error(error));
  }

  onProjectSelected( )
  {
    this.tasksListService.getTasks(this.selectedProject, this.getDateParameter())
      .subscribe(result =>{
        
        this.tasks = result.tasks;
        //this.total = result.total;
        this.total = 0;
        this.tasks.forEach( task => { this.total+=parseInt(task.time); });        

    }, error => console.error(error)); 
  }

  onProjectDateChanged( evt )
  {   
    this.dateFilter = evt;  
    this.tasksListService.getTasks(this.selectedProject, this.getDateParameter())
      .subscribe(result =>{
        
        this.tasks = result.tasks;
        //this.total = result.total;
        this.total = 0;
        this.tasks.forEach( task => { this.total+=parseInt(task.time); });        

    }, error => console.error(error));     
  }

  getDateParameter()
  {
    return this.dateFilter? this.toDateString(this.dateFilter): '0';
  }

  // форматирование 
  formatTime(seconds: number)
  {    
    seconds -= seconds % 60;
    let minutesCount = (seconds / 60);
    let minutes = minutesCount % 60;
    minutesCount -= minutes;
    let hoursCount = (minutesCount / 60);
    let hours = hoursCount%24;
    /*let seconds = time%(60*60*24);
    let hours = (seconds-(seconds%(60*60)))/(60*60);
    seconds -= hours*60*60;
    let minutes = (seconds-(seconds%60))/60;*/
    return `${hours<10?('0'+hours):hours}:${minutes<10?('0'+minutes):minutes}`;
  }

  // форматирование 
  formatTime2(time: number)
  {    
    return new Date(time);
  }
  
  toDateString( d: Date )
  {    
    let datestr =
      (d.getDate()<10?'0'+d.getDate(): d.getDate())+'.'+
      (d.getMonth()<10?'0'+(d.getMonth()+1): (d.getMonth()+1))+'.'+
      (d.getFullYear());
    return datestr;
  }
}

class Task {
  position: number = 1;
  projectId: string = 'projectId';  
  projectName: string = 'projectName';  
  time: string = 'time';  
  taskId: string = 'taskId';  
  taskName: string = 'taskName';  
  startTime: string = 'startTime';  
  endTime: string = 'endTime';   
}
