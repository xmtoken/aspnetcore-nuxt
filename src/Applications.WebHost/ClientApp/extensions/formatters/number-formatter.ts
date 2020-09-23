import numbro from 'numbro';

export function format(val: string | number | null | undefined, format: string | numbro.Format): string {
  const value = pretty(val);
  return isValid(value) ? numbro(value).format(format) : value?.toString() ?? '';
}

export function isValid(val: string | number | null | undefined): boolean {
  const value = pretty(val);
  const number: number | null | undefined = numbro(value).value();
  return number !== null && number !== undefined && number !== Infinity && !Number.isNaN(number);
}

export function pretty(val: string | number | null | undefined): string {
  return val?.toString().trim() || '';
}
