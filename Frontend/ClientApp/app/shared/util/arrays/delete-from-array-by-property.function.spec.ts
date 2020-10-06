import {deleteFromArrayByProperty} from './delete-from-array-by-property.function';

describe('DeleteFromArrayByProperty', () => {

    let ARRAY;

    beforeEach(() => {
        ARRAY = [
            {
                id: "1",
                prop1: "prop1"
            },
            {
                id: "2",
                prop1: "prop2"
            },
            {
                id: "3",
                prop1: "prop3"
            },
            {
                id: "4",
                prop1: "prop4"
            },
        ];
    })

    it('should delete object from array when property match with value', () => {
        let prop = "id";
        let val = "2";

        deleteFromArrayByProperty(ARRAY, prop, val);

        expect(ARRAY.length).toBe(3);
    })

    it('should not delete object from array when property not match with any value', () => {
        let prop = "id";
        let val = "6";

        deleteFromArrayByProperty(ARRAY, prop, val);

        expect(ARRAY.length).toBe(4);  
    })

    it('should not delete object from array when property doesnt match ', () => {
        let prop = "sarasa";
        let val = "6";

        deleteFromArrayByProperty(ARRAY, prop, val);

        expect(ARRAY.length).toBe(4);  
    })

})