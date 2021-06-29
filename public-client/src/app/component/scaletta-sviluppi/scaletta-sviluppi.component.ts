import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-scaletta-sviluppi',
  templateUrl: './scaletta-sviluppi.component.html',
  styleUrls: ['./scaletta-sviluppi.component.css']
})
export class ScalettaSviluppiComponent implements OnInit {

  constructor(private toastr: ToastrService) { }

  ngOnInit(): void {
    setTimeout(() => { this.toastr.show('Visita spesso questo menù e tieniti aggiornato!', 'Scaletta Sviluppi'); }, 4000);
  }

}
