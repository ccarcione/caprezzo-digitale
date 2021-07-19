import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Feedback } from '../../model/feedback';
import { FeedbackService } from '../../feedback.service';
import { LoadingService } from 'src/app/layout/loading/loading.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {

  feedback: Feedback = new Feedback();

  constructor(private toastrService: ToastrService,
    private feedbackService: FeedbackService,
    private loadingService: LoadingService) { }

  ngOnInit(): void {
  }

  async inviaFeedback() {
    if (!this.feedback.rating)
    {
      this.toastrService.warning('Dai una valutazione generale all\'app selezionando le stelle');
      return;
    }

    // salva recensione su db
    this.loadingService.setStatusLoadingApp(true);
    await this.feedbackService.save(this.feedback);
    this.loadingService.setStatusLoadingApp(false);

    // messaggio ringraziamento
    this.toastrService.success('La tua opinione è importante per migliorare questa idea. Grazie di aver dedicato qualche minuto per dire la tua!',
      'Grazie per il feedback',
      { timeOut: 10000 });
  }

  updateRating(event) {
    this.feedback.rating = event.rating;
  }
}
