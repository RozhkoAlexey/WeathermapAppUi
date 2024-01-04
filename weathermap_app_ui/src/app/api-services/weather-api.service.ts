import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap, throwError, catchError, defer, finalize } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IHistoryQueryModel } from '../models/history-query.model';
import { ErrorService } from '../services/error.service';
import { PreloaderService } from '../services/preloader.service';

@Injectable({
  providedIn: 'root'
})
export class WeatherApiService {
  private _route = `${environment.apiUrl}Weather`;

  public histories: IHistoryQueryModel[] = [];

  constructor(
    private _http: HttpClient,
    private errorService: ErrorService,
    private preloaderService: PreloaderService
  ) { }

  public get(countryCode: string, zipCode: string): Observable<IHistoryQueryModel> {
    return defer<Observable<IHistoryQueryModel>>(() => {
      this.preloaderService.handle()

      return this._http.get<IHistoryQueryModel>(`${this._route}`, {
        params: new HttpParams({
          fromObject: {
            countryCode,
            zipCode
          }
        })
      }).pipe(
        tap(x => this.histories?.unshift(x) ?? [x]),
        catchError(this.errorHandler.bind(this)),
        finalize(() => this.preloaderService.clear())
      );
    })
  }

  public history(): Observable<IHistoryQueryModel[]> {
    return defer<Observable<IHistoryQueryModel[]>>(() => {
      this.preloaderService.handle()

      return this._http.get<IHistoryQueryModel[]>(`${this._route}/history`)
        .pipe(
          tap(x => this.histories = x),
          catchError(this.errorHandler.bind(this)),
          finalize(() => this.preloaderService.clear())
        );
    })
  }

  public remove(id: string): Observable<string> {
    return defer<Observable<string>>(() => {
      this.preloaderService.handle()
      
      return this._http.delete<string>(`${this._route}`,{
        params: new HttpParams({
          fromObject: {
            id
          }
        })
      })
      .pipe(
        tap(x => this.histories = this.histories.filter(x => x.id !== id)),
        catchError(this.errorHandler.bind(this)),
        finalize(() => this.preloaderService.clear())
      );
    });
  }

  private errorHandler(httpError: HttpErrorResponse) {
    this.errorService.handle(httpError.error.message)
    return throwError(() => httpError.message)
  }
}
