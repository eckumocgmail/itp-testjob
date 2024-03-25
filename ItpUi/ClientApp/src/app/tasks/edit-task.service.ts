import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class EditTaskService
{
    constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient)
    {
    }

    getTask(taskId: string): Observable<any>
    {
        return this.http.get<any>(`${this.baseUrl}api/EditTask`,{
            params: {
                taskId: taskId
            }
        });
    }

    updateTask(taskId, taskName, projectId, startTime, endTime, taskDescription ): Observable<any>
    {
        return this.http.patch<any>(`${this.baseUrl}api/EditTask`,{            
            taskId: taskId,
            taskName: taskName,
            projectId: projectId,
            startTime: startTime,
            endTime: endTime,
            taskDescription: taskDescription           
        });
    }

    downloadFile(commentId)
    {
        return this.http.get<any>(`${this.baseUrl}api/EditTask/DownloadFile`, {            
            params: {
                commentId: commentId
            }
        });    
    }
    
    postFileComment(taskId: any, commentType: string, content: any): Observable<any>
    {        
        return this.http.post<any>(`${this.baseUrl}api/EditTask/PostFileComment`,content,{
            params: {
                taskId: taskId,
                commentType: commentType    
            }
        });
    }

    postTextComment(taskId: any, commentType: string, content: string): Observable<any>
    {        
        return this.http.post<any>(`${this.baseUrl}api/EditTask/PostTextComment`,{
            taskId: taskId,
            commentType: commentType,
            content: content
        });
    }

    deleteComment(commentId: string): Observable<any>
    {        
        return this.http.delete<any>(`${this.baseUrl}api/EditTask`,{
            params: {
                commentId: commentId
            }
        });
    }    
}