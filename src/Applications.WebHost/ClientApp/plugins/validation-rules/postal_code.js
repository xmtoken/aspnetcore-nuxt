export default {
  message: '{_field_}はハイフン区切りの数字7文字で入力してください。',
  validate(val) {
    const value = val || '';
    return /[0-9]{3}-[0-9]{4}/.test(value);
  },
};
