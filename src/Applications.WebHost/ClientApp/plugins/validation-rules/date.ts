import { ValidationRule } from 'vee-validate/dist/types/types';
import * as DateFormatter from '~/extensions/formatters/date-formatter';

const rule: ValidationRule = {
  validate(val: string | null | undefined): boolean {
    return DateFormatter.isValid(val);
  },
};

export default rule;
