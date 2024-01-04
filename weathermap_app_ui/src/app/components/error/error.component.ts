import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ErrorService } from 'src/app/services/error.service';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})
export class ErrorComponent implements OnInit, OnDestroy {
  error$: Subscription;
  errors: string[] = [];

  constructor(
    private errorService: ErrorService    
  ){}

  ngOnInit(): void {
    this.error$ = this.errorService.error$.subscribe(x => this.showError(x));
  }
  
  ngOnDestroy(): void {
    this.error$.unsubscribe();
  }

  showError(error: string){
    this.errors.push(error);
    
    setTimeout(() => {
      this.errors = this.errors.slice(1, this.errors.length-1);
    }, 3000)
  }
}
