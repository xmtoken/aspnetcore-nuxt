import dayjs from 'dayjs';

export default {
  message: '{_field_}は過去の日付を入力してください。',
  validate(val) {
    const value = val?.trim() || '';
    return !!value && dayjs(value).isValid() && dayjs(value).format('YYYYMMDD') < dayjs().format('YYYYMMDD');
  },
};
