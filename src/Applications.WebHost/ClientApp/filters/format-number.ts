import * as NumberHelper from '~/extensions/number';

export default function (val: string | number | null | undefined, format: string): string | null | undefined {
  return NumberHelper.format(val, format);
}
