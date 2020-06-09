export default function (val, enums) {
  return enums.find(x => x.value === val)?.text;
}
