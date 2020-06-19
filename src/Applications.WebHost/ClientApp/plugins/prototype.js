Array.create = function (iterable) {
  return Array.from(iterable || []);
};

Array.prototype.ofNumbers = function () {
  return this.map(x => Number(x?.toString().trim() || undefined)).filter(x => Number.isInteger(x));
};

Number.is32bitInteger = function (val) {
  const value = +val;
  return -2147483648 <= value && value <= 2147483647 && /^[+-]?[0-9]+$/.test(value);
};
