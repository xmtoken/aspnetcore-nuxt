import * as TimeHelper from '~/extensions/time';

export default {
  message: '{_field_}は有効な時刻ではありません。',
  validate(val: string | null | undefined): boolean {
    return TimeHelper.isValid(val);
  },
};
