import { Component, Input } from '@angular/core';
import { WeatherApiService } from 'src/app/api-services/weather-api.service';
import { IHistoryQueryModel } from 'src/app/models/history-query.model';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent {
  @Input() history: IHistoryQueryModel;

  constructor(public weatherApiService: WeatherApiService) {
    
  }

  onClose(id: string){
    const removeSubscription = this.weatherApiService.remove(id)
      .subscribe(_ => {removeSubscription.unsubscribe()})
  }
}
