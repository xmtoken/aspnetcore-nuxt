import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  validate(val: string | null | undefined): boolean {
    const value = val?.toString() || '';
    const index = value.indexOf('@');
    return index > 0 && index !== value.length - 1 && index === value.lastIndexOf('@');
  },
};

export default rule;
