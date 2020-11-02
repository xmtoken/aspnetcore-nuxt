import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  validate(val: string | null | undefined): boolean {
    const value = val?.toString() || '';
    return /[0-9０-９]{3}-?[0-9０-９]{4}/.test(value);
  },
};

export default rule;
