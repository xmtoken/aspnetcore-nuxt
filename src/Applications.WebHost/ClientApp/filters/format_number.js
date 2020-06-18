import numbro from 'numbro';
export default function (val, format) {
  const value = numbro(val).value();
  if (value === undefined || value === null || Number.isNaN(value)) {
    return val;
  } else {
    return numbro(val).format(format);
  }
}
