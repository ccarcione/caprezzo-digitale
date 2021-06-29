import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'src/app/message.service';
import { map } from 'rxjs/operators';
import { Messaggio } from 'src/app/model/messaggio';

@Component({
  selector: 'app-messaggio',
  templateUrl: './messaggio.component.html',
  styleUrls: ['./messaggio.component.css']
})
export class MessaggioComponent implements OnInit {
id:string;
message:Messaggio = new Messaggio();
  constructor(private activatedRoute: ActivatedRoute,
    private messageService:MessageService) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.pipe(
      map(async m => {

        this.id = m.get('id');
        return await this.messageService.getById(+this.id);
      }))
      .subscribe(async c => {
        this.message = new Messaggio(await c);
      });
  }

}
