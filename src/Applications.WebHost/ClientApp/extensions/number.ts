import numbro from 'numbro';

export function format(val: string | number | null | undefined, format: string): string {
  const value = pretty(val);
  return isValid(value) ? numbro(value).format(format) : value?.toString() ?? '';
}

export function is32bitInteger(val: string | number | null | undefined): boolean {
  if (/^[+-]?[0-9]+$/.test(val?.toString() ?? '')) {
    const value = Number(val);
    return value >= -2147483648 && value <= 2147483647;
  } else {
    return false;
  }
}

export function isValid(val: string | number | null | undefined): boolean {
  const value = pretty(val);
  const number: number | null | undefined = numbro(value).value();
  return number !== null && number !== undefined && number !== Infinity && !Number.isNaN(number);
}

export function pretty(val: string | number | null | undefined): string {
  return val?.toString().trim() || '';
}
