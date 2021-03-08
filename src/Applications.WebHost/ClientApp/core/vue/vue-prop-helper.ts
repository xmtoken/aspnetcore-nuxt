export class VuePropHelper {
  public static toBoolean(val: string | boolean | undefined) {
    return !!val || val === '';
  }
}
