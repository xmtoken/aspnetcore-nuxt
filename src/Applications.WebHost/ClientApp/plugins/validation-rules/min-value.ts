import numbro from 'numbro';
import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  message: '{_field_}は{length}文字以上で入力してください。',
  params: ['length'],
  validate(val: string | number | null | undefined, { length }: Record<string, any> = {}): boolean {
    const value = numbro.unformat(val?.toString() ?? '', {});
    return Number(length) <= value;
  },
};

export default rule;
