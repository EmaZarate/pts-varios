import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

import { IUserServices } from '../models/IUserServices';



@Injectable()
export class GoogleService implements IUserServices {
  _profile: any;
  private authWindow: Window;
  private redirectUri = environment.BASE_URL + "/assets/google-auth.html"; 
  constructor() { }

   login(){
    var url = 'https://accounts.google.com/o/oauth2/v2/auth?scope=profile%20email&access_type=offline&include_granted_scopes=true&state=state_parameter_passthrough_value&redirect_uri='+this.redirectUri+'&response_type=code&client_id=972488029154-ki002b8p0onvv7drhho92m0ecbgmun72.apps.googleusercontent.com';
    var w = 600;
    var h = 400;
    var left = (screen.width/2)-(w/2);
    var top = (screen.height/2)-(h/2);
       this.authWindow = window.open(url,null,'width='+w+',height='+h+',top='+top+',left='+left);
   }

  closeWindow(){
    if(this.authWindow)
    this.authWindow.close();
  }
}
