import numbro from 'numbro';
import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  message: '{_field_}は{max}以下の値を入力してください。',
  params: ['max'],
  validate(val: string | number | null | undefined, { max }: Record<string, any> = {}): boolean {
    const value = numbro.unformat(val?.toString() ?? '', {});
    return value <= Number(max);
  },
};

export default rule;
