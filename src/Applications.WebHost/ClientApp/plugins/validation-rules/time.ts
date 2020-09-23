import { ValidationRule } from 'vee-validate/dist/types/types';
import * as TimeFormatter from '~/extensions/formatters/time-formatter';

const rule: ValidationRule = {
  message: '{_field_}は有効な時刻ではありません。',
  validate(val: string | null | undefined): boolean {
    return TimeFormatter.isValid(val);
  },
};

export default rule;
