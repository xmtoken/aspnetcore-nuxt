import numbro from 'numbro';
import * as NumberHelper from '~/extensions/number';

export default function (val: string | number | null | undefined, format: string | numbro.Format): string {
  return NumberHelper.format(val, format);
}
