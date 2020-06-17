import numbro from 'numbro';
export default function (val, format) {
  return val && numbro.validate(val, {}) ? numbro(val).format(format) : val;
}
