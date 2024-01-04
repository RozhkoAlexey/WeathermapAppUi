import { Pipe, PipeTransform } from '@angular/core';
import { IWeatherModel } from '../models/weather.model';

@Pipe({
  name: 'formatIcon'
})
export class FormatIconPipe implements PipeTransform {

  transform(value: IWeatherModel[], ...args: unknown[]): string {
    if (value?.length === 0)
      return '';

    const icon = value[0].icon;

    return `data:image/png;base64, ${icon}`;
  }
}
