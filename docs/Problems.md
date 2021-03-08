# PROBLEMS

xxx

## vetur

xxx

### Vue インスタンスメンバーの型が any 型として扱われる

以下のようにパラメーターの型を明示した computed プロパティを定義すると、Vue インスタンスのメンバーの型が any 型として判定されてしまうことがあります。

```
export default Vue.extend({
  data() {
    return {
      value: null as string | null,
    },
  },
  computed: {
    value(val: string | null): void {
      ...
    }
  },
  methods: {
    action(): void {
      this.$axios ... // $axios メンバーが any 型として扱われる。
    }
  }
})
```

上記事象は props の定義を含まない場合に vetur のバグによって引き起こされますが、props の定義を追加することで型の判定を正しく動作させることができます。

```
export default Vue.extend({
  props: {}, // required props
  data() {
    return {
      value: null as string | null,
    },
  },
  computed: {
    value(val: string | null): void {
      ...
    }
  },
  methods: {
    action(): void {
      this.$axios ... // $axios typed NuxtAxiosInstanse
    }
  }
})
```
