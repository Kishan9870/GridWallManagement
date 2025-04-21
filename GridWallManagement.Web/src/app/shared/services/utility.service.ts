import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UtilityService {
  checkEmailFormat(value: string): boolean {
    const emailReg =
      /^[a-zA-Z0-9-_]+(\.[a-zA-Z0-9-_]+)*@[a-zA-Z0-9-_]+(\.[a-zA-Z0-9]+)*(\.[a-zA-Z]{2,5})$/g;
    return emailReg.test(value);
  }

  checkNumberFormat(value: string): boolean {
    const numberReg = /^[0-9]*$/g;
    return numberReg.test(value);
  }

  checkPasswordFormat(value: string): boolean {
    const passwordReg = /^(?=.*[a-z])(?=.*[0-9])\S{6,}$/g;
    return passwordReg.test(value);
  }

  getId(length: number): string {
    let text = '';
    const charList =
      'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    for (let i = 0; i < length; i++) {
      text += charList.charAt(Math.floor(Math.random() * charList.length));
    }
    return text;
  }

  getTimeStamp(prefix: string): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = now.getMonth() + 1;
    const monthString = ('0' + month).slice(-2);
    const date = now.getDate();
    const dateString = ('0' + date).slice(-2);
    const hour = now.getHours();
    const hourString = ('0' + hour).slice(-2);
    const min = now.getMinutes();
    const minString = ('0' + min).slice(-2);
    const sec = now.getSeconds();
    const secString = ('0' + sec).slice(-2);
    const nowString =
      year.toString() +
      monthString +
      dateString +
      hourString +
      minString +
      secString;
    const timeStamp = prefix + '-' + nowString;
    return prefix === '' ? nowString : timeStamp;
  }

  // Format width of excel worksheet based on content length
  autoWidth = (worksheet: any, minimalWidth = 0) => {
    worksheet.columns.forEach((column: any) => {
      let maxColumnLength = 0;
      column.eachCell({ includeEmpty: true }, (cell: any) => {
        maxColumnLength = Math.max(
          maxColumnLength,
          minimalWidth,
          cell.value ? this.maxNewLineLength(cell.value.toString()) : 0
        );
      });
      column.width = maxColumnLength + 2;
    });
  };

  maxNewLineLength(str: string) {
    var calc = str.split('\n');
    calc.sort(function (a, b) {
      return b.length - a.length;
    });
    return calc[0].length;
  }

  //  Convert number to excel column reference
  numberToLetters(num: number): string {
    let letters = '';
    while (num >= 0) {
      letters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'[num % 26] + letters;
      num = Math.floor(num / 26) - 1;
    }
    return letters;
  }
}
