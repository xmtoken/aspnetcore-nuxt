import { ValidationRule } from 'vee-validate/dist/types/types';
import * as DateFormatter from '~/extensions/formatters/date-formatter';

const rule: ValidationRule = {
  message: '{_field_}は有効な日付ではありません。',
  validate(val: string | null | undefined): boolean {
    return DateFormatter.isValid(val);
  },
};

export default rule;
