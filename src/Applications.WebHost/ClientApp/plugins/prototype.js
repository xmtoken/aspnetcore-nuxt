Array.create = function (iterable) {
  return Array.from(iterable || []);
};

Array.prototype.ofIntegers = function () {
  return this.map(x => Number(x?.toString().trim() || undefined)).filter(x => Number.isInteger(x));
};
