export default function (val: string | number | null | undefined, enums: { text: string; value: string }[]): string | null | undefined {
  return enums.find(x => x.value === val)?.text;
}
