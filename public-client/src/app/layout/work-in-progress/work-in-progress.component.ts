import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-work-in-progress',
  templateUrl: './work-in-progress.component.html',
  styleUrls: ['./work-in-progress.component.css']
})
export class WorkInProgressComponent implements OnInit {

  constructor(private toastr: ToastrService) { }

  ngOnInit(): void {
    setTimeout(() => { this.toastr.success('Presto disponibile! Forse..', 'Work In Progress'); }, 2000);

  }

}
