import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PreloaderService } from 'src/app/services/preloader.service';

@Component({
  selector: 'app-preloader',
  templateUrl: './preloader.component.html',
  styleUrls: ['./preloader.component.scss']
})
export class PreloaderComponent implements OnInit, OnDestroy{
  loading = false;
  loadingSubscription: Subscription

  constructor(
    private preloaderService: PreloaderService
  ){}
  
  ngOnDestroy(): void {
    this.loadingSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.loadingSubscription = this.preloaderService.loading$.subscribe(x => {
      this.loading = x
    })
  }
}
