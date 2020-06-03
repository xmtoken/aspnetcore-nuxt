import dayjs from 'dayjs';

export default {
  message: '{_field_}は有効な時刻ではありません。',
  validate(val) {
    const value = val?.trim() || '';
    return !!value && dayjs(`0001/01/01 ${value}`).isValid();
  },
};
