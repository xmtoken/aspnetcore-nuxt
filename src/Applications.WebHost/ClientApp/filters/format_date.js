import dayjs from 'dayjs';
export default function (val, format) {
  return !!val && dayjs(val).isValid() ? dayjs(val).format(format) : val;
}
