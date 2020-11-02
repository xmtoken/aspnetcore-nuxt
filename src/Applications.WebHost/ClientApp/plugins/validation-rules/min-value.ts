import numbro from 'numbro';
import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  message: '{_field_}は{min}以上の値を入力してください。',
  params: ['min'],
  validate(val: string | number | null | undefined, { min }: Record<string, any> = {}): boolean {
    const value = numbro.unformat(val?.toString() ?? '', {});
    return Number(min) <= value;
  },
};

export default rule;
