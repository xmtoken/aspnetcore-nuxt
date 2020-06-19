import numbro from 'numbro';
export default function (val, format) {
  const value = val?.toString().trim() || '';
  const number = numbro(value).value();
  if (number === undefined || number === null || number === Infinity || Number.isNaN(number)) {
    return val;
  } else {
    return numbro(value).format(format);
  }
}
