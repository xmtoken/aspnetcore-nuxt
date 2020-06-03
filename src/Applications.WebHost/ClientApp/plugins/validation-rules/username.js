const REQUIRED_LENGTH = 8;
const REQUIRED_DIGIT = true;
const REQUIRED_LOWERCASE = true;
const REQUIRED_UPPERCASE = true;

export default {
  message(field) {
    let message = `${field}は`;
    if (REQUIRED_DIGIT || REQUIRED_LOWERCASE || REQUIRED_UPPERCASE) {
      const symbols = [
        //
        REQUIRED_DIGIT ? '数字' : null,
        REQUIRED_LOWERCASE ? '英小文字' : null,
        REQUIRED_UPPERCASE ? '英大文字' : null,
      ];
      message += `${symbols.filter(x => x).join('、')}を含めた`;
    }
    return `${message}${REQUIRED_LENGTH}文字以上で入力してください。`;
  },
  validate(val) {
    const value = val || '';
    if (REQUIRED_LENGTH > value.length) {
      return false;
    }
    if (REQUIRED_DIGIT && !/[0-9]+/.test(value)) {
      return false;
    }
    if (REQUIRED_LOWERCASE && !/[a-z]+/.test(value)) {
      return false;
    }
    if (REQUIRED_UPPERCASE && !/[A-Z]+/.test(value)) {
      return false;
    }
    return true;
  },
};
