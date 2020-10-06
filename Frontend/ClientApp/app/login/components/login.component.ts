import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { Router } from "@angular/router";
import { FactoryLogin } from '../factories/factory-login';
import { Credentials } from '../models/credentials';
import { LoginService } from '../../core/services/login.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { IUserServices } from '../models/IUserServices';
import { Subject } from '../../../../node_modules/rxjs';
import { AuthService} from '../../core/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
    
})
export class LoginComponent implements OnInit, OnDestroy {

    @BlockUI() blockUI: NgBlockUI;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    private serviceLogin: IUserServices;
    credentials: Credentials;

    private toggleButton: any;
    private sidebarVisible: boolean;
    private nativeElement: Node;

    public errorLogin = false;
    private errorType:any = [];

    constructor(
        private loginFactory: FactoryLogin, 
        private router: Router,
        private _loginService: LoginService,
        private _authService: AuthService,
        private element: ElementRef
    ) { 
        this.nativeElement = element.nativeElement;
        this.sidebarVisible = false;
     }

    public creds = {
        email: "",
        password: ""
    };

    ngOnInit() {

        var navbar : HTMLElement = this.element.nativeElement;
        this.toggleButton = navbar.getElementsByClassName('navbar-toggle')[0];
        const body = document.getElementsByTagName('body')[0];
        body.classList.add('login-page');
        body.classList.add('off-canvas-sidebar');
        const card = document.getElementsByClassName('card')[0];
        setTimeout(function() {
            // after 1000 ms we add the class animated to the login/register card
            card.classList.remove('card-hidden');
        }, 700);

        let token = localStorage.getItem('auth_token');
        if(!!token) this.router.navigate(['/home']);

        this._loginService.isLogging$
        .takeUntil(this.ngUnsubscribe)
        .subscribe((val)=>{        
            if(val){
                this.blockUI.start("Cargando...");
            }
            else{
                this.blockUI.stop();
            }
        });

        this._authService.errorInLogin$
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res: any) => {
                if(res){
                    this.errorLogin = true;
                    this.errorType = res.error;
                }
                else{
                    this.errorLogin = false;
                }
            });



    }

    ngOnDestroy(){
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();

        const body = document.getElementsByTagName('body')[0];
        body.classList.remove('login-page');
        body.classList.remove('off-canvas-sidebar');

    }

    onLogin(x) {
        
        this.serviceLogin = this.loginFactory.createLogin(x);
        this.serviceLogin.login();
    }

    onLoginHoshin() {
        this.errorType = []
        let isValid = true;        
        if(!this.creds.email){
            this.errorType.push('El E-mail es obligatorio');   
            this.errorLogin = true;         
            isValid = false;
        }
        
        if(!this.creds.password){
            this.errorType.push('La contraseña es obligatoria');               
            this.errorLogin = true;     
            isValid = false;          
        }        

        if(!isValid) return;
        
        if(this.serviceLogin){
            this.serviceLogin.closeWindow();
        }

        this.blockUI.start('Cargando...');
        this._loginService.loginHoshinUser(this.creds.email, this.creds.password)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(result => {
            if(result){
                this.router.navigate(['/home']);         
            }
        },err => { 
            this.creds.email = "";
            this.creds.password = "";
            this.errorLogin = true;
            this.errorType.push(err.error.message);               
        }
      );
    }

    sidebarToggle() {
        var toggleButton = this.toggleButton;
        var body = document.getElementsByTagName('body')[0];
        var sidebar = document.getElementsByClassName('navbar-collapse')[0];
        if (this.sidebarVisible == false) {
            setTimeout(function() {
                toggleButton.classList.add('toggled');
            }, 500);
            body.classList.add('nav-open');
            this.sidebarVisible = true;
        } else {
            this.toggleButton.classList.remove('toggled');
            this.sidebarVisible = false;
            body.classList.remove('nav-open');
        }
    }
     
}
