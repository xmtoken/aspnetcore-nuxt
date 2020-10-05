import numbro from 'numbro';
import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  message: '{_field_}は{min}から{max}までの値を入力してください。',
  params: ['min', 'max'],
  validate(val: string | number | null | undefined, { min, max }: Record<string, any> = {}): boolean {
    const value = numbro.unformat(val?.toString() ?? '', {});
    return Number(min) <= value && value <= Number(max);
  },
};

export default rule;
