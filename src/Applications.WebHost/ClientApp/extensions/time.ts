import dayjs from 'dayjs';

export function format(val: string | null | undefined, format: string): string {
  const value = pretty(val);
  return isValid(value) ? dayjs(`0001/01/01 ${value}`).format(format) : value?.toString() ?? '';
}

export function isValid(val: string | null | undefined): boolean {
  const value = pretty(val);
  return !!value && dayjs(`0001/01/01 ${value}`).isValid();
}

export function pretty(val: string | null | undefined): string {
  return val?.toString().trim() || '';
}
