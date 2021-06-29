<!-- PROJECT LOGO -->
<br />
<p align="center">
  <img src="immagini/logo.png" alt="Logo" width="270" height="117">

  <h3 align="center">Caprezzo X Tutti</h3>

  <p align="center">
    Servizio .Net Core - PWA dedicato a una più corretta ed efficacia comunicazione con il cittadino.
    <br />
    <br />
    <strong>« Esplora la documentazione presente nel repository »</strong>
    <br />
    oppure
    <br />
    consulta le API esposte dal servizio tramite <a href="https://caprezzoxtutti.it/api/api-docs/index.html"><strong>« Swagger API docs »</strong></a>
    <br />
    <br />
    <a href="https://caprezzoxtutti.master.experimenta.cloud">Ambiente di Sviluppo</a>
    ·
    <a href="https://caprezzoxtutti.master.experimenta.cloud">Ambiente di Test</a>
    ·
    <a href="SEGNALA_BUG.md">Segnala Bug</a>
    ·
    <a href="https://caprezzoxtutti.it/feedback">Richiedi Feature</a>
    .
    <a hrfe="https://drive.google.com/drive/folders/180SqtIJMT_oJDoRXu5TmDu0psUMPP1iQ?usp=sharing">Documentazione GDrive</a>
  </p>
</p>

# Caprezzo X Tutti

## Introduzione

L'idea nasce da una necessità di avere una corretta comunicazione con tutti i cittadini del paese. Ad oggi chiunque è dotato di un telefono smartphone equipaggiato di browser, e grazie a questo tutti posso accedere e rimanere informati sulle notizie del paese.

In questo periodo di covid, nel quale con i vari lock-down le persone non potevano uscire di casa, una comunicazione affidabile e veloce può aiutare sensibilmente i rapporti tra amministrazione e cittadino.

I primi obiettivi di questa idea sono:
- permettere a ogni cittadino del paese di consultare gli avvisi esposti in bacheca (virtuale tramite questo servizio)
- informare la popolazione riguardo avvisi urgenti e/o relativamente importanti derivanti da enti comunale e non
- mettere tutti a conoscenza di novità ed aggiornameti della cittadella
- offrire informazioni di "buone pratiche" e/o di carattere culturale consultabili da residenti e turisti
- servizio di notifiche push disponibile per alcuni dispositivi (no device apple. maggiori info qui --> [Sending Push Notifications to iOS from PWA](https://stackoverflow.com/questions/63819485/sending-push-notifications-to-ios-from-pwa)

## Documentazione del progetto

In questa sezione vedrò di documentare e spiegare al meglio ogni funzione offerta dal servizio.

### Assunzioni

- L'utilizzo del servizio viene offerto in modo gratuito
- Il servizio può essere consultato da qualsiasi dispositivo tramite i browser più popolari oppure installato sul proprio device come una applicazione nativa (attenzione: alcuni sistemi operativi mostrano limitazioni)
- In futuro verrà sviluppato un servizio di identità per garantire ad alcuni utenti l'utilizzo del servizio
- Esiste una informativa sulla privacy consultabile qui --> [Privacy Policy](https://caprezzoxtutti.it/privacy-policy)

### Bacheca del paese

Prima funzionalità sviluppata e cavallo di battaglia del programma.
- [ ] Tramite questa sezione l'utente può consultare feed di vario tipo in ordine cronologico
- [ ] La notizia può contenere allegati aggiuntivi pubblicamente scaricabili
- [ ] Le notizie sono "etichettate" in modo da definirne velocemente la tipologia
- [ ] Le notizie possono contenere una immagine come intestazione.

### Allerte

La funzione "Allerte" si preoccupa di mostrare dati riguardante le allerte sul territorio. E' possibile consultare tutti i dati accedendo al sito [Arpa Piemonte](http://www.arpa.piemonte.it/bollettini/elenco-bollettini)
- [ ] L'utente può consultare le informazioni
- [ ] Quando il bollettino presenta delle allerte diverse da "Verde" l'applicazione provvede (dove possibile) a comunicarlo all'utente tramite notifica push

### Eventi

Funzionalità simile a "**Bacheca del paese**". Qui potrebbero essere pubblicate informazioni di caratte ludico, oppure questionari relativi alla partecipazione e/o preferenze del diretto cittadino a eventi promossi nel paese.

### Galleria/Foto

Galleria foto del paese. Il programma offre la possibilità all'utente di condividere pubblicamente foto tramite la piattaforma (pubblicazione post-moderazione).

### Turismo

Sezione del programma a scopo parzialmente consultativo. Contiene info e curiosità riguardanti la cittadella. Attualmente sono state pensate/predisposte le seguenti idee:
- Luoghi di interesse --> tutto quello che puà interessare al turista
- Info Sentieri --> semplice elenco/dettaglio dei sentieri del paese

### Wiki

Sezione del programma unicamente a scopo consultativo contenente info e curiosità della cittadella. Questa sezione vuole proprio comportarsi come una Wiki del paese.

Attualmente sono state pensate le seguenti idee:
- Cenni storici
- Info flora e fauna
- Info funghi presenti sul territorio (link di rimando a wiki)
- Cultura e tradizioni

### Servizi al Cittadino

Sezione del programma unicamente a scopo consultativo. Qui saranno sviscerati argomenti/informazioni come:
- [ ] Uffici
- [ ] Negozi e Strutture
- [ ] Raccolta Differenziata
- [ ] Trasporto
- [ ] Recapiti vari

### Amministrazione Trasparente

Link utili per raggiungere alcuni servizi comunali.
- Amministrazione Trasparente
- Albo Pretorio
- Albo Pretorio 2018

### Informazioni

Informazioni relative a servizio emesso.
- [ ] About
- [ ] Chi Siamo
- [ ] Chengelog
- [ ] Privacy Policy

### La tua opinione conta

Form dedicato all'utente generico utile per fornire una valutazione del servizio/programma.

---

## PWA

qui mettere vari screen dell'app (gif)

---

## Built With

Angular UI frontend
* [Angular CLI](https://cli.angular.io/)
* [Material Design components for Angular](https://material.angular.io/)


.Net Core backend
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
* [PostgreSQL](https://www.postgresql.org/)

## Getting Started

Per ottenere una copia locale attiva e funzionante, segui questi semplici passaggi --> [HOW TO INSTALL](/HOW_TO_INSTALL.md)

## Roadmap
Per maggiori info consultare la [Board](https://gitlab.com/projects-experimenta/app-comune/-/boards/1949143) e le [Milestone](https://gitlab.com/projects-experimenta/app-comune/-/milestones) del repository.

## Contributing

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Merge Request

## License

Distributed under the GNU Affero General Public License v3. See `LICENSE` for more information.

Invito a leggere e comprendere --> [Problema del free rider](https://it.wikipedia.org/wiki/Problema_del_free_rider#:~:text=Per%20la%20teoria%20dell'azione,%C3%A8%20un%20incentivo%20(monetario).)

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://gitlab.com/projects-experimenta/app-comune/-/tags). 

## Autori

Carcione Christian - [@Carcione](https://gitlab.com/Carcione) - carcione.christian@gmail.com

Jessica Sala - [@jessicasala](https://gitlab.com/jessicasala) - jesssicasala@gmail.com

Project Link: [GitLab Repository](https://gitlab.com/projects-experimenta/app-comune
