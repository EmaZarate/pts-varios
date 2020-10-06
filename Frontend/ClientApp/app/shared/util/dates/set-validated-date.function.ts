import { AbstractControl } from "@angular/forms";

export function setValidatedDate(datePicker: AbstractControl, date: string) {
    if (!date || date.substring(0, 10) == "0001-01-01") {
        datePicker.reset();
      }
    else {
        datePicker.patchValue(new Date(date));
    }
}