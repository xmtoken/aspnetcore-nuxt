export default {
  message: '{_field_}はハイフン区切りの数字7文字で入力してください。',
  validate(val: string | null | undefined): boolean {
    const value = val?.toString() || '';
    return /[0-9]{3}-[0-9]{4}/.test(value);
  },
};