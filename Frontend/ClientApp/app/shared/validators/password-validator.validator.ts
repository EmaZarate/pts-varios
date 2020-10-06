import { AbstractControl } from '@angular/forms';

export class PasswordValidation {

    static MatchPassword(AC: AbstractControl) {
       const password = AC.get('password').value; // to get value in input tag
       const confirmPassword = AC.get('repeatPassword').value; // to get value in input tag
        if (password !== confirmPassword) {
            //AC.get('repeatPassword').setErrors( {MatchPassword: true} );
            return {MatchPassword: true };
        } else {
            return null;
        }
    }
}       