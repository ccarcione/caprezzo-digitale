import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { EmailFeedback } from '../../model/emailFeedback';
import { EmailService } from '../../email.service';
import { LoadingService } from 'src/app/layout/loading/loading.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {

  emailFeedback: EmailFeedback = new EmailFeedback();

  constructor(private toastrService: ToastrService,
    private emailService: EmailService,
    private loadingService: LoadingService) { }

  ngOnInit(): void {
  }

  async inviaEmailFeedback() {
    if (!this.emailFeedback.rating)
    {
      this.toastrService.warning('Dai una valutazione generale all\'app selezionando le stelle');
      return;
    }

    // invia email
    // salva recensione su db
    this.loadingService.setStatusLoadingApp(true);
    await this.emailService.sendEmail(this.emailFeedback, true);
    this.loadingService.setStatusLoadingApp(false);

    // messaggio ringraziamento
    this.toastrService.success('La tua opinione è importante per migliorare questa idea. Grazie di aver dedicato qualche minuto per dire la tua!',
      'Grazie per il feedback',
      { timeOut: 10000 });
  }

  updateRating(event) {
    this.emailFeedback.rating = event.rating;
  }
}
