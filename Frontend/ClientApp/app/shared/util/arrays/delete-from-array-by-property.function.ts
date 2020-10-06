export function deleteFromArrayByProperty(arrayObj: any, property: string, value: any) {
    let pos = arrayObj.findIndex(obj => {return obj[property] === value});
    if (pos != -1) arrayObj.splice(pos, 1);
}