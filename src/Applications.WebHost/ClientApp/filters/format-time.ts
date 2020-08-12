import * as TimeHelper from '~/extensions/time';

export default function (val: string | null | undefined, format: string): string {
  return TimeHelper.format(val, format);
}
