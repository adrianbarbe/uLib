import merge from "lodash/merge";
export const remapErrors = (errors) => {
    let errorsWithoutDuplicates = removeDuplicates(errors);
    
    return errorsWithoutDuplicates.reduce((obj, error) => {
        if (error.field.includes('.')) {
            let camelized = camelizeDotDelimitedPascal(error.field);
            let expanded = expand(camelized, error.message);
            
            obj = merge(obj, expanded);
        }
        else {
            obj[camelizePascal(error.field)] = error.message;
        }

        return obj
    }, {})
};
export const removeDuplicates = (errors) => {
    const errorsNew = [];
    errors.forEach(error => {
        let foundErrors = errors.filter(e => e.field === error.field);
        
        if (foundErrors.length === 1) {
            errorsNew.push({
                field: foundErrors[0].field,
                message: foundErrors[0].message,
            })
        }
        else {
            let messages = foundErrors.map(fe => fe.message);

            errorsNew.push({
                field: foundErrors[0].field,
                message: messages.join("<br>"),
            });  
        }
    });
    
    return errorsNew;
};

export const camelizePascal = (string) => {    
    return `${string.charAt(0).toLowerCase()}${string.slice(1)}`;
};

export const camelizeDotDelimitedPascal = (string) => {
  const stringArray = string.split(".");
  const remappedStringArray = stringArray.map(str => {
      return camelizePascal(str);
  });
  
  return remappedStringArray.join(".");
};

export const expand = (str, val = {}) => {
    return str.split('.').reduceRight((acc, currentValue) => {
        return { [currentValue]: acc }
    }, val)
};