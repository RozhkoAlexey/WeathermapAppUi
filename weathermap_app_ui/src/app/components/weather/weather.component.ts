import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WeatherApiService } from 'src/app/api-services/weather-api.service';
import { CountryCode } from '../../data/country-codes';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss']
})
export class WeatherComponent implements OnInit {
  countryCodeData = CountryCode;
  formGroup: FormGroup;

  get zipCodeControl() { return this.formGroup.get('zipCode'); }
  get countryCodeControl() { return this.formGroup.get('countryCode'); }

  constructor(
    public weatherApiService: WeatherApiService
  ) {
    this.formGroup = new FormGroup({
      zipCode: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[0-9]\d*$/)]),
      countryCode: new FormControl('',
        [Validators.required])
    });
  }

  ngOnInit(): void {
    const historySubscription = this.weatherApiService.history()
      .subscribe(_ => {historySubscription.unsubscribe()});
  }

  onClick(event: any) {
    if (this.formGroup.invalid || !this.formGroup.dirty) {
      this.zipCodeControl?.markAsTouched();
      this.countryCodeControl?.markAsTouched();

      return;
    }

    const historySubscription = this.weatherApiService.get(this.countryCodeControl?.value, this.zipCodeControl?.value)
      .subscribe(_ => {historySubscription.unsubscribe()});
  }
}
