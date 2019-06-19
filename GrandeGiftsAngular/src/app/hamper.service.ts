import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import Hamper from './hamper'

@Injectable({
  providedIn: 'root'
})
export class HamperService {
  constructor(private http: HttpClient) { }

  getHampers() {
    return this.http.get<Hamper[]>('https://grandegiftsryanvandenberg.azurewebsites.net/api/hampers');
  }

  filterHampers(keyword: string) {
    if (keyword == "") {
      return this.getHampers();
    }
    return this.http.get<Hamper[]>('https://grandegiftsryanvandenberg.azurewebsites.net/api/hampers/' + keyword );
  } 
}
