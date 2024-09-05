import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { concatMap } from 'rxjs/operators';
import { PlayerService } from './player.service';

@Component({
  selector: 'app-player-dashboard',
  templateUrl: './player-dashboard.component.html',
  styleUrls: ['./player-dashboard.component.css']
})
export class PlayerDashboardComponent implements OnInit {

  player$: Observable<any>;

  constructor(private playerService: PlayerService, private avRoute: ActivatedRoute) {

    this.player$ = this.avRoute.params
      .pipe(concatMap((value, _) => this.playerService.get(+value['id'])));
  }

  ngOnInit(): void {
  }

}
