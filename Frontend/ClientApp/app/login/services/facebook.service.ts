import { Injectable } from '@angular/core';

import { LoginModule } from '../login.module';

import { environment } from '../../../environments/environment';

import { IUserServices } from '../models/IUserServices';



@Injectable()
export class FacebookService implements IUserServices {

    constructor() { }

    private redirectUri = environment.BASE_URL + "/assets/facebook-auth.html"; 

    private authWindow: Window;
    failed: boolean;
    error: string;
    errorDescription: string;

    login() {
      var url = 'https://www.facebook.com/v2.11/dialog/oauth?&response_type=token&display=popup&client_id=1655513044575767&display=popup&redirect_uri=' + this.redirectUri +'&scope=email'
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

