import numbro from 'numbro';
import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  message: '{_field_}は{length}文字以下で入力してください。',
  params: ['length'],
  validate(val: string | number | null | undefined, { length }: Record<string, any> = {}): boolean {
    const value = numbro.unformat(val?.toString() ?? '', {});
    return value <= Number(length);
  },
};

export default rule;
