import { Injectable } from '@angular/core';
import {Subject} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class PreloaderService {
  public loading$ = new Subject<boolean>()

  handle() {
    this.loading$.next(true)
  }

  clear() {
    this.loading$.next(false)
  }
}
