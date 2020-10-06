import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'validateDate'
})
export class ValidateDatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    const date = new Date(value);
    return date.getFullYear() != 1
  }

}
