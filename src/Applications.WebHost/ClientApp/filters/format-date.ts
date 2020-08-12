import * as DateHelper from '~/extensions/date';

export default function (val: string | null | undefined, format: string): string {
  return DateHelper.format(val, format);
}
