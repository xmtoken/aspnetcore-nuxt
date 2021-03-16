export class PagingQueryBuilder {
  private offset: number | undefined;
  private limit: number | undefined;

  private constructor() {
    this.offset = undefined;
    this.limit = undefined;
  }

  public static create(val: { offset?: number; limit?: number }) {
    const builder = new PagingQueryBuilder();
    builder.offset = val.offset;
    builder.limit = val.limit;
    return builder;
  }

  public getQuery() {
    return {
      $offset: this.offset,
      $limit: this.limit,
    };
  }
}
