import dayjs from 'dayjs';
export default function (val, format) {
  const value = val?.toString().trim() || '';
  return value && dayjs(value).isValid() ? dayjs(value).format(format) : val;
}
