import dayjs from 'dayjs';

export default {
  message: '{_field_}は有効な日付ではありません。',
  validate(val) {
    const value = val?.toString().trim() || '';
    return value && dayjs(value).isValid();
  },
};
