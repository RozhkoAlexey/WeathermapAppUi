import { Pipe, PipeTransform } from '@angular/core';
import { IWeatherModel } from '../models/weather.model';

@Pipe({
  name: 'weatherDescription'
})
export class WeatherDescriptionPipe implements PipeTransform {
  transform(value: IWeatherModel[], ...args: unknown[]): string  {
    return value?.length !== 0
      ? this.ucFirst(value[0].description)
      : '';
  }

  ucFirst(str: string): string {
    return !str
      ? str
      : str[0].toUpperCase() + str.slice(1)
  }
}
