import { Component } from '@angular/core';

export enum LoadingMode {
  Loading = '0',
  Creating = '1',
  Retrieving = '2',
  Updating = '3',
  Deleting = '4'
}

const MESSAGE_LOADING = 'Cargando...';
const MESSAGE_CREATING = 'Creando...';
const MESSAGE_RETRIEVING = 'Recuperando información...';
const MESSAGE_UPDATING = 'Actualizando...';
const MESSAGE_DELETING = 'Eliminando...';

@Component({
  selector: 'app-block-template',
  templateUrl: './block-template.component.html',
  styleUrls: ['./block-template.component.css']
})

export class BlockTemplateComponent {
  message: any;

  getMessage(): string {
    switch(this.message) {
      case LoadingMode.Loading: return MESSAGE_LOADING;
      case LoadingMode.Creating: return MESSAGE_CREATING;
      case LoadingMode.Retrieving: return MESSAGE_RETRIEVING;
      case LoadingMode.Updating: return MESSAGE_UPDATING;
      case LoadingMode.Deleting: return MESSAGE_DELETING;
      default: return MESSAGE_LOADING;
    }
  }
}
