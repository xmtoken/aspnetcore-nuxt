import dayjs from 'dayjs';

export function format(val: string | null | undefined, format: string): string {
  const value = pretty(val);
  return isValid(value) ? dayjs(value).format(format) : value?.toString() ?? '';
}

export function isValid(val: string | null | undefined): boolean {
  const value = pretty(val);
  return !!value && dayjs(value).isValid();
}

export function pretty(val: string | null | undefined): string {
  return val?.toString().trim() || '';
}
