import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class TasksListService
{
    constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient)
    {
    }

    getTasks(projectId: string, projectDate: string): Observable<any>
    {
        return this.http.get<any>(`${this.baseUrl}api/TasksList`,{
            params: {
                projectId: projectId,
                projectDate: projectDate
            }
        });
    }

    postTask(taskName: string, projectId: string, startTime: string, endTime: string, taskDescription: string): Observable<any>
    {        
        return this.http.post<any>(`${this.baseUrl}api/TasksList`,{
            taskName: taskName,
            projectId: projectId,
            startTime: startTime,
            endTime: endTime,
            taskDescription: taskDescription,
        });
    }
}