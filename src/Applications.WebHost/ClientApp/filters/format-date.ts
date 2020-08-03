import * as DateHelper from '~/extensions/date';

export default function (val: string | null | undefined, format: string): string | null | undefined {
  return DateHelper.format(val, format);
}
