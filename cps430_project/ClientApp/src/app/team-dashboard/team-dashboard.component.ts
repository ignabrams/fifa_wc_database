import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { concat, Observable } from 'rxjs';
import { concatMap, map, switchMap } from 'rxjs/operators';
import { HomeService } from '../home/home.service';
import { TeamService } from './player.service';

@Component({
  selector: 'app-team-dashboard',
  templateUrl: './team-dashboard.component.html',
  styleUrls: ['./team-dashboard.component.css']
})
export class TeamDashboardComponent implements OnInit {

  players$: Observable<any[]>;
  team$: Observable<any>;

  constructor(private playerService: TeamService, private avRoute: ActivatedRoute) {
    this.players$ =
      this.avRoute.params
        .pipe(
          concatMap((value, _) => this.playerService.getAllPlayers(value['id'])),
          map(this.resolvePositionName)
        );

    this.team$ =
      this.avRoute.params
        .pipe(
          concatMap((value, _) => this.playerService.get(+value['id']))
        );
  }

  ngOnInit(): void {
  }

  resolvePositionName(player: any[]) {
    player.forEach(p => {
      let returned;
      switch (p.position.name) {
        case "Goalkeeper": returned = "G"; break;
        case "Defender": returned = "D"; break;
        case "Midfielder": returned = "MF"; break;
        case "Forward": returned = "F"; break;
        case "Manager": returned = "MA"; break;
        default: returned = name;
      }

      p.position.name = returned;
    });
    return player;
  }

}
