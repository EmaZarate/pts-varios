import { AbstractControl, ValidatorFn } from "@angular/forms";

export function existsRoleValidator(existsRole) : ValidatorFn{
    return(control: AbstractControl): {[key: string]: boolean} | null => {
        if(existsRole == undefined) return {'ExistsRole':true};
        if(existsRole){
            return {'ExistsRole': true};
        }
        return null;
    }

}



