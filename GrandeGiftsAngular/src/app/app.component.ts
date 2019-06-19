import { Component } from '@angular/core';
import Hamper from './hamper';
import { HamperService } from './hamper.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'GrandeGiftsAngular';
  data: Hamper[] = [];
  keyword: string = "";
  service: HamperService;
  searchWord: string = "";
  constructor(private hamperService: HamperService) {
    this.service = hamperService;
    this.service.getHampers().subscribe((data) => this.data = data)
  }

  getString(val: object) {
    return JSON.stringify(val);
  }

  onSearch() {
    this.searchWord = this.keyword;
    this.service.filterHampers(this.keyword).subscribe((data) => this.data = data);
  }
}
