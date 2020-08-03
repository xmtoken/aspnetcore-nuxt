import * as DateHelper from '~/extensions/date';

export default {
  message: '{_field_}は有効な日付ではありません。',
  validate(val: string | null | undefined): boolean {
    return DateHelper.isValid(val);
  },
};
