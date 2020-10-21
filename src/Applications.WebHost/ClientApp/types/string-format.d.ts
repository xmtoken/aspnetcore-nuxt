declare interface String {
  format(template: string, ...args: Array<{ [k: string]: any } | string>): string;
}
