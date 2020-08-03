import dayjs from 'dayjs';
import * as DateHelper from '~/extensions/date';

export default {
  message: '{_field_}は過去の日付を入力してください。',
  validate(val: string | null | undefined): boolean {
    const value = DateHelper.pretty(val);
    return DateHelper.isValid(value) && dayjs(value).format('YYYYMMDD') < dayjs().format('YYYYMMDD');
  },
};
