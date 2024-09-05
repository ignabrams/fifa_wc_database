import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { combineLatest, Observable, of } from 'rxjs';
import { debounce, debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.styles.css']
})
export class HomeComponent {

  teams: Observable<any[]> = of([]);
  topPerformers$: Observable<any[]>;
  losers$: Observable<any[]>;
  winners$: Observable<any[]>;

  search: FormControl = new FormControl('');

  constructor(private service: HomeService) {
    this.topPerformers$ = this.service.getTopPerformers();
    this.losers$ = this.service.getExclusiveLosers();
    this.winners$ = this.service.getWinners();

    this.teams = this.search.valueChanges
      .pipe(startWith(''),
        debounceTime(250),
        switchMap(x => this.service.getAll(x)));
  }

}
