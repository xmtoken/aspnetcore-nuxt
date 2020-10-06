import dayjs from 'dayjs';
import { ValidationRule } from 'vee-validate/dist/types/types';
import * as DateFormatter from '~/extensions/formatters/date-formatter';

const rule: ValidationRule = {
  message: '{_field_}は過去の日付を入力してください。',
  validate(val: string | null | undefined): boolean {
    const value = DateFormatter.pretty(val);
    return DateFormatter.isValid(value) && dayjs(value).isBefore(dayjs(), 'day');
  },
};

export default rule;
