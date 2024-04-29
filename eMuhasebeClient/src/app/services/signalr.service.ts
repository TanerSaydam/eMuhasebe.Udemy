import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { signalRAPI } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  hub: signalR.HubConnection | undefined;

  constructor() { }

  connect(callBack: () => void){
    this.hub = new signalR.HubConnectionBuilder()
            .withUrl(`${signalRAPI}/report-hub`)
            .build();

    this.hub
    .start()
    .then(()=> {
      console.log("Report hub ile bağlantı başlatıldı...");
      callBack();
    })
  }
}
