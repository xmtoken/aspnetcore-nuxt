import { validate } from 'vee-validate';
import { ValidationResult } from 'vee-validate/dist/types/types';
import Vue from 'vue';

type ValidationHoge = ValidationProps | ((i: number) => ValidationProps) | ValidationObjectXxx;
interface ValidationObjectXxx {
  [key: string]: ValidationHoge;
}

interface Nest {
  xxx: string | null;
  yyy: string | null;
}

interface Xxx {
  aaa: string | null;
  bbb: number | null;
  array: Nest[];
  nest: Nest;
}

type ValidationProps = {
  rules: string | Record<string, any>;
  options: {
    name?: string;
    values?: Record<string, any>;
    names?: Record<string, string>;
    bails?: boolean;
    skipIfEmpty?: boolean;
    isInitial?: boolean;
    customMessages?: Record<string, string>;
  };
};

function isValidationProps(arg: any): arg is ValidationProps {
  return arg !== null && arg !== undefined && arg.rules !== undefined && arg.options !== undefined;
}

type ValidationObject<T> = {
  [P in keyof T]: T[P] extends Array<infer U> //
    ? (i: number) => ValidationObject<U>
    : T[P] extends Record<string, any>
    ? ValidationObject<T[P]>
    : ValidationProps;
};

type ValidationError<T> = {
  [P in keyof T]: T[P] extends Array<infer U> //
    ? ValidationError<U>[]
    : T[P] extends Record<string, any>
    ? ValidationError<T[P]>
    : string[];
};

type Entity<T> = {
  [P in keyof T]: T[P] extends Array<infer U> //
    ? Entity<U>[]
    : T[P] extends Record<string, any>
    ? Entity<T[P]>
    : any;
};

export default Vue.extend({
  computed: {
    validation(): ValidationObjectXxx {
      return {
        value1: {
          rules: 'required|max:10',
          options: { name: 'hoge' },
        },
        value2: _i => ({
          rules: 'required',
          options: { name: 'hoge' },
        }),
      };
    },
  },
  methods: {
    generic<T>(): ValidationObject<T> {
      return {} as any;
    },
    geterror<T>(): ValidationError<T> {
      return {} as any;
    },
    getentity<T>(): Entity<T> {
      return {} as any;
    },
    hoge(): void {
      const val = this.generic<Xxx>();
      // val.array(0).
      const error = this.geterror<Xxx>();
      // error.array[0].
      const entity = this.getentity<Xxx>();
      // entity.array[0].
    },
  },
});

export function autoValidate<T extends Record<string, any>>(entity: Entity<T>, validation: ValidationObject<T>, errors: ValidationError<T>): Promise<boolean> {
  const promises = [] as Promise<boolean>[];
  for (const [key, value] of Object.entries(validation)) {
    if (typeof value === 'function') {
      for (let index = 0; index < (entity[key] as any[]).length; index++) {
        const arrayvalidation = (value as (i: number) => ValidationProps)(index);
        const val = (entity[key] as any[])[index];
        const err = (errors[key] as any[])[index];
        const promise = autoValidate(val as any, arrayvalidation as any, err as any);
        promises.push(promise);
      }
    } else if (isValidationProps(value)) {
      const val = entity[key];
      const promise = validate(val, value.rules, value.options).then(result => {
        (errors[key] as string[]) = result.errors;
        return result.valid;
      });
      promises.push(promise);
    } else {
      const promise = autoValidate(entity[key] as any, value, errors[key] as any);
      promises.push(promise);
    }
  }
  return Promise.all(promises).then(values => values.every(value => value));
}
