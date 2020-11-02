import numbro from 'numbro';
import { ValidationRule } from 'vee-validate/dist/types/types';

const rule: ValidationRule = {
  params: ['min'],
  validate(val: string | number | null | undefined, { min }: Record<string, any> = {}): boolean {
    const value = numbro.unformat(val?.toString() ?? '', {});
    return Number(min) <= value;
  },
};

export default rule;
