import { IUserServices } from '../models/IUserServices';
import { MicrosoftGraphService } from '../services/microsoft-graph.service';
import { GoogleService } from '../services/google.service';
import { Injectable } from '@angular/core';
import { FacebookService } from '../services/facebook.service';
import { LoginModule } from '../login.module';


export enum LoginKind{
    Office,
    Gmail,
    Facebook
}

@Injectable()
export class FactoryLogin {

    constructor(private graph : MicrosoftGraphService,
                private google: GoogleService,
                private facebook: FacebookService){ }

    createLogin(kind:LoginKind) : IUserServices{
        switch(kind){
            case LoginKind.Office:              
                return this.graph;
            case LoginKind.Gmail:
                return this.google;
            case LoginKind.Facebook:
                return this.facebook;
        }
    }
}
